package com.dalworth.servman.main.employee.lead
{
	import com.dalworth.servman.domain.Lead;
	
	import mx.collections.ArrayCollection;

	[Bindable]
	public class EmployeeLeadsModel
	{
		public static const MODEL_INITED:String = "OwnerLeadsModelInited";
		
		public var localBusinessPartners:ArrayCollection;

		public var businessPartners:ArrayCollection;
		public var leads:ArrayCollection;
		public var projects:ArrayCollection;
		
		private static var _instance:EmployeeLeadsModel;
		public static function getInstance():EmployeeLeadsModel
		{
			if (_instance == null)
				_instance = new EmployeeLeadsModel(new Private());
			
			return _instance;
		}
		
		public function EmployeeLeadsModel(accessPrivate:Private) 
		{
			leads = new ArrayCollection();
			projects = new ArrayCollection();
		}
	}
}	

class Private {}
