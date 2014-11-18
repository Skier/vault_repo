package com.dalworth.leadCentral.domain
{
    [Bindable]
    [RemoteClass(alias="Dalworth.LeadCentral.Domain.LeadAmountSummary")]
	public class LeadAmountSummary extends AmountSummary
	{
		public var LeadId:int;
		public var InvoiceStatus:String;
		public var IsInvoiced:Boolean;
	}
}
