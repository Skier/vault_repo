package com.dalworth.servman.main
{
	import com.dalworth.servman.domain.User;
	
	import mx.collections.ArrayCollection;

	[Bindable]
	public class MainAppModel
	{
		public var currentUser:User;
		
		private static var _instance:MainAppModel;
		public static function getInstance():MainAppModel
		{
			if (_instance == null)
				_instance = new MainAppModel(new Private());
			
			return _instance;
		}
		
		public function MainAppModel(accessPrivate:Private) 
		{
		}
	}
}

class Private {}
