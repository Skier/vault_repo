package com.dalworth.leadCentral.lead
{
	import com.dalworth.leadCentral.domain.AmountSummary;
	
	import mx.collections.ArrayCollection;
	import mx.collections.Sort;
	import mx.collections.SortField;

	[Bindable]
	public class LeadsManageModel
	{
		public static const MODEL_INITED:String = "LeadsModelInited";
        private const DEFAULT_PAGE_SIZE:int = 25;
		
		public var isBusy:Boolean = false;
		
		public var leads:ArrayCollection;

		public var summary:AmountSummary;
		
		public var leadsCount:int;
		public var pages:ArrayCollection;
		public var currentPage:int;
		public var pageSize:int;
		
		private static var _instance:LeadsManageModel;
		public static function getInstance():LeadsManageModel
		{
			if (_instance == null)
				_instance = new LeadsManageModel(new Private());
			
			return _instance;
		}
		
		public function LeadsManageModel(accessPrivate:Private) 
		{
			leads = new ArrayCollection();
/* 			
			var sort:Sort = new Sort();
			sort.fields = [new SortField("LeadStatusId"), new SortField("timeCreated", false, true, true)];
			leads.sort = sort;
			leads.refresh();
 */			
			pages = new ArrayCollection();
			pageSize = DEFAULT_PAGE_SIZE;
		}
	}
}	

class Private {}
