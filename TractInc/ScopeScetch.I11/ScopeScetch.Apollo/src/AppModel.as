package src    
{
    import mx.collections.ArrayCollection;
    import mx.rpc.remoting.RemoteObject;
    
    import src.common.UserTractRegistry;
    import src.deedplotter.domain.Tract;
    import src.deedplotter.domain.User;
    
    [Bindable]
    public class AppModel
    {
        public static const WORKFLOW_STATE_LOGOUT:int = 0;
        public static const WORKFLOW_STATE_LOGIN:int = 1;

        public var WorkflowState:Number;
        
        public var SyncStatus:String;
        
		public var RemoteObj:RemoteObject;
        
		public var CurrentTract:Tract;
		
		public var Storage:UserTractRegistry;
		
        public var IsOnline:Boolean = false;
        
    }
}