package tractIncContractManager
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
    import tractInc.domain.packages.ContractManagerPackage;
    import tractInc.domain.storage.IContractManagerStorage;
    import tractInc.domain.storage.ContractManagerStorage;
    
    [Bindable]
    public class ContractManagerController
    {
        private static var instance:ContractManagerController = null;
        
        public static function getInstance():ContractManagerController
        {
            return instance;
        }
        
        public var tabData:Array = [
            {label:"Contracts", data:"Contracts"},
            {label:"Accounts", data:"Accounts"},
        ];
        
        public var user:User = null;
        public var model:ContractManagerModel = null;
        public var storage:IContractManagerStorage = null;
        public var view:ContractManagerView = null;

        public function ContractManagerController():void 
        {
            instance = this;    
        }
        
        public function init(u:User):void 
        {
            user = u;
            model = new ContractManagerModel();
            storage = ContractManagerStorage.instance;

            reloadModel();
        }

        private function reloadModel():void
        {
            var responder:Responder = new Responder(
                    getContractManagerPackageResultHandler, 
                    getContractManagerPackageFaultHandler);
            model.isBusy = true;
            storage.getContractManagerPackage(user.UserId, responder);
            
        }
        
        public function reloadContractList():void
        {
            var responder:Responder = new Responder(
                    getContractListResultHandler, 
                    getContractListFaultHandler);
            model.isBusy = true;
            storage.getContractList(user.UserId, responder);
        }
        
        public function reloadAccountList():void
        {
            var responder:Responder = new Responder(
                    getAccountListResultHandler, 
                    getAccountListFaultHandler);
            model.isBusy = true;
            storage.getAccountList(user.UserId, responder);
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

        private function getContractManagerPackageResultHandler(event:ResultEvent):void 
        {
            model.isBusy = false;
            model.contractManagerPackage = event.result as ContractManagerPackage;
            view.contractTabView.controller.init(this);
            view.accountTabView.controller.init(this);
        }
        
        private function getContractManagerPackageFaultHandler(event:FaultEvent):void 
        {
            model.isBusy = false;
            Alert.show(event.fault.message);
        }
        
        private function getContractListResultHandler(event:ResultEvent):void 
        {
            model.isBusy = false;
            model.contractManagerPackage.ContractList = event.result as Array;
            view.contractTabView.controller.init(this);
            view.accountTabView.controller.init(this);
        }
        
        private function getContractListFaultHandler(event:FaultEvent):void 
        {
            model.isBusy = false;
            Alert.show(event.fault.message);
        }
        
        private function getAccountListResultHandler(event:ResultEvent):void 
        {
            model.isBusy = false;
            model.contractManagerPackage.AccountList = event.result as Array;
            view.contractTabView.controller.init(this);
            view.accountTabView.controller.init(this);
        }
        
        private function getAccountListFaultHandler(event:FaultEvent):void 
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