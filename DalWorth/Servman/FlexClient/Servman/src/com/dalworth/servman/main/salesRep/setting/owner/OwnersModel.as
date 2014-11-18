package com.dalworth.servman.main.owner.setting.owner
{
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class OwnersModel
	{
		public var owners:ArrayCollection;
		public var isBusy:Boolean;
		
		private static var _instance:OwnersModel;
		public static function getInstance():OwnersModel
		{
			if (_instance == null)
				_instance = new OwnersModel(new Private());
			
			return _instance;
		}
		
		public function OwnersModel(accessPrivate:Private) 
		{
			owners = new ArrayCollection();
		}
	}
}

class Private {}
