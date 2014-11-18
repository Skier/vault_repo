package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.StateEntity")]
public class State
{
    public var id:int;      
    public var name:String;
    public var fips:String;
    public var abbr:String;
    public var counties:ArrayCollection;
}
}
