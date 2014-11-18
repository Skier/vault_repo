package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.DocumentRecordEntity")]
public class DocumentRecord
{
    public var id:int;      
    public var document:Document;
    public var state:State;
    public var county:County;
    public var docNo:String;
    public var docDate:Date;
    public var volume:String;
    public var page:String;
    public var isPublic:Boolean;
    public var attachment:DocumentAttachment;
    
    public function DocumentRecord()
    {
        this.document = new Document();
    }
    
    public function populate(value:DocumentRecord):void 
    {
        this.id = value.id;
        this.state = value.state;
        this.county = value.county;
        this.docNo = value.docNo;
        this.docDate = value.docDate;
        this.volume = value.volume;
        this.page = value.page;
        this.isPublic = value.isPublic;
        
        if (value.attachment != null) {
	        var attach:DocumentAttachment = new DocumentAttachment();
	        attach.populate(value.attachment);
	        this.attachment = attach;
        } else {
        	this.attachment = null;
        }
    }
    
    public function getInfoStr():String 
    {
        var result:String = "";

        result += (isPublic ? "Memo " : "OGML");
        result += ("(" + county.name + ") ");
        result += (docNo + ":" + volume + "/" + page);
        result += "; ";
            
        return result;
    }
}
}
