package com.dalworth.servman.main.businessPartner.profile
{
	import mx.collections.ArrayCollection;

	[Bindable]
	public class BusinessPartnerProfileModel
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
		
		public function BusinessPartnerProfileModel(accessPrivate:Private) 
		{
			leads = new ArrayCollection();
			projects = new ArrayCollection();
		}
	}
}	

class Private {}
