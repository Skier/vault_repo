package com.llsvc.domain
{
import com.llsvc.domain.memento.IMemento;
import com.llsvc.domain.memento.UserMemento;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.UserEntity")]
public class User implements IMemento
{
    public var id:int;      
    public var login:String;
    public var password:String;
    public var isActive:Boolean;
    public var hackAttempts:int;

	public var isAdmin:Boolean;
	public var isProjectManager:Boolean;
	    
    public var client:Client;
    
    public var isLoading:Boolean;
    
    private var memento:UserMemento;
    
    public function User() 
    {
    	personal = new Person();
    	memento = new UserMemento();
    }
    
    public function get clientName():String 
    {
    	if (client != null) 
    	{
    		return client.name;
    	} else {
    		return "n/a";
    	}
    }

    public var personal:Person;
//    public var Preference:UserPreference;

    private var _roleList:Array;
    public function get RoleList():Array { return _roleList; }
    public function set RoleList(value:Array):void { 
        _roleList = value;
    }
    
	public function get firstName():String {
		if (this.personal != null)
			return this.personal.firstName;
		else 
			return "";
	}
	public function set firstName(value:String):void{
		if (this.personal != null)
			this.personal.firstName = value;
	}
	
	public function get middleName():String {
		if (this.personal != null)
			return this.personal.middleName;
		else 
			return "";
	}
	public function set middleName(value:String):void{
		if (this.personal != null)
			this.personal.middleName = value;
	}
	
	public function get lastName():String {
		if (this.personal != null)
			return this.personal.lastName;
		else 
			return "";
	}
	public function set lastName(value:String):void{
		if (this.personal != null)
			this.personal.lastName = value;
	}
	
	public function get primaryPhoneNumber():String {
		if (this.personal != null)
			return this.personal.primaryPhoneNumber;
		else 
			return "";
	}
	public function set primaryPhoneNumber(value:String):void{
		if (this.personal != null)
			this.personal.primaryPhoneNumber = value;
	}
	
	public function get secondaryPhoneNumber():String {
		if (this.personal != null)
			return this.personal.secondaryPhoneNumber;
		else 
			return "";
	}
	public function set secondaryPhoneNumber(value:String):void{
		if (this.personal != null)
			this.personal.secondaryPhoneNumber = value;
	}
	
	public function get email():String {
		if (this.personal != null)
			return this.personal.email;
		else 
			return "";
	}
	public function set email(value:String):void{
		if (this.personal != null)
			this.personal.email = value;
	}
	
	public function get ssn():String {
		if (this.personal != null)
			return this.personal.ssn;
		else 
			return "";
	}
	public function set ssn(value:String):void{
		if (this.personal != null)
			this.personal.ssn = value;
	}
	
	public function get birthDay():Date {
		if (this.personal != null)
			return this.personal.birthDay;
		else 
			return null;
	}
	public function set birthDay(value:Date):void{
		if (this.personal != null)
			this.personal.birthDay = value;
	}
	
	public function setMemento():void 
	{
		memento.id = this.id;
    	memento.login = this.login;
    	memento.password = this.password;
    	memento.isActive = this.isActive;
    	memento.hackAttempts = this.hackAttempts;
		memento.isAdmin = this.isAdmin;    
		memento.isProjectManager = this.isProjectManager;
	
		memento.client = this.client;
	
    	memento.firstName = this.personal.firstName;
    	memento.middleName = this.personal.middleName;
    	memento.lastName = this.personal.lastName;
    	memento.primaryPhoneNumber = this.personal.primaryPhoneNumber;
    	memento.secondaryPhoneNumber = this.personal.secondaryPhoneNumber;
    	memento.email = this.personal.email;
    	memento.ssn = this.personal.ssn;
    	if (this.personal.birthDay != null)	{
	    	memento.birthDay = new Date(this.personal.birthDay.time);
    	}
	}

	public function getMemento():void 
	{
		this.id = memento.id;
    	this.login = memento.login;
    	this.password = memento.password;
    	this.isActive = memento.isActive;
    	this.hackAttempts = memento.hackAttempts;
		this.isAdmin = memento.isAdmin;    
		this.isProjectManager = memento.isProjectManager;
		
		this.client = memento.client;
	
    	this.personal.firstName = memento.firstName;
    	this.personal.middleName = memento.middleName;
    	this.personal.lastName = memento.lastName;
    	this.personal.primaryPhoneNumber = memento.primaryPhoneNumber;
    	this.personal.secondaryPhoneNumber = memento.secondaryPhoneNumber;
    	this.personal.email = memento.email;
    	this.personal.ssn = memento.ssn;
    	this.personal.birthDay = memento.birthDay;
	}
	
    public function populate(value:User):void 
    {
    	this.id = value.id;
    	this.login = value.login;
    	this.password = value.password;
    	this.isActive = value.isActive;
    	this.hackAttempts = value.hackAttempts;
    	this.isAdmin = value.isAdmin;
    	this.isProjectManager = value.isProjectManager;
    	
    	if (value.personal != null) 
    	{
    		this.personal.populate(value.personal);
    	} else 
    	{
    		this.personal = null;
    	}
    }
}
}
