package com.dalworth.servman.main.employee.project
{
	import com.dalworth.servman.service.JobService;
	import com.dalworth.servman.service.registry.BusinessPartnerRegistry;
	
	import flash.events.Event;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class EmployeeProjectsController
	{
		private var view:UIComponent;
		private var model:EmployeeProjectsModel = EmployeeProjectsModel.getInstance();
		
		public function EmployeeProjectsController(view:UIComponent)
		{
			this.view = view;
		}
		
		public function initModel():void 
		{
			model.businessPartners = BusinessPartnerRegistry.getInstance().getAll();
			initProjects();
		}
		
		private function initProjects():void 
		{
		}

	}
}