package com.dalworth.servman.main.employee.project
{
	import mx.collections.ArrayCollection;

	[Bindable]
	public class EmployeeProjectsModel
	{
		public static const MODEL_INITED:String = "OwnerProjectsModelInited";
		
		public var businessPartners:ArrayCollection;
		public var projects:ArrayCollection;
		
		private static var _instance:EmployeeProjectsModel;
		public static function getInstance():EmployeeProjectsModel
		{
			if (_instance == null)
				_instance = new EmployeeProjectsModel(new Private());
			
			return _instance;
		}
		
		public function EmployeeProjectsModel(accessPrivate:Private) 
		{
			projects = new ArrayCollection();
		}
	}
}	

class Private {}
