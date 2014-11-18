package com.llsvc.domain.vo
{
	[RemoteClass(alias="com.llsvc.domain.cfc.projectVO")]

	[Bindable]
	public class projectVO
	{

		public var projectid:Number = 0;
		public var clientid:Number = 0;
		public var projectname:String = "";
		public var afe:String = "";
		public var description:String = "";
		public var status:String = "";


		public function projectVO()
		{
		}

	}
}