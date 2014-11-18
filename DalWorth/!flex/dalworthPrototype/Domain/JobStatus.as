// ActionScript file
package Domain
{
	[Bindable]
	
	
	public class JobStatus
	{
		public static  const PENDING_DISPATCH_STATUS:String = "PENDING";
		public static  const INWORK:String = "INWORK";
		public static  const DISPATCHED:String = "COMPLETED";
		
		public var status:String;
		public var description:String;
	}
}