package Domain
{
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class Route
	{
		public var routeId:int;
		public var dispatcher:Dispatcher;
		public var technician:Technician;
		public var van:Van;
		public var startDate:Date;
		public var status:String;
		public var startMessage:String;
		public var endMessage:String;
		public var routeStops:ArrayCollection = new ArrayCollection();
		
	}
}