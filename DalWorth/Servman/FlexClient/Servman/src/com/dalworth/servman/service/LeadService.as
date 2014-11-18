package com.dalworth.servman.service
{
	import com.dalworth.servman.domain.Lead;
	import com.dalworth.servman.main.lead.LeadFilter;
	
	import flash.events.EventDispatcher;
	
	import mx.rpc.AsyncToken;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.InvokeEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.mxml.RemoteObject;
	
	public class LeadService extends EventDispatcher
	{
		private static var _instance:LeadService;
		
		private var _service:RemoteObject;
		private var serviceInvokeCounter:int = 0;
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function LeadService()
		{
			super(null);
			
			if (_instance != null)
				throw new Error("Singleton !");
			
			serviceInvokeCounter = 0;
		}
		
		public static function getInstance():LeadService 
		{
			if (_instance == null)
				_instance = new LeadService();
			
			return _instance;
		}
		
		public function getLead(id:int):AsyncToken 
		{
			var asyncToken:AsyncToken = service.getById(id);
			return asyncToken;
		}
		
		public function getLeads(filter:LeadFilter = null):AsyncToken 
		{
			var asyncToken:AsyncToken;
			
			if (filter == null)
				asyncToken = service.GetAll();
			else 
				asyncToken = service.GetLeads(filter);
				
			return asyncToken;
		}
		
		public function getAllPending():AsyncToken 
		{
			var asyncToken:AsyncToken = service.GetAllPending();
			return asyncToken;
		}
		
		public function getByBusinessPartnerId(id:int, startDate:Date, endDate:Date):AsyncToken 
		{
			var asyncToken:AsyncToken = service.GetByBusinessPartnerId(id, startDate, endDate);
			return asyncToken;
		}
		
		public function getBySalesRepId(id:int, startDate:Date, endDate:Date):AsyncToken 
		{
			var asyncToken:AsyncToken = service.GetBySalesRepId(id, startDate, endDate);
			return asyncToken;
		}
		
		public function getByDatePeriod(startDate:Date, endDate:Date):AsyncToken 
		{
			var asyncToken:AsyncToken = service.GetByPeriod(startDate, endDate);
			return asyncToken;
		}
		
		public function getChangeHistory(id:int):AsyncToken 
		{
			var asyncToken:AsyncToken = service.GetChangeHistory(id);
			return asyncToken;
		}
		
		public function saveLead(value:Lead):AsyncToken 
		{
			if (value == null) return null;
			
			var asyncToken:AsyncToken = service.Save(value.prepareToSend());
			asyncToken.addResponder(
				new Responder(
					function (event:ResultEvent):void
					{
						var result:Lead = Lead(event.result);
						value.applyFields(result);
					},
					function (event:FaultEvent):void
					{
						trace(event.fault.message);
					}
				)
			);
			
			return asyncToken;
		}
		
		public function saveLeadChangeHistory(lead:Lead, historyItems:Array):AsyncToken 
		{
			if (lead == null) return null;
			
			var asyncToken:AsyncToken = service.SaveLeadChangeHistory(lead.prepareToSend(), historyItems);
			asyncToken.addResponder(
				new Responder(
					function (event:ResultEvent):void
					{
						var result:Lead = Lead(event.result);
						lead.applyFields(result);
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
				_service.source = "Servman.Intuit.Weborb.LeadService";
				_service.showBusyCursor = false;
				
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