using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Dalworth.Server.Domain.Reports;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.ReportRevenue
{
    public class ReportRevenueModel : IModel
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

        #region Revenues

        private BindingList<Revenue> m_revenues;
        public BindingList<Revenue> Revenues
        {
            get { return m_revenues; }
        }

        #endregion

        #region GrandTotal

        private decimal m_grandTotal;
        public decimal GrandTotal
        {
            get { return m_grandTotal; }
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

            m_grandTotal = 0;

            m_revenues = new BindingList<Revenue>(
                Revenue.Find(startDate, endDate));

            foreach(Revenue revenue in m_revenues)
            {
                m_grandTotal += revenue.RugsAmt;
                m_grandTotal += revenue.FloodsAmt;
                m_grandTotal += revenue.ConstructionAmt;
                m_grandTotal += revenue.ContentAmt;
            }
        }

        #endregion
    }
}
