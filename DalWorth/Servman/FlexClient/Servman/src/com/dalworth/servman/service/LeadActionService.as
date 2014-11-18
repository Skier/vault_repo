package com.dalworth.servman.service
{
	import com.dalworth.servman.domain.LeadAction;
	
	import flash.events.EventDispatcher;
	
	import mx.collections.ArrayCollection;
	import mx.rpc.AsyncToken;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.InvokeEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.mxml.RemoteObject;
	
	public class LeadActionService extends EventDispatcher
	{
		private static var _instance:LeadActionService;
		
		private var _service:RemoteObject;
		private var localCash:Object;
		private var serviceInvokeCounter:int = 0;
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function LeadActionService()
		{
			super(null);
			
			if (_instance != null)
				throw new Error("Singleton !");
			
			localCash = new Object();
			serviceInvokeCounter = 0;
		}
		
		public static function getInstance():LeadActionService 
		{
			if (_instance == null)
				_instance = new LeadActionService();
			
			return _instance;
		}
		
		public function getByLeadStatusId(id:int):AsyncToken 
		{
			var asyncToken:AsyncToken = service.GetByLeadStatusId(id);
			asyncToken.addResponder(
				new Responder(
					function (event:ResultEvent):void
					{
						trace(event.message);
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
				_service.source = "Servman.Intuit.Weborb.LeadActionService";
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