using System.Collections.Generic;

namespace TractInc.Server.Domain.Package.ContractManager
{
public class ContractManagerPackage
{
    public const int CONTRACT_MANAGER_MODULE_ID = 4;
    public const int CONTRACT_MANAGER_RUN_PERM_ID = 8;

    #region ContractStatusList
    private List<ContractStatus> m_contractStatusList;
    public List<ContractStatus> ContractStatusList
    {
        get { return m_contractStatusList; }
        set { m_contractStatusList = value; }
    }
    #endregion

    #region InvoiceItemTypeList
    private List<InvoiceItemType> m_invoiceItemTypeList;
    public List<InvoiceItemType> InvoiceItemTypeList
    {
        get { return m_invoiceItemTypeList; }
        set { m_invoiceItemTypeList = value; }
    }
    #endregion

    #region AccountTypeList
    private List<AccountType> m_accountTypeList;
    public List<AccountType> AccountTypeList
    {
        get { return m_accountTypeList; }
        set { m_accountTypeList = value; }
    }
    #endregion

    #region CompanyList
    private List<Company> m_companyList;
    public List<Company> CompanyList
    {
        get { return m_companyList; }
        set { m_companyList = value; }
    }
    #endregion

    #region ClientList
    private List<Client> m_clientList;
    public List<Client> ClientList
    {
        get { return m_clientList; }
        set { m_clientList = value; }
    }
    #endregion

    #region AccountList
    private List<Account> m_accountList;
    public List<Account> AccountList
    {
        get { return m_accountList; }
        set { m_accountList = value; }
    }
    #endregion

    #region ContractList
    private List<Contract> m_contractList;
    public List<Contract> ContractList
    {
        get { return m_contractList; }
        set { m_contractList = value; }
    }
    #endregion

    #region ActAsCompany
    private bool m_actAsCompany;
    public bool ActAsCompany
    {
        get { return m_actAsCompany; }
        set { m_actAsCompany = value; }
    }
    #endregion
}
}
