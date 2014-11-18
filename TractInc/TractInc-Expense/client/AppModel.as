package
{

    import mx.collections.ArrayCollection;
    import mx.rpc.remoting.RemoteObject;
    import common.PermissionsRegistry;
    import App.Entity.DictionariesDataObject;
    import App.Entity.UserDataObject;
    import App.Entity.AssetDataObject;
    import App.Entity.UserAssetDataObject;
    
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
        
        public var CurrentUser: UserDataObject;
        
        public var DeviceId: String;

        public var currentAsset:AssetDataObject;

        public var currentUserAsset:UserAssetDataObject;
        
        public var permissions:PermissionsRegistry;
        
        public var assetsNotFound:Boolean = false;
        
        public var afesHash:Array = new Array();
        
        public var projectsHash:Array = new Array();
        
        public var clients:ArrayCollection;
        
        public var dictionaries:DictionariesDataObject;
        
    }

}
