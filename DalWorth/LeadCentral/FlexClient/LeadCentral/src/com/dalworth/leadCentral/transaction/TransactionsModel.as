package com.dalworth.leadCentral.transaction
{
	import mx.collections.ArrayCollection;
	import mx.collections.Sort;
	import mx.collections.SortField;

	[Bindable]
	public class TransactionsModel
	{
        private const DEFAULT_PAGE_SIZE:int = 25;
        
        public var pageSizes:ArrayCollection = new ArrayCollection([10,25,50,100]);

		public var isBusy:Boolean = false;
		
		public var transactions:ArrayCollection;
		
		public var transactionsCount:int;
		public var pages:ArrayCollection;
		public var currentPage:int;
		public var selectedPage:int;
		public var pageSize:int;
		
		private static var _instance:TransactionsModel;
		public static function getInstance():TransactionsModel
		{
			if (_instance == null)
				_instance = new TransactionsModel(new Private());
			
			return _instance;
		}
		
		public function TransactionsModel(accessPrivate:Private) 
		{
			transactions = new ArrayCollection();

			var sort:Sort = new Sort();
			sort.fields = [new SortField("timeStamp", false, true, true)];
			transactions.sort = sort;
			transactions.refresh();
			
			pages = new ArrayCollection();
			pageSize = DEFAULT_PAGE_SIZE;
		}
	}
}	

class Private {}
