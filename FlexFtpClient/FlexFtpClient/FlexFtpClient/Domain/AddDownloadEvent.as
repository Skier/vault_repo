/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package Domain
{
	import flash.events.Event;
	import Domain.Common.FtpConnectionInfo;
	import Domain.Common.FtpFile;
	
	public class AddDownloadEvent extends Event {

        public static const EVENT_ADD_DOWNLOAD:String = "addDownloadProcess";
        		
		public var ftpFile:FtpFile = null;
		public var connectionInfo:FtpConnectionInfo = null;

        public function AddDownloadEvent(ftpFile:FtpFile, connectionInfo:FtpConnectionInfo,
        						   bubbles:Boolean=true,cancelable:Boolean=false):void {
            super(EVENT_ADD_DOWNLOAD, bubbles, cancelable);
			this.ftpFile = ftpFile;
			this.connectionInfo = connectionInfo;
        }
		
	    override public function clone():Event {
	        return new AddDownloadEvent(ftpFile, connectionInfo, bubbles, cancelable);
	    }

	}
}