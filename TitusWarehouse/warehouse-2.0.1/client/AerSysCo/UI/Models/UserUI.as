package AerSysCo.UI.Models
{
	import mx.collections.ArrayCollection;
	import AerSysCo.Server.SalesRep;
	import AerSysCo.Server.Customer;
	
	[Bindable]
	public class UserUI
	{
		public var customerList:ArrayCollection = new ArrayCollection();
		public var currentCustomer:CustomerUI;
		public var userId:int;
		public var login:String;
		public var password:String;
		public var firstName:String;
		public var lastName:String;
		
		public function populateFromSalesRep(value:SalesRep, cascade:Boolean = false):void 
		{
			this.userId = value.SalesRepId;
			this.login = value.UserName;
			this.password = value.Password;
			this.firstName = value.FirstName;
			this.lastName = value.LastName;
			
			if (cascade && value.customerList) 
			{
				this.customerList.removeAll();

				for each (var customer:Customer in value.customerList.toArray()) 
				{
					var cust:CustomerUI = new CustomerUI();
					cust.populateFromCustomer(customer, cascade);
					this.customerList.addItem(cust);
				}
			}
			
		}
		
		public function toSalesRep():SalesRep 
		{
			var result:SalesRep = new SalesRep();
			
			result.SalesRepId = this.userId;
			result.UserName = this.login;
			result.Password = this.password;
			result.FirstName = this.firstName;
			result.LastName = this.lastName;
			
			result.customerList.removeAll();
			for each (var custUI:CustomerUI in this.customerList) 
			{
				result.customerList.addItem(custUI.toCustomer());
			}
			
			return result;
		}

	}
}