package UI.manager.admin.assignment
{
	
	import mx.collections.ArrayCollection;
	import App.Entity.ManagerDataObject;
	import App.Entity.AssetDataObject;
	import mx.collections.ListCollectionView;
	
	[Bindable]
	public class AssignmentsModel
	{
		
		public var countableBillItemTypes:ArrayCollection;
		
		public var assets:ListCollectionView;
		
		public var assetsByChief:Array;
		
		public var clients:ArrayCollection = new ArrayCollection();

		public var afesByClient:Array;
		
		public var projectsByAfe:Array;
		
		public var projectsByAsset:Array;
		
		public var data:ManagerDataObject;
			
		public var isLoading:Boolean = true;
		
		public var currentAsset:AssetDataObject;
		
	}
	
}
