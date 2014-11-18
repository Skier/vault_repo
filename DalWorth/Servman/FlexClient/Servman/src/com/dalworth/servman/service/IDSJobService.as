package com.dalworth.servman.service
{
	import Intuit.Sb.Cdm.vo.Customer;
	
	import com.dalworth.servman.domain.Lead;
	
	import flash.events.EventDispatcher;
	
	import mx.rpc.AsyncToken;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.InvokeEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.mxml.RemoteObject;
	
	public class IDSJobService extends EventDispatcher
	{
		private static var _instance:IDSJobService;
		
		private var _service:RemoteObject;
		private var serviceInvokeCounter:int = 0;
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function IDSJobService()
		{
			super(null);
			
			if (_instance != null)
				throw new Error("Singleton !");
			
			serviceInvokeCounter = 0;
		}
		
		public static function getInstance():IDSJobService 
		{
			if (_instance == null)
				_instance = new IDSJobService();
			
			return _instance;
		}
		
		public function getByCustomer(customer:Customer):AsyncToken 
		{
			var asyncToken:AsyncToken = service.GetByCustomer(customer);
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
		
		public function getUnmatchedByLead(lead:Lead, dateFrom:Date):AsyncToken 
		{
			var asyncToken:AsyncToken = service.GetUnmatchedByLead(lead, dateFrom);
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
		
		public function getAllByLead(lead:Lead):AsyncToken 
		{
			var asyncToken:AsyncToken = service.GetAllByLead(lead);
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
				_service.source = "Servman.Intuit.Weborb.IDSJobService";
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