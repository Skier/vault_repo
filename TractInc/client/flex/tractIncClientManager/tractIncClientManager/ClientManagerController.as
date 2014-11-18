package tractIncClientManager
{
    import flash.events.Event;
	import mx.events.ItemClickEvent;
    import mx.rpc.Responder;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;
	import mx.collections.ArrayCollection;
    
    import TractInc.Domain.User;
    import tractInc.domain.packages.ClientManagerPackage;
    import tractInc.domain.storage.IClientManagerStorage;
    import tractInc.domain.storage.ClientManagerStorage;
    
    [Bindable]
    public class ClientManagerController
    {
        private static var instance:ClientManagerController = null;
        
        public static function getInstance():ClientManagerController
        {
            return instance;
        }
        
        public var tabData:Array = [
            {label:"Clients", data:"Clients"},
            {label:"Companies", data:"Companies"}
        ];
        
        public var user:User = null;
        public var model:ClientManagerModel = null;
        public var storage:IClientManagerStorage = null;
        public var view:ClientManagerView = null;
        
        public function ClientManagerController():void 
        {
            instance = this;    
        }
        
        public function init(u:User):void 
        {
            user = u;
            model = new ClientManagerModel();
            storage = ClientManagerStorage.instance;

            reloadModel();
        }

        public function reloadModel():void
        {
            var responder:Responder = new Responder(
                    getClientManagerPackageResultHandler, 
                    getClientManagerPackageFaultHandler);
            model.isBusy = true;
            storage.getClientManagerPackage(user.UserId, responder);
            
            reloadClientList();
            reloadCompanyList();
        }
        
        public function reloadClientList():void
        {
            var responder:Responder = new Responder(
                    getClientListResultHandler, 
                    getClientListFaultHandler);
            model.isBusy = true;
            storage.getClientList(responder);
        }
        
        public function reloadCompanyList():void
        {
            var responder:Responder = new Responder(
                    getCompanyListResultHandler, 
                    getCompanyListFaultHandler);
            model.isBusy = true;
            storage.getCompanyList(responder);
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
        private function getClientManagerPackageResultHandler(event:ResultEvent):void 
        {
            model.isBusy = false;
            model.clientManagerPackage = event.result as ClientManagerPackage;
//            Alert.show("Received: " + model.clientManagerPackage.ContractStatusList.length + " contract statuses.");
        }
        
        private function getClientManagerPackageFaultHandler(event:FaultEvent):void 
        {
            model.isBusy = false;
            Alert.show(event.fault.message);
        }
        
        private function getClientListResultHandler(event:ResultEvent):void 
        {
            model.isBusy = false;
            model.clientList = event.result as Array;
            trace("ClientManagerController.getClientListResultHandler: retrieved "
                    + model.clientList.length + " clients.");
            view.clientTabView.controller.init(this);
        }
        
        private function getClientListFaultHandler(event:FaultEvent):void 
        {
            model.isBusy = false;
            Alert.show(event.fault.message);
        }
        
        private function getCompanyListResultHandler(event:ResultEvent):void 
        {
            model.isBusy = false;
            model.companyList = event.result as Array;
            view.companyTabView.controller.init(this);
        }
        
        private function getCompanyListFaultHandler(event:FaultEvent):void 
        {
            model.isBusy = false;
            Alert.show(event.fault.message);
        }
        
    }
}