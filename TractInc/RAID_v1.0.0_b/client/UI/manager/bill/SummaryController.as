package UI.manager.bill
{

    import flash.display.DisplayObject;
    import App.Domain.*;
    import mx.managers.PopUpManager;
    import weborb.data.DynamicLoadEvent;
    import mx.collections.ArrayCollection;
    import util.ArrayUtil;
    import weborb.data.ActiveCollection;
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

    [Bindable]
    public class SummaryController
    {
        
        public var view:SummaryView;
        public var model:SummaryModel;
        public var parentController:ManagerController;
        
		public var assets:Object;

        public function SummaryController(view:SummaryView, parentController:ManagerController): void 
        {
            this.view = view;
            this.parentController = parentController;
        }
        
        public function init():void 
        {
        	model = new SummaryModel();
        	loadAssets();
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
        	for each (var bill:Bill in model.approvedBills) {
        		if (bill.statusTemp.Status == BillStatus.BILL_STATUS_CONFIRMED || bill.statusTemp.Status == BillStatus.BILL_STATUS_DECLINED) {
        			return true;
        		}
        	}
        	return false;
        }

		private function loadAssets():void 
		{
			var assetList:ArrayCollection = ActiveRecords.Asset.findAll();
			assetList.addEventListener("loaded", 
				function(event:DynamicLoadEvent):void {
					assets = new Object();
		
					for each (var asset:Asset in event.data as ActiveCollection) {
						assets[asset.AssetId] = asset;
					}
		
					loadBills();
				});
		}
		
		private function loadBills():void 
		{
            var sql:String = "";
            sql += " select * from [Bill] ";
            sql += " where Status not in ( '' ";
            sql += " , '";
            sql += BillStatus.BILL_STATUS_NEW;
            sql += "' ";
            sql += " , '";
            sql += BillStatus.BILL_STATUS_CHANGED;
            sql += "' ";
            sql += " ) ";

			var bills:ArrayCollection = ActiveRecords.Bill.findBySql(sql);
			bills.addEventListener("loaded", 
				function onBillLoaded(event:DynamicLoadEvent):void {
					model.submittedBills.removeAll();
					model.rejectedBills.removeAll();
					model.correctedBills.removeAll();
					model.approvedBills.removeAll();
					model.declinedBills.removeAll();
					model.confirmedBills.removeAll();
					model.verifiedBills.removeAll();
					
					for each (var bill:Bill in bills) {
		
						var asset:Asset = assets[bill.AssetId] as Asset;
						if (asset == null) {
							throw new Error("Asset primary key not found (" + bill.AssetId.toString() + ")");
						}
						
						bill.RelatedAsset = asset;
						bill.toTempFields();
		
						bill.loadNotes();
		
						switch (bill.Status) {
		
							case BillStatus.BILL_STATUS_SUBMITTED:
							model.submittedBills.addItem(bill);
							break;
							
							case BillStatus.BILL_STATUS_REJECTED:
							model.rejectedBills.addItem(bill);
							break;
							
							case BillStatus.BILL_STATUS_CORRECTED:
							model.correctedBills.addItem(bill);
							break;
							
							case BillStatus.BILL_STATUS_APPROVED:
							model.approvedBills.addItem(bill);
//							bill.RelatedBillItem.addEventListener("loaded", onBillItemsLoaded);
							break;
							
							case BillStatus.BILL_STATUS_DECLINED:
							model.declinedBills.addItem(bill);
							break;
							
							case BillStatus.BILL_STATUS_CONFIRMED:
							model.confirmedBills.addItem(bill);
							break;
							
							case BillStatus.BILL_STATUS_VERIFIED:
							model.verifiedBills.addItem(bill);
							break;
							
						}
		
					}
		
					model.notBusy = true;

				});
		}
		
		private function onBillItemsLoaded(event:DynamicLoadEvent):void 
		{
			for each (var item:BillItem in event.data as ArrayCollection){
				item.toTempFields();
			}
		}
		
        public function showDetails(evt:ListEvent):void 
        {
        	var bill:Bill = evt.currentTarget.selectedItem as Bill;
        	DetailView.Open(bill, view, true);
        }
        
        public function showDetailsRO(evt:ListEvent):void 
        {
        	var bill:Bill = evt.currentTarget.selectedItem as Bill;
        	DetailViewRO.Open(bill, view, true);
        }
        
        public function submit():void 
        {
        	if (model.approvedBills.length == 0) {
        		return;
        	}
        	
        	model.notBusy = false;

         	for each (var bill:Bill in model.approvedBills) {

	        	for each (var billItem:BillItem in bill.RelatedBillItem) {
	        		billItem.fromTempFields();
	        	}

         		bill.fromTempFields();
       			bill.save(true, new Responder(onSaved, onFault));
       			
       			if (BillStatus.BILL_STATUS_REJECTED == bill.Status) {
       				sendEmail(bill);
       			}
	       	}
        }
        
        private var body:String;
        
        private function sendEmail(bill:Bill):void {
        	body = "Notes History\n" + "-------------\n\n";
        	for each (var item:BillItem in bill.RelatedBillItem) {
        		if (BillItemStatus.BILL_ITEM_STATUS_REJECTED == item.Status) {
        			// item.loadNotes();
        			// item.Notes.
        			// notesCounter ++;
        			body += "Bill item type: " + item.RelatedBillItemType.Name
        				+ ", AFE: " + item.RelatedAssetAssignment.AFE
        				+ ", Project: " + item.RelatedAssetAssignment.SubAFE + "\n";
        			for each (var note:Note in item.relatedNotes) {
        				body += DateUtil.format(note.Posted) + " " + note.NoteText + "\n";
        			}
        			body += "\n";
        		}
        	}
        	body += "\n";
        	
        	body += "Bill notes History\n" + "------------------\n";
        	body += "Bill start date: " + bill.StartDate + "\n\n";
        	// bill.loadNotes();
        	for each (var billNote:Note in bill.relatedNotes) {
       			body += DateUtil.format(note.Posted) + " " + note.NoteText + "\n";
        	}
        	
        	var userAssets:ActiveCollection = ActiveRecords.UserAsset.findByAssetId(bill.AssetId);
        	userAssets.addEventListener("loaded", onUserAssetsLoaded);
        }
        
        private function onUserAssetsLoaded(evt:DynamicLoadEvent):void {
        	ActiveCollection(evt.data).removeEventListener("loaded", onUserAssetsLoaded);
        	
        	var baseService:RemoteObject = new RemoteObject("GenericDestination");
        	baseService.source = "TractInc.Expense.BaseService";
        	baseService.SendEmail.addEventListener(FaultEvent.FAULT, onFault);
        	
        	var user:User = UserAsset(ActiveCollection(evt.data).getItemAt(0)).RelatedUser;
        	baseService.SendEmail(user.Email, "Bill rejected", body);
        }
        
        public function resetBill(bill:Bill):void 
        {
        	for each (var billItem:BillItem in bill.RelatedBillItem) {
        		billItem.toTempFields();
        	}

        	bill.toTempFields();
        	setBillStatusTemp(bill);
        }
        
        public function updateBill(bill:Bill):void 
        {
        	for each (var billItem:BillItem in bill.RelatedBillItem) {
        		billItem.fromTempFields();
        	}
        	
        	setBillStatusTemp(bill);
        	bill.fromTempFields();
        }

		private function onSaved(bill:Bill):void 
		{

       		bill.isSaved = true;

       		for each (var b:Bill in model.approvedBills) {
       			if (!b.isSaved) {
       				return;
       			}
       		}

			init();
		}

		private function setBillStatusTemp(bill:Bill):void 
		{
		
           	var isConfirmed:Boolean = true;
           	var isDeclined:Boolean = false;
            
        	for each (var billItem:BillItem in bill.RelatedBillItem) {
        		if ( billItem.StatusTemp.Status == BillItemStatus.BILL_ITEM_STATUS_DECLINED ) {
        			isDeclined = true;
        		}
        		if ( billItem.StatusTemp.Status != BillItemStatus.BILL_ITEM_STATUS_CONFIRMED ) {
        			isConfirmed = false;
        		}
        	}
            
            if (isDeclined) {
            	bill.statusTemp = StatusesRegistry.getInstance().getBillStatusByName(BillStatus.BILL_STATUS_DECLINED);
            } else if (isConfirmed) {
            	bill.statusTemp = StatusesRegistry.getInstance().getBillStatusByName(BillStatus.BILL_STATUS_CONFIRMED);
            }
        	
		}

        private function onFault(event:FaultEvent):void 
        {
        	model.notBusy = true;
        	Alert.show(event.fault.message);
        }
        
    }

}
