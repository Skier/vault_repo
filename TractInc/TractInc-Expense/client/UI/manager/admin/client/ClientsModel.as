package UI.manager.admin.client
{
	
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class ClientsModel
	{
		
		public var clients:ArrayCollection;
		
		public var afesByClient:Array;
		
		public var projectsByAfe:Array;
		
		public var assetsByProject:Array;
		
		public var assignmentsByProject:Array;
		
	}
	
}
