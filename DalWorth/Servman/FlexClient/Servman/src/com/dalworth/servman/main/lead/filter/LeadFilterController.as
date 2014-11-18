package com.dalworth.servman.main.lead.filter
{
	import com.dalworth.servman.domain.BusinessPartner;
	import com.dalworth.servman.domain.LeadStatus;
	import com.dalworth.servman.domain.LeadType;
	import com.dalworth.servman.domain.SalesRep;
	import com.dalworth.servman.domain.User;
	import com.dalworth.servman.service.registry.BusinessPartnerRegistry;
	import com.dalworth.servman.service.registry.LeadTypeRegistry;
	import com.dalworth.servman.service.registry.SalesRepRegistry;
	import com.dalworth.servman.service.registry.UserRegistry;
	
	import mx.collections.ArrayCollection;
	import mx.core.UIComponent;
	import mx.events.CollectionEvent;
	
	public class LeadFilterController
	{
		private var view:UIComponent;
		private var model:LeadFilterModel;
		
		public function LeadFilterController(view:UIComponent)
		{
			this.view = view;
			this.model = LeadFilterModel.getInstance();
		}
		
		public function initModel():void 
		{
			initLeadTypes();
			initLeadStatuses();
			initSalesReps();
			initBusinessPartners();
			initUsers();
		}
		
		private function initLeadTypes():void 
		{
			model.leadTypes.removeAll();
			var source:ArrayCollection = LeadTypeRegistry.getInstance().getAll();
			
			model.leadTypes.addItem(LeadType.getEmpty());
			for each (var leadType:LeadType in source)
			{
				model.leadTypes.addItem(leadType);
			}
			source.addEventListener(CollectionEvent.COLLECTION_CHANGE, 
				function (event:CollectionEvent):void 
				{
					while (model.leadTypes.length > 1) 
						model.leadTypes.source.pop();
					model.leadTypes.source = model.leadTypes.source.concat(LeadTypeRegistry.getInstance().getAll().source);
				});
		}
		
		private function initLeadStatuses():void 
		{
			model.leadStatuses.removeAll();
			var source:ArrayCollection = LeadStatus.getStatuses();
			
			//model.leadStatuses.addItem(LeadStatus.getEmpty());
			for each (var leadStatus:LeadStatus in source)
			{
				model.leadStatuses.addItem(leadStatus);
			}
		}

		private function initSalesReps():void 
		{
			model.salesReps.removeAll();
			var source:ArrayCollection = SalesRepRegistry.getInstance().getAll();
			
			model.salesReps.addItem(SalesRep.getEmpty());
			for each (var salesRep:SalesRep in source)
			{
				model.salesReps.addItem(salesRep);
			}
			source.addEventListener(CollectionEvent.COLLECTION_CHANGE, 
				function (event:*):void 
				{
					while (model.salesReps.length > 1) 
						model.salesReps.source.pop();
					model.salesReps.source = model.salesReps.source.concat(SalesRepRegistry.getInstance().getAll().source);
				})
		}

		private function initBusinessPartners():void 
		{
			model.businessPartners.removeAll();
			var source:ArrayCollection = BusinessPartnerRegistry.getInstance().getAll();
			
			model.businessPartners.addItem(BusinessPartner.getEmpty());
			for each (var businessPartner:BusinessPartner in source)
			{
				model.businessPartners.addItem(businessPartner);
			}
			source.addEventListener(CollectionEvent.COLLECTION_CHANGE, 
				function (event:*):void 
				{
					while (model.businessPartners.length > 1) 
						model.businessPartners.source.pop();
					model.businessPartners.source = model.businessPartners.source.concat(BusinessPartnerRegistry.getInstance().getAll().source);
				})
		}

		private function initUsers():void 
		{
			model.users.removeAll();
			var source:ArrayCollection = UserRegistry.getInstance().getAll();
			
			model.users.addItem(User.getEmpty());
			for each (var user:User in source)
			{
				if (user.RelatedOwner || user.RelatedSalesRep || user.RelatedCustomerServiceRep)
					model.users.addItem(user);
			}
			source.addEventListener(CollectionEvent.COLLECTION_CHANGE, 
				function (event:*):void 
				{
					while (model.businessPartners.length > 1) 
						model.businessPartners.source.pop();

					var source:ArrayCollection = UserRegistry.getInstance().getAll();
					for each (var user:User in source)
					{
						if (user.RelatedOwner || user.RelatedSalesRep || user.RelatedCustomerServiceRep)
							model.users.addItem(user);
					}
				})
		}

	}
}