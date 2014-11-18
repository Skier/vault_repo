package com.dalworth.servman.service.registry
{
	import com.dalworth.servman.domain.User;
	
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
