using System;
using System.Collections;
using System.Collections.Generic;
using TractInc.Server.Domain;
using TractInc.Server.Domain.Package.ContractManager;

namespace TractInc.Server.WebOrbServices
{
public class ContractManagerService
{

    public ContractManagerPackage GetContractManagerPackage(int userId) {
        User currentUser = User.FindByPrimaryKey(userId);
        if ( null == currentUser ) {
            throw new InvalidOperationException("userId is not valid");
        }

        Company company = Company.findUserCompany(currentUser);

        if ( null == company ) {
            throw new InvalidOperationException("User does not belongs to company.");
        }

        ContractManagerPackage result = new ContractManagerPackage();
        result.ActAsCompany = true;
        result.CompanyList = new List<Company>();
        result.CompanyList.Add(company);
        result.ClientList = Client.findByCompany(company);
        result.AccountList = Account.findByCompany(company);
        result.ContractList = Contract.findByCompany(company);
        result.ContractStatusList = ContractStatus.Find();
        result.InvoiceItemTypeList = InvoiceItemType.Find();
        result.AccountTypeList = AccountType.Find();
        
        return result;
    }

    public List<Contract> GetContractList(int userId) {
        User currentUser = User.FindByPrimaryKey(userId);
        if ( null == currentUser ) {
            throw new InvalidOperationException("userId is not valid");
        }

        Company company = Company.findUserCompany(currentUser);

        if ( null == company ) {
            throw new InvalidOperationException("User does not belongs to company.");
        }

        return Contract.findByCompany(company);
    }

    public void SaveContract(Contract contract) {
        Contract.Save(contract);
    }

    public void RemoveContract(Contract contract) {
        Contract.Remove(contract);
    }

    public List<Account> GetAccountList(int userId) {
        User currentUser = User.FindByPrimaryKey(userId);
        if ( null == currentUser ) {
            throw new InvalidOperationException("userId is not valid");
        }

        Company company = Company.findUserCompany(currentUser);

        if ( null == company ) {
            throw new InvalidOperationException("User does not belongs to company.");
        }

        return Account.findByCompany(company);
    }

    public void SaveAccount(Account account) {
        Account.Save(account);
    }

    public void RemoveAccount(Account account) {
        Account.Remove(account);
    }

    public ContractPackage GetContractPackage(int contractId) {
        Contract contract = Contract.FindByPrimaryKey(contractId);
        if ( null == contract ) {
            throw new InvalidOperationException("contractId is not valid");
        }

        ContractPackage result = new ContractPackage();
        result.Main = contract;
        result.ContractClient = Client.FindByPrimaryKey(contract.ClientId);
        result.ContractCompany = Company.FindByPrimaryKey(contract.CompanyId);

        result.ContractClient.PersonList = Person.findByClient(result.ContractClient);
        result.ContractCompany.PersonList = Person.findByCompany(result.ContractCompany);

        result.ClientContactList = ClientContact.findByContract(contract);
        foreach (ClientContact clc in result.ClientContactList) {
            clc.ContactPerson = Person.FindByPrimaryKey(clc.PersonId);
        }

        result.CompanyContactList = CompanyContact.findByContract(contract);
        foreach (CompanyContact coc in result.CompanyContactList) {
            coc.ContactPerson = Person.FindByPrimaryKey(coc.PersonId);
        }

        result.ContractRateList = ContractRate.findByContract(contract);

        return result;
    }

    public List<ContractRate> GetContractRateList(int contractId) {
        Contract contract = Contract.FindByPrimaryKey(contractId);
        if ( null == contract ) {
            throw new InvalidOperationException("contractId is not valid");
        }

        return ContractRate.findByContract(contract);
    }

    public void SaveContractRate(ContractRate contractRate) {
        ContractRate.Save(contractRate);
    }

    public void RemoveContractRate(ContractRate contractRate) {
        ContractRate.Remove(contractRate);
    }

    public List<ClientContact> GetClientContactList(int contractId) {
        Contract contract = Contract.FindByPrimaryKey(contractId);
        if ( null == contract ) {
            throw new InvalidOperationException("contractId is not valid");
        }

        List<ClientContact> result = ClientContact.findByContract(contract);
        foreach (ClientContact clc in result) {
            clc.ContactPerson = Person.FindByPrimaryKey(clc.PersonId);
        }

        return result;
    }

    public void SaveClientContact(ClientContact clientContact) {
        ClientContact.Save(clientContact);
    }

    public void RemoveClientContact(ClientContact clientContact) {
        ClientContact.Remove(clientContact);
    }

    public List<CompanyContact> GetCompanyContactList(int contractId) {
        Contract contract = Contract.FindByPrimaryKey(contractId);
        if ( null == contract ) {
            throw new InvalidOperationException("contractId is not valid");
        }

        List<CompanyContact> result = CompanyContact.findByContract(contract);
        foreach (CompanyContact clc in result) {
            clc.ContactPerson = Person.FindByPrimaryKey(clc.PersonId);
        }

        return result;
    }

    public void SaveCompanyContact(CompanyContact companyContact) {
        CompanyContact.Save(companyContact);
    }

    public void RemoveCompanyContact(CompanyContact companyContact) {
        CompanyContact.Remove(companyContact);
    }
}
}
