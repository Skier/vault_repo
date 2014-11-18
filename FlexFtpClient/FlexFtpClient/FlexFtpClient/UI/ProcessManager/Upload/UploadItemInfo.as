package UI.ProcessManager.Upload
{
	import flash.net.FileReference;
	import Domain.Common.FtpConnectionInfo;
	import UI.ProcessManager.ProcessStatus;
	import mx.formatters.NumberFormatter;
	import flash.events.IOErrorEvent;
	import flash.events.ProgressEvent;
	import mx.utils.ObjectUtil;
	import Domain.Common.FtpFile;
	import flash.events.Event;
	import mx.states.State;
	import flash.net.URLRequest;
	
	[Bindable]
	public class UploadItemInfo
	{
		public var File:FtpFile;
		public var ConnectionInfo:FtpConnectionInfo;
		public var Status:ProcessStatus;
		public var ProgressLabel:String = "";
		public var PctComplete:String = "";
		public var ErrorMessage:String = null;
	
		private var fileRef:FileReference;
		private	var numberFormatter:NumberFormatter = new NumberFormatter();

		public function UploadItemInfo(fileRef:FileReference, connectionInfo:FtpConnectionInfo):void
		{
			ConnectionInfo = ObjectUtil.copy(connectionInfo) as FtpConnectionInfo;
			Status = new ProcessStatus;
			Status.ProcessId = UIDUtil.createUID();

			File = new FtpFile();

			this.fileRef = fileRef;
			this.fileRef.addEventListener(Event.COMPLETE, OnCompleteLoad);
            this.fileRef.addEventListener(IOErrorEvent.IO_ERROR, OnErrorLoad);
            this.fileRef.addEventListener(ProgressEvent.PROGRESS, OnProgressLoad);
            File.Name = fileRef.name;
            File.Size = fileRef.size;
            
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

		public function StartLoad(requestURL:URLRequest):void 
		{
			fileRef.upload(requestURL);
		}

		private function OnCompleteLoad(event:Event):void
		{
			dispatchEvent(new UploadEvent(UploadEvent.COMPLETE_LOAD_PROCESS, this));
			Status.State = ProcessStatus.LOAD_COMPLETED;
		}
		
		private function OnErrorLoad(event:IOErrorEvent):void
		{
			dispatchEvent(new UploadEvent(UploadEvent.ERROR_LOAD_PROCESS, this));
			Status.State = ProcessStatus.LOADING_ERROR;
		}
		
		private function OnProgressLoad(event:ProgressEvent):void 
		{
			Status.ProcessedBytes = event.bytesLoaded;
			Status.TotalBytes = event.bytesTotal;
			Status.RebuildDescription();
		}
	}
}