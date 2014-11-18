package truetract.web.dashboard
{
import flash.display.DisplayObject;
import flash.events.MouseEvent;

import mx.collections.ArrayCollection;
import mx.controls.Alert;
import mx.controls.List;
import mx.controls.Menu;
import mx.controls.TextInput;
import mx.controls.Tree;
import mx.core.UIComponent;
import mx.events.DragEvent;
import mx.events.DynamicEvent;
import mx.events.ListEvent;
import mx.events.MenuEvent;
import mx.managers.DragManager;
import mx.rpc.AsyncToken;
import mx.rpc.Responder;
import mx.rpc.events.FaultEvent;
import mx.rpc.events.InvokeEvent;
import mx.rpc.events.ResultEvent;
import mx.rpc.remoting.mxml.RemoteObject;

import truetract.domain.*;
import truetract.plotter.utils.ArrayUtil;
import truetract.web.AppModel;
import truetract.web.dashboard.documentPanel.DocumentPanel;
import truetract.web.dashboard.documentPanel.documentEditor.DocumentEditorView;
import truetract.web.dashboard.documentPanel.tractEditor.TractEditorView;
import truetract.web.dashboard.groupPanel.GroupPanel;
import truetract.web.dashboard.projectPanel.ProjectPanel;
import truetract.web.services.TrueTractService;
import truetract.web.util.TokenResponder;
import truetract.web.wizards.createDrawingWizard.CreateDrawingWizardView;
import truetract.web.dashboard.events.DocumentEvent;
import mx.events.CloseEvent;
import truetract.web.services.ProjectService;

public class DashboardController
{
    public var view:DashboardView;

    [Bindable] public var model:DashboardModel = new DashboardModel();

    [Bindable] public var createPopupButtonMenu:Menu = new Menu();

    [Bindable] public var appModel:AppModel = AppModel.getInstance();

    private var ttService:TrueTractService = TrueTractService.getInstance();

    public function init():void
    {
        model.reset();
        view.vs.selectedChild = view.dashboard;
        view.userGroupsTree.validateNow();

        var myDocumentsGroup:UserGroup = model.myDocumentsGroup;

        view.userGroupsTree.selectedItem = myDocumentsGroup;

        selectGroup(myDocumentsGroup);

        loadUsersGroupList();
        loadUsersClientList();
    }

    public function deleteGroupRequest(group:UserGroup):void
    {
        var asyncToken:AsyncToken = ttService.service.DeleteUserGroup(group.groupId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                var groupIndex:Number = model.userGroupList.getItemIndex(group);
                model.userGroupList.removeItemAt(groupIndex);
            },
            "Unable to delete Group")
        );            
    }

    private function addDocumentToGroup(document:Document, group:UserGroup):void
    {
        var token:AsyncToken =
            ttService.service.AddDocumentToGroup(group.groupId, document.DocBranchUid);

        token.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                if (group.isLoaded && !group.groupItemsList.contains(document)) 
                {
                    group.groupItemsList.addItem(document);
                }
            },
            "Unable to add document to Group")
        );
    }

    private function removeDocumentFromGroup(document:Document, group:UserGroup):void
    {
        var token:AsyncToken = ttService.service.RemoveDocumentFromGroup(
            group.groupId, document.DocBranchUid);
        
        token.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                var docIndex:int = group.groupItemsList.getItemIndex(document);
                if (docIndex != -1)
                    group.groupItemsList.removeItemAt(docIndex);
            },
            "Unable to remove Document from Group")
        );
    }

    public function selectGroup(group:UserGroup):void
    {
        model.selectedGroup = null;
        model.selectedGroup = group;

        for each (var panel:* in view.groupPanels.getChildren())
        {
            if (panel is GroupPanel && panel.group == group)
            {
                view.groupPanels.selectedChild = panel;
                break;
            }
        }
    }

    private function loadUsersGroupList():void
    {
        var asyncToken:AsyncToken = ttService.service.GetGroupListByUserId(appModel.user.UserId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                var groups:Array = event.result as Array;
                
                for each (var group:UserGroup in groups)
                {
                    model.userGroupList.addItem(group);
                }
            },
            "Unable to load Groups List")
        );
    }

    private function loadUsersClientList():void
    {
        var asyncToken:AsyncToken = ttService.service.GetClientListByUser(appModel.user.UserId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                model.userClientList = new ArrayCollection(event.result as Array);

                for each (var client:Client in model.userClientList)
                {
                    ProjectService.getInstance().getClientProjectList(client, appModel.user.UserId);
                }
            },
            "Unable to load Client List"
            )
        );
    }

    public function addFolder():void 
    {
        var userId:int = appModel.user.UserId;
        var newGroup:UserGroup = new UserGroup();
        newGroup.groupName = "New Group";
        newGroup.systemGroup = false;
        
        var asyncToken:AsyncToken = ttService.service.CreateUserGroup(newGroup.groupName, userId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                newGroup.groupId = int(event.result);
                newGroup.isLoaded = true;
                model.userGroupList.addItem(newGroup);

                selectGroup(newGroup);

                var newGroupIndex:int = model.userGroupList.length - 1;
                view.userGroupsTree.editable = true;                                    
                view.userGroupsTree.editedItemPosition = {rowIndex: newGroupIndex};
            },
            "Unable to load Group Items")
        );
    }

    public function addDocument():void 
    {
        var documentEditor:DocumentEditorView = DocumentEditorView.open(view, true);
        documentEditor.docum = new Document();
        documentEditor.addEventListener("commit", 
            function(event:DynamicEvent):void
            {
                var request:AsyncToken = ttService.saveDocument(
                    documentEditor.docum, AppModel.getInstance().user.UserId);

                request.addResponder(new TokenResponder(
                    function(re:ResultEvent):void
                    {   
                        var docum:Document = Document(re.result);

                        model.myDocumentsGroup.groupItemsList.addItem(docum);

                        if (!model.selectedGroup.systemGroup)
                        {
                            addDocumentToGroup(docum, model.selectedGroup);
                        }

                        documentEditor.close();
                        openDocumentView(docum);
                    },
                    "Unable to Save document")
                );
            }
        );
    }

    public function addDrawing():void 
    {
        var tract:Tract = new Tract();
        var popup:TractEditorView = TractEditorView.open(view, true);

        popup.tract = tract;
        popup.oneLevelTractsList = model.myDrawingsGroup.groupItemsList;

        popup.addEventListener("commit", 
            function ():void 
            {
                openTract(tract);
            }
       );
    }

    public function applyFilter():void
    {
        model.selectedGroup.isLoaded = false;
        selectGroup(model.selectedGroup);
    }

    public function resetFilter():void
    {
        model.selectedGroup.isLoaded = false;
        selectGroup(model.selectedGroup);
    }

    public function openTractRequestHandler(event:DynamicEvent):void
    {
        var tract:Tract = Tract(event.tract);
        openTract(tract);
    }

    public function openTract(tract:Tract):void 
    {
        view.plotter.controller.openTract(tract);
        model.plotterMode = true;
    }

    public function openDocumentRequestHandler(event:DocumentEvent):void
    {
        openDocumentView(event.document);
    }

    public function removeDocumentFromFolderRequestHandler(event:DynamicEvent):void
    {
        var documentList:Array = event.documentList as Array;
        var group:UserGroup = UserGroup(event.group);
        
        for each (var doc:Document in documentList)
        {
            removeDocumentFromGroup(doc, group);
        }
    }

    public function openDocumentView(docum:Document):DocumentPanel
    {
        var docPanel:DocumentPanel;

        for each (var panel:* in view.groupPanels.getChildren())
        {
            if (panel is DocumentPanel && panel.docum && panel.docum.DocBranchUid == docum.DocBranchUid)
            {
                docPanel = panel;
                break;
            }
        }

        if (null == docPanel)
        {
            docPanel = new DocumentPanel();
            docPanel.docum = docum;
            docPanel.addEventListener("openTractRequest", openTractRequestHandler);

            view.groupPanels.addChild(docPanel);
        }

        view.groupPanels.selectedChild = docPanel;
        return docPanel;
    }

    public function openProjectView(project:Project):ProjectPanel
    {
        var projectPanel:ProjectPanel;

        for each (var panel:* in view.projectPanels.getChildren())
        {
            if (panel is ProjectPanel && panel.project == project)
            {
                projectPanel = panel;
                break;
            }
        }
        
        if (null == projectPanel)
        {
            projectPanel = new ProjectPanel();
            projectPanel.project = project;
            projectPanel.addEventListener("openDocument", openDocumentRequestHandler);

            view.projectPanels.addChild(projectPanel);
            
        }

        view.projectPanels.selectedChild = projectPanel;

        return projectPanel;
    }

    public function userGroupsTree_dragEnterHandler(event:DragEvent):void
    {
        var dragInitiator:UIComponent = UIComponent(event.currentTarget);

        if (event.dragInitiator == view.groupPanel.documentList)
        {
            DragManager.acceptDragDrop(dragInitiator);
        }
    }

    public function userGroupsTree_dragDropHandler(event:DragEvent):void
    {
        var dropIndex:int = view.userGroupsTree.calculateDropIndex(event);
        var selectedGroup:UserGroup = UserGroup(view.userGroupsTree.dataProvider[dropIndex]);

        var documentList:List = List(event.dragInitiator);
        var documents:Array = documentList.selectedItems;

        for each (var doc:Document in documents)
        {
            addDocumentToGroup(doc, selectedGroup);
        }
    }

    public function userGroupsTree_dragOverHandler(event:DragEvent):void
    {
        var dropTarget:Tree = Tree(event.currentTarget);
        var dropIndex:int = dropTarget.calculateDropIndex(event);

        var userGroup:UserGroup;

        if (dropTarget.dataProvider.length > dropIndex)
            userGroup = UserGroup(view.userGroupsTree.dataProvider[dropIndex]);

        dropTarget.showDropFeedback(event);

        if( userGroup && !userGroup.systemGroup ) 
        {
            DragManager.showFeedback(DragManager.COPY);
        } 
        else 
        {
            DragManager.showFeedback(DragManager.NONE);        
        }
    }

    public function userProjectsTree_dragEnterHandler(event:DragEvent):void
    {
        var dragInitiator:UIComponent = UIComponent(event.currentTarget);

        if (event.dragInitiator == view.groupPanel.documentList ||
            (event.dragInitiator == view.userProjectsTree && 
            view.userProjectsTree.selectedItem is ProjectTab))
        {
            DragManager.acceptDragDrop(dragInitiator);
        }
    }

    public function userProjectsTree_dragDropHandler(event:DragEvent):void
    {
        var dropTarget:Tree = Tree(event.currentTarget);
        var dropIndex:int = dropTarget.calculateDropIndex(event);

        var dropIR:* = view.userProjectsTree.indexToItemRenderer(dropIndex);
        var project:Project;

        if (dropIR && dropIR.data) 
        {
            if (event.dragInitiator == view.userProjectsTree)
            {
                var droppedTab:ProjectTab = ProjectTab(dropIR.data);
                var draggedTab:ProjectTab = ProjectTab(view.userProjectsTree.selectedItem);
                project = draggedTab.TabProject;
                
                var draggedTabIndex:int = project.TabsList.getItemIndex(draggedTab);
                var droppedTabIndex:int = project.TabsList.getItemIndex(droppedTab);
                
                project.TabsList.removeItemAt(draggedTabIndex);
                project.TabsList.addItemAt(draggedTab, droppedTabIndex);
            } 
            else 
            {
                var documentList:List = List(event.dragInitiator);
                var documents:Array = documentList.selectedItems;
    
                if (dropIR.data is Project)
                {
                    project = Project(dropIR.data);
                    Alert.show(
                        "Do you want to create new Project Tab and put this document to it ?", 
                        "Project Tab is not specified.", Alert.YES | Alert.NO, null, 
                        function (event:CloseEvent):void
                        {
                            if (event.detail == Alert.YES) 
                            {
                                var projectPanel:ProjectPanel = openProjectView(project);
                                projectPanel.insertDocumentsIntoNewTab(documents);
                            }
                        }
                    );
                } 
                else
                {
                    var projectTab:ProjectTab = ProjectTab(dropIR.data);

                    for each (var doc:Document in documents)
                    {
                        if (!projectTab.containsDocument(doc))                
                            projectTab.addDocument(doc);
                    }

                    var projectPanel:ProjectPanel = openProjectView(projectTab.TabProject);
                    projectPanel.selectProjectTab(projectTab);
                }
            }
        }
        dropTarget.hideDropFeedback(event);
    }

    public function userProjectsTree_dragOverHandler(event:DragEvent):void
    {
        var dropTarget:Tree = Tree(event.currentTarget);
        var dropIndex:int = dropTarget.calculateDropIndex(event);

        var dropIR:* = view.userProjectsTree.indexToItemRenderer(dropIndex);

        if (dropIR && dropIR.data) 
        {
            if (event.dragInitiator == view.userProjectsTree)
            {
                view.userProjectsTree.useExtendedDropFeetback = false;
                if (dropIR.data is ProjectTab)
                {
                    DragManager.showFeedback(DragManager.MOVE);
                } 
                else 
                {
                    DragManager.showFeedback(DragManager.NONE);
                }
            } 
            else
            {
                view.userProjectsTree.useExtendedDropFeetback = true;

                if( dropIR.data is Client) 
                {
                    DragManager.showFeedback(DragManager.NONE);
                } 
                else 
                {
                    DragManager.showFeedback(DragManager.COPY);
                }
            }

            dropTarget.showDropFeedback(event);
        }
    }

    public function userProjectsTree_doubleClickHandler():void 
    {
        var item:* = view.userProjectsTree.selectedItem;

        var project:Project;
        var projectTab:ProjectTab;

        if (item is Client) return;

        if (item is Project) {
            project = Project(item)
        } else {
            project = ProjectTab(item).TabProject;
            projectTab = item;
        }

        var projectPanel:ProjectPanel = openProjectView(project);

        if (projectTab) {
            projectPanel.selectProjectTab(projectTab);
        }
    }

    public function userGroupName_changeHandler(event:ListEvent):void 
    {
        var newGroupName:String = TextInput(event.currentTarget.itemEditorInstance).text;
        var group:UserGroup = UserGroup(view.userGroupsTree.selectedItem);
        
        view.userGroupsTree.editable = false;

        ttService.saveUserGroup(group, newGroupName);
    }

    public function plotter_closeHandler():void
    {
        model.plotterMode = false;
    }

    public function app_timeOutHandler():void 
    {
        if (model.plotterMode && view.plotter.plotter.Model != null) 
        {
            //saveTract(model.currentTract);
        }

        view.appController.logOut();
    }

}
}