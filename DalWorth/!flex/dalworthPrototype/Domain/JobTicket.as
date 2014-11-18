package Domain
{
	public class JobTicket
	{
		
		public static  const PENDING_DISPATCH_STATUS:String = "PENDING";
		public static  const INWORK:String = "INWORK";
		public static  const DISPATCHED:String = "COMPLETED";
		
		
		public static  const RUG_PICKUP_TICKET_TYPE:String = "RUG_PICKUP";
		public static  const RUG_DELIVERY_TICKET_TYPE:String = "RUG_DELIVERY";
		
		public var ticketNumber:int;
		public var job:Job;
		public var status:String;
		public var type:String;
		public var createDate:Date;
		public var serviceDate:Date;
		public var description:String;
		public var message:String;
		public var notes:String;
	}
}