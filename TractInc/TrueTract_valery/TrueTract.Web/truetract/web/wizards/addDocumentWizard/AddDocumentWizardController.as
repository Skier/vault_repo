package truetract.web.wizards.addDocumentWizard
{
    import mx.controls.Alert;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.ResultEvent;
    
    import truetract.domain.Tract;
    import truetract.web.util.wizard.WizardController;
    import truetract.domain.Document;
    import truetract.web.wizards.addDocumentWizard.AddDocumentWizardView;
    import mx.events.DynamicEvent;
    
    public class AddDocumentWizardController extends WizardController
    {
        public var view:AddDocumentWizardView;

        public function AddDocumentWizardController(view:AddDocumentWizardView) 
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
                    	view.docum = view.selectDocumentStep.docum;
                    	
                    	if (view.runsheetEntry != null) 
                    	{
                    		view.fillTabDocStep.tabDocum = view.runsheetEntry;
                    		view.fillTabDocStep.docum = view.docum;
                    		setStep(view.fillTabDocStep);
                    	} else {
	                    	view.dispatchEvent(new DynamicEvent("documentSelected"));
                    	}

                        break;

                    case view.fillDocReqFieldsStep:
                    	view.docum = view.fillDocReqFieldsStep.docum;

                    	if (view.runsheetEntry != null) 
                    	{
                    		view.fillTabDocStep.tabDocum = view.runsheetEntry;
                    		view.fillTabDocStep.docum = view.docum;
                    		setStep(view.fillTabDocStep);
                    	} else {
	                    	view.dispatchEvent(new DynamicEvent("documentCreated"));
                    	}

                        break;

                    case view.fillTabDocStep:
                    	view.runsheetEntry = view.fillTabDocStep.tabDocum;

                    	if (view.docum.DocID > 0) 
                    	{
	                    	view.dispatchEvent(new DynamicEvent("documentSelected"));
                    	} else {
	                    	view.dispatchEvent(new DynamicEvent("documentCreated"));
                    	}

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
