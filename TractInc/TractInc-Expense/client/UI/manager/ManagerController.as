package UI.manager
{
	import mx.rpc.events.ResultEvent;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.remoting.RemoteObject;
	import mx.controls.Alert;
	import App.Entity.ManagerDataObject;
	import App.Entity.AssetDataObject;
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class ManagerController
	{
		
		private var view:ManagerView;
		public var appController:AppController;
		
		public var model:ManagerModel;
		
		public function ManagerController(view:ManagerView, appController:AppController) 
		{
			this.view = view;
			this.appController = appController;
			this.model = new ManagerModel();
		}
		
		public function init():void 
		{
			SetManagerState(ManagerModel.VIEW_STATE_DASHBOARD);
			
			view.enabled = false;
			
          	var userService:RemoteObject = new RemoteObject("GenericDestination");
	    	userService.source = "TractInc.Expense.UserService";
       		userService.GetManagerData.addEventListener(ResultEvent.RESULT,
       			function(result:ResultEvent):void {
       				model.data = ManagerDataObject(result.result);
       				
       				model.assetsHash = new Array();
       				for each (var asset:AssetDataObject in model.data.Assets) {
       					model.assetsHash[asset.AssetId] = asset;
       				}
       				
       				model.assets = new ArrayCollection(model.data.Assets);
       				
       				model.projects = new ArrayCollection(appController.Model.projectsHash);
       				
            		view.enabled = true;
       			}
       		);
       		userService.GetManagerData.addEventListener(FaultEvent.FAULT,
       			function(fault:FaultEvent):void {
       				Alert.show("Please contact administrator", "System Error");
       			}
       		);
       		userService.GetManagerData();
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
                    view.adminView.controller.init();
                    break;

                default:
                    throw new Error("Workflow state is invalid");
            }
        }
        
        public function importData():void {
          	var userService:RemoteObject = new RemoteObject("GenericDestination");
	    	userService.source = "TractInc.Expense.UserService";
	    	userService.ImportData.addEventListener(FaultEvent.FAULT,
       			function(fault:FaultEvent):void {
       				Alert.show(String(fault.message.body), "System Error");
       			}
       		);
       		userService.ImportData();
        }
		
	}
}