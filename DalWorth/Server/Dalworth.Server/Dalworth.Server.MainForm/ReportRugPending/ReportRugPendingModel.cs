using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.Reports;
using Dalworth.Server.MainForm.Reports;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.ReportRugPending
{
    public class ReportRugPendingModel : IModel
    {
        #region RugPending

        private RugPending m_rugPending;
        public RugPending RugPending
        {
            get { return m_rugPending; }
        }

        #endregion

        #region Init

        public void Init()
        {
        }

        #endregion

        #region RefreshReportData

        public void RefreshReportData()
        {
            m_rugPending = Domain.Reports.RugPending.Find();
        }

        #endregion        
    }    
}
