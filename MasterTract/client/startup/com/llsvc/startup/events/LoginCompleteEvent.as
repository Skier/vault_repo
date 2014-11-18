package com.llsvc.startup.events
{

import flash.events.Event;
//    import TractInc.Domain.User;

public class LoginCompleteEvent extends Event
{
    public var user:Object;

    public static const LOGIN_COMPLETE:String = "loginComplete";
    
    public function LoginCompleteEvent(type:String, user:Object,
            bubbles:Boolean=true, cancelable:Boolean=false)
    {
        this.user = user;
        super(type, bubbles, cancelable);
    }
 
    override public function clone():Event 
    {
        return new LoginCompleteEvent(type, user, bubbles, cancelable);
    }
   
}

}