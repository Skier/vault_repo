package com.dalworth.servman.main.owner.setting.leadTypes
{
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class LeadTypesModel
	{
		public var leadTypes:ArrayCollection;
		public var isBusy:Boolean;
		
		private static var _instance:LeadTypesModel;
		public static function getInstance():LeadTypesModel
		{
			if (_instance == null)
				_instance = new LeadTypesModel(new Private());
			
			return _instance;
		}
		
		public function LeadTypesModel(accessPrivate:Private) 
		{
			leadTypes = new ArrayCollection();
		}
	}
}

class Private {}
