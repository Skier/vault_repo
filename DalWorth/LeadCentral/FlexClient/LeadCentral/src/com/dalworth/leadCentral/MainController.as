package com.dalworth.leadCentral
{
	import com.dalworth.leadCentral.domain.ServmanCustomer;
	import com.dalworth.leadCentral.domain.User;
	import com.dalworth.leadCentral.service.BaseService;
	import com.dalworth.leadCentral.service.LeadSourceService;
	import com.dalworth.leadCentral.service.PhoneService;
	import com.dalworth.leadCentral.service.UserService;
	
	import flash.events.Event;
	import flash.events.TimerEvent;
	import flash.utils.Timer;
	
	import mx.controls.Alert;
	import mx.core.Application;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class MainController
	{
		private var view:UIComponent;
		private var model:MainModel;
		
		private var timer:Timer;
		
		public function MainController(view:UIComponent)
		{
			this.view = view;
			this.model = MainModel.getInstance();
			
			Application.application.addEventListener("oAuthInited", onOAuthInited);
			Application.application.addEventListener("leadSourcesInited", onLeadSourcesInited);
			Application.application.addEventListener("workflowsInited", onWorkflowsInited);
			
			initTimer();
		}
		
		private function initTimer():void 
		{
			timer = new Timer(300000);
			timer.addEventListener(TimerEvent.TIMER, onTimer);
			timer.start();
		}
		
		public function initModel():void
		{
            model.currentTicket = Application.application.parameters["t"];
            model.currentRealm = Application.application.parameters["r"];
            model.currentDb = Application.application.parameters["d"];

			initApp();
		}
		
		private function faultHandler(event:FaultEvent):void 
		{
			log("Fault");
			log(event.fault.message);
			view.dispatchEvent(new Event("FaultInit"));
		}
		
		public function initApp():void 
		{
			if (model.currentTicket != null && model.currentTicket != "")
			{
				getCurrentUser();
				return;
			}
			
			log("Init application...");
			BaseService.getInstance().initApp(
				new Responder(
					function(event:ResultEvent):void 
					{
						model.currentTicket = event.result as String;
			            log("ticket=" + model.currentTicket);
						log("Done.");
						getCurrentUser();
					}, faultHandler))
		}

		private function getCurrentUser():void 
		{
			log("Init current User...");
			BaseService.getInstance().getCurrentUser(
				new Responder(
					function(event:ResultEvent):void 
					{
						log("Done.");
						model.currentUser = event.result as User;
						model.currentCustomer = model.currentUser.RelatedCustomer;
						initUsers();
					}, faultHandler))
		}

		private function initUsers():void 
		{
			log("Init Users...");
			UserService.getInstance().getAll(
				new Responder(
					function(event:ResultEvent):void 
					{
						log("Done.");
						initTrackingPhones();
					}, faultHandler))
		}
		
		private function initTrackingPhones():void 
		{
			log("Init Tracking Phones...");
			PhoneService.getInstance().getCompanyPhoneNumbers(
				new Responder(
					function(event:ResultEvent):void 
					{
						log("Done.");
						initLeadSources();
					}, faultHandler))
		}

		private function initLeadSources():void 
		{
			log("Init Lead Sources...");
			LeadSourceService.getInstance().getAll(
				new Responder(
					function(event:ResultEvent):void 
					{
						log("Done.");
						if (model.currentUser.RoleName == User.ROLE_ADMINISTRATOR)
							initOAuthUrl();
						else 
							view.dispatchEvent(new Event(MainModel.INIT_MODEL_COMPLETE));
					}, faultHandler))
		}

		private function initOAuthUrl():void 
		{
			log("Init OAuth URL...");
			BaseService.getInstance().getOAuthUrl(
				new Responder(
					function(event:ResultEvent):void 
					{
						model.oAuthUrl = event.result as String;
						log("Done.");
						initPaymentUrl();
					}, faultHandler))
		}

		private function initPaymentUrl():void 
		{
			log("Init Payment URL...");
			BaseService.getInstance().getPaymentUrl(
				new Responder(
					function(event:ResultEvent):void 
					{
						model.paymentUrl = event.result as String;
						log("Done.");
						view.dispatchEvent(new Event(MainModel.INIT_MODEL_COMPLETE));
					}, faultHandler))
		}

		private function onOAuthInited(event:Event):void 
		{
			if (model.currentCustomer.IsOAuthInited)
				return;
				
			var customer:ServmanCustomer = model.currentCustomer.prepareToSend();
			customer.IsOAuthInited = true;
			//updateCustomer(customer);
		}

		private function onLeadSourcesInited(event:Event):void 
		{
			if (model.currentCustomer.IsLeadSourcesInited)
				return;
				
			var customer:ServmanCustomer = model.currentCustomer.prepareToSend();
			customer.IsLeadSourcesInited = true;
			updateCustomer(customer);
		}

		private function onWorkflowsInited(event:Event):void 
		{
			if (model.currentCustomer.IsWorkflowsInited)
				return;
				
			var customer:ServmanCustomer = model.currentCustomer.prepareToSend();
			customer.IsWorkflowsInited = true;
			updateCustomer(customer);
		}
		
		private function updateCustomer(customer:ServmanCustomer):void
		{
			BaseService.getInstance().updateServmanCustomer(customer, 
				new Responder(
					function(event:ResultEvent):void 
					{
						model.currentCustomer.applyFields(event.result as ServmanCustomer);
					},
					function(event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}))
		}
		
		private function onTimer(event:TimerEvent):void 
		{
			refreshCurrentUser();
		}
		
		private function refreshCurrentUser():void 
		{
			UserService.getInstance().refreshUser(model.currentUser.QbUserId, 
				new Responder(
					function(event:ResultEvent):void 
					{
						
					},
					function(event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}))
		}

		private function log(value:String):void 
		{
			model.logContent += value;
			model.logContent += "\n";

			view.dispatchEvent(new Event(MainModel.UPDATE_INTERFACE));
		}
			
	}
}