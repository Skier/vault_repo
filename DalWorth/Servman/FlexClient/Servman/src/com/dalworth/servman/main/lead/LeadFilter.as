package com.dalworth.servman.main.lead
{
	[Bindable]
    [RemoteClass(alias="Servman.Domain.LeadFilter")]
	public class LeadFilter
	{
		public var DateFrom:Date;
		public var DateTo:Date;
		public var SalesRepId:int;
		public var BusinessPartnerId:int;
		public var AssignedToUserId:int;
		public var CreatedByUserId:int;
		public var LeadTypeId:int;
		public var LeadStatuses:Array;
		
		public function LeadFilter()
		{
		}

	}
}