package truetract.web.dashboard.searchPanel
{
    import flash.events.Event;
    
    import mx.events.DynamicEvent;
    import mx.rpc.AsyncToken;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.remoting.mxml.RemoteObject;
    
    import truetract.plotter.domain.Document;
    import truetract.web.services.TrueTractService;
    import truetract.web.util.TokenResponder;
    import mx.collections.ArrayCollection;
    
    public class SearchPanelControlller
    {

        public var view:SearchPanelView;

        [Bindable] public var model:SearchPanelModel = new SearchPanelModel();

        private var trueTractService:TrueTractService = TrueTractService.getInstance();

        public function resetDrawingFilter_clickHandler():void
        {
            view.refNameTxt.text = "";
            model.drawingList = null;
        }

        public function resetDocumentFilter_clickHandler():void
        {
            view.stateCmb.selectedIndex = 0;
            view.countyCmb.selectedIndex = 0;
            view.documentTypeCmb.selectedIndex = 0;
            view.documNoTxt.text = "";
            view.volumeTxt.text = "";
            view.pageTxt.text = "";
            
            model.documentList = null;
        }

        public function applyDocumentFilter_clickHandler():void
        {
            if (!view.documentSearchFormValidator.validate(true)) return;

            var queryTemplate:Document = new Document();
            queryTemplate.DocTypeId = int(view.documentTypeCmb.selectedItem.@DocTypeID);
        	queryTemplate.State = int(view.stateCmb.selectedItem.@StateId);

        	queryTemplate.County = view.countyCmb.selectedItem 
        	    ? int(view.countyCmb.selectedItem.@CountyId) : 0;

        	queryTemplate.DocumentNo = (view.documNoTxt.text.length > 0) ? view.documNoTxt.text : null;
        	queryTemplate.Volume = (view.volumeTxt.text.length > 0) ? view.volumeTxt.text : null;
        	queryTemplate.Page = (view.pageTxt.text.length > 0) ? view.pageTxt.text : null;
            
            var asyncToken:AsyncToken = trueTractService.findDocumentsByTemplate(queryTemplate);
            asyncToken.addResponder(new TokenResponder(
                function (event:ResultEvent):void
                {
                    model.documentList = new ArrayCollection(event.result as Array);
                },
                "Unable to find Documents")
            );
        }

        public function applyDrawingFilter_clickHandler():void
        {
            var refName:String = view.refNameTxt.text;
            
            var asyncToken:AsyncToken = trueTractService.findDrawingsByTemplate(refName);
            asyncToken.addResponder(new TokenResponder(
                function (event:ResultEvent):void
                {
                    model.drawingList = new ArrayCollection(event.result as Array);
                },
                "Unable to find Drawings")
            );
        }
    }
}