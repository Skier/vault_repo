package truetract.web.dashboard.documentList
{
    import flash.events.Event;
    
    import mx.controls.Alert;
    import mx.controls.Menu;
    import mx.events.DynamicEvent;
    import mx.events.ListEvent;
    import mx.rpc.AsyncToken;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.remoting.mxml.RemoteObject;
    
    import truetract.plotter.domain.Document;
    import truetract.plotter.domain.Tract;
    import truetract.web.dashboard.DashboardModel;
    import truetract.web.dashboard.UserGroup;
    import truetract.web.dashboard.documentList.documentEditor.DocumentEditorView;
    import truetract.web.dashboard.tractEditor.TractEditorView;
    import truetract.web.services.TrueTractService;
    
    public class DocumentListController
    {
        
        public function DocumentListController(view:DocumentListView)
        {
            this.view = view;
        }

        public var view:DocumentListView;

        private var ttService:TrueTractService = TrueTractService.getInstance();

        public function docDG_changeHandler():void
        {
            var document:Document = Document(view.docDG.selectedItem);
            
            if (!document.IsLoaded) {
                
                var asyncToken:AsyncToken = ttService.service.GetDocumentTractList(document.DocID);
                asyncToken.addResponder(new Responder(
                    function (event:ResultEvent):void {
                        document.Tracts = event.result as Array;
                        document.IsLoaded = true;
                    },
                    
                    function (event:FaultEvent):void {
                        Alert.show("Unable to load document tract List. " + event.fault.faultString);
                    }
                ));
            }
        }

        public function docDG_doubleClickHandler():void
        {
            var documentEditor:DocumentEditorView = DocumentEditorView.open(view, true);
            documentEditor.docum = Document(view.docDG.selectedItem);
            documentEditor.addEventListener("commit", 
                function(event:DynamicEvent):void {
                    var d:Document = Document(event.result);

/*                     docum.DocID = d.DocID;
                    docum.Buyer.ParticipantID = d.Buyer.ParticipantID;
                    docum.Buyer.DocID = d.DocID;

                    docum.Seller.ParticipantID = d.Seller.ParticipantID;
                    docum.Seller.DocID = d.DocID;
 */                });
        }

        public function addTractHandler():void
        {
            var document:Document = Document(view.docDG.selectedItem);

            var tract:Tract = new Tract();
            tract.ParentDocument = document;
            tract.DocId = document.DocID;

            var popup:TractEditorView = TractEditorView.open(view, true);
            popup.tract = tract;
            popup.oneLevelTractsList = document.TractsList;
            popup.addEventListener("commit", function ():void {
                var e:DynamicEvent = new DynamicEvent("openTractRequest");
                e.tract = tract;
                view.dispatchEvent(e);
            });
        }

        public function openTractHandler(event:DynamicEvent):void
        {
            var e:DynamicEvent = new DynamicEvent("openTractRequest");
            e.tract = Tract(event.tract);
            view.dispatchEvent(e);
        }

    }
}