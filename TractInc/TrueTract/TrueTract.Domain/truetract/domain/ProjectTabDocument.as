package truetract.domain
{
import mx.collections.ArrayCollection;
import mx.formatters.DateFormatter;

import truetract.domain.mementos.ProjectTabDocumentMemento;
    
[Bindable]
[RemoteClass(alias="TractInc.TrueTract.Entity.ProjectTabDocumentInfo")]
public class ProjectTabDocument implements IMemento
{
    public var ProjectTabDocumentId:int;
    public var ProjectTabId:int;
    public var DocumentId:int;
    public var Description:String;
    public var Remarks:String;
    public var IsActive:Boolean = false;

    public var ProjectTabRef:ProjectTab;
    public var DocumentRef:Document;

    private var _tractsList:ArrayCollection = new ArrayCollection();
    public function get TractsList():ArrayCollection { return _tractsList; }

    private var _tracts:Array;
    public function get Tracts():Array { return _tracts; }
    public function set Tracts(value:Array):void 
    {
        TractsList.source = _tracts = value;
        
        if (value && value.length > 0)
        {
            for each (var tabTract:ProjectTabDocumentTract in value)
            {
                tabTract.ProjectTabDocumentRef = this;
            }
        }
    }

    public function get DocumentTypeName():String
    {
        return DocumentRef ? DocumentRef.DocumentTypeName : null;
    }

    public function get DateSigned():String
    {
        return DocumentRef ? getDateFormater().format(DocumentRef.DateSigned) : null;
    }

    public function get DateFiledDisplayValue():String
    {
        return DocumentRef ? getDateFormater().format(DocumentRef.DateFiled) : null;
    }

    public function get DateFiledSortValue():Number
    {
        return DocumentRef ? DocumentRef.DateFiled.getTime() : NaN;
    }

    public function get SellerName():String
    {
        return DocumentRef ? DocumentRef.SellerName : null;
    }

    public function get BuyerName():String
    {
        return DocumentRef ? DocumentRef.BuyerName : null;
    }
    
    public function get TractsStr():String 
    {
    	return (Tracts.length.toString()) + "/" + (DocumentRef ? DocumentRef.Tracts.length.toString() : "?");
    }

    public function getMemento():Object
    {
        var memento:ProjectTabDocumentMemento = new ProjectTabDocumentMemento();
        memento.projectTabDocumentId = ProjectTabDocumentId;
        memento.documentId = DocumentId;
        memento.projectTabId = ProjectTabId;
        memento.description = Description;
        memento.remarks = Remarks;
        
        memento.tracts = Tracts;

        return memento;
    }

    public function setMemento(value:Object):void
    {
        var memento:ProjectTabDocumentMemento = ProjectTabDocumentMemento(value);
        ProjectTabDocumentId = memento.projectTabDocumentId;
        DocumentId = memento.documentId;
        ProjectTabId = memento.projectTabId;
        Description = memento.description;
        Remarks = memento.remarks;
        
        Tracts = memento.tracts;
    }
    
    public function updateTracts():void 
    {
        if (DocumentRef != null && DocumentRef.TractsList != null && TractsList != null)
        {
            var newTracts:Array = new Array();
            
            for each (var tract:Tract in DocumentRef.TractsList)
            {
                var localTract:ProjectTabDocumentTract = getTabTractByUniqueId(tract.UniqueId);
                
                if (localTract != null)
                {
                    localTract.TractId = tract.TractId;
                    newTracts.push(localTract);
                }
            }

            Tracts = newTracts;
        }
    }

    public function IsTractExists(tract:Tract):Boolean
    {
        for each (var tabTract:ProjectTabDocumentTract in TractsList)
        {
            if (tabTract.TractRef != null && tabTract.TractRef.UniqueId == tract.UniqueId)
                return true;
        }

        return false;
    }
    
    private function getTabTractByUniqueId(id:String):ProjectTabDocumentTract
    {
        for each (var tabTract:ProjectTabDocumentTract in TractsList)
        {
            if (tabTract.TractRef != null && tabTract.TractRef.UniqueId == id)
                return tabTract;
        }

        return null;
    }
    
    private var _df:DateFormatter;
    private function getDateFormater():DateFormatter
    {
        if (!_df)
        {
            _df = new DateFormatter();
            _df.formatString = "MMM DD YYYY";
        }
        return _df;
    };

}
}