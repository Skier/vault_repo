package com.dalworth.servman.main.owner.setting.customer
{
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class CustomersModel
	{
		public var customers:ArrayCollection;
		
		private static var _instance:CustomersModel;
		public static function getInstance():CustomersModel
		{
			if (_instance == null)
				_instance = new CustomersModel(new Private());
			
			return _instance;
		}
		
		public function CustomersModel(accessPrivate:Private) 
		{
			customers = new ArrayCollection();
		}
	}
}

class Private {}
