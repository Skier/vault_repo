package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.DocumentStatusEntity")]
public class DocumentStatus
{
	public static const COMPLETE_ID:int = 1;
	public static const DRAFT_ID:int = 2;

	public static const COMPLETE_NAME:String = "COMPLETE";
	public static const DRAFT_NAME:String = "DRAFT";
	
    public var id:int;      
    public var name:String;
    
    public function populate(value:DocumentStatus):void 
    {
    	this.id = value.id;
    	this.name = value.name;
    }

    public function createCopy():DocumentStatus 
    {
    	var result:DocumentStatus = new DocumentStatus();
    	
    	result.name = this.name;
    	
    	return result;
    }
}
}
