package truetract.domain
{
import mx.collections.ArrayCollection;
    
[Bindable]
[RemoteClass(alias="TractInc.TrueTract.Entity.ProjectTabInfo")]
public class ProjectTab
{
    public var ProjectTabId:int;
    public var ProjectId:int;
    public var Name:String;
    public var Description:String;

    private var _documentsList:ArrayCollection = new ArrayCollection();
    public function get DocumentsList():ArrayCollection { return _documentsList; }

    private var _documents:Array;
    public function get Documents():Array { return _documents; }
    public function set Documents(value:Array):void 
    {
        DocumentsList.source = _documents = value;
        
        if (value && value.length > 0)
        {
            for each (var tab:ProjectTabDocument in value)
            {
                tab.ProjectTabRef = this;
            }
        }
    }

    public var TabProject:Project;

    public function addDocument(doc:Document):ProjectTabDocument
    {
        var tabDocument:ProjectTabDocument = new ProjectTabDocument();
        tabDocument.DocumentRef = doc;
        tabDocument.DocumentId = doc.DocID;
        tabDocument.ProjectTabRef = this;
        
        DocumentsList.addItem(tabDocument);

        return tabDocument;
    }

    public function containsDocument(doc:Document):Boolean
    {
        var result:Boolean = false;

        for each (var tabDocument:ProjectTabDocument in DocumentsList){
            if (tabDocument.DocumentRef == doc) {
                result = true;
                break;
            }
        }

        return result;
    }
}
}