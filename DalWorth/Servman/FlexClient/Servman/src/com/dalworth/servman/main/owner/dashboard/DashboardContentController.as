package com.dalworth.servman.main.owner.dashboard
{
	import com.dalworth.servman.domain.BusinessPartner;
	import com.dalworth.servman.domain.Lead;
	import com.dalworth.servman.domain.LeadStatus;
	import com.dalworth.servman.domain.SalesRep;
	import com.dalworth.servman.main.breadCrumb.BreadCrumbModel;
	import com.dalworth.servman.service.LeadService;
	
	import flash.events.Event;
	
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
		
		public function setContent(content:Object):void 
		{
			model.currentContent = content;
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
			if (model.currentContent is SalesRep)
				initSalesRepContent(model.currentContent as SalesRep);
			else if (model.currentContent is BusinessPartner)
				initBusinessPartnerContent(model.currentContent as BusinessPartner);
			else 
				initFullContent();
		}
		
		private function initSalesRepContent(salesRep:SalesRep):void 
		{
			model.currentUser = salesRep.RelatedUser;
			model.contentName = salesRep.ShowAs;
			model.isBusy = true;
			
			BreadCrumbModel.getInstance().breadCrumbString = "Dashboard > Sales Representative";
			
			LeadService.getInstance().getBySalesRepId(salesRep.Id, model.startDate, model.endDate).addResponder(getLeadsResponder);
		}

		private function initBusinessPartnerContent(businessPartner:BusinessPartner):void 
		{
			model.currentUser = businessPartner.RelatedUser;
			model.contentName = businessPartner.ShowAs;
			model.isBusy = true;
			
			BreadCrumbModel.getInstance().breadCrumbString = "Dashboard > Business Partner";
			
			LeadService.getInstance().getByBusinessPartnerId(businessPartner.Id, model.startDate, model.endDate).addResponder(getLeadsResponder);
		}

		private function initFullContent():void 
		{
			model.currentUser = null;
			model.contentName = "All Leads";
			model.isBusy = true;
			
			BreadCrumbModel.getInstance().breadCrumbString = "Dashboard > All";
			
			LeadService.getInstance().getByDatePeriod(model.startDate, model.endDate).addResponder(getLeadsResponder);
		}
		
		private function onGetLeadsResult(event:ResultEvent):void 
		{
			model.isBusy = false;
			model.leads.source = event.result as Array;
			refreshModelProperties();
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
			var closedAmount:Number = 0;

			for each (var lead:Lead in model.leads)
			{
				totalLeads++;
				
				if (lead.LeadStatusId != LeadStatus.STATUS_NEW_ID)
					contactedLeads++;
				
				if (lead.LeadStatusId == LeadStatus.STATUS_CONVERTED_ID)
					convertedLeads++;
				
				if (lead.DateCreated != null && lead.DateContacted != null)
					totalContactTime += ((lead.DateContacted.time - lead.DateCreated.time) / 60000);
				
				closedAmount += 543;
			}
			
			model.totalLeads = totalLeads;
			model.contactedLeads = contactedLeads;
			model.contactedLeadsPct = totalLeads != 0 ? (contactedLeads / totalLeads) * 100 : 0;
			model.convertedLeads = convertedLeads;
			model.convertedLeadsPct = totalLeads != 0 ? (convertedLeads / totalLeads) * 100 : 0;
			model.closedAmount = closedAmount;
			model.averageContactTime = contactedLeads != 0 ? (totalContactTime / contactedLeads) : 0;
		}

	}
}