package com.llsvc.services
{
	import com.llsvc.domain.Invoice;
	import com.llsvc.domain.InvoiceItem;
	import com.llsvc.domain.vo.invoiceVO;
	import com.llsvc.domain.vo.invoiceitemVO;
	
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

	public class InvoiceService extends EventDispatcher
	{
		private static var _instance:InvoiceService;
		
		private var _service:RemoteObject;
		private var localCash:Object;
		private var serviceInvokeCounter:int = 0;
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function InvoiceService(target:IEventDispatcher=null)
		{
			super(target);
			
			if (_instance != null)
				throw new Error("Singleton !");
				
			localCash = new Object();
			serviceInvokeCounter = 0;
		}
		
		public static function get instance():InvoiceService 
		{
			if (_instance == null)
				_instance = new InvoiceService();
			
			return _instance;
		}
		
		public function saveInvoice(value:Invoice):AsyncToken 
		{
			if (value == null) return null;
			
			value.isLoaded = false;
			value.isLoading = true;
	        var asyncToken:AsyncToken = service.save(value.toVO());
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:invoiceVO = invoiceVO(event.result);
		            	value.updateFields(result);
		            	localCash[value.invoiceid] = value;
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
		
		public function getInvoice(id:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.get(id);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:invoiceVO = invoiceVO(event.result);
		            	updateLocalCash(result);
	                	Invoice(localCash[result.invoiceid]).isLoaded = true;
	                	Invoice(localCash[result.invoiceid]).isLoading = false;
		            },
		            function (event:FaultEvent):void
		            {
		            	trace(event.fault.message);
		            }
	            )
	        );
	
	        return asyncToken;
		}
		
		public function deleteInvoice(id:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.deleteEntity(id);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
	                	Invoice(localCash[id]).isLoading = false;
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
		                for each (var item:invoiceVO in result)
		                {
		                	updateLocalCash(item);
		                	Invoice(localCash[item.invoiceid]).isLoaded = true;
		                	Invoice(localCash[item.invoiceid]).isLoading = false;
		                }
		                
		                dispatchEvent(new Event("invoicesByUserIdLoaded"));
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
		                for each (var item:invoiceVO in result)
		                {
		                	updateLocalCash(item);
		                	Invoice(localCash[item.invoiceid]).isLoaded = true;
		                	Invoice(localCash[item.invoiceid]).isLoading = false;
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
		
		private function load(invoice:Invoice):void 
		{
			invoice.isLoaded = false;
			invoice.isLoading = true;
			InvoiceItemService.instance.getItemsByInvoiceId(invoice.invoiceid).addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
						invoice.isLoaded = true;
						invoice.isLoading = false;

		            	var result:ArrayCollection = new ArrayCollection(event.result as Array);
		                for each (var item:invoiceitemVO in result)
		                {
		                	var invoiceItem:InvoiceItem = InvoiceItemService.instance.getLocal(item.invoiceitemid);
		                	invoiceItem.invoice = invoice;
		                }
		            },
		            function (event:FaultEvent):void
		            {
						invoice.isLoaded = false;
						invoice.isLoading = false;
		            	trace(event.fault.message);
		            }
	            ));
		}
		
		public function getLocal(invoiceId:int, getFromDB:Boolean = false):Invoice 
		{
			var result:Invoice = localCash[invoiceId] as Invoice;

			if (result == null && getFromDB) 
			{
				result = new Invoice();
				result.invoiceid = invoiceId;
				localCash[invoiceId] = result;
				getInvoice(invoiceId);
			}
			
			return result;
		}
		
		private function updateLocalCash(item:invoiceVO):void 
		{
        	var existingItem:Invoice = localCash[item.invoiceid] as Invoice;
        	if (existingItem == null) 
        	{
        		existingItem = new Invoice();
        		localCash[item.invoiceid] = existingItem;
        	}
        	
        	existingItem.updateFields(item);
		}
		
	    private function get service():RemoteObject
	    {
	        if (_service == null) {
	           
	            _service = new RemoteObject( "ColdFusion" );
	            _service.source = "com.llsvc.domain.cfc.invoiceGateway";
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