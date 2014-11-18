package com.dalworth.servman.dashboard.leads.total
{
	import mx.core.UIComponent;
	
	public class LeadsTotalsController
	{
		private var model:LeadsTotalsModel;
		private var view:UIComponent;
		
		public function LeadsTotalsController(view:UIComponent)
		{
			this.view = view;
			this.model = LeadsTotalsModel.getInstance();
		}

	}
}