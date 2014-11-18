package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.AddressEntity")]
public class Address
{
    public var id:int;      
    public var state:State;
    public var address1:String;
    public var address2:String;
    public var city:String;
    public var zip:String;
    
    public function createCopy():Address 
    {
    	var result:Address = new Address();
    	
    	result.state = this.state;
    	result.address1 = this.address1;
    	result.address2 = this.address2;
    	result.city = this.city;
    	result.zip = this.zip;
    	
    	return result;
    }
}
}
