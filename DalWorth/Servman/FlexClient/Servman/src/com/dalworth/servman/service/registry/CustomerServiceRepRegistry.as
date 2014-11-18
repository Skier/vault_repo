package com.dalworth.servman.service.registry
{
	import com.dalworth.servman.domain.CustomerServiceRep;
	
	public class CustomerServiceRepRegistry extends BaseRegistry
	{
		private static var _instance:CustomerServiceRepRegistry;
		public static function getInstance():CustomerServiceRepRegistry
		{
			if (_instance == null)
				_instance = new CustomerServiceRepRegistry(new Private());
			
			return _instance;
		}
		
		public function CustomerServiceRepRegistry(accessPrivate:Private) 
		{
			super();
		}
	}
}

class Private {}
