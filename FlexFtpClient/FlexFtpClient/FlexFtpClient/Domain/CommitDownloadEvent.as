/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package Domain
{
	import flash.events.Event;
	import Domain.Common.FtpConnectionInfo;
	
	public class CommitDownloadEvent extends Event {

        public static const EVENT_COMMIT_DOWNLOAD_PROCESS:String = "commitDownloadProcess";
        		
		public var connectionInfo:FtpConnectionInfo = null;

        public function CommitDownloadEvent(connectionInfo:FtpConnectionInfo,
        						   bubbles:Boolean=true,cancelable:Boolean=false):void {
            super(EVENT_COMMIT_DOWNLOAD_PROCESS, bubbles, cancelable);
			this.connectionInfo = connectionInfo;
        }
		
	    override public function clone():Event {
	        return new CommitDownloadEvent(connectionInfo, bubbles, cancelable);
	    }

	}
}