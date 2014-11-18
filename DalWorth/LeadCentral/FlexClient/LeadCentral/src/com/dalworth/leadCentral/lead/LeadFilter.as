package com.dalworth.leadCentral.lead
{
	[Bindable]
    [RemoteClass(alias="Dalworth.LeadCentral.Domain.LeadFilter")]
	public class LeadFilter
	{
		public var DateFrom:Date;
		public var DateTo:Date;
		public var LeadSourceId:int;
		public var AssignedToUserId:int;
		public var CreatedByUserId:int;
		public var LeadStatuses:Array;
		
		public function LeadFilter()
		{
		}

	}
}