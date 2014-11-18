package com.llsvc.services
{
	import com.llsvc.domain.InvoiceItemAttachment;
	import com.llsvc.domain.vo.invoiceitemattachmentVO;
	
	import flash.events.Event;
	import flash.events.EventDispatcher;
	import flash.events.IEventDispatcher;
	
	import mx.collections.ArrayCollection;
	import mx.rpc.AsyncToken;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.InvokeEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.mxml.RemoteObject;
	
	public class InvoiceItemAttachmentService extends EventDispatcher
	{
		private static var _instance:InvoiceItemAttachmentService;
		
		private var _service:RemoteObject;
		private var localCash:Object;
		private var serviceInvokeCounter:int = 0;
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function InvoiceItemAttachmentService(target:IEventDispatcher=null)
		{
			super(target);
			
			if (_instance != null)
				throw new Error("Singleton !");
				
			localCash = new Object();
			serviceInvokeCounter = 0;
		}
		
		public static function get instance():InvoiceItemAttachmentService 
		{
			if (_instance == null)
				_instance = new InvoiceItemAttachmentService();
			
			return _instance;
		}
		
		public function saveInvoiceItemAttachment(value:InvoiceItemAttachment):AsyncToken 
		{
			if (value == null) throw new Error("Cannot save null InvoiceItemAttachment");
			
			value.isLoaded = false;
			value.isLoading = true;
	        var asyncToken:AsyncToken = service.save(value.toVO());
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:invoiceitemattachmentVO = invoiceitemattachmentVO(event.result);
		            	value.updateFields(result);
		            	updateLocalCash(result);
						value.isLoaded = true;
						value.isLoading = false;
		            },
		            function (event:FaultEvent):void
		            {
						value.isLoaded = false;
						value.isLoading = false;
		            	trace(event.fault.message);
		            }
	            )
	        );
	
	        return asyncToken;
		}
		
		public function getInvoiceItemAttachment(id:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.get(id);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:invoiceitemattachmentVO = invoiceitemattachmentVO(event.result);
		            	updateLocalCash(result);
		            	var attachment:InvoiceItemAttachment = InvoiceItemAttachment(localCash[result.invoiceitemattachmentid]);
	                	attachment.isLoaded = true;
	                	attachment.isLoading = false;

	                	attachment.file = FileService.instance.getLocal(attachment.fileid, true);
		            },
		            function (event:FaultEvent):void
		            {
		            	trace(event.fault.message);
		            }
	            )
	        );
	
	        return asyncToken;
		}
		
		public function deleteInvoiceItemAttachment(id:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.deleteEntity(id);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
	                	InvoiceItemAttachment(localCash[id]).isLoading = false;
		            	localCash[id] = null;
		            },
		            function (event:FaultEvent):void
		            {
		            	trace(event.fault.message);
		            }
	            )
	        );
	
	        return asyncToken;
		}
		
		public function getByUserId(userId:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.getByUserId(userId);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:ArrayCollection = new ArrayCollection(event.result as Array);
		                for each (var item:invoiceitemattachmentVO in result)
		                {
		                	updateLocalCash(item);
			            	var attachment:InvoiceItemAttachment = InvoiceItemAttachment(localCash[item.invoiceitemattachmentid]);
		                	attachment.isLoaded = true;
		                	attachment.isLoading = false;
		                	attachment.file = FileService.instance.getLocal(attachment.fileid, true);
		                }
		                
		                dispatchEvent(new Event("invoiceitemattachmentsByUserIdLoaded"));
		            },
		            function (event:FaultEvent):void
		            {
		            	trace(event.fault.message);
		            }
	            )
	        );
	
	        return asyncToken;
		}
		
		public function getAll():AsyncToken 
		{
	        var asyncToken:AsyncToken = service.getAll();
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:ArrayCollection = new ArrayCollection(event.result as Array);
		                for each (var item:invoiceitemattachmentVO in result)
		                {
		                	updateLocalCash(item);
			            	var attachment:InvoiceItemAttachment = InvoiceItemAttachment(localCash[item.invoiceitemattachmentid]);
		                	attachment.isLoaded = true;
		                	attachment.isLoading = false;
		                	attachment.file = FileService.instance.getLocal(attachment.fileid, true);
		                }
		            },
		            function (event:FaultEvent):void
		            {
		            	trace(event.fault.message);
		            }
	            )
	        );
	
	        return asyncToken;
		}
		
		public function getLocal(invoiceitemattachmentId:int, getFromDB:Boolean = false):InvoiceItemAttachment 
		{
			var result:InvoiceItemAttachment = localCash[invoiceitemattachmentId] as InvoiceItemAttachment;

			if (result == null && getFromDB) 
			{
				result = new InvoiceItemAttachment();
				result.invoiceitemattachmentid = invoiceitemattachmentId;
				localCash[invoiceitemattachmentId] = result;
				getInvoiceItemAttachment(invoiceitemattachmentId);
			}
			
			return result;
		}
		
		private function updateLocalCash(item:invoiceitemattachmentVO):void 
		{
        	var existingItem:InvoiceItemAttachment = localCash[item.invoiceitemattachmentid] as InvoiceItemAttachment;
        	if (existingItem == null) 
        	{
        		existingItem = new InvoiceItemAttachment();
        		localCash[item.invoiceitemattachmentid] = existingItem;
        	}
        	
        	existingItem.updateFields(item);
		}
		
	    private function get service():RemoteObject
	    {
	        if (_service == null) {
	           
	            _service = new RemoteObject( "ColdFusion" );
	            _service.source = "com.llsvc.domain.cfc.invoiceitemattachmentGateway";
	            _service.showBusyCursor = true;
	            
	            _service.addEventListener(InvokeEvent.INVOKE, 
	                function(event:InvokeEvent):void 
	                { 
	                	serviceInvokeCounter++;
	                	serviceIsBusy = true; 
	                });
	            
	            _service.addEventListener(ResultEvent.RESULT,
	                function(event:ResultEvent):void 
	                { 
	                	serviceInvokeCounter--;
	                	if (serviceInvokeCounter <= 0) 
	                	{
	                		serviceInvokeCounter = 0;
	                		serviceIsBusy = false; 
	                	}
	                });
	            
	            _service.addEventListener(FaultEvent.FAULT,
	                function(event:FaultEvent):void 
	                { 
	                	serviceInvokeCounter--;
	                	if (serviceInvokeCounter <= 0) 
	                	{
	                		serviceInvokeCounter = 0;
	                		serviceIsBusy = false; 
	                	}
	                });
	        }
	
	        return _service;
	    }
	    
	}
}