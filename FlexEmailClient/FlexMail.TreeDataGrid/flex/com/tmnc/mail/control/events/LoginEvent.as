package com.tmnc.mail.control.events
{
	import flash.events.Event;
	
	public class LoginEvent extends Event {

        public static const EVENT_LOGIN:String = "login";
        		
		public var email:String = "";
		public var password:String = "";

        /**
         *  Constructor.
         * 
         *  @param email The user email to log in or out (!! an empty email causes a logout !!)
         *  @param password The user SMTP/POP3 server password.
		**/
        public function LoginEvent(email:String, password:String, 
        						   bubbles:Boolean=true,cancelable:Boolean=false):void {
            super(EVENT_LOGIN,bubbles,cancelable);
			this.email = email;
			this.password = password;
        }
		
	    override public function clone():Event {
	        return new LoginEvent(email, password, bubbles, cancelable);
	    }

	}
}