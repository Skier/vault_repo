package com.dalworth.leadCentral.service.registry
{
	public class UserRegistry extends BaseRegistry
	{
		private static var _instance:UserRegistry;
		public static function getInstance():UserRegistry
		{
			if (_instance == null)
				_instance = new UserRegistry(new Private());
			
			return _instance;
		}
		
		public function UserRegistry(accessPrivate:Private) 
		{
			super();
		}
	}
}

class Private {}
