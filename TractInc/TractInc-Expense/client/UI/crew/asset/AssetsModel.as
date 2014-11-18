package UI.crew.asset
{
	
	import mx.collections.ArrayCollection;
	import App.Entity.CrewChiefDataObject;
	
	[Bindable]
	public class AssetsModel
	{
		
		public var assets:ArrayCollection = new ArrayCollection();
		
		public var clients:ArrayCollection = new ArrayCollection();
		
		public var afes:ArrayCollection = new ArrayCollection();
		
		public var afesByClient:Array;
		
		public var projectsByAfe:Array;
			
		public var projectsByAsset:Array;
		
		public var isLoading:Boolean = true;
		
		public var data:CrewChiefDataObject;
		
	}
	
}
