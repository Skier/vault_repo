package truetract.domain
{
import flash.events.EventDispatcher;
import flash.events.IEventDispatcher;

import mx.collections.ArrayCollection;
import mx.collections.ICollectionView;
import mx.events.CollectionEvent;
import mx.events.CollectionEventKind;
import mx.events.PropertyChangeEvent;
import mx.states.State;

[Bindable]
[RemoteClass(alias="TractInc.TrueTract.Entity.TractInfo")]
public class Tract extends EventDispatcher
{

    //--------------------------------------------------------------------------
    //
    //  Constructor
    //
    //--------------------------------------------------------------------------
    public function Tract()
    {
        addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, property_changeHandler);
        CallsList.addEventListener(CollectionEvent.COLLECTION_CHANGE, childCollection_changeHandler);
        TextObjectsList.addEventListener(CollectionEvent.COLLECTION_CHANGE, childCollection_changeHandler);

        Calls = [];
        TextObjects = [];
    }

    //--------------------------------------------------------------------------
    //
    //  Table fields
    //
    //--------------------------------------------------------------------------

    public var TractId:int;
    public var Easting:Number = 0;
    public var Northing:Number = 0;
    public var RefName:String = "";
    public var CreatedBy:int;
	public var DocId:int;
	public var CalledAC:Number = 0;
	public var UnitId:int;
	
	public var UniqueId:String;

    //--------------------------------------------------------------------------
    //
    //  Foreign key objects
    //
    //--------------------------------------------------------------------------
	public var ParentDocument:Document;

    //--------------------------------------------------------------------------
    //
    //  Related Collections. ArrayCollection for binding, Array for WebOrb. 
    //  Thank you, WebOrb, for this.
    //
    //--------------------------------------------------------------------------

    private var _CallsList:ArrayCollection = new ArrayCollection();
    public function get CallsList():ArrayCollection { return _CallsList; }

    private var _Calls:Array;
    public function get Calls():Array { return _Calls; }
    public function set Calls(value:Array):void 
    {
        CallsList.source = _Calls = value;
    }

    private var _TextObjectsList:ArrayCollection = new ArrayCollection();
    public function get TextObjectsList():ArrayCollection { return _TextObjectsList; }

    private var _TextObjects:Array;
    public function get TextObjects():Array { return _TextObjects; }
    public function set TextObjects(value:Array):void 
    { 
        TextObjectsList.source = _TextObjects = value;
    }

    //--------------------------------------------------------------------------
    //
    //  Service fields
    //
    //--------------------------------------------------------------------------
    public var IsClosed:Boolean;
    public var IsDirty:Boolean = false;
    public var IsLoaded:Boolean = false;

	public var IsSelected:Boolean = false;

    //--------------------------------------------------------------------------
    //
    //  Properties
    //
    //--------------------------------------------------------------------------

    public function get UnitName():String
    {
        return DictionaryRegistry.getInstance().getUnitName(UnitId);
    }
    
    public function get calledString():String
    {
        return CalledAC + " " + UnitName;
    }
    
    public function GetCallByOrder(callOrder:int):TractCall
    {
        for each (var call:TractCall in CallsList)
        {
            if (call.CallOrder == callOrder)
                return call;
        }

        return null;
    }

    //--------------------------------------------------------------------------
    //
    //  Methods
    //
    //--------------------------------------------------------------------------

    public function RemoveCall(call:TractCall):void
    {
        CallsList.removeItemAt(CallsList.getItemIndex(call));
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

    public function clone():Tract 
    {
        var clone:Tract = new Tract();
        
        clone.TractId = TractId;
        clone.Easting = Easting;
        clone.Northing = Northing;
        clone.RefName = RefName;
        clone.CreatedBy = CreatedBy;
    	clone.DocId = DocId;
    	clone.CalledAC = CalledAC;
    	clone.UnitId = UnitId;
    	clone.ParentDocument = ParentDocument.clone();
        
    	clone.Calls = [];
        for each (var call:TractCall in Calls){
            clone.Calls.push(call.clone());
        }

    	clone.TextObjects = [];
        for each (var textObject:TractTextObject in TextObjects){
            clone.TextObjects.push(textObject.clone());
        }

        return clone;
    }

    public function get hashString():String 
    {
        var result:String = "";
        
        result += Easting.toString();
        result += Northing.toString();
        result += RefName.toString();
        result += CalledAC.toString();
        result += UnitId.toString();
/* 
        for each (var call:TractCall in Calls){
            result += call.hashString;
        }

        for each (var textObject:TractTextObject in TextObjects){
            result += textObject.hashString;
        }
 */
        return result;
    }

    //--------------------------------------------------------------------------
    //
    //  Event Handlers
    //
    //--------------------------------------------------------------------------

    private function childCollection_changeHandler(event:CollectionEvent):void 
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
            event.newValue.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, property_changeHandler);
        }
    }
    
}
}