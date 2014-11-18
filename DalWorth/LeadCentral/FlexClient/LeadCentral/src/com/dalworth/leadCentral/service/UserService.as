package com.dalworth.leadCentral.service
{
	import com.dalworth.leadCentral.MainModel;
	import com.dalworth.leadCentral.domain.User;
	import com.dalworth.leadCentral.service.registry.UserRegistry;
	
	import flash.events.EventDispatcher;
	
	import mx.core.Application;
	import mx.rpc.AsyncToken;
	import mx.rpc.IResponder;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.InvokeEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.mxml.RemoteObject;
	
	public class UserService extends EventDispatcher
	{
		private static var _instance:UserService;
		
		private var _service:RemoteObject;
		private var serviceInvokeCounter:int = 0;
		private var faultResponder:Responder = new Responder(emptyResultHandler, faultHandler);
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function UserService()
		{
			super(null);
			
			if (_instance != null)
				throw new Error("Singleton !");
			
			serviceInvokeCounter = 0;
		}
		
		private function emptyResultHandler(event:ResultEvent):void {}
		private function faultHandler(event:FaultEvent):void 
		{
			if (event.fault.faultString.toUpperCase() == MainModel.SESSION_EXPIRED_STRING)
				Application.application.dispatchEvent(new Event(MainModel.SESSION_EXPIRED_STRING));
		}
		
		public static function getInstance():UserService 
		{
			if (_instance == null)
				_instance = new UserService();
			
			return _instance;
		}

		public function refreshUser(id:String, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetUser(MainModel.getInstance().currentTicket, id);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}

		public function saveUser(user:User, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.Save(MainModel.getInstance().currentTicket, user);
			
			asyncToken.addResponder(faultResponder);
			
			asyncToken.addResponder(
				new Responder(
					function (event:ResultEvent):void
					{
						UserRegistry.getInstance().storeLocal(event.result);
					},
					function (event:FaultEvent):void
					{
						trace(event.fault.message);
					}
				)
			);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getByQbUserId(id:String, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetByQbUserId(MainModel.getInstance().currentTicket, id);
			
			asyncToken.addResponder(faultResponder);
			
			asyncToken.addResponder(
				new Responder(
					function (event:ResultEvent):void
					{
						UserRegistry.getInstance().storeLocal(event.result);
					},
					function (event:FaultEvent):void
					{
						trace(event.fault.message);
					}
				)
			);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getAll(responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetAll(MainModel.getInstance().currentTicket);
			
			asyncToken.addResponder(faultResponder);
			
			asyncToken.addResponder(
				new Responder(
					function (event:ResultEvent):void
					{
						UserRegistry.getInstance().forceUpdate(event.result as Array);
					},
					function (event:FaultEvent):void
					{
						trace(event.fault.message);
					}
				)
			);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function inviteUser(user:User, message:String, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.InviteUser(MainModel.getInstance().currentTicket, user, message);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		private function get service():RemoteObject
		{
			if (_service == null) {
				
				_service = new RemoteObject( "GenericDestination" );
				_service.source = "Dalworth.LeadCentral.Service.Flex.UserService";
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