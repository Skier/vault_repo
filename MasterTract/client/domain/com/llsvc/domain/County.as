package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.CountyEntity")]
public class County
{
    public var id:int;      
    public var name:String;
    public var cfips:String;
    public var fips:String;
    public var state:State;
}
}
