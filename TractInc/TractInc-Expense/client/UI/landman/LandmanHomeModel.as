package UI.landman
{

	import mx.collections.ListCollectionView;
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class LandmanHomeModel
	{
		
		public var bills:ArrayCollection;
		
		public var rejectedBills:ListCollectionView;
		public var approvedBills:ListCollectionView;
		public var newBills:ListCollectionView;
		public var submittedBills:ListCollectionView;
		
	}
	
}
