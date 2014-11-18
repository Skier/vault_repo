package truetract.domain
{
import flash.events.EventDispatcher;

import mx.events.PropertyChangeEvent;
import mx.events.PropertyChangeEventKind;

[Bindable]
[RemoteClass(alias="TractInc.TrueTract.Entity.DocumentFilterInfo")]
public class DocumentFilter extends EventDispatcher
{
    public var stateId:int;
    public var countyId:int;
    public var docTypeId:int;
    public var docNumber:String;
    public var volume:String;
    public var page:String;
    public var seller:String;
    public var buyer:String;
    public var createdRange:DateRange;
    public var signedRange:DateRange;
    public var filedRange:DateRange;

    public function DocumentFilter()
    {
        reset();
    }

    public function isSpecified():Boolean
    {
        return stateId != 0 || countyId != 0 || docTypeId != 0 || docNumber || 
            volume || page || seller || buyer || createdRange || signedRange || filedRange;
    }

    public function reset():void
    {
        stateId = 0;
        countyId = 0;
        docTypeId = 0;
        docNumber = null;
        volume = null;
        page = null;
        createdRange = null;
        signedRange = null;
        filedRange = null;
        seller = null;
        buyer = null;

        dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, 
            true, false, PropertyChangeEventKind.UPDATE, "isSpecified"));
    }
}
}