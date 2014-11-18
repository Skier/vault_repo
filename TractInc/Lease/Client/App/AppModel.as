package App
{
    import mx.collections.ArrayCollection;
    import App.Domain.User;
    import mx.rpc.remoting.RemoteObject;
    
    [Bindable]
    public class AppModel
    {
        public static const WORKFLOW_STATE_LOGIN:int = 0;
        public static const WORKFLOW_STATE_LEASE_GRID:int = 1;
        public static const WORKFLOW_STATE_LEASE_DETAIL:int = 2;
        public static const WORKFLOW_STATE_INIT:int = 3;

        public var WorkflowState:Number;
		public var RemoteObj:RemoteObject;
		public var CurrentUser:User;
        
    }
}