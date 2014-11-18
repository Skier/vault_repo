package com.dalworth.leadCentral.service
{
	import Intuit.Sb.Cdm.vo.Customer;
	
	import com.dalworth.leadCentral.MainModel;
	import com.dalworth.leadCentral.domain.QbInvoice;
	import com.dalworth.leadCentral.domain.Lead;
	
	import flash.events.EventDispatcher;
	
	import mx.core.Application;
	import mx.rpc.AsyncToken;
	import mx.rpc.IResponder;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.InvokeEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.mxml.RemoteObject;
	
	public class QbInvoiceService extends EventDispatcher
	{
		private static var _instance:QbInvoiceService;
		
		private var _service:RemoteObject;
		private var serviceInvokeCounter:int = 0;
		private var faultResponder:Responder = new Responder(emptyResultHandler, faultHandler);
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function QbInvoiceService()
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
		
		public static function getInstance():QbInvoiceService 
		{
			if (_instance == null)
				_instance = new QbInvoiceService();
			
			return _instance;
		}
		
		public function getQbInvoicesByCustomer(customer:Customer, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetQbInvoicesByCustomer(MainModel.getInstance().currentTicket, customer);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getUnmatchedQbInvoicesByLead(lead:Lead, dateFrom:Date, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetUnmatchedInvoicesByLead(MainModel.getInstance().currentTicket, lead, dateFrom);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getAllQbInvoicesByLead(lead:Lead, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetInvoicesByLead(MainModel.getInstance().currentTicket, lead.Id);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function matchToLead(qbInvoice:QbInvoice, lead:Lead, responder:IResponder = null):void 
		{
			if (qbInvoice == null) 
				return;
			
			var asyncToken:AsyncToken = service.MatchToLead(MainModel.getInstance().currentTicket, qbInvoice, lead.prepareToSend());
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}

		public function unMatchQbInvoiceFromLead(qbInvoice:QbInvoice, lead:Lead, responder:IResponder = null):void 
		{
			if (qbInvoice == null) 
				return;
			
			var asyncToken:AsyncToken = service.UnMatchFromLead(MainModel.getInstance().currentTicket, qbInvoice.prepareToSend(), lead.prepareToSend());
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}

		private function get service():RemoteObject
		{
			if (_service == null) {
				
				_service = new RemoteObject( "GenericDestination" );
				_service.source = "Dalworth.LeadCentral.Service.Flex.QbInvoiceService";
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