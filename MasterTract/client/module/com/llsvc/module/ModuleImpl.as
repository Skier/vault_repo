package com.llsvc.module
{
import mx.core.IFlexModuleFactory;
import mx.modules.Module;

public class ModuleImpl 
    extends Module 
    implements IModule
{
    public static const MODE_ONLINE:String = "online";
    public static const MODE_OFFLINE:String = "offline";
    
    public function init(user:Object, module:Object):void
    {
        throw new Error("not overriden yet!");
    }

    public function modeChanged(mode:String):void
    {
        // i'm not aware of mode changes
    }
    
    public function logout():Boolean
    {
        throw new Error("not overriden yet!");
    }

}
}
