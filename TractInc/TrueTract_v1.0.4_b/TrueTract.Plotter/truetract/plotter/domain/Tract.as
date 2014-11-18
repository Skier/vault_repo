package truetract.plotter.domain
{
    import flash.events.EventDispatcher;
    import flash.net.SharedObject;
    
    import mx.collections.ArrayCollection;
    import mx.events.CollectionEvent;
    import mx.events.CollectionEventKind;
    import mx.events.PropertyChangeEvent;
    import mx.states.State;
    
    import truetract.plotter.domain.dictionary.DictionaryRegistry;
    import flash.events.IEventDispatcher;
    import mx.collections.ICollectionView;
    
    [Bindable]
//    [RemoteClass(alias="TractInc.TrueTract.Entity.TractInfo")]
    public class Tract extends EventDispatcher
    {
        public var TractId:int;
        public var Easting:Number = 0;
        public var Northing:Number = 0;
        public var RefName:String = "";
        public var CreatedBy:int;
        public var IsDeleted:Boolean;
		
		public var DocId:int;
		public var CalledAC:Number = 0;
		public var UnitId:int;

		public var ParentDocument:Document;

        public var Calls:ArrayCollection;
        public var TextObjects:ArrayCollection;

        public var IsClosed:Boolean;
        public var IsDirty:Boolean = false;

        public function Tract()
        {
            addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, property_changeHandler);

            Calls = new ArrayCollection();
            TextObjects = new ArrayCollection();
        }

        public function get UnitName():String
        {
            return DictionaryRegistry.getInstance().getUnitName(UnitId);
        }
        
        public function get calledString():String
        {
            return "Called " + CalledAC + " " + UnitName;
        }
        
        public function GetCallByOrder(callOrder:int):TractCall
        {
            for each (var call:TractCall in Calls)
            {
                if (call.CallOrder == callOrder)
                    return call;
            }

            return null;
        }

        public function RemoveCall(call:TractCall):void
        {
            Calls.removeItemAt(Calls.getItemIndex(call));
        }
        
        public function RemoveTextObject(textObject:TractTextObject):void
        {
            TextObjects.removeItemAt(TextObjects.getItemIndex(textObject));
        }
            
        public function isRequiredFieldsEmpty():Boolean
        {
            var result:Boolean = false;
            
            if (RefName.length == 0 || UnitId == 0)
                result = true;

            if (ParentDocument)
            {
                if (ParentDocument.DocTypeId == 0 
                    || ParentDocument.State == 0 
                    || ParentDocument.County == 0
                    || ParentDocument.Buyer.AsNamed.length == 0
                    || ParentDocument.Seller.AsNamed.length == 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public function ToTractWO():TractWO 
        {
            var tractWO:TractWO = new TractWO();

            tractWO.TractId = TractId;
            tractWO.Easting = Easting;
            tractWO.Northing = Northing;
            tractWO.RefName = RefName;
            tractWO.CreatedBy = CreatedBy;
            tractWO.IsDeleted = IsDeleted;
            tractWO.DocId = DocId;
            tractWO.CalledAC = CalledAC;
            tractWO.UnitId = UnitId;
            tractWO.UnitName = UnitName;

            tractWO.Calls = Calls.toArray();
            tractWO.TextObjects = TextObjects.toArray();

            if (ParentDocument)
                tractWO.ParentDocument = ParentDocument.ToDocumentWO();

            return tractWO;
        }

        private function childCollection_changehandler(event:CollectionEvent):void 
        {
            if (event.kind != CollectionEventKind.REFRESH) {
                IsDirty = true;
            }
        }

        private function property_changeHandler(event:PropertyChangeEvent):void 
        {
            if (event.property != "IsDirty" && event.property != "IsClosed" && event.oldValue != event.newValue) 
            {
                IsDirty = true;
            }
            
            if (event.newValue is IEventDispatcher)
            {
                if (event.newValue is ICollectionView)
                {
                    event.newValue.addEventListener(CollectionEvent.COLLECTION_CHANGE, childCollection_changehandler);
                }
                else
                {
                    event.newValue.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, property_changeHandler);
                }
            }
        }
        
    }
}