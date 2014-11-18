package com.dalworth.leadCentral.dashboard.leads
{
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class DashboardLeadsModel
	{
		public var dailyCollection:ArrayCollection;

		public var leads:ArrayCollection;
		public var startDate:Date;
		public var endDate:Date;

		private static var _instance:DashboardLeadsModel;
		public static function getInstance():DashboardLeadsModel
		{
			if (_instance == null)
				_instance = new DashboardLeadsModel(new Private());
			
			return _instance;
		}
		
		public function DashboardLeadsModel(accessPrivate:Private) 
		{
			dailyCollection = new ArrayCollection();
		}
		
	}
}

class Private {}
