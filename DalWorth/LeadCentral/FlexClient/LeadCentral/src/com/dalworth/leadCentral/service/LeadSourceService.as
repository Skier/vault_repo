package com.dalworth.leadCentral.service
{
	import com.dalworth.leadCentral.MainModel;
	import com.dalworth.leadCentral.domain.LeadSource;
	import com.dalworth.leadCentral.domain.TrackingPhone;
	import com.dalworth.leadCentral.service.registry.LeadSourceRegistry;
	import com.dalworth.leadCentral.service.registry.TrackingPhoneRegistry;
	
	import flash.events.EventDispatcher;
	
	import mx.core.Application;
	import mx.rpc.AsyncToken;
	import mx.rpc.IResponder;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.InvokeEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.mxml.RemoteObject;
	
	public class LeadSourceService extends EventDispatcher
	{
		private static var _instance:LeadSourceService;
		
		private var _service:RemoteObject;
		private var serviceInvokeCounter:int = 0;
		private var faultResponder:Responder = new Responder(emptyResultHandler, faultHandler);
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function LeadSourceService()
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
		
		public static function getInstance():LeadSourceService 
		{
			if (_instance == null)
				_instance = new LeadSourceService();
			
			return _instance;
		}
		
		public function saveLeadSource(partner:LeadSource, ownPhones:Array = null, companyPhones:Array = null, responder:IResponder = null):void 
		{
			var localResponder:Responder = new Responder(
					function (event:ResultEvent):void
					{
						LeadSourceRegistry.getInstance().storeLocal(event.result);
					},
					function (event:FaultEvent):void
					{
						trace(event.fault.message);
					});
 
			
			var asyncToken:AsyncToken;
			
			if (ownPhones == null && companyPhones == null)
				asyncToken = service.Save(MainModel.getInstance().currentTicket, partner);
			else 
				asyncToken = service.SaveWithPhones(MainModel.getInstance().currentTicket, partner, ownPhones, companyPhones);
			
			asyncToken.addResponder(faultResponder);
			
			asyncToken.addResponder(localResponder);

			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getByCompanyPhoneId(phoneId:int, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetByTrackingPhoneId(MainModel.getInstance().currentTicket, phoneId);
			
			asyncToken.addResponder(faultResponder);
			
			asyncToken.addResponder(
				new Responder(
					function (event:ResultEvent):void
					{
						var result:Array = event.result as Array;
						for each (var item:LeadSource in result)
						{
							LeadSourceRegistry.getInstance().storeLocal(item);
						}
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
		
		public function addCompanyPhone(leadSource:LeadSource, phone:TrackingPhone, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.AddTrackingPhone(MainModel.getInstance().currentTicket, leadSource.prepareToSend(), phone.prepareToSend());
			
			asyncToken.addResponder(faultResponder);
			
			asyncToken.addResponder(
				new Responder(
					function (event:ResultEvent):void
					{
						TrackingPhoneRegistry.getInstance().storeLocal(event.result);
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
		
		public function removeCompanyPhone(leadSource:LeadSource, phone:TrackingPhone, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.RemoveTrackingPhone(MainModel.getInstance().currentTicket, leadSource.prepareToSend(), phone.prepareToSend());
			
			asyncToken.addResponder(faultResponder);
			
			asyncToken.addResponder(
				new Responder(
					function (event:ResultEvent):void
					{
						TrackingPhoneRegistry.getInstance().storeLocal(event.result);
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
						LeadSourceRegistry.getInstance().storeLocal(event.result);
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
						LeadSourceRegistry.getInstance().forceUpdate(event.result as Array);
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
		
		private function get service():RemoteObject
		{
			if (_service == null) {
				
				_service = new RemoteObject( "GenericDestination" );
				_service.source = "Dalworth.LeadCentral.Service.Flex.LeadSourceService";
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