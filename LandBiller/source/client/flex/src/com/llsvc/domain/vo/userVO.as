package com.llsvc.domain.vo
{
	[RemoteClass(alias="com.llsvc.domain.cfc.userVO")]

	[Bindable]
	public class userVO
	{

		public var userid:Number = 0;
		public var loginid:Number = 0;
		public var logourl:String = "";


		public function userVO()
		{
		}

	}
}