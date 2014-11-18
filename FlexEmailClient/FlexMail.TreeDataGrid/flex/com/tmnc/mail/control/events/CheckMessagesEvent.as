package com.tmnc.mail.control.events
{
	import flash.events.Event;
	
	public class CheckMessagesEvent extends Event {

        public static const EVENT_CHECK_MESSAGES:String = "check_messages";
        		
        public function CheckMessagesEvent(bubbles:Boolean=true,cancelable:Boolean=false):void {
            super(EVENT_CHECK_MESSAGES,bubbles,cancelable);
        }
		
	    override public function clone():Event {
	        return new CheckMessagesEvent(bubbles, cancelable);
	    }

	}
}