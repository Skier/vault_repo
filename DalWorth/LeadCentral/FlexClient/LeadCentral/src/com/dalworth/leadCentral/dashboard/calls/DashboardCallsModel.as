package com.dalworth.leadCentral.dashboard.calls
{
	import mx.collections.ArrayCollection;
	import mx.collections.Sort;
	import mx.collections.SortField;
	
	[Bindable]
	public class DashboardCallsModel
	{
		public var dailyCollection:ArrayCollection;
		
		public var calls:ArrayCollection;
		public var callTransactions:ArrayCollection;
		
		public var selectedLeadSources:ArrayCollection; 
		
		public var startDate:Date;
		public var endDate:Date;
		
		public var isBusy:Boolean;

		private static var _instance:DashboardCallsModel;
		public static function getInstance():DashboardCallsModel
		{
			if (_instance == null)
				_instance = new DashboardCallsModel(new Private());
			
			return _instance;
		}
		
		public function DashboardCallsModel(accessPrivate:Private) 
		{
			dailyCollection = new ArrayCollection();
			calls = new ArrayCollection();
			callTransactions = new ArrayCollection();
		}
		
	}
}

class Private {}
