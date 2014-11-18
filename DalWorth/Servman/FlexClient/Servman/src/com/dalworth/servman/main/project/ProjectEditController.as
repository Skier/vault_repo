package com.dalworth.servman.main.project
{
	import com.dalworth.servman.domain.QbJob;
	import com.dalworth.servman.main.MainAppModel;
	
	import mx.core.UIComponent;
	
	public class ProjectEditController
	{
		private var view:UIComponent;
		private var model:ProjectEditModel;
		
		public function ProjectEditController(view:UIComponent)
		{
			this.view = view;
			this.model = ProjectEditModel.getInstance()
			initModel();
		}
		
		private function initModel():void 
		{
			model.projectTypes = MainAppModel.getInstance().projectTypes;
			initJobs();
			initCustomers();
		}
		
		private function initJobs():void 
		{
			model.jobList.removeAll();
			QbJob
		}
		
		public function refreshCustomerList(value:String):void 
		{
			
		}

	}
}