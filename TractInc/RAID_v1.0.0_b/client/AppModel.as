package
{

    import mx.collections.ArrayCollection;
    import mx.rpc.remoting.RemoteObject;

    import App.Domain.*;
    import common.PermissionsRegistry;
    
    [Bindable]
    public class AppModel
    {

        public static const WORKFLOW_STATE_LOGOUT: int = 0;
        public static const WORKFLOW_STATE_LOGIN: int = 1;
        public static const WORKFLOW_STATE_LOGIN_CHIEF: int = 2;
        public static const WORKFLOW_STATE_LOGIN_MANAGER: int = 3;

        public var WorkflowState: Number;

        public var RemoteObj: RemoteObject;
        
        public var IsOnline: Boolean = false;
        
        public var CurrentUser: User;
        
        public var DeviceId: String;

        public var currentAsset:Asset;

        public var currentUserAsset:UserAsset;
        
        public var permissions:PermissionsRegistry;
        
        public var assetsNotFound:Boolean = false;
        
    }

}
