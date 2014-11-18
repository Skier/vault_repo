using System.Collections.Generic;

namespace TractInc.Server.Domain.Package.ClientManager
{
public class ClientManagerPackage
{
    public const int CLIENT_MANAGER_MODULE_ID = 3;
    public const int CLIENT_MANAGER_RUN_PERM_ID = 7;

    #region ContractStatusList
    private List<ContractStatus> m_contractStatusList;
    public List<ContractStatus> ContractStatusList
    {
        get { return m_contractStatusList; }
        set { m_contractStatusList = value; }
    }
    #endregion

}
}
