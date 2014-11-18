package truetract.web.dashboard.tractEditor
{
    import mx.controls.Alert;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.ResultEvent;
    
    import truetract.plotter.domain.Tract;
    import truetract.web.util.wizard.WizardController;
    import truetract.web.wizards.selectDocumentWizard.SelectDocumentWizardView;
    import flash.events.Event;
    
    public class TractEditorController extends WizardController
    {
        public var view:TractEditorView;

        public function TractEditorController(view:TractEditorView) 
        {
            this.view = view;
        }

        public function init():void
        {
            initAnimationRectangle(view.stepsVS);

            setStep(view.editTractStep);
        }

        override public function goToNextStep():Boolean
        {
            var result:Boolean = super.goToNextStep();

            if (result && activeStep == view.editTractStep)
            {
                view.editTractStep.applyTractChanges();
                view.dispatchEvent(new Event("commit"));
                view.close();
            }

            return result;
        }

        public function cancel():void
        {
            view.close();
        }
        
        public function editTract_showRefNameListRequestHandler():void
        {
            setStep(view.showTractListStep);
        }

        public function cancelButton_clickHandler():void
        {
            if (activeStep == view.editTractStep) {
                view.close();
            } else {
                goToPreviousStep();
            }
        }
    }
}
