package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.TractEntity")]
public class Tract
{
    public var id:int;
    public var state:State;
    public var township:String;
    public var townshipDir:String;
    public var range:String;
    public var rangeDir:String;
    public var meridian:String;
    public var section:String;
    public var qq:String;
    public var lot:String;
    public var externalId:int;
}
}
