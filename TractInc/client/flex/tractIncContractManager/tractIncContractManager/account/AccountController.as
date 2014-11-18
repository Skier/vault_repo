package tractIncContractManager.account
{
    import flash.events.Event;
    import mx.events.ItemClickEvent;
    import mx.rpc.Responder;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;
    import mx.controls.dataGridClasses.DataGridColumn;
    import mx.collections.ArrayCollection;
    
    import TractInc.Domain.Client;
    import TractInc.Domain.Account;
    import tractInc.domain.packages.ContractManagerPackage;
    import tractInc.domain.storage.IContractManagerStorage;
    import tractInc.domain.storage.ContractManagerStorage;
    import tractIncContractManager.ContractManagerController;
    
    [Bindable]
    public class AccountController
    {
        private static var instance:AccountController = null;
        
        public static function getInstance():AccountController
        {
            return instance;
        }

        public var parentController:ContractManagerController = null;  
        public var view:AccountView = null;
        private var editView:EditView = null;
        
        public function AccountController():void 
        {
            instance = this;    
        }

        public function init(pc:ContractManagerController):void 
        {
            parentController = pc;
/*            
            view.dataGrid.dataProvider = new ArrayCollection(
                    parentController.model.contractManagerPackage.AccountList);
*/            
            loadAccountTree();    
        }

        private function loadAccountTree():void
        {
            var root:Object = new Object();
            root.children = new ArrayCollection();
            for each(var c:Client in parentController.model.contractManagerPackage.ClientList) {
                var n:Object = new Object();
                n.data = c;
                n.label = c.ClientName;
                root.children.addItem(n);
                loadNode(0, c.ClientId, n);
            }    
//            view.trAccount.dataProvider = root;
            view.masterDataGrid.dataProvider = root;
        }
        
        private function loadNode(parentId:int, clientId:int, node:Object):void {
            for each(var a:Account in parentController.model.contractManagerPackage.AccountList) {
                if ( parentId == a.ParentAccountId && clientId == a.ClientId ) {
                    if ( null == node.children ) {
                        node.children = new ArrayCollection();
                    }
                    var n:Object = new Object();
                    node.children.addItem(n);
                    n.data = a;
                    n.label = a.AccountName;
                    loadNode(a.AccountId, clientId, n);
                }
            }
        }
        
        public function openAccount(account:Account):void
        {
            editView = EditView.open(this, account, true);
        }
        
        public function addButtonOnClickHandler(event:Event):void 
        {
            if ( null != view.masterDataGrid.selectedItem ) {
                var client:Client = view.masterDataGrid.selectedItem.data as Client;
                var pa:Account = view.masterDataGrid.selectedItem.data as Account;
                var account:Account = new Account();
                if ( null != client ) {
                    account.ClientId = client.ClientId;
                }
                if ( null != pa ) {
                    account.ParentAccountId = pa.AccountId;
                    account.ClientId = pa.ClientId;
                }
                editView = EditView.open(this, account, true);
            } else {
                Alert.show("Please select Client or Account");
            }
        }

        public function saveAccount(account:Account):void 
        {
            var responder:Responder = new Responder(
                    saveAccountResultHandler, 
                    saveAccountFaultHandler);

            parentController.model.isBusy = true;
            parentController.storage.saveAccount(account, responder);
        }
        
        public function removeAccount(account:Account):void
        {
            var responder:Responder = new Responder(
                    removeAccountResultHandler, 
                    removeAccountFaultHandler);

            parentController.model.isBusy = true;
            parentController.storage.removeAccount(account, responder);
        }
        
        private function saveAccountResultHandler(event:ResultEvent):void 
        {
            parentController.model.isBusy = false;
            editView.close();
            parentController.reloadAccountList();
        }
        
        private function saveAccountFaultHandler(event:FaultEvent):void 
        {
            parentController.model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
        
        private function removeAccountResultHandler(event:ResultEvent):void 
        {
            parentController.model.isBusy = false;
            parentController.reloadAccountList();
        }
        
        private function removeAccountFaultHandler(event:FaultEvent):void 
        {
            parentController.model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
    }
}
