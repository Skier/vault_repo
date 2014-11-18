package com.dalworth.servman.main.owner.dashboard
{
	import mx.collections.ArrayCollection;

	[Bindable]
	public class SalesRepDashboardModel
	{
		public var businessPartners:ArrayCollection;
		public var currentContent:Object;
		public var leads:ArrayCollection;
		public var jobs:ArrayCollection;
		
		private static var _instance:SalesRepDashboardModel;
		public static function getInstance():SalesRepDashboardModel
		{
			if (_instance == null)
				_instance = new SalesRepDashboardModel(new Private());
			
			return _instance;
		}
		
		public function SalesRepDashboardModel(accessPrivate:Private) 
		{
			salesReps = new ArrayCollection();
			businessPartners = new ArrayCollection();
			leads = new ArrayCollection();
			jobs = new ArrayCollection();
		}
	}
}	

class Private {}
