using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Domain.SyncService;

namespace Dalworth.Windows.ServiceVisit.CustomerMessage
{
    public class CustomerMessageModel
    {
        #region Visit

        private VisitPackage m_visit;
        public VisitPackage Visit
        {
            get { return m_visit; }
            set { m_visit = value; }
        }

        #endregion
    }
}
