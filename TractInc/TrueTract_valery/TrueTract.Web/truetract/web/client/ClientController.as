package truetract.web.client
{
import flash.display.DisplayObject;
import flash.events.MouseEvent;
import truetract.web.AppModel;
import mx.rpc.AsyncToken;
import truetract.web.services.ProjectService;
import mx.rpc.Responder;
import mx.rpc.events.ResultEvent;
import mx.rpc.events.FaultEvent;
import truetract.web.client.ClientModel;
import truetract.domain.Project;
import truetract.domain.Document;
import truetract.web.services.DocumentService;
import mx.controls.Alert;
import mx.collections.ArrayCollection;
import mx.managers.HistoryManager;
import truetract.plotter.containers.dockerClasses.DockableToolBar;
import truetract.web.services.TrueTractService;

public class ClientController
{
    public var view:ClientView;

    [Bindable] public var model:ClientModel = new ClientModel();

    [Bindable] public var appModel:AppModel = AppModel.getInstance();

    private var projectService:ProjectService = ProjectService.getInstance();
    private var documentService:DocumentService = DocumentService.getInstance();
    private var ttService:TrueTractService = TrueTractService.getInstance();

    public function init():void
    {
    	model.reset();
    	
    	model.projects = projectService.getAllProjects(appModel.user.ClientId);
    	
    	setClientWorkflowState(ClientModel.WORKFLOW_STATE_SUMMARY);
    }

    public function openDocument(docum:Document):void 
    {
    	if (docum == null)
    		return;
    	
    	model.historyList.removeAll();
    	model.currentProject = docum.referencedProject;

    	setClientWorkflowState(ClientModel.WORKFLOW_STATE_DOCUMENT);
    	
    	loadToCurrentDocument(docum);
    }
    
    public function openReference(docum:Document):void 
    {
    	if (docum == null)
    		return;

    	model.historyList.addItem(model.currentDocument);
//    	model.currentProject = null;
    	
    	setClientWorkflowState(ClientModel.WORKFLOW_STATE_DOCUMENT);

    	loadToCurrentDocument(docum);
    }
    
    public function openHistoryDocument(docum:Document):void 
    {
    	if (docum == null)
    		return;

		var lastItem:Document;
			
		do
		{
	    	lastItem = model.historyList.getItemAt(model.historyList.length - 1) as Document;
	    	model.historyList.removeItemAt(model.historyList.length - 1);
		} while (model.historyList.length > 0 && lastItem != docum )
    	
    	setClientWorkflowState(ClientModel.WORKFLOW_STATE_DOCUMENT);

    	loadToCurrentDocument(docum);
    }
    
    private function loadToCurrentDocument(docum:Document):void 
    {
    	if (docum == null)
    	{
    		return;
    		model.currentDocument = null;
    	}
    	
    	documentService.loadDocumentReferences(docum).addResponder(
    		new Responder(
    			function (event:ResultEvent):void 
    			{
			    	model.currentDocument = event.result as Document;
    			}, 
    			function (event:FaultEvent):void 
    			{
    				model.currentDocument = null;
    			}
    		)
    	);
    }
    
    public function openProject(project:Project):void 
    {
    	if (project == null)
    	{
    		Alert("Can't open project (project is null)");
    		return;
    	}
    	
    	if (view.viewProjects.selectedProject != project) 
    	{
    		view.viewProjects.selectedProject = project;
    		view.viewProjects.loadSelectedProject();
    		view.viewProjects.singleProjectMode = true;
    	}

    	setClientWorkflowState(ClientModel.WORKFLOW_STATE_PROJECT);
    }
    
    public function searchByClient(searchString:String):void 
    {
    	if (searchString.length < 3) 
    	{
    		Alert.show("Search string must have more 2 symbols ");
    	}
    	
    	model.searchResult = ttService.searchByClient(searchString, appModel.user.ClientId);
    	model.searchString = searchString;
    	
    	setClientWorkflowState(ClientModel.WORKFLOW_STATE_SEARCH);
    }
    
    private function setClientWorkflowState(state:int):void {

        switch (state)
        {
            case ClientModel.WORKFLOW_STATE_SUMMARY :
            	if (view.vsHubPart.selectedChild != view.viewSummary) {
                    view.vsHubPart.selectedChild = view.viewSummary;
            	}
                break;
                                                            
            case ClientModel.WORKFLOW_STATE_PROJECT :
            	if (view.vsHubPart.selectedChild != view.vsProjects) {
                    view.vsHubPart.selectedChild = view.vsProjects;
            	}
            	if (view.vsProjects.selectedChild != view.viewProjects) {
                    view.vsProjects.selectedChild = view.viewProjects;
            	}
                break;
                                                            
            case ClientModel.WORKFLOW_STATE_DOCUMENT :
            	if (view.vsHubPart.selectedChild != view.vsProjects) {
                    view.vsHubPart.selectedChild = view.vsProjects;
            	}
            	if (view.vsProjects.selectedChild != view.viewDocuments) {
                    view.vsProjects.selectedChild = view.viewDocuments;
            	}
                break;
                                                            
            case ClientModel.WORKFLOW_STATE_SEARCH :
            	if (view.vsHubPart.selectedChild != view.viewSearch) {
                    view.vsHubPart.selectedChild = view.viewSearch;
            	}
                break;
                                                            
            default :
                throw new Error("Workflow state is invalid");
        }
    }        

}
}