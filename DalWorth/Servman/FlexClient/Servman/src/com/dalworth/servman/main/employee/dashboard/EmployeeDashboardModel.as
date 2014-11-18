package com.dalworth.servman.main.owner.dashboard
{
	import mx.collections.ArrayCollection;

	[Bindable]
	public class EmployeeDashboardModel
	{
		public var businessPartners:ArrayCollection;
		public var leads:ArrayCollection;
		public var projects:ArrayCollection;
		
		private static var _instance:EmployeeDashboardModel;
		public static function getInstance():EmployeeDashboardModel
		{
			if (_instance == null)
				_instance = new EmployeeDashboardModel(new Private());
			
			return _instance;
		}
		
		public function EmployeeDashboardModel(accessPrivate:Private) 
		{
			businessPartners = new ArrayCollection();
			leads = new ArrayCollection();
			projects = new ArrayCollection();
		}
	}
}	

class Private {}
