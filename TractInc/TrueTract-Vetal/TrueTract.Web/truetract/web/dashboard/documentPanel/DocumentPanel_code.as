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
import truetract.web.dashboard.documentPanel.attachmentEditor.AttachmentEditorView;
import truetract.web.dashboard.documentPanel.tractEditor.TractEditorView;
import truetract.web.services.TrueTractService;
import truetract.web.util.TokenResponder;

[Event(name="openTractRequest", type="mx.events.DynamicEvent")]
[Event(name="openAttachmentRequest", type="mx.events.DynamicEvent")]

public class DocumentPanel_code extends TabPanel
{
    [Bindable] public var docEditor:DocumentFieldsEditor;
    [Bindable] public var revisionsDG:DataGrid;

    [Bindable] protected var documRevisions:ArrayCollection;

    private var ttService:TrueTractService = TrueTractService.getInstance();

    private var _docum:Document;
    [Bindable] public function get docum():Document { return _docum; }
    public function set docum(value:Document):void
    {
        _docum = value;

        loadRevisions();

        loadTractList(docum);
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
            openTract(tract);
        });
    }

/*     protected function addAttachmentRequestHandler():void
    {
        var attachment:DocumentAttachment= new DocumentAttachment();
        attachment.DocumentId = docum.DocID;

        var popup:AttachmentEditorView = AttachmentEditorView.open(this, true);
        popup.attachment = attachment;
        popup.addEventListener("commit", function ():void 
        {
            if (attachment.IsPdfCopy() && docum.PdfCopy) {
                Alert.show("The PDF Copy of the Document is already specified");
                return;
            }

            var token:AsyncToken = ttService.service.AddDocumentAttachment(attachment, popup.uploadID);
            token.addResponder(new TokenResponder(
                function (event:ResultEvent):void 
                {
                    var a:DocumentAttachment = DocumentAttachment(event.result);

                    docum.AttachmentsList.addItem(a);

                    popup.close();
                },
                "Unable to Add Attachment"
                ));
        });
    }
 */
    protected function openTractRequestHandler(event:DynamicEvent):void
    {
        var tract:Tract = Tract(event.tract);
        openTract(tract);
    }

    protected function saveButton_clickHandler():void
    {
        if (docEditor.isFormValid())
        {
            var doc:Document = docEditor.getChanges();
            
            var request:AsyncToken = ttService.saveDocument(
                doc, AppModel.getInstance().user.UserId);

            request.addResponder(new TokenResponder(
                function(event:ResultEvent):void
                {
                    docEditor.editable = false;
                    //loadRevisions();
                },
                "Unable to Save document"));
        }
    }

    protected function activateButton_clickHandler():void
    {
        var docRevision:Document = Document(revisionsDG.selectedItem);
        if (docRevision.IsActive) return;

        //TODO: implement me
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
            var asyncToken:AsyncToken = ttService.service.GetDocumentTractList(doc.DocID);
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