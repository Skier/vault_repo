package com.dalworth.leadCentral.setting.user
{
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class UsersModel
	{
		public var users:ArrayCollection;
		public var isBusy:Boolean;
		
		private static var _instance:UsersModel;
		public static function getInstance():UsersModel
		{
			if (_instance == null)
				_instance = new UsersModel(new Private());
			
			return _instance;
		}
		
		public function UsersModel(accessPrivate:Private) 
		{
			users = new ArrayCollection();
		}
	}
}

class Private {}
