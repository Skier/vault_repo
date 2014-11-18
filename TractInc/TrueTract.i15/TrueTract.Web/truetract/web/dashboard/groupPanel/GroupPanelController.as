package truetract.web.dashboard.groupPanel
{
    import mx.events.DynamicEvent;
    import mx.events.MenuEvent;
    import mx.rpc.AsyncToken;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.remoting.mxml.RemoteObject;
    
    import truetract.plotter.domain.Document;
    import truetract.plotter.domain.Tract;
    import truetract.web.AppModel;
    import truetract.web.dashboard.DashboardModel;
    import truetract.web.dashboard.UserGroup;
    import truetract.web.services.TrueTractService;
    import truetract.web.util.TokenResponder;
    
    public class GroupPanelController
    {
        
        [Bindable] public var view:GroupPanelView;

        private var ttService:TrueTractService = TrueTractService.getInstance();

        public function groupChangedHandler():void
        {
            var group:UserGroup = view.group;

            if (group && !group.isLoaded)
            {
                var userId:int = AppModel.getInstance().user.UserId;
                var asyncToken:AsyncToken;
                var loadGroupResponder:TokenResponder = new TokenResponder(
                    function (event:ResultEvent):void
                    {
                        var loadedGroup:UserGroup = UserGroup(event.result);
                        group.groupDocuments = loadedGroup.groupDocuments;
                        group.groupDrawings = loadedGroup.groupDrawings;
                        group.isLoaded = true;
                    },
                    "Unable to load User Items");

                switch (group.groupName)
                {
                    case DashboardModel.MY_ITEMS_GROUP:
                        asyncToken = ttService.loadUserItemsGroup(userId);
                        asyncToken.addResponder(loadGroupResponder);
                        break;
    
                    case DashboardModel.RECENT_ITEMS_GROUP:
                        asyncToken = ttService.loadUserRecentItemsGroup(userId);
                        asyncToken.addResponder(loadGroupResponder);
                        break;

                    default:
                        asyncToken = ttService.loadUserGroup(group.groupId, userId);
                        asyncToken.addResponder(loadGroupResponder);
                        break;
                }
            }
        }

        public function docDG_doubleClickHandler():void
        {
            var doc:Document = Document(view.docDG.selectedItem);

            var event:DynamicEvent = new DynamicEvent("openDocumentRequest");
            event.document = doc;

            view.dispatchEvent(event);
        }

        private function folderListMenu_itemClickHandler(event:MenuEvent):void
        {
            var group:UserGroup = UserGroup(event.item);
            var token:AsyncToken;

/*             if (view.dashboardNav.selectedChild == view.documentListView)
            {
                var document:Document = Document(view.documentListView.docDG.selectedItem);
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
                var drawing:Tract = Tract(view.drawingListView.drawingsListControl.selectedItem);
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
        

    }
}