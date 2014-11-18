package com.dalworth.servman.service
{
	import com.dalworth.servman.domain.BusinessPartner;
	import com.dalworth.servman.domain.Phone;
	import com.dalworth.servman.domain.PhoneCallWorkflow;
	import com.dalworth.servman.domain.SalesRep;
	import com.dalworth.servman.domain.User;
	
	import flash.events.EventDispatcher;
	
	import mx.collections.ArrayCollection;
	import mx.rpc.AsyncToken;
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
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function PhoneService()
		{
			super(null);
			
			if (_instance != null)
				throw new Error("Singleton !");
			
			serviceInvokeCounter = 0;
		}
		
		public static function getInstance():PhoneService 
		{
			if (_instance == null)
				_instance = new PhoneService();
			
			return _instance;
		}
		
		public function getActiveCalls():AsyncToken 
		{
			service.showBusyCursor = false;
			var asyncToken:AsyncToken = service.GetActiveCalls();
			asyncToken.addResponder(
				new Responder(
					function (event:ResultEvent):void
					{
						service.showBusyCursor = true;
						trace(event.message); 
					},
					function (event:FaultEvent):void
					{
						service.showBusyCursor = true;
						trace(event.fault.message); 
					}
				)
			);
			
			return asyncToken;
		}
		
		public function getCallsByPhoneId(phoneId:int):AsyncToken 
		{
			var asyncToken:AsyncToken = service.GetCallsByPhoneId(phoneId);
			return asyncToken;
		}
		
		public function handleCall(callSid:String, user:User, phoneNumber:String = null):AsyncToken 
		{
			var phone:String = (phoneNumber == null) ? user.Phone : phoneNumber
			var asyncToken:AsyncToken = service.HandleCall(callSid, user, phone);
			
			return asyncToken;
		}
		
		public function getCompanyPhoneNumbers():AsyncToken 
		{
			var asyncToken:AsyncToken = service.GetActivePhoneNumbers();
			return asyncToken;
		}
		
		public function getRulesByPhone(phone:Phone):AsyncToken 
		{
			var asyncToken:AsyncToken = service.GetRulesByPhoneId(phone.Id);
			return asyncToken;
		}
		
		public function getPhonesByBusinessPartner(businessPartner:BusinessPartner):AsyncToken 
		{
			var asyncToken:AsyncToken = service.GetPhonesByBusinessPartnerId(businessPartner.Id);
			return asyncToken;
		}
		
		public function getPhonesBySalesRep(salesRep:SalesRep):AsyncToken 
		{
			var asyncToken:AsyncToken = service.GetPhonesBySalesRepId(salesRep.Id);
			return asyncToken;
		}
		
		public function getAvailablePhoneNumbers(areaCode:String):AsyncToken 
		{
			var asyncToken:AsyncToken = service.GetAvailablePhoneNumbers(areaCode);
			return asyncToken;
		}
		
		public function getAvailableTollFreePhoneNumbers(areaCode:String):AsyncToken 
		{
			var asyncToken:AsyncToken = service.GetAvailableTollFreePhoneNumbers(areaCode);
			return asyncToken;
		}
		
		public function purchasePhoneNumber(phone:Phone):AsyncToken 
		{
			var asyncToken:AsyncToken = service.PurchasePhoneNumber(phone.prepareToSend());
			return asyncToken;
		}
		
		public function purchaseTollFreePhoneNumber(phone:Phone):AsyncToken 
		{
			var asyncToken:AsyncToken = service.PurchaseTollFreePhoneNumber(phone.prepareToSend());
			return asyncToken;
		}
		
		public function suspendPhoneNumber(phone:Phone):AsyncToken 
		{
			var asyncToken:AsyncToken = service.SuspendPhoneNumber(phone.prepareToSend());
			return asyncToken;
		}
		
		public function activatePhoneNumber(phone:Phone):AsyncToken 
		{
			var asyncToken:AsyncToken = service.ActivatePhoneNumber(phone.prepareToSend());
			return asyncToken;
		}
		
		public function removePhoneNumber(phone:Phone):AsyncToken 
		{
			var asyncToken:AsyncToken = service.RemovePhoneNumber(phone.prepareToSend());
			return asyncToken;
		}
		
		public function retrieveWorkflows():AsyncToken 
		{
			var asyncToken:AsyncToken = service.RetrieveWorkflows();
			return asyncToken;
		}
		
		public function getAllPhones():AsyncToken 
		{
			var asyncToken:AsyncToken = service.GetAllPhones();
			return asyncToken;
		}
		
		public function getAllWorkflows():AsyncToken 
		{
			var asyncToken:AsyncToken = service.GetAllWorkflows();
			return asyncToken;
		}
		
		public function commitWorkflows(workflows:Array):AsyncToken 
		{
			var asyncToken:AsyncToken = service.CommitWorkflows(workflows);
			return asyncToken;
		}
		
		public function savePhoneCallWorkflow(workflow:PhoneCallWorkflow):AsyncToken 
		{
			var asyncToken:AsyncToken = service.SavePhoneCallWorkflow(workflow);
			return asyncToken;
		}
		
		public function removePhoneCallWorkflow(workflow:PhoneCallWorkflow):AsyncToken 
		{
			var asyncToken:AsyncToken = service.RemovePhoneCallWorkflow(workflow);
			return asyncToken;
		}
		
		public function commitPhoneCallWorkflows(workflows:ArrayCollection):AsyncToken 
		{
			var arr:Array = new Array();
			for each (var workflow:PhoneCallWorkflow in workflows)
			{
				arr.push(workflow.prepareToSend());
			}
			var asyncToken:AsyncToken = service.UpdatePhoneCallWorkflows(arr);
			return asyncToken;
		}
		
		private function get service():RemoteObject
		{
			if (_service == null) {
				
				_service = new RemoteObject( "GenericDestination" );
				_service.source = "Servman.Intuit.Weborb.PhoneService";
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