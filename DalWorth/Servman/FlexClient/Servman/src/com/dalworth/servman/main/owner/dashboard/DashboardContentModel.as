package com.dalworth.servman.main.owner.dashboard
{
	import com.dalworth.servman.domain.User;
	
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class DashboardContentModel
	{
		public var currentContent:Object;
		
		public var currentUser:User;
		public var leads:ArrayCollection;
		public var startDate:Date;
		public var endDate:Date;
		
		public var contentName:String;
		
		public var totalLeads:int;
		public var contactedLeads:int;
		public var contactedLeadsPct:Number;
		public var convertedLeads:int;
		public var convertedLeadsPct:Number;
		public var averageContactTime:Number;
		
		public var closedAmount:Number;
		
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
			leads = new ArrayCollection();
			endDate = new Date();
			startDate = new Date(endDate.fullYear, endDate.month);
		}
	}
}	

class Private {}
