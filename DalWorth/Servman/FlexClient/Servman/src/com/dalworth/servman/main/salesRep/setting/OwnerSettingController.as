package com.dalworth.servman.main.owner.setting
{
	import com.dalworth.servman.service.registry.BusinessPartnerRegistry;
	import com.dalworth.servman.service.registry.CustomerServiceRepRegistry;
	import com.dalworth.servman.service.registry.LeadTypeRegistry;
	import com.dalworth.servman.service.registry.OwnerRegistry;
	import com.dalworth.servman.service.registry.SalesRepRegistry;
	
	import mx.core.UIComponent;
	
	public class OwnerSettingController
	{
		private var view:UIComponent;
		private var model:OwnerSettingModel = OwnerSettingModel.getInstance();
		
		public function OwnerSettingController(view:UIComponent)
		{
			this.view = view;
		}
		
		public function initModel():void 
		{
			model.owners = OwnerRegistry.getInstance().getAll();
			model.salesReps = SalesRepRegistry.getInstance().getAll();
			model.businessPartners = BusinessPartnerRegistry.getInstance().getAll();
			model.customerServiceReps = CustomerServiceRepRegistry.getInstance().getAll();

			model.leadTypes = LeadTypeRegistry.getInstance().getAll();
		}
	}
}