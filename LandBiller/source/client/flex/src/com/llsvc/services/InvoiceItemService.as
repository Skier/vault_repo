package com.llsvc.services
{
	import com.llsvc.domain.InvoiceItem;
	import com.llsvc.domain.vo.invoiceitemVO;
	
	import flash.events.EventDispatcher;
	import flash.events.IEventDispatcher;
	
	import mx.collections.ArrayCollection;
	import mx.rpc.AsyncToken;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.InvokeEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.mxml.RemoteObject;

	public class InvoiceItemService extends EventDispatcher
	{
		private static var _instance:InvoiceItemService;
		
		private var _service:RemoteObject;
		private var localCash:Object;
		private var serviceInvokeCounter:int = 0;
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function InvoiceItemService(target:IEventDispatcher=null)
		{
			super(target);
			
			if (_instance != null)
				throw new Error("Singleton !");
				
			localCash = new Object();
			serviceInvokeCounter = 0;
		}
		
		public static function get instance():InvoiceItemService 
		{
			if (_instance == null)
				_instance = new InvoiceItemService();
			
			return _instance;
		}
		
		public function saveInvoiceItem(value:InvoiceItem):AsyncToken 
		{
			if (value == null) return null;

			value.isLoaded = false;
			value.isLoading = true;			
	        var asyncToken:AsyncToken = service.save(value.toVO());
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
						value.isLoaded = true;
						value.isLoading = false;			
		            	var result:invoiceitemVO = invoiceitemVO(event.result);
		            	value.updateFields(result);
		            	localCash[value.invoiceitemid] = value;
		            	updateLocalCash(result);
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
		
		public function getInvoiceItem(id:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.get(id);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:invoiceitemVO = invoiceitemVO(event.result);
		            	updateLocalCash(result);
	                	InvoiceItem(localCash[result.invoiceitemid]).isLoaded = true;
	                	InvoiceItem(localCash[result.invoiceitemid]).isLoading = false;
		            },
		            function (event:FaultEvent):void
		            {
		            	trace(event.fault.message);
		            }
	            )
	        );
	
	        return asyncToken;
		}
		
		public function deleteInvoiceItem(id:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.deleteEntity(id);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
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
		
		public function getAll():AsyncToken 
		{
	        var asyncToken:AsyncToken = service.getAll();
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:ArrayCollection = new ArrayCollection(event.result as Array);
		                for each (var item:invoiceitemVO in result)
		                {
		                	updateLocalCash(item);
		                	InvoiceItem(localCash[item.invoiceitemid]).isLoaded = true;
		                	InvoiceItem(localCash[item.invoiceitemid]).isLoading = false;
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
		
		public function getItemsByInvoiceId(invoiceid:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.getByInvoiceId(invoiceid);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:ArrayCollection = new ArrayCollection(event.result as Array);
		                for each (var item:invoiceitemVO in result)
		                {
		                	updateLocalCash(item);
		                	InvoiceItem(localCash[item.invoiceitemid]).isLoaded = true;
		                	InvoiceItem(localCash[item.invoiceitemid]).isLoading = false;
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
		
		public function getItemsByUserId(invoiceid:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.getByUserId(invoiceid);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:ArrayCollection = new ArrayCollection(event.result as Array);
		                for each (var item:invoiceitemVO in result)
		                {
		                	updateLocalCash(item);
		                	InvoiceItem(localCash[item.invoiceitemid]).isLoaded = true;
		                	InvoiceItem(localCash[item.invoiceitemid]).isLoading = false;
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
		
		public function getLocal(invoiceItemId:int, getFromDB:Boolean = false):InvoiceItem 
		{
			var result:InvoiceItem = localCash[invoiceItemId] as InvoiceItem;

			if (result == null && getFromDB) 
			{
				result = new InvoiceItem();
				result.invoiceitemid = invoiceItemId;
				localCash[invoiceItemId] = result;
				getInvoiceItem(invoiceItemId);
			}
			
			return result;
		}
		
		private function updateLocalCash(item:invoiceitemVO):void 
		{
        	var existingItem:InvoiceItem = localCash[item.invoiceitemid] as InvoiceItem;
        	if (existingItem == null) 
        	{
        		existingItem = new InvoiceItem();
        		localCash[item.invoiceitemid] = existingItem;
        	}
        	
        	existingItem.updateFields(item);
		}
		
	    private function get service():RemoteObject
	    {
	        if (_service == null) {
	           
	            _service = new RemoteObject( "ColdFusion" );
	            _service.source = "com.llsvc.domain.cfc.invoiceitemGateway";
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