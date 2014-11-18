package com.llsvc.domain.vo
{
	[RemoteClass(alias="com.llsvc.domain.cfc.expencetypeVO")]

	[Bindable]
	public class expencetypeVO
	{

		public var expencetypeid:Number = 0;
		public var basedon:Number = 0;
		public var userid:Number = 0;
		public var itemname:String = "";
		public var defaultrate:Number = 0;


		public function expencetypeVO()
		{
		}

	}
}