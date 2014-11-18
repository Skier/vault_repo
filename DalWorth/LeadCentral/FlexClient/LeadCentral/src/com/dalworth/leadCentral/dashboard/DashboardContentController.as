package com.dalworth.leadCentral.dashboard
{
	import com.dalworth.leadCentral.domain.AmountSummary;
	import com.dalworth.leadCentral.domain.Lead;
	import com.dalworth.leadCentral.domain.enum.LeadStatusEnum;
	import com.dalworth.leadCentral.service.LeadService;
	
	import flash.events.Event;
	
	import mx.collections.ArrayCollection;
	import mx.controls.Alert;
	import mx.core.IUIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class DashboardContentController
	{
		private var model:DashboardContentModel;
		private var view:IUIComponent;
		
		private var getLeadsResponder:Responder;
		
		public function DashboardContentController(view:IUIComponent)
		{
			this.view = view;
			this.model = DashboardContentModel.getInstance();
			getLeadsResponder = new Responder(onGetLeadsResult, onGetLeadsFault);
		}
		
		private var isRequireRefresh:Boolean = false;
		public function setContent(content:ArrayCollection):void 
		{
			model.currentLeadSources = content;
			refreshContent();
		}
		
		public function setDatePeriod(startDate:Date, endDate:Date):void 
		{
			model.startDate = startDate;
			model.endDate = endDate;
			refreshContent();
		}
		
		private function refreshContent():void 
		{
			if (model.isBusy)
			{
				isRequireRefresh = true;
				return;
			}
			
			if (model.currentLeadSources == null)
				return;
				
			model.isBusy = true;

			var fromDate:Date = new Date(model.startDate.fullYear, model.startDate.month, model.startDate.date);
			var toDate:Date = new Date(model.endDate.fullYear, model.endDate.month, model.endDate.date);
			toDate = new Date(toDate.time + 86400000 - 1);
			LeadService.getInstance().getByLeadSourcesAndDatePeriod(model.currentLeadSources, fromDate, toDate, getLeadsResponder);
		}
		
		private function onGetLeadsResult(event:ResultEvent):void 
		{
			model.isBusy = false;
			if(isRequireRefresh)
			{
				isRequireRefresh = false;
				refreshContent();
				return;
			}
			
			model.leads.source = event.result as Array;
			refreshModelProperties();
			getSummary();
			view.dispatchEvent(new Event("leadsChanged"));
		}

		private function onGetLeadsFault(event:FaultEvent):void 
		{
			model.isBusy = false;
			model.leads.source = new Array();
			refreshModelProperties();
			view.dispatchEvent(new Event("leadsChanged"));
			
			Alert.show(event.fault.message);
		}
		
		private function refreshModelProperties():void 
		{
			var totalLeads:int = 0;
			var contactedLeads:int = 0;
			var convertedLeads:int = 0;
			var totalContactTime:Number = 0;

			for each (var lead:Lead in model.leads)
			{
				totalLeads++;
				
				if (lead.LeadStatusId != LeadStatusEnum.NEW)
					contactedLeads++;
				
				if (lead.LeadStatusId == LeadStatusEnum.CONVERTED)
					convertedLeads++;
				
				if (lead.DateCreated != null && lead.DateContacted != null)
					totalContactTime += ((lead.DateContacted.time - lead.DateCreated.time) / 60000);
			}
			
			model.totalLeads = totalLeads;
			model.contactedLeads = contactedLeads;
			model.contactedLeadsPct = totalLeads != 0 ? (contactedLeads / totalLeads) * 100 : 0;
			model.convertedLeads = convertedLeads;
			model.convertedLeadsPct = totalLeads != 0 ? (convertedLeads / totalLeads) * 100 : 0;
			model.averageContactTime = contactedLeads != 0 ? (totalContactTime / contactedLeads) : 0;
		}
		
		private var waitingForRefreshSummary:Boolean = false;
		private var gettingSummary:Boolean = false;
		private function getSummary():void 
		{
			if (gettingSummary)
			{
				waitingForRefreshSummary = true;
				return;
			} else 
			{
				waitingForRefreshSummary = false;
			}
			
			model.summary = new AmountSummary();
			var leadIds:Array = new Array();
			for each (var lead:Lead in model.leads)
			{
				leadIds.push(lead.Id);
			}

			gettingSummary = true;
			LeadService.getInstance().getSummaryByLeadIds(leadIds, 
				new Responder(
					function (event:ResultEvent):void 
					{
						gettingSummary = false;

						if (waitingForRefreshSummary)
						{
							getSummary();
							return;
						}

						model.summary = event.result as AmountSummary;
					}, 
					function (event:FaultEvent):void 
					{
						gettingSummary = false;
						Alert.show(event.fault.message);
					}));
		}

	}
}