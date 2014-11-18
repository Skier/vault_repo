package com.dalworth.servman.main.businessPartner.leads
{
	import com.dalworth.servman.domain.Lead;
	import com.dalworth.servman.main.businessPartner.BusinessPartnerModel;
	import com.dalworth.servman.service.LeadService;
	
	import flash.events.Event;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class BusinessPartnerLeadsController
	{
		private var view:UIComponent;
		private var model:BusinessPartnerLeadsModel = BusinessPartnerLeadsModel.getInstance();
		
		public function BusinessPartnerLeadsController(view:UIComponent)
		{
			this.view = view;
		}
		
		public function initModel():void 
		{
			model.businessPartner = BusinessPartnerModel.getInstance().businessPartner;
			initLeads();
		}
		
		public function refreshProjects(lead:Lead):void 
		{
			// todo;
		}
		
		private function initLeads():void 
		{
			LeadService.getInstance().getByBusinessPartnerId(model.businessPartner.Id, null, null).addResponder(
				new Responder(
					function(event:ResultEvent):void 
					{
						model.leads.source = event.result as Array;
						view.dispatchEvent(new Event(BusinessPartnerLeadsModel.MODEL_INITED));
					},
					function(event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}))
		}

	}
}