using System.Collections.Generic;

namespace TractInc.Server.Domain.Package.ProjectManager
{
public class ProjectManagerPackage
{
    public const int PROJECT_MANAGER_MODULE_ID = 5;
    public const int PROJECT_MANAGER_RUN_PERM_ID = 9;
    public const int PROJECT_MANAGER_ASSIGN_ACCOUNT_PERM_ID = 10;

    #region ProjectStatusList
    private List<ProjectStatus> m_projectStatusList;
    public List<ProjectStatus> ProjectStatusList
    {
        get { return m_projectStatusList; }
        set { m_projectStatusList = value; }
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

    #region ContractList
    private List<Contract> m_contractList;
    public List<Contract> ContractList
    {
        get { return m_contractList; }
        set { m_contractList = value; }
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

    #region ProjectList
    private List<Project> m_projectList;
    public List<Project> ProjectList
    {
        get { return m_projectList; }
        set { m_projectList = value; }
    }
    #endregion

    #region CanAssignAccount
    private bool m_canAssignAccount;
    public bool CanAssignAccount
    {
        get { return m_canAssignAccount; }
        set { m_canAssignAccount = value; }
    }
    #endregion
}
}
