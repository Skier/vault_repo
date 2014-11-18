package com.dalworth.servman.service.registry
{
	import com.dalworth.servman.domain.SalesRep;
	
	public class SalesRepRegistry extends BaseRegistry
	{
		private static var _instance:SalesRepRegistry;
		public static function getInstance():SalesRepRegistry
		{
			if (_instance == null)
				_instance = new SalesRepRegistry(new Private());
			
			return _instance;
		}
		
		public function SalesRepRegistry(accessPrivate:Private) 
		{
			super();
		}
	}
}

class Private {}
