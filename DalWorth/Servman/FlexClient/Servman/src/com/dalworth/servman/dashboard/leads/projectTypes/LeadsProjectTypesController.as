package com.dalworth.servman.dashboard.leads.projectTypes
{
	import com.dalworth.servman.domain.Lead;
	import com.dalworth.servman.domain.LeadType;
	import com.dalworth.servman.service.registry.LeadTypeRegistry;
	
	import mx.collections.ArrayCollection;
	import mx.core.UIComponent;
	
	public class LeadsProjectTypesController
	{
		private var model:LeadsProjectTypesModel;
		private var view:UIComponent;
		
		public function LeadsProjectTypesController(view:UIComponent)
		{
			this.view = view;
			this.model = LeadsProjectTypesModel.getInstance();
		}
		
		public function setLeads(value:ArrayCollection):void
		{
			model.typesCollection.removeAll();
			
			var leadTypes:ArrayCollection = LeadTypeRegistry.getInstance().getAll();
			
			for each (var lead:Lead in value) 
			{
				var type:LeadType = LeadTypeRegistry.getInstance().getLocal(lead.LeadTypeId) as LeadType;
				var item:Object = getItemByType(type);
				item["total"] = (item["total"] as int) + 1;
			}
		}
		
		private function getItemByType(leadType:LeadType):Object 
		{
			var item:Object;
			if (leadType != null) 
			{
				for each (item in model.typesCollection)
				{
					if ((item["typeId"] as int) == leadType.Id)
						return item;
				}
				item = {typeId:leadType.Id, name:leadType.Name, total:0};
			} else 
			{
				for each (item in model.typesCollection)
				{
					if ((item["typeId"] as int) == 0)
						return item;
				}
				item = {typeId:0, name:"[unknown]", total:0};
			}
			model.typesCollection.addItem(item);
			return item;
		}

	}
}