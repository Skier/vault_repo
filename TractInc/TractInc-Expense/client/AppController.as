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

    import App.Login.LoginView;
    import common.StatusesRegistry;
    import common.TypesRegistry;
    import common.PermissionsRegistry;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.remoting.RemoteObject;
    import App.Entity.DictionariesDataObject;
    import App.Entity.AFEDataObject;
    import App.Entity.ProjectDataObject;
    import App.Entity.UserDataObject;
    import App.Entity.LoginDataObject;

    public class AppController
    {
    	
		public static const BILL_ITEMS_STORAGE_DIRECTORY:String = "BillItems";
		
		public static var uploaderUrl:String = null;
		public static var uploaderUrlLoaded:Boolean = false;

		public static var storageBaseUrl:String = null;
		public static var storageBaseUrlLoaded:Boolean = false;
		
		private static var service:RemoteObject;
		
		public static function getServerUrls():void 
		{
            service = new RemoteObject("GenericDestination");
   	        service.source = "TractInc.Expense.BaseService";
			
			service.GetStorageUrl.addEventListener(ResultEvent.RESULT, onGetStorageUrl);
			service.GetUploaderUrl.addEventListener(ResultEvent.RESULT, onGetUploaderUrl);
			service.addEventListener(FaultEvent.FAULT, onFault);
			
			service.GetStorageUrl();
			service.GetUploaderUrl();
		}
		
		private static function onGetStorageUrl(event:ResultEvent):void 
		{
			storageBaseUrl = event.result as String;
			storageBaseUrlLoaded = true;
		}

		private static function onGetUploaderUrl(event:ResultEvent):void 
		{
			uploaderUrl = event.result as String;
			uploaderUrlLoaded = true;
		}
			
		private static function onFault(event:FaultEvent):void 
		{
			Alert.show(event.fault.message);
		}
		
		public static var User:UserDataObject;
			
        [Bindable]
        public var Model: AppModel = new AppModel;

        private var view: AppView;
        
        private var responder:Responder;

        public function AppController(view: AppView) {
            this.view = view;
            getServerUrls();
        }
        
        public function CreationComplete(): void {
            view.enabled = false;
            view.expenseView.Parent = this;
            
          	var userService:RemoteObject = new RemoteObject("GenericDestination");
	    	userService.source = "TractInc.Expense.UserService";
       		userService.GetDictionaries.addEventListener(ResultEvent.RESULT,
       			function(result:ResultEvent):void {
       				var dictionary:DictionariesDataObject = DictionariesDataObject(result.result);
       				
       				StatusesRegistry.instance.Load(dictionary);
            		TypesRegistry.instance.Load(dictionary);
            		
            		Model.dictionaries = dictionary;
            		
            		view.enabled = true;
            		
            		init();
       			}
       		);
       		userService.GetDictionaries.addEventListener(FaultEvent.FAULT,
       			function(fault:FaultEvent):void {
       				Alert.show("Please contact administrator", "System Error");
       			}
       		);
       		userService.GetDictionaries();
        }
        
        public function get IsOnline():Boolean {
            return Model.IsOnline;
        }
        
        public function OnUserLoggedIn(userId:int):void 
        {
          	var userService:RemoteObject = new RemoteObject("GenericDestination");
	    	userService.source = "TractInc.Expense.UserService";
       		userService.GetLoginData.addEventListener(ResultEvent.RESULT,
       			function(result:ResultEvent):void {
       				var loginInfo:LoginDataObject = LoginDataObject(result.result);
       				
            		for each (var afe:AFEDataObject in loginInfo.AFEs) {
            			Model.afesHash[afe.AFE] = afe;
            		}
            		
            		for each (var project:ProjectDataObject in loginInfo.Projects) {
            			Model.projectsHash[project.SubAFE] = project;
            		}
            		
       				User = loginInfo.UserInfo;
       				
       				Model.CurrentUser = loginInfo.UserInfo;
       				Model.currentAsset = loginInfo.AssetInfo;
       				
					if (loginInfo.UserRoleInfo.RoleId == 1) {
           				view.managerView.controller.init();
           				SetAppWorkflowState(AppModel.WORKFLOW_STATE_LOGIN_MANAGER);
           			} else {
	            		if (Model.DeviceId == "undefined") {
	                		Model.DeviceId = UIDUtil.createUID();
	            		}
	            		
	                	Model.currentUserAsset = loginInfo.UserAssetInfo;
                		LoadUI();
           			}
       			}
       		);
       		userService.GetLoginData.addEventListener(FaultEvent.FAULT,
       			function(fault:FaultEvent):void {
       				Alert.show("Please contact administrator", "System Error");
       			}
       		);
       		userService.GetLoginData(userId);
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

    }

}
