package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.DocumentActorPhoneEntity")]
public class DocumentActorPhone
{
    public var id:int;
    public var actor:DocumentActor;
    public var phone:String;
    public var isPrimary:Boolean;
    
    public function populate(value:DocumentActorPhone):void 
    {
    	this.id = value.id;
    	this.phone = value.phone;
    	this.isPrimary = value.isPrimary;
    }

    public function createCopy():DocumentActorPhone 
    {
    	var result:DocumentActorPhone = new DocumentActorPhone();

    	result.phone = this.phone;
    	result.isPrimary = this.isPrimary;
    	
    	return result;
    }
}
}
