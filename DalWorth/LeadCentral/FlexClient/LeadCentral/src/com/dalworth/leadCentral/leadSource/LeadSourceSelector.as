package com.dalworth.leadCentral.leadSource
{
	import com.affilia.controls.TreeComboBox;
	import com.dalworth.leadCentral.service.registry.LeadSourceRegistry;
	
	import mx.collections.ArrayCollection;

	public class LeadSourceSelector extends TreeComboBox
	{
		public function LeadSourceSelector()
		{
			super();
			dataProvider = getDataProvider();
		}
		
		private function getDataProvider():ArrayCollection
		{
			var source = LeadSourceRegistry.getInstance().getAll();
			
		}
		
		private function parseLeadSources(leadSource:Array):void 
		{
			var customerType:CustomerType;
			var sort:Sort;

			for each (customerType in customerTypes)
			{
				if (model.leadSource.QbCustomerTypeId == customerType.IdStr)
					model.leadSource.qbCustomerType = customerType;
			}
			
			for each (customerType in customerTypes)
			{
				customerType.parentType = getCustomerTypeById(customerType.CustomerTypeParentId, customerTypes);
			
				if (customerType.parentType != null)
					customerType.parentType.relatedTypes.addItem(customerType);
			}

			var result:Array = new Array();
			for each (customerType in customerTypes)
			{
				sort = new Sort();
				sort.fields = [new SortField("Name")];
				customerType.relatedTypes.sort = sort;
				customerType.relatedTypes.refresh();
				
				if (customerType.parentType == null)
					result.push(customerType);
			}
			model.customerTypes.source = result;
			
			sort = new Sort();
			sort.fields = [new SortField("Name")];
			model.customerTypes.sort = sort;
			model.customerTypes.refresh();
		}
		
	}
}