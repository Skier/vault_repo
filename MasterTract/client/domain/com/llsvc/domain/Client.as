package com.llsvc.domain
{
import com.llsvc.domain.memento.ClientMemento;
import com.llsvc.domain.memento.IMemento;

import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.ClientEntity")]
public class Client implements IMemento
{
	public var id:int;
	public var name:String; 
	public var abbreviation:String;
	public var isActive:Boolean;
	
	public var users:ArrayCollection;
	
	public var isLoading:Boolean;
	
	public function Client()
	{
		users = new ArrayCollection();
		memento = new ClientMemento();
	}
	
	private var memento:ClientMemento;
	public function setMemento():void 
	{
		memento.id = this.id;
		memento.isActive = this.isActive;
		memento.name = this.name;
		memento.abbreviation = this.abbreviation;
	}
	
	public function getMemento():void 
	{
		this.id = memento.id;
		this.isActive = memento.isActive;
		this.name = memento.name;
		this.abbreviation = memento.abbreviation;
	}
	
	public function populate(value:Client):void 
	{
		this.id = value.id;
		this.name = value.name;
		this.abbreviation = value.abbreviation;
		this.isActive = value.isActive;
	}
	
}
}