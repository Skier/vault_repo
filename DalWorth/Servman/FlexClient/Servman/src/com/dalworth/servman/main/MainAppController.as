package com.dalworth.servman.main
{
	import com.dalworth.servman.domain.User;
	import com.dalworth.servman.service.BaseService;
	import com.dalworth.servman.service.LeadTypeService;
	
	import flash.events.Event;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class MainAppController
	{
		private var view:UIComponent;
		private var model:MainAppModel = MainAppModel.getInstance();
		
		public function MainAppController(view:UIComponent)
		{
			this.view = view;
		}
		
		public function initApp():void 
		{
			view.dispatchEvent(new Event(MainAppView.INIT_APP_EVENT_START));
			BaseService.getInstance().initApp().addResponder(
				new Responder(
					function(event:ResultEvent):void 
					{
						view.dispatchEvent(new Event(MainAppView.INIT_APP_EVENT_END));
						getCurrentUser();
					},
					function(event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
						view.dispatchEvent(new Event(MainAppView.INIT_APP_EVENT_FAULT));
					}))
		}

		public function getCurrentUser():void 
		{
			view.dispatchEvent(new Event(MainAppView.INIT_USER_EVENT_START));
			BaseService.getInstance().getCurrentUser().addResponder(
				new Responder(
					function(event:ResultEvent):void 
					{
						model.currentUser = event.result as User;
						view.dispatchEvent(new Event(MainAppView.INIT_USER_EVENT_END));
						getLeadTypes();
					},
					function(event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
						view.dispatchEvent(new Event(MainAppView.INIT_USER_EVENT_FAULT));
					}))
		}

		public function getLeadTypes():void 
		{
			view.dispatchEvent(new Event(MainAppView.INIT_PROJECT_TYPES_START));
			LeadTypeService.getInstance().getAll().addResponder(
				new Responder(
					function(event:ResultEvent):void 
					{
						view.dispatchEvent(new Event(MainAppView.INIT_PROJECT_TYPES_END));
					},
					function(event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
						view.dispatchEvent(new Event(MainAppView.INIT_PROJECT_TYPES_FAULT));
					}))
		}

	}
}