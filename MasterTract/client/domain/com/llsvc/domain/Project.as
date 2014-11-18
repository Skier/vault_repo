package com.llsvc.domain
{

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.ProjectEntity")]
public class Project
{
	public var id:int;
	public var name:String; 
	public var isActive:Boolean;
	
	public var client:Client;
	
	public function populate(value:Project):void 
	{
		this.id = value.id;
		this.name = value.name;
		this.isActive = value.isActive;
		
		this.client = value.client;
	}

}
}