package com.llsvc.module
{
import flash.events.Event;

public class ModuleEvent extends Event
{
    public var module:Object;

    public static const MODULE_EXCEPTION:String = "ModuleException";
    
    public function ModuleEvent(type:String, module:Object,
            bubbles:Boolean=true, cancelable:Boolean=false)
    {
        this.module = module;
        super(type, bubbles, cancelable);
    }
 
    override public function clone():Event 
    {
        return new ModuleEvent(type, module, bubbles, cancelable);
    }
   
}

}
