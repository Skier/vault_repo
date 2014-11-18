using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.Reports;
using Dalworth.Server.MainForm.Reports;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.ReportFloodProduction
{
    public class ReportFloodProductionModel : IModel
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

        #region FloodProductions

        private BindingList<FloodProduction> m_floodProductions;
        public BindingList<FloodProduction> FloodProductions
        {
            get { return m_floodProductions; }
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

            m_floodProductions = new BindingList<FloodProduction>(
                FloodProduction.Find(startDate, endDate));
        }

        #endregion        
    }    
}
