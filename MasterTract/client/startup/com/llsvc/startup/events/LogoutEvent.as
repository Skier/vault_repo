package com.llsvc.startup.events
{
    import flash.events.Event;
    
    public class LogoutEvent extends Event
    {
        public static const LOGOUT_EVENT:String = "logoutEvent";
        
        public function LogoutEvent(type:String,
            bubbles:Boolean=true, cancelable:Boolean=false)
        {
            super(type, bubbles, cancelable);
        }
     
        override public function clone():Event 
        {
            return new LogoutEvent(type, bubbles, cancelable);
        }
       
    }

}