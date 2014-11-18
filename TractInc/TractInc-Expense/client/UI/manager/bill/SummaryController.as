package UI.manager.bill
{

    import flash.display.DisplayObject;
    import mx.managers.PopUpManager;
    import mx.collections.ArrayCollection;
    import util.ArrayUtil;
    import mx.events.ListEvent;
    import flash.events.MouseEvent;
    import mx.controls.Alert;
    import util.NumberUtil;
    import util.DateUtil;
    import common.StatusesRegistry;
    import flash.events.Event;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.Responder;
    import mx.rpc.events.ResultEvent;
    import mx.events.CollectionEvent;
    import mx.binding.utils.ChangeWatcher;
    import UI.manager.ManagerController;
    import mx.rpc.remoting.RemoteObject;
    import App.Service.ManagerService;
    import App.Entity.BillDataObject;
    import App.Entity.AssetDataObject;
    import App.Entity.BillItemDataObject;
    import App.Entity.NoteDataObject;
    import App.Entity.PeriodDataObject;

    [Bindable]
    public class SummaryController
    {
        
        public var view:SummaryView;
        public var model:SummaryModel;
        public var parentController:ManagerController;
        
		public var assets:Array;

        public function SummaryController(view:SummaryView, parentController:ManagerController): void 
        {
            this.view = view;
            this.parentController = parentController;
        }
        
        public function init():void 
        {
        	model = new SummaryModel();
        	loadBills();
        }
        
        public function Logout():void {
        	if (!changedBillExists()) {
	        	parentController.Logout();
        	} else {
        		Alert.show("You have unsubmitted bills. Please submit them before logout.");
        	}
        }
        
        private function changedBillExists():Boolean 
        {
        	for each (var bill:BillDataObject in model.approvedBills) {
        		if (bill.statusTemp == BillDataObject.BILL_STATUS_CONFIRMED
        				|| bill.statusTemp == BillDataObject.BILL_STATUS_DECLINED) {
        			return true;
        		}
        	}
        	return false;
        }

		private function loadBills():void 
		{
			assets = new Array();
			
			for each (var asset:AssetDataObject in parentController.model.data.Assets) {
				assets[asset.AssetId] = asset;
			}

			model.bills.removeAll();
			
			model.submittedBills.removeAll();
			model.rejectedBills.removeAll();
			model.correctedBills.removeAll();
			model.approvedBills.removeAll();
			model.declinedBills.removeAll();
			model.verifiedBills.removeAll();
			
			model.submittedBills.filterFunction = function(obj:Object):Boolean {
				return BillDataObject(obj).StartDate == DateUtil.formatFromPeriod(PeriodDataObject(view.cbSubmittedPeriods.selectedItem));
			}
			model.submittedBills.refresh();
					
			model.correctedBills.filterFunction = function(obj:Object):Boolean {
				return BillDataObject(obj).StartDate == DateUtil.formatFromPeriod(PeriodDataObject(view.cbCorrectedPeriods.selectedItem));
			}
			model.correctedBills.refresh();
					
			model.declinedBills.filterFunction = function(obj:Object):Boolean {
				return BillDataObject(obj).StartDate == DateUtil.formatFromPeriod(PeriodDataObject(view.cbDeclinedPeriods.selectedItem));
			}
			model.declinedBills.refresh();
					
			model.rejectedBills.filterFunction = function(obj:Object):Boolean {
				return BillDataObject(obj).StartDate == DateUtil.formatFromPeriod(PeriodDataObject(view.cbRejectedPeriods.selectedItem));
			}
			model.rejectedBills.refresh();
					
			for each (var bill:BillDataObject in parentController.model.data.Bills) {
				bill.AssetInfo = AssetDataObject(assets[bill.AssetId]);
				bill.ChiefAssetInfo = AssetDataObject(assets[bill.AssetInfo.ChiefAssetId]);
				
				if ((BillDataObject.BILL_STATUS_NEW == bill.Status)
						|| (BillDataObject.BILL_STATUS_CHANGED == bill.Status)) {
					continue;
				}
				
				bill.toTempFields();
				
				model.bills.addItem(bill);
				
				switch (bill.Status) {
					case BillDataObject.BILL_STATUS_SUBMITTED:
					model.submittedBills.addItem(bill);
					break;
					
					case BillDataObject.BILL_STATUS_REJECTED:
					model.rejectedBills.addItem(bill);
					break;
					
					case BillDataObject.BILL_STATUS_CORRECTED:
					model.correctedBills.addItem(bill);
					break;
					
					case BillDataObject.BILL_STATUS_APPROVED:
					model.approvedBills.addItem(bill);
					break;
					
					case BillDataObject.BILL_STATUS_DECLINED:
					model.declinedBills.addItem(bill);
					break;
					
					case BillDataObject.BILL_STATUS_VERIFIED:
					model.verifiedBills.addItem(bill);
					break;
					
				}
				
/*			for each (var item:BillItem in event.data as ArrayCollection){
				item.toTempFields();
			}!!!!!!!!!!!!!!!!!!!!!!*/
			}
		
			model.notBusy = true;
		}
		
		private function getBillById(id:int):BillDataObject
		{
			for each (var bill:BillDataObject in model.bills) {
				if (bill.BillId == id) {
					return bill;
				}
			}
			
			return null;
		}
		
        public function showDetails(evt:ListEvent):void 
        {
        	var bill:BillDataObject = BillDataObject(evt.currentTarget.selectedItem);
        	DetailView.Open(bill, view, true);
        }
        
        public function showDetailsRO(evt:ListEvent):void 
        {
        	var bill:BillDataObject = BillDataObject(evt.currentTarget.selectedItem);
        	DetailViewRO.Open(bill, view, true);
        }
        
        public function submit():void 
        {
        	if (model.approvedBills.length == 0) {
        		return;
        	}
        	
        	model.notBusy = false;

         	for each (var bill:BillDataObject in model.approvedBills) {
	        	for each (var billItem:BillItemDataObject in bill.BillItems) {
	        		billItem.fromTempFields();
	        	}

         		bill.fromTempFields();
	       	}
	       	
			ManagerService.getInstance().submitBills(model.approvedBills, new Responder(onSaved, onFault));
        }
        
        public function resetBill(bill:BillDataObject):void 
        {
        	for each (var billItem:BillItemDataObject in bill.BillItems) {
        		billItem.toTempFields();
        	}

        	bill.toTempFields();
        	setBillStatusTemp(bill);
        }
        
        public function updateBill(bill:BillDataObject):void 
        {
        	for each (var billItem:BillItemDataObject in bill.BillItems) {
        		billItem.fromTempFields();
        	}
        	
        	setBillStatusTemp(bill);
        	bill.fromTempFields();
        }

        public function loadOldBills(period:PeriodDataObject):void {
        	ManagerService.getInstance().getOldBills(
        		DateUtil.formatFromPeriod(period),
        		new Responder(
        			function(result:ResultEvent):void {
        				model.confirmedBills = new ArrayCollection(result.result as Array);
        			},
        			onFault
        		)
        	);
        }
        
		private function onSaved(evt:ResultEvent):void 
		{
       		for each (var bill:BillDataObject in model.approvedBills) {
       			bill.isSaved = true;
       		}

			init();
		}

		private function setBillStatusTemp(bill:BillDataObject):void 
		{
		
           	var isConfirmed:Boolean = true;
           	var isDeclined:Boolean = false;
            
        	for each (var billItem:BillItemDataObject in bill.BillItems) {
        		if ( billItem.StatusTemp == BillItemDataObject.BILL_ITEM_STATUS_DECLINED ) {
        			isDeclined = true;
        		}
        		if ( billItem.StatusTemp != BillItemDataObject.BILL_ITEM_STATUS_CONFIRMED ) {
        			isConfirmed = false;
        		}
        	}
            
            if (isDeclined) {
            	bill.statusTemp = BillDataObject.BILL_STATUS_DECLINED;
            } else if (isConfirmed) {
            	bill.statusTemp = BillDataObject.BILL_STATUS_CONFIRMED;
            }
        	
		}

        private function onFault(event:FaultEvent):void 
        {
        	model.notBusy = true;
        	Alert.show(event.fault.message);
        }
        
    }

}
