package UI.landman
{
	import weborb.data.ActiveCollection;
	import mx.collections.ListCollectionView;
	
	[Bindable]
	public class LandmanHomeModel
	{
		
		public var bills:ActiveCollection;
		
		public var rejectedBills:ListCollectionView;
		public var approvedBills:ListCollectionView;
		public var newBills:ListCollectionView;
		public var submittedBills:ListCollectionView;
		
	}
	
}
