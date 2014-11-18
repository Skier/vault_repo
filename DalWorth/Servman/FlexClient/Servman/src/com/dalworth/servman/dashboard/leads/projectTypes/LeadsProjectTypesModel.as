package com.dalworth.servman.dashboard.leads.projectTypes
{
	import com.dalworth.servman.domain.Lead;
	import com.dalworth.servman.domain.LeadStatus;
	
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class LeadsProjectTypesModel
	{
		public var typesCollection:ArrayCollection;

		private static var _instance:LeadsProjectTypesModel;
		public static function getInstance():LeadsProjectTypesModel
		{
			if (_instance == null)
				_instance = new LeadsProjectTypesModel(new Private());
			
			return _instance;
		}
		
		public function LeadsProjectTypesModel(accessPrivate:Private) 
		{
			typesCollection = new ArrayCollection();
		}
		
	}
}

class Private {}
