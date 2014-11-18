package tractInc.domain.storage
{
    import mx.rpc.Responder;
    import mx.rpc.AsyncToken;
    import mx.rpc.remoting.RemoteObject;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;

    import TractInc.Domain.Client;
    import TractInc.Domain.Company;
    import TractInc.Domain.Contract;
    import tractInc.domain.packages.ClientManagerPackage;

    public class ClientManagerStorage implements IClientManagerStorage
    {
        private static const CLIENT_MANAGER_SERVICE:String = "TractInc.Server.WebOrbServices.ClientManagerService";
        
        private static var _instance:IClientManagerStorage;
        private var _services:Object = new Object();
        
        public static function get instance():IClientManagerStorage
        {
            if (_instance == null) {
                _instance = new ClientManagerStorage();
            }
            return _instance;
        }
        
        public function ClientManagerStorage()
        {
            if (_instance != null) {
                throw new Error("Singleton!");
            }
        }
        
        public function getClientManagerPackage(userId:int, responder:Responder):void
        {
            var asyncToken:AsyncToken = getClientManagerService().GetClientManagerPackage(userId);
            asyncToken.addResponder(responder);
        }
        
        public function getClientList(responder:Responder):void
        {
            var asyncToken:AsyncToken = getClientManagerService().GetClientList();
            asyncToken.addResponder(responder);
        }

        public function saveClient(client:Client, responder:Responder):void
        {
            var asyncToken:AsyncToken = getClientManagerService().SaveClient(client);
            asyncToken.addResponder(responder);
        }
        
        public function removeClient(client:Client, responder:Responder):void
        {
            var asyncToken:AsyncToken = getClientManagerService().RemoveClient(client);
            asyncToken.addResponder(responder);
        }

        public function getCompanyList(responder:Responder):void
        {
            var asyncToken:AsyncToken = getClientManagerService().GetCompanyList();
            asyncToken.addResponder(responder);
        }

        public function saveCompany(company:Company, responder:Responder):void
        {
            var asyncToken:AsyncToken = getClientManagerService().SaveCompany(company);
            asyncToken.addResponder(responder);
        }
        
        public function removeCompany(company:Company, responder:Responder):void
        {
            var asyncToken:AsyncToken = getClientManagerService().RemoveCompany(company);
            asyncToken.addResponder(responder);
        }

        public function getContractList(responder:Responder):void
        {
            var asyncToken:AsyncToken = getClientManagerService().GetContractList();
            asyncToken.addResponder(responder);
        }

        public function saveContract(contract:Contract, responder:Responder):void
        {
            var asyncToken:AsyncToken = getClientManagerService().SaveContract(contract);
            asyncToken.addResponder(responder);
        }
        
        public function removeContract(contract:Contract, responder:Responder):void
        {
            var asyncToken:AsyncToken = getClientManagerService().RemoveContract(contract);
            asyncToken.addResponder(responder);
        }

        private function getClientManagerService():RemoteObject {
            return getService(ClientManagerStorage.CLIENT_MANAGER_SERVICE);
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