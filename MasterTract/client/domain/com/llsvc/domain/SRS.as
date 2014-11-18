package com.llsvc.domain
{

[Bindable]
[RemoteClass(alias="com.llsvc.server.geo.SRS")]
public class SRS
{
    public var srid:String;
    public var name:String; 
}
}