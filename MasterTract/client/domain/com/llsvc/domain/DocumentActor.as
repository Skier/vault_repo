package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.DocumentActorEntity")]
public class DocumentActor
{
    public var id:int;      
    public var name:String;
    public var address:Address;
    public var isGiver:Boolean;
    public var isCompany:Boolean;
    public var taxId:String;

    public var phones:ArrayCollection;
    
    public function DocumentActor()
    {
        this.phones = new ArrayCollection();
        this.taxId = "";
    }
    
    public function get phonesStr():String 
    {
    	var result:String = "";
    	
    	for each (var p:DocumentActorPhone in phones) 
    	{
    		result += p.phone;
    		result += ";"
    	}
    	
    	return result;
    }
    
    public function get addressStr():String 
    {
    	var result:String = "";
    	
    	result += (address.address1 + " " + address.address2);
    	result += (address.city + ", " + address.state.abbr + " " + address.zip);
    	
    	return result;
    }
    
    public function get infoStr():String 
    {
    	return (name + " " + addressStr + " " + phonesStr);
    }
    
    public function populate(value:DocumentActor):void 
    {
        this.id = value.id;
        this.name = value.name;
        this.address = value.address;
        this.isGiver = value.isGiver;
        this.isCompany = value.isCompany;
        this.taxId = value.taxId;
        
        phones.removeAll();
        for each (var phone:DocumentActorPhone in value.phones) 
        {
            var p:DocumentActorPhone = new DocumentActorPhone();
            p.actor = this;
            p.populate(phone);
            this.phones.addItem(p);
        }
    }

    public function createCopy():DocumentActor 
    {
    	var result:DocumentActor = new DocumentActor();
    	
        result.name = this.name;
        result.address = this.address.createCopy();
        result.isGiver = this.isGiver;
        result.isCompany = this.isCompany;
        result.taxId = this.taxId;
        
        for each (var phone:DocumentActorPhone in this.phones) 
        {
            var p:DocumentActorPhone = phone.createCopy();
            p.actor = result;
            result.phones.addItem(p);
        }
        
        return result;
    }
}
}
