package com.dalworth.leadCentral.owner
{
	import com.dalworth.leadCentral.service.BaseService;
	import com.dalworth.leadCentral.service.LeadSourceService;
	import com.dalworth.leadCentral.service.UserService;
	
	import flash.events.Event;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class OwnerController
	{
		private var view:UIComponent;
		private var model:OwnerModel;
		
		private var isUsersInited:Boolean = false;
		private var isLeadSourcesInited:Boolean = false;
		private var isSalesRepsInited:Boolean = false;
		private var isCustomerTypesInited:Boolean = false;
		
		public function OwnerController(view:UIComponent)
		{
			this.view = view;
			this.model = OwnerModel.getInstance();
		}
		
		public function initModel():void 
		{
			initLocalCash();
		}
		
		private function tryToCommit():void 
		{
			model.isBusy = true;

			if (isUsersInited && isLeadSourcesInited) 
			{
				model.isBusy = false;
				view.dispatchEvent(new Event("ModelInited"));
			}
		} 
		
		private function initLocalCash():void 
		{
			initUsers();
			initLeadSources();
		}
		
		private function initUsers():void 
		{
			UserService.getInstance().getAll(
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
		
		private function initLeadSources():void 
		{
			LeadSourceService.getInstance().getAll(
				new Responder(
					function(event:ResultEvent):void 
					{
						isLeadSourcesInited = true;
						tryToCommit();
					},
					function(event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}))
		}

	}
}