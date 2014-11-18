package com.dalworth.servman.main.owner
{
	import com.dalworth.servman.domain.User;
	import com.dalworth.servman.service.BusinessPartnerService;
	import com.dalworth.servman.service.CustomerServiceRepService;
	import com.dalworth.servman.service.OwnerService;
	import com.dalworth.servman.service.SalesRepService;
	import com.dalworth.servman.service.UserService;
	
	import flash.events.Event;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class SalesRepController
	{
		private var view:UIComponent;
		
		private var isUsersInited:Boolean = false;
		private var isOwnersInited:Boolean = false;
		private var isSalesRepsInited:Boolean = false;
		private var isBusinessPartnersInited:Boolean = false;
		private var isCustomerServiceRepsInited:Boolean = false;
		
		public function SalesRepController(view:UIComponent)
		{
			this.view = view;
		}
		
		public function initModel():void 
		{
			view.enabled = false;
			initLocalCash();
		}
		
		private function tryToCommit():void 
		{
			if (isUsersInited && isOwnersInited && isSalesRepsInited && isBusinessPartnersInited && isCustomerServiceRepsInited) 
			{
				view.enabled = true;
				view.dispatchEvent(new Event("ModelInited"));
			}
		} 
		
		private function initLocalCash():void 
		{
			initUsers();
			initOwners();
			initSalesReps();
			initBusinessPartners();
			initCustomerServiceReps();
		}
		
		private function initUsers():void 
		{
			UserService.getInstance().getAll().addResponder(
				new Responder(
					function(event:ResultEvent):void 
					{
						isUsersInited = true;
						tryToCommit();
					},
					function(event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}))
		}
		
		private function initOwners():void 
		{
			OwnerService.getInstance().getAll().addResponder(
				new Responder(
					function(event:ResultEvent):void 
					{
						isOwnersInited = true;
						tryToCommit();
					},
					function(event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}))
		}
		
		private function initSalesReps():void 
		{
			SalesRepService.getInstance().getAll().addResponder(
				new Responder(
					function(event:ResultEvent):void 
					{
						isSalesRepsInited = true;
						tryToCommit();
					},
					function(event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}))
		}
		
		private function initBusinessPartners():void 
		{
			BusinessPartnerService.getInstance().getAll().addResponder(
				new Responder(
					function(event:ResultEvent):void 
					{
						isBusinessPartnersInited = true;
						tryToCommit();
					},
					function(event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}))
		}
		
		private function initCustomerServiceReps():void 
		{
			CustomerServiceRepService.getInstance().getAll().addResponder(
				new Responder(
					function(event:ResultEvent):void 
					{
						isCustomerServiceRepsInited = true;
						tryToCommit();
					},
					function(event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}))
		}
	}
}