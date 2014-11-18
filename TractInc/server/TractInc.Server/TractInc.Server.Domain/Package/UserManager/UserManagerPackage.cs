using System.Collections.Generic;

namespace TractInc.Server.Domain.Package.UserManager
{
public class UserManagerPackage
{
    public const int USER_MANAGER_MODULE_ID = 2;
    public const int USER_MANAGER_RUN_PERM_ID = 4;
    public const int USER_MANAGER_MANAGE_ROLES_PERM_ID = 5;
    public const int USER_MANAGER_MANAGE_USERS_PERM_ID = 6;

    #region RoleList
    private List<Role> m_roleList;
    public List<Role> RoleList
    {
        get { return m_roleList; }
        set { m_roleList = value; }
    }
    #endregion
/*
    #region PermissionList
    private List<Permission> m_permissionList;
    public List<Permission> PermissionList
    {
        get { return m_permissionList; }
        set { m_permissionList = value; }
    }
    #endregion
*/
    #region ModuleList
    private List<Module> m_moduleList;
    public List<Module> ModuleList
    {
        get { return m_moduleList; }
        set { m_moduleList = value; }
    }
    #endregion

    #region UserList
    private List<User> m_userList;
    public List<User> UserList
    {
        get { return m_userList; }
        set { m_userList = value; }
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


    #region canManageUsers
    private bool m_canManageUsers;
    public bool canManageUsers
    {
        get { return m_canManageUsers; }
        set { m_canManageUsers = value; }
    }

    #endregion

    #region canManageRoles
    private bool m_canManageRoles;
    public bool canManageRoles
    {
        get { return m_canManageRoles; }
        set { m_canManageRoles = value; }
    }

    #endregion
}
}
