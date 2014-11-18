/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package Domain
{
	import flash.events.Event;
	import UI.ConnectionManager.FtpConnection.FtpConnectionView;
	
	public class DisconnectEvent extends Event {

        public static const EVENT_DISCONNECT:String = "disconnect";
        		
		public var ftpView:FtpConnectionView;

        public function DisconnectEvent(ftpView:FtpConnectionView , 
        						   bubbles:Boolean=true,cancelable:Boolean=false):void {
            super(EVENT_DISCONNECT, bubbles, cancelable);
			this.ftpView = ftpView;
        }
		
	    override public function clone():Event {
	        return new DisconnectEvent(ftpView, bubbles, cancelable);
	    }

	}

}