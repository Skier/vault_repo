package com.dalworth.servman.main.employee.businessPartner
{
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class BusinessPartnersModel
	{
		public static const LEADS_INITED:String = "BusinessPartnerLeadsInited";
		
		public var businessPartners:ArrayCollection;
		public var leads:ArrayCollection;
		public var projects:ArrayCollection;
		
		private static var _instance:BusinessPartnersModel;
		public static function getInstance():BusinessPartnersModel
		{
			if (_instance == null)
				_instance = new BusinessPartnersModel(new Private());
			
			return _instance;
		}
		
		public function BusinessPartnersModel(accessPrivate:Private) 
		{
			businessPartners = new ArrayCollection();
			leads = new ArrayCollection();
			projects = new ArrayCollection();
		}
	}
}

class Private {}
