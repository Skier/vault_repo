package com.dalworth.servman.main.employee.lead
{
	import com.dalworth.servman.domain.BusinessPartner;
	import com.dalworth.servman.domain.Lead;
	import com.dalworth.servman.main.employee.EmployeeModel;
	import com.dalworth.servman.service.LeadService;
	
	import flash.events.Event;
	
	import mx.collections.ArrayCollection;
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.events.CollectionEvent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class EmployeeLeadsController
	{
		private var view:UIComponent;
		private var model:EmployeeLeadsModel = EmployeeLeadsModel.getInstance();
		
		public function EmployeeLeadsController(view:UIComponent)
		{
			this.view = view;
		}
		
		public function initModel():void 
		{
			model.businessPartners = EmployeeModel.getInstance().businessPartners;
			model.localBusinessPartners = new ArrayCollection();
			model.businessPartners.removeEventListener(CollectionEvent.COLLECTION_CHANGE, onBusinessPartnersChange);
			model.businessPartners.addEventListener(CollectionEvent.COLLECTION_CHANGE, onBusinessPartnersChange);

			initBusinessPartners();

			initLeads();
		}
		
		private function initBusinessPartners():void 
		{
			model.localBusinessPartners.removeAll();
			var allLabel:BusinessPartner = new BusinessPartner();
				allLabel.RelatedUser.Name = "All";
			model.localBusinessPartners.addItem(allLabel);
			model.localBusinessPartners.source = model.localBusinessPartners.source.concat(model.businessPartners.source);
		}
		
		public function refreshProjects(lead:Lead):void 
		{
			// todo;
		}
		
		private function onBusinessPartnersChange(event:CollectionEvent):void 
		{
			initBusinessPartners();
		}
		
		private function initLeads():void 
		{
			LeadService.getInstance().getLeads().addResponder(
				new Responder(
					function(event:ResultEvent):void 
					{
						model.leads.source = event.result as Array;
						view.dispatchEvent(new Event(EmployeeLeadsModel.MODEL_INITED));
					},
					function(event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}))
		}

	}
}