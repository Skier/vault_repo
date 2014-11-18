package com.dalworth.servman.main.employee
{
	import mx.collections.ArrayCollection;

	[Bindable]
	public class EmployeeModel
	{
		public var customers:ArrayCollection;
		public var businessPartners:ArrayCollection;
		
		public var ownerMenuContent:ArrayCollection;
		
		private static var _instance:EmployeeModel;
		public static function getInstance():EmployeeModel
		{
			if (_instance == null)
				_instance = new EmployeeModel(new Private());
			
			return _instance;
		}
		
		public function EmployeeModel(accessPrivate:Private) 
		{
			customers = new ArrayCollection();
			businessPartners = new ArrayCollection();
		}
	}
}	

class Private {}
