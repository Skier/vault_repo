package src.deedplotter.domain
{
    import flash.events.EventDispatcher;
    import flash.net.SharedObject;
    
    import mx.collections.ArrayCollection;
    import mx.events.CollectionEvent;
    import mx.events.CollectionEventKind;
    import mx.events.PropertyChangeEvent;
    import mx.states.State;
    
    import src.deedplotter.domain.dictionary.DictionaryRegistry;
    
    [Bindable]
    public class Tract extends EventDispatcher
    {
        public var Uid:String;

        public var TractId:int;
        public var Easting:Number = 0;
        public var Northing:Number = 0;
        public var Description:String = "";
        public var CreatedBy:int;
        public var IsDeleted:Boolean;
		
		public var DocId:int;
		public var CalledAC:Number = 0;
		public var UnitId:int;

		public var ParentDocument:Document;

        public var Calls:ArrayCollection = new ArrayCollection();
        public var TextObjects:ArrayCollection = new ArrayCollection();

        public var IsClosed:Boolean;
        public var IsDirty:Boolean = false;

        public function Tract()
        {
            Calls.addEventListener(CollectionEvent.COLLECTION_CHANGE, childCollection_changehandler);
            TextObjects.addEventListener(CollectionEvent.COLLECTION_CHANGE, childCollection_changehandler);

            addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, property_changeHandler);
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
            
        public function IfExist(tractList:ArrayCollection):Boolean
        {
            if (!tractList)
                return false;

            for each (var tract:Tract in tractList)
            {
                if (tract.Uid == Uid)
                    return true;
            }

            return false;
        }

        public function isRequiredFieldsEmpty():Boolean
        {
            var result:Boolean = false;
            
            if (Description.length == 0 || UnitId == 0)
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

            tractWO.Uid = Uid;
            tractWO.TractId = TractId;
            tractWO.Easting = Easting;
            tractWO.Northing = Northing;
            tractWO.Description = Description;
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
            if (event.property != "IsDirty" && event.oldValue != event.newValue) 
            {
                IsDirty = true;
            }
            
            if (event.property == 'ParentDocument' && event.newValue != null)
            {
                ParentDocument.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, property_changeHandler);
            }
        }
        
    }
}