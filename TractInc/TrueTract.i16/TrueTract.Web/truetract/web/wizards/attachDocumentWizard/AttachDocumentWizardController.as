package truetract.web.wizards.attachDocumentWizard
{
    import mx.controls.Alert;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.ResultEvent;
    
    import truetract.plotter.domain.Document;
    import truetract.plotter.domain.Tract;
    import truetract.plotter.domain.TractListInfo;
    import truetract.plotter.domain.TractWO;
    import truetract.web.util.wizard.WizardController;
    
    public class AttachDocumentWizardController extends WizardController
    {
        public var view:AttachDocumentWizardView;

        public function AttachDocumentWizardController(view:AttachDocumentWizardView) 
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
            var document:Document;

            if (result)
            {
                switch (activeStep)
                {
                    case view.selectDocumentStep:
                        document = view.selectDocumentStep.docum;
                        view.appController.model.currentTract.ParentDocument = document;
                        view.appController.model.currentTract.DocId = document.DocID;
                        view.close();
                        break;

                    case view.fillDocReqFieldsStep:
                        document = view.fillDocReqFieldsStep.docum;
                        view.appController.model.currentTract.ParentDocument = document;
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

    }
}
