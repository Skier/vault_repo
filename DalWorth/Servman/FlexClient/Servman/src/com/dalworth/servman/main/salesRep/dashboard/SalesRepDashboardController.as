package com.dalworth.servman.main.owner.dashboard
{
	import com.dalworth.servman.domain.BusinessPartner;
	import com.dalworth.servman.domain.SalesRep;
	import com.dalworth.servman.service.registry.BusinessPartnerRegistry;
	import com.dalworth.servman.service.registry.SalesRepRegistry;
	
	import mx.core.UIComponent;
	
	public class SalesRepDashboardController
	{
		private var model:SalesRepDashboardModel;
		private var view:UIComponent;
		
		public function SalesRepDashboardController(view:UIComponent)
		{
			this.model = SalesRepDashboardModel.getInstance();
			this.view = view;
		}
		
		public function initModel():void 
		{
			initSalesReps();
			initBusinessPartners();
		}
		
		public function selectContent(value:Object):void 
		{
			if (value is SalesRep)
				initModelBySalesRep(value as SalesRep);
			else if (value is BusinessPartner)
				initModelByBusinessPartner(value as BusinessPartner);
			else 
				initModelByFullContent();
		}
		
		private function initModelBySalesRep(value:SalesRep):void 
		{
		}
		
		private function initModelByBusinessPartner(value:BusinessPartner):void 
		{
		}
		
		private function initModelByFullContent():void 
		{
		}
		
		private function initSalesReps():void 
		{
			model.salesReps = SalesRepRegistry.getInstance().getAll()
		}

		private function initBusinessPartners():void 
		{
			model.businessPartners = BusinessPartnerRegistry.getInstance().getAll();
			parseBusinessPartners();
		}
		
		private function parseBusinessPartners():void 
		{
			for each (var sr:SalesRep in model.salesReps) 
			{
				sr.BusinessPartners.removeAll();
			}
			
			for each(var bp:BusinessPartner in model.businessPartners)
			{
				var salesRep:SalesRep = SalesRepRegistry.getInstance().getLocal(bp.SalesRepId) as SalesRep;
				if (salesRep != null)
					salesRep.BusinessPartners.addItem(bp);
			}
		}
		
	}
}