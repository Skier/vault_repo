package com.llsvc.domain
{

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.LayerEntity")]
public class Layer
{
    public var id:int;
    public var name:String; 
    public var description:String;
    public var isActive:Boolean;
    public var isPublic:Boolean;
}
}