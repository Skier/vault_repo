package com.dalworth.servman.main.owner.setting.businessPartner
{
	import com.dalworth.servman.domain.BusinessPartner;
	import com.dalworth.servman.service.BusinessPartnerService;
	import com.dalworth.servman.service.LeadService;
	
	import flash.events.Event;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class BusinessPartnersController
	{
		private var view:UIComponent;
		private var model:BusinessPartnersModel = BusinessPartnersModel.getInstance();
		
		public function BusinessPartnersController(view:UIComponent)
		{
			this.view = view;
		}
		
		public function initModel():void 
		{
			model.businessPartners.removeAll();
			
			BusinessPartnerService.getInstance().getAll().addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						model.businessPartners.source = event.result as Array;
					}, 
					function (event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}));
		}
		
		public function initLeads(businessPartner:BusinessPartner):void 
		{
			if (businessPartner == null)
			{
				model.leads.removeAll();
				return;
			}
			
			LeadService.getInstance().getByBusinessPartnerId(businessPartner.Id, null, null).addResponder(
				new Responder(
					function(event:ResultEvent):void 
					{
						model.leads.source = event.result as Array;
						view.dispatchEvent(new Event(BusinessPartnersModel.LEADS_INITED));
					},
					function(event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}))
		}

	}
}