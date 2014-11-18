package com.dalworth.servman.service.registry
{
	import com.dalworth.servman.domain.BusinessPartner;
	
	public class BusinessPartnerRegistry extends BaseRegistry
	{
		private static var _instance:BusinessPartnerRegistry;
		public static function getInstance():BusinessPartnerRegistry
		{
			if (_instance == null)
				_instance = new BusinessPartnerRegistry(new Private());
			
			return _instance;
		}
		
		public function BusinessPartnerRegistry(accessPrivate:Private) 
		{
			super();
		}
	}
}

class Private {}
