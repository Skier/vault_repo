using System.Collections.Generic;

namespace TractInc.Server.Domain.Package.StaffManager
{
public class StaffManagerPackage
{
    public const int STAFF_MANAGER_MODULE_ID = 6;
    public const int STAFF_MANAGER_RUN_PERM_ID = 11;

    #region AssetTypeList
    private List<AssetType> m_assetTypeList;
    public List<AssetType> AssetTypeList
    {
        get { return m_assetTypeList; }
        set { m_assetTypeList = value; }
    }
    #endregion

    #region BillItemTypeList
    private List<BillItemType> m_billItemTypeList;
    public List<BillItemType> BillItemTypeList
    {
        get { return m_billItemTypeList; }
        set { m_billItemTypeList = value; }
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

    #region PersonList
    private List<Person> m_personList;
    public List<Person> PersonList
    {
        get { return m_personList; }
        set { m_personList = value; }
    }
    #endregion

    #region TeamList
    private List<Team> m_teamList;
    public List<Team> TeamList
    {
        get { return m_teamList; }
        set { m_teamList = value; }
    }
    #endregion

    #region AssetList
    private List<Asset> m_assetList;
    public List<Asset> AssetList
    {
        get { return m_assetList; }
        set { m_assetList = value; }
    }
    #endregion

    #region ProjectList
    private List<Project> m_projectList;
    public List<Project> ProjectList
    {
        get { return m_projectList; }
        set { m_projectList = value; }
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

    #region ClientList
    private List<Client> m_clientList;
    public List<Client> ClientList
    {
        get { return m_clientList; }
        set { m_clientList = value; }
    }
    #endregion

    #region StaffCompany
    private Company staffCompany;
    public Company StaffCompany
    {
        get { return staffCompany; }
        set { staffCompany = value; }
    }
    #endregion
}
}
