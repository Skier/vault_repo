package com.llsvc.domain.memento
{

import com.llsvc.domain.Client;

[Bindable]
public class UserMemento
{
    public var id:int;      
    public var login:String;
    public var password:String;
    public var isActive:Boolean;
    public var hackAttempts:int;
	public var isAdmin:Boolean;    
	public var isProjectManager:Boolean;
	
	public var client:Client;
	
    public var firstName:String;
    public var middleName:String;
    public var lastName:String;
    public var primaryPhoneNumber:String;
    public var secondaryPhoneNumber:String;
    public var email:String;
    public var ssn:String;
    public var birthDay:Date;
}
}