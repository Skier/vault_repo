package com.dalworth.leadCentral.dashboard.graph.daily
{
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class LeadsDailyModel
	{
		public var dailyCollection:ArrayCollection;

		public var leads:ArrayCollection;
		public var startDate:Date;
		public var endDate:Date;

		private static var _instance:LeadsDailyModel;
		public static function getInstance():LeadsDailyModel
		{
			if (_instance == null)
				_instance = new LeadsDailyModel(new Private());
			
			return _instance;
		}
		
		public function LeadsDailyModel(accessPrivate:Private) 
		{
			dailyCollection = new ArrayCollection();
		}
		
	}
}

class Private {}
