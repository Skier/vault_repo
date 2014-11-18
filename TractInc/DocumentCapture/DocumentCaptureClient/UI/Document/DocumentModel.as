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
		public static const VIEWSTATE_DOCUMENT_CREATE:int = 0;
		public static const VIEWSTATE_DOCUMENT_EDIT:int = 1;
		public static const VIEWSTATE_PARTICIPANT_EDIT:int = 2;
		public static const VIEWSTATE_TRACT_EDIT:int = 3;
		public static const VIEWSTATE_WAITING:int = 4;

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

		public var IsSellerAsNamedInited:Boolean = false;
		public var IsBuyerAsNamedInited:Boolean = false;
		public var IsSellersDetailedInited:Boolean = false;
		public var IsBuyersDetailedInited:Boolean = false;
		public var IsTractsInited:Boolean = false;

	}
}