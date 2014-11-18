package com.dalworth.servman.main.owner
{
	import mx.collections.ArrayCollection;

	[Bindable]
	public class SalesRepModel
	{
		private static var _instance:SalesRepModel;
		public static function getInstance():SalesRepModel
		{
			if (_instance == null)
				_instance = new SalesRepModel(new Private());
			
			return _instance;
		}
		
		public function SalesRepModel(accessPrivate:Private) 
		{
			initOwnerMenu();
		}
	}
}	

class Private {}
