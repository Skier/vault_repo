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
import truetract.web.services.DocumentService;
import mx.core.IUIComponent;
//import truetract.web.dashboard.projectPanel.tabPanel.ProjectTabPanel;
import truetract.plotter.containers.ExtendedTabNavigator;
import truetract.web.wizards.addDocumentWizard.AddDocumentWizardView;
import truetract.web.dashboard.events.DocumentReferenceEvent;
import truetract.web.dashboard.documentPanel.documentDetail.DocumentDetailView;
import truetract.plotter.containers.extendedTabNavigatorClasses.TabPanel;
import truetract.web.dashboard.projectPanel.tabPanel.ProjectTabDetailView;

public class DashboardController
{
    public var view:DashboardView;

    [Bindable] public var model:DashboardModel = new DashboardModel();

    [Bindable] public var createPopupButtonMenu:Menu = new Menu();

    [Bindable] public var appModel:AppModel = AppModel.getInstance();

    private var ttService:TrueTractService = TrueTractService.getInstance();
    private var documentService:DocumentService = DocumentService.getInstance();
    private var projectService:ProjectService = ProjectService.getInstance();

    public function init():void
    {
        model.reset();
        view.vs.selectedChild = view.dashboard;
        view.userGroupsTree.validateNow();

        var myDocumentsGroup:UserGroup = model.myDocumentsGroup;

        view.userGroupsTree.selectedItem = myDocumentsGroup;

		resetTabNavigator();
        selectGroup(myDocumentsGroup);

        loadUsersGroupList();
        loadUsersClientList();
    }

