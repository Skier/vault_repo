package truetract.web
{
    import mx.collections.ArrayCollection;
    import mx.collections.ItemResponder;
    import mx.controls.Alert;
    import mx.events.CloseEvent;
    import mx.events.DynamicEvent;
    import mx.rpc.AsyncToken;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.InvokeEvent;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.remoting.mxml.RemoteObject;
    import mx.styles.CSSStyleDeclaration;
    import mx.styles.StyleManager;
    
    import truetract.plotter.Plotter;
    import truetract.plotter.components.SettingsView;
    import truetract.plotter.domain.Tract;
    import truetract.plotter.domain.TractListInfo;
    import truetract.plotter.domain.TractWO;
    import truetract.plotter.domain.User;
    import truetract.plotter.domain.settings.UserSettingsRegistry;
    import truetract.plotter.events.TractExportEvent;
    import truetract.web.appMenu.AppMenuEvent;
    import truetract.web.wizards.attachDocumentWizard.AttachDocumentWizardView;
    import truetract.web.wizards.createDrawingWizard.CreateDrawingWizardView;
    import truetract.web.wizards.editTractWizard.EditTractWizardView;
    import truetract.web.wizards.selectDocumentWizard.SelectDocumentWizardView;
    import truetract.web.wizards.selectDrawingWizard.SelectDrawingWizardView;
    import truetract.web.wizards.startPageWizard.StartPageWizardView;
    import flash.utils.ByteArray;
    import truetract.plotter.utils.PNGEncoder;
    import truetract.web.util.TractConverter;
    import flash.net.URLRequest;
    import flash.net.navigateToURL;
    import mx.containers.Box;
    import truetract.plotter.domain.Module;

    public class AppController
    {
        [Bindable] public var model:AppModel = new AppModel;

        [Bindable] public var serviceIsBusy:Boolean = false;

        public var view:AppView;

        private var trueTractService:RemoteObject;

        public function AppController()
        {
            trueTractService = new RemoteObject( "GenericDestination" );
            trueTractService.source = "TractInc.TrueTract.TrueTractService";
			trueTractService.showBusyCursor = true;

			trueTractService.addEventListener(InvokeEvent.INVOKE, 
			    function(event:InvokeEvent):void { serviceIsBusy = true });

            trueTractService.addEventListener(ResultEvent.RESULT,
                function(event:ResultEvent):void { serviceIsBusy = false });

            trueTractService.addEventListener(FaultEvent.FAULT,
                function(event:FaultEvent):void { serviceIsBusy = false });
        }
        
        public function getCurrentUser():User 
        {
        	return model.user;
        }
        
		public function switchToPlotter():void
		{
			setAppWorkflowState(AppModel.WORKFLOW_STATE_LOGIN);
			view.callLater(setPlotterView);
			
            var startPage:StartPageWizardView = StartPageWizardView.open(view, true);
        	startPage.appController = this;
		}

        public function logIn(user:User):void 
        {
        	model.reset();
        	
            model.user = user;

            var asyncToken:AsyncToken = trueTractService.GetModuleListByUserId(model.user.UserId);
            asyncToken.addResponder ( new Responder(
                function (event:ResultEvent):void {
					model.userModuleList = new ArrayCollection();
                	var modules:ArrayCollection = new ArrayCollection(event.result as Array);
                	for each (var module:Module in modules){
                		if (!(module.Description == "ScopemappingSite" && !model.isBrowserIE)) {
                			model.userModuleList.addItem(module);
                		}
                	}

                	switchToPlotter();
                }, 
                function (event:FaultEvent):void {
                	setAppWorkflowState(AppModel.WORKFLOW_STATE_LOGOUT);
                	Alert.show(event.fault.faultString);
                }));
        }
        
        private function setPlotterView():void 
        {
        	view.appsViewStack.selectedChild = view.plotterView;
        }
        
        public function logOut():void
        {
        	if (view.appsViewStack.selectedChild == view.plotterView) {
	            view.plotter.clean();
        	}
            setAppWorkflowState(AppModel.WORKFLOW_STATE_LOGOUT);
        }

        public function loadTract(tractId:int):AsyncToken
        {
            return trueTractService.LoadTract(tractId);
        }

        public function openTract(tract:Tract):void
        {
            if (model.currentTract && model.currentTract.IsDirty)
            {
                Alert.show("Do you want to the changes to " + model.currentTract.RefName + "?",
                "True Tract", Alert.YES | Alert.NO | Alert.CANCEL, null, 
                    function (event:CloseEvent):void 
                    {
                        if (event.detail == Alert.CANCEL) return;

                        if (event.detail == Alert.YES)
                        {
                            saveTract(model.currentTract);
                        }

                        view.plotter.initTract(tract);
                        model.currentTract = tract;
                        tract.IsDirty = (tract.TractId > 0) ? false : true;

                    }, null, Alert.YES
                );
            } 
            else 
            {
                view.plotter.initTract(tract);
                model.currentTract = tract;

                tract.IsDirty = (tract.TractId > 0) ? false : true;
            }
        }

        public function saveTract(tract:Tract):AsyncToken
        {
            if (tract.TractId == 0)
                tract.CreatedBy = model.user.UserId;

            var asyncToken:AsyncToken = trueTractService.SaveTract(tract.ToTractWO(), model.user.UserId);
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
                        tract.ParentDocument.Buyer.DocID = serverTract.DocId;
                        tract.ParentDocument.Seller.DocID = serverTract.DocId;
                    }

                    tract.IsDirty = false;
                },
                service_onFaultHandler));
            
            return asyncToken;
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
                	if (view.mainViewStack.selectedChild != view.mainView) {
	                    view.mainViewStack.selectedChild = view.mainView;
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

        public function printTract():void
        {
            if (model.currentTract.TractId > 0) {
                view.plotter.Print();
            } else {
                
                Alert.show("In order to print you must save the tract, would you like to save now?",
                "True Tract", Alert.YES | Alert.NO, null, 
                    function (event:CloseEvent):void 
                    {
                        if (event.detail == Alert.NO) return;

                        if (event.detail == Alert.YES)
                        {
                            saveTract(model.currentTract).addResponder ( new Responder(
                                function (event:ResultEvent):void {
                                    view.plotter.Print();
                                }, null));
                        }
                    }, null, Alert.YES);
            }
        }

        public function plotter_printRequestHandler():void
        {
            printTract();
        }

        public function plotter_editTractInfoRequestHandler():void
        {
            var wizard:EditTractWizardView = EditTractWizardView.open(view, true);
        	wizard.tract = model.currentTract;
        }

        public function plotter_pdfExportRequestHandler(event:TractExportEvent):void
        {
            var tractPng:ByteArray = PNGEncoder.encode(event.tractBitmapData);
            var scaleBarPng:ByteArray = PNGEncoder.encode(event.scaleBarBitmapData);
            
            var converter:TractConverter = new TractConverter();
            converter.convertToPdf(event.tract, tractPng, scaleBarPng);
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
            printTract();
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