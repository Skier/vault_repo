package com.dalworth.servman.main.owner.project
{
	import com.dalworth.servman.main.owner.OwnerModel;
	import com.dalworth.servman.service.JobService;
	
	import flash.events.Event;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class OwnerProjectsController
	{
		private var view:UIComponent;
		private var model:OwnerProjectsModel = OwnerProjectsModel.getInstance();
		
		public function OwnerProjectsController(view:UIComponent)
		{
			this.view = view;
		}
		
		public function initModel():void 
		{
			model.businessPartners = OwnerModel.getInstance().businessPartners;
			initProjects();
		}
		
		private function initProjects():void 
		{
			JobService.getInstance().getAll().addResponder(
				new Responder(
					function(event:ResultEvent):void 
					{
						model.projects.source = event.result as Array;
						view.dispatchEvent(new Event(OwnerProjectsModel.MODEL_INITED));
					},
					function(event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}))
		}

	}
}