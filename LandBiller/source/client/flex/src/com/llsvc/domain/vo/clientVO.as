package com.llsvc.domain.vo
{
	[RemoteClass(alias="com.llsvc.domain.cfc.clientVO")]

	[Bindable]
	public class clientVO
	{

		public var clientid:Number = 0;
		public var userid:Number = 0;
		public var personid:Number = 0;
		public var name:String = "";
		public var description:String = "";
		public var companyid:Number = 0;


		public function clientVO()
		{
		}

	}
}