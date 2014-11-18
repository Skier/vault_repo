package com.llsvc.domain.vo
{
	[RemoteClass(alias="com.llsvc.domain.cfc.invoiceitemattachmentVO")]

	[Bindable]
	public class invoiceitemattachmentVO
	{

		public var invoiceitemattachmentid:Number = 0;
		public var invoiceitemid:Number = 0;
		public var fileid:Number = 0;


		public function invoiceitemattachmentVO()
		{
		}

	}
}