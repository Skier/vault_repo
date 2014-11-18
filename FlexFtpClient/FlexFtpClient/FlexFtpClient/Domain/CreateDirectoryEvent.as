/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package Domain
{
	import flash.events.Event;
	import UI.ConnectionManager.FtpConnection.FtpConnectionView;
	
	public class CreateDirectoryEvent extends Event {

        public static const EVENT_CREATE_DIRECTORY:String = "create_new_directory";
        		
		public var newDirectoryName:String = null;

        public function CreateDirectoryEvent(newDirectoryName:String,
        						   bubbles:Boolean=true,cancelable:Boolean=false):void {
            super(EVENT_CREATE_DIRECTORY, bubbles, cancelable);
			this.newDirectoryName = newDirectoryName;
        }
		
	    override public function clone():Event {
	        return new CreateDirectoryEvent(newDirectoryName, bubbles, cancelable);
	    }

	}
}