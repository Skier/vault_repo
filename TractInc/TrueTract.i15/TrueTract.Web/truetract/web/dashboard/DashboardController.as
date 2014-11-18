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
    import truetract.web.dashboard.documentList.documentEditor.DocumentEditorView;
    import truetract.web.dashboard.documentPanel.DocumentPanelView;
    import truetract.web.dashboard.groupPanel.GroupPanelView;
    import truetract.web.dashboard.searchPanel.SearchPanelView;
    import truetract.web.dashboard.tractEditor.TractEditorView;
    import truetract.web.services.TrueTractService;
    import truetract.web.util.TokenResponder;
    import truetract.web.wizards.createDrawingWizard.CreateDrawingWizardView;
    
    public class DashboardController
    {
        public function DashboardController()
        {
            createPopupButtonMenu.dataProvider = 
                [ {label: "Document"}, {label: "Drawing"}, {label: "Folder"} ];

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
            view.userGroupsTree.selectedItem = model.myItemsGroup;
            selectGroup(model.myItemsGroup);// ?
            loadUsersGroupList();

            model.folderListMenu.addEventListener("itemClick", folderListMenu_itemClickHandler);
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
                "Unable delete Group")
            );            
        }

        public function showSearchPanel():void
        {
            var searchPanel:SearchPanelView;

            for each (var panel:* in view.panels.getChildren())
            {
                if (panel is SearchPanelView)
                {
                    searchPanel = panel;
                    break;
                }
            }

            if (null == searchPanel) {
                searchPanel = new SearchPanelView();
                view.panels.addChild(searchPanel);
            }

            view.panels.selectedChild = searchPanel;
        }

        private function folderListMenu_itemClickHandler(event:MenuEvent):void
        {
            var group:UserGroup = UserGroup(event.item);
            var token:AsyncToken;

/*             if (view.groupPanel.dashboardNav.selectedChild == view.groupPanel.documentListView)
            {
                var document:Document = Document(view.groupPanel.docDG.selectedItem);
                token = trueTractService.AddDocumentToGroup(group.groupId, document.DocID);

                token.addResponder(new TokenResponder(
                    function (event:ResultEvent):void
                    {
                        group.groupDocumentsList.addItem(document);
                    },
                    "Unable to add document to Group")
                );
            } 
            else 
            {
                var drawing:Tract = Tract(view.groupPanel.drawingsListControl.selectedItem);
                token = trueTractService.AddDrawingToGroup(group.groupId, drawing.TractId);

                token.addResponder(new TokenResponder(
                    function (event:ResultEvent):void
                    {
                        group.groupDrawingsList.addItem(drawing);
                    },
                    "Unable to add Drawing to Group")
                );
            }
 */        }

        public function selectGroup(group:UserGroup):void
        {
            model.selectedGroup = group;

            for each (var panel:* in view.panels.getChildren())
            {
                if (panel is GroupPanelView && panel.group == group)
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
                    
                    ArrayUtil.addRange(model.userGroupList.source, groups);
                },
                "Unable to load Groups List")
            );
            
        }

        public function userGroupName_changeHandler(group:UserGroup):void 
        {
            view.userGroupsTree.editable = false;

            ttService.service.SaveUserGroup(group.groupId, group.groupName);
        }

        private function createPopupButtonMenu_itemClickHandler(event:MenuEvent):void 
        {
            switch (event.label)
            {
                case 'Drawing':
                    addDrawing();
                    break;

                case 'Document':
                    var documentEditor:DocumentEditorView = DocumentEditorView.open(view, true);
                    documentEditor.docum = new Document();
                    documentEditor.addEventListener("commit", 
                        function(event:DynamicEvent):void
                        {
                            var docum:Document = Document(event.document);

                            model.myItemsGroup.groupDocumentsList.addItem(docum);

                            if (!model.selectedGroup.systemGroup) {
                                model.selectedGroup.groupDocumentsList.addItem(docum);
                            }
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
                            model.userGroupList.addItem(newGroup);
                        },
                        "Unable to load Group Items")
                    );
                    break;
            }
        }

        public function openTract(tract:Tract):void 
        {
            view.plotter.controller.openTract(tract);
            model.plotterMode = true;
        }

        public function openDocument(document:Document):void
        {
            var docPanel:DocumentPanelView;

            for each (var panel:* in view.panels.getChildren())
            {
                if (panel is DocumentPanelView && panel.getDocBranchUid() == document.DocBranchUid)
                {
                    docPanel = panel;
                    break;
                }
            }

            if (null == docPanel)
            {
                docPanel = new DocumentPanelView();
                docPanel.setDocument(document);
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

            popup.addEventListener("commit", function ():void {
                openTract(tract);
            });
        }

        public function plotter_closeHandler():void
        {
            model.plotterMode = false;
        }

/*         public function applyDocumentFilter(event:DynamicEvent):void
        {
            var queryTemplate:Document = Document(event.queryTemplate);
            
            var asyncToken:AsyncToken = trueTractService.FindDocumentsByTemplate(queryTemplate);
            asyncToken.addResponder(new TokenResponder(
                function (event:ResultEvent):void
                {
                    model.searchGroup.groupDocuments = event.result as Array;
                    resolveDuplicateConflict(model.searchGroup);
                },
                "Unable to find Documents")
            );
        }

        public function resetDocumentFilter(event:Event):void
        {
            model.searchGroup.groupDocuments = [];
        }

        public function applyDrawingFilter(event:DynamicEvent):void
        {
            var refName:String = String(event.refName);
            
            var asyncToken:AsyncToken = trueTractService.FindDrawingsByTemplate(refName);
            asyncToken.addResponder(new TokenResponder(
                function (event:ResultEvent):void
                {
                    model.searchGroup.groupDrawings = event.result as Array;
                    resolveDuplicateConflict(model.searchGroup);
                },
                "Unable to find Drawings")
            );
        }

        public function resetDrawingFilter(event:Event):void
        {
            model.searchGroup.groupDrawings = [];
        }
 */
        public function app_timeOutHandler():void 
        {
            if (model.plotterMode && view.plotter.plotter.Model != null) 
            {
//                saveTract(model.currentTract);
            }

            view.appController.logOut();
        }

    }
}