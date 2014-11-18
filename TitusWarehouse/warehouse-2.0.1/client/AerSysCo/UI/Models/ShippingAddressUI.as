package AerSysCo.UI.Models
{
	import AerSysCo.Server.ShippingAddress;
	
	[Bindable]
	public class ShippingAddressUI
	{
	    public var addressId:int;
	    public var customerId:int;
	    public var name:String;
	    public var address1:String;
	    public var address2:String;
	    public var city:String;
	    public var state:String;
	    public var zip:String;
	    public var country:String;

	    public function populateFromShippingAddress(value:ShippingAddress, cascade:Boolean = false):void 
	    {
	    	this.addressId = value.addressId;
	    	this.customerId = value.customerId;
	    	this.name = value.name;
	    	this.address1 = value.address1;
	    	this.address2 = value.address2;
	    	this.city = value.city;
	    	this.state = value.state;
	    	this.zip = value.zip;
	    	this.country = value.country;
	    }
	    
	    public function toShippingAddress():ShippingAddress 
	    {
	    	var result:ShippingAddress = new ShippingAddress();
	    	
	    	result.addressId = this.addressId;
	    	result.customerId = this.customerId;
	    	result.name = this.name;
	    	result.address1 = this.address1;
	    	result.address2 = this.address2;
	    	result.city = this.city;
	    	result.state = this.state;
	    	result.zip = this.zip;
	    	result.country = this.country;
	    	result.dateCreated = new Date();
	    	result.lastUpdateDate = new Date();
	    	
	    	return result;
	    }
	    
	    public function get addressLabel():String 
	    {
	    	return (name + " " + address1 + " " + address2); 
	    }
	    
	    public function isEqual(value:ShippingAddressUI):Boolean 
	    {
	    	if (!value)
	    		return false;
	    	
	    	if ( name == value.name
	    		&& country == value.country
	    		&& address1 == value.address1
	    		&& address2 == value.address2
	    		&& city == value.city
	    		&& state == value.state
	    		&& zip == value.zip		
	    		) 
	    	{
	    		return true;
	    	} else 
	    	{
	    		return false;
	    	}
	    }
	    
	}
}