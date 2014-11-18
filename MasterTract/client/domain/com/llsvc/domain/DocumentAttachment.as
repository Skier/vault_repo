package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.DocumentAttachmentEntity")]
public class DocumentAttachment
{
    public static const UNRECORDED_TYPE:String = "Unrecorded";
    public static const RECORDED_TYPE:String = "Recorded";
    public static const CORRESPONDENCE_TYPE:String = "Correspondence";
    public static const OTHER_TYPE:String = "Other";

    public var id:int;      
    public var document:Document;
    public var file:File;
    public var type:String;
    public var name:String;
    public var memo:String;
    public var memoDate:Date;
    public var correspondenceDate:Date;
    public var from:String;
    public var to:String;
    public var record:DocumentRecord;
    
    public function populate(value:DocumentAttachment):void 
    {
        this.id = value.id;
        this.file = value.file;
        this.record = value.record;
        this.type = value.type;
        this.name = value.name;
        this.memo = value.memo;
        this.memoDate = value.memoDate;
        this.correspondenceDate = value.correspondenceDate;
        this.from = value.from;
        this.to = value.to;
    } 
    
    public function get date():String 
    {
    	if (this.type == RECORDED_TYPE && this.record != null) 
    	{
    		return this.record.docDate.toDateString();
    	}
    	
    	if (this.type == CORRESPONDENCE_TYPE && this.correspondenceDate != null) 
    	{
    		return this.correspondenceDate.toDateString();
    	}

    	if (this.memoDate != null) 
    	{
    		return this.memoDate.toDateString();
    	}
    	
    	return "n/a";
    }
}
}
