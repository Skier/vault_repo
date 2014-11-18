package truetract.domain
{
import flash.events.EventDispatcher;

import mx.events.PropertyChangeEvent;
import mx.events.PropertyChangeEventKind;
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="TractInc.TrueTract.Entity.DocumentsFilterInfo")]
public class DocumentsFilter extends EventDispatcher implements IItemsFilter
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

    public function DocumentsFilter()
    {
        reset();
    }

    private var dictionary:DictionaryRegistry = DictionaryRegistry.getInstance();

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
    
    public function applyFilter(itemsList:ArrayCollection):void
    {
        if (!isSpecified()) return;

        for (var i:int = 0; i < itemsList.length; i++)
        {
            var doc:Document = Document(itemsList.getItemAt(i));

            if (   (stateId > 0 && doc.State != stateId)
                || (countyId > 0 && doc.County != countyId)
                || (docTypeId > 0 && doc.DocTypeId != docTypeId)
                || (docNumber && doc.DocumentNo != docNumber)
                || (volume && doc.Volume != volume)
                || (page && doc.Page != page)
                || (seller && doc.Seller.AsNamed.indexOf(seller) == -1)
                || (buyer && doc.Buyer.AsNamed.indexOf(buyer) == -1)
                || (createdRange && !createdRange.inRange(doc.Created))
                || (filedRange && !filedRange.inRange(doc.DateFiled))
                || (signedRange && !signedRange.inRange(doc.DateSigned)))
            {
                itemsList.removeItemAt(i);
                break;
            }
        }
    }
    
    public function getFilterParams():Object
    {
        var params:Object = new Object();

        if (stateId != 0) {
            params['State'] = dictionary.getState(stateId).@Name;
        }

        if (countyId != 0) {
            params["County"] = dictionary.getCountyName(stateId, countyId);
        }

        if (docTypeId != 0) {
            params["Doc.Type"] = dictionary.getDocumentType(docTypeId).@Name;
        }

        if (docNumber != null) {
            params["Doc.Number"] = docNumber;
        }

        if (volume != null) {
            params["Volume"] = volume;
        }

        if (page != null) {
            params["Page"] = page;
        }

        if (buyer != null) {
            params["Buyer"] = buyer;
        }

        if (seller != null) {
            params["Seller"] = seller;
        }

        if (signedRange != null) {
            params["Signed"] = signedRange;
        }

        if (filedRange != null) {
            params["Filed"] = filedRange;
        }

        if (createdRange != null) {
            params["Created"] = createdRange;
        }
        
        return params;
    }
}
}