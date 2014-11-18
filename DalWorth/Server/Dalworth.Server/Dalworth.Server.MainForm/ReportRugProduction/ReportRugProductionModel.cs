using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.Reports;
using Dalworth.Server.MainForm.Reports;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.ReportRugProduction
{
    public class ReportRugProductionModel : IModel
    {
        #region StartDate

        private DateTime m_startDate;
        public DateTime StartDate
        {
            get { return m_startDate; }
        }

        #endregion

        #region EndDate

        private DateTime m_endDate;
        public DateTime EndDate
        {
            get { return m_endDate; }
        }

        #endregion

        #region RugProductions

        private BindingList<RugProduction> m_rugProductions;
        public BindingList<RugProduction> RugProductions
        {
            get { return m_rugProductions; }
        }

        #endregion

        #region Init

        public void Init()
        {
        }

        #endregion

        #region RefreshReportData

        public void RefreshReportData(DateTime startDate, DateTime endDate)
        {
            m_startDate = startDate;
            m_endDate = endDate;

            m_rugProductions = new BindingList<RugProduction>(
                RugProduction.Find(startDate, endDate));
        }

        #endregion        
    }    
}
