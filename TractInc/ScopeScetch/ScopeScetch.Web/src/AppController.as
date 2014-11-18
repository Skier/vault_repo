package src
{
    import mx.collections.ArrayCollection;
    import mx.collections.ItemResponder;
    import mx.controls.Alert;
    import mx.events.DynamicEvent;
    import mx.rpc.AsyncToken;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.InvokeEvent;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.remoting.mxml.RemoteObject;
    import mx.styles.CSSStyleDeclaration;
    import mx.styles.StyleManager;
    
    import src.appMenu.AppMenuEvent;
    import src.deedplotter.DeedPlotter;
    import src.deedplotter.components.SettingsView;
    import src.deedplotter.domain.Tract;
    import src.deedplotter.domain.TractListInfo;
    import src.deedplotter.domain.TractWO;
    import src.deedplotter.domain.User;
    import src.deedplotter.domain.settings.UserSettingsRegistry;
    import src.deedplotter.events.TractExportEvent;
    import src.wizards.attachDocumentWizard.AttachDocumentWizardView;
    import src.wizards.createDrawingWizard.CreateDrawingWizardView;
    import src.wizards.editTractWizard.EditTractWizardView;
    import src.wizards.selectDocumentWizard.SelectDocumentWizardView;
    import src.wizards.selectDrawingWizard.SelectDrawingWizardView;
    import src.wizards.startPageWizard.StartPageWizardView;

    public class AppController
    {
        [Bindable] public var model:AppModel = new AppModel;

        [Bindable] public var serviceIsBusy:Boolean = false;

        public var view:AppView;

        private var deedProService:RemoteObject;

        public function AppController()
        {
            deedProService = new RemoteObject( "GenericDestination" );
            deedProService.source = "TractInc.DeedPro.DeedProService";
			deedProService.showBusyCursor = true;

			deedProService.addEventListener(InvokeEvent.INVOKE, 
			    function(event:InvokeEvent):void { serviceIsBusy = true });

            deedProService.addEventListener(ResultEvent.RESULT,
                function(event:ResultEvent):void { serviceIsBusy = false });

            deedProService.addEventListener(FaultEvent.FAULT,
                function(event:FaultEvent):void { serviceIsBusy = false });
        }

        public function logIn(user:User):void 
        {
            model.user = user;
            
            setAppWorkflowState(AppModel.WORKFLOW_STATE_LOGIN);

            view.plotter.clean();

            var startPage:StartPageWizardView = StartPageWizardView.open(view, true);
        	startPage.appController = this;
        }

        public function logOut():void
        {
            view.plotter.clean();
            setAppWorkflowState(AppModel.WORKFLOW_STATE_LOGOUT);
        }

        public function loadTract(tractId:int):AsyncToken
        {
            return deedProService.LoadTract(tractId);
        }

        public function openTract(tract:Tract):void
        {
            view.plotter.initTract(tract);
            model.currentTract = tract;
            tract.IsDirty = false;
        }

        public function saveTract(tract:Tract):void
        {
            if (tract.TractId == 0)
                tract.CreatedBy = model.user.UserId;

            var asyncToken:AsyncToken = deedProService.SaveTract(tract.ToTractWO(), model.user.UserId);
            asyncToken.addResponder ( new Responder(
                function (event:ResultEvent):void {
                    var serverTract:Tract = TractWO(event.result).ToTract();

                    if (tract.TractId == 0) {
                        tract.TractId = serverTract.TractId;
                    }
                    
                    if (serverTract.DocId) {
                        tract.DocId = tract.ParentDocument.DocID = serverTract.DocId;
                        tract.ParentDocument.Buyer.ParticipantID = serverTract.ParentDocument.Buyer.ParticipantID;
                        tract.ParentDocument.Seller.ParticipantID = serverTract.ParentDocument.Seller.ParticipantID;
                    }

                    tract.IsDirty = false;
                },
                service_onFaultHandler));
        }

        private function setAppWorkflowState(state:int):void {

            model.workflowState = state;
            
            switch (state)
            {
                case AppModel.WORKFLOW_STATE_LOGOUT :
                	if (view.mainViewStack.selectedChild != view.loginView) {
	                    view.mainViewStack.selectedChild = view.loginView;
                	}
                    break;
                                                                
                case AppModel.WORKFLOW_STATE_LOGIN :
                	if (view.mainViewStack.selectedChild != view.scopeScetchView) {
	                    view.mainViewStack.selectedChild = view.scopeScetchView;
                	}
                    break;
                        
                default :
                    throw new Error("Workflow state is invalid");
            }
        }        
        
        private function applyUserSettings():void 
        {
            var popupAlpha:Number = UserSettingsRegistry.getInstance().PopUpAlpha;
            
            var popupDeclaration:CSSStyleDeclaration = 
                StyleManager.getStyleDeclaration("ExtendedTitleWindow");
                
            if (!isNaN(popupAlpha))
            {
                popupDeclaration.setStyle('backgroundAlpha', popupAlpha);
                popupDeclaration.setStyle('borderAlpha', popupAlpha - 0.1);
            }

            view.plotter.tractView.showCallAnnotations = 
                UserSettingsRegistry.getInstance().ShowAnnotations;

            view.plotter.tractView.showArea = 
                UserSettingsRegistry.getInstance().ShowArea;
            
            StyleManager.setStyleDeclaration("ExtendedTitleWindow", popupDeclaration, true);            
        }

        public function app_timeOutHandler():void 
        {
            if (model.currentTract) 
            {
                saveTract(model.currentTract);
            }

            logOut();
        }

        public function plotter_editTractInfoRequestHandler():void
        {
            var wizard:EditTractWizardView = EditTractWizardView.open(view, true);
        	wizard.tract = model.currentTract;
        }

        public function menu_fileCreateDrawingHandler(event:AppMenuEvent):void
        {
            var wizard:CreateDrawingWizardView = CreateDrawingWizardView.open(view, true);
        	wizard.appController = this;
        }

        public function menu_fileOpenDrawingHandler(event:AppMenuEvent):void
        {
            var wizard:SelectDrawingWizardView = SelectDrawingWizardView.open(view, true);
        	wizard.appController = this;
        }

        public function menu_fileCreateTractHandler(event:AppMenuEvent):void
        {
            menu_fileOpenTractHandler(event);
        }

        public function menu_fileOpenTractHandler(event:AppMenuEvent):void
        {
            var wizard:SelectDocumentWizardView = SelectDocumentWizardView.open(view, true);
        	wizard.appController = this;
        }

        public function menu_fileAttachDrawingHandler(event:AppMenuEvent):void
        {
            var wizard:AttachDocumentWizardView = AttachDocumentWizardView.open(view, true);
        	wizard.appController = this;
        }

        public function menu_fileSaveHandler(event:AppMenuEvent):void
        {
            saveTract(model.currentTract);
        }

        public function menu_filePrintHandler(event:AppMenuEvent):void
        {
            view.plotter.Print();
        }
        
        public function menu_fileLogoutHandler(event:AppMenuEvent):void
        {
            logOut();
        }
        
        public function menu_viewZoomAllHandler(event:AppMenuEvent):void
        {
            view.plotter.ZoomAll();
        }

        public function menu_viewZoomInHandler(event:AppMenuEvent):void
        {
            view.plotter.ZoomIn();
        }

        public function menu_viewZoomOutHandler(event:AppMenuEvent):void
        {
            view.plotter.ZoomOut();
        }

        public function menu_settingsShowAnnotationHandler(event:AppMenuEvent):void
        {
            UserSettingsRegistry.getInstance().ShowAnnotations = event.item.toggled;
            view.plotter.tractView.showCallAnnotations = event.item.toggled;
        }

        public function menu_settingsShowAreaHandler(event:AppMenuEvent):void
        {
            UserSettingsRegistry.getInstance().ShowArea = event.item.toggled;
            view.plotter.tractView.showArea = event.item.toggled;
        }
        
        public function menu_settingsMoreHandler(event:AppMenuEvent):void
        {
            SettingsView.Open(view, true);
        }

        private function service_onFaultHandler(event:FaultEvent):void
        {
            Alert.show(event.fault.faultString);
        }
        
    }
}