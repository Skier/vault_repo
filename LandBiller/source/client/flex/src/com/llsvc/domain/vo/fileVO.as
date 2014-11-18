package com.llsvc.domain.vo
{
	[RemoteClass(alias="com.llsvc.domain.cfc.fileVO")]

	[Bindable]
	public class fileVO
	{

		public var fileid:Number = 0;
		public var origfilename:String = "";
		public var storagekey:String = "";
		public var userid:Number = 0;
		public var note:String = "";


		public function fileVO()
		{
		}

	}
}