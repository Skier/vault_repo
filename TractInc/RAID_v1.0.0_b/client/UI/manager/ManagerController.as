package UI.manager
{
	import App.Domain.Invoice;
	
	[Bindable]
	public class ManagerController
	{
		
		private var view:ManagerView;
		public var appController:AppController;
		
		public function ManagerController(view:ManagerView, appController:AppController) 
		{
			this.view = view;
			this.appController = appController;
		}
		
		public function init():void 
		{
			SetManagerState(ManagerModel.VIEW_STATE_DASHBOARD);
		}
		
        public function Logout():void {
            appController.SetAppWorkflowState(AppModel.WORKFLOW_STATE_LOGOUT);
        }
        
        public function SetManagerState(state:int): void {

            switch (state) {
                case ManagerModel.VIEW_STATE_DASHBOARD:
                    view.vsManager.selectedChild = view.dashboardView;
                    view.dashboardView.controller.init();
                    break;

                case ManagerModel.VIEW_STATE_BILL:
                    view.vsManager.selectedChild = view.billView;
                    view.billView.controller.init();
                    break;

                case ManagerModel.VIEW_STATE_INVOICE:
                    view.vsManager.selectedChild = view.invoiceView;
                    view.invoiceView.controller.init();
                    break;

                case ManagerModel.VIEW_STATE_PAYMENT:
                    view.vsManager.selectedChild = view.paymentView;
                    break;

                case ManagerModel.VIEW_STATE_ADMIN:
                    view.vsManager.selectedChild = view.adminView;
                    break;

                default:
                    throw new Error("Workflow state is invalid");
            }
        }
		
	}
}