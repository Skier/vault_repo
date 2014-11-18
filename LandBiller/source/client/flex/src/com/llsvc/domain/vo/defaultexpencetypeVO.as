package com.llsvc.domain.vo
{
	[RemoteClass(alias="com.llsvc.domain.cfc.defaultexpencetypeVO")]

	[Bindable]
	public class defaultexpencetypeVO
	{

		public var defaultexpencetypeid:Number = 0;
		public var itemname:String = "";
		public var defaultrate:Number = 0;


		public function defaultexpencetypeVO()
		{
		}

	}
}