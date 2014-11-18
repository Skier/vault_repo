package UI.landman
{
	
    import mx.collections.ArrayCollection;
    import common.StatusesRegistry;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;
    import mx.managers.PopUpManager;
    import mx.rpc.remoting.RemoteObject;
    import mx.rpc.events.ResultEvent;
    import common.TypesRegistry;
    import App.Service.LandmanService;
    import App.Entity.BillSubmitDataObject;
    import App.Entity.NoteDataObject;
    import App.Entity.BillItemAttachmentDataObject;
    import App.Entity.BillDataObject;
    import App.Entity.BillItemDataObject;
    import App.Entity.BillItemCompositionDataObject;
    import App.Entity.BillItemTypeDataObject;
    import App.Entity.LandmanDataObject;

    [Bindable]
    public class SubmitController
    {
        
        public var view: SubmitView;
        public var Model: SubmitModel = new SubmitModel();
        public var mainApp: DiaryController;
        public var mainModel: DiaryModel;
        
       	public var itemsRequireAttachments:Array = new Array();
       	public var compositesRequireAttachments:Array = new Array();
        	
        public function SubmitController(view: SubmitView, parent: DiaryController): void {
            this.view = view;
            mainApp = parent;
            mainModel = mainApp.Model;
        }
        
        public function submitBills():void {
        	var billsToSubmit:Array = new Array();
        	for each (var bill:BillDataObject in Model.bills) {
        		if (bill.toSubmit) {
        			billsToSubmit.push(bill);
        		}
        	}
        	
        	if (0 == billsToSubmit.length) {
        		return;
        	}
        	
        	view.enabled = false;
        	
        	LandmanService.getInstance().getLoadedBills(billsToSubmit, new Responder(
        		function(result:ResultEvent):void {
        			view.enabled = true;
        			Model.bills = new ArrayCollection(result.result as Array);
        			
        			var billsToSubmit:Array = prepareBillsToSubmit();
        			
        			if ((0 < itemsRequireAttachments.length)
        					|| (0 < compositesRequireAttachments.length)) {
        				var attachmentsPopup:AttachmentView = AttachmentView.Open(view,
        					function():void {
        						billsToSubmit = prepareBillsToSubmit();
        						callSubmitService(billsToSubmit);
        					}
        				);
        				attachmentsPopup.Controller.Model.items = new ArrayCollection(itemsRequireAttachments);
        				attachmentsPopup.Controller.Model.compositeItems = new ArrayCollection(compositesRequireAttachments);
        				attachmentsPopup.Controller.Model.assignmentsByIdHash = mainApp.Model.assignmentsByIdHash;
   					} else {
						callSubmitService(billsToSubmit);
       				}
        		},
        		onFault
        	));
        }
        
        private function prepareBillsToSubmit():Array {
        	itemsRequireAttachments = new Array();
        	compositesRequireAttachments = new Array();
        	
        	var billsToSubmit:Array = new Array();
        	
        	for each (var bill:BillDataObject in Model.bills) {
       			var billInfo:BillSubmitDataObject = new BillSubmitDataObject();
       			billInfo.BillId = bill.BillId;
       			billInfo.Notes = new Array();
       			billInfo.Attachments = new Array();
   				for each (var note:NoteDataObject in bill.Notes) {
   					var noteInfo:NoteDataObject = new NoteDataObject();
   					noteInfo.ItemType = NoteDataObject.NOTE_TYPE_BILL;
   					noteInfo.NoteText = note.NoteText;
					noteInfo.NoteId = note.NoteId;
   					noteInfo.Posted = note.Posted;
   					noteInfo.SenderId = note.SenderId;
   					noteInfo.RelatedItemId = note.RelatedItemId;
   					
   					billInfo.Notes.push(noteInfo);
   				}
    			
	   			var item:BillItemDataObject;
        		
        		var attachmentInfo:BillItemAttachmentDataObject;
        		
        		for each (item in bill.BillItems) {
        			if (null != item.Notes) {
        				if (0 == item.Notes.length) {
        					item.Notes = null;
        				}
        			}
        			
        			if (null == item.AttachmentInfo) {
        				if (0 != item.BillItemCompositionId) {
        					continue;
        				}
        				if (BillItemTypeDataObject(TypesRegistry.instance.getAllBillItemTypes.getItemAt(item.BillItemTypeId - 1)).IsAttachRequired) {
        					item.toTempFields();
        					itemsRequireAttachments.push(item);
        					continue;
        				}
        			}
        			
        			if (null != item.AttachmentInfo) {
       					billInfo.Attachments.push(item.AttachmentInfo.clone());
       				}
        		}
        		
        		for each (var composition:BillItemCompositionDataObject in bill.Compositions) {
        			if (null == composition.AttachmentInfo) {
        				if (BillItemTypeDataObject(TypesRegistry.instance.getAllBillItemTypes.getItemAt(composition.BillItemTypeId - 1)).IsAttachRequired) {
        					compositesRequireAttachments.push(composition);
        					continue;
        				}
        			} else {
        				for each (item in composition.BillItems) {
       						billInfo.Attachments.push(item.AttachmentInfo.clone());
       					}
       				}
        		}
        		
        		if (0 == billInfo.Attachments.length) {
        			billInfo.Attachments = null;
        		}
        		
        		if (0 == billInfo.Notes.length) {
        			billInfo.Notes = null;
        		}
        		
        		billsToSubmit.push(billInfo);
        	}
        	
        	return billsToSubmit;
        }
        
        public function callSubmitService(billsToSubmit:Array):void {
        	view.enabled = false;
			var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
       		userService.SubmitBills.addEventListener(ResultEvent.RESULT, onSuccess);
       		userService.SubmitBills.addEventListener(FaultEvent.FAULT, onFault);
       		userService.SubmitBills(billsToSubmit);
        }
        
        public function selectAll(flag:Boolean):void {
        	for each (var bill:BillDataObject in Model.bills) {
        		bill.toSubmit = flag;
        	}
        }
        
        private function onFault(event:FaultEvent):void {
        	view.enabled = true;
        	if (null != event) {
        		Alert.show(event.fault.message);
        	}
        }
        
        private function onSuccess(result:ResultEvent):void {
    		PopUpManager.removePopUp(view);
    		
            LandmanService.getInstance().getLandmanData(mainApp.Model.asset.AssetId, new Responder(
            	OnBillsLoaded,
            	OnBillsLoadFailed
            ));
        }
        
        private function OnBillsLoaded(evt:ResultEvent):void {
			mainApp.open(mainApp.Model.asset, LandmanDataObject(evt.result), mainApp.Model.mainModel);
        }

        private function OnBillsLoadFailed(fault:FaultEvent):void {
            view.enabled = true;
            Alert.show("Cannot load bills", "System error");
        }

    }

}
