package tractIncContractManager.contract.companyContact
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
    import TractInc.Domain.CompanyContact;
    import TractInc.Domain.Person;
    import tractInc.domain.packages.ContractPackage;
    import tractInc.domain.packages.ContractManagerPackage;
    import tractInc.domain.storage.IContractManagerStorage;
    import tractInc.domain.storage.ContractManagerStorage;
    import tractIncContractManager.ContractManagerController;
    import tractIncContractManager.contract.ContractController;
    
    [Bindable]
    public class CompanyContactController
    {
        private static var instance:CompanyContactController = null;
        
        public static function getInstance():CompanyContactController
        {
            return instance;
        }

        public var contractPackage:ContractPackage = null;  
        public var contract:Contract = null;  
        public var parentController:ContractController = null;  
        public var view:CompanyContactView = null;
        private var editView:EditView = null;
        
        public function CompanyContactController():void 
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
                view.dataGrid.dataProvider = new ArrayCollection(cp.CompanyContactList);
            }
        }

        public function personLabelFunction(item:Object, column:DataGridColumn):String
        {
            var cContact:CompanyContact = item as CompanyContact;
            return cContact.ContactPerson.LastName + " " + cContact.ContactPerson.FirstName;
        }
            
        public function dateLabelFunction(item:Object, column:DataGridColumn):String
        {
            var companyContact:CompanyContact = item as CompanyContact;
            if ( "Start Date" == column.headerText ) {
                return ContractManagerController.getInstance().getDateFormater().format(companyContact.StartDate);
            } else {
                if ( !DateHelper.isNullDate(companyContact.EndDate) ) {
                    return ContractManagerController.getInstance().getDateFormater().format(companyContact.EndDate);
                } else {
                    return "";
                }
            }
        }
            
        public function getCompanyPersonById(id:int):Person
        {
            for each (var i:Person in contractPackage.ContractCompany.PersonList) {
                if ( id == i.PersonId ) {
                    return i;
                }
            }
            return null;    
        }

        public function openCompanyContact(cContact:CompanyContact):void
        {
            editView = EditView.open(this, cContact, true);
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

        public function saveCompanyContact(cContact:CompanyContact):void 
        {
            cContact.ContractId = contract.ContractId;
            cContact.ContactPerson.CompanyId = contract.CompanyId;
            
            var responder:Responder = new Responder(
                    saveCompanyContactResultHandler, 
                    saveCompanyContactFaultHandler);
                    
            ContractManagerController.getInstance().model.isBusy = true;
            ContractManagerController.getInstance().storage.saveCompanyContact(cContact, responder);
        }
        
        public function removeCompanyContact(cContact:CompanyContact):void
        {
            var responder:Responder = new Responder(
                    removeCompanyContactResultHandler, 
                    removeCompanyContactFaultHandler);

            ContractManagerController.getInstance().model.isBusy = true;
            ContractManagerController.getInstance().storage.removeCompanyContact(cContact, responder);
        }
        
        public function reloadCompanyContactList():void
        {
            var responder:Responder = new Responder(
                    reloadCompanyContactListResultHandler, 
                    reloadCompanyContactListFaultHandler);

            ContractManagerController.getInstance().model.isBusy = true;
            ContractManagerController.getInstance().storage.getCompanyContactList(contract.ContractId, responder);
        }
        
        private function saveCompanyContactResultHandler(event:ResultEvent):void 
        {
            ContractManagerController.getInstance().model.isBusy = false;
            editView.close();
            reloadCompanyContactList();
        }
        
        private function saveCompanyContactFaultHandler(event:FaultEvent):void 
        {
            ContractManagerController.getInstance().model.isBusy = false;
            Alert.show(event.fault.faultString);
        }

        private function removeCompanyContactResultHandler(event:ResultEvent):void 
        {
            ContractManagerController.getInstance().model.isBusy = false;
            reloadCompanyContactList();
        }
        
        private function removeCompanyContactFaultHandler(event:FaultEvent):void 
        {
            ContractManagerController.getInstance().model.isBusy = false;
            Alert.show(event.fault.faultString);
        }

        private function reloadCompanyContactListResultHandler(event:ResultEvent):void 
        {
            ContractManagerController.getInstance().model.isBusy = false;
            contractPackage.CompanyContactList = event.result as Array;
            view.dataGrid.dataProvider = new ArrayCollection(contractPackage.CompanyContactList);
        }
        
        private function reloadCompanyContactListFaultHandler(event:FaultEvent):void 
        {
            ContractManagerController.getInstance().model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
    }
}
