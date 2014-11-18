using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.Reports;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.ReportConstructionSummary
{
    public class ReportConstructionSummaryModel : IModel
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

        #region ConstructionSummaries

        private BindingList<ConstructionSummary> m_constructionSummaries;
        public BindingList<ConstructionSummary> ConstructionSummaries
        {
            get { return m_constructionSummaries; }
        }

        #endregion

        #region ProjectType

        private int m_projectTypeId;
        public ProjectTypeEnum ProjectType
        {
            get { return (ProjectTypeEnum)m_projectTypeId; }
            set { m_projectTypeId = (int)value; }
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

            m_constructionSummaries = new BindingList<ConstructionSummary>(
                ConstructionSummary.Find(startDate, endDate, m_projectTypeId));
        }

        #endregion
    }
}
