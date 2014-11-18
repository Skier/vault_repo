package com.llsvc.services
{
	import com.llsvc.domain.Client;
	import com.llsvc.domain.vo.clientVO;
	
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
	
	public class ClientService extends EventDispatcher
	{
		private static var _instance:ClientService;
		
		private var _service:RemoteObject;
		private var localCash:Object;
		private var serviceInvokeCounter:int = 0;
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function ClientService(target:IEventDispatcher=null)
		{
			super(target);
			
			if (_instance != null)
				throw new Error("Singleton !");
				
			localCash = new Object();
			serviceInvokeCounter = 0;
		}
		
		public static function get instance():ClientService 
		{
			if (_instance == null)
				_instance = new ClientService();
			
			return _instance;
		}
		
		public function saveClient(value:Client):AsyncToken 
		{
			if (value == null) return null;
			
			value.isLoaded = false;
			value.isLoading = true;
	        var asyncToken:AsyncToken = service.save(value.toVO());
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:clientVO = clientVO(event.result);
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
		
		public function getClient(id:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.get(id);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:clientVO = clientVO(event.result);
		            	updateLocalCash(result);
	                	Client(localCash[result.clientid]).isLoaded = true;
	                	Client(localCash[result.clientid]).isLoading = false;
		            },
		            function (event:FaultEvent):void
		            {
		            	trace(event.fault.message);
		            }
	            )
	        );
	
	        return asyncToken;
		}
		
		public function deleteClient(id:int):AsyncToken 
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
		
		public function getByUserId(userId:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.getByUserId(userId);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:ArrayCollection = new ArrayCollection(event.result as Array);
		                for each (var item:clientVO in result)
		                {
		                	updateLocalCash(item);
		                	Client(localCash[item.clientid]).isLoaded = true;
		                	Client(localCash[item.clientid]).isLoading = false;
		                }
		                
		                dispatchEvent(new Event("clientsByUserIdLoaded"));
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
		                for each (var item:clientVO in result)
		                {
		                	updateLocalCash(item);
		                	Client(localCash[item.clientid]).isLoaded = true;
		                	Client(localCash[item.clientid]).isLoading = false;
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
		
		public function getLocal(clientId:int, getFromDB:Boolean = false):Client 
		{
			var result:Client = localCash[clientId] as Client;

			if (result == null && getFromDB) 
			{
				result = new Client();
				result.clientid = clientId;
				localCash[clientId] = result;
				getClient(clientId);
			}
			
			return result;
		}
		
		private function updateLocalCash(item:clientVO):void 
		{
        	var existingItem:Client = localCash[item.clientid] as Client;
        	if (existingItem == null) 
        	{
        		existingItem = new Client();
        		localCash[item.clientid] = existingItem;
        	}
        	
        	existingItem.updateFields(item);
		}
		
	    private function get service():RemoteObject
	    {
	        if (_service == null) {
	           
	            _service = new RemoteObject( "ColdFusion" );
	            _service.source = "com.llsvc.domain.cfc.clientGateway";
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