package truetract.plotter.domain
{
	import flash.events.EventDispatcher;
	import flash.utils.Timer;
	import flash.utils.getTimer;
	
	import mx.collections.ArrayCollection;
	import mx.events.FlexEvent;
	import mx.events.PropertyChangeEvent;
	import mx.states.State;
	
	import truetract.plotter.domain.dictionary.DictionaryRegistry;

    [Bindable]
//	[RemoteClass(alias="TractInc.TrueTract.Entity.Document")]
	public class Document extends EventDispatcher implements IAttach
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

        public var Tracts:ArrayCollection;

		private var _attachments:ArrayCollection = new ArrayCollection;
		public function get Attachments():ArrayCollection {return _attachments;}
		public function set Attachments(value:ArrayCollection):void {_attachments = value;}

        private var _buyer:Participant = new Participant;
        public function get Buyer():Participant { return _buyer; }
        public function set Buyer(value:Participant):void
        {
            _buyer = value;
            
            if (value != null)
            {
                var docIntance:Document = this;
                
                value.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE,
                    function(event:PropertyChangeEvent):void
                    {
                        docIntance.dispatchEvent(event);
                    })
            }
        }

        private var _seller:Participant = new Participant;
        public function get Seller():Participant { return _seller; }
        public function set Seller(value:Participant):void
        {
            _seller = value;
            
            if (value != null)
            {
                _seller.IsSeler = true;

                var docIntance:Document = this;

                value.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE,
                    function(event:PropertyChangeEvent):void
                    {
                        docIntance.dispatchEvent(event);
                    })
            }
        }

        public function Document()
        {
            DateFiled = new Date();
            DateSigned = new Date();
            
            Buyer = new Participant;
            Seller = new Participant;
        }

        public function get DocumentType():XMLList
        {
            return DictionaryRegistry.getInstance().getDocumentType(DocTypeId);
        }

        public function get DocumentPlacement():String
        {
            if (DocumentNo != null) {
                return "DocNo: " + DocumentNo;
            } else {
                return "Vol: " + Volume + ", Pg: " + Page;
            }
        }

        public function get CountyName():String
        {   
            var result:String = DictionaryRegistry.getInstance().getCountyName(State, County);
            return result ? result : "";
        }
        
        public function get StateName():String
        {
            return DictionaryRegistry.getInstance().getStateName(State);
        }
        
        public function get DocumentTypeName():String
        {
            return DocumentType.@Name;
        }

        public function get SellerName():String
        {
            return (Seller) ? Seller.AsNamed : '';
        }

        public function get BuyerName():String
        {
            return (Buyer) ? Buyer.AsNamed : '';
        }
        
        public function get SellerRoleName():String
        {
            return DocumentType.@GiverRoleName;
        }
        
        public function get BuyerRoleName():String
        {
            return DocumentType.@ReceiverRoleName;
        }
        
        public function get DateSigned():Date
        {
            return new Date(DateSignedYear, DateSignedMonth - 1, DateSignedDay);
        }

        public function set DateSigned(value:Date):void
        {
            DateSignedYear = value ? value.getFullYear() : null;
            DateSignedMonth = value ? value.getMonth() + 1 : null;
            DateSignedDay = value ? value.getDate() : null;
        }

        public function get DateFiled():Date
        {
            return new Date(DateFiledYear, DateFiledMonth -1, DateFiledDay);
        }

        public function set DateFiled(value:Date):void
        {
            DateFiledYear = value ? value.getFullYear() : null;
            DateFiledMonth = value ? value.getMonth()+ 1 : null;
            DateFiledDay = value ? value.getDate() : null;
        }
        
        public function ToDocumentWO():DocumentWO 
        {
            var result:DocumentWO = new DocumentWO();

            result.County = County;
            result.DateFiledDay = DateFiledDay;
            result.DateFiledMonth = DateFiledMonth;
            result.DateFiledYear = DateFiledYear;
            result.DateSignedDay = DateSignedDay;
            result.DateSignedMonth = DateSignedMonth;
            result.DateSignedYear = DateSignedYear;
            result.DocID = DocID;
            result.DocTypeId = DocTypeId;
            result.DocumentNo = DocumentNo;
            result.ImageLink = ImageLink;
            result.IsPublic = IsPublic;
            result.Page = Page;
            result.ResearchNote = ResearchNote;
            result.State = State;
            result.Volume = Volume;
            result.Buyer = Buyer;
            result.Seller = Seller;
            result.StateName = StateName;
            result.CountyName = CountyName;
            result.DocumentTypeName = DocumentTypeName;
            result.SellerRoleName = SellerRoleName;
            result.BuyerRoleName = BuyerRoleName;
            
            if (Attachments != null) {
	            result.Attachments = Attachments.toArray();
            }
    
            return result;
        }

	}
}