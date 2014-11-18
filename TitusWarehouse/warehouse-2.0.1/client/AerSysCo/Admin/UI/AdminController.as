package AerSysCo.Admin.UI
{
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.controls.Alert;
	import AerSysCo.Service.WarehouseStorage;
	import mx.rpc.Responder;
	import AerSysCo.Server.LoginResult;
	import AerSysCo.UI.Models.ASCUserUI;
	import AerSysCo.UI.Models.RequestResultUI;
	
	[Bindable]
	public class AdminController
	{
        public var view:AdminView;
        public var model:AdminModel = new AdminModel();

		public function init(view:AdminView):void 
		{
			model = new AdminModel();
			this.view = view;
		}
        
		public function login(user:ASCUserUI):void 
		{
			model.loginInProcess = true;
			
			WarehouseStorage.getInstance().login(user.login, user.password,
				new Responder (
					function (event:ResultEvent):void 
					{
						model.loginInProcess = false;

						var result:LoginResult = event.result as LoginResult;
						if (result.result.status == RequestResultUI.SUCCESS) 
						{
							model.currentUser = new ASCUserUI();
							model.currentUser.populateFromASCUser(result.user);
							SetAppWorkflowState(AdminModel.ADMIN_VIEW_DASHBOARD);
						} else 
						{
							Alert.show(result.result.message, "Login error");
						}
						
					},
					function (event:FaultEvent):void 
					{
						model.loginInProcess = false;
						Alert.show("Login fault: " + event.fault.faultString, "Login fault");
					}
				)
			);
		}
		
		public function logout():void 
		{
			model = new AdminModel();
			SetAppWorkflowState(AdminModel.ADMIN_VIEW_LOGIN);
		}
		
        public function SetAppWorkflowState(state:int): void {

			switch (state) {
				
            	case AdminModel.ADMIN_VIEW_LOGIN:
            		if (view.mainViewStack.selectedChild != view.viewLogin)
                    	view.mainViewStack.selectedChild = view.viewLogin;
                    break;
                
                case AdminModel.ADMIN_VIEW_DASHBOARD:
                	if (view.mainViewStack.selectedChild != view.viewDashboard)
                    	view.mainViewStack.selectedChild = view.viewDashboard;
                    	
	                view.viewDashboard.init(model.currentUser);
                    break;
                
                default:
                    throw new Error("Workflow state is invalid");
            }
        }

	}
}