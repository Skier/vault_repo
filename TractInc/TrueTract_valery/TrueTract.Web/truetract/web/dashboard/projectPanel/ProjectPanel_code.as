package truetract.web.dashboard.projectPanel
{
import mx.controls.Button;
import mx.controls.CheckBox;
import mx.core.Application;
import mx.core.Repeater;
import mx.events.DynamicEvent;
import mx.events.FlexEvent;
import mx.rpc.AsyncToken;
import mx.rpc.Responder;
import mx.rpc.events.FaultEvent;
import mx.rpc.events.ResultEvent;

import truetract.domain.Document;
import truetract.domain.Project;
import truetract.domain.ProjectTab;
import truetract.plotter.containers.extendedTabNavigatorClasses.TabPanel;
import truetract.web.dashboard.projectPanel.tabPanel.ProjectTabEditView;
//import truetract.web.dashboard.projectPanel.tabPanel.ProjectTabPanel;
import truetract.web.services.ProjectService;
import truetract.web.dashboard.events.DocumentEvent;
import truetract.web.AppModel;
import mx.events.CloseEvent;
import mx.controls.Alert;

public class ProjectPanel_code extends TabPanel
{
    [Bindable] public var tabsPanel:ProjectTabsLayout; 
    [Bindable] public var wellPlansPanel:WellPlansSection;
    [Bindable] public var projectMapsPanel:ProjectMapsSection;
    [Bindable] public var contactMapPanel:ContactMapSection;
    
    public function ProjectPanel_code()
    {
        addEventListener(FlexEvent.CREATION_COMPLETE, creationCompleteHandler);
    }

    private var projectService:ProjectService = ProjectService.getInstance();

    private var _project:Project;
    [Bindable] public function get project():Project { return _project; }
    public function set project(value:Project):void
    {
        _project = value;
        loadProject();
    }
        
    public function collapseAll(collapse:Boolean):void
    {
        wellPlansPanel.collapsed = collapse;
        projectMapsPanel.collapsed = collapse;
        contactMapPanel.collapsed = collapse;

//        for each (var panel:ProjectTabPanel in tabPanel) 
//            panel.collapsed = collapse;
    }
/* 
    public function selectProjectTab(tab:ProjectTab):ProjectTabPanel
    {
        var projectTabPanel:ProjectTabPanel;

        collapseAll(true);

        for each (var panel:ProjectTabPanel in tabPanel) 
        {
            if (panel.projectTab == tab)
            {
                panel.collapsed = false;
                projectTabPanel = panel;
                break;
            }
        }

        return projectTabPanel;
    }
 */
 
 	public function openProjectTab(projectTab:ProjectTab):void 
 	{
 		tabsPanel.openProjectTab(projectTab);
 	}
 
    public function createProjectTab(tabDocuments:Array = null):void
    {
        var projectTab:ProjectTab = new ProjectTab();
        projectTab.ProjectId = project.ProjectId;

        var popup:ProjectTabEditView = 
            ProjectTabEditView.open(Application(Application.application), true);

        popup.projectTab = projectTab;
        popup.addEventListener("commit", 
            function(event:Event):void
            {
                var asynToken:AsyncToken = projectService.addProjectTab(projectTab);
                asynToken.addResponder(new Responder(
                    function (event:ResultEvent):void
                    {
                        popup.close();
                        //selectProjectTab(projectTab);
                        openProjectTab(projectTab);

                        if (tabDocuments)
                        {
                            projectService.addTabDocuments(projectTab, tabDocuments);
                        }
                    },
                    function (event:FaultEvent):void { }
                ));
            }
        );
    }
    
    protected function addDocumentHandler(event:DocumentEvent):void 
    {
    	var docTarget:* = event.docTarget;
    	event.preventDefault();
    	dispatchEvent(new DocumentEvent(DocumentEvent.ADD_DOCUMENT, null, docTarget));
    }
    
    protected function setProjectComplete(isComplete:Boolean):void 
    {
    	var statusId:int = isComplete ? Project.PROJECT_STATUS_COMPLETE : Project.PROJECT_STATUS_ACTIVE;
		var status:String = isComplete ? 'COMPLETE' : 'ACTIVE';

        Alert.show(
            "Are you really want to change Project status to " + status + " ?", 
            "Change Project status.", Alert.YES | Alert.NO, null, 
            function (event:CloseEvent):void
            {
                if (event.detail == Alert.YES) 
                {
			    	projectService.changeProjectStatus(project, statusId, AppModel.getInstance().user.Login);
                }
            }
        );
    }

    private function loadProject():void
    {
        
    }

    private function creationCompleteHandler(event:FlexEvent):void
    {
    }
    
}
}