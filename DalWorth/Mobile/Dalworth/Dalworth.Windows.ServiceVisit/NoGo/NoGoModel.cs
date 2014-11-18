using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Domain.Sync;
using Dalworth.Domain.SyncService;
using Dalworth.SDK;
using WorkTransaction=Dalworth.Domain.WorkTransaction;

namespace Dalworth.Windows.ServiceVisit.NoGo
{
    public class NoGoModel
    {
        #region Visit

        private VisitPackage m_visit;
        public VisitPackage Visit
        {
            get { return m_visit; }
            set { m_visit = value; }
        }

        #endregion

        #region SubmitNoGo

        public void SubmitNoGo()
        {
            Domain.Visit.NoGo(Configuration.CurrentTechnicianId, Visit.Visit.ID);            
        }

        #endregion
    }
}
