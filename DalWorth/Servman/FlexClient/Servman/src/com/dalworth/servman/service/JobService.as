package com.dalworth.servman.service
{
	import Intuit.Sb.Cdm.vo.Customer;
	
	import com.dalworth.servman.domain.Job;
	import com.dalworth.servman.domain.Lead;
	
	import flash.events.EventDispatcher;
	
	import mx.collections.ArrayCollection;
	import mx.rpc.AsyncToken;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.InvokeEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.mxml.RemoteObject;
	
	public class JobService extends EventDispatcher
	{
		private static var _instance:JobService;
		
		private var _service:RemoteObject;
		private var localCash:Object;
		private var serviceInvokeCounter:int = 0;
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function JobService()
		{
			super(null);
			
			if (_instance != null)
				throw new Error("Singleton !");
			
			localCash = new Object();
			serviceInvokeCounter = 0;
		}
		
		public static function getInstance():JobService 
		{
			if (_instance == null)
				_instance = new JobService();
			
			return _instance;
		}
		
		public function getByLead(lead:Lead):AsyncToken 
		{
			var asyncToken:AsyncToken = service.GetByLead(lead);
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
		
		public function matchToLead(job:Job, lead:Lead):AsyncToken 
		{
			if (job == null) return null;
			
			var oldValue:Job = job;
			var asyncToken:AsyncToken = service.MatchToLead(job, lead.prepareToSend());
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
				_service.source = "Servman.Intuit.Weborb.JobService";
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