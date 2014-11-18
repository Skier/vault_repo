package com.dalworth.leadCentral.setting.user
{
	import com.dalworth.leadCentral.service.UserService;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class UsersController
	{
		private var model:UsersModel;
		private var view:UIComponent;
		
		public function UsersController(view:UIComponent)
		{
			this.view = view;
			this.model = UsersModel.getInstance();
			
			initModel();
		}
		
		public function initModel():void 
		{
			//model.users.removeAll();
			model.isBusy = true;
			UserService.getInstance().getAll(
				new Responder(
					function (event:ResultEvent):void 
					{
						model.isBusy = false;	
						model.users.source = event.result as Array;
					}, 
					function (event:FaultEvent):void 
					{
						model.isBusy = false;
						Alert.show(event.fault.message);	
					}));
		}
	}
}