package com.dalworth.servman.service
{
	import com.dalworth.servman.domain.Lead;
	import com.dalworth.servman.domain.User;
	
	import flash.events.EventDispatcher;
	
	import mx.rpc.AsyncToken;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.InvokeEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.mxml.RemoteObject;
	
	public class BaseService extends EventDispatcher
	{
		private static var _instance:BaseService;
		
		private var _service:RemoteObject;
		private var localCash:Object;
		private var serviceInvokeCounter:int = 0;
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function BaseService()
		{
			super(null);
			
			if (_instance != null)
				throw new Error("Singleton !");
			
			localCash = new Object();
			serviceInvokeCounter = 0;
		}
		
		public static function getInstance():BaseService 
		{
			if (_instance == null)
				_instance = new BaseService();
			
			return _instance;
		}
		
		public function getCurrentUser():AsyncToken 
		{
			var asyncToken:AsyncToken = service.GetCurrentUser();
			asyncToken.addResponder(
				new Responder(
					function (event:ResultEvent):void
					{
						var result:User = event.result as User;
						updateLocalCash(result);
					},
					function (event:FaultEvent):void
					{
						trace(event.fault.message); 
					}
				)
			);
			
			return asyncToken;
		}
		
		public function initApp():AsyncToken 
		{
			var asyncToken:AsyncToken = service.TestInit();
			asyncToken.addResponder(
				new Responder(
					function (event:ResultEvent):void
					{
					},
					function (event:FaultEvent):void
					{
						trace(event.fault.message);
					}
				)
			);
			
			return asyncToken;
		}
		
		public function getLocal(id:int):Lead 
		{
			return localCash[id] as Lead;
		}
		
		private function updateLocalCash(item:User):void 
		{
			localCash[item.Id] = item;
		}
		
		private function get service():RemoteObject
		{
			if (_service == null) {
				
				_service = new RemoteObject( "GenericDestination" );
				_service.source = "Servman.Intuit.Weborb.BaseServices";
				_service.showBusyCursor = true;
				//_service.endpoint = "http://gateway.hotservice.com.ua/weborb.aspx";
				
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