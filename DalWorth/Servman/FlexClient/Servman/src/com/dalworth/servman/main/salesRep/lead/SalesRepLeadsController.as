package com.dalworth.servman.main.salesRep.lead
{
	import com.dalworth.servman.domain.BusinessPartner;
	import com.dalworth.servman.domain.Lead;
	import com.dalworth.servman.domain.User;
	import com.dalworth.servman.main.lead.LeadFilter;
	import com.dalworth.servman.service.LeadService;
	import com.dalworth.servman.service.registry.BusinessPartnerRegistry;
	
	import flash.events.Event;
	
	import mx.collections.ArrayCollection;
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.events.CollectionEvent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class SalesRepLeadsController
	{
		private var view:UIComponent;
		private var model:SalesRepLeadsModel = SalesRepLeadsModel.getInstance();
		
		public function SalesRepLeadsController(view:UIComponent)
		{
			this.view = view;
		}
		
		public function refreshLeads(filter:LeadFilter):void 
		{
			if (model.isBusy)
				return;

			model.isBusy = true;
			LeadService.getInstance().getLeads(filter).addResponder(
				new Responder(
					function(event:ResultEvent):void 
					{
						model.isBusy = false;
						var result:Array = event.result as Array;
						if (result.length != model.leads.length)
						{
							model.leads.source = event.result as Array;
						} else 
						{
							var len:int = result.length;
							for (var i:int=0; i<len; i++)
							{
								var oldLead:Lead = model.leads.source[i];
								var newLead:Lead = result[i] as Lead;
								oldLead.applyFields(newLead);
								oldLead.RelatedPhoneCall = newLead.RelatedPhoneCall;
								
								model.leads.itemUpdated(oldLead);
							}
						}
					},
					function(event:FaultEvent):void 
					{
						model.isBusy = false;
					}))
		}
	}
}