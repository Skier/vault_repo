using System;
using System.Collections;
using System.Collections.Generic;
using TractInc.Server.Domain;
using TractInc.Server.Domain.Package.ClientManager;

namespace TractInc.Server.WebOrbServices
{
public class ClientManagerService
{

    public ClientManagerPackage GetClientManagerPackage(int userId) {
        User currentUser = User.FindByPrimaryKey(userId);
        if ( null == currentUser ) {
            throw new InvalidOperationException("userId is not valid");
        }

        ClientManagerPackage result = new ClientManagerPackage();
        result.ContractStatusList = ContractStatus.Find();

        return result;
    }

    public List<Client> GetClientList() {
        List<Client> result = Client.Find();
        foreach(Client cl in result) {
            cl.CompanyList = Company.findByClient(cl);
        }
        return result;
    }

    public void SaveClient(Client client) {
        Client.Save(client);
    }

    public void RemoveClient(Client client) {
        Client.Remove(client);
    }

    public List<Company> GetCompanyList() {
        List<Company> result = Company.Find();
        foreach(Company c in result) {
            c.ClientList = Client.findByCompany(c);
        }
        return result;
    }

    public void SaveCompany(Company company) {
        Company.Save(company);
    }

    public void RemoveCompany(Company company) {
        Company.Remove(company);
    }

    public List<Contract> GetContractList() {
        return Contract.Find();
    }

    public void SaveContract(Contract contract) {
        Contract.Save(contract);
    }

    public void RemoveContract(Contract contract) {
        Contract.Remove(contract);
    }
}
}
