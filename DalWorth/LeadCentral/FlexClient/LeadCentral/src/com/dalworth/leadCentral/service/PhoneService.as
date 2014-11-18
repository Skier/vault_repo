package com.dalworth.leadCentral.service
{
	import com.dalworth.leadCentral.MainModel;
	import com.dalworth.leadCentral.domain.LeadForm;
	import com.dalworth.leadCentral.domain.LeadSource;
	import com.dalworth.leadCentral.domain.PhoneCall;
	import com.dalworth.leadCentral.domain.PhoneCallWorkflow;
	import com.dalworth.leadCentral.domain.PhoneSms;
	import com.dalworth.leadCentral.domain.TrackingPhone;
	import com.dalworth.leadCentral.domain.User;
	import com.dalworth.leadCentral.service.registry.TrackingPhoneRegistry;
	
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
	
	public class PhoneService extends EventDispatcher
	{
		private static var _instance:PhoneService;
		
		private var _service:RemoteObject;
		private var serviceInvokeCounter:int = 0;
		private var faultResponder:Responder = new Responder(emptyResultHandler, faultHandler);
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function PhoneService()
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
		
		public static function getInstance():PhoneService 
		{
			if (_instance == null)
				_instance = new PhoneService();
			
			return _instance;
		}
		
		public function getActiveCalls(responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetActiveCalls(MainModel.getInstance().currentTicket);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getCallsByPhoneId(phoneId:int, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetCallsByPhoneId(MainModel.getInstance().currentTicket, phoneId);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function handleCall(callSid:String, user:User, phoneNumber:String = null, responder:IResponder = null):void 
		{
			var phone:String = (phoneNumber == null) ? user.Phone : phoneNumber
			var asyncToken:AsyncToken = service.HandleCall(MainModel.getInstance().currentTicket, callSid, user, phone);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getCompanyPhoneNumbers(responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetActivePhoneNumbers(MainModel.getInstance().currentTicket);
			
			asyncToken.addResponder(faultResponder);

			asyncToken.addResponder(
				new Responder(
					function (event:ResultEvent):void
					{
						TrackingPhoneRegistry.getInstance().forceUpdate(event.result as Array);
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
		
		public function getRulesByPhone(phone:TrackingPhone, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetRulesByTrackingPhoneId(MainModel.getInstance().currentTicket, phone.Id);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getTrackingPhonesByLeadSource(leadSource:LeadSource, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetTrackingPhonesByLeadSourceId(MainModel.getInstance().currentTicket, leadSource.Id);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getLeadSourcePhones(leadSource:LeadSource, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetLeadSourcePhones(MainModel.getInstance().currentTicket, leadSource.Id);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getAvailablePhoneNumbers(areaCode:String, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetAvailablePhoneNumbers(areaCode);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getAvailableTollFreePhoneNumbers(areaCode:String, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetAvailableTollFreePhoneNumbers(areaCode);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function purchasePhoneNumber(phone:TrackingPhone, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.PurchasePhoneNumber(MainModel.getInstance().currentTicket, phone.prepareToSend());
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function purchaseTollFreePhoneNumber(phone:TrackingPhone, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.PurchaseTollFreePhoneNumber(MainModel.getInstance().currentTicket, phone.prepareToSend());
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function suspendPhoneNumber(phone:TrackingPhone, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.SuspendPhoneNumber(MainModel.getInstance().currentTicket, phone.prepareToSend());
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function activatePhoneNumber(phone:TrackingPhone, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.ActivatePhoneNumber(MainModel.getInstance().currentTicket, phone.prepareToSend());
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function savePhoneNumber(phone:TrackingPhone, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.Save(MainModel.getInstance().currentTicket, phone);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function removePhoneNumber(phone:TrackingPhone, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.RemovePhoneNumber(MainModel.getInstance().currentTicket, phone.prepareToSend());
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function retrieveWorkflows(responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.RetrieveWorkflows(MainModel.getInstance().currentTicket);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getAllLeadSourcePhones(responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetAllLeadSourcePhones(MainModel.getInstance().currentTicket);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getAllWorkflows(responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetAllWorkflows(MainModel.getInstance().currentTicket);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function commitWorkflows(workflows:Array, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.CommitWorkflows(MainModel.getInstance().currentTicket, workflows);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function savePhoneCallWorkflow(workflow:PhoneCallWorkflow, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.SavePhoneCallWorkflow(MainModel.getInstance().currentTicket, workflow);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function removePhoneCallWorkflow(workflow:PhoneCallWorkflow, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.RemovePhoneCallWorkflow(MainModel.getInstance().currentTicket, workflow);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function commitPhoneCallWorkflows(workflows:ArrayCollection, responder:IResponder = null):void 
		{
			var arr:Array = new Array();
			for each (var workflow:PhoneCallWorkflow in workflows)
			{
				arr.push(workflow.prepareToSend());
			}
			var asyncToken:AsyncToken = service.UpdatePhoneCallWorkflows(MainModel.getInstance().currentTicket, arr);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getAllTransactions(responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetAllTransactions(MainModel.getInstance().currentTicket);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getTrackingPhoneRotations(leadSources:Array, fromDate:Date, toDate:Date, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetTrackingPhoneRotations(MainModel.getInstance().currentTicket, leadSources, fromDate, toDate);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getTrackingPhoneRotationsByPhoneCall(phoneCall:PhoneCall, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetTrackingPhoneRotationsByPhoneCall(MainModel.getInstance().currentTicket, phoneCall.prepareToSend());
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getTrackingPhoneRotationsByPhoneSms(phoneSms:PhoneSms, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetTrackingPhoneRotationsByPhoneSms(MainModel.getInstance().currentTicket, phoneSms.prepareToSend());
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		public function getTrackingPhoneRotationsByLeadForm(leadForm:LeadForm, responder:IResponder = null):void 
		{
			var asyncToken:AsyncToken = service.GetTrackingPhoneRotationsByLeadForm(MainModel.getInstance().currentTicket, leadForm);
			
			asyncToken.addResponder(faultResponder);
			
			if (responder != null)
				asyncToken.addResponder(responder);
		}
		
		private function get service():RemoteObject
		{
			if (_service == null) {
				
				_service = new RemoteObject( "GenericDestination" );
				_service.source = "Dalworth.LeadCentral.Service.Flex.PhoneService";
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