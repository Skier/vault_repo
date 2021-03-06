package truetract.web.dashboard.documentPanel
{
import mx.collections.ArrayCollection;
import mx.collections.Sort;
import mx.collections.SortField;
import mx.controls.Alert;
import mx.controls.DataGrid;
import mx.events.DynamicEvent;
import mx.rpc.AsyncToken;
import mx.rpc.Responder;
import mx.rpc.events.FaultEvent;
import mx.rpc.events.ResultEvent;

import truetract.domain.Document;
import truetract.domain.DocumentAttachment;
import truetract.domain.Tract;
import truetract.plotter.containers.extendedTabNavigatorClasses.TabPanel;
import truetract.web.AppModel;
import truetract.web.dashboard.documentPanel.tractEditor.TractEditorView;
import truetract.web.services.TrueTractService;
import truetract.web.util.TokenResponder;
import truetract.web.services.DocumentService;
import mx.core.Application;
import flash.display.DisplayObject;
import truetract.web.dashboard.events.DocumentEvent;
import truetract.domain.DocumentReference;
import mx.events.CloseEvent;
import truetract.web.dashboard.projectPanel.tabPanel.ProjectTabDetailView;
import truetract.web.wizards.steps.DocumentLeaseView;
import truetract.web.dashboard.events.DocumentReferenceEvent;
import truetract.web.dashboard.DocumentController;
import truetract.web.dashboard.documentPanel.documentDetail.DocumentDetailView;
import truetract.web.dashboard.plotter.PlotterController;

[Event(name="openTractRequest", type="mx.events.DynamicEvent")]
[Event(name="openAttachmentRequest", type="mx.events.DynamicEvent")]

public class DocumentPanel_code extends TabPanel
{
    [Bindable] public var appModel:AppModel = AppModel.getInstance();

    [Bindable] public var docEditor:DocumentFieldsEditor;
    [Bindable] public var leasePanel:DocumentLeaseView;
    [Bindable] public var revisionsDG:DataGrid;
    [Bindable] public var docController:DocumentController;
    [Bindable] public var detailView:DocumentDetailView;

    [Bindable] protected var documRevisions:ArrayCollection;

    private var _docum:Document;
    [Bindable] public function get docum():Document { return _docum; }
    public function set docum(value:Document):void
    {
        _docum = value;

        loadRevisions();

        loadTractList(docum);
        
        loadReferences(docum);
    }
    
    public function setEditorMode(mode:String):void 
    {
        callLater(setMode, [mode]);
    }
    
    private function setMode(mode:String):void 
    {
        switch (mode) 
        {
            case DocumentPanel.EDIT_MODE:
                docEditor.editable = true;
                docEditor.setCombos();
                break;
                
            case DocumentPanel.TRACTS_MODE:
                docEditor.editable = false;
                break;
                
            case DocumentPanel.ATTACHMENT_MODE:
                docEditor.editable = false;
                if (docum.PdfCopy == null) 
                {
                    docController.addAttachment();
                }
                break;
                
            case DocumentPanel.REFERENCES_MODE:
                docEditor.editable = false;
                break;
                
            default :
                docEditor.editable = false;
        }

        detailView.setMode(mode);
    }
            
    protected function addTractRequestHandler():void
    {
        var tract:Tract = new Tract();
        tract.ParentDocument = docum;
        tract.DocId = docum.DocID;

        var popup:TractEditorView = TractEditorView.open(this, true);
        popup.tract = tract;
        popup.oneLevelTractsList = docum.TractsList;
        popup.addEventListener("commit", function ():void {
            saveTract(tract);
        });
    }

    public function saveTract(tract:Tract):AsyncToken
    {
        var isTractNew:Boolean = tract.TractId == 0;

        if (isTractNew)
            tract.CreatedBy = appModel.user.UserId;

        // it is incorrect and need to refactor.
        
        var asyncToken:AsyncToken;
 
//        Alert.show("DocumentPanel_code.saveTract: tract.ParentDocument=" + tract.ParentDocument);
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

                    docum.recalculateTractsCount();
                    doc.TractsList.refresh();
                    
                }, "Unable to Save Tract"));
        } 

        return asyncToken;
    }

   protected function openTractRequestHandler(event:DynamicEvent):void
   {
        var tract:Tract = Tract(event.tract);
        var popup:TractEditorView = TractEditorView.open(this, true);
        popup.tract = tract;
        popup.oneLevelTractsList = docum.TractsList;
        popup.addEventListener("commit", function ():void {
            saveTract(tract);
        });
    }

    protected function saveButton_clickHandler():void
    {
        if (docEditor.isFormValid())
        {
            var doc:Document = docEditor.getChanges();
            if ( leasePanel.visible ) {
                leasePanel.completeEdit();
                doc.Lease = leasePanel.docLease;
            }
            
            var request:AsyncToken = DocumentService.getInstance().saveDocument(
                doc, AppModel.getInstance().user.UserId);

            request.addResponder(new TokenResponder(
                function(event:ResultEvent):void
                {
                    leasePanel.editable = false;
                    docEditor.editable = false;
                    docEditor.discardChanges();
                    //loadRevisions();
                },
                "Unable to Save document"));
        }
    }

    protected function revisionsDG_changedHandler():void
    {
        var doc:Document = Document(revisionsDG.selectedItem);
        
        loadTractList(doc);
    }
    
    private function openTract(tract:Tract):void
    {
        var event:DynamicEvent = new DynamicEvent("openTractRequest");
        event.tract = tract;
        dispatchEvent(event);
    }

    private function loadTractList(doc:Document):void
    {
        if (doc != null && !doc.IsLoaded)
        {
            var asyncToken:AsyncToken = DocumentService.getInstance().getDocumentTractList(doc.DocID);

            asyncToken.addResponder(new Responder(
                function (event:ResultEvent):void 
                {
                    doc.Tracts = event.result as Array;
                    doc.TractsList.refresh();
                    doc.IsLoaded = true;
                },
                
                function (event:FaultEvent):void 
                {
                    Alert.show("Unable to load document tract List. " + event.fault.faultString);
                }
            ));
        }
    }

    private function loadReferences(doc:Document):void
    {
        if (doc != null)
        {
            var asyncToken:AsyncToken = DocumentService.getInstance().loadDocumentReferences(doc);

            asyncToken.addResponder(new Responder(
                function (event:ResultEvent):void {},
                function (event:FaultEvent):void 
                {
                    Alert.show("Unable to load document References. " + event.fault.faultString);
                }
            ));
        }
    }

    private function loadRevisions():void 
    {
        documRevisions = new ArrayCollection([ docum ]);

    /*  Work with revisions is not tested yet. So will be uncommented in next iteration.

        var asyncToken:AsyncToken = ttService.getDocumentBranchRevisions(document.DocBranchUid);

        asyncToken.addResponder(new Responder(
            function (event:ResultEvent):void 
            {
                var sort:Sort = new Sort();
                sort.fields = [ new SortField("DateModified", false, true) ];

                documentRevisions = new ArrayCollection(event.result as Array);
                documentRevisions.addItem(document);
                documentRevisions.sort = sort;
                documentRevisions.refresh();

                view.revisionsDG.selectedIndex = 0;
            },

            function (event:FaultEvent):void {
                Alert.show("Unable to load document revisions. " + event.fault.faultString);
            }
        ));
    */
    }
    
}
}