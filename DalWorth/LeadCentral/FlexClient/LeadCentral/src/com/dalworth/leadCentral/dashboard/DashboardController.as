package com.dalworth.leadCentral.dashboard
{
	import com.dalworth.leadCentral.MainModel;
	import com.dalworth.leadCentral.domain.LeadSource;
	import com.dalworth.leadCentral.domain.User;
	import com.dalworth.leadCentral.service.registry.LeadSourceRegistry;
	import com.dalworth.leadCentral.service.registry.UserRegistry;
	
	import mx.collections.ArrayCollection;
	import mx.collections.ListCollectionView;
	import mx.core.UIComponent;
	
	public class DashboardController
	{
		private var model:DashboardModel;
		private var view:UIComponent;
		
		public function DashboardController(view:UIComponent)
		{
			this.model = DashboardModel.getInstance();
			this.view = view;
		}
		
		public function initModel():void 
		{
			initLeadSources();
			initSalesReps();
			initBusinessPartners();
		}
		
		public function refreshFilter(salesRepId:int, businessPartnerId:int):void 
		{
			model.salesRepId = salesRepId;
			model.businessPartnerId = businessPartnerId;
			model.leadSources.refresh();
		}
		
		private function initLeadSources():void 
		{
			refreshLeadSources();
		}
		
		private function refreshLeadSources():void 
		{
			model.leadSources.removeAll();
			
			var leadSources:ArrayCollection = LeadSourceRegistry.getInstance().getAll();
			var user:User = MainModel.getInstance().currentUser;
			for each (var item:LeadSource in leadSources) 
			{
				if (user.RoleName == User.ROLE_ADMINISTRATOR)
					model.leadSources.addItem(item);
				else if (item.OwnedByUserId == user.Id || item.UserId == user.Id)
					model.leadSources.addItem(item);
			}
		}
		
		private function initSalesReps():void 
		{
			var salesReps:ArrayCollection = new ArrayCollection();
			salesReps.addItem(User.getEmpty());

			for each (var leadSource:LeadSource in model.leadSources)
			{
				if (leadSource.OwnedByUserId > 0)
				{
					var user:User = UserRegistry.getInstance().getLocal(leadSource.OwnedByUserId) as User;
					if (user != null && !salesReps.contains(user))
						salesReps.addItem(user);
				}
			}
			
			model.salesReps.source = salesReps.source;
			model.salesReps.refresh();
		}

		private function initBusinessPartners():void 
		{
			var businessPartners:ArrayCollection = new ArrayCollection();
			businessPartners.addItem(User.getEmpty());

			for each (var leadSource:LeadSource in model.leadSources)
			{
				if (leadSource.UserId > 0)
				{
					var user:User = UserRegistry.getInstance().getLocal(leadSource.UserId) as User;
					if (user != null && !businessPartners.contains(user))
						businessPartners.addItem(user);
				}
			}
			
			model.businessPartners.source = businessPartners.source;
			model.businessPartners.refresh();
		}
	}
}