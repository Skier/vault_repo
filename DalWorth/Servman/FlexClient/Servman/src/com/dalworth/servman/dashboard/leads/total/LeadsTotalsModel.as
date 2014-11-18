package com.dalworth.servman.dashboard.leads.total
{
	import com.dalworth.servman.domain.Lead;
	import com.dalworth.servman.domain.LeadStatus;
	
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class LeadsTotalsModel
	{
		public var leadsTotal:Number;
		public var leadsConverted:Number;
		public var leadsConvertedPercent:Number;
		public var averageTimeToContact:Number;
		public var closedAmount:Number;

		public function set leads(value:ArrayCollection):void 
		{
			var total:Number = 0;
			var converted:Number = 0;
			var contacted:Number = 0;
			var timeToContact:Number = 0;
			
			for each (var lead:Lead in value)
			{
				total += 1;
				if (lead.LeadStatusId == LeadStatus.STATUS_CONVERTED_ID)
					converted += 1;
				if (lead.LeadStatusId != LeadStatus.STATUS_NEW_ID)
				{
					contacted += 1;
					timeToContact += (lead.DateContacted.time - lead.DateCreated.time);
				}
			}
			
			leadsTotal = total;
			leadsConverted = converted;
			leadsConvertedPercent = (leadsConverted / leadsTotal) * 100;
			averageTimeToContact = new Date(timeToContact / contacted).time;
		}
		
		private static var _instance:LeadsTotalsModel;
		public static function getInstance():LeadsTotalsModel
		{
			if (_instance == null)
				_instance = new LeadsTotalsModel(new Private());
			
			return _instance;
		}
		
		public function LeadsTotalsModel(accessPrivate:Private) 
		{
		}
		
	}
}

class Private {}
