package com.dalworth.leadCentral.service
{
	import com.dalworth.leadCentral.MainModel;
	import com.dalworth.leadCentral.domain.Lead;
	import com.dalworth.leadCentral.domain.LeadSource;
	import com.dalworth.leadCentral.lead.LeadFilter;
	
	import flash.events.Event;
	import flash.events.EventDispatcher;
	
	import mx.collections.ArrayCollection;
	import mx.core.Application;
	import mx.rpc.AsyncToken;
	import mx.rpc.IResponder;
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
		private var faultResponder:Responder = new Responder(emptyResultHandler, faultHandler);
		
		[Bindable]
		public var serviceIsBusy:Boolean = false;
		
		public function LeadService()
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
		
		public static function getInstance():LeadService 
		{
			if (_instance == null)
				_instance = new LeadService();
			
			return _instance;
		}
		
		public function getLead(leadId:int, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetLead(MainModel.getInstance().currentTicket, leadId);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getLeads(filter:LeadFilter, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetLeads(MainModel.getInstance().currentTicket, filter);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getLeadsCount(filter:LeadFilter, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetLeadsCount(MainModel.getInstance().currentTicket, filter);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getLeadsLimit(filter:LeadFilter, offset:int, limit:int, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetLeadsLimit(MainModel.getInstance().currentTicket, filter, offset, limit);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getByLeadSourcesAndDatePeriod(leadSources:ArrayCollection, startDate:Date, endDate:Date, responder:IResponder = null):void 
		{
			var sources:Array = new Array();
			
			if (leadSources != null)
			{
				for each (var leadSource:LeadSource in leadSources)
				{
					sources.push(leadSource.prepareToSend());
				}
			}
				
			var asyncToken:AsyncToken = service.GetByLeadSourcesAndDatePeriod(MainModel.getInstance().currentTicket, sources, startDate, endDate);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function saveLead(value:Lead, responder:IResponder = null):void 
		{
			if (value == null) return;
			
			var asyncToken:AsyncToken = service.Save(MainModel.getInstance().currentTicket, value.prepareToSend());
			
			asyncToken.addResponder(faultResponder);
			
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
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getSummaryByLeadIds(leadIds:Array, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetSummaryByLeadIds(MainModel.getInstance().currentTicket, leadIds);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getLeadSummariesByLeadIds(leadIds:Array, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetLeadSummariesByLeadIds(MainModel.getInstance().currentTicket, leadIds);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}

		private function get service():RemoteObject
		{
			if (_service == null) {
				
				_service = new RemoteObject( "GenericDestination" );
				_service.source = "Dalworth.LeadCentral.Service.Flex.LeadService";
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