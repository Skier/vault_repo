package AerSysCo.UI.Models
{
	import AerSysCo.Server.Address;
	
	[Bindable]
	public class AddressUI
	{
	    public var name:String;
	    public var address1:String;
	    public var address2:String;
	    public var city:String;
	    public var state:String;
	    public var zip:String;
	    public var country:String;
	    
	    public function populateFromAddress(value:Address, cascade:Boolean = true):void 
	    {
	    	this.name = value.name;
	    	this.address1 = value.address1;
	    	this.address2 = value.address2;
	    	this.city = value.city;
	    	this.state = value.state;
	    	this.zip = value.zip;
	    	this.country = value.country;
	    }
	    
	    public function toAddress():Address 
	    {
	    	var result:Address = new Address();
	    	
	    	result.name = this.name;
	    	result.address1 = this.address1;
	    	result.address2 = this.address2;
	    	result.city = this.city;
	    	result.state = this.state;
	    	result.zip = this.zip;
	    	result.country = this.country;
	    	
	    	return result;
	    }
	    
	}
}