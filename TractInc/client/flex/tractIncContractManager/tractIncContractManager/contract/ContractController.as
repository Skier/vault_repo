package tractIncContractManager.contract
{
    import flash.events.Event;
	import mx.events.ItemClickEvent;
    import mx.rpc.Responder;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;
    import mx.controls.dataGridClasses.DataGridColumn;
	import mx.collections.ArrayCollection;
    
    import TractInc.Domain.Contract;
    import tractInc.domain.packages.ContractPackage;
    import tractInc.domain.packages.ContractManagerPackage;
    import tractInc.domain.storage.IContractManagerStorage;
    import tractInc.domain.storage.ContractManagerStorage;
    import tractIncContractManager.ContractManagerController;
    
    [Bindable]
    public class ContractController
    {
        private static var instance:ContractController = null;
        
        public static function getInstance():ContractController
        {
            return instance;
        }

        public var parentController:ContractManagerController = null;  
        public var view:ContractView = null;
        private var editView:EditView = null;
        
        public function ContractController():void 
        {
            instance = this;    
        }

        public function init(pc:ContractManagerController):void 
        {
            parentController = pc;
            
            view.dataGrid.dataProvider = new ArrayCollection(
                    parentController.model.contractManagerPackage.ContractList);
            
        }

        public function dateLabelFunction(item:Object, column:DataGridColumn):String
        {
            var contract:Contract = item as Contract;
            if ( "Start Date" == column.headerText ) {
                return ContractManagerController.getInstance().getDateFormater().format(contract.StartDate);
            } else {
                return ContractManagerController.getInstance().getDateFormater().format(contract.EndDate);
            }
        }
            
        private function openContractWithContractPackage(c:Contract):void 
        {
            if ( null != c ) {
                var responder:Responder = new Responder(
                        getContractPackageResultHandler, 
                        getContractPackageFaultHandler);
    
                ContractManagerController.getInstance().model.isBusy = true;
                ContractManagerController.getInstance().storage.getContractPackage(c.ContractId, responder);
            } else {
                editView = EditView.open(this, null, true);
            }
        }
        
        private function getContractPackageResultHandler(event:mx.rpc.events.ResultEvent):void 
        {
            ContractManagerController.getInstance().model.isBusy = true;
            
            var cp:ContractPackage = event.result as ContractPackage;
            editView = EditView.open(this, cp, true);
        }
        
        private function getContractPackageFaultHandler(event:mx.rpc.events.FaultEvent):void 
        {
            ContractManagerController.getInstance().model.isBusy = true;
            Alert.show(event.fault.faultString);
        }
    
        public function openContract(contract:Contract):void
        {
//            editView = EditView.open(this, contract, true);
            openContractWithContractPackage(contract);
        }
        
        public function addButtonOnClickHandler(event:Event):void 
        {
/*            
            if ( model.ClientManagerPackage.canManageClients ) {
*/            
//                editView = EditView.open(this, null, true);
                openContractWithContractPackage(null);
/*                
            } else {
                Alert.show("No permissions to manage Clients");
            }
*/            
        }

        public function saveContract(contract:Contract):void 
        {
            var responder:Responder = new Responder(
                    saveContractResultHandler, 
                    saveContractFaultHandler);

            parentController.model.isBusy = true;
            parentController.storage.saveContract(contract, responder);
        }
        
        public function removeContract(contract:Contract):void
        {
            var responder:Responder = new Responder(
                    removeContractResultHandler, 
                    removeContractFaultHandler);

            parentController.model.isBusy = true;
            parentController.storage.removeContract(contract, responder);
        }
        
        private function saveContractResultHandler(event:ResultEvent):void 
        {
            parentController.model.isBusy = false;
            editView.close();
            parentController.reloadContractList();
        }
        
        private function saveContractFaultHandler(event:FaultEvent):void 
        {
            parentController.model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
        
        private function removeContractResultHandler(event:ResultEvent):void 
        {
            parentController.model.isBusy = false;
            parentController.reloadContractList();
        }
        
        private function removeContractFaultHandler(event:FaultEvent):void 
        {
            parentController.model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
    }
}
