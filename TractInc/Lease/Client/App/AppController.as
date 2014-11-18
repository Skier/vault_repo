package App
{
    import App.Domain.User;
    
    import flash.events.Event;
    import flash.events.MouseEvent;
    import flash.external.ExternalInterface;
    
    import mx.collections.ArrayCollection;
    import mx.controls.Alert;
    import mx.core.Application;
    import mx.rpc.AsyncToken;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.remoting.RemoteObject;
    import mx.utils.UIDUtil;
    import mx.events.CloseEvent;
    import App.Domain.Lease;
    import App.Domain.ActiveRecords;
    import weborb.data.DynamicLoadEvent;
    import mx.events.CollectionEvent;
    import mx.collections.Sort;
    import mx.collections.SortField;
    import mx.managers.CursorManager;
        
    public class AppController
    {
    	
/*     	public static const WORKFLOW_STATE_LOGIN:int = 0;
        public static const WORKFLOW_STATE_LEASE_GRID:int = 1;
        public static const WORKFLOW_STATE_LEASE_DETAIL:int = 2;
        public static const WORKFLOW_STATE_INIT:int = 3;
 */
		[Bindable]
        public var WorkflowState:Number;
		public var RemoteObj:RemoteObject;
		[Bindable]
		public var _currentUser:User;
		
		public var statesAndCounties:XML;
		
        private var View:AppView;
        
        public function AppController(view:AppView) {
            View = view;
        }
        
        public function CreationComplete():void {
        	statesAndCounties = View.statesAndCounties;
        	showLogin();
        }

        [Bindable]
        public function get CurrentUser():User {
            return _currentUser;
        }
        
        public function set CurrentUser(user:User):void {
            _currentUser = user;
        }
        
        public function OnUserLoggedIn(user:User):void {
           	    CurrentUser = user;
          		showLeaseList();
       	}
        
        public function showInit():void {
	            setAppWorkflowState(AppModel.WORKFLOW_STATE_INIT);
        }
        
		public function showLogin():void {
            setAppWorkflowState(AppModel.WORKFLOW_STATE_LOGIN);
		}
		
        public function showLeaseDetail(lease:Lease):void {
        	View.detailView.init(lease);
        	setAppWorkflowState(AppModel.WORKFLOW_STATE_LEASE_DETAIL);
        }
        
        public function logout():void {
        	CurrentUser = null;
            setAppWorkflowState(AppModel.WORKFLOW_STATE_LOGIN);
        }
        
        public function showLeaseList(lease:Lease = null):void {
        	View.gridView.init(lease);
            setAppWorkflowState(AppModel.WORKFLOW_STATE_LEASE_GRID);
        }
		
        private function setAppWorkflowState(state:int):void {
           WorkflowState = state;
        }
        
        public  function onFaultHandler(event:FaultEvent):void {
            Alert.show(event.fault.faultString);
        }
    }
}