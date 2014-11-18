package com.llsvc.startup
{
//import TractInc.Domain.User;
//import TractInc.Domain.storage.ITractStorage;

public class MainModel
{
    public static const VIEW_LOGIN:int = 0;
    public static const VIEW_DESKTOP:int = 1;
    
    public static var storage:Object;

    public var user:Object;
    
    public function get loggedIn():Boolean 
    {
        return user != null;
    }

    public function get loggedOut():Boolean 
    {
        return !loggedIn;
    }

}

}
