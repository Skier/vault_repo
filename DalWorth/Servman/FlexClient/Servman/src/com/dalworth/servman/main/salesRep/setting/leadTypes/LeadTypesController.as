package com.dalworth.servman.main.owner.setting.leadTypes
{
	import com.dalworth.servman.service.LeadTypeService;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class LeadTypesController
	{
		private var model:LeadTypesModel;
		private var view:UIComponent;
		
		public function LeadTypesController(view:UIComponent)
		{
			this.view = view;
			this.model = LeadTypesModel.getInstance();
			
			initModel();
		}
		
		public function initModel():void 
		{
			model.leadTypes.removeAll();
			model.isBusy = true;
			LeadTypeService.getInstance().getAll().addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						model.isBusy = false;	
						model.leadTypes.source = event.result as Array;
					}, 
					function (event:FaultEvent):void 
					{
						model.isBusy = false;
						Alert.show(event.fault.message);	
					}));
		}
	}
}