package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.ModuleEntityType")]
public class ModuleType
{
    public var id:int;      
    public var name:String;
    
}
}
