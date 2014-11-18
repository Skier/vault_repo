package truetract.web.services
{
import mx.core.Application;
import mx.rpc.AbstractService;
import mx.rpc.AsyncToken;
import mx.rpc.Responder;
import mx.rpc.events.FaultEvent;
import mx.rpc.events.InvokeEvent;
import mx.rpc.events.ResultEvent;
import mx.rpc.remoting.mxml.RemoteObject;

import truetract.domain.*;
import truetract.web.util.TokenResponder;
import truetract.web.dashboard.events.DocumentEvent;
import mx.collections.ArrayCollection;
import mx.events.CollectionEvent;

public class ProjectService
{
    
    //--------------------------------------------------------------------------
    //
    //  Singleton stuff
    //
    //--------------------------------------------------------------------------
    
    private static var _instance : ProjectService;
    public static function getInstance() : ProjectService
    {
        if ( _instance == null )
            _instance = new ProjectService(new SingletonEnforcer());
            
        return _instance;
    }

    public function ProjectService(singletonEnforcer:SingletonEnforcer) 
    {
        clean();

        Application.application.addEventListener("logout", 
            function(event:Event):void { clean(); });
    }

    //--------------------------------------------------------------------------
    //
    //  Class members
    //
    //--------------------------------------------------------------------------

    [Bindable] public var serviceIsBusy:Boolean = false;

    private var _service:RemoteObject;

    private var docService:DocumentService = DocumentService.getInstance();

    private var projectsHash:Object;

    public function clean():void
    {
        projectsHash = new Object();
    }

    private function get service():RemoteObject
    {
        if (_service == null)
        {
            _service = new RemoteObject( "GenericDestination" );
            _service.source = "TractInc.TrueTract.Project";
            _service.showBusyCursor = true;
            _service.addEventListener(InvokeEvent.INVOKE, 
                function(event:InvokeEvent):void { serviceIsBusy = true });
            _service.addEventListener(ResultEvent.RESULT,
                function(event:ResultEvent):void { serviceIsBusy = false });
            _service.addEventListener(FaultEvent.FAULT,
                function(event:FaultEvent):void { serviceIsBusy = false });
        }

        return _service;
    }
    
    public function getLocalProject(projectId:int):Project 
    {
    	return projectsHash[projectId] as Project;
    }

    public function getClientProjectList(client:Client, userId:int):AsyncToken
    {
        var asyncToken:AsyncToken = service.GetProjectsByClientAndUser(client.ClientId, userId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                client.Projects = event.result as Array;
                for each (var project:Project in client.Projects)
                {
                    projectsHash[project.ProjectId] = project;

                    loadProject(project);
                }
            },
            "Unable to load Project List for client [" + client.Name + "]"
            )
        );

        return asyncToken;
    }

    public function getProjectTabs(project:Project):AsyncToken
    {
        var asyncToken:AsyncToken = service.GetProjectTabs(project.ProjectId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                project.Tabs = event.result as Array;
                for each (var tab:ProjectTab in project.Tabs)
                {
                    tab.ProjectRef = project;
                    loadProjectTabDocuments(tab);
                    addDocListener(tab);
                }
            },
            "Unable to load Project Tab List for project [" + project.ShortName + "]"
            )
        );
        
        return asyncToken;
    }

	private function addDocListener(tab:ProjectTab):void 
	{
		Application.application.addEventListener(DocumentEvent.SAVE_DOCUMENT, 
			function(event:DocumentEvent):void {
				
				var newDoc:Document = event.document;
				
				for each (var tabDoc:ProjectTabDocument in tab.DocumentsList) 
				{
					if (tabDoc.DocumentId == newDoc.PreviousVersion) 
					{
						updateTabDocument(tabDoc);
					}
				}
			}
		);
	}
	
	private function updateTabDocument(tabDoc:ProjectTabDocument):void 
	{
        var asyncToken:AsyncToken = docService.getDocument(tabDoc.DocumentId);
        asyncToken.addResponder(new Responder(
            function (event:ResultEvent):void
            {
                var doc:Document = event.result as Document;
                if (doc) 
                {
                    tabDoc.DocumentRef = doc;
                }
            },
            function (event:FaultEvent):void {}
        ));
	}

    private function loadProjectTabDocuments(projectTab:ProjectTab):void
    {
        for each (var tabDocument:ProjectTabDocument in projectTab.DocumentsList)
        {
            if (tabDocument.IsActive)
            {
            	projectTab.ActiveTabDocument = tabDocument;
            }

            tabDocument.DocumentRef = docService.getStoredDocument(tabDocument.DocumentId);

            if (!tabDocument.DocumentRef)
            {
            	getProjectTabDocument(tabDocument);
            } else 
            {
                docService.loadDocumentReferences(tabDocument.DocumentRef);
            }
        }
    }
    
    private function getProjectTabDocument(tabDocument:ProjectTabDocument):void 
    {
        var asyncToken:AsyncToken = docService.getDocument(tabDocument.DocumentId);
        asyncToken.addResponder(new Responder(
            function (event:ResultEvent):void
            {
            	var doc:Document = event.result as Document;
                if (doc) 
                {
                    tabDocument.DocumentRef = doc;
                    docService.loadDocumentReferences(doc);
                }
            },
            function (event:FaultEvent):void {}
        ));
    }

    public function getProjectAttachments(project:Project):AsyncToken
    {
        var asyncToken:AsyncToken = service.GetProjectAttachments(project.ProjectId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                project.Attachments = event.result as Array;
                for each (var attachment:ProjectAttachment in project.Attachments)
                {
                    attachment.ProjectRef = project;
                }
            },
            "Unable to load Project Tab List for project [" + project.ShortName + "]"
            )
        );

        return asyncToken;
    }

    public function addProjectTab(projectTab:ProjectTab):AsyncToken
    {
        var asyncToken:AsyncToken = service.AddTab(projectTab);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                projectTab.setMemento(ProjectTab(event.result).getMemento());

                var project:Project = projectsHash[projectTab.ProjectId];
                project.addTab(projectTab);
            },
            "Unable to add Project Tab"
            )
        );

        return asyncToken;
    }

    public function updateProjectTab(projectTab:ProjectTab):AsyncToken
    {
        var asyncToken:AsyncToken = service.UpdateTab(projectTab);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                projectTab.setMemento(ProjectTab(event.result).getMemento());
            },
            "Unable to update Project Tab"
            )
        );

        return asyncToken;
    }

    public function deleteProjectTab(projectTab:ProjectTab):AsyncToken
    {
        var asyncToken:AsyncToken = service.DeleteTab(projectTab);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                var project:Project = projectsHash[projectTab.ProjectId];
                project.deleteTab(projectTab);
            },
            "Unable to delete Project Tab"
            )
        );

        return asyncToken;
    }

    public function addProjectTabContact(projectTab:ProjectTab, projectTabContact:ProjectTabContact):AsyncToken
    {
        var asyncToken:AsyncToken = service.AddTabContact(projectTabContact);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                projectTabContact.setMemento(ProjectTabContact(event.result).getMemento());

                projectTab.addContact(projectTabContact);
            },
            "Unable to add Project Tab Contact"
            )
        );

        return asyncToken;
    }

    public function updateProjectTabContact(projectTabContact:ProjectTabContact):AsyncToken
    {
        var asyncToken:AsyncToken = service.UpdateTabContact(projectTabContact);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                projectTabContact.setMemento(ProjectTabContact(event.result).getMemento());
            },
            "Unable to update Project Tab"
            )
        );

        return asyncToken;
    }

    public function deleteProjectTabContacts(projectTab:ProjectTab, contacts:Array):AsyncToken
    {
        var asyncToken:AsyncToken = service.DeleteTabContacts(contacts);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
            	for each (var contact:ProjectTabContact in contacts) 
            	{
            		projectTab.deleteContact(contact);
            	}
            },
            "Unable to delete Project Tab"
            )
        );

        return asyncToken;
    }

    public function addTabDocuments(projectTab:ProjectTab, documents:Array):AsyncToken
    {
        var documentIds:Array = [];

        for each (var docObject:* in documents)
        {
            var document:Document;

            if (docObject is Document)
                document = Document(docObject);
            else if (docObject is ProjectTabDocument)
                document = ProjectTabDocument(docObject).DocumentRef;

            if (document && !projectTab.containsDocument(document))
                documentIds.push(document.DocID); 
        }

        var asyncToken:AsyncToken = service.AddTabDocuments(projectTab, documentIds);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                var tabDocuments:Array = event.result as Array;
                for each (var tabDocument:ProjectTabDocument in tabDocuments)
                {
                    //all referenced documents objects must be already loaded in DocumentService
                    tabDocument.DocumentRef = docService.getStoredDocument(tabDocument.DocumentId);
                    projectTab.addDocument(tabDocument);
                }
            },
            "Unable to add documents to Tab"
            )
        );

        return asyncToken;
    }

    public function updateProjectTabDocument(projectTabDocument:ProjectTabDocument):AsyncToken
    {
        var asyncToken:AsyncToken = service.UpdateTabDocument(projectTabDocument);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                projectTabDocument.setMemento(ProjectTabDocument(event.result).getMemento());
            },
            "Unable to update Project Tab Document"
            )
        );

        return asyncToken;
    }

    public function actualizeDocument(project:Project, docum:Document):AsyncToken
    {
        var asyncToken:AsyncToken = service.ActualizeDocument(project.ProjectId, docum.DocID);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
            	var actualDoc:Document = event.result as Document;
            	
            	project.actualizeDocument(actualDoc);
            	
            	docService.getDocument(actualDoc.DocID);
            },
            "Unable to actualize Project Tab Document"
            )
        );

        return asyncToken;
    }

    public function activateProjectTabDocument(projectTab:ProjectTab, projectTabDocument:ProjectTabDocument):AsyncToken
    {
    	throw new Error("Do not realized yet!");
    }

    public function deleteProjectTabDocuments(projectTab:ProjectTab, documents:Array):AsyncToken
    {
        var asyncToken:AsyncToken = service.DeleteTabDocuments(documents);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                for each (var document:Document in documents)
                {
                    projectTab.deleteDocument(document);
                }
            },
            "Unable to remove Tab Documents"
            )
        );

        return asyncToken;
    }

    public function addAttachment(attachment:ProjectAttachment, uploadId:String):AsyncToken
    {
        var asyncToken:AsyncToken = service.AddAttachment(attachment, uploadId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                attachment.setMemento(ProjectAttachment(event.result).getMemento());

                var project:Project = projectsHash[attachment.ProjectId];
                project.addAttachment(attachment);
            },
            "Unable to add Project Attachment"
            )
        );

        return asyncToken;
    }

    public function updateAttachment(attachment:ProjectAttachment, uploadId:String):AsyncToken
    {
        var asyncToken:AsyncToken = service.UpdateAttachment(attachment, uploadId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                attachment.setMemento(ProjectAttachment(event.result).getMemento());
            },
            "Unable to update Project Attachment"
            )
        );

        return asyncToken;
    }

    public function deleteAttachment(attachment:ProjectAttachment):AsyncToken
    {
        var asyncToken:AsyncToken = service.DeleteAttachment(attachment);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                var project:Project = projectsHash[attachment.ProjectId];
                project.deleteAttachment(attachment);
            },
            "Unable to delete Project Attachment"
            )
        );

        return asyncToken;
    }
    
    public function saveOrderedTabs(project:Project):AsyncToken 
    {
    	var serverTabs:Array = new Array();
    	var tabs:Array = project.Tabs;
    	
    	if (tabs == null || tabs.length == 0) 
    		return null;
   		
    	for (var i:int = 0; i < tabs.length; i++) 
    	{
    		var tab:ProjectTab = ProjectTab(tabs[i]).clone();
    		tab.TabOrder = i;
    		serverTabs.push(tab);
    	}
    	
        var asyncToken:AsyncToken = service.SaveOrderedTabs(serverTabs);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
		    	for (var i:int = 0; i < tabs.length; i++) 
		    	{
		    		ProjectTab(tabs[i]).TabOrder = i;
		    	}
            },
            "Unable to save Tab's order"
            )
        );

        return asyncToken;
    }
    
    public function exportTabToExcel(tabId:int):AsyncToken 
    {
        return service.GetProjectTabExcelFileUrl(tabId);
    }
    
    public function loadFullProject(project:Project):AsyncToken
    {
        var asyncToken:AsyncToken = service.LoadFullProject(project.ProjectId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
				project = event.result as Project;
				
				for each (var tab:ProjectTab in project.TabsList)
				{
					tab.updateActiveTabDocument();
				}
				
            },
            "Unable to load full Project"
            )
        );

        return asyncToken;
    }
    
    public function changeProjectStatus(project:Project, newStatus:int, changedBy:String):void 
    {
/* to do: fix me    	
        var asyncToken:AsyncToken = service.ChangeProjectStatus(project.ProjectId, newStatus, changedBy);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
				var result:Project = event.result as Project;
				project.ProjectStatusId = result.ProjectStatusId;
            },
            "Unable to change Projects status"
            )
        );
*/        
    }
    
    public function getCurrentProjects(clientId:int):ArrayCollection
    {
    	var projects:ArrayCollection = new ArrayCollection();
    	
        var asyncToken:AsyncToken = service.GetOpenProjectsByClient(clientId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
            	var result:Array = event.result as Array;

            	for each (var project:Project in result) 
            	{
            		var localProject:Project = projectsHash[project.ProjectId];
            		
            		if (localProject == null) 
            		{
	            		projectsHash[project.ProjectId] = project;
            		} else 
            		{
            			project = projectsHash[project.ProjectId];
            		}
            		
            		projects.addItem(projectsHash[project.ProjectId]);
            	}
            	
            	projects.dispatchEvent(new CollectionEvent(CollectionEvent.COLLECTION_CHANGE));
            },
            "Unable to get all open Projects"
            )
        );

        return projects;
    }
    
    public function getLastWeekProjects(clientId:int):ArrayCollection
    {
    	var projects:ArrayCollection = new ArrayCollection();
    	
        var asyncToken:AsyncToken = service.GetProjectsClosedLastWeekByClient(clientId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
            	var result:Array = event.result as Array;

            	for each (var project:Project in result) 
            	{
            		var localProject:Project = projectsHash[project.ProjectId];
            		
            		if (localProject == null) 
            		{
	            		projectsHash[project.ProjectId] = project;
            		} else 
            		{
            			project = projectsHash[project.ProjectId];
            		}
            		
            		projects.addItem(projectsHash[project.ProjectId]);
            	}
            	
            	projects.dispatchEvent(new CollectionEvent(CollectionEvent.COLLECTION_CHANGE));
            },
            "Unable to get Projects, closed last week"
            )
        );

        return projects;
    }
    
    public function getAllClosedProjects(clientId:int):ArrayCollection
    {
    	var projects:ArrayCollection = new ArrayCollection();
    	
        var asyncToken:AsyncToken = service.GetAllClosedProjectsByClient(clientId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
            	var result:Array = event.result as Array;

            	for each (var project:Project in result) 
            	{
            		var localProject:Project = projectsHash[project.ProjectId];
            		
            		if (localProject == null) 
            		{
	            		projectsHash[project.ProjectId] = project;
            		} else 
            		{
            			project = projectsHash[project.ProjectId];
            		}
            		
            		projects.addItem(projectsHash[project.ProjectId]);
            	}
            	
            	projects.dispatchEvent(new CollectionEvent(CollectionEvent.COLLECTION_CHANGE));
            },
            "Unable to get all closed Project"
            )
        );

        return projects;
    }
    
    private function loadProject(project:Project):void
    {
        getProjectTabs(project);
        getProjectAttachments(project);
    }
    
}
}
class SingletonEnforcer {}