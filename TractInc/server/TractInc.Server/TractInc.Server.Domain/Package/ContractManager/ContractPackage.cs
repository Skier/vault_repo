using System.Collections.Generic;

namespace TractInc.Server.Domain.Package.ContractManager
{
public class ContractPackage
{

    #region ContractRateList
    private List<ContractRate> m_contractRateList;
    public List<ContractRate> ContractRateList
    {
        get { return m_contractRateList; }
        set { m_contractRateList = value; }
    }
    #endregion

    #region ClientContactList
    private List<ClientContact> m_clientContactList;
    public List<ClientContact> ClientContactList
    {
        get { return m_clientContactList; }
        set { m_clientContactList = value; }
    }
    #endregion

    #region CompanyContactList
    private List<CompanyContact> m_companyContactList;
    public List<CompanyContact> CompanyContactList
    {
        get { return m_companyContactList; }
        set { m_companyContactList = value; }
    }
    #endregion

    #region Contract
    private Contract m_contract;
    public Contract Main
    {
        get { return m_contract; }
        set { m_contract = value; }
    }
    #endregion

    #region Client
    private Client m_client;
    public Client ContractClient
    {
        get { return m_client; }
        set { m_client = value; }
    }
    #endregion

    #region Company
    private Company m_company;
    public Company ContractCompany
    {
        get { return m_company; }
        set { m_company = value; }
    }
    #endregion

}
}
