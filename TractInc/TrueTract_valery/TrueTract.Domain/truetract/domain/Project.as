package truetract.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="TractInc.TrueTract.Entity.ProjectInfo")]
public class Project
{
	public static const PROJECT_STATUS_ACTIVE:String = "ACTIVE";
	public static const PROJECT_STATUS_COMPLETE:String = "COMPLETE";
	
    public var ProjectId:int;
    public var Name:String;
    public var ShortName:String;
    public var ClientId:int;
    public var Status:String;
    public var Changed:Date;

    public var Attachments:Array;

    private var _tabsList:ArrayCollection = new ArrayCollection();
    public function get TabsList():ArrayCollection { return _tabsList; }

    private var _tabs:Array;
    public function get Tabs():Array { return _tabs; }
    public function set Tabs(value:Array):void 
    {
        TabsList.source = _tabs = value;
        
        if (value && value.length > 0)
        {
            for each (var tab:ProjectTab in value)
            {
                tab.ProjectRef = this;
            }
        }
    }

    public function get children():ArrayCollection
    {
        return TabsList;
    }

    public function addAttachment(attachment:ProjectAttachment):ProjectAttachment
    {
        attachment.ProjectId = ProjectId;
        attachment.ProjectRef = this;

        Attachments.push(attachment);

        return attachment;
    }

    public function deleteAttachment(attachment:ProjectAttachment):void
    {
        var itemIndex:int = Attachments.indexOf(attachment);
        Attachments.splice(itemIndex, 1);
    }

    public function deleteTab(tab:ProjectTab):void
    {
        var itemIndex:int = TabsList.getItemIndex(tab);
        TabsList.removeItemAt(itemIndex);
    }

    public function addTab(tab:ProjectTab):ProjectTab
    {
        tab.ProjectId = ProjectId;
        tab.ProjectRef = this;

        TabsList.addItem(tab);

        return tab;
    }
    
    public function getExistingDocIds(docIds:Array):Array 
    {
    	var result:Array = new Array;
    	
    	if (TabsList) {
    		for each (var tab:ProjectTab in TabsList) {
    			if (tab.DocumentsList) {
    				for each (var tabDoc:ProjectTabDocument in tab.DocumentsList) {
    					for each (var id:int in docIds) {
	    					if (tabDoc.DocumentId == id) {
	    						result.push(id);
	    					}
    					}
    				}
    			}
    		}
    	}
    	
    	return result;
    }

	public function actualizeDocument(newDoc:Document):void 
	{
		for each (var tab:ProjectTab in TabsList) 
		{
			for each (var doc:ProjectTabDocument in tab.DocumentsList) 
			{
				if (doc.DocumentRef.DocBranchUid == newDoc.DocBranchUid) {
					doc.DocumentId = newDoc.DocID;
					doc.DocumentRef = newDoc;
				}
			}
		}
	}
	
	public function createSimpleCopy():Project 
	{
		var result:Project = new Project();
		
		result.ProjectId = ProjectId;
		result.ClientId = ClientId;
		result.Name = Name;
		result.ShortName = ShortName;
		
		return result;
	}

	public function setFieldsValues(project:Project):void 
	{
		ProjectId = project.ProjectId;
		ClientId = project.ClientId;
		Name = project.Name;
		ShortName = project.ShortName;
		Status = project.Status;
    	Changed = project.Changed;

    	Attachments = project.Attachments;
    	Tabs = project.Tabs;
	}

}
}