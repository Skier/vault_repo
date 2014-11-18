package com.llsvc.framework.util
{

import mx.rpc.events.FaultEvent;

public class ExceptionHelper
{
    public static function parseFault(faultEvent:FaultEvent):String {
        return ExceptionHelper.parseFaultString(faultEvent.fault.faultString);        
    }

    public static function parseFaultString(faultString:String):String {
        var index:int = faultString.indexOf(":");
        if ( -1 != index ) {
            return faultString.substr(index+1);
        } else {
            return faultString;
        }
    }
}
}
