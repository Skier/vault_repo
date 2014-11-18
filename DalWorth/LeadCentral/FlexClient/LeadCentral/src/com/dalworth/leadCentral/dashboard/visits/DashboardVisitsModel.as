package com.dalworth.leadCentral.dashboard.visits
{
	import mx.collections.ArrayCollection;
	import mx.collections.Sort;
	import mx.collections.SortField;
	
	[Bindable]
	public class DashboardVisitsModel
	{
		public var dailyCollection:ArrayCollection;
		
		public var visits:ArrayCollection;
		public var pageViews:ArrayCollection;
		
		public var selectedLeadSources:ArrayCollection; 
		
		public var startDate:Date;
		public var endDate:Date;
		
		public var isBusy:Boolean;

		private static var _instance:DashboardVisitsModel;
		public static function getInstance():DashboardVisitsModel
		{
			if (_instance == null)
				_instance = new DashboardVisitsModel(new Private());
			
			return _instance;
		}
		
		public function DashboardVisitsModel(accessPrivate:Private) 
		{
			dailyCollection = new ArrayCollection();
			visits = new ArrayCollection();
			pageViews = new ArrayCollection();
		}
		
	}
}

class Private {}
