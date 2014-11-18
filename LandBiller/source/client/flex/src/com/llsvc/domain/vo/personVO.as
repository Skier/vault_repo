package com.llsvc.domain.vo
{
	[RemoteClass(alias="com.llsvc.domain.cfc.personVO")]

	[Bindable]
	public class personVO
	{

		public var personid:Number = 0;
		public var firstname:String = "";
		public var middlename:String = "";
		public var lastname:String = "";
		public var phone:String = "";
		public var phonealt:String = "";
		public var addressid:Number = 0;


		public function personVO()
		{
		}

	}
}