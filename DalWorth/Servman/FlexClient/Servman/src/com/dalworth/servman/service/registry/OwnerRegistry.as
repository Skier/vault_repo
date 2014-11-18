package com.dalworth.servman.service.registry
{
	import com.dalworth.servman.domain.Owner;
	
	public class OwnerRegistry extends BaseRegistry
	{
		private static var _instance:OwnerRegistry;
		public static function getInstance():OwnerRegistry
		{
			if (_instance == null)
				_instance = new OwnerRegistry(new Private());
			
			return _instance;
		}
		
		public function OwnerRegistry(accessPrivate:Private) 
		{
			super();
		}
	}
}

class Private {}
