/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package Domain
{
	import flash.events.Event;
	import Domain.Common.FtpConnectionInfo;
	
	public class AddConnectionEvent extends Event {

        public static const EVENT_ADD_CONNECTION:String = "add_connection";
        		
		public var connectionInfo:FtpConnectionInfo = null;

        public function AddConnectionEvent(connectionInfo:FtpConnectionInfo,
        						   bubbles:Boolean=true,cancelable:Boolean=false):void {
            super(EVENT_ADD_CONNECTION, bubbles, cancelable);
			this.connectionInfo = connectionInfo;
        }
		
	    override public function clone():Event {
	        return new AddConnectionEvent(connectionInfo, bubbles, cancelable);
	    }

	}
}