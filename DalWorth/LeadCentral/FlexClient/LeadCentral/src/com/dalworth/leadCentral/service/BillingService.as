package com.dalworth.leadCentral.service
{
	import com.dalworth.leadCentral.MainModel;
	
	import flash.events.EventDispatcher;
	
	import mx.core.Application;
	import mx.rpc.AsyncToken;
	import mx.rpc.IResponder;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.InvokeEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.mxml.RemoteObject;
	
	public class BillingService extends EventDispatcher
	{
		private static var _instance:BillingService;
		
		private var _service:RemoteObject;
		private var faultResponder:Responder = new Responder(emptyResultHandler, faultHandler);
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function BillingService()
		{
			super(null);
			
			if (_instance != null)
				throw new Error("Singleton !");
		}
		
		private function emptyResultHandler(event:ResultEvent):void {}
		private function faultHandler(event:FaultEvent):void 
		{
			if (event.fault.faultString.toUpperCase() == MainModel.SESSION_EXPIRED_STRING)
				Application.application.dispatchEvent(new Event(MainModel.SESSION_EXPIRED_STRING));
		}
		
		public static function getInstance():BillingService 
		{
			if (_instance == null)
				_instance = new BillingService();
			
			return _instance;
		}
		
		public function getAllTransactions(responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetAllTransactions(MainModel.getInstance().currentTicket);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getTransactionsCount(responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetTransactionsCount(MainModel.getInstance().currentTicket);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getTransactions(offset:int, limit:int, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetTransactions(MainModel.getInstance().currentTicket, offset, limit);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		private function get service():RemoteObject
		{
			if (_service == null) {
				
				_service = new RemoteObject( "GenericDestination" );
				_service.source = "Dalworth.LeadCentral.Service.Flex.BillingService";
				_service.showBusyCursor = false;
				
				_service.addEventListener(InvokeEvent.INVOKE, 
					function(event:InvokeEvent):void 
					{ 
						serviceIsBusy = true; 
					});
				
				_service.addEventListener(ResultEvent.RESULT,
					function(event:ResultEvent):void 
					{ 
						serviceIsBusy = false; 
					});
				
				_service.addEventListener(FaultEvent.FAULT,
					function(event:FaultEvent):void 
					{ 
						serviceIsBusy = false; 
					});
			}
			
			return _service;
		}
		
	}
}