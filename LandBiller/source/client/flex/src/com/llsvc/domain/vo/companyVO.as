package com.llsvc.domain.vo
{
	[RemoteClass(alias="com.llsvc.domain.cfc.companyVO")]

	[Bindable]
	public class companyVO
	{

		public var companyid:Number = 0;
		public var userid:Number = 0;
		public var name:String = "";
		public var description:String = "";


		public function companyVO()
		{
		}

	}
}