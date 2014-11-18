package com.dalworth.leadCentral.setting.leadSource
{
	import Intuit.Sb.Cdm.vo.CustomerType;
	import Intuit.Sb.Cdm.vo.IdType;
	import Intuit.Sb.Cdm.vo.SalesRep;
	
	import com.dalworth.leadCentral.MainModel;
	import com.dalworth.leadCentral.domain.LeadSource;
	import com.dalworth.leadCentral.domain.User;
	import com.dalworth.leadCentral.events.LeadSourceEvent;
	import com.dalworth.leadCentral.service.LeadSourceService;
	import com.dalworth.leadCentral.service.registry.LeadSourceRegistry;
	import com.dalworth.leadCentral.service.registry.UserRegistry;
	
	import flash.events.Event;
	
	import mx.collections.ArrayCollection;
	import mx.collections.Sort;
	import mx.collections.SortField;
	import mx.controls.Alert;
	import mx.core.Application;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class LeadSourceEditController
	{
		private var view:UIComponent;
		private var model:LeadSourceEditModel;
		
		public function LeadSourceEditController(view:UIComponent)
		{
			this.view = view;
			this.model = LeadSourceEditModel.getInstance();
		}
		
		public function initModel(leadSource:LeadSource):void 
		{
			model.leadSource = leadSource;

			initParents();
			initUsers();
		}
		
		private function initParents():void 
		{
			model.salesReps.removeAll();
			
			var emptyUser:User = User.getEmpty();
			model.salesReps.addItem(emptyUser);
			var users:ArrayCollection = UserRegistry.getInstance().getAll();
			for each (var user:User in users) 
			{
				if (user.RoleName == User.ROLE_STAFF)
					model.salesReps.addItem(user);
			}
		}
		
		private function initUsers():void 
		{
			model.businessPartners.removeAll();
			
			var emptyUser:User = User.getEmpty();
			emptyUser.Name = "< create new >";
			model.businessPartners.addItem(emptyUser);
			var users:ArrayCollection = UserRegistry.getInstance().getAll();
			for each (var user:User in users) 
			{
				if (user.RoleName == User.ROLE_BUSINESS_PARTNER)
					model.businessPartners.addItem(user);
			}
		}
		
		public function saveLeadSource(leadSource:LeadSource, ownPhones:Array, companyPhones:Array):void 
		{
			model.isBusy = true;
			
			LeadSourceService.getInstance().saveLeadSource(leadSource, ownPhones, companyPhones, 
				new Responder(
					function (event:ResultEvent):void 
					{
						Application.application.dispatchEvent(new Event("leadSourcesInited"));

						model.isBusy = false;
						var result:LeadSource = event.result as LeadSource;
						
						if (model.leadSource.Id == 0)
							model.leadSource = result;
						else
							model.leadSource.applyFields(result);
						
						if (result.RelatedUser != null)
						{
							UserRegistry.getInstance().storeLocal(result.RelatedUser);
							model.leadSource.RelatedUser = UserRegistry.getInstance().getLocal(model.leadSource.UserId) as User;
						}
						
						view.dispatchEvent(new LeadSourceEvent(LeadSourceEvent.LEAD_SOURCE_SAVE, model.leadSource));
						view.dispatchEvent(new Event("closeLeadSourceEditor"));
					},
					function (event:FaultEvent):void 
					{
						model.isBusy = false;
						Alert.show(event.fault.message);
					}));
		}

	}
}