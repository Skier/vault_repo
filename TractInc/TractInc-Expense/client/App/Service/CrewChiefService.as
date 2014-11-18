package App.Service
{
	import mx.rpc.Responder;
	import mx.rpc.remoting.RemoteObject;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.events.FaultEvent;
	import App.Entity.BillDataObject;
	import App.Entity.BillItemDataObject;
	import mx.collections.ArrayCollection;
	import App.Entity.BillItemCompositionDataObject;
	import App.Entity.BillItemAttachmentDataObject;
	import App.Entity.UserDataObject;
	import App.Entity.NoteDataObject;
	import App.Domain.Bill;
	import App.Domain.BillItem;
	import App.Entity.BillSubmitDataObject;
	import App.Domain.Note;
	import App.Entity.AssetAssignmentDataObject;
	import App.Domain.RateByAssignment;
	import App.Entity.RateByAssignmentDataObject;
	
	public class CrewChiefService
	{
		
		public static function getInstance():CrewChiefService {
			return new CrewChiefService();
		}
		
		private var m_userService:RemoteObject;
		
		public function CrewChiefService() {
          	m_userService = new RemoteObject("GenericDestination");
	    	m_userService.source = "TractInc.Expense.UserService";
		}
		
		public static function getResponder(onSuccess:Function, onFault:Function):Responder {
			return new Responder(onSuccess, onFault);
		}
		
        public function submitBills(bills:ArrayCollection, responder:Responder):void {
        	var billsToSubmit:Array = new Array();
        	
        	for each (var bill:BillDataObject in bills) {
       			var billInfo:BillSubmitDataObject = new BillSubmitDataObject();
       			billInfo.BillId = bill.BillId;
       			billInfo.Notes = new Array();
       			billInfo.Status = bill.Status;
       			
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
	   			
	   			billInfo.BillItems = new Array();
        		
        		for each (item in bill.BillItems) {
        			var newItem:BillItemDataObject = new BillItemDataObject();
        			newItem.AssetAssignmentId = item.AssetAssignmentId;
        			newItem.BillId = item.BillId;
        			newItem.BillingDate = item.BillingDate;
        			newItem.BillItemCompositionId = item.BillItemCompositionId;
        			newItem.BillItemId = item.BillItemId;
        			newItem.BillItemTypeId = item.BillItemTypeId;
        			newItem.BillRate = item.BillRate;
        			newItem.BillRateTemp = item.BillRate;
        			newItem.Qty = item.Qty;
        			newItem.Status = item.Status;
        			
        			if (null != item.Notes) {
        				if (0 == item.Notes.length) {
        					item.Notes = null;
        				}
        			}
        			
        			billInfo.BillItems.push(newItem);
        		}
        		
        		if (null != billInfo.Attachments) {
        			if (0 == billInfo.Attachments.length) {
        				billInfo.Attachments = null;
        			}
        		}
        		
        		if (null != billInfo.Notes) {
        			if (0 == billInfo.Notes.length) {
        				billInfo.Notes = null;
        			}
        		}
        		
        		if (null != billInfo.BillItems) {
        			if (0 == billInfo.BillItems.length) {
        				billInfo.BillItems = null;
        			}
        		}
        		
        		billsToSubmit.push(billInfo);
        	}
        	
			var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
       		userService.ApproveBills.addEventListener(ResultEvent.RESULT, responder.result);
       		userService.ApproveBills.addEventListener(FaultEvent.FAULT, responder.fault);
       		userService.ApproveBills(billsToSubmit);
        }
        
        public function getBillAttachments(billId:int, responder:Responder):void {
			var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
       		userService.GetBillAttachments.addEventListener(ResultEvent.RESULT, responder.result);
       		userService.GetBillAttachments.addEventListener(FaultEvent.FAULT, responder.fault);
       		userService.GetBillAttachments(billId);
        }
        
        public function canDeleteAssignment(assignmentId:int, responder:Responder):void {
			var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
       		userService.CanDeleteAssignment.addEventListener(ResultEvent.RESULT, responder.result);
       		userService.CanDeleteAssignment.addEventListener(FaultEvent.FAULT, responder.fault);
       		userService.CanDeleteAssignment(assignmentId);
        }
        
        public function deleteAssignment(assignmentId:int, responder:Responder = null):void {
			var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
   		    
   		    if (null != responder) {
       			userService.DeleteAssignment.addEventListener(ResultEvent.RESULT, responder.result);
       			userService.DeleteAssignment.addEventListener(FaultEvent.FAULT, responder.fault);
       		}
       		
       		userService.DeleteAssignment(assignmentId);
        }
        
        public function getOldCrewBills(chiefId:int, period:String, responder:Responder = null):void {
			var userService:RemoteObject = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
   		    
   		    if (null != responder) {
       			userService.GetOldCrewBills.addEventListener(ResultEvent.RESULT, responder.result);
       			userService.GetOldCrewBills.addEventListener(FaultEvent.FAULT, responder.fault);
       		}
       		
       		userService.GetOldCrewBills(chiefId, period);
        }
        
        public function storeAssignment(assignmentInfo:AssetAssignmentDataObject, responder:Responder):void {
        	var dummy:int = 0;
        	var ratesHash:Array = new Array();
        	for each (var rateInfo:RateByAssignmentDataObject in assignmentInfo.Rates) {
        		rateInfo.Dummy = dummy;
        		ratesHash[dummy] = rateInfo;
        		dummy++;
        		
        		if (isNaN(rateInfo.BillRate)) {
        			rateInfo.BillRate = 0;
        		}
        		if (isNaN(rateInfo.InvoiceRate)) {
        			rateInfo.InvoiceRate = 0;
        		}
        	}
        	
			var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
   		    
   			userService.StoreAssignment.addEventListener(ResultEvent.RESULT,
   				function(result:ResultEvent):void {
   					var resultAssignmentInfo:AssetAssignmentDataObject = AssetAssignmentDataObject(result.result);
   					assignmentInfo.AssetAssignmentId = resultAssignmentInfo.AssetAssignmentId;
   					
   					for each (var resultRateInfo:RateByAssignmentDataObject in resultAssignmentInfo.Rates) {
   						RateByAssignmentDataObject(ratesHash[resultRateInfo.Dummy]).RateByAssignmentId = resultRateInfo.RateByAssignmentId;
   						RateByAssignmentDataObject(ratesHash[resultRateInfo.Dummy]).AssetAssignmentId = resultAssignmentInfo.AssetAssignmentId;
   					}
   					
   					responder.result(result);
   				}
   			);
   			userService.StoreAssignment.addEventListener(FaultEvent.FAULT, responder.fault);
       		
       		userService.StoreAssignment(assignmentInfo);
        }
        
	}
	
}
