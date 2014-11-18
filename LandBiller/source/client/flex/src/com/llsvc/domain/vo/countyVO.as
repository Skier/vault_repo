package com.llsvc.domain.vo
{
	[RemoteClass(alias="com.llsvc.domain.cfc.countyVO")]

	[Bindable]
	public class countyVO
	{

		public var countyid:Number = 0;
		public var name:String = "";
		public var stateid:Number = 0;
		public var statefips:String = "";
		public var countyfips:String = "";
		public var fullfips:String = "";


		public function countyVO()
		{
		}

	}
}