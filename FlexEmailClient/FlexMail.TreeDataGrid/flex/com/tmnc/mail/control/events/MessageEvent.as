package com.tmnc.mail.control.events
{
	import flash.events.Event;
	import com.tmnc.mail.business.messages.MessageInfo;
	
	public class MessageEvent extends Event {

        public static const EVENT_SEND:String    = "send_message";
        public static const EVENT_REPLY:String   = "reply_message";
        public static const EVENT_FORWARD:String = "forward_message";
        public static const EVENT_COMPOSE:String = "compose_message";
                                        		
		public var message:MessageInfo;

        public function MessageEvent(type:String, message:MessageInfo, bubbles:Boolean=true,cancelable:Boolean=false):void {
            super(type, bubbles, cancelable);
			this.message = message;
        }

	    override public function clone():Event {
	        return new MessageEvent(type, message, bubbles, cancelable);
	    }

	}
}