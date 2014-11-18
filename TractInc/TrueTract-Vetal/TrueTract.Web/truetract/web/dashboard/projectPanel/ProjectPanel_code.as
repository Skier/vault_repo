package truetract.web.dashboard.projectPanel
{
import mx.core.Repeater;

import truetract.domain.Project;
import truetract.domain.ProjectTab;
import truetract.plotter.containers.extendedTabNavigatorClasses.TabPanel;
import truetract.web.dashboard.projectPanel.tabPanel.ProjectTabPanel;
import mx.controls.Button;
import mx.events.FlexEvent;
import mx.controls.CheckBox;
import truetract.web.dashboard.projectPanel.tabPanel.ProjectTabEditView;
import mx.events.DynamicEvent;
import truetract.domain.Document;
import mx.core.Application;

public class ProjectPanel_code extends TabPanel
{
    [Bindable] public var tabPanel:*; //it should be Array
    [Bindable] public var wellPlanPanel:WellPlanSection;
    [Bindable] public var projectMapPanel:CollapsiblePanel;

    [Bindable]
    [Embed(source="/assets/note_edit.png")]
    private var editIcon:Class;

    [Bindable]
    [Embed(source="/assets/map_add.png")]
    private var mapAddIcon:Class;

    [Bindable]
    [Embed(source="/assets/map_edit.png")]
    private var mapEditIcon:Class;

    [Bindable]
    [Embed(source="/assets/map_delete.png")]
    private var mapDeleteIcon:Class;

    public function ProjectPanel_code()
    {
        addEventListener(FlexEvent.CREATION_COMPLETE, creationCompleteHandler);
    }

    private var _project:Project;
    [Bindable] public function get project():Project { return _project; }
    public function set project(value:Project):void
    {
        _project = value;
        loadProject();
    }
        
    private function loadProject():void
    {
        
    }
    
    public function collapseAll(collapse:Boolean):void
    {
        wellPlanPanel.collapsed = collapse;
        projectMapPanel.collapsed = collapse;

        for each (var panel:ProjectTabPanel in tabPanel) 
            panel.collapsed = collapse;
    }

    public function selectProjectTab(tab:ProjectTab):void
    {
        collapseAll(true);

        for each (var panel:ProjectTabPanel in tabPanel) 
        {
            if (panel.projectTab == tab) {
                panel.collapsed = false;
                break;
            }
        }
    }

    public function insertDocumentsIntoNewTab(documents:Array):void
    {
        var tab:ProjectTab = new ProjectTab();
        var popup:ProjectTabEditView = 
            ProjectTabEditView.open(Application(Application.application), true);

        popup.projectTab = tab;
        popup.addEventListener("submit", 
            function(event:DynamicEvent):void
            {
                project.addTab(tab);

                for each (var doc:Document in documents) {
                    tab.addDocument(doc);
                }

                selectProjectTab(tab);
            }
        )
    }

    protected function addNewTab_handler():void
    {
        var tab:ProjectTab = new ProjectTab();
        var popup:ProjectTabEditView = ProjectTabEditView.open(this, true);
        popup.projectTab = tab;
        popup.addEventListener("submit", 
            function(event:DynamicEvent):void {
                project.addTab(tab);
                selectProjectTab(tab);
            }
        );
    }

    private function createProjectMapButtons():void
    {
/*         var addButton:Button = createToolButton(mapAddIcon, "Add Map");
        var deleteButton:Button = createToolButton(mapDeleteIcon, "Remove Map");
        var editButton:Button = createToolButton(mapEditIcon, "Edit Map Info");

        projectMapPanel.addHeaderItem(addButton);
        projectMapPanel.addHeaderItem(deleteButton);
        projectMapPanel.addHeaderItem(editButton);
 */    }

    private function creationCompleteHandler(event:FlexEvent):void
    {
        createProjectMapButtons();
    }
}
}