package truetract.web.wizards.editTractWizard
{
    import mx.controls.Alert;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.ResultEvent;
    
    import truetract.domain.Tract;
    import truetract.web.util.wizard.WizardController;
    import truetract.web.wizards.selectDocumentWizard.SelectDocumentWizardView;
    
    public class EditTractWizardController extends WizardController
    {
        public var view:EditTractWizardView;

        public function EditTractWizardController(view:EditTractWizardView) 
        {
            this.view = view;
        }

        public function init():void
        {
            initAnimationRectangle(view.stepsVS);

            setStep(view.editTractStep);
            
            view.selectTractStep.createNewButton.visible = false;
            view.selectTractStep.createNewButton.includeInLayout = false;

            view.selectDrawingStep.createNewButton.visible = false;
            view.selectDrawingStep.createNewButton.includeInLayout = false;
        }

        override public function goToNextStep():Boolean
        {
            var result:Boolean = super.goToNextStep();

            if (result && activeStep == view.editTractStep)
            {
                view.editTractStep.applyTractChanges();
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
            if (view.tract.DocId > 0){
                setStep(view.selectTractStep);
            } else {
                setStep(view.selectDrawingStep);
            }
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
