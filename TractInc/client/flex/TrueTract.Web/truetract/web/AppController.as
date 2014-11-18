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

    import truetract.domain.User;
    import truetract.web.services.TrueTractService;
    import truetract.web.util.TokenResponder;
    import truetract.domain.Module;
    import truetract.plotter.domain.settings.UserSettingsRegistry;
    import mx.core.Application;
    import flash.events.Event;
    
    public class AppController
    {
        [Bindable] public var model:AppModel = AppModel.getInstance();

        public var view:AppView;

        private var ttService:TrueTractService = TrueTractService.getInstance();

        public function switchToScopeMapping():void 
        {
            var url:String = "http://www.scopemapping.com/maps/index3.cfm";
            url += "?username=";
            url += model.user.Login;
            url += "&moxsite=";
            url += model.user.DefaultSite;
            navigateToURL( new URLRequest(url), '_blank');
        }

		public function switchToDasboard():void
		{
			setAppWorkflowState(AppModel.WORKFLOW_STATE_LOGIN);
		}

		public function switchToClientScreen():void
		{
			setAppWorkflowState(AppModel.WORKFLOW_STATE_CLIENT);
		}

        public function logIn(user:User):void 
        {
        	model.reset();

            model.user = user;
            
            if (user.ClientId > 0) 
            {
            	setAppWorkflowState(AppModel.WORKFLOW_STATE_CLIENT);
            } else 
            {
	         	setAppWorkflowState(AppModel.WORKFLOW_STATE_LOGIN);
            }
/*
            var asyncToken:AsyncToken = ttService.getModuleListByUser(model.user.UserId);
            asyncToken.addResponder ( new TokenResponder(
                function (event:ResultEvent):void {
                	model.userModuleList = new ArrayCollection(event.result as Array);

                	if (model.userModuleList) {
                        for each (var module:Module in model.userModuleList){
                            if (module.Description == 'ScopemappingSite'){
                                model.isScopeMappingAllowed = true;
                                break;
                            }
                        }
                	}
                }, 
                "Unable to load user module list"));
*/
            applyUserSettings();
        }

        public function logOut():void
        {
/*        	
        	view.dashboardView.controller.model.plotterMode = false;
            setAppWorkflowState(AppModel.WORKFLOW_STATE_LOGOUT);
            Application.application.dispatchEvent(new Event("logout"));
*/
 			var urlString:String = "javascript:self.close()";
       		var request:URLRequest = new URLRequest(urlString);
       		navigateToURL(request, "_self");            
        }

        private function setAppWorkflowState(state:int):void {

            model.workflowState = state;
            
            switch (state)
            {
                case AppModel.WORKFLOW_STATE_LOGOUT :
/*                
                	if (view.mainViewStack.selectedChild != view.loginView) {
	                    view.mainViewStack.selectedChild = view.loginView;
                	}
*/                	
                    break;
                                                                
                case AppModel.WORKFLOW_STATE_LOGIN :
/*                
                	if (view.mainViewStack.selectedChild != view.dashboardView) {
	                    view.mainViewStack.selectedChild = view.dashboardView;
                	}
*/                	
                	view.dashboardView.controller.init();
                    break;

                case AppModel.WORKFLOW_STATE_CLIENT :
/*                
                	if (view.mainViewStack.selectedChild != view.clientView) {
	                    view.mainViewStack.selectedChild = view.clientView;
                	}
                	view.clientView.init();
*/                	
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