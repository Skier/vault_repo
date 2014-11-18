package truetract.web.wizards.selectDocumentWizard
{
    import mx.controls.Alert;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.ResultEvent;
    
    import truetract.plotter.domain.Tract;
    import truetract.plotter.domain.TractListInfo;
    import truetract.plotter.domain.TractWO;
    import truetract.web.util.wizard.WizardController;
    import truetract.web.wizards.selectDocumentWizard.SelectDocumentWizardView;
    
    public class SelectDocumentWizardController extends WizardController
    {
        public var view:SelectDocumentWizardView;

        public function SelectDocumentWizardController(view:SelectDocumentWizardView) 
        {
            this.view = view;
        }

        public function init():void
        {
            initAnimationRectangle(view.stepsVS);

            setStep(view.selectDocumentStep);
        }

        override public function goToNextStep():Boolean
        {
            var result:Boolean = super.goToNextStep();

            if (result)
            {

                switch (activeStep)
                {
                    case view.selectDocumentStep:
                        view.selectTractStep.docum = view.selectDocumentStep.docum;
                        setStep(view.selectTractStep);
                        break;

                    case view.selectTractStep:
                        loadAndOpenTract(view.selectTractStep.tractInfo.tractId);
                        break;

                    case view.fillDocReqFieldsStep:
                        view.fillTractReqFieldsStep.docum = view.fillDocReqFieldsStep.docum;
                        setStep(view.fillTractReqFieldsStep);
                        break;

                    case view.fillTractReqFieldsStep:
                        view.appController.openTract(view.fillTractReqFieldsStep.tract);
                        view.close();
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
        
        public function selectDocument_newDocumentRequestHandler():void
        {
            view.fillDocReqFieldsStep.docum = view.selectDocumentStep.docum;
            setStep(view.fillDocReqFieldsStep);
        }

        public function selectTract_newTractRequestHandler():void
        {
            view.fillTractReqFieldsStep.docum = view.selectTractStep.docum;
            view.fillTractReqFieldsStep.tractRefsList = view.selectTractStep.documentTracts;
            setStep(view.fillTractReqFieldsStep);
        }

        private function loadAndOpenTract(tractId:int):void
        {
            var loadTractResponder:Responder = new Responder(
                function (event:ResultEvent):void
                {
                    var tract:TractWO = TractWO(event.result);
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
