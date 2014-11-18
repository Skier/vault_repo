package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.ModuleEntity")]
public class Module
{
    public var id:int;      
    public var name:String;
    public var description:String;
    public var url:String;
/*
    public var moduleType:ModuleType;
*/
    public var moduleTypeId:int;
    
    private var _permissions:Array;
    public function get PermissionList():Array { return _permissions; }
    public function set PermissionList(value:Array):void 
    { 
        _permissions = value;
    }
}
}
