package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.DocumentTypeEntity")]
public class DocumentType
{
	public static const DOC_TYPE_LEASE:String = "Lease";
	public static const DOC_TYPE_RECORD:String = "Record";
	public static const DOC_TYPE_LEASE_MEMO:String = "Lease Memo";
	public static const DOC_TYPE_ASSIGNMENT:String = "Assignment";
	public static const DOC_TYPE_CORRESPONDENCE:String = "Correspondence";
	public static const DOC_TYPE_AGREEMENT:String = "Agreement";
	
    public var id:int;      
    public var name:String;
    public var giverRole:String;
    public var receiverRole:String;
    
    public function populate(value:DocumentType):void 
    {
    	this.id = value.id;
    	this.name = value.name;
    	this.giverRole = value.giverRole;
    	this.receiverRole =value.receiverRole;
    }

    public function createCopy():DocumentType 
    {
    	var result:DocumentType = new DocumentType();
    	
    	result.name = this.name;
    	result.giverRole = this.giverRole;
    	result.receiverRole =this.receiverRole;
    	
    	return result;
    }
}
}
