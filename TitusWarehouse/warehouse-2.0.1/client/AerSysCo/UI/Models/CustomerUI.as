package AerSysCo.UI.Models
{
	import AerSysCo.Server.Customer;
	import mx.collections.ArrayCollection;
	import AerSysCo.UI.Models.Memento.CustomerUIMemento;
	
	[Bindable]
	public class CustomerUI
	{
		public var customerId:int;
		public var brandId:int;
		public var defaultWarehouseId:int;
		public var mACPACCustomerNumber:String;
		public var salesRepCompanyName:String;
		public var creditStatus:Boolean;
		public var maxOrderTotal:Number;
		public var shoppingCart:ShoppingCartUI;

		public var brandName:String;
    	public var dayBalance:Number;
		
		public var address:AddressUI;
		public var phoneNumber:String;
		public var fax:String;
		public var email:String;

		public var shipmentAddresses:ArrayCollection = new ArrayCollection();
		public var defaultWarehouse:WarehouseUI;
		
		public var enabled:Boolean = true;
		
		public function get addressString():String 
		{
			if (address) 
			{
				return (address.address1 + " " + address.address2 + " " + address.city + ", " + address.state + " " + address.zip);
			} else 
			{
				return "n/a";
			}
		}
		
		public function get mACPACStatus():Boolean 
		{
			return true; 
		}
		
		public function get customerNumber():String 
		{
			return (mACPACCustomerNumber.substr(8));
		}
		
		public function populateFromCustomer(value:Customer, cascade:Boolean = true):void 
		{
			this.customerId = value.customerId;
			this.defaultWarehouseId = value.defaultWarehouseId;
			this.mACPACCustomerNumber = value.MACPACCustonerNumber;
			this.salesRepCompanyName = value.salesRepCompanyName;
			this.creditStatus = value.creditStatus;
			this.maxOrderTotal = value.maxOrderTotal;
			this.phoneNumber = value.phoneNumber;
			this.fax = value.fax;
			this.email = value.email;
			this.brandId = value.brandId;
			this.brandName = value.brandName;
			this.dayBalance = value.dayBalance;
			
			if (cascade && value.address) 
			{
				var addr:AddressUI = new AddressUI();
				addr.populateFromAddress(value.address, cascade);
				this.address = addr;
			}
		}
		
		public function toCustomer():Customer 
		{
			var result:Customer = new Customer();
			
			result.customerId = this.customerId;
			result.defaultWarehouseId = this.defaultWarehouseId;
			result.MACPACCustonerNumber = this.mACPACCustomerNumber;
			result.salesRepCompanyName = this.salesRepCompanyName;
			result.creditStatus = this.creditStatus;
			result.maxOrderTotal = this.maxOrderTotal;
			result.phoneNumber = this.phoneNumber;
			result.fax = this.fax;
			result.email = this.email;
			result.brandId = this.brandId;
			result.brandName = this.brandName;
	    	result.dateCreated = new Date();
	    	result.lastUpdateDate = new Date();
			result.dayBalance = this.dayBalance;
			
			if (this.address) 
			{
				result.address = this.address.toAddress();
			}
			
			return result;
		}
		
	    private var memento:CustomerUIMemento;
	    public function setMemento():void 
	    {
	    	if (!memento)
	    		memento = new CustomerUIMemento();
	    	
			memento.customerId = customerId;
			memento.defaultWarehouseId = defaultWarehouseId;
			memento.mACPACCustomerNumber = mACPACCustomerNumber;
			memento.salesRepCompanyName = salesRepCompanyName;
			memento.creditStatus = creditStatus;
			memento.maxOrderTotal = maxOrderTotal;
			memento.phoneNumber = phoneNumber;
			memento.fax = fax;
			memento.email = email;
			memento.brandName = brandName;
	    }
	    public function getMemento():void 
	    {
	    	if (memento) 
	    	{
				customerId = memento.customerId;
				defaultWarehouseId = memento.defaultWarehouseId;
				mACPACCustomerNumber = memento.mACPACCustomerNumber;
				salesRepCompanyName = memento.salesRepCompanyName;
				creditStatus = memento.creditStatus;
				maxOrderTotal = memento.maxOrderTotal;
				phoneNumber = memento.phoneNumber;
				fax = memento.fax;
				email = memento.email;
				brandName = memento.brandName;
	    	}
	    }
	    
	}
}