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
	import App.Entity.ClientDataObject;
	import App.Entity.DefaultInvoiceRateDataObject;
	import App.Entity.AFEDataObject;
	import App.Entity.ProjectDataObject;
	import App.Entity.AssetDataObject;
	import App.Entity.DefaultBillRateDataObject;
	import App.Entity.InvoiceDataObject;
	import App.Entity.InvoiceItemDataObject;
	
	public class ManagerService
	{
		
		public static function getInstance():ManagerService {
			return new ManagerService ();
		}
		
		private var m_userService:RemoteObject;
		
		public function ManagerService () {
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
        
        public function createInvoice(year:int, month:int, isFirstPart:Boolean, clientId:int, assetId:int, responder:Responder):void {
        	var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
       		userService.CreateInvoice.addEventListener(ResultEvent.RESULT, responder.result);
       		userService.CreateInvoice.addEventListener(FaultEvent.FAULT, responder.fault);
       		userService.CreateInvoice(year, month, isFirstPart, clientId, assetId);
        }
        
        public function removeInvoice(invoiceId:int, responder:Responder = null):void {
        	var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
   		    
   		    if (null != responder) {
       			userService.RemoveInvoice.addEventListener(ResultEvent.RESULT, responder.result);
       			userService.RemoveInvoice.addEventListener(FaultEvent.FAULT, responder.fault);
       		}
       		
       		userService.RemoveInvoice(invoiceId);
        }
        
        public function storeClient(client:ClientDataObject, responder:Responder = null): void {
        	var ratesHash:Array = new Array();
        	var rate:DefaultInvoiceRateDataObject;
        	
        	if (null != client.DefaultRates) {
        		if (0 == client.DefaultRates.length) {
        			client.DefaultRates = null;
        		} else if (0 == client.ClientId) {
        			var i:int = 0;
        			for each (rate in client.DefaultRates) {
        				if (1 == rate.InvoiceItemTypeId) {
        					rate.InvoiceRate /= 8;
        				}
        				
        				rate.Dummy = i;
        				ratesHash[i] = rate;
        				i++;
        			}
        		}
        	}
        	
        	var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
   		    
   		    if (null != responder) {
       			userService.StoreClient.addEventListener(FaultEvent.FAULT, responder.fault);
       		}
       		
       		userService.StoreClient.addEventListener(ResultEvent.RESULT,
       			function(result:ResultEvent):void {
       				var updatedClient:ClientDataObject = ClientDataObject(result.result);
       				
       				client.ClientId = updatedClient.ClientId;
       				
       				if (0 < ratesHash.length) {
       					for each (rate in updatedClient.DefaultRates) {
       						var oldRate:DefaultInvoiceRateDataObject = DefaultInvoiceRateDataObject(ratesHash[rate.Dummy]);
       						oldRate.DefaultInvoiceRateId = rate.DefaultInvoiceRateId;
       						oldRate.ClientId = client.ClientId;
       					}
       				}
       				
       				if (null != responder) {
       					responder.result(result);
       				}
       			}
       		);
       		
       		userService.StoreClient(client);
        }
        
        public function canRemoveClient(clientId:int, responder:Responder):void {
        	var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
   			userService.CanRemoveClient.addEventListener(FaultEvent.FAULT, responder.fault);
       		userService.CanRemoveClient.addEventListener(ResultEvent.RESULT, responder.result);
       		userService.CanRemoveClient(clientId);
        }
        
        public function removeClient(clientId:int, responder:Responder = null):void {
        	var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
   		    
   		    if (null != responder) {
   				userService.RemoveClient.addEventListener(FaultEvent.FAULT, responder.fault);
       			userService.RemoveClient.addEventListener(ResultEvent.RESULT, responder.result);
       		}
       		
       		userService.RemoveClient(clientId);
        }
        
        public function storeAFE(afeInfo:AFEDataObject, responder:Responder = null):void {
        	var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
   		    
   		    if (null != responder) {
   				userService.StoreAFE.addEventListener(FaultEvent.FAULT, responder.fault);
       			userService.StoreAFE.addEventListener(ResultEvent.RESULT, responder.result);
       		}
       		
       		userService.StoreAFE(afeInfo);
        }
        
        public function canRemoveAfe(afe:String, responder:Responder):void {
        	var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
   			userService.CanRemoveAFE.addEventListener(FaultEvent.FAULT, responder.fault);
       		userService.CanRemoveAFE.addEventListener(ResultEvent.RESULT, responder.result);
       		userService.CanRemoveAFE(afe);
        }
        
        public function removeAfe(afe:String, responder:Responder = null):void {
        	var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
   		    
   		    if (null != responder) {
   				userService.RemoveAFE.addEventListener(FaultEvent.FAULT, responder.fault);
       			userService.RemoveAFE.addEventListener(ResultEvent.RESULT, responder.result);
       		}
       		
       		userService.RemoveAFE(afe);
        }
        
        public function storeProject(projectInfo:ProjectDataObject, responder:Responder = null):void {
        	if (null != projectInfo.Assignments) {
        		if (0 == projectInfo.Assignments.length) {
        			projectInfo.Assignments = null;
        		}
        	}
        	
        	var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
   		    
   		    if (null != responder) {
   				userService.StoreProject.addEventListener(FaultEvent.FAULT, responder.fault);
       			userService.StoreProject.addEventListener(ResultEvent.RESULT, responder.result);
       		}
       		
       		userService.StoreProject(projectInfo);
        }
        
        public function canRemoveProject(project:String, responder:Responder):void {
        	var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
   			userService.CanRemoveProject.addEventListener(FaultEvent.FAULT, responder.fault);
       		userService.CanRemoveProject.addEventListener(ResultEvent.RESULT, responder.result);
       		userService.CanRemoveProject(project);
        }
        
        public function removeProject(project:String, responder:Responder = null):void {
        	var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
   		    
   		    if (null != responder) {
   				userService.RemoveProject.addEventListener(FaultEvent.FAULT, responder.fault);
       			userService.RemoveProject.addEventListener(ResultEvent.RESULT, responder.result);
       		}
       		
       		userService.RemoveProject(project);
        }
        
        public function storeAsset(asset:AssetDataObject, responder:Responder = null): void {
        	var ratesHash:Array = new Array();
        	var rate:DefaultBillRateDataObject;
        	
        	if (null != asset.DefaultRates) {
        		if (0 == asset.DefaultRates.length) {
        			asset.DefaultRates = null;
        		} else if (0 == asset.AssetId) {
        			var i:int = 0;
        			for each (rate in asset.DefaultRates) {
        				if (1 == rate.BillItemTypeId) {
        					rate.BillRate /= 8;
        				}
        				
        				rate.Dummy = i;
        				ratesHash[i] = rate;
        				i++;
        			}
        		}
        	}
        	
        	var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
   		    
   		    if (null != responder) {
       			userService.StoreAsset.addEventListener(FaultEvent.FAULT, responder.fault);
       		}
       		
       		userService.StoreAsset.addEventListener(ResultEvent.RESULT,
       			function(result:ResultEvent):void {
       				var updatedAsset:AssetDataObject = AssetDataObject(result.result);
       				
       				asset.AssetId = updatedAsset.AssetId;
       				asset.ChiefAssetId = updatedAsset.ChiefAssetId;
       				
       				if (null != asset.UserInfo) {
       					asset.UserInfo.UserId = updatedAsset.UserInfo.UserId;
       				}
       				
       				if (0 < ratesHash.length) {
       					for each (rate in updatedAsset.DefaultRates) {
       						var oldRate:DefaultBillRateDataObject = DefaultBillRateDataObject(ratesHash[rate.Dummy]);
       						oldRate.DefaultBillRateId = rate.DefaultBillRateId;
       						oldRate.AssetId = asset.AssetId;
       					}
       				}
       				
       				if (null != responder) {
       					responder.result(result);
       				}
       			}
       		);
       		
       		userService.StoreAsset(asset);
        }
        
        public function canRemoveAsset(assetId:int, responder:Responder):void {
        	var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
   			userService.CanRemoveAsset.addEventListener(FaultEvent.FAULT, responder.fault);
       		userService.CanRemoveAsset.addEventListener(ResultEvent.RESULT, responder.result);
       		userService.CanRemoveAsset(assetId);
        }
        
        public function removeAsset(assetId:int, responder:Responder = null):void {
        	var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
   		    
   		    if (null != responder) {
   				userService.RemoveAsset.addEventListener(FaultEvent.FAULT, responder.fault);
       			userService.RemoveAsset.addEventListener(ResultEvent.RESULT, responder.result);
       		}
       		
       		userService.RemoveAsset(assetId);
        }
        
        public function storeDefaultBillRate(rateInfo:DefaultBillRateDataObject):void {
        	var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
   		    
   			userService.StoreDefaultBillRate.addEventListener(ResultEvent.RESULT, function(result:ResultEvent):void {
   				rateInfo.DefaultBillRateId = DefaultBillRateDataObject(result.result).DefaultBillRateId;
   			});
       		
       		userService.StoreDefaultBillRate(rateInfo);
        }
        
        public function storeDefaultInvoiceRate(rateInfo:DefaultInvoiceRateDataObject):void {
        	var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
   		    
   			userService.StoreDefaultInvoiceRate.addEventListener(ResultEvent.RESULT, function(result:ResultEvent):void {
   				rateInfo.DefaultInvoiceRateId = DefaultInvoiceRateDataObject(result.result).DefaultInvoiceRateId;
   			});
       		
       		userService.StoreDefaultInvoiceRate(rateInfo);
        }
        
        public function storeInvoice(invoice:InvoiceDataObject, responder:Responder):void {
        	var itemsHash:Array = new Array();
        	var i:int = 0;
        	for each (var item:InvoiceItemDataObject in invoice.InvoiceItems) {
        		itemsHash[i] = item;
        		i ++;
        	}
        	
        	var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
   			userService.StoreInvoice.addEventListener(ResultEvent.RESULT, function(result:ResultEvent):void {
   				var updatedInvoice:InvoiceDataObject = InvoiceDataObject(result.result);
   				invoice.InvoiceId = updatedInvoice.InvoiceId;
   				for each (var updatedItem:InvoiceItemDataObject in updatedInvoice.InvoiceItems) {
   					var oldItem:InvoiceItemDataObject = InvoiceItemDataObject(itemsHash[updatedItem.Dummy])
   					oldItem.InvoiceId = invoice.InvoiceId;
   				}
   				responder.result(result);
   			});
       		userService.StoreInvoice.addEventListener(FaultEvent.FAULT, responder.fault);
       		userService.StoreInvoice(invoice);
        }
        
        public function checkInvoiceNumber(invoiceId:int, invoiceNumber:String, responder:Responder):void {
        	var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
   			userService.CheckInvoiceNumber.addEventListener(FaultEvent.FAULT, responder.fault);
       		userService.CheckInvoiceNumber.addEventListener(ResultEvent.RESULT, responder.result);
       		userService.CheckInvoiceNumber(invoiceId, invoiceNumber);
        }
        
        public function getOldBills(period:String, responder:Responder = null):void {
			var userService:RemoteObject = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
   		    
   		    if (null != responder) {
       			userService.GetOldBills.addEventListener(ResultEvent.RESULT, responder.result);
       			userService.GetOldBills.addEventListener(FaultEvent.FAULT, responder.fault);
       		}
       		
       		userService.GetOldBills(period);
        }
        
	}
	
}
