using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Domain;
using Dalworth.Domain.Sync;
using Dalworth.Domain.SyncService;
using Dalworth.SDK;
using WorkTransaction=Dalworth.Domain.WorkTransaction;

namespace Dalworth.Windows.ServiceVisit.DeclineVisit
{
    public class DeclineVisitModel : IModel
    {
        #region Visit

        private VisitPackage m_visit;
        public VisitPackage Visit
        {
            get { return m_visit; }
            set { m_visit = value; }
        }

        #endregion

        #region Init

        public void Init()
        {
            
        }

        #endregion

        #region DeclineVisit

        public void DeclineVisit(string reason)
        {
            Domain.Visit.Decline(Configuration.CurrentTechnicianId, Visit.Visit.ID, reason);            
        }

        #endregion

    }
}
