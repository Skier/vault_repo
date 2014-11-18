package com.dalworth.servman.main.lead.leadConnection
{
	import com.dalworth.servman.domain.Lead;
	
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class LeadConnectionModel
	{
		public var leads:ArrayCollection;
		public var currentLead:Lead;
		public var jobs:ArrayCollection;
		public var dateFrom:Date;
		
		public var isBusy:Boolean = false;
		
		private static var _instance:LeadConnectionModel;
		public static function getInstance():LeadConnectionModel
		{
			if (_instance == null)
				_instance = new LeadConnectionModel(new Private());
			
			return _instance;
		}
		
		public function LeadConnectionModel(accessPrivate:Private) 
		{
			leads = new ArrayCollection();
			jobs = new ArrayCollection();
		}
	}
}

class Private {}
