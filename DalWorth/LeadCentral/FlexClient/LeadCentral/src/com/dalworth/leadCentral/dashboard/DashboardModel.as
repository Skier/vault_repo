package com.dalworth.leadCentral.dashboard
{
	import com.dalworth.leadCentral.domain.LeadSource;
	
	import mx.collections.ArrayCollection;
	import mx.collections.Sort;
	import mx.collections.SortField;

	[Bindable]
	public class DashboardModel
	{
		public var leadSources:ArrayCollection;
		public var salesReps:ArrayCollection;
		public var businessPartners:ArrayCollection;
		
		public var salesRepId:int;
		public var businessPartnerId:int;
		
		private static var _instance:DashboardModel;
		public static function getInstance():DashboardModel
		{
			if (_instance == null)
				_instance = new DashboardModel(new Private());
			
			return _instance;
		}
		
		public function DashboardModel(accessPrivate:Private) 
		{
			initLeadSources();
			initSalesReps();
			initBusinessPartner();
		}

		private function initLeadSources():void 
		{
			leadSources = new ArrayCollection();
			
			var sort:Sort = new Sort();
			sort.fields = [new SortField("Name", true)];
			leadSources.sort = sort;
			leadSources.filterFunction = filterFunction;
			leadSources.refresh();
		}

		private function initSalesReps():void 
		{
			salesReps = new ArrayCollection();
			
			var sort:Sort = new Sort();
			sort.fields = [new SortField("Name", true)];
			salesReps.sort = sort;
			salesReps.refresh();
		}

		private function initBusinessPartner():void 
		{
			businessPartners = new ArrayCollection();
			
			var sort:Sort = new Sort();
			sort.fields = [new SortField("Name", true)];
			businessPartners.sort = sort;
			businessPartners.refresh();
		}
		
		private function filterFunction(object:Object):Boolean 
		{
			if (object is LeadSource)
			{
				var leadSource:LeadSource = object as LeadSource;

				if (salesRepId > 0 && leadSource.OwnedByUserId != salesRepId)
					return false;

				if (businessPartnerId > 0 && leadSource.UserId != businessPartnerId)
					return false;
				
				return true;
			} else 
			{
				return false;
			}
		}
	}
}	

class Private {}
