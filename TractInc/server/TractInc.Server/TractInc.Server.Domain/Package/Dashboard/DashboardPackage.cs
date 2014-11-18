using System.Collections.Generic;

namespace TractInc.Server.Domain.Package.Dashboard
{
    public class DashboardPackage
    {
        #region User

        private User m_user;
        public User user
        {
            get { return m_user; }
            set { m_user = value; }
        }

        #endregion

        #region Modules

        private List<Module> m_moduleList;
        public List<Module> ModuleList
        {
            get { return m_moduleList; }
            set { m_moduleList = value; }
        }

        #endregion

    }
}
