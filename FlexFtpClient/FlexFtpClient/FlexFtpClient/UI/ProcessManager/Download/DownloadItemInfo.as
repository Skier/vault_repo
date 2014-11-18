package UI.ProcessManager.Download
{
	import Domain.Common.FtpFile;
	import flash.net.FileReference;
	import UI.ProcessManager.ProcessStatus;
	import flash.events.IOErrorEvent;
	import flash.events.ProgressEvent;
	import Domain.Common.FtpConnectionInfo;
	import mx.formatters.NumberFormatter;
	import mx.utils.ObjectUtil;
	import mx.states.State;
	import flash.events.Event;
	import flash.net.URLRequest;
	
	[Bindable]
	public class DownloadItemInfo
	{

		public var File:FtpFile;
		public var FileUrl:String;
		public var ConnectionInfo:FtpConnectionInfo;
		public var Status:ProcessStatus;
		public var ErrorMessage:String = null;
	
		private var fileRef:FileReference;
		private	var numberFormatter:NumberFormatter = new NumberFormatter();

		public function DownloadItemInfo(file:FtpFile, connectionInfo:FtpConnectionInfo):void
		{
			ConnectionInfo = ObjectUtil.copy(connectionInfo) as FtpConnectionInfo;
			Status = new ProcessStatus();
			File = file.SimpleClone();

			fileRef = new FileReference;
			fileRef.addEventListener(Event.OPEN, OnBeginLoad);			
			fileRef.addEventListener(Event.COMPLETE, OnCompleteLoad);
	        fileRef.addEventListener(IOErrorEvent.IO_ERROR, OnErrorLoad);
	        fileRef.addEventListener(ProgressEvent.PROGRESS, OnProgressLoad);
            
			numberFormatter.precision = 2;
			numberFormatter.useThousandsSeparator = true;
		}		
		
		public function get bytesLoaded():int
		{
			return Status.ProcessedBytes;
		} 
		
		public function get bytesTotal():int 
		{
			return Status.TotalBytes;
		}
		
		public function StartLoad():void
		{
            var urlRequest:URLRequest = new URLRequest;
            urlRequest.url = FileUrl;
			fileRef.download(urlRequest);
		}

		private function OnBeginLoad(event:Event):void
		{
			Status.State = ProcessStatus.LOADING;
			Status.ProcessedBytes = 0;
		}
		
		private function OnCompleteLoad(event:Event):void
		{
			dispatchEvent(new DownloadEvent(DownloadEvent.COMPLETE_LOAD_PROCESS, this));
			Status.State = ProcessStatus.LOAD_COMPLETED;
		}
		
		private function OnErrorLoad(event:IOErrorEvent):void
		{
			dispatchEvent(new DownloadEvent(DownloadEvent.ERROR_LOAD_PROCESS, this));
			Status.State = ProcessStatus.ERROR;
		}
		
		private function OnProgressLoad(event:ProgressEvent):void 
		{
			Status.ProcessedBytes = event.bytesLoaded;
			Status.TotalBytes = event.bytesTotal;
			Status.RebuildDescription();
		}
	}
}