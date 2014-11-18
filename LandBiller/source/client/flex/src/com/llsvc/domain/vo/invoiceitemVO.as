package com.llsvc.domain.vo
{
	[RemoteClass(alias="com.llsvc.domain.cfc.invoiceitemVO")]

	[Bindable]
	public class invoiceitemVO
	{

		public var invoiceitemid:Number = 0;
		public var invoiceid:Number = 0;
		public var projectid:Number = 0;
		public var expencetypeid:Number = 0;
		public var itemdate:Date = null;
		public var quantity:Number = 0;
		public var rate:Number = 0;
		public var total:Number = 0;
		public var status:Number = 0;


		public function invoiceitemVO()
		{
		}

	}
}