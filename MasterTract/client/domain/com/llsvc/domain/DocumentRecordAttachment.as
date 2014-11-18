package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.DocumentRecordAttachmentEntity")]
public class DocumentRecordAttachment
{
	public static const RECORD_DOC_FILE:String = "RECORD_DOC_FILE";
	
    public var id:int;      
    public var record:DocumentRecord;
    public var file:File;
    public var description:String;
    public var note:String;

    public function get name():String 
    {
    	if (note && note.length > 0)
    		return note;
    	
    	if (this.file != null)
    		return file.origFilename
    	else 
    		return "n/a";
    }
    
    public function set name(value:String):void 
    {
    	this.note = value;
    }
}
}
