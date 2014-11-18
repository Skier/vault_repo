package com.llsvc.domain.vo
{
	[RemoteClass(alias="com.llsvc.domain.cfc.loginVO")]

	[Bindable]
	public class loginVO
	{

		public var loginid:Number = 0;
		public var username:String = "";
		public var password:String = "";
		public var email:String = "";
		public var personid:Number = 0;


		public function loginVO()
		{
		}

	}
}