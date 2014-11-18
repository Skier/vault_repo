package com.dalworth.servman.main.businessPartner
{
	import com.dalworth.servman.domain.BusinessPartner;

	[Bindable]
	public class BusinessPartnerModel
	{
		public var businessPartner:BusinessPartner;
		
		private static var _instance:BusinessPartnerModel;
		public static function getInstance():BusinessPartnerModel
		{
			if (_instance == null)
				_instance = new BusinessPartnerModel(new Private());
			
			return _instance;
		}
		
		public function BusinessPartnerModel(accessPrivate:Private) 
		{
		}
	}
}	

class Private {}
