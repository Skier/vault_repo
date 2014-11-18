package tractInc.domain.storage
{
    import mx.rpc.Responder;
    
    import TractInc.Domain.User;
    import TractInc.Domain.Client;
    import TractInc.Domain.ClientContact;
    import TractInc.Domain.Company;
    import TractInc.Domain.CompanyContact;
    import TractInc.Domain.Contract;
    import TractInc.Domain.ContractRate;
    import TractInc.Domain.Account;
    import tractInc.domain.packages.ContractManagerPackage;
    
    public interface IContractManagerStorage
    {
        function getContractManagerPackage(userId:int, responder:Responder):void;
        
        function getContractList(userId:int, responder:Responder):void;
        function saveContract(contract:Contract, responder:Responder):void;
        function removeContract(contract:Contract, responder:Responder):void;
        
        function getAccountList(userId:int, responder:Responder):void;
        function saveAccount(account:Account, responder:Responder):void;
        function removeAccount(account:Account, responder:Responder):void;
        
        function getContractPackage(contractId:int, responder:Responder):void;
        
        function getContractRateList(contractId:int, responder:Responder):void;
        function saveContractRate(contractRate:ContractRate, responder:Responder):void;
        function removeContractRate(contractRate:ContractRate, responder:Responder):void;

        function getClientContactList(contractId:int, responder:Responder):void;
        function saveClientContact(clientContact:ClientContact, responder:Responder):void;
        function removeClientContact(clientContact:ClientContact, responder:Responder):void;

        function getCompanyContactList(contractId:int, responder:Responder):void;
        function saveCompanyContact(companyContact:CompanyContact, responder:Responder):void;
        function removeCompanyContact(companyContact:CompanyContact, responder:Responder):void;
    }
}