package tractInc.domain.storage
{
    import mx.rpc.Responder;
    import mx.rpc.AsyncToken;
    import mx.rpc.remoting.RemoteObject;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;

    import TractInc.Domain.Client;
    import TractInc.Domain.ClientContact;
    import TractInc.Domain.Company;
    import TractInc.Domain.CompanyContact;
    import TractInc.Domain.Contract;
    import TractInc.Domain.ContractRate;
    import TractInc.Domain.Account;
    import tractInc.domain.packages.ContractManagerPackage;

    public class ContractManagerStorage implements IContractManagerStorage
    {
        private static const CONTRACT_MANAGER_SERVICE:String = "TractInc.Server.WebOrbServices.ContractManagerService";
        
        private static var _instance:IContractManagerStorage;
        private var _services:Object = new Object();
        
        public static function get instance():IContractManagerStorage
        {
            if (_instance == null) {
                _instance = new ContractManagerStorage();
            }
            return _instance;
        }
        
        public function ContractManagerStorage()
        {
            if (_instance != null) {
                throw new Error("Singleton!");
            }
        }
        
        public function getContractManagerPackage(userId:int, responder:Responder):void
        {
            var asyncToken:AsyncToken = getContractManagerService().GetContractManagerPackage(userId);
            asyncToken.addResponder(responder);
        }
        
        public function getContractList(userId:int, responder:Responder):void
        {
            var asyncToken:AsyncToken = getContractManagerService().GetContractList(userId);
            asyncToken.addResponder(responder);
        }

        public function saveContract(contract:Contract, responder:Responder):void
        {
            var asyncToken:AsyncToken = getContractManagerService().SaveContract(contract);
            asyncToken.addResponder(responder);
        }
        
        public function removeContract(contract:Contract, responder:Responder):void
        {
            var asyncToken:AsyncToken = getContractManagerService().RemoveContract(contract);
            asyncToken.addResponder(responder);
        }

        public function getAccountList(userId:int, responder:Responder):void
        {
            var asyncToken:AsyncToken = getContractManagerService().GetAccountList(userId);
            asyncToken.addResponder(responder);
        }

        public function saveAccount(account:Account, responder:Responder):void
        {
            var asyncToken:AsyncToken = getContractManagerService().SaveAccount(account);
            asyncToken.addResponder(responder);
        }
        
        public function removeAccount(account:Account, responder:Responder):void
        {
            var asyncToken:AsyncToken = getContractManagerService().RemoveAccount(account);
            asyncToken.addResponder(responder);
        }

        public function getContractPackage(contractId:int, responder:Responder):void
        {
            var asyncToken:AsyncToken = getContractManagerService().GetContractPackage(contractId);
            asyncToken.addResponder(responder);
        }
        
        public function getContractRateList(contractId:int, responder:Responder):void
        {
            var asyncToken:AsyncToken = getContractManagerService().GetContractRateList(contractId);
            asyncToken.addResponder(responder);
        }
        
        public function saveContractRate(contractRate:ContractRate, responder:Responder):void
        {
            var asyncToken:AsyncToken = getContractManagerService().SaveContractRate(contractRate);
            asyncToken.addResponder(responder);
        }
        
        public function removeContractRate(contractRate:ContractRate, responder:Responder):void
        {
            var asyncToken:AsyncToken = getContractManagerService().RemoveContractRate(contractRate);
            asyncToken.addResponder(responder);
        }
        
        public function getClientContactList(contractId:int, responder:Responder):void
        {
            var asyncToken:AsyncToken = getContractManagerService().GetClientContactList(contractId);
            asyncToken.addResponder(responder);
        }
        
        public function saveClientContact(clientContact:ClientContact, responder:Responder):void
        {
            var asyncToken:AsyncToken = getContractManagerService().SaveClientContact(clientContact);
            asyncToken.addResponder(responder);
        }
        
        public function removeClientContact(clientContact:ClientContact, responder:Responder):void
        {
            var asyncToken:AsyncToken = getContractManagerService().RemoveClientContact(clientContact);
            asyncToken.addResponder(responder);
        }
        
        public function getCompanyContactList(contractId:int, responder:Responder):void
        {
            var asyncToken:AsyncToken = getContractManagerService().GetCompanyContactList(contractId);
            asyncToken.addResponder(responder);
        }
        
        public function saveCompanyContact(companyContact:CompanyContact, responder:Responder):void
        {
            var asyncToken:AsyncToken = getContractManagerService().SaveCompanyContact(companyContact);
            asyncToken.addResponder(responder);
        }
        
        public function removeCompanyContact(companyContact:CompanyContact, responder:Responder):void
        {
            var asyncToken:AsyncToken = getContractManagerService().RemoveCompanyContact(companyContact);
            asyncToken.addResponder(responder);
        }
        
        private function getContractManagerService():RemoteObject {
            return getService(ContractManagerStorage.CONTRACT_MANAGER_SERVICE);
        }

        private function getService(name:String):RemoteObject {
            if (_services[name] == null) {
                var service:RemoteObject = new RemoteObject();
                service.destination = "GenericDestination";
                service.source = name;
                _services[name] = service;
            } 
            return _services[name];
        }
        
    }
}