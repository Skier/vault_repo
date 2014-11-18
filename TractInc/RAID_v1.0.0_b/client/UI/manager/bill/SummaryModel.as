package UI.manager.bill
{
	
	import mx.collections.ArrayCollection;
	import App.Domain.User;
	import weborb.data.ActiveCollection;
	
	[Bindable]
	public class SummaryModel
	{
		public var messages:ArrayCollection = new ArrayCollection();
		
		public var approvedBills:ArrayCollection = new ArrayCollection();
		public var submittedBills:ArrayCollection = new ArrayCollection();
		public var rejectedBills:ArrayCollection = new ArrayCollection();
		public var correctedBills:ArrayCollection = new ArrayCollection();
		public var declinedBills:ArrayCollection = new ArrayCollection();
		public var confirmedBills:ArrayCollection = new ArrayCollection();
		public var verifiedBills:ArrayCollection = new ArrayCollection();

		public var startDate:Date;
		
		public var endDate:Date;
		
		public var showAllBills:Boolean;
		
		public var notBusy:Boolean = false;

	}
}
