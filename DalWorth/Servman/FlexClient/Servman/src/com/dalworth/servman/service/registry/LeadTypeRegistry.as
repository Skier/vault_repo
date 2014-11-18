package com.dalworth.servman.service.registry
{
	import com.dalworth.servman.domain.LeadType;
	
	public class LeadTypeRegistry extends BaseRegistry
	{
		private static var _instance:LeadTypeRegistry;
		public static function getInstance():LeadTypeRegistry
		{
			if (_instance == null)
				_instance = new LeadTypeRegistry(new Private());
			
			return _instance;
		}
		
		public function LeadTypeRegistry(accessPrivate:Private) 
		{
			super();
		}
	}
}

class Private {}
