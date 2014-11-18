/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package Domain
{
	import flash.events.Event;
	import Domain.Common.FtpConnectionInfo;
	import flash.net.FileReference;
	
	public class AddUploadEvent extends Event {

        public static const EVENT_ADD_UPLOAD:String = "addUploadProcess";
        		
		public var fileRef:FileReference = null;
		public var connectionInfo:FtpConnectionInfo = null;

        public function AddUploadEvent(fileRef:FileReference, connectionInfo:FtpConnectionInfo, 
        						   bubbles:Boolean=true,cancelable:Boolean=false):void {
            super(EVENT_ADD_UPLOAD, bubbles, cancelable);
			this.fileRef = fileRef;
			this.connectionInfo = connectionInfo;
        }
		
	    override public function clone():Event {
	        return new AddUploadEvent(fileRef, connectionInfo, bubbles, cancelable);
	    }

	}
}