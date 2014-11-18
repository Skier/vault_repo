package tractIncContractManager.contract.clientContact
{
    import flash.events.Event;
    import mx.events.ItemClickEvent;
    import mx.rpc.Responder;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;
    import mx.collections.ArrayCollection;
    import mx.controls.dataGridClasses.DataGridColumn;
    
    import TractInc.SDK.utils.DateHelper;
    import TractInc.Domain.Contract;
    import TractInc.Domain.ClientContact;
    import TractInc.Domain.Person;
    import tractInc.domain.packages.ContractPackage;
    import tractInc.domain.packages.ContractManagerPackage;
    import tractInc.domain.storage.IContractManagerStorage;
    import tractInc.domain.storage.ContractManagerStorage;
    import tractIncContractManager.ContractManagerController;
    import tractIncContractManager.contract.ContractController;
    
    [Bindable]
    public class ClientContactController
    {
        private static var instance:ClientContactController = null;
        
        public static function getInstance():ClientContactController
        {
            return instance;
        }

        public var contractPackage:ContractPackage = null;  
        public var contract:Contract = null;  
        public var parentController:ContractController = null;  
        public var view:ClientContactView = null;
        private var editView:EditView = null;
        
        public function ClientContactController():void 
        {
            instance = this;    
        }

        public function init(cp:ContractPackage, pc:ContractController):void 
        {
            contractPackage = cp;
            if ( null != cp ) {
                contract = cp.Main;
            }
            parentController = pc;
            if ( null != cp ) {
                view.dataGrid.dataProvider = new ArrayCollection(cp.ClientContactList);
            }
        }

        public function personLabelFunction(item:Object, column:DataGridColumn):String
        {
            var clientContact:ClientContact = item as ClientContact;
            return clientContact.ContactPerson.LastName + " " + clientContact.ContactPerson.FirstName;
        }
            
        public function dateLabelFunction(item:Object, column:DataGridColumn):String
        {
            var clientContact:ClientContact = item as ClientContact;
            if ( "Start Date" == column.headerText ) {
                return ContractManagerController.getInstance().getDateFormater().format(clientContact.StartDate);
            } else {
                if ( !DateHelper.isNullDate(clientContact.EndDate) ) {
                    return ContractManagerController.getInstance().getDateFormater().format(clientContact.EndDate);
                } else {
                    return "";
                }
            }
        }
            
        public function getClientPersonById(id:int):Person
        {
            for each (var i:Person in contractPackage.ContractClient.PersonList) {
                if ( id == i.PersonId ) {
                    return i;
                }
            }
            return null;    
        }

        public function openClientContact(clientContact:ClientContact):void
        {
            editView = EditView.open(this, clientContact, true);
        }
        
        public function addButtonOnClickHandler(event:Event):void 
        {
/*            
            if ( model.ClientManagerPackage.canManageClients ) {
*/            
                editView = EditView.open(this, null, true);
/*                
            } else {
                Alert.show("No permissions to manage Clients");
            }
*/            
        }

        public function saveClientContact(clientContact:ClientContact):void 
        {
            clientContact.ContractId = contract.ContractId;
            clientContact.ContactPerson.ClientId = contract.ClientId;
            
            var responder:Responder = new Responder(
                    saveClientContactResultHandler, 
                    saveClientContactFaultHandler);
                    
            ContractManagerController.getInstance().model.isBusy = true;
            ContractManagerController.getInstance().storage.saveClientContact(clientContact, responder);
        }
        
        public function removeClientContact(clientContact:ClientContact):void
        {
            var responder:Responder = new Responder(
                    removeClientContactResultHandler, 
                    removeClientContactFaultHandler);

            ContractManagerController.getInstance().model.isBusy = true;
            ContractManagerController.getInstance().storage.removeClientContact(clientContact, responder);
        }
        
        public function reloadClientContactList():void
        {
            var responder:Responder = new Responder(
                    reloadClientContactListResultHandler, 
                    reloadClientContactListFaultHandler);

            ContractManagerController.getInstance().model.isBusy = true;
            ContractManagerController.getInstance().storage.getClientContactList(contract.ContractId, responder);
        }
        
        private function saveClientContactResultHandler(event:ResultEvent):void 
        {
            ContractManagerController.getInstance().model.isBusy = false;
            editView.close();
            reloadClientContactList();
        }
        
        private function saveClientContactFaultHandler(event:FaultEvent):void 
        {
            ContractManagerController.getInstance().model.isBusy = false;
            Alert.show(event.fault.faultString);
        }

        private function removeClientContactResultHandler(event:ResultEvent):void 
        {
            ContractManagerController.getInstance().model.isBusy = false;
            reloadClientContactList();
        }
        
        private function removeClientContactFaultHandler(event:FaultEvent):void 
        {
            ContractManagerController.getInstance().model.isBusy = false;
            Alert.show(event.fault.faultString);
        }

        private function reloadClientContactListResultHandler(event:ResultEvent):void 
        {
            ContractManagerController.getInstance().model.isBusy = false;
            contractPackage.ClientContactList = event.result as Array;
            view.dataGrid.dataProvider = new ArrayCollection(contractPackage.ClientContactList);
        }
        
        private function reloadClientContactListFaultHandler(event:FaultEvent):void 
        {
            ContractManagerController.getInstance().model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
    }
}
