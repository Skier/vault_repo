package com.dalworth.servman.main.owner.setting.owner
{
	import com.dalworth.servman.domain.Owner;
	
	[Bindable]
	public class OwnerEditModel
	{
		public var owner:Owner;
		
		public var isBusy:Boolean;

		private static var _instance:OwnerEditModel;
		public static function getInstance():OwnerEditModel
		{
			if (_instance == null)
				_instance = new OwnerEditModel(new Private());
			
			return _instance;
		}
		
		public function OwnerEditModel(accessPrivate:Private) 
		{
		}
	}
}

class Private {}