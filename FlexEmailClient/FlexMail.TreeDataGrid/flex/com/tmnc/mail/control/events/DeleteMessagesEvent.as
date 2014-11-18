package com.tmnc.mail.control.events
{
	import flash.events.Event;
	
	public class DeleteMessagesEvent extends Event {

        public static const EVENT_DELETE_MESSAGES:String = "delete_messages";
        		
		public var messageList:Array;
		
        public function DeleteMessagesEvent(messageList:Array, bubbles:Boolean=true,cancelable:Boolean=false):void {
            super(EVENT_DELETE_MESSAGES,bubbles,cancelable);
			this.messageList = messageList;
        }
		
	    override public function clone():Event {
	        return new DeleteMessagesEvent(messageList, bubbles, cancelable);
	    }

	}
}