package UI.crew
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
    import mx.rpc.remoting.RemoteObject;

    [Bindable]
    public class SummaryController
    {
        
        public var view:SummaryView;
        public var model:SummaryModel;
        public var mainApp:InvoiceController;
        
		public var assets:Object;
		
        public function SummaryController(view:SummaryView, parent:InvoiceController): void 
        {
            this.view = view;
            mainApp = parent;
        }
        
        public function open():void 
        {
            view.msgPanel.init(mainApp.mainApp.Model.CurrentUser);
        	model = new SummaryModel();
        	loadAssets();
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
        	for each (var bill:Bill in model.approvedBills) {
        		if (bill.statusTemp.Status == BillStatus.BILL_STATUS_CONFIRMED || bill.statusTemp.Status == BillStatus.BILL_STATUS_DECLINED) {
        			return true;
        		}
        	}
        	return false;
        }

        private function loadAssets():void 
        {
            var assetList:ArrayCollection = ActiveRecords.Asset.findByChiefAssetId(mainApp.mainApp.Model.currentAsset.AssetId);
            assetList.addEventListener("loaded", 
            	function(event:DynamicLoadEvent):void {
		        	assets = new Object();
		        	var crewList:ArrayCollection = new ArrayCollection();

		        	for each (var asset:Asset in event.data as ActiveCollection) {
		        		assets[asset.AssetId] = asset;
		        		crewList.addItem(asset);
		        	}
		        	
		        	loadBills(crewList);
            	})
        }
        
		private function loadBills(crewList:ArrayCollection):void 
		{
            var sql:String = "";
            sql += " select * from [Bill] ";
            sql += " where AssetId in ( 0 ";
			for each (var asset:Asset in crewList) {
	            sql += " , ";
	            sql += asset.AssetId.toString();
			}
            sql += " ) ";
            sql += " and Status not in ( '' ";
            sql += " , '";
            sql += BillStatus.BILL_STATUS_NEW;
            sql += "' ";
            sql += " , '";
            sql += BillStatus.BILL_STATUS_CHANGED;
            sql += "' ";
            sql += " ) ";
            
            var bills:ArrayCollection = ActiveRecords.Bill.findBySql(sql, {Monitored:false});
			bills.addEventListener("loaded", 
				function onBillLoaded(event:DynamicLoadEvent):void {
					if (model == null) {
						return;
					}
					model.currentBills.removeAll();
					model.rejectedBills.removeAll();
					model.approvedBills.removeAll();
					model.confirmedBills.removeAll();
					
					for each (var bill:Bill in bills) {
						
						bill.loadNotes();
						
						var asset:Asset = assets[bill.AssetId] as Asset;

						if (asset == null) {
							throw new Error("Asset primary key not found (" + bill.AssetId.toString() + ")");
						}

						bill.RelatedAsset = asset;
						bill.toTempFields();
						
						switch (bill.Status) {
		
							case BillStatus.BILL_STATUS_SUBMITTED:
							model.currentBills.addItem(bill);
							break;
							
							case BillStatus.BILL_STATUS_REJECTED:
							model.rejectedBills.addItem(bill);
							break;
							
							case BillStatus.BILL_STATUS_CORRECTED:
							model.currentBills.addItem(bill);
							break;
							
							case BillStatus.BILL_STATUS_APPROVED:
							model.approvedBills.addItem(bill);
							break;
							
							case BillStatus.BILL_STATUS_DECLINED:
							model.currentBills.addItem(bill);
							break;
							
							case BillStatus.BILL_STATUS_CONFIRMED:
							model.confirmedBills.addItem(bill);
							break;
							
						}
		
					}
		
					model.notBusy = true;

				});
			view.enabled = true;

			view.tnBills.selectedChild = view.viewCurrentBills;
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
        	if (model.currentBills.length == 0) {
        		return;
        	}
        	
        	model.notBusy = false;

         	for each (var bill:Bill in model.currentBills) {

         		for each (var item:BillItem in bill.RelatedBillItem) {
         			item.fromTempFields();
         		}

         		bill.fromTempFields();
         		bill.isSaved = false;
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

       		for each (var b:Bill in model.currentBills) {
       			if (!b.isSaved) {
       				return;
       			}
       		}

			open();
		}

		private function setBillStatusTemp(bill:Bill):void {
		
           	var isApproved:Boolean = true;
           	var isRejected:Boolean = false;
            
        	for each (var billItem:BillItem in bill.RelatedBillItem) {
        		if ( billItem.StatusTemp.Status == BillItemStatus.BILL_ITEM_STATUS_REJECTED ) {
        			isRejected = true;
        		}
        		if ( billItem.StatusTemp.Status != BillItemStatus.BILL_ITEM_STATUS_APPROVED && billItem.StatusTemp.Status != BillItemStatus.BILL_ITEM_STATUS_CONFIRMED) {
        			isApproved = false;
        		}
        	}
            
            if (isRejected) {
            	bill.statusTemp = StatusesRegistry.getInstance().getBillStatusByName(BillStatus.BILL_STATUS_REJECTED);
            } else if (isApproved) {
            	bill.statusTemp = StatusesRegistry.getInstance().getBillStatusByName(BillStatus.BILL_STATUS_APPROVED);
            }
        	
		}

        private function onFault(event:FaultEvent):void 
        {
        	model.notBusy = true;
        	Alert.show(event.fault.message);
        }
        
    }

}
