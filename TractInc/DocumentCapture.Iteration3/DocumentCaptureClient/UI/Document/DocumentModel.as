package UI.Document
{
	import Domain.Document;
	import mx.collections.ArrayCollection;
	import Domain.Documenttype;
	import Domain.UsStates;
	import Domain.Participant;
	import Domain.Participantrole;
	
	[Bindable]
	public class DocumentModel
	{
		public var CurrentDocument:Document;
		public var CurrentDocumentType:Documenttype;
		
		public var Types:ArrayCollection = new ArrayCollection();
		public var States:ArrayCollection = new ArrayCollection();
		public var Counties:ArrayCollection = new ArrayCollection();
		
		public var SellerAsNamed:Participant;
		public var BuyerAsNamed:Participant;
		
		public var SellerAsNamedReservations:ArrayCollection = new ArrayCollection();
		public var BuyerAsNamedReservations:ArrayCollection = new ArrayCollection();
		
		public var SellersDetailed:ArrayCollection = new ArrayCollection();
		public var BuyersDetailed:ArrayCollection = new ArrayCollection();

		public var SellerRole:Participantrole;
		public var BuyerRole:Participantrole;
		
		public var Tracts:ArrayCollection = new ArrayCollection;
		
		public var IsMatched:Boolean = false;

		public var IsKeysFilled:Boolean = false;

		public function DocumentModel() {
//			States = UsStates.GetItems();
//			Counties = County.GetItems();
			
			monthes.addItem("01-Jan");
			monthes.addItem("02-Feb");
			monthes.addItem("03-Mar");
			monthes.addItem("04-Apr");
			monthes.addItem("05-May");
			monthes.addItem("06-Jun");
			monthes.addItem("07-Jul");
			monthes.addItem("08-Aug");
			monthes.addItem("09-Sep");
			monthes.addItem("10-Oct");
			monthes.addItem("11-Nov");
			monthes.addItem("12-Dec");
			
		}
		
		public var monthes:ArrayCollection = new ArrayCollection();

	}
}