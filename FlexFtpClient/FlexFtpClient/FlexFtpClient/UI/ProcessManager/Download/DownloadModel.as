/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package UI.ProcessManager.Download
{
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class DownloadModel
	{
		public static const WAITING:String = "Waiting";
		public static const COMPRESSING:String = "Compressing";
		public static const FTP_IN_PROCESS:String = "FtpInProcess";
		public static const COMPLETE:String = "Complete";
		
		public static const WEBORB_SERVICE_NAME:String = "DownloadService";
		
		public var MaxFileSize:int = int.MAX_VALUE;
		public var MaxDirectorySize:int = int.MAX_VALUE;
		public var CompressionLevel:int = 5;
		
		public var Items:ArrayCollection;
		
		public function DownloadModel()
		{
			Items = new ArrayCollection();
		}
	}
	
}