using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.DispatchVisit
{
    public class DispatchVisitModel
    {
        #region WorkDetail

        private WorkDetail m_workDetail;
        public WorkDetail WorkDetail
        {
            get { return m_workDetail; }
            set { m_workDetail = value; }
        }

        #endregion

        #region VisitStartTime

        private DateTime m_visitStartTime;
        public DateTime VisitStartTime
        {
            get { return m_visitStartTime; }
            set { m_visitStartTime = value; }
        }

        #endregion
    }
}
