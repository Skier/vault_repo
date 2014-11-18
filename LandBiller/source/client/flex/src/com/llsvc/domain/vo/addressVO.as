package com.llsvc.domain.vo
{
	[RemoteClass(alias="com.llsvc.domain.cfc.addressVO")]

	[Bindable]
	public class addressVO
	{

		public var addressid:Number = 0;
		public var address1:String = "";
		public var address2:String = "";
		public var city:String = "";
		public var stateid:Number = 0;
		public var zip:String = "";


		public function addressVO()
		{
		}

	}
}