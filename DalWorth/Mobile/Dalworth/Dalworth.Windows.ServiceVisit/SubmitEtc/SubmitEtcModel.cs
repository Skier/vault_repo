using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Domain.Sync;
using Dalworth.Domain.SyncService;
using Dalworth.SDK;
using WorkTransaction=Dalworth.Domain.WorkTransaction;

namespace Dalworth.Windows.ServiceVisit.SubmitEtc
{
    public class SubmitEtcModel
    {
        #region Visit

        private VisitPackage m_visit;
        public VisitPackage Visit
        {
            get { return m_visit; }
            set { m_visit = value; }
        }

        #endregion

        #region SubmitEtc

        public void SubmitEtc(decimal saleAmount, int? hours, int? minutes, string notes)
        {
            Domain.Visit.Etc(Configuration.CurrentTechnicianId, Visit.Visit.ID, saleAmount, hours, minutes, notes);            
        }

        #endregion
    }
}
