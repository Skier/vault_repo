package com.dalworth.leadCentral.owner
{
	[Bindable]
	public class OwnerModel
	{
		public var isBusy:Boolean = false;
		
		private static var _instance:OwnerModel;
		public static function getInstance():OwnerModel
		{
			if (_instance == null)
				_instance = new OwnerModel(new Private());
			
			return _instance;
		}
		
		public function OwnerModel(accessPrivate:Private) 
		{
		}
	}
}	

class Private {}
