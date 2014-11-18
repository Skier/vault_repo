package com.dalworth.leadCentral.lead.filter
{
	import com.dalworth.leadCentral.MainModel;
	import com.dalworth.leadCentral.domain.LeadSource;
	import com.dalworth.leadCentral.domain.LeadStatus;
	import com.dalworth.leadCentral.domain.User;
	import com.dalworth.leadCentral.service.registry.LeadSourceRegistry;
	import com.dalworth.leadCentral.service.registry.UserRegistry;
	
	import mx.collections.ArrayCollection;
	import mx.core.UIComponent;
	import mx.events.CollectionEvent;
	
	public class LeadsFilterController
	{
		private var view:UIComponent;
		private var model:LeadsFilterModel;
		
		public function LeadsFilterController(view:UIComponent)
		{
			this.view = view;
			this.model = LeadsFilterModel.getInstance();
		}
		
		public function initModel():void 
		{
			initLeadStatuses();
			initLeadSources();
			initUsers();
		}
		
		private function initLeadStatuses():void 
		{
			model.leadStatuses.removeAll();
			var source:ArrayCollection = LeadStatus.getStatuses();
			
			for each (var leadStatus:LeadStatus in source)
			{
				model.leadStatuses.addItem(leadStatus);
			}
		}

		private function initLeadSources():void 
		{
			refreshLeadSources();
			LeadSourceRegistry.getInstance().getAll().addEventListener(CollectionEvent.COLLECTION_CHANGE, 
				function (event:*):void 
				{
					refreshLeadSources();
				});
		}

		private function refreshLeadSources():void 
		{
			var leadSources:ArrayCollection = new ArrayCollection();

			var source:ArrayCollection = LeadSourceRegistry.getInstance().getAll();
			var currentUser:User = MainModel.getInstance().currentUser;
			if (currentUser.RoleName != User.ROLE_BUSINESS_PARTNER)
				leadSources.addItem(LeadSource.getEmpty());

			for each (var leadSource:LeadSource in source)
			{
				if (currentUser.RoleName != User.ROLE_BUSINESS_PARTNER)
					leadSources.addItem(leadSource);
				else if (leadSource.OwnedByUserId == currentUser.Id || leadSource.UserId == currentUser.Id)
					leadSources.addItem(leadSource);
			}

			model.leadSources.source = leadSources.source;
		}

		private function initUsers():void 
		{
			model.users.removeAll();
			var source:ArrayCollection = UserRegistry.getInstance().getAll();
			
			model.users.addItem(User.getEmpty());
			for each (var user:User in source)
			{
				if (user.RoleName == User.ROLE_STAFF || user.RoleName == User.ROLE_ADMINISTRATOR)
					model.users.addItem(user);
			}
			source.addEventListener(CollectionEvent.COLLECTION_CHANGE, 
				function (event:*):void 
				{
					while (model.users.length > 1) 
						model.users.source.pop();

					var source:ArrayCollection = UserRegistry.getInstance().getAll();
					for each (var user:User in source)
					{
						if (user.RoleName == User.ROLE_STAFF || user.RoleName == User.ROLE_ADMINISTRATOR)
							model.users.addItem(user);
					}
				})
		}

	}
}