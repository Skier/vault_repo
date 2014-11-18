package com.llsvc.domain.vo
{
	[RemoteClass(alias="com.llsvc.domain.cfc.invoiceVO")]

	[Bindable]
	public class invoiceVO
	{

		public var invoiceid:Number = 0;
		public var userid:Number = 0;
		public var clientid:Number = 0;
		public var invoicedate:Date = null;
		public var startdate:Date = null;
		public var enddate:Date = null;
		public var amount:Number = 0;
		public var adjustment:Number = 0;
		public var total:Number = 0;
		public var status:String = "";
		public var invoiceno:String = "";


		public function invoiceVO()
		{
		}

	}
}