package Domain
{
	public class RouteStop
	{
		public static const PENDING_STATUS:String = "PENDING";
		public static const ON_ROUTE_STATUS:String = "ONROUTE";
		public static const ARRIVED_STATUS:String = "ARRIVED";
		public static const COMPLETED_STATUS:String = "COMPLETED";
		public static const NOGO_STATUS:String = "NOGO";
		
		
		public var routeStopId:int;
		public var route:Route;
		public var jobTicket:JobTicket;
		public var sequence:int;
		public var status:String;
		
	}
}