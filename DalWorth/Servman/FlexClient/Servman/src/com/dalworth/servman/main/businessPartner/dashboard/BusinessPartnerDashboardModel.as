package com.dalworth.servman.main.businessPartner.dashboard
{
	import mx.collections.ArrayCollection;

	[Bindable]
	public class BusinessPartnerDashboardModel
	{
		public var leads:ArrayCollection;
		public var projects:ArrayCollection;
		
		private static var _instance:BusinessPartnerDashboardModel;
		public static function getInstance():BusinessPartnerDashboardModel
		{
			if (_instance == null)
				_instance = new BusinessPartnerDashboardModel(new Private());
			
			return _instance;
		}
		
		public function BusinessPartnerDashboardModel(accessPrivate:Private) 
		{
			leads = new ArrayCollection();
			projects = new ArrayCollection();
		}
	}
}	

class Private {}
