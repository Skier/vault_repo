package truetract.web.dashboard.documentList
{
    import mx.controls.Alert;
    import mx.events.ListEvent;
    import mx.rpc.AsyncToken;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.remoting.mxml.RemoteObject;
    
    import truetract.plotter.domain.Document;
    import truetract.plotter.domain.Tract;
    import truetract.web.dashboard.documentList.documentEditor.DocumentEditorView;
    import mx.events.DynamicEvent;
    import truetract.web.util.ServiceLocator;
    import mx.controls.Menu;
    
    public class DrawingListController
    {
        
        public function DrawingListController()
        {
        }

        public var view:DocumentListView;

        private var trueTractService:RemoteObject = ServiceLocator.getInstance().getTrueTractService();

        public function docDG_changeHandler():void
        {
            var document:Document = Document(view.docDG.selectedItem);
            
            if (!document.IsLoaded) {
                
                var asyncToken:AsyncToken = trueTractService.GetDocumentTractList(document.DocID);
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
        }

        public function addTractHandler():void
        {
            var document:Document = Document(view.docDG.selectedItem);
            
            view.dashboardController.addTract(document);
        }

        public function openTractHandler(event:DynamicEvent):void
        {
            var tract:Tract = Tract(event.tract);
            view.dashboardController.openTract(tract);
        }

        public function view_creationCompleteHandler():void
        {
        }
    }
}