package Service
{
	import mx.collections.ArrayCollection;
	import Domain.Customer;
	
	public class CustomerService
	{
		public static function getAllCustomers():ArrayCollection{
			return Database.Instance.Customers;
		}
		
		public static function findCustomerByPhoneNumber(phoneNumber:String):ArrayCollection{
			return Database.Instance.findCustomerByPhoneNumber(phoneNumber);
		}
		
		public static function findJobsByCustomer(customer:Customer):ArrayCollection{
			return Database.Instance.findJobsByCustomer(customer);
		}
		
		public static function createCustomer(cust:Customer):Customer{
			return Database.Instance.createCustomer(cust);
		}
	}
}