package com.llsvc.module
{
//import TractInc.Domain.User;
//import com.llsvcTractInc.Domain.Module;

public interface IModule
{
    function init(user:Object, module:Object):void;
    function modeChanged(mode:String):void;
    function logout():Boolean;
}
}
