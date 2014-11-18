package com.dalworth.servman.main.salesRep.lead
{
	import mx.collections.ArrayCollection;
	import mx.collections.Sort;
	import mx.collections.SortField;

	[Bindable]
	public class SalesRepLeadsModel
	{
		public var isBusy:Boolean = false;
		public var leads:ArrayCollection;
		
		private static var _instance:SalesRepLeadsModel;
		public static function getInstance():SalesRepLeadsModel
		{
			if (_instance == null)
				_instance = new SalesRepLeadsModel(new Private());
			
			return _instance;
		}
		
		public function SalesRepLeadsModel(accessPrivate:Private) 
		{
			leads = new ArrayCollection();
			
			var sort:Sort = new Sort();
			sort.fields = [new SortField("LeadStatusId"), new SortField("timeCreated", false, true), new SortField("FirstName"), new SortField("LastName")];
			leads.sort = sort;
		}
	}
}	

class Private {}
