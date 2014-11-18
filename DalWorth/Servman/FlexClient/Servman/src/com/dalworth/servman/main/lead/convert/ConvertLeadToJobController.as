package com.dalworth.servman.main.lead.convert
{
	import com.dalworth.servman.domain.Job;
	import com.dalworth.servman.service.JobService;
	
	import flash.events.Event;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class ConvertLeadToJobController
	{
		private var model:ConvertLeadToJobModel;
		private var view:UIComponent;
		
		public function ConvertLeadToJobController(view:UIComponent)
		{
			this.view = view;
			this.model = ConvertLeadToJobModel.getInstance();
		}
		
		public function commitJob():void 
		{
			prepareJob();
			
			model.isBusy = true;
			JobService.getInstance().matchToLead(model.currentJob, model.currentLead.prepareToSend()).addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						model.isBusy = false;
						view.dispatchEvent(new Event("jobMatchComplete"));
					},
					function (event:FaultEvent):void 
					{
						model.isBusy = false;
						Alert.show(event.fault.message);
					}
				));
			
		} 
		
		private function prepareJob():void 
		{
			model.currentJob.LeadId = model.currentLead.Id;
		}

	}
}