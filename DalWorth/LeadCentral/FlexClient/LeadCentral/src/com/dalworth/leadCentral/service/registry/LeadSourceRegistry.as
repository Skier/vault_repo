package com.dalworth.leadCentral.service.registry
{
	import com.dalworth.leadCentral.domain.LeadSource;
	
	public class LeadSourceRegistry extends BaseRegistry
	{
		private static var _instance:LeadSourceRegistry;
		public static function getInstance():LeadSourceRegistry
		{
			if (_instance == null)
				_instance = new LeadSourceRegistry(new Private());
			
			return _instance;
		}
		
		public function LeadSourceRegistry(accessPrivate:Private) 
		{
			super();
		}
	}
}

class Private {}
