package com.llsvc.domain.vo
{
	[RemoteClass(alias="com.llsvc.domain.cfc.noteVO")]

	[Bindable]
	public class noteVO
	{

		public var noteid:Number = 0;
		public var userid:Number = 0;
		public var invoiceid:Number = 0;
		public var notedate:Date = null;
		public var notetext:String = "";
		public var notefrom:String = "";


		public function noteVO()
		{
		}

	}
}