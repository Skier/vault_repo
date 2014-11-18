package com.dalworth.servman.main.owner.lead
{
	import mx.collections.ArrayCollection;
	import mx.collections.Sort;
	import mx.collections.SortField;

	[Bindable]
	public class OwnerLeadsModel
	{
		public static const MODEL_INITED:String = "OwnerLeadsModelInited";
		
		public var isBusy:Boolean = false;
		
		public var localBusinessPartners:ArrayCollection;

		public var leads:ArrayCollection;
		public var businessPartners:ArrayCollection;
		public var projects:ArrayCollection;
		
		private static var _instance:OwnerLeadsModel;
		public static function getInstance():OwnerLeadsModel
		{
			if (_instance == null)
				_instance = new OwnerLeadsModel(new Private());
			
			return _instance;
		}
		
		public function OwnerLeadsModel(accessPrivate:Private) 
		{
			leads = new ArrayCollection();
			
			var sort:Sort = new Sort();
			sort.fields = [new SortField("LeadStatusId"), new SortField("timeCreated", false, true), new SortField("FirstName"), new SortField("LastName")];
			leads.sort = sort;
			
			projects = new ArrayCollection();
		}
	}
}	

class Private {}
