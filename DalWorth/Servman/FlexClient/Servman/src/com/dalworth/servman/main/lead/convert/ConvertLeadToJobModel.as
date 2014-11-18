package com.dalworth.servman.main.lead.convert
{
	import com.dalworth.servman.domain.Job;
	import com.dalworth.servman.domain.Lead;
	
	[Bindable]
	public class ConvertLeadToJobModel
	{
		public var currentLead:Lead;
		public var currentJob:Job;
		
		public var isBusy:Boolean = false;
		
		private static var _instance:ConvertLeadToJobModel;
		public static function getInstance():ConvertLeadToJobModel
		{
			if (_instance == null)
				_instance = new ConvertLeadToJobModel(new Private());
			
			return _instance;
		}
		
		public function ConvertLeadToJobModel(accessPrivate:Private) 
		{
			currentJob = new Job();
		}
		
		public function reset():void 
		{
			currentJob = new Job();
		}
	}
}

class Private {}
