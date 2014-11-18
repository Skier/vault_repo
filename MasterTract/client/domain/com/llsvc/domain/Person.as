package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.PersonEntity")]
public class Person
{
    public var id:int;      
//    public var ClientId:int;      
//    public var CompanyId:int;      
    public var firstName:String;
    public var middleName:String;
    public var lastName:String;
    public var primaryPhoneNumber:String;
    public var secondaryPhoneNumber:String;
    public var email:String;
    public var ssn:String;
    public var birthDay:Date;
    
    public function populate(value:Person):void 
    {
    	this.id = value.id;
    	this.firstName = value.firstName;
    	this.middleName = value.middleName;
    	this.lastName = value.lastName;
    	this.primaryPhoneNumber = value.primaryPhoneNumber;
    	this.secondaryPhoneNumber = value.secondaryPhoneNumber;
    	this.email = value.email;
    	this.ssn = value.ssn;
    	this.birthDay = value.birthDay;
    }
}
}