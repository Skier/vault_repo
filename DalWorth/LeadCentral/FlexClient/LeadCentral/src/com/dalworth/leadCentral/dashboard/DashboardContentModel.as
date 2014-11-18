package com.dalworth.leadCentral.dashboard
{
	import com.dalworth.leadCentral.domain.AmountSummary;
	import com.dalworth.leadCentral.domain.LeadSource;
	
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class DashboardContentModel
	{
		public var currentLeadSource:LeadSource;
		public var currentLeadSources:ArrayCollection;

		public var summary:AmountSummary;
		
		public var leads:ArrayCollection;
		public var startDate:Date;
		public var endDate:Date;
		
		public var totalLeads:int;
		public var contactedLeads:int;
		public var contactedLeadsPct:Number;
		public var convertedLeads:int;
		public var convertedLeadsPct:Number;
		public var averageContactTime:Number;
		
		public var isBusy:Boolean = false;
		
		private static var _instance:DashboardContentModel;
		public static function getInstance():DashboardContentModel
		{
			if (_instance == null)
				_instance = new DashboardContentModel(new Private());
			
			return _instance;
		}
		
		public function DashboardContentModel(accessPrivate:Private) 
		{
			currentLeadSources = new ArrayCollection();
			
			leads = new ArrayCollection();
			
			var now:Date = new Date();
			var morning:Date = new Date(now.fullYear, now.month, now.date);
			endDate = new Date(morning.time + 86400000 - 1);
			startDate = new Date(morning.time - 604800000);
		}
	}
}	

class Private {}
