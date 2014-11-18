package truetract.domain
{
import mx.collections.ArrayCollection;
import mx.collections.Sort;
import mx.collections.SortField;

import truetract.domain.mementos.ProjectTabMemento;
import mx.controls.Label;
    
[Bindable]
[RemoteClass(alias="TractInc.TrueTract.Entity.ProjectTabInfo")]
public class ProjectTab implements IMemento
{
    public var ProjectTabId:int;
    public var ProjectId:int;
    public var Name:String;
    public var Label:String;
    public var TabOrder:int;

    public var ProjectRef:Project;

    public var ActiveTabDocument:ProjectTabDocument;

    public function ProjectTab()
    {
         _documentsList = new ArrayCollection();
         _contactsList = new ArrayCollection();

         var sortByDateFiled:SortField = new SortField("DateFiledSortValue", false, true, true);
         var sort:Sort = new Sort();

         sort.fields = [sortByDateFiled];
         _documentsList.sort = sort;
         _documentsList.refresh();
    }

    private var _documentsList:ArrayCollection;
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

    private var _contactsList:ArrayCollection;
    public function get ContactsList():ArrayCollection { return _contactsList; }

	private var _contacts:Array;
	public function get Contacts():Array { return _contacts; }
	public function set Contacts(value:Array):void 
	{
		ContactsList.source = _contacts = value;
		
		if (value && value.length > 0) 
		{
			for each (var contact:ProjectTabContact in value) 
			{
				contact.ProjectTabRef = this;
			}
		}
	}
	
	public function get instrument():String 
	{
		return (ActiveTabDocument != null) ? ActiveTabDocument.DocumentTypeName : "";
	}

	public function get dateSigned():String 
	{
		return (ActiveTabDocument != null) ? ActiveTabDocument.DateSigned : "";
	}

	public function get dateFiled():String 
	{
		return (ActiveTabDocument != null) ? ActiveTabDocument.DateFiledDisplayValue : "";
	}

	public function get sellerName():String 
	{
		return (ActiveTabDocument != null) ? ActiveTabDocument.SellerName : "";
	}

	public function get buyerName():String 
	{
		return (ActiveTabDocument != null) ? ActiveTabDocument.BuyerName : "";
	}

    public function addDocument(tabDocument:ProjectTabDocument):ProjectTabDocument
    {
    	var exists:Boolean = false;

        tabDocument.ProjectTabRef = this;
        
        var project:Project = ProjectRef;
        var i:int;
        var tabDoc:ProjectTabDocument;
        
    	for each (var tab:ProjectTab in project.TabsList)
    	{
	    	for (i = 0; i < tab.DocumentsList.length; i++) 
	    	{
	    		tabDoc = tab.DocumentsList[i];
	    		
	    		if (tabDocument.DocumentRef.DocBranchUid == tabDoc.DocumentRef.DocBranchUid) 
	    		{
	    			ProjectTabDocument(tab.DocumentsList[i]).DocumentRef = tabDocument.DocumentRef;
	    			
	    			if (tab.ProjectTabId == ProjectTabId) 
	    				exists = true;
	    		}
	    	}
	    	
    	}

    	if (!exists) 
    	{
	        DocumentsList.addItem(tabDocument);
    	}

        return tabDocument;
    }

    public function deleteDocument(tabDocument:ProjectTabDocument):void
    {
        for (var i:int = 0; i < DocumentsList.length; i++)
        {
            var tabDoc:ProjectTabDocument = ProjectTabDocument(DocumentsList[i]);
            
            if (tabDoc.DocumentId == tabDocument.DocumentId)
            {
                DocumentsList.removeItemAt(i);
                DocumentsList.refresh();
                break;
            }
        }
    }

    public function setActiveDocument(tabDocument:ProjectTabDocument):void
    {
        for each (var item:ProjectTabDocument in DocumentsList)
        {
        	if (item.IsActive) 
        	{
    		    ActiveTabDocument = item;
    		    return;
	       	}
        }

        ActiveTabDocument = null;
    }

    public function containsDocument(doc:Document):Boolean
    {
        var result:Boolean = false;

        for each (var tabDocument:ProjectTabDocument in DocumentsList)
        {
            if (tabDocument.DocumentRef == doc) 
            {
                result = true;
                break;
            }
        }

        return result;
    }
    
    public function addContact(tabContact:ProjectTabContact):ProjectTabContact
    {
        tabContact.ProjectTabRef = this;

        ContactsList.addItem(tabContact);

        return tabContact;
    }

    public function deleteContact(tabContact:ProjectTabContact):void
    {
    	var idx:int = ContactsList.getItemIndex(tabContact);
    	
    	if (idx != -1) 
    	{
    		ContactsList.removeItemAt(idx);
    		ContactsList.refresh();
    	}
    	
    }

    public function getMemento():Object
    {
        var memento:ProjectTabMemento = new ProjectTabMemento();

        memento.projectTabId = ProjectTabId;
        memento.projectId = ProjectId;
        memento.name = Name;
        memento.label = Label;
        memento.tabOrder = TabOrder;

        return memento;
    }

    public function setMemento(value:Object):void
    {
        var memento:ProjectTabMemento = ProjectTabMemento(value);

        ProjectTabId = memento.projectTabId;
        ProjectId = memento.projectId;
        Name = memento.name;
        Label = memento.label;
        TabOrder = memento.tabOrder;
    }    

	public function clone():ProjectTab 
	{
		var result:ProjectTab = new ProjectTab();
		
		result.ProjectTabId = ProjectTabId;
		result.ProjectId = ProjectId;
		result.Name = Name;
		result.Label = Label;
		result.TabOrder = TabOrder;
		
		return result;
	}
	
	public function updateActiveTabDocument():void 
	{
		for each (var tabDoc:ProjectTabDocument in DocumentsList) 
		{
			if (tabDoc.IsActive) 
			{
				ActiveTabDocument = tabDoc;
				return;
			}
		}
		
		ActiveTabDocument = null;
	}
}
}