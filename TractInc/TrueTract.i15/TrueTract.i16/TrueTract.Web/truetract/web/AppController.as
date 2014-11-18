package truetract.web
{
    import flash.net.URLRequest;
    import flash.net.navigateToURL;
    
    import mx.collections.ArrayCollection;
    import mx.rpc.AsyncToken;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.remoting.mxml.RemoteObject;
    import mx.styles.CSSStyleDeclaration;
    import mx.styles.StyleManager;
    
    import truetract.plotter.domain.User;
    import truetract.plotter.domain.settings.UserSettingsRegistry;
    import truetract.web.services.TrueTractService;
    import truetract.web.util.TokenResponder;
    

    public class AppController
    {
        [Bindable] public var model:AppModel = AppModel.getInstance();

        public var view:AppView;

        private var ttService:TrueTractService = TrueTractService.getInstance();

        public function switchToScopemapping():void 
        {
			var requestURL:URLRequest = new URLRequest;
			requestURL.url = "http://www.scopemapping.com/maps/index.cfm?username=" + model.user.Login;
			navigateToURL(requestURL, "_self");
        }

		public function switchToDasboard():void
		{
			setAppWorkflowState(AppModel.WORKFLOW_STATE_LOGIN);
		}

        public function logIn(user:User):void 
        {
        	model.reset();
        	
            model.user = user;

        	setAppWorkflowState(AppModel.WORKFLOW_STATE_SELECT_APP);

            var asyncToken:AsyncToken = ttService.service.GetModuleListByUserId(model.user.UserId);
            asyncToken.addResponder ( new TokenResponder(
                function (event:ResultEvent):void {
                	model.userModuleList = new ArrayCollection(event.result as Array);
                }, 
                "Unable to load user module list"));
                
            applyUserSettings();
        }

        public function logOut():void
        {
            setAppWorkflowState(AppModel.WORKFLOW_STATE_LOGOUT);
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
                                                                
                case AppModel.WORKFLOW_STATE_SELECT_APP :
                	if (view.mainViewStack.selectedChild != view.switchAppView) {
	                    view.mainViewStack.selectedChild = view.switchAppView;
                	}
                    break;
                                                                
                case AppModel.WORKFLOW_STATE_LOGIN :
                	if (view.mainViewStack.selectedChild != view.dashboardView) {
	                    view.mainViewStack.selectedChild = view.dashboardView;
                	}
                	view.dashboardView.controller.init();
                    break;

                default :
                    throw new Error("Workflow state is invalid");
            }
        }        

        private function applyUserSettings():void 
        {
            var popupAlpha:Number = UserSettingsRegistry.getInstance().PopUpAlpha;
            
            if (!isNaN(popupAlpha))
            {
                var popupDeclaration:CSSStyleDeclaration = 
                    StyleManager.getStyleDeclaration("ExtendedTitleWindow");

                popupDeclaration.setStyle('backgroundAlpha', popupAlpha);
                popupDeclaration.setStyle('borderAlpha', popupAlpha - 0.1);
                
                StyleManager.setStyleDeclaration("ExtendedTitleWindow", popupDeclaration, true);            
            }
        }

    }
}