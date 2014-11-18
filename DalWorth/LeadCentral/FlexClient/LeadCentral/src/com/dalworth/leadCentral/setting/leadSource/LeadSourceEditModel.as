package com.dalworth.leadCentral.setting.leadSource
{
	import com.dalworth.leadCentral.domain.LeadSource;
	
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class LeadSourceEditModel
	{
		public var leadSource:LeadSource;

		public var salesReps:ArrayCollection;
		public var businessPartners:ArrayCollection;
		
		public var isBusy:Boolean;

		private static var _instance:LeadSourceEditModel;
		public static function getInstance():LeadSourceEditModel
		{
			if (_instance == null)
				_instance = new LeadSourceEditModel(new Private());
			
			return _instance;
		}
		
		public function LeadSourceEditModel(accessPrivate:Private) 
		{
			salesReps = new ArrayCollection();
			businessPartners = new ArrayCollection();
		}
	}
}

class Private {}