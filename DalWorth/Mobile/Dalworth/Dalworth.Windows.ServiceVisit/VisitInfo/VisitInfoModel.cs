using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Domain.Sync;
using Dalworth.Domain.SyncService;
using Dalworth.SDK;
using WorkTransaction=Dalworth.Domain.WorkTransaction;

namespace Dalworth.Windows.ServiceVisit.VisitInfo
{
    public class VisitInfoModel : IModel
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

        #region ArriveToVisit

        public void ArriveToVisit()
        {
            Domain.Visit.Arrive(Configuration.CurrentTechnicianId, Visit.Visit.ID);            
        }

        #endregion
    }
}
