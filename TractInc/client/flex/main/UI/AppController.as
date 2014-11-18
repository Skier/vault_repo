package UI
{
	import mx.controls.Alert;
	import common.events.LoginEvent;
	import common.events.LoginFaultEvent;
	import TractInc.Domain.storage.RemoteStorage;
	import common.events.LogoutEvent;
	
	[Bindable]
	public class AppController
	{
        public var view:AppView;
        public var model:AppModel = new AppModel();
        
        public function AppController()
        {
        	AppModel.storage = RemoteStorage.instance;
        }
        
		public function onLogin_resultHandler(event:LoginEvent):void 
		{
			model.currentUser = event.user;

			SetAppWorkflowState(AppModel.APP_VIEW_DASHBOARD);
		}
		
		public function onLogin_faultHandler(event:LoginFaultEvent):void 
		{
			Alert.show("Login fault: " + event.fault.faultString);
		}
	
		public function logout_eventHandler(event:LogoutEvent):void 
		{
			model.currentUser = null;

			SetAppWorkflowState(AppModel.APP_VIEW_LOGIN);
		}
		
        public function SetAppWorkflowState(state:int): void {

			switch (state) {
				
            	case AppModel.APP_VIEW_LOGIN:
                    view.mainViewStack.selectedChild = view.viewLogin;
                    break;
                
                case AppModel.APP_VIEW_DASHBOARD:
                    view.mainViewStack.selectedChild = view.viewDashboard;
	                view.viewDashboard.init(model.currentUser);
                    break;
                
                default:
                    throw new Error("Workflow state is invalid");
            }
        }

	}
}