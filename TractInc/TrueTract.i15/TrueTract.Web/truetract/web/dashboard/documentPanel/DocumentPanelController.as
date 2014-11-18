package truetract.web.dashboard.documentPanel
{
    import mx.collections.ArrayCollection;
    import mx.controls.Alert;
    import mx.rpc.AsyncToken;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.ResultEvent;
    
    import truetract.plotter.domain.Document;
    import truetract.web.services.TrueTractService;
    import truetract.web.AppModel;
    import truetract.web.util.TokenResponder;

    public class DocumentPanelController
    {
        public var view:DocumentPanelView;

        [Bindable] public var document:Document;
        [Bindable] public var documentRevisions:ArrayCollection;

        private var ttService:TrueTractService = TrueTractService.getInstance();

        public function DocumentPanelController(document:Document, view:DocumentPanelView):void
        {
            this.view = view;
            this.document = document;

            loadRevisions();

            loadTractList(document);
        }

        public function saveButtonClickHandler():void
        {
            if (view.docEditor.isFormValid())
            {
                var doc:Document = view.docEditor.getChanges();
                
                var request:AsyncToken = ttService.service.SaveDocument(
                    doc, AppModel.getInstance().user.UserId);

                request.addResponder(new TokenResponder(
                    function(event:ResultEvent):void
                    {   
                        var d:Document = Document(event.result);
                        //TODO: eheheh..
                    },
                    "Unable to Save document"));
            }
        }

        public function revisionChangedHandler():void
        {
            var docRevision:Document = Document(view.revisionsDG.selectedItem);
            
            loadTractList(docRevision);
        }

        private function loadTractList(doc:Document):void
        {
            if (doc != null && !doc.IsLoaded)
            {
                var asyncToken:AsyncToken = ttService.service.GetDocumentTractList(doc.DocID);
                asyncToken.addResponder(new Responder(
                    function (event:ResultEvent):void {
                        doc.Tracts = event.result as Array;
                        doc.IsLoaded = true;
                    },
                    
                    function (event:FaultEvent):void {
                        Alert.show("Unable to load document tract List. " + event.fault.faultString);
                    }
                ));
            }
        }

        private function loadRevisions():void 
        {
            var asyncToken:AsyncToken = 
                ttService.service.GetDocumentBranchRevisions(document.DocBranchUid);

            asyncToken.addResponder(new Responder(
                function (event:ResultEvent):void {
                    documentRevisions = new ArrayCollection(event.result as Array);
                },

                function (event:FaultEvent):void {
                    Alert.show("Unable to load document revisions. " + event.fault.faultString);
                }
            ));
        }
    }
}