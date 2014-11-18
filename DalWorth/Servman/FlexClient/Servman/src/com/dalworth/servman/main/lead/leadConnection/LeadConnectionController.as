package com.dalworth.servman.main.lead.leadConnection
{
	import com.dalworth.servman.domain.Job;
	import com.dalworth.servman.domain.Lead;
	import com.dalworth.servman.service.IDSJobService;
	import com.dalworth.servman.service.JobService;
	import com.dalworth.servman.service.LeadService;
	
	import mx.collections.Sort;
	import mx.collections.SortField;
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class LeadConnectionController
	{
		private var model:LeadConnectionModel;
		private var view:UIComponent;
		
		public function LeadConnectionController(view:UIComponent)
		{
			this.view = view;
			this.model = LeadConnectionModel.getInstance();
		}
		
		public function initModel():void 
		{
			model.leads.removeAll();
			model.jobs.removeAll();
			model.currentLead = null;
			
			initLeads();
		}
		
		public function selectLead(lead:Lead):void 
		{
			model.currentLead = lead;
			model.dateFrom = null;

			initJobs();
		}
		
		public function matchJobToLead(job:Job):void 
		{
			model.isBusy = true;
			
			job.LeadId = model.currentLead.Id;
			
			JobService.getInstance().matchToLead(job.prepareToSend(), model.currentLead).addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						model.isBusy = false;
						model.jobs.removeAll();
						model.leads.removeItemAt(model.leads.getItemIndex(model.currentLead));
						model.currentLead = null;
					},
					function (event:FaultEvent):void 
					{
						model.isBusy = false;
						Alert.show(event.fault.message);
					}
				));
		}
		
		private function initLeads():void 
		{
			model.isBusy = true;
			LeadService.getInstance().getAllPending().addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						model.isBusy = false;
						model.leads.source = event.result as Array;
					},
					function (event:FaultEvent):void 
					{
						model.isBusy = false;
						Alert.show(event.fault.message);
					}
				));
		}
		
		public function initJobs():void 
		{
			model.jobs.removeAll();

			if (model.currentLead == null)
				return;
				
			model.isBusy = true;
			IDSJobService.getInstance().getUnmatchedByLead(model.currentLead, model.dateFrom).addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						model.isBusy = false;
						model.jobs.source = event.result as Array;

						var sort:Sort = new Sort();
							sort.fields = [new SortField("MatchLevel", false, true, true)]
						model.jobs.sort = sort;
						model.jobs.refresh();
					},
					function (event:FaultEvent):void 
					{
						model.isBusy = false;
						Alert.show(event.fault.message);
					}
				));
		}

	}
}