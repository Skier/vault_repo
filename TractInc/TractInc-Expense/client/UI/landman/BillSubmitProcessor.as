package UI.landman
{
	import App.Domain.*;
	import mx.rpc.Responder;
	import weborb.data.DynamicLoadEvent;
	import weborb.data.ActiveCollection;
	import common.StatusesRegistry;
	import mx.rpc.events.FaultEvent;
	import mx.collections.ArrayCollection;
	import flash.display.DisplayObject;
	import mx.core.UIComponent;
	import mx.rpc.remoting.RemoteObject;
	import mx.rpc.events.ResultEvent;
	import App.Entity.BillDataObject;
	import App.Entity.BillItemDataObject;
	import App.Entity.NoteDataObject;
	import App.Entity.BillItemAttachmentDataObject;
	
	public class BillSubmitProcessor
	{
		
		private var _bill:Bill;
		private var _itemsCount:int;
		private var _attachmentsCount:int;
		private var _parent:UIComponent;
		private var _billsToSubmit:Array;
		
		public function BillSubmitProcessor(parent:UIComponent, bill:Bill, billsToSubmit:Array) {
			_billsToSubmit = billsToSubmit;
			_parent = parent;
			_bill = bill;
			_bill.RelatedBillItem.removeAll();
			_bill.RelatedBillItem.IsLoaded = false;
			if (_bill.RelatedBillItem.IsLoaded) {
				onBillItemsLoaded(null);
			} else {
				_bill.RelatedBillItem.addEventListener("loaded", onBillItemsLoaded);
			}
		}
		
        private function onBillItemsLoaded(evt:DynamicLoadEvent):void {
        	_bill.RelatedBillItem.removeEventListener("loaded", onBillItemsLoaded);
        	
        	_attachmentsCount = 0;
        	var hasNotLoaded:Boolean = false;
        	for each (var item:BillItem in _bill.RelatedBillItem) {
	        	if (!item.RelatedBillItemAttachment.IsLoaded) {
	        		hasNotLoaded = true;
	        		_attachmentsCount++;
	        		item.RelatedBillItemAttachment.addEventListener("loaded", onAttachmentsLoaded);
	        	}
	        }
	        
	        if (!hasNotLoaded) {
	        	checkAttachments();
	        }
        }
        
        private function onAttachmentsLoaded(evt:DynamicLoadEvent):void {
        	ActiveCollection(evt.data).removeEventListener("loaded", onAttachmentsLoaded);
        	_attachmentsCount--;
        	
        	if (0 == _attachmentsCount) {
        		checkAttachments();
        	}
        }
        
        private function checkAttachments():void {
		    var items:ActiveCollection = _bill.RelatedBillItem;
		    var attachRequired:ArrayCollection = new ArrayCollection();
		    
        	for each (var item:BillItem in items) {
        		if (item.RelatedBillItemType.IsAttachRequired
        				&& (0 == item.RelatedBillItemAttachment.length)) {
        			attachRequired.addItem(item);
        		}
        	}
        	
        	if (0 < attachRequired.length) {
	            var attachmentView:AttachmentView = AttachmentView.Open(_parent,
	            	new Responder(
	            		function(result:Object):void {
        					processSubmit();
	            		},
	            		function(evt:FaultEvent):void {
	            			// _responder.fault(evt);
	            		})
	            );
	            attachmentView.Controller.init(attachRequired);
    	    } else {
    	    	processSubmit();
    	    }
        }

        private function processSubmit():void {
        	var billInfo:BillDataObject = new BillDataObject();
        	
        	billInfo.BillId = _bill.BillId;
	       	billInfo.DailyBillAmt = 0;
        	billInfo.OtherBillAmt = 0;
    		billInfo.TotalBillAmt = 0;
		    billInfo.TotalDailyBill = 0;
		    // billInfo.Notes = _bill.Notes;
		    billInfo.BillItems = new Array();
		    // billInfo.Notes = new Array();
		    
		    if (_bill.relatedNotes.IsLoaded) {
			    for each (var note:Note in _bill.relatedNotes) {
			    	var noteInfo:NoteDataObject = new NoteDataObject();
			    	noteInfo.NoteId = note.NoteId;
			    	noteInfo.ItemType = note.ItemType;
			    	noteInfo.NoteText = note.NoteText;
			    	noteInfo.Posted = note.Posted;
			    	noteInfo.RelatedItemId = note.RelatedItemId;
			    	noteInfo.SenderId = note.SenderId;
			    	
			    	// billInfo.Notes.push(noteInfo);
			    }
		    }
		    
		    var items:ActiveCollection = _bill.RelatedBillItem;
		    _itemsCount = items.length;
		    
		    if (BillStatus.BILL_STATUS_CHANGED == _bill.Status) {
    			billInfo.Status = BillStatus.BILL_STATUS_CORRECTED;
        	} else if (BillStatus.BILL_STATUS_NEW == _bill.Status) {
        		billInfo.Status = BillStatus.BILL_STATUS_SUBMITTED;
	        } else if (null == _bill.RelatedBillStatus) {
   				billInfo.Status = _bill.Status;
   			}
   			
       		for each (var item:BillItem in items) {
       			var itemInfo:BillItemDataObject = new BillItemDataObject();
       			itemInfo.BillId = _bill.BillId;
       			itemInfo.BillItemId = item.BillItemId;
       			itemInfo.Attachments = null;
       			
       			if (null != item.RelatedBillItemAttachment) {
       				if (item.RelatedBillItemAttachment.IsLoaded) {
       					if (1 == item.RelatedBillItemAttachment.length) {
   							var attachment:BillItemAttachment = BillItemAttachment(item.RelatedBillItemAttachment[0]);
   							
       						if (0 != attachment.BillItemAttachmentId) {
       							// itemInfo.Attachments = new Array();
       							
       							var attachmentInfo:BillItemAttachmentDataObject = new BillItemAttachmentDataObject();
       							attachmentInfo.BillItemAttachmentId = attachment.BillItemAttachmentId;
       							attachmentInfo.BillItemId = attachment.BillItemId;
       							attachmentInfo.FileName = attachment.FileName;
       							attachmentInfo.OriginalFileName = attachment.OriginalFileName;
       							
       							// itemInfo.Attachments.push(attachmentInfo);
       						}
       					} else {
       						/* itemInfo.Attachments = new Array();
       						itemInfo.Attachments.push(null); */
       					}
       				}
       			}
       			
				var amount:Number = item.Qty * item.BillRate;

				if (item.BillItemTypeId == 1) {
					billInfo.TotalDailyBill += item.Qty;
					billInfo.DailyBillAmt += amount;
				} else {
					billInfo.OtherBillAmt += amount;
				}

				billInfo.TotalBillAmt += amount;

       			if (BillItemStatus.BILL_ITEM_STATUS_CHANGED == item.Status) {
       				itemInfo.Status = BillItemStatus.BILL_ITEM_STATUS_CORRECTED;
       			} else if (BillItemStatus.BILL_ITEM_STATUS_NEW == item.Status) {
   					itemInfo.Status = BillItemStatus.BILL_ITEM_STATUS_SUBMITTED;
       			} else {
       				itemInfo.Status = item.Status;
       			}
       			
       			billInfo.BillItems.push(itemInfo);
   			}
   			/*
            var userService:RemoteObject;
            userService = new RemoteObject("GenericDestination");
            userService.source = "TractInc.Expense.UserService";
            userService.SubmitBill.addEventListener(ResultEvent.RESULT, _responder.result);
            userService.SubmitBill.addEventListener(FaultEvent.FAULT, _responder.fault);
            userService.SubmitBill(billInfo); */
            _billsToSubmit.push(billInfo);
        }
        
	}
	
}
