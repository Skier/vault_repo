package com.dalworth.leadCentral.service
{
	import com.dalworth.leadCentral.MainModel;
	import com.dalworth.leadCentral.domain.ServmanCustomer;
	
	import flash.events.EventDispatcher;
	
	import mx.core.Application;
	import mx.rpc.AsyncToken;
	import mx.rpc.IResponder;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.InvokeEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.mxml.RemoteObject;
	
	public class BaseService extends EventDispatcher
	{
		private static var _instance:BaseService;
		
		private var _service:RemoteObject;
		private var serviceInvokeCounter:int = 0;
		private var faultResponder:Responder = new Responder(emptyResultHandler, faultHandler);
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function BaseService()
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
		
		public static function getInstance():BaseService 
		{
			if (_instance == null)
				_instance = new BaseService();
			
			return _instance;
		}
		
		public function getCurrentUser(responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetCurrentUser(MainModel.getInstance().currentTicket);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}

		public function initApp(responder:IResponder = null):void 
		{
			var ticket:String = MainModel.getInstance().currentTicket;
			var realm:String = MainModel.getInstance().currentRealm;
			var db:String = MainModel.getInstance().currentDb;
			
			var asyncToken:AsyncToken = service.Init(ticket, realm, db);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}

		public function getCurrentBalance(responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetCurrentBalance(MainModel.getInstance().currentTicket);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getOAuthUrl(responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetOAuthUrl(MainModel.getInstance().currentTicket);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getPaymentUrl(responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetPaymentUrl(MainModel.getInstance().currentTicket);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getOAuthConnectionStatus(responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetOAuthConnectionStatus(MainModel.getInstance().currentTicket);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function updateServmanCustomer(customer:ServmanCustomer, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.UpdateServmanCustomer(MainModel.getInstance().currentTicket, customer);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		private function get service():RemoteObject
		{
			if (_service == null) {
				
				_service = new RemoteObject( "GenericDestination" );
				_service.source = "Dalworth.LeadCentral.Service.Flex.BaseServices";
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