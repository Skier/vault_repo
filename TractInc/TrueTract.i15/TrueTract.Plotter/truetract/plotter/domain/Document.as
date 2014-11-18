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
	import mx.formatters.DateFormatter;

    [Bindable]
	[RemoteClass(alias="TractInc.TrueTract.Entity.DocumentInfo")]
	public class Document extends EventDispatcher
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
        public var CreatedBy:int;
        public var DateModified:Date;
        public var IsActive:Boolean;
        public var DocBranchUid:String;
        public var IsLoaded:Boolean;

        private var _TractsList:ArrayCollection = new ArrayCollection();
        public function get TractsList():ArrayCollection { return _TractsList; }
    
        private var _Tracts:Array;
        public function get Tracts():Array { return _Tracts; }
        public function set Tracts(value:Array):void 
        {
            TractsList.source = _Tracts = value;
            
            if (value && value.length > 0)
            {
                for each (var tract:Tract in value)
                {
                    tract.ParentDocument = this;
                }
            }
        }

        private var _buyer:Participant = new Participant;
        public function get Buyer():Participant { return _buyer; }
        public function set Buyer(value:Participant):void
        {
            _buyer = value;
            
            if (value != null)
            {
                _seller.IsSeler = false;
                var docInstance:Document = this;
                
                value.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE,
                    function(event:PropertyChangeEvent):void
                    {
                        docInstance.dispatchEvent(event);
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
        
        public function get CountyName():String
        {   
            var result:String = DictionaryRegistry.getInstance().getCountyName(State, County);
            return result ? result : "";
        }
        
        public function get StateName():String
        {
            return DictionaryRegistry.getInstance().getState(State).@Name;
        }
        
        public function get DocumentTypeName():String
        {
            return DocumentType.@Name;
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

        public function get DateModifiedString():String
        {
            var df:DateFormatter = new DateFormatter();
            df.formatString = "DD MMM YYYY J:NN";
    
            return df.format(DateModified);
        }

        public function get DocumentPlacement():String
        {
            return (DocumentNo && DocumentNo.length > 0) 
                ? 'Doc.No:' + DocumentNo
                : 'Vol:' + Volume + ', Pg:' + Page;
        }
        
        override public function toString():String
        {
            var stateAbbr:String = DictionaryRegistry.getInstance().getState(State).@StateAbbr;
            return stateAbbr + ', ' + CountyName + ', ' + DocumentPlacement;
        }
 
	}
}