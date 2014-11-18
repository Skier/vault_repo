package com.llsvc.startup.events
{

import flash.events.Event;
import mx.rpc.Fault;

public class LoginFaultEvent extends Event
{
    public static const LOGIN_FAULT:String = "loginFailed";
    
    public var fault:Fault;
    
    public function LoginFaultEvent(type:String, 
        fault:Fault,
        bubbles:Boolean=true, cancelable:Boolean=false)
    {
        this.fault = fault;
        super(type, bubbles, cancelable);
    }
 
    override public function clone():Event 
    {
        return new LoginFaultEvent(type, fault, bubbles, cancelable);
    }
   
}

}