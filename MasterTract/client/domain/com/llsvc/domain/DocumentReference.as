package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.DocumentReferenceEntity")]
public class DocumentReference
{
    
    public var id:int;      
    public var referrer:Document;
    public var refereeId:int;    
    
    public var refereeDoc:Object;
    
    public function get name():String 
    {
    	if (refereeDoc != null) 
    	{
    		return refereeDoc.documentType.name;
    	} else 
    	{
    		return "N/A";
    	}
    }  

    public function get date():String 
    {
    	if (refereeDoc != null) 
    	{
    		return "N/A";
    	} else 
    	{
    		return "N/A";
    	}
    }  

}
}
