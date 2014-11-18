package com.dalworth.leadCentral.lead
{
	import com.dalworth.leadCentral.domain.AmountSummary;
	import com.dalworth.leadCentral.domain.Lead;
	import com.dalworth.leadCentral.domain.LeadAmountSummary;
	import com.dalworth.leadCentral.domain.LeadSource;
	import com.dalworth.leadCentral.domain.LeadStatus;
	import com.dalworth.leadCentral.domain.QbInvoice;
	import com.dalworth.leadCentral.domain.User;
	import com.dalworth.leadCentral.domain.enum.LeadStatusEnum;
	import com.dalworth.leadCentral.events.LeadEvent;
	import com.dalworth.leadCentral.service.LeadService;
	import com.dalworth.leadCentral.service.QbInvoiceService;
	import com.dalworth.leadCentral.service.registry.LeadSourceRegistry;
	import com.dalworth.leadCentral.service.registry.UserRegistry;
	
	import flash.events.Event;
	
	import mx.collections.ArrayCollection;
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.events.CollectionEvent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class LeadEditController
	{
		private var view:UIComponent;
		private var model:LeadEditModel;
		
		public function LeadEditController(view:UIComponent)
		{
			this.view = view;
			this.model = LeadEditModel.getInstance();
		}
		
		public function initModel(lead:Lead):void 
		{
			model.lead = lead;
			
			initStatuses();
			initLeadSources();
			initUsers();
			initSummary();
		}
		
		private function initStatuses():void 
		{
			model.leadStatuses = LeadStatus.getStatuses();
		}
		
		private function initUsers():void 
		{
			refreshUsers();
			var source:ArrayCollection = UserRegistry.getInstance().getAll();
			source.addEventListener(CollectionEvent.COLLECTION_CHANGE, 
				function (event:*):void 
				{
					refreshUsers();
				})
		}
		
		private function refreshUsers():void 
		{
			model.users.removeAll();
			model.users.addItem(User.getEmpty());
			var source:ArrayCollection = UserRegistry.getInstance().getAll();
			for each (var item:User in source)
			{
				if (item.RoleName == User.ROLE_STAFF || item.RoleName == User.ROLE_ADMINISTRATOR)
					model.users.addItem(item);
			}
		}
		
		private function initLeadSources():void 
		{
			model.leadSources.removeAll();
			model.leadSources.addItem(LeadSource.getEmpty());
			var source:ArrayCollection = LeadSourceRegistry.getInstance().getAll();
			for each (var item:LeadSource in source)
			{
				model.leadSources.addItem(item);
			}
			source.addEventListener(CollectionEvent.COLLECTION_CHANGE, 
				function (event:*):void 
				{
					while(model.leadSources.length > 1)
						model.leadSources.source.pop();
					model.leadSources.source = model.leadSources.source.concat(LeadSourceRegistry.getInstance().getAll().source);
				})
		}
		
		private function initInvoices():void 
		{
			model.relatedInvoices.removeAll();
			if (model.lead == null)
				return;
			
			QbInvoiceService.getInstance().getAllQbInvoicesByLead(model.lead, 
				new Responder(
					function (event:ResultEvent):void 
					{
						if (model.lead != null) 
						{
							model.lead.RelatedQbInvoices = event.result as Array;
							model.relatedInvoices.source = model.lead.RelatedQbInvoices;
						}
					}, 
					function (event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}
				));
		}
		
		public function initSummary():void 
		{
			model.summary = new AmountSummary();
			if (model.lead == null || model.lead.LeadStatusId != LeadStatusEnum.CONVERTED)
				return;

			LeadService.getInstance().getSummaryByLeadIds([model.lead.Id], 
				new Responder(
					function (event:ResultEvent):void 
					{
						model.summary = event.result as AmountSummary;
						model.lead.AmountSummary = event.result as LeadAmountSummary;
						initInvoices();
					}, 
					function (event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
						initInvoices();
					}));
		}

		public function saveLead(newLead:Lead, closeAfterSave:Boolean):void 
		{
			model.isBusy = true;
			LeadService.getInstance().saveLead(newLead,  
				new Responder(
					function (event:ResultEvent):void 
					{
						model.isBusy = false;
						model.lead.applyFields(event.result as Lead);
						model.lead.RelatedQbInvoices = model.relatedInvoices.source;
						model.lead.AmountSummary = null;
						view.dispatchEvent(new LeadEvent(LeadEvent.LEAD_SAVE, model.lead));
						if (closeAfterSave)
							view.dispatchEvent(new Event("closeLeadEditor"));
					},
					function (event:FaultEvent):void 
					{
						model.isBusy = false;
						Alert.show(event.fault.message);
					}));
		}

		public function disconnectInvoice(qbInvoice:QbInvoice):void 
		{
			model.isBusy = true;
			QbInvoiceService.getInstance().unMatchQbInvoiceFromLead(qbInvoice, model.lead,
				new Responder(
					function(event:ResultEvent):void 
					{
						model.isBusy = false;
						var idx:int = model.relatedInvoices.getItemIndex(qbInvoice);
						if (idx > -1)
						{
							model.relatedInvoices.removeItemAt(idx);
							model.relatedInvoices.refresh();
							model.lead.RelatedQbInvoices = model.relatedInvoices.source;
							initSummary();
						}

						view.dispatchEvent(new Event("updateUiRequire"));
					}, 
					function (event:FaultEvent):void 
					{
						model.isBusy = false;
						Alert.show(event.fault.message);
					} ));

		}
		
	}
}