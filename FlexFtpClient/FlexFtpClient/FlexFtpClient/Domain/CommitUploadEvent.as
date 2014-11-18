/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package Domain
{
	import flash.events.Event;
	import Domain.Common.FtpConnectionInfo;
	
	public class CommitUploadEvent extends Event {

        public static const EVENT_COMMIT_UPLOAD_PROCESS:String = "commitUploadProcess";
        		
		public var connectionInfo:FtpConnectionInfo = null;

        public function CommitUploadEvent(connectionInfo:FtpConnectionInfo,
        						   bubbles:Boolean=true,cancelable:Boolean=false):void {
            super(EVENT_COMMIT_UPLOAD_PROCESS, bubbles, cancelable);
			this.connectionInfo = connectionInfo;
        }
		
	    override public function clone():Event {
	        return new CommitUploadEvent(connectionInfo, bubbles, cancelable);
	    }

	}
}