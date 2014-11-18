package truetract.web.dashboard.groupPanel
{
import flash.events.Event;

import mx.containers.VBox;
import mx.controls.List;
import mx.controls.Menu;
import mx.events.DynamicEvent;
import mx.events.MenuEvent;
import mx.rpc.AsyncToken;
import mx.rpc.Responder;
import mx.rpc.events.ResultEvent;
import mx.rpc.remoting.mxml.RemoteObject;

import truetract.domain.UserGroup;
import truetract.plotter.containers.extendedTabNavigatorClasses.TabPanel;
import truetract.plotter.domain.Document;
import truetract.plotter.domain.Tract;
import truetract.web.AppModel;
import truetract.web.dashboard.DashboardModel;
import truetract.web.dashboard.documentPanel.tractEditor.TractEditorView;
import truetract.web.dashboard.groupPanel.filter.FilterPanel;
import truetract.web.services.TrueTractService;
import truetract.web.util.TokenResponder;
import truetract.domain.DocumentFilter;

[Event(name="openTractRequest", type="mx.events.DynamicEvent")]
[Event(name="openDocumentRequest", type="mx.events.DynamicEvent")]
[Event(name="removeDocumentFromFolderRequest", type="mx.events.DynamicEvent")]

public class GroupPanel_code extends TabPanel
{
    [Bindable] public var documentList:List;
    [Bindable] public var folderListMenu:Menu;

    private var _group:UserGroup;
    [Bindable] public function get group():UserGroup { return _group; }
    public function set group(value:UserGroup):void
    {
        _group = value;
        loadGroup(group);
    }

    private var ttService:TrueTractService = TrueTractService.getInstance();

    protected function isFilterSpecified(filter:DocumentFilter):Boolean
    {
        return filter.isSpecified();
    }

    private function loadGroup(g:UserGroup):void
    {
        if (g && !g.isLoaded)
        {
            var userId:int = AppModel.getInstance().user.UserId;
            var asyncToken:AsyncToken;
            var loadGroupResponder:TokenResponder = new TokenResponder(
                function (event:ResultEvent):void
                {
                    var loadedGroup:UserGroup = UserGroup(event.result);
                    g.groupDocuments = loadedGroup.groupDocuments;
                    g.groupDrawings = loadedGroup.groupDrawings;
                    g.isLoaded = true;
                },
                "Unable to load User Items");

            switch (g.groupName)
            {
                case DashboardModel.ALL_ITEMS_GROUP_NAME:
                    if (g.documentFilter.isSpecified()) 
                    {
                        asyncToken = ttService.getDocuments(g.documentFilter);
                        asyncToken.addResponder(new TokenResponder(
                            function (event:ResultEvent):void {
                                g.groupDocuments = event.result as Array;
                            }, 'Unable to load Documents' ));
                    }
                    break;

                case DashboardModel.MY_ITEMS_GROUP_NAME:
                    asyncToken = ttService.loadUserItemsGroup(userId, g.documentFilter);
                    asyncToken.addResponder(loadGroupResponder);
                    break;

                case DashboardModel.RECENT_ITEMS_GROUP_NAME:
                    asyncToken = ttService.loadUserRecentItemsGroup(userId, g.documentFilter);
                    asyncToken.addResponder(loadGroupResponder);
                    break;

                default:
                    asyncToken = ttService.loadUserGroup(g.groupId, userId, g.documentFilter);
                    asyncToken.addResponder(loadGroupResponder);
                    break;
            }
        }
    }

    protected function showHandler():void
    {
        if (group) 
            group.applyFilter();
    }

    protected function resetFilterRequestHandler():void
    {
        group.documentFilter.reset();
        group.groupDocuments = [];
        group.isLoaded = false;

        //Binding initiation
        var filter:DocumentFilter = group.documentFilter;
        group.documentFilter = null;
        group.documentFilter = filter;

        loadGroup(group);
    }

    protected function applyFilter(filter:DocumentFilter):void
    {
        group.documentFilter = null;
        group.documentFilter = filter;
        group.groupDocuments = [];
        group.isLoaded = false;

        loadGroup(group);
    }

    protected function filterButton_clickHandler():void
    {
        var filterView:FilterPanel = FilterPanel.open(this, true);
        filterView.filter = group.documentFilter;
        filterView.keyFieldsRequired = group.groupName == DashboardModel.ALL_ITEMS_GROUP_NAME;

        filterView.addEventListener("applyFilter", 
            function (event:Event):void { 
                applyFilter(filterView.filter);
            }
        );
        
        filterView.addEventListener("resetFilter", 
            function (event:Event):void { resetFilterRequestHandler(); }
        );
    }

    protected function detailView_addTractHandler():void
    {
        var document:Document = Document(documentList.selectedItem);

        var tract:Tract = new Tract();
        tract.ParentDocument = document;
        tract.DocId = document.DocID;

        var popup:TractEditorView = TractEditorView.open(this, true);
        popup.tract = tract;
        popup.oneLevelTractsList = document.TractsList;
        popup.addEventListener("commit", function ():void {
            openTract(tract);
        });
    }

    protected function openTract(tract:Tract):void
    {
        var event:DynamicEvent = new DynamicEvent("openTractRequest");
        event.tract = tract;
        dispatchEvent(event);
    }

    protected function removeFromFolderButton_clickHandler():void
    {
        var event:DynamicEvent = new DynamicEvent("removeDocumentFromFolderRequest");
        event.document = Document(documentList.selectedItem);
        event.group = group;

        dispatchEvent(event);
    }

    protected function detailView_openTractHandler(event:DynamicEvent):void
    {
        var tract:Tract = Tract(event.tract);
        openTract(tract);
    }

    protected function documentList_changeHandler():void
    {
        var document:Document = Document(documentList.selectedItem);
        
        if (!document.IsLoaded) {
            
            var asyncToken:AsyncToken = ttService.service.GetDocumentTractList(document.DocID);
            asyncToken.addResponder(new TokenResponder(
                function (event:ResultEvent):void 
                {
                    document.Tracts = event.result as Array;
                    document.TractsList.refresh();
                    document.IsLoaded = true;
                },
                "Unable to load document tract List"
            ));
        }
    }

    protected function documentList_dblClickHandler():void
    {
        trace("Double Click");

        var doc:Document = Document(documentList.selectedItem);

        var event:DynamicEvent = new DynamicEvent("openDocumentRequest");
        event.document = doc;

        dispatchEvent(event);
    }

}
}