using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.Reports;
using Dalworth.Server.MainForm.Reports;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.ReportConstructionLead
{
    public class ReportConstructionLeadModel : IModel
    {
        #region ConstructionLeads

        private BindingList<ConstructionLead> m_constructionLeads;
        public BindingList<ConstructionLead> ConstructionLeads
        {
            get { return m_constructionLeads; }
        }

        #endregion

        #region Init

        public void Init()
        {
        }

        #endregion

        #region RefreshReportData

        public void RefreshReportData(int month, int year)
        {
            DateTime start = new DateTime(year, month, 1, 0, 0, 0);
            DateTime end = new DateTime(year, month, DateTime.DaysInMonth(year, month), 0, 0, 0);
            
            m_constructionLeads = new BindingList<ConstructionLead>(
                ConstructionLead.Find(start, end));
        }

        #endregion        
    }    
}
