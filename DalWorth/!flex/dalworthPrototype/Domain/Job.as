package Domain
{
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class Job
	{
		public static  const PENDING_DISPATCH_STATUS:String = "PENDING";
		public static  const INWORK:String = "INWORK";
		public static  const DISPATCHED:String = "COMPLETED";
		
		public static const JOB_TYPE_CLEAN_RUG:String = "CLEAN_RUG";
		
		
		public var jobNumber:int;
		public var type:String;
		public var customer:Customer;
		public var serviceAddress:Address;
		public var status:String;
		public var description:String;
		public var tickets:ArrayCollection = new ArrayCollection();
		
	}
}