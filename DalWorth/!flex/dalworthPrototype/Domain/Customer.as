package Domain
{
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class Customer
	{
		public var customerId:int = -1;
		public var companyName:String;
		public var firstName:String;
		public var lastName:String;
		public var phone:String;
		public var mobile:String;
		public var email:String;
		public var billAddress:Address = new Address();
		public var jobs:ArrayCollection = new ArrayCollection();
		
	}
}