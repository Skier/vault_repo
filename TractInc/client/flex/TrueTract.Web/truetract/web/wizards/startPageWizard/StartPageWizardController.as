package truetract.web.wizards.startPageWizard
{
    import mx.collections.ArrayCollection;
    import mx.controls.Alert;
    import mx.rpc.AsyncToken;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.ResultEvent;
    
    import truetract.domain.Tract;
    import truetract.domain.TractListInfo;
    import truetract.web.util.wizard.WizardController;
    
    public class StartPageWizardController extends WizardController
    {
        public var view:StartPageWizardView;

        private var drawingRefList:ArrayCollection;

        public function StartPageWizardController(view:StartPageWizardView) 
        {
            this.view = view;
        }

        public function init():void
        {
            loadDrawingList();
            
            initAnimationRectangle(view.stepsVS);

            setStep(view.initialStep);
        }

        override public function goToNextStep():Boolean
        {
            var result:Boolean = super.goToNextStep();

            if (result)
            {

                switch (activeStep)
                {
                    case view.initialStep:
                        var createNew:Boolean = view.initialStep.actionTypeRBGroup.selectedValue == 0;
                        var tractTypeIsDrawing:Boolean = (view.initialStep.tractTypeCmb.selectedLabel == "Drawing");
                        
                        if (tractTypeIsDrawing) 
                        {
                            view.ds_fillDrawReqFields.tractRefsList = drawingRefList;
                            setStep(createNew ? view.ds_fillDrawReqFields : view.ds_selectDrawing);
                        } 
                        else 
                        {
                            setStep(view.ts_selectDocument);
                        }
    
                        break;
                    
                    case view.ds_fillDrawReqFields:
                        view.appController.openTract(view.ds_fillDrawReqFields.tract);
                        view.close();
                        break;

                    case view.ts_fillTractReqFields:
                        view.appController.openTract(view.ts_fillTractReqFields.tract);
                        view.close();
                        break;
    
                    case view.ts_fillDocReqFields:
                        view.ts_fillTractReqFields.docum = view.ts_fillDocReqFields.docum;
                        setStep(view.ts_fillTractReqFields);
                        break;
    
                    case view.ds_selectDrawing:
                        loadAndOpenTract(view.ds_selectDrawing.tractInfo.tractId);
                        break;

                    case view.ts_selectTract:
                        loadAndOpenTract(view.ts_selectTract.tractInfo.tractId);
                        break;
    
                    case view.ts_selectDocument:
                        view.ts_selectTract.docum = view.ts_selectDocument.docum;
                        setStep(view.ts_selectTract);
                        break;
    
                    default:
                        break;
                }
            }
            
            return result;
        }

        public function cancel():void
        {
            view.close();
        }
        
        public function ts_selectDocument_newDocumentRequestHandler():void
        {
            view.ts_fillDocReqFields.docum = view.ts_selectDocument.docum;
            setStep(view.ts_fillDocReqFields);
        }

        public function ds_selectDrawing_newDrawingRequestHandler():void
        {
            setStep(view.ds_fillDrawReqFields);
        }

        public function ts_selectTract_newTractRequestHandler():void
        {
            view.ts_fillTractReqFields.docum = view.ts_selectTract.docum;
            view.ts_fillTractReqFields.tractRefsList = view.ts_selectTract.documentTracts;

            setStep(view.ts_fillTractReqFields);
        }
        
        public function openResentRequestHandler():void
        {
            var tractInfo:TractListInfo = view.initialStep.tractInfo;
            
            loadAndOpenTract(tractInfo.tractId);
        }

        private function loadDrawingList():void 
        {
            var asyncToken:AsyncToken = view.trueTractService.GetDrawingList();
            asyncToken.addResponder ( new Responder(
                function (event:ResultEvent):void 
                {
                    drawingRefList = new ArrayCollection(event.result as Array);
                },
                null)
            );
        }
        
        private function loadAndOpenTract(tractId:int):void
        {
            var loadTractResponder:Responder = new Responder(
                function (event:ResultEvent):void
                {
                    var tract:Tract = Tract(event.result);
                    view.appController.openTract(tract.ToTract());
                    view.close();
                }, 
                function (event:FaultEvent):void
                {
                    Alert.show("Unable to open Tract. Error: " + event.fault.faultString);
                }
            );

            view.appController.loadTract(tractId).addResponder(loadTractResponder);
        }
    }
}
