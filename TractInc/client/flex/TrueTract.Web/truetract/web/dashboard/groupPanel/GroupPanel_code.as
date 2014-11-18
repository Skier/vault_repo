package truetract.web.dashboard.groupPanel
{
import flash.events.Event;
import flash.events.MouseEvent;
import flash.display.DisplayObject;

import mx.core.Application;
import mx.containers.VBox;
import mx.controls.Alert;
import mx.controls.DataGrid;
import mx.controls.List;
import mx.controls.Menu;
import mx.events.DragEvent;
import mx.events.DynamicEvent;
import mx.events.MenuEvent;
import mx.managers.DragManager;
import mx.managers.PopUpManager;
import mx.rpc.AsyncToken;
import mx.rpc.Responder;
import mx.rpc.events.ResultEvent;
import mx.rpc.remoting.mxml.RemoteObject;

import truetract.domain.*;
import truetract.plotter.containers.extendedTabNavigatorClasses.TabPanel;
import truetract.web.AppModel;
import truetract.web.dashboard.DashboardModel;
import truetract.web.dashboard.documentPanel.tractEditor.TractEditorView;
import truetract.web.dashboard.groupPanel.filter.DocumentFilterPanel;
import truetract.web.dashboard.groupPanel.filter.DrawingsFilterPanel;
import truetract.web.dashboard.groupPanel.filter.FilterPanel;
import truetract.web.services.TrueTractService;
import truetract.web.util.TokenResponder;
import truetract.web.dashboard.events.DocumentEvent;
import truetract.web.services.DocumentService;

[Event(name="openTractRequest", type="mx.events.DynamicEvent")]
[Event(name="openDocument", type="truetract.web.dashboard.events.DocumentEvent")]
[Event(name="removeDocumentFromFolderRequest", type="mx.events.DynamicEvent")]

public class GroupPanel_code extends TabPanel
{
    [Bindable] public var appModel:AppModel = AppModel.getInstance();

    [Bindable] public var documentList:List;
    [Bindable] public var drawingDG:DataGrid;
    [Bindable] public var folderListMenu:Menu;

    [Bindable] public var documentMode:Boolean = true;

    private var _group:UserGroup;
    [Bindable] public function get group():UserGroup { return _group; }
    public function set group(value:UserGroup):void
    {
        _group = value;
        getGroupItems(group);
        
        if (group)
            documentMode = (group.groupName != DashboardModel.MY_DRAWINGS_GROUP_NAME);
    }

    private var ttService:TrueTractService = TrueTractService.getInstance();
    private var documentService:DocumentService = DocumentService.getInstance();

    protected function isFilterSpecified(filter:IItemsFilter):Boolean
    {
        return filter && filter.isSpecified();
    }

    private function getGroupItems(g:UserGroup):void
    {
        if (!g) return;

        if (g.isLoaded) 
        {
            g.applyFilter();
        }
        else
        {
            var userId:int = AppModel.getInstance().user.UserId;
            var asyncToken:AsyncToken;
            var loadGroupItemsResponder:TokenResponder = new TokenResponder(
                function (event:ResultEvent):void
                {
                    g.groupItems = event.result as Array;
                    g.isLoaded = true;
                },
                "Unable to load User Items");

            switch (g.groupName)
            {
                case DashboardModel.ALL_DOCUMENTS_GROUP_NAME:
                    if (!g.filter.isSpecified()) return;

                    asyncToken = documentService.getDocuments(g.filter);
                    break;

                case DashboardModel.MY_DOCUMENTS_GROUP_NAME:
                    asyncToken = documentService.getUserDocuments(userId, g.filter);
                    break;

                case DashboardModel.MY_DRAWINGS_GROUP_NAME:
                    asyncToken = documentService.getUserDrawings(userId, g.filter);
                    break;

                case DashboardModel.RECENT_DOCUMENTS_GROUP_NAME:
                    asyncToken = documentService.getUserRecentDocuments(userId, g.filter);
                    break;

                default:
                    asyncToken = documentService.getGroupDocuments(g.groupId, userId, g.filter);
                    break;
            }

            asyncToken.addResponder(loadGroupItemsResponder);
            
        }
    }

    protected function showHandler():void
    {
        if (group) 
            group.applyFilter();
    }

    protected function resetFilterRequestHandler():void
    {
        group.filter.reset();
        group.groupItems = [];
        group.isLoaded = false;

        //Binding initiation
        var filter:IItemsFilter = group.filter;
        group.filter = null;
        group.filter = filter;

        getGroupItems(group);
    }

    protected function applyFilter(filter:IItemsFilter):void
    {
        group.filter = null;
        group.filter = filter;
        group.groupItems = [];
        group.isLoaded = false;

        getGroupItems(group);
    }

    protected function filterButton_clickHandler():void
    {
        var filterView:FilterPanel;
        
        if (group is UserDocumentsGroup)
        {
            filterView = new DocumentFilterPanel();

            DocumentFilterPanel(filterView).keyFieldsRequired = 
                group.groupName == DashboardModel.ALL_DOCUMENTS_GROUP_NAME;
        } 
        else 
        {
            filterView = new DrawingsFilterPanel();
        }

        filterView.filter = group.filter;

        filterView.addEventListener("applyFilter", 
            function (event:Event):void { 
                applyFilter(filterView.filter);
            }
        );
        
        filterView.addEventListener("resetFilter", 
            function (event:Event):void { resetFilterRequestHandler(); }
        );

        PopUpManager.addPopUp(filterView, parent, true);
        PopUpManager.centerPopUp(filterView);
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
            saveTract(tract);
        });
    }

    public function saveTract(tract:Tract):AsyncToken
    {
        var isTractNew:Boolean = tract.TractId == 0;

        if (isTractNew)
            tract.CreatedBy = appModel.user.UserId;

        var asyncToken:AsyncToken;
        
        if (tract.ParentDocument) 
        {
            if (tract.TractId == 0) {
                tract.ParentDocument.TractsList.addItem(tract);
            }
            
            asyncToken = DocumentService.getInstance().saveDocument(
                tract.ParentDocument, appModel.user.UserId);
    
            asyncToken.addResponder(new TokenResponder(
                function(event:ResultEvent):void 
                {
                    var doc:Document = event.result as Document;
                    
                    for each (var t:Tract in doc.TractsList) 
                    {
                        if (tract.RefName == t.RefName) 
                        {
                            t = tract;
                        }
                    }
                    doc.TractsList.refresh();
                    tract.ParentDocument.recalculateTractsCount();
                    
                }, "Unable to Save Tract"));
        } 

        return asyncToken;
    }

    protected function openSelectedItem():void
    {
        if (documentMode)
            openDocument(Document(documentList.selectedItem));
        else 
            openTract(Tract(drawingDG.selectedItem));
    }

    protected function deleteSelectedItem():void
    {
        if (documentMode)
        {
            ;
        }
        else
        {
            deleteDrawing(Tract(drawingDG.selectedItem));
        }
    }

    private function deleteDrawing(tract:Tract):void
    {
        var asyncToken:AsyncToken = documentService.deleteTract(tract, AppModel.getInstance().user.UserId);

        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void 
            {
                var itemIndex:int = group.groupItemsList.getItemIndex(tract);
                group.groupItemsList.removeItemAt(itemIndex);
            },
            "Unable to delete Drawing"
        ));
    }

    protected function openTract(tract:Tract):void
    {
/*
        var event:DynamicEvent = new DynamicEvent("openTractRequest");
        event.tract = tract;
        dispatchEvent(event);
*/
        var document:Document = Document(documentList.selectedItem);
        var popup:TractEditorView = TractEditorView.open(this, true);
        popup.tract = tract;
        popup.oneLevelTractsList = document.TractsList;
        popup.addEventListener("commit", function ():void {
            saveTract(tract);
        });
    }

    protected function openDocument(doc:Document):void
    {
        dispatchEvent(new DocumentEvent(DocumentEvent.OPEN_DOCUMENT, doc));
    }

    protected function removeFromFolderButton_clickHandler():void
    {
        var event:DynamicEvent = new DynamicEvent("removeDocumentFromFolderRequest");
        event.documentList = documentList.selectedItems;
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
        var doc:Document = Document(documentList.selectedItem);
        
 
        if (doc && !doc.IsLoaded) {

            var asyncToken:AsyncToken = documentService.loadDocumentReferences(doc);
            asyncToken.addResponder(new TokenResponder(
                function (event:ResultEvent):void 
                {
                    doc.IsLoaded = true;
                },
                "Unable to load document detail"
            ));

/* 
            var asyncToken:AsyncToken = documentService.getDocumentTractList(doc.DocID);
            asyncToken.addResponder(new TokenResponder(
                function (event:ResultEvent):void 
                {
                    doc.Tracts = event.result as Array;
                    doc.TractsList.refresh();
                    doc.IsLoaded = true;
                },
                "Unable to load document tract List"
            ));
 */
        }
    }

}
}