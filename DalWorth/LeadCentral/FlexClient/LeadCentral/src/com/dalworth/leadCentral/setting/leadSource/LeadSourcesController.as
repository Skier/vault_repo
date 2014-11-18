package com.dalworth.leadCentral.setting.leadSource
{
	import com.dalworth.leadCentral.MainModel;
	import com.dalworth.leadCentral.domain.LeadSource;
	import com.dalworth.leadCentral.domain.User;
	import com.dalworth.leadCentral.service.LeadSourceService;
	import com.dalworth.leadCentral.service.registry.UserRegistry;
	
	import mx.collections.ArrayCollection;
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class LeadSourcesController
	{
		private var model:LeadSourcesModel;
		private var view:UIComponent;
		
		public function LeadSourcesController(view:UIComponent)
		{
			this.view = view;
			this.model = LeadSourcesModel.getInstance();
		}
		
		public function initModel():void 
		{
			initLeadSources();
		}
		
		public function updateModel(leadSource:LeadSource):void 
		{
			if (!model.leadSources.contains(leadSource))
			{
				model.leadSources.addItem(leadSource);
				model.leadSources.refresh();
			}
		}
		
		public function refreshFilter(salesRepId:int, businessPartnerId:int):void 
		{
			model.salesRepId = salesRepId;
			model.businessPartnerId = businessPartnerId;
			model.leadSources.refresh();
		}
		
		private function initLeadSources():void 
		{
			if (model.isBusy)
				return;
				
			model.isBusy = true;
			LeadSourceService.getInstance().getAll(
				new Responder(
					function(event:ResultEvent):void 
					{
						model.isBusy = false;
						var result:Array = event.result as Array;
						var leadSources:Array = new Array();
						for each (var leadSource:LeadSource in result)
						{
							var user:User = MainModel.getInstance().currentUser;
							if (user.RoleName != User.ROLE_BUSINESS_PARTNER)
								leadSources.push(leadSource);
							else if (user.Id == leadSource.UserId || user.Id == leadSource.OwnedByUserId)
								leadSources.push(leadSource);
						}

						model.leadSources.source = leadSources;

						initSalesReps();
						initBusinessPartners();
					}, 
					function (event:FaultEvent):void 
					{
						model.isBusy = false;
						Alert.show(event.fault.message);
					}));
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