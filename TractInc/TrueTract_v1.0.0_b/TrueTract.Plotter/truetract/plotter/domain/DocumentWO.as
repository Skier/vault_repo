package truetract.plotter.domain
{
	import mx.collections.ArrayCollection;
	
	[RemoteClass(alias="TractInc.TrueTract.Entity.DocumentInfo")]
	public class DocumentWO
	{
        public var DocID:int;
        public var IsPublic:Boolean;
        public var DocTypeId:int;
        public var Volume:String;
        public var Page:String;
        public var DocumentNo:String;
        public var County:int;
        public var State:int;
        public var DateFiledYear:int;
        public var DateFiledMonth:int;
        public var DateFiledDay:int;
        public var DateSignedYear:int;
        public var DateSignedMonth:int;
        public var DateSignedDay:int;
        public var ResearchNote:String;
        public var ImageLink:String;
        
        public var CountyName:String;
        public var StateName:String;
        public var DocumentTypeName:String;
        public var BuyerRoleName:String;
        public var SellerRoleName:String;

        public var Buyer:Participant;
        public var Seller:Participant;

        public var Tracts:Array = new Array();

        public function DocumentWO()
        {
        }

		public function ToDocument():Document {
			var document:Document = new Document();
			
	        document.DocID = DocID;
	        document.IsPublic = IsPublic;
	        document.DocTypeId = DocTypeId;
	        document.Volume = Volume;
	        document.Page = Page;
	        document.DocumentNo = DocumentNo;
	        document.County = County;
	        document.State = State;
	        document.DateFiledYear = DateFiledYear;
	        document.DateFiledMonth = DateFiledMonth;
	        document.DateFiledDay = DateFiledDay;
	        document.DateSignedYear = DateSignedYear;
	        document.DateSignedMonth = DateSignedMonth;
	        document.DateSignedDay = DateSignedDay;
	        document.ResearchNote = ResearchNote;
	        document.ImageLink = ImageLink;
	        document.Buyer = Buyer;
	        document.Seller = Seller;

	        document.Tracts = new ArrayCollection();
	        
	        for each (var tractWO:TractWO in Tracts)
	        {
	        	var tract:Tract = tractWO.ToTract();
	        	tract.ParentDocument = document;
		        document.Tracts.addItem(tract);
	        }
        
			return document;
		}
	}
}