    public function deleteGroupRequest(group:UserGroup):void
    {
        var asyncToken:AsyncToken = ttService.deleteUserGroup(group.groupId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                var groupIndex:Number = model.userGroupList.getItemIndex(group);
                model.userGroupList.removeItemAt(groupIndex);
                
                closeGroupPanel(group);
                
            },
            "Unable to delete Group")
        );            
    }

	private function resetTabNavigator():void 
	{
        for each (var panel:* in view.panels.getChildren())
        {
            if (TabPanel(panel).id != "tabSummary")
            {
            	var idx:int = view.panels.getChildIndex(panel);
            	if (idx > -1) {
	            	view.panels.removeChildAt(idx);
            	}
            }
        }
	}

	private function closeGroupPanel(group:UserGroup):void 
	{
        for each (var panel:* in view.panels.getChildren())
        {
            if (panel is GroupPanel && panel.group == group)
            {
            	var idx:int = view.panels.getChildIndex(panel);
            	view.panels.removeChildAt(idx);
            }
        }
	}

    private function addDocumentToGroup(document:Document, group:UserGroup):void
    {
        var token:AsyncToken = ttService.addDocumentToGroup(group.groupId, document.DocBranchUid);

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
        var token:AsyncToken = ttService.removeDocumentFromGroup(
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
//        model.selectedGroup = null;
//        model.selectedGroup = group;

        openGroupView(group);
    }

    private function loadUsersGroupList():void
    {
        var asyncToken:AsyncToken = ttService.getGroupListByUser(appModel.user.UserId);
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
        var asyncToken:AsyncToken = ttService.getClientListByUser(appModel.user.UserId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                model.userClientList = new ArrayCollection(event.result as Array);
                
                if (model.userClientList.length > 0) 
                {
                	view.setProjectsVisible(true);
                } else 
                {
                	view.setProjectsVisible(false);
                }

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
        newGroup.filter = new DocumentsFilter();

        var asyncToken:AsyncToken = ttService.createUserGroup(newGroup.groupName, userId);
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

    public function addDocumentToGroupRequestHandler():void 
    {
        if (view.panels.selectedChild is GroupPanel)
        {
            selectDocumentForAdding(view.panels.selectedChild as GroupPanel);
        } else 
        {
        	return;
        }
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

    public function refreshGroup(group:UserGroup):void
    {
        group.isLoaded = false;
        openGroupView(group).group = group;
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
        var panel:DocumentPanel = openDocumentView(event.document);
        
        if (event.docTarget && event.docTarget is ProjectTabDetailView && !ProjectTabDetailView(event.docTarget).readOnly) 
        {
        	panel.docum.referencedProject = ProjectTabDetailView(event.docTarget).projectTab.ProjectRef;
        } else 
        {
        	panel.docum.referencedProject = null;
        }
        
        if (event.docTarget && event.docTarget is DocumentReference) 
        {
        	panel.docum.parentReference = DocumentReference(event.docTarget);
        } else 
        {
        	panel.docum.parentReference = null;
        }
        
        if (event.docEditorMode != null) 
        {
        	panel.setEditorMode(event.docEditorMode);
        }
    }

    public function addDocumentToProjectRequestHandler(event:DocumentEvent):void
    {
		selectDocumentForAdding(event.docTarget);
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

        for each (var panel:* in view.panels.getChildren())
        {
            if (panel is DocumentPanel && panel.docum && panel.docum.DocID == docum.DocID)
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
            docPanel.addEventListener("openDocument", openDocumentRequestHandler);

            view.panels.addChild(docPanel);
        }

        view.panels.selectedChild = docPanel;
        return docPanel;
    }

    public function openProjectView(project:Project):ProjectPanel
    {
        var projectPanel:ProjectPanel;

        for each (var panel:* in view.panels.getChildren())
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
            projectPanel.addEventListener(DocumentEvent.ADD_DOCUMENT, addDocumentToProjectRequestHandler);

            view.panels.addChild(projectPanel);
            
        }

        view.panels.selectedChild = projectPanel;

        return projectPanel;
    }

    public function openGroupView(group:UserGroup):GroupPanel
    {
        var groupPanel:GroupPanel;

        for each (var panel:* in view.panels.getChildren())
        {
            if (panel is GroupPanel && panel.group == group)
            {
                groupPanel = panel;
                break;
            }
        }

        if (null == groupPanel)
        {
            groupPanel = new GroupPanel();
            groupPanel.group = group;
            groupPanel.addEventListener("openDocument", openDocumentRequestHandler);
            groupPanel.addEventListener("openTractRequest", openTractRequestHandler);
            groupPanel.addEventListener("removeDocumentFromFolderRequest", removeDocumentFromFolderRequestHandler);

            view.panels.addChild(groupPanel);
        }

        view.panels.selectedChild = groupPanel;

        return groupPanel;
    }
    
    public function userGroupsTree_dragEnterHandler(event:DragEvent):void
    {
        var dragObj:Array= event.dragSource.dataForFormat("items") as Array;

        if (dragObj && dragObj.length > 0 && dragObj[0] is Document)
        {
            DragManager.acceptDragDrop(IUIComponent(event.currentTarget));
        }
    }

    public function userGroupsTree_dragDropHandler(event:DragEvent):void
    {
        var dropIndex:int = view.userGroupsTree.calculateDropIndex(event);
        var selectedGroup:UserGroup = UserGroup(view.userGroupsTree.dataProvider[dropIndex]);

        var dragObj:Array= event.dragSource.dataForFormat("items") as Array;

        if (dragObj && dragObj.length > 0 && dragObj[0] is Document)
        {
            for each (var doc:Document in dragObj)
            {
                addDocumentToGroup(doc, selectedGroup);
            }
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

    public function userGroupsTree_doubleClickHandler():void
    {
        if (!view.userGroupsTree.selectedItem.systemGroup)
        {
            view.userGroupsTree.editable = true; 
            view.userGroupsTree.editedItemPosition = {rowIndex: view.userGroupsTree.selectedIndex};
        }
    }
    
    public function userGroupsTree_changeHandler():void
    {
        selectGroup(UserGroup(view.userGroupsTree.selectedItem));
    }

    public function userProjectsTree_dragEnterHandler(event:DragEvent):void
    {
        var dataFormat:String = event.dragSource.formats[0];
        var dragObj:Array= event.dragSource.dataForFormat(dataFormat) as Array;

        if (dragObj && dragObj.length > 0 && 
            (dragObj[0] is Document || dragObj[0] is ProjectTabDocument || dragObj[0] is ProjectTab))
        {
            DragManager.acceptDragDrop(IUIComponent(event.currentTarget));
        }
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

    public function userProjectsTree_dragDropHandler(event:DragEvent):void
    {
        var dropTarget:Tree = Tree(event.currentTarget);
        var dropIndex:int = dropTarget.calculateDropIndex(event);

        var dropIR:* = view.userProjectsTree.indexToItemRenderer(dropIndex);
        var project:Project;

        var dataFormat:String = event.dragSource.formats[0];
        var dragObj:Array= event.dragSource.dataForFormat(dataFormat) as Array;

        dropTarget.hideDropFeedback(event);

        if (!dragObj || dragObj.length == 0) return;
        if (!dropIR || !dropIR.data) return;

        if (event.dragInitiator == view.userProjectsTree)
        {
            var droppedTab:ProjectTab = ProjectTab(dropIR.data);
            var draggedTab:ProjectTab = ProjectTab(view.userProjectsTree.selectedItem);
            project = draggedTab.ProjectRef;
            
            var draggedTabIndex:int = project.TabsList.getItemIndex(draggedTab);
            var droppedTabIndex:int = project.TabsList.getItemIndex(droppedTab);
            
            project.TabsList.removeItemAt(draggedTabIndex);
            project.TabsList.addItemAt(draggedTab, droppedTabIndex);
        } 
        else 
        {
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
                            projectPanel.createProjectTab(dragObj);
                        }
                    }
                );
            } 
            else
            {
                var projectTab:ProjectTab = ProjectTab(dropIR.data);

                projectService.addTabDocuments(projectTab, dragObj);

                openProjectView(projectTab.ProjectRef).openProjectTab(projectTab);
            }
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
            project = ProjectTab(item).ProjectRef;
            projectTab = item;
        }

        var projectPanel:ProjectPanel = openProjectView(project);

        if (projectTab) {
            projectPanel.openProjectTab(projectTab);
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

    public function selectDocumentForAdding(target:*):void 
    {
		var addWizard:AddDocumentWizardView = AddDocumentWizardView.open(view, true);
		
		if (target && target is ProjectTabDetailView) 
		{
			addWizard.runsheetEntry = new ProjectTabDocument();
		}
		
        addWizard.addEventListener("documentCreated", 
            function(event:DynamicEvent):void
            {
            	var doc:Document = addWizard.docum;
            	
                var request:AsyncToken = documentService.saveDocument(
                    doc, AppModel.getInstance().user.UserId);

                request.addResponder(new TokenResponder(
                    function(re:ResultEvent):void
                    {  
                        var docum:Document = Document(re.result);
                        doc = docum;
                        
                        addDocToTarget(docum, target, addWizard.runsheetEntry);

                        addWizard.close();
						
                    },
                    "Unable to Save document")
                );
            });

        addWizard.addEventListener("documentSelected", 
            function(event:DynamicEvent):void
            {
				var exists:Boolean = false;
				var docum:Document = addWizard.docum;

				if (target && target is ProjectTabDetailView) 
				{
					var project:Project = ProjectTabDetailView(target).projectTab.ProjectRef;
					
					for each (var tab:ProjectTab in project.TabsList) 
					{
						for each (var doc:ProjectTabDocument in tab.DocumentsList) 
						{
							if (doc.DocumentRef.DocBranchUid == docum.DocBranchUid) {
								exists = true;
								break;
							}
						}
					}
				}
				
				if (!exists) 
				{
                    addDocToTarget(docum, target, addWizard.runsheetEntry);

	                addWizard.close();
				} else 
				{
	                Alert.show("Document already exists in the project. Old Documents will be replaced. Are you agree?", 
                       "Replace Document", Alert.YES | Alert.NO, null, 
                       function (event:CloseEvent):void 
                       {
                            if (event.detail == Alert.YES)
                            {
		                        addDocToTarget(docum, target, addWizard.runsheetEntry);

				                addWizard.close();
                            }
                        });
				}
            	
            });

    }

    private function addDocToTarget(docum:Document, target:*, runsheetEntry:ProjectTabDocument = null):void
    {
    	if (docum == null) return;
    	
        if (!model.myDocumentsGroup.groupItemsList.contains(docum)) 
        {
	        model.myDocumentsGroup.groupItemsList.addItem(docum);
        }
		
		if (target && target is GroupPanel) 
		{
			var group:UserGroup = GroupPanel(target).group;

			if (!group.systemGroup) 
			{
	            addDocumentToGroup(docum, group);
			}

			openDocumentView(docum);
		}
		
		if (target && target is ProjectTabDetailView) 
		{
			var projectService:ProjectService = ProjectService.getInstance();
			var panel:ProjectTabDetailView = target as ProjectTabDetailView;
			
            var request:AsyncToken = projectService.addTabDocuments(panel.projectTab, ([docum]));

            request.addResponder(new TokenResponder(
                function(re:ResultEvent):void
                {   
	                var tabDocuments:Array = re.result as Array;
	                
	                if (tabDocuments.length > 0)
	                {
						var tabEntry:ProjectTabDocument = tabDocuments[0];
						
	                    if (runsheetEntry != null) 
	                    {
	                    	tabEntry.Description = runsheetEntry.Description;
	                    	tabEntry.Remarks = runsheetEntry.Remarks;

	                    	projectService.updateProjectTabDocument(tabEntry);
	                    }
	                }
	                
	                Alert.show("Do you want to open document for editing?", 
                       "Open Document", Alert.YES | Alert.NO, null, 
                       function (event:CloseEvent):void 
                       {
                            if (event.detail == Alert.YES)
                            {
                            	openDocumentView(docum);
                            }
                        });
	                
                }, null)
            );
			
		}
		
		if (target && target is DocumentDetailView) 
		{
			var parentDoc:Document = DocumentDetailView(target).docum;
			
			if (parentDoc.DocID == docum.DocID) 
			{
				Alert.show("You can not add reference to itself !");
				return;
			}

			if (!parentDoc.existsReference(docum)) 
			{
				var ref:DocumentReference = new DocumentReference();
				ref.DocumentId = parentDoc.DocID;
				ref.ReferenceId = docum.DocID;

	            documentService.addReference(ref);
			} else 
			{
				Alert.show("Reference to this document alredy exists !");
				return;
			}
		}
		
    }
}
}