package com.dalworth.servman.service
{
	import com.dalworth.servman.domain.Owner;
	import com.dalworth.servman.service.registry.OwnerRegistry;
	
	import flash.events.EventDispatcher;
	
	import mx.collections.ArrayCollection;
	import mx.rpc.AsyncToken;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.InvokeEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.mxml.RemoteObject;
	
	public class OwnerService extends EventDispatcher
	{
		private static var _instance:OwnerService;
		
		private var _service:RemoteObject;
		private var serviceInvokeCounter:int = 0;
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function OwnerService()
		{
			super(null);
			
			if (_instance != null)
				throw new Error("Singleton !");
			
			serviceInvokeCounter = 0;
		}
		
		public static function getInstance():OwnerService 
		{
			if (_instance == null)
				_instance = new OwnerService();
			
			return _instance;
		}
		
		public function getOwner(id:int):AsyncToken 
		{
			var asyncToken:AsyncToken = service.getById(id);
			asyncToken.addResponder(
				new Responder(
					function (event:ResultEvent):void
					{
						OwnerRegistry.getInstance().storeLocal(event.result);
					},
					function (event:FaultEvent):void
					{
						trace(event.fault.message);
					}
				)
			);
			
			return asyncToken;
		}
		
		public function saveOwner(owner:Owner):AsyncToken 
		{
			var asyncToken:AsyncToken = service.Save(owner);
			asyncToken.addResponder(
				new Responder(
					function (event:ResultEvent):void
					{
						OwnerRegistry.getInstance().storeLocal(event.result);
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
			var asyncToken:AsyncToken = service.GetAll();
			asyncToken.addResponder(
				new Responder(
					function (event:ResultEvent):void
					{
						OwnerRegistry.getInstance().forceUpdate(event.result as Array);
					},
					function (event:FaultEvent):void
					{
						trace(event.fault.message);
					}
				)
			);
			
			return asyncToken;
		}
		
		private function get service():RemoteObject
		{
			if (_service == null) {
				
				_service = new RemoteObject( "GenericDestination" );
				_service.source = "Servman.Intuit.Weborb.OwnerService";
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