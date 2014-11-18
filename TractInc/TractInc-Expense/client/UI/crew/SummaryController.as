package UI.crew
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
    import mx.rpc.remoting.RemoteObject;
    import App.Service.CrewChiefService;
    import App.Entity.BillDataObject;
    import App.Entity.BillItemDataObject;
    import App.Entity.AssetDataObject;
    import App.Entity.CrewChiefDataObject;
    import mx.collections.ListCollectionView;
    import App.Entity.PeriodDataObject;

    [Bindable]
    public class SummaryController
    {
        
        public var view:SummaryView;
        public var model:SummaryModel;
        public var mainApp:InvoiceController;
        
		public var assets:Array;
		
        public function SummaryController(view:SummaryView, parent:InvoiceController): void 
        {
            this.view = view;
            mainApp = parent;
        }
        
        public function open():void 
        {
            view.msgPanel.init(mainApp.mainApp.Model.CurrentUser.UserId);
        	model = new SummaryModel();
        	
       		model.data = mainApp.model.data;
       				
        	assets = new Array();
        	var crewList:ArrayCollection = new ArrayCollection();

		    for each (var asset:AssetDataObject in model.data.Assets) {
		    	assets[asset.AssetId] = asset;
		    	crewList.addItem(asset);
		    }
		    
			loadBills(crewList);
        }
        
        public function Logout():void {
        	model = null;
        	if (!changedBillExists()) {
	        	mainApp.Logout();
        	} else {
        		Alert.show("You have unsubmitted bills. Please submit them before logout.");
        	}
        }
        
        private function changedBillExists():Boolean 
        {
        	for each (var bill:BillDataObject in model.approvedBills) {
        		if (bill.statusTemp == BillDataObject.BILL_STATUS_CONFIRMED || bill.statusTemp == BillDataObject.BILL_STATUS_DECLINED) {
        			return true;
        		}
        	}
        	return false;
        }

		private function loadBills(crewList:ArrayCollection):void 
		{
			model.bills.removeAll();
			model.currentBills.removeAll();
			model.rejectedBills.removeAll();
			model.approvedBills.removeAll();
			
			for each (var bill:BillDataObject in model.data.Bills) {
				var asset:AssetDataObject = assets[bill.AssetId] as AssetDataObject;
				bill.AssetInfo = asset;
				
				bill.toTempFields();
				
				model.bills.addItem(bill);
				
				switch (bill.Status) {
					case BillDataObject.BILL_STATUS_SUBMITTED:
						model.currentBills.addItem(bill);
						break;
					
					case BillDataObject.BILL_STATUS_REJECTED:
					 	model.rejectedBills.addItem(bill);
						break;
					
					case BillDataObject.BILL_STATUS_CORRECTED:
						model.currentBills.addItem(bill);
						break;
					
					case BillDataObject.BILL_STATUS_APPROVED:
						model.approvedBills.addItem(bill);
						break;
					
					case BillDataObject.BILL_STATUS_DECLINED:
						model.currentBills.addItem(bill);
						break;
				}
			}
			/*
			model.confirmedBills = new ListCollectionView(model.bills);
			model.confirmedBills.filterFunction = function(obj:Object):Boolean {
				var bill:BillDataObject = BillDataObject(obj);
				return (bill.Status == BillDataObject.BILL_STATUS_CONFIRMED)
					&& ((null == view.cbPeriod.selectedItem)
						|| (DateUtil.formatFromPeriod(PeriodDataObject(view.cbPeriod.selectedItem)) == bill.StartDate));
			}
			model.confirmedBills.refresh(); */
					
			model.notBusy = true;
			
			view.enabled = true;

			view.tnBills.selectedChild = view.viewCurrentBills;
		}
		
		private function getBillById(id:int):BillDataObject {
			for each (var bill:BillDataObject in model.bills) {
				if (bill.BillId == id) {
					return bill;
				}
			}
			
			return null;
		}
		
        public function showDetails(evt:ListEvent):void 
        {
        	var bill:BillDataObject = evt.currentTarget.selectedItem as BillDataObject;
        	DetailView.Open(bill, view, true);
        }
        
        public function showDetailsRO(evt:ListEvent):void 
        {
        	var bill:BillDataObject = evt.currentTarget.selectedItem as BillDataObject;
        	DetailViewRO.Open(bill, view, true);
        }
        
        public function submit():void 
        {
        	if (model.currentBills.length == 0) {
        		return;
        	}
        	
        	model.notBusy = false;
        	
        	var bill:BillDataObject;

        	for each (bill in model.currentBills) {
         		for each (var item:BillItemDataObject in bill.BillItems) {
         			item.fromTempFields();
         		}

         		bill.fromTempFields();
			}
         	
			CrewChiefService.getInstance().submitBills(model.currentBills, new Responder(onSaved, onFault));
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

		private function onSaved(evt:ResultEvent):void 
		{
       		for each (var b:BillDataObject in model.currentBills) {
       			b.isSaved = true;
       			
       			if (BillDataObject.BILL_STATUS_REJECTED == b.Status) {
		        	var userService:RemoteObject = new RemoteObject("GenericDestination");
        			userService.source = "TractInc.Expense.UserService";
        			userService.SendRejectionNotify.addEventListener(FaultEvent.FAULT, onFault);
        			userService.SendRejectionNotify(b.BillId, false);
       			}
       		}

			open();
		}

		private function setBillStatusTemp(bill:BillDataObject):void {
           	var isApproved:Boolean = true;
           	var isRejected:Boolean = false;
            
        	for each (var billItem:BillItemDataObject in bill.BillItems) {
        		if ( billItem.StatusTemp == BillItemDataObject.BILL_ITEM_STATUS_REJECTED ) {
        			isRejected = true;
        		}
        		if ( billItem.StatusTemp != BillItemDataObject.BILL_ITEM_STATUS_APPROVED
        				&& billItem.StatusTemp != BillItemDataObject.BILL_ITEM_STATUS_CONFIRMED) {
        			isApproved = false;
        		}
        	}
            
            if (isRejected) {
            	bill.statusTemp = BillDataObject.BILL_STATUS_REJECTED;
            } else if (isApproved) {
            	bill.statusTemp = BillDataObject.BILL_STATUS_APPROVED;
            }
		}

        private function onFault(event:FaultEvent):void 
        {
        	model.notBusy = true;
        	Alert.show(event.fault.message);
        }
        
        public function loadOldCrewBills():void {
        	CrewChiefService.getInstance().getOldCrewBills(
        		mainApp.mainApp.Model.currentAsset.AssetId,
        		DateUtil.formatFromPeriod(PeriodDataObject(view.cbPeriod.selectedItem)),
        		new Responder(
        			function(result:ResultEvent):void {
        				model.confirmedBills = new ArrayCollection(result.result as Array);
        			},
        			onFault
        		)
        	);
        }
        
    }

}
