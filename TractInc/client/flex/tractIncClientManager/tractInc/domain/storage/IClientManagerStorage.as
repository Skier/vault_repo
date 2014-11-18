package tractInc.domain.storage
{
    import mx.rpc.Responder;
    
    import TractInc.Domain.User;
    import TractInc.Domain.Client;
    import TractInc.Domain.Company;
    import TractInc.Domain.Contract;
    import tractInc.domain.packages.ClientManagerPackage;
    
    public interface IClientManagerStorage
    {
        function getClientManagerPackage(userId:int, responder:Responder):void;
        
        function getClientList(responder:Responder):void;
        function saveClient(client:Client, responder:Responder):void;
        function removeClient(client:Client, responder:Responder):void;
        
        function getCompanyList(responder:Responder):void;
        function saveCompany(company:Company, responder:Responder):void;
        function removeCompany(company:Company, responder:Responder):void;

        function getContractList(responder:Responder):void;
        function saveContract(contract:Contract, responder:Responder):void;
        function removeContract(contract:Contract, responder:Responder):void;
    }
}