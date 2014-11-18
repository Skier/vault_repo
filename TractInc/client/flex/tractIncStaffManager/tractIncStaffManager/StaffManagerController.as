package tractIncStaffManager
{
    import flash.events.Event;
    import mx.formatters.DateFormatter;
	import mx.events.ItemClickEvent;
    import mx.rpc.Responder;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;
	import mx.collections.ArrayCollection;
    
    import TractInc.Domain.User;
    import tractInc.domain.packages.StaffManagerPackage;
    import tractInc.domain.storage.IStaffManagerStorage;
    import tractInc.domain.storage.StaffManagerStorage;
    
    [Bindable]
    public class StaffManagerController
    {
        private static var instance:StaffManagerController = null;
        
        public static function getInstance():StaffManagerController
        {
            return instance;
        }
        
        public var tabData:Array = [
            {label:"Assets", data:"Assets"},
            {label:"Teams", data:"Teams"},
        ];
        
        public var user:User = null;
        public var model:StaffManagerModel = null;
        public var storage:IStaffManagerStorage = null;
        public var view:StaffManagerView = null;

        public function StaffManagerController():void 
        {
            instance = this;    
        }
        
        public function init(u:User):void 
        {
            user = u;
            model = new StaffManagerModel();
            storage = StaffManagerStorage.instance;

            reloadModel();
        }

        private function reloadModel():void
        {
            var responder:Responder = new Responder(
                    getStaffManagerPackageResultHandler, 
                    getStaffManagerPackageFaultHandler);
            model.isBusy = true;
            storage.getStaffManagerPackage(user.UserId, responder);
        }
        
        public function reloadTeamList():void
        {
            var responder:Responder = new Responder(
                    getTeamListResultHandler, 
                    getTeamListFaultHandler);
            model.isBusy = true;
            storage.getTeamList(user.UserId, responder);
        }
        
        public function reloadAssetList():void
        {
            var responder:Responder = new Responder(
                    getAssetListResultHandler, 
                    getAssetListFaultHandler);
            model.isBusy = true;
            storage.getAssetList(user.UserId, responder);
        }
        
        public function logout():Boolean 
        {
            if ( model.isBusy ) {
                Alert.show("Client Manager service is running");
                return false;
            } else {
                return true;
            }
        }
        
        public function tabChanged(event:ItemClickEvent):void 
        {
            view.tabStack.selectedIndex = event.index;
        }

        private function getStaffManagerPackageResultHandler(event:ResultEvent):void 
        {
            model.isBusy = false;
            model.staffManagerPackage = event.result as StaffManagerPackage;
            view.teamTabView.controller.init(this);
            view.assetTabView.controller.init(this);
        }
        
        private function getStaffManagerPackageFaultHandler(event:FaultEvent):void 
        {
            model.isBusy = false;
            Alert.show(event.fault.message);
        }
        
        private function getTeamListResultHandler(event:ResultEvent):void 
        {
            model.isBusy = false;
            model.staffManagerPackage.TeamList = event.result as Array;
            view.teamTabView.controller.init(this);
            view.assetTabView.controller.init(this);
        }
        
        private function getTeamListFaultHandler(event:FaultEvent):void 
        {
            model.isBusy = false;
            Alert.show(event.fault.message);
        }
        
        private function getAssetListResultHandler(event:ResultEvent):void 
        {
            model.isBusy = false;
            model.staffManagerPackage.AssetList = event.result as Array;
            view.teamTabView.controller.init(this);
            view.assetTabView.controller.init(this);
        }
        
        private function getAssetListFaultHandler(event:FaultEvent):void 
        {
            model.isBusy = false;
            Alert.show(event.fault.message);
        }
        
        private var _df:DateFormatter;
        public function getDateFormater():DateFormatter
        {
            if (!_df)
            {
                _df = new DateFormatter();
                _df.formatString = "MMM DD YYYY";
            }
            return _df;
        }
    }
}