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
	import App.Domain.BillItemComposition;
	
	public class LandmanService
	{
		
		public static function getInstance():LandmanService {
			return new LandmanService();
		}
		
		private var m_userService:RemoteObject;
		
		public function LandmanService() {
          	m_userService = new RemoteObject("GenericDestination");
	    	m_userService.source = "TractInc.Expense.UserService";
		}
		
		public function getBill(billId:int, responder:Responder):void {
       		m_userService.GetBill.addEventListener(ResultEvent.RESULT,
       			function(result:ResultEvent):void {
       				var billInfo:BillDataObject = BillDataObject(result.result);
       				processEntireBill(billInfo);
       				
       				responder.result(result);
       			}
       		);
       		m_userService.GetBill.addEventListener(FaultEvent.FAULT, responder.fault);
       		m_userService.GetBill(billId);
		}
		
		public function getLoadedBills(bills:Array, responder:Responder):void {
			var billsToLoad:Array = new Array();
			
			for each (var billInfo:BillDataObject in bills) {
				var billInfoNew:BillDataObject = new BillDataObject();
				billInfoNew.BillId = billInfo.BillId;
				billsToLoad.push(billInfoNew);
			}
			
       		m_userService.GetLoadedBills.addEventListener(ResultEvent.RESULT,
       			function(result:ResultEvent):void {
       				var loadedBills:Array = result.result as Array;
       				
       				for each (var loadedBillInfo:BillDataObject in loadedBills) {
       					processEntireBill(billInfo);
       				}
       				
       				responder.result(result);
       			}
       		);
       		m_userService.GetLoadedBills.addEventListener(FaultEvent.FAULT, responder.fault);
       		m_userService.GetLoadedBills(billsToLoad);
		}
		
		private function processEntireBill(billInfo:BillDataObject):void {
			var itemsByComposition:Array = new Array();
			var itemsList:Array;
			for each (var item:BillItemDataObject in billInfo.BillItems) {
				if (null == itemsByComposition[item.BillItemCompositionId]) {
					itemsList = new Array();
					itemsByComposition[item.BillItemCompositionId] = itemsList;
				} else {
					itemsList = itemsByComposition[item.BillItemCompositionId] as Array;
				}
				
				itemsList.push(item);
			}
			
			for each (var composition:BillItemCompositionDataObject in billInfo.Compositions) {
				if (null == itemsByComposition[composition.BillItemCompositionId]) {
					composition.BillItems = new Array();
				} else {
					composition.BillItems = itemsByComposition[composition.BillItemCompositionId] as Array;
					
					if (0 < composition.BillItems.length) {
						composition.AttachmentInfo = BillItemDataObject((new ArrayCollection(composition.BillItems)).getItemAt(0)).AttachmentInfo;
					}
				}
			}
		}
		
		public function getLandmanData(assetId:int, responder:Responder):void {
       		m_userService.GetLandmanData.addEventListener(ResultEvent.RESULT, responder.result);
       		m_userService.GetLandmanData.addEventListener(FaultEvent.FAULT, responder.fault);
       		m_userService.GetLandmanData(assetId);
		}
		
		public function storeBillItems(bill:BillDataObject, billItems:ArrayCollection, responder:Responder):void {
			var item:BillItemDataObject;
			
			bill.BillItems = null;
			
			if (null != bill.Notes) {
				if (0 == bill.Notes.length) {
					bill.Notes = null;
				}
			}
			
			if (null != bill.Compositions) {
				if (0 == bill.Compositions.length) {
					bill.Compositions = null;
				}
			}
			
			var itemsByDummyId:Array = new Array();
			for each (item in billItems) {
				item.isSaved = false;
				
				item.Dummy = billItems.getItemIndex(item);
				itemsByDummyId[item.Dummy] = item;
				
				if (null != item.Notes) {
					if (0 == item.Notes.length) {
						item.Notes = null;
					}
				}
			}
			
			m_userService.StoreBillItems.addEventListener(ResultEvent.RESULT,
				function(result:ResultEvent):void {
					var updatedBill:BillDataObject = BillDataObject(result.result);
					
					if (0 == updatedBill.BillItems.length) {
						billItems.removeAll();
					} else {
						bill.BillItems = new Array();
						for each (var updatedItem:BillItemDataObject in updatedBill.BillItems) {
							var oldItem:BillItemDataObject = BillItemDataObject(itemsByDummyId[updatedItem.Dummy]);
							
							if (oldItem.IsMarkedToRemove) {
								billItems.removeItemAt(billItems.getItemIndex(oldItem));
								continue;
							} else {
								bill.BillItems.push(oldItem);
							}
							
							oldItem.BillItemId = updatedItem.BillItemId;
							oldItem.Status = updatedItem.Status;
							oldItem.toTempFields();
							
							if (1 == oldItem.BillItemTypeId) {
								oldItem.WorkLogInfo.BillItemId = updatedItem.BillItemId;
								oldItem.WorkLogInfo.WorkLogId = updatedItem.WorkLogInfo.WorkLogId;
							}
							
    	   					oldItem.isSaved = true;
						}
					}
					
					responder.result(result);
				}
			);
       		m_userService.StoreBillItems.addEventListener(FaultEvent.FAULT, responder.fault);
       		m_userService.StoreBillItems(bill, billItems.toArray());
		}
		
		public function storeComposition(compositionInfo:BillItemCompositionDataObject, responder:Responder):void {
			var item:BillItemDataObject;
			
			var itemsByDummyId:Array = new Array();
			for each (item in compositionInfo.BillItems) {
				item.isSaved = false;
				
				item.Dummy = compositionInfo.BillItems.indexOf(item);
				itemsByDummyId[item.Dummy] = item;
				
				if (null != item.Notes) {
					if (0 == item.Notes.length) {
						item.Notes = null;
					}
				}
			}
			
			if (null != compositionInfo.Notes) {
				if (0 == compositionInfo.Notes.length) {
					compositionInfo.Notes = null;
				}
			}
			
			m_userService.StoreComposition.addEventListener(ResultEvent.RESULT,
				function(result:ResultEvent):void {
					var updatedComposition:BillItemCompositionDataObject = BillItemCompositionDataObject(result.result);
					compositionInfo.BillItemCompositionId = updatedComposition.BillItemCompositionId;
					
					if (0 == updatedComposition.BillItems.length) {
						compositionInfo.BillItems.removeAll();
					} else {
						for each (var updatedItem:BillItemDataObject in updatedComposition.BillItems) {
							var oldItem:BillItemDataObject = BillItemDataObject(itemsByDummyId[updatedItem.Dummy]);
							
							if (oldItem.IsMarkedToRemove) {
								compositionInfo.BillItems.splice(compositionInfo.BillItems.indexOf(oldItem), 1);
								continue;
							}
							
							oldItem.BillItemId = updatedItem.BillItemId;
							oldItem.BillItemCompositionId = updatedComposition.BillItemCompositionId;
							oldItem.Status = updatedItem.Status;
							oldItem.AttachmentInfo = updatedItem.AttachmentInfo;
							oldItem.toTempFields();
							
    	   					oldItem.isSaved = true;
						}
					}
					
					compositionInfo.Notes = updatedComposition.Notes;
					
					responder.result(result);
				}
			);
       		m_userService.StoreComposition.addEventListener(FaultEvent.FAULT, responder.fault);
       		m_userService.StoreComposition(compositionInfo);
		}
		
		public function storeCompositions(compositions:Array, responder:Responder):void {
			var compositionInfo:BillItemCompositionDataObject;
			
			var compositionsByDummyId:Array = new Array();
			for each (compositionInfo in compositions) {
				compositionInfo.Dummy = compositions.indexOf(compositionInfo);
				compositionsByDummyId[compositionInfo.Dummy] = compositionInfo;
				
				var item:BillItemDataObject;
				
				var itemsByDummyId:Array = new Array();
				for each (item in compositionInfo.BillItems) {
					item.isSaved = false;
					
					item.Dummy = compositionInfo.BillItems.indexOf(item);
					itemsByDummyId[item.Dummy] = item;
				
					if (null != item.Notes) {
						if (0 == item.Notes.length) {
							item.Notes = null;
						}
					}
				}
			}
			
			m_userService.StoreCompositions.addEventListener(ResultEvent.RESULT,
				function(result:ResultEvent):void {
					var updatedCompositions:Array = result.result as Array;
					
					for each (var updatedComposition:BillItemCompositionDataObject in updatedCompositions) {
						compositionInfo = BillItemCompositionDataObject(compositionsByDummyId[updatedComposition.Dummy]);
						
						compositionInfo.BillItemCompositionId = updatedComposition.BillItemCompositionId;
						
						if (0 == updatedComposition.BillItems.length) {
							compositionInfo.BillItems.removeAll();
						} else {
							for each (var updatedItem:BillItemDataObject in updatedComposition.BillItems) {
								var oldItem:BillItemDataObject = BillItemDataObject(itemsByDummyId[updatedItem.Dummy]);
								
								if (oldItem.IsMarkedToRemove) {
									compositionInfo.BillItems.splice(compositionInfo.BillItems.indexOf(oldItem), 1);
									continue;
								}
								
								oldItem.BillItemId = updatedItem.BillItemId;
								oldItem.BillItemCompositionId = updatedComposition.BillItemCompositionId;
								oldItem.Status = updatedItem.Status;
								oldItem.AttachmentInfo = updatedItem.AttachmentInfo;
								oldItem.toTempFields();
								
	    	   					oldItem.isSaved = true;
							}
						}
					}
					
					responder.result(result);
				}
			);
       		m_userService.StoreCompositions.addEventListener(FaultEvent.FAULT, responder.fault);
       		m_userService.StoreCompositions(compositions);
		}
		
		public function removeComposition(compositionInfo:BillItemCompositionDataObject, responder:Responder):void {
			m_userService.RemoveComposition.addEventListener(ResultEvent.RESULT,
				function(result:ResultEvent):void {
					if (null != responder) {
						responder.result(result);
					}
				}
			);
			if (null != responder) {
       			m_userService.RemoveComposition.addEventListener(FaultEvent.FAULT, responder.fault);
   			}
       		m_userService.RemoveComposition(compositionInfo);
		}
		
		public function updateBillStatus(billId:int, responder:Responder):void {
			m_userService.UpdateBillStatus.addEventListener(ResultEvent.RESULT, responder.result);
       		m_userService.UpdateBillStatus.addEventListener(FaultEvent.FAULT, responder.fault);
       		m_userService.UpdateBillStatus(billId);
		}
		
		public function storeNotes(notes:Array, responder:Responder):void {
			if (null == notes) {
				return;
			} else if (0 == notes.length) {
				return;
			}
			
			var noteInfo:NoteDataObject;
			
			var notesByDummyId:Array = new Array();
			for each (noteInfo in notes) {
				noteInfo.Dummy = notes.indexOf(noteInfo);
				notesByDummyId[noteInfo.Dummy] = noteInfo;
			}
			
			m_userService.StoreNotes.addEventListener(ResultEvent.RESULT,
				function(result:ResultEvent):void {
					var updatedNotes:Array = result.result as Array;
					
					if (0 == notes.length) {
						notes.removeAll();
					} else {
						for each (var updatedNoteInfo:NoteDataObject in updatedNotes) {
							var oldNoteInfo:NoteDataObject = NoteDataObject(notesByDummyId[updatedNoteInfo.Dummy]);
							
							oldNoteInfo.NoteId = updatedNoteInfo.NoteId;
						}
					}
					
					responder.result(result);
				}
			);
       		m_userService.StoreNotes.addEventListener(FaultEvent.FAULT, responder.fault);
       		m_userService.StoreNotes(notes);
		}
		
		public static function getResponder(onSuccess:Function, onFault:Function):Responder {
			return new Responder(onSuccess, onFault);
		}
		
	}
}
