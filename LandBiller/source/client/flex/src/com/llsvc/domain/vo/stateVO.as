package com.llsvc.domain.vo
{
	[RemoteClass(alias="com.llsvc.domain.cfc.stateVO")]

	[Bindable]
	public class stateVO
	{

		public var stateid:Number = 0;
		public var name:String = "";
		public var statefips:String = "";
		public var stateabbr:String = "";


		public function stateVO()
		{
		}

	}
}