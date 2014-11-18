package com.dalworth.leadCentral.owner.home
{
	import com.dalworth.leadCentral.MainModel;
	import com.dalworth.leadCentral.domain.Lead;
	import com.dalworth.leadCentral.domain.LeadStatus;
	import com.dalworth.leadCentral.domain.enum.LeadStatusEnum;
	import com.dalworth.leadCentral.lead.LeadFilter;
	import com.dalworth.leadCentral.service.LeadService;
	
	import flash.events.Event;
	
	import mx.binding.utils.ChangeWatcher;
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class OwnerHomeController
	{
		private var view:UIComponent;
		private var model:OwnerHomeModel;
		
		private var isUsersInited:Boolean = false;
		private var isLeadSourcesInited:Boolean = false;
		
		public function OwnerHomeController(view:UIComponent)
		{
			this.view = view;
			this.model = OwnerHomeModel.getInstance();
		}
		
		public function initModel():void 
		{
			var now:Date = new Date();
			var morning:Date = new Date(now.fullYear, now.month, now.date);

			model.endDate = new Date(morning.time + 86400000 - 1);
			model.startDate = new Date(morning.time - 604800000);
			
			updateInit();
			initLeads();
		}
		
		private function initLeads():void 
		{
			if (model.isBusy)
				return;
			
			var filter:LeadFilter = new LeadFilter();
			filter.DateFrom = model.startDate;
			filter.DateTo = model.endDate;
			filter.LeadStatuses = [1,2,3,4,5];
			
			model.isBusy = true;
			LeadService.getInstance().getLeads(filter, 
				new Responder(
					function (event:ResultEvent):void 
					{
						model.isBusy = false;
						model.leads.source = event.result as Array;
						
						updateModel();
						view.dispatchEvent(new Event("leadsLoaded"));
					},
					function (event:FaultEvent):void 
					{
						model.isBusy = false;
						Alert.show(event.fault.message);
					}));
		}
		
		private function updateInit():void 
		{
			model.customer = MainModel.getInstance().currentCustomer;
			
			ChangeWatcher.watch(model.customer, "IsOAuthInited", customerChangeHandler);
			ChangeWatcher.watch(model.customer, "IsWorkflowsInited", customerChangeHandler);
			ChangeWatcher.watch(model.customer, "IsLeadSourcesInited", customerChangeHandler);
			
			model.isWelcomeShow = !(model.customer.IsOAuthInited && model.customer.IsLeadSourcesInited && model.customer.IsWorkflowsInited);
		}
		
		private function customerChangeHandler(e:*):void
		{
			model.isWelcomeShow = !(model.customer.IsOAuthInited && model.customer.IsLeadSourcesInited && model.customer.IsWorkflowsInited);
		} 

		private function updateModel():void 
		{
			var leadsTotal:int = 0;
			var leadsConverted:int = 0;
			var leadsConvertedPct:Number = 0;
			var leadsAmount:Number = 0;
			
			var leadsNew:int = 0;
			var leadsPending:int = 0;
			
			for each (var lead:Lead in model.leads) 
			{
				switch (lead.LeadStatusId)
				{
					case LeadStatusEnum.NEW :
						leadsNew++;
						leadsTotal++;
						break;
						
					case LeadStatusEnum.PENDING :
						leadsPending++;
						leadsTotal++;
						break;
						
					case LeadStatusEnum.CANCELLED :
						leadsTotal++;
						break;
						
					case LeadStatusEnum.CONVERTED :
						leadsConverted++;
						leadsTotal++;
						break;
						
				}
			}
		}
	}
}