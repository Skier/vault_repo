/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package UI.ProcessManager.Upload
{
	import flash.net.FileReference;
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class UploadModel
	{
		public static const WAITING:String = "Waiting";
		public static const IN_PROCESS:String = "FtpInProcess";
		public static const COMPLETE:String = "Complete";
		
		public static const WEBORB_SERVICE_NAME:String = "UploadService";
		
		public var Items:ArrayCollection;

		public var UploaderUrl:String;
	
		public function UploadModel()
		{
			Items = new ArrayCollection();
		}
	}
}