package com.dalworth.servman.main.owner.dashboard
{
	import mx.collections.ArrayCollection;

	[Bindable]
	public class OwnerDashboardModel
	{
		public var salesReps:ArrayCollection;
		public var businessPartners:ArrayCollection;
		public var currentContent:Object;
		public var leads:ArrayCollection;
		public var jobs:ArrayCollection;
		
		private static var _instance:OwnerDashboardModel;
		public static function getInstance():OwnerDashboardModel
		{
			if (_instance == null)
				_instance = new OwnerDashboardModel(new Private());
			
			return _instance;
		}
		
		public function OwnerDashboardModel(accessPrivate:Private) 
		{
			salesReps = new ArrayCollection();
			businessPartners = new ArrayCollection();
			leads = new ArrayCollection();
			jobs = new ArrayCollection();
		}
	}
}	

class Private {}
