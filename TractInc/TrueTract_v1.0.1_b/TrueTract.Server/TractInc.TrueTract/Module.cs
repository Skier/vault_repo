using System.Collections.Generic;
using System.Data.SqlClient;
using TractInc.TrueTract.Data;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract
{
    public class Module
    {
        #region Fields

        private ModuleDataMapper m_moduleDM;

        #endregion

        #region Methods

        public List<ModuleInfo> GetModuleListByUserId(int userId)
        {
            return ModuleDM.GetModuleListByUserId(userId);
        }

        #endregion

        #region Properties

        private ModuleDataMapper ModuleDM
        {
            get
            {
                if (null == m_moduleDM)
                {
                    m_moduleDM = new ModuleDataMapper();
                }

                return m_moduleDM;
            }
        }

        #endregion

    }
}
