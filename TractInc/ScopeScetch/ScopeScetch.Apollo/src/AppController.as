package src
{
    import flash.events.Event;
    import flash.events.MouseEvent;
    import flash.external.ExternalInterface;
    
    import mx.collections.ArrayCollection;
    import mx.controls.Alert;
    import mx.core.Application;
    import mx.events.CloseEvent;
    import mx.rpc.AsyncToken;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.remoting.RemoteObject;
    import mx.utils.UIDUtil;
    
    import src.common.Pinger;
    import src.common.UserTractRegistry;
    import src.deedplotter.domain.Tract;
    import src.deedplotter.domain.TractWO;
    import src.deedplotter.domain.User;
    import src.login.LoginController;
    
    public class AppController
    {
        [Bindable]
        public var Model:AppModel = new AppModel;

        private var pinger:Pinger;
        private var view:AppView;
        private var syncService:RemoteObject;
        
        public function AppController(view:AppView) {

            this.view = view;

            syncService = new RemoteObject( "GenericDestination" );
            syncService.source = "TractInc.ScopeScetch.SyncService";
            syncService.GetLatestChanges.addEventListener(ResultEvent.RESULT, getLatestChanges_onResultHandler);
            syncService.GetLatestChanges.addEventListener(FaultEvent.FAULT, onFaultHandler);
            syncService.DataReceived.addEventListener(ResultEvent.RESULT, dataReceived_onResultHandler);
            syncService.DataReceived.addEventListener(FaultEvent.FAULT, onFaultHandler);
            syncService.AcceptClientChanges.addEventListener(ResultEvent.RESULT, acceptClientChanges_onResultHandler);
            syncService.AcceptClientChanges.addEventListener(FaultEvent.FAULT, onFaultHandler);

            pinger = new Pinger(5000);
            pinger.addEventListener(Pinger.EVENT_PING_OK, toOnline);
            pinger.addEventListener(Pinger.EVENT_PING_FAILED, toOffline);
            pinger.Start();
        }
        
        public function CreationComplete():void {

            init();
            
            if (ExternalInterface.available) {
                ExternalInterface.addCallback("onMiddleMouseButtonDown", app_middleMouseDownHandler);
                ExternalInterface.addCallback("onMiddleMouseButtonUp", app_middleMouseUpHandler);
            }
        }
        
        private function init():void {
            SetAppWorkflowState(AppModel.WORKFLOW_STATE_LOGOUT);
        }
        
        public function get CurrentUser():User {
            return Model.Storage.user;
        }
        
        public function set CurrentUser(user:User):void {
            Model.Storage.user = user;
        }
        
        public function setCurrentUserInactive():void {
        	Model.Storage.user.IsActive = false;
        	Model.Storage.Save();
        }
        
        public function get IsOnline():Boolean {
            return Model.IsOnline;
        }
        
        public function GetTracts():ArrayCollection {
            return Model.Storage.TractList;
        }
        
        public function SaveTract(tract:Tract):void 
        {
            if (tract.Uid) 
            {
                Model.Storage.SaveTract(tract, true);
            } else 
            {
            	try {
	                Model.Storage.AddTract(tract, true);
            	} catch (err:Error) {
            		Alert.show(err.message);
            	}
            }
        }
        
        public function OnUserLoggedIn(user:User):void 
        {
            InitStorage(user.Login);

            Model.Storage.user = user;
            SetAppWorkflowState(AppModel.WORKFLOW_STATE_LOGIN);
            
            if (Model.Storage.DeviceId == "undefined") 
            {
                Model.Storage.DeviceId = UIDUtil.createUID();
                Sync();
            }
        }
        
        public function Sync():void 
        {
            if (IsOnline) 
            {
                view.scopeScetchView.enabled = false;
                syncService.GetLatestChanges( Model.Storage.user.UserId, Model.Storage.DeviceId);
                Model.SyncStatus = "Synchronize in process";
            }
        }
        
        public function InitStorage(login:String):void 
        {
            Model.Storage = new UserTractRegistry(login);
        }

        public function ResetLocalData():void {
        	
            Alert.show("All local data will be removed. Do you want to do it? ", "Alert", 
                Alert.YES | Alert.NO, null, 
                function (event:CloseEvent):void {
                    if (event.detail == Alert.YES) {
                        Model.Storage.ClearAll();
                        Logout();
                    } else {
                    	return;
                    }
                }, null, Alert.YES);
        	
        }
        
        public function Logout():void {

            SetAppWorkflowState(AppModel.WORKFLOW_STATE_LOGOUT);

        }
        
        private function SetAppWorkflowState(state:int):void {

            Model.WorkflowState = state;
            
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
                    view.scopeScetchView.Controller.Init();
                    break;
                        
                default :
                    throw new Error("Workflow state is invalid");
            }
        }        
        
        private function toOnline(event:Event):void {
            Model.IsOnline = true;
        }
        
        private function toOffline(event:Event):void {
            Model.IsOnline = false;
        }
        
        
        private function dataReceived_onResultHandler(event:ResultEvent):void {
        }
        
        private function getLatestChanges_onResultHandler(event:ResultEvent):void {
            
            var changedTracts:ArrayCollection = new ArrayCollection(event.result as Array);

            syncService.DataReceived( Model.Storage.user.UserId, Model.Storage.DeviceId );
            
            var conflictCount:int = 0;
            
            for each (var tract:TractWO in changedTracts) {
                if (Model.Storage.Exists(tract.ToTract(), Model.Storage.ChangedTracts)) {
                    conflictCount++;
                } else {
                    
                    var record:Object = Model.Storage.GetTractRecordByUid(tract.Uid);
                    
                    if (record != null) {
                        Model.Storage.SaveTract(tract.ToTract(), false);
                    } else {
                        Model.Storage.AddTract(tract.ToTract(), false);
                    }
                    
                }
            }
                    
            if (conflictCount > 0) {
                Alert.show("Total " + conflictCount + " conflict(s) detected. Did not realized yet !");
            } else {
                var commitList:ArrayCollection = new ArrayCollection();
                var uid:String;
                
                for each (uid in Model.Storage.NewTracts) {
                    commitList.addItem(Model.Storage.GetTract(uid).ToTractWO());
                }
    
                for each (uid in Model.Storage.ChangedTracts) {
                    commitList.addItem(Model.Storage.GetTract(uid).ToTractWO());
                }
                
                syncService.AcceptClientChanges(commitList.source, Model.Storage.user.UserId, 
                    Model.Storage.DeviceId);
            }
        }
        
        private function acceptClientChanges_onResultHandler(event:ResultEvent):void {

            Model.SyncStatus = "Synchronize done";
            view.scopeScetchView.enabled = true;

            var tractIds:Object = event.result;
            var tract:Tract;

            for each (var uid:String in Model.Storage.NewTracts) {
                tract = Model.Storage.GetTract(uid);
                tract.TractId = tractIds[uid];
                Model.Storage.SaveTract(tract, false);
            }
            
            Model.Storage.ClearChanges();
            
// re-login begin

            var loginController:LoginController = view.loginView.Controller;

            loginController.view.username.text = CurrentUser.Login;
            loginController.view.password.text = CurrentUser.Password;
            loginController.doLogin();

// re-login end
        }
                
        private function onFaultHandler(event:FaultEvent):void {
            Model.SyncStatus = "";
            view.scopeScetchView.enabled = true;
            Alert.show(event.fault.faultString);
        }
        
        protected function app_middleMouseDownHandler(x:Number, y:Number):void {
            var event:MouseEvent = new MouseEvent(MouseEvent.MOUSE_DOWN, true, true, x, y);
            event.altKey = true;
            Application.application.dispatchEvent(event);
        }

        protected function app_middleMouseUpHandler(x:Number, y:Number):void {
            var event:MouseEvent = new MouseEvent(MouseEvent.MOUSE_UP, true, true, x, y);
            event.altKey = true;
            Application.application.dispatchEvent(event);
        }
        
    }
}