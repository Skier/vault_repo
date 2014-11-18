package truetract.web.dashboard
{
import flash.display.DisplayObject;
import flash.events.MouseEvent;

import mx.controls.Alert;
import mx.controls.Menu;
import mx.events.DynamicEvent;
import mx.events.MenuEvent;
import mx.rpc.AsyncToken;
import mx.rpc.Responder;
import mx.rpc.events.FaultEvent;
import mx.rpc.events.InvokeEvent;
import mx.rpc.events.ResultEvent;
import mx.rpc.remoting.mxml.RemoteObject;

import truetract.plotter.domain.Document;
import truetract.plotter.domain.Tract;
import truetract.plotter.utils.ArrayUtil;
import truetract.web.AppModel;
import truetract.web.dashboard.documentPanel.documentEditor.DocumentEditorView;
import truetract.web.dashboard.documentPanel.tractEditor.TractEditorView;
import truetract.web.dashboard.groupPanel.GroupPanel;
import truetract.web.services.TrueTractService;
import truetract.web.util.TokenResponder;
import truetract.web.wizards.createDrawingWizard.CreateDrawingWizardView;
import mx.controls.TextInput;
import mx.events.ListEvent;
import truetract.web.dashboard.documentPanel.DocumentPanel;
import truetract.domain.UserGroup;

public class DashboardController
{
    public function DashboardController()
    {
        createPopupButtonMenu.dataProvider = 
            [ {label: "Document"}, {label: "Folder"} ];

        createPopupButtonMenu.addEventListener("itemClick", 
            createPopupButtonMenu_itemClickHandler);
    }

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

        view.userGroupsTree.expandItem(model.allItemsGroup, true);
        view.userGroupsTree.selectedItem = model.myItemsGroup;

        selectGroup(model.myItemsGroup);
        loadUsersGroupList();

        model.folderListMenu.addEventListener("itemClick", folderListMenu_itemClickHandler);
    }

    public function deleteGroupRequest(group:UserGroup):void
    {
        var asyncToken:AsyncToken = ttService.service.DeleteUserGroup(group.groupId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                var groupIndex:Number = model.allItemsGroup.children.getItemIndex(group);
                model.allItemsGroup.children.removeItemAt(groupIndex);
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
                if (group.isLoaded && !group.groupDocumentsList.contains(document)) 
                {
                    group.groupDocumentsList.addItem(document);
                }
            },
            "Unable to add document to Group")
        );
    }

    public function selectGroup(group:UserGroup):void
    {
        model.selectedGroup = null;
        model.selectedGroup = group;

        for each (var panel:* in view.panels.getChildren())
        {
            if (panel is GroupPanel && panel.group == group)
            {
                view.panels.selectedChild = panel;
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
                
                ArrayUtil.addRange(model.allItemsGroup.children.source, groups);
            },
            "Unable to load Groups List")
        );
        
    }

    public function userGroupName_changeHandler(event:ListEvent):void 
    {
        var newGroupName:String = TextInput(event.currentTarget.itemEditorInstance).text;
        var group:UserGroup = UserGroup(view.userGroupsTree.selectedItem);
        
        view.userGroupsTree.editable = false;

        ttService.saveUserGroup(group, newGroupName);
    }

    private function folderListMenu_itemClickHandler(event:MenuEvent):void
    {
        var group:UserGroup = UserGroup(event.item);
        var document:Document = Document(view.groupPanel.documentList.selectedItem);

        addDocumentToGroup(document, group);
    }

    private function createPopupButtonMenu_itemClickHandler(event:MenuEvent):void 
    {
        switch (event.label)
        {
            case 'Document':
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
    
                                model.myItemsGroup.groupDocumentsList.addItem(docum);

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
                break;

            case 'Folder':
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
                        model.allItemsGroup.children.addItem(newGroup);
                    },
                    "Unable to load Group Items")
                );
                break;
        }
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

    public function openDocumentRequestHandler(event:DynamicEvent):void
    {
        var document:Document = Document(event.document);
        openDocumentView(document);
    }

    public function removeDocumentFromFolderRequestHandler(event:DynamicEvent):void
    {
        var document:Document = Document(event.document);
        var group:UserGroup = UserGroup(event.group);
        
        var token:AsyncToken = ttService.service.RemoveDocumentFromGroup(
            group.groupId, document.DocBranchUid);
        
        token.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                var docIndex:int = group.groupDocumentsList.getItemIndex(document);
                if (docIndex != -1)
                    group.groupDocumentsList.removeItemAt(docIndex);
            },
            "Unable to remove Document from Group")
        );
    }

    public function openDocumentView(document:Document):void
    {
        var docPanel:DocumentPanel;

        for each (var panel:* in view.panels.getChildren())
        {
            if (panel is DocumentPanel && panel.getDocBranchUid() == document.DocBranchUid)
            {
                docPanel = panel;
                break;
            }
        }

        if (null == docPanel)
        {
            docPanel = new DocumentPanel();
            docPanel.docum = document;
            docPanel.addEventListener("openTractRequest", openTractRequestHandler);

            view.panels.addChild(docPanel);
        }

        view.panels.selectedChild = docPanel;
    }
    
    public function addDrawing():void
    {
        var tract:Tract = new Tract();
        var popup:TractEditorView = TractEditorView.open(view, true);

        popup.tract = tract;
        popup.oneLevelTractsList = model.myItemsGroup.groupDrawingsList;

        popup.addEventListener("commit", 
            function ():void 
            {
                openTract(tract);
            }
       );
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