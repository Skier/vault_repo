package UI.crew
{
	
	import mx.collections.ArrayCollection;
	import App.Entity.CrewChiefDataObject;
	import mx.collections.ListCollectionView;
	
	[Bindable]
	public class SummaryModel
	{
		public var messages:ArrayCollection = new ArrayCollection();
		
		public var bills:ArrayCollection = new ArrayCollection();
		
		public var currentBills:ArrayCollection = new ArrayCollection();
		public var approvedBills:ArrayCollection = new ArrayCollection();
		public var rejectedBills:ArrayCollection = new ArrayCollection();
		public var confirmedBills:ListCollectionView;

		public var startDate:Date;
		
		public var endDate:Date;
		
		public var showAllBills:Boolean;
		
		public var notBusy:Boolean = false;
		
		public var data:CrewChiefDataObject;

	}
}
