package com.dalworth.servman.main.lead
{
	import com.dalworth.servman.domain.BusinessPartner;
	import com.dalworth.servman.domain.Lead;
	import com.dalworth.servman.domain.LeadChangeHistory;
	import com.dalworth.servman.domain.LeadType;
	import com.dalworth.servman.domain.SalesRep;
	import com.dalworth.servman.domain.User;
	import com.dalworth.servman.events.LeadEvent;
	import com.dalworth.servman.main.MainAppModel;
	import com.dalworth.servman.service.IDSJobService;
	import com.dalworth.servman.service.LeadService;
	import com.dalworth.servman.service.registry.BusinessPartnerRegistry;
	import com.dalworth.servman.service.registry.LeadTypeRegistry;
	import com.dalworth.servman.service.registry.SalesRepRegistry;
	import com.dalworth.servman.service.registry.UserRegistry;
	
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
			
			initTypes();
			initSalesReps();
			initBusinessPartners();
			initUsers();
			initJobs();
		}
		
		private function initTypes():void 
		{
			model.leadTypes.removeAll();
			model.leadTypes.addItem(LeadType.getEmpty());
			var source:ArrayCollection = LeadTypeRegistry.getInstance().getAll();
			for each (var item:LeadType in source)
			{
				model.leadTypes.addItem(item);
			}
			source.addEventListener(CollectionEvent.COLLECTION_CHANGE, 
				function (event:*):void 
				{
					initTypes();
				})
		}
		
		private function initUsers():void 
		{
			model.users.removeAll();
			model.users.addItem(User.getEmpty());
			var source:ArrayCollection = UserRegistry.getInstance().getAll();
			for each (var item:User in source)
			{
				if (item.RelatedOwner || item.RelatedSalesRep || item.RelatedCustomerServiceRep)
					model.users.addItem(item);
			}
			source.addEventListener(CollectionEvent.COLLECTION_CHANGE, 
				function (event:*):void 
				{
					initUsers();
				})
		}
		
		private function initSalesReps():void 
		{
			model.salesReps.removeAll();
			model.salesReps.addItem(SalesRep.getEmpty());
			var source:ArrayCollection = SalesRepRegistry.getInstance().getAll();
			for each (var item:SalesRep in source)
			{
				model.salesReps.addItem(item);
			}
			source.addEventListener(CollectionEvent.COLLECTION_CHANGE, 
				function (event:*):void 
				{
					initSalesReps();
				})
		}
		
		private function initBusinessPartners():void 
		{
			model.businessPartners.removeAll();
			model.businessPartners.addItem(BusinessPartner.getEmpty());
			var source:ArrayCollection = BusinessPartnerRegistry.getInstance().getAll();
			for each (var item:BusinessPartner in source)
			{
				model.businessPartners.addItem(item);
			}
			source.addEventListener(CollectionEvent.COLLECTION_CHANGE, 
				function (event:*):void 
				{
					initBusinessPartners();
				})
		}
		
		private function initJobs():void 
		{
			model.jobs.removeAll();
			if (model.lead == null)
				return;
			
			IDSJobService.getInstance().getAllByLead(model.lead).addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						model.jobs.source = event.result as Array;
					}, 
					function (event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}
				));
		}
		
		public function saveLead(newLead:Lead, closeAfterSave:Boolean):void 
		{
			var historyItems:Array = new Array();
			
			var historyItem:LeadChangeHistory = new LeadChangeHistory();
			historyItem.Action = "Update";
			historyItem.DateChanged = new Date();
			historyItem.Description = "Update Lead Detail ";
			historyItem.LeadId = model.lead.Id;
			historyItem.UserId = MainAppModel.getInstance().currentUser.Id;

			historyItems.push(historyItem);

			model.isBusy = true;
			LeadService.getInstance().saveLeadChangeHistory(newLead, historyItems).addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						model.isBusy = false;
						model.lead.applyFields(event.result as Lead);
						initJobs();
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

	}
}