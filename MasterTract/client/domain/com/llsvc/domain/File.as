package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.FileEntity")]
public class File
{
    public var id:int;      
    public var user:User;
    public var origFilename:String;
    public var mimeType:String;
    public var storageKey:String;
    public var changed:Date;
    public var note:String;
}
}
