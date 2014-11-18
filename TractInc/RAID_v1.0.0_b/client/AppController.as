package
{

    import flash.events.Event;
    import flash.events.MouseEvent;
    import flash.external.ExternalInterface;

    import mx.collections.ArrayCollection;
    import mx.controls.Alert;
    import mx.core.Application;
    import mx.rpc.events.ResultEvent;
    import mx.utils.UIDUtil;
    import weborb.data.ActiveCollection;

    import App.Domain.*;
    import weborb.data.DynamicLoadEvent;
    import App.Login.LoginView;
    import common.StatusesRegistry;
    import common.TypesRegistry;
    import common.PermissionsRegistry;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;

    public class AppController
    {
        [Bindable]
        public var Model: AppModel = new AppModel;

        private var view: AppView;
        
        private var responder:Responder;

        public function AppController(view: AppView) {
            this.view = view;
        }
        
        public function CreationComplete(): void {
            view.enabled = false;
            StatusesRegistry.getInstance().Load();
            StatusesRegistry.getInstance().addEventListener(StatusesRegistry.STATUSES_LOADED_EVENT, onStatusesLoaded);
            TypesRegistry.getInstance().Load();
        }

        private function onStatusesLoaded(evt:Event):void {
        	StatusesRegistry.getInstance().removeEventListener(StatusesRegistry.STATUSES_LOADED_EVENT, onStatusesLoaded);
        	view.enabled = true;
            init();
        }
        
        public function get IsOnline():Boolean {
            return Model.IsOnline;
        }
        
        public function OnUserLoggedIn(user:User):void 
        {
            Model.CurrentUser = user;

            Model.permissions = PermissionsRegistry.getInstance();
            Model.permissions.init(user);
            Model.permissions.addEventListener("user_permissions_loaded", onPermissionsLoaded);
        }

        private function OnUserAssetsLoaded(evt:DynamicLoadEvent): void {
            ActiveCollection(evt.data).removeEventListener("loaded", OnUserAssetsLoaded);
            Model.currentUserAsset = UserAsset(ArrayCollection(evt.data).getItemAt(0));
            LoadCurrentAsset();
        }
        
        private function LoadCurrentAsset():void {
            Model.currentAsset = ActiveRecords.Asset.findByPrimaryKey(Model.currentUserAsset.AssetId);
            if (Model.currentAsset.IsLoaded) {
                LoadUI();
            } else {
                Model.currentAsset.addEventListener("loaded", OnCurrentAssetLoaded);
            }
        }

        private function OnCurrentAssetLoaded(evt:DynamicLoadEvent): void {
            Model.currentAsset.removeEventListener("loaded", OnCurrentAssetLoaded);
            LoadUI();
        }
        
        private function onPermissionsLoaded(event:Event):void 
        {
//           	if (Model.permissions.ExistsRole("Manager", 1)) {
			var role:Role = Model.permissions.roles.getItemAt(0) as Role;
			if (role.Name == "Manager") {
           		view.managerView.controller.init();
           		SetAppWorkflowState(AppModel.WORKFLOW_STATE_LOGIN_MANAGER);
           	} else {
	            if (Model.DeviceId == "undefined") {
	                Model.DeviceId = UIDUtil.createUID();
	            }
	            
	            var userAssets:ActiveCollection = Model.CurrentUser.RelatedUserAsset;
	            if (!userAssets.IsLoaded) {
	                userAssets.addEventListener("loaded", OnUserAssetsLoaded);
	            } else {
	                Model.currentUserAsset = UserAsset(userAssets.getItemAt(0));
	                LoadCurrentAsset();
	            }
           	}
        }
        
        private function LoadUI():void 
        {
           	view.loginView.enabled = false;
           	view.expenseView.diaryView.enabled = false;
           	if (Model.currentAsset.AssetId == Model.currentAsset.ChiefAssetId) {
                view.invoiceView.Controller.open();
                SetAppWorkflowState(AppModel.WORKFLOW_STATE_LOGIN_CHIEF);
            } else {
                view.expenseView.Controller.open();
                SetAppWorkflowState(AppModel.WORKFLOW_STATE_LOGIN);
            }
        }

        private function init(): void {
            SetAppWorkflowState(AppModel.WORKFLOW_STATE_LOGOUT);
        }

        public function SetAppWorkflowState(state: int): void {
            Model.WorkflowState = state;

            switch (state) {
                case AppModel.WORKFLOW_STATE_LOGOUT:
                    view.mainViewStack.selectedChild = view.loginView;
                    toOffline();
                    break;
                case AppModel.WORKFLOW_STATE_LOGIN:
                    view.mainViewStack.selectedChild = view.expenseView;
                    break;
                case AppModel.WORKFLOW_STATE_LOGIN_CHIEF:
                    view.mainViewStack.selectedChild = view.invoiceView;
                    break;
                case AppModel.WORKFLOW_STATE_LOGIN_MANAGER:
                    view.mainViewStack.selectedChild = view.managerView;
                    break;
                default:
                    throw new Error("Workflow state is invalid");
            }
        }

        public function toOnline(event: Event = null): void {
            Model.IsOnline = true;
        }

        public function toOffline(event: Event = null): void {
        	view.loginView.enabled = true;
            Model.CurrentUser = null;
            Model.currentAsset = null;
            Model.currentUserAsset = null;
        }

        private function dataReceived_onResultHandler(event: ResultEvent): void {
        }

    }

}
