package com.dalworth.servman.main.businessPartner.leads
{
	import com.dalworth.servman.domain.BusinessPartner;
	
	import mx.collections.ArrayCollection;

	[Bindable]
	public class BusinessPartnerLeadsModel
	{
		public static const MODEL_INITED:String = "BusinessPartnerLeadsModelInited";
		
		public var localBusinessPartners:ArrayCollection;

		public var businessPartner:BusinessPartner;
		public var leads:ArrayCollection;
		public var projects:ArrayCollection;
		
		private static var _instance:BusinessPartnerLeadsModel;
		public static function getInstance():BusinessPartnerLeadsModel
		{
			if (_instance == null)
				_instance = new BusinessPartnerLeadsModel(new Private());
			
			return _instance;
		}
		
		public function BusinessPartnerLeadsModel(accessPrivate:Private) 
		{
			leads = new ArrayCollection();
			projects = new ArrayCollection();
		}
	}
}	

class Private {}
