using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.Reports;
using Dalworth.Server.MainForm.Reports;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.ReportRugReadyAging
{
    public class ReportRugReadyAgingModel : IModel
    {
        #region RugReadyAging

        private List<RugReadyAging> m_rugReadyAging;
        public List<RugReadyAging> RugReadyAging
        {
            get { return m_rugReadyAging; }
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
            m_rugReadyAging = new List<RugReadyAging>();
            m_rugReadyAging.Add(Domain.Reports.RugReadyAging.Find(0, 5));
            m_rugReadyAging.Add(Domain.Reports.RugReadyAging.Find(6, 20));
            m_rugReadyAging.Add(Domain.Reports.RugReadyAging.Find(21, int.MaxValue));            
        }

        #endregion        
    }        

    public class RugReadyAgingSingle
    {
        public RugReadyAgingSingle(IList<RugReadyAging> data)
        {
            m_days5Qty = data[0].RugsCount;
            m_days5Amt = data[0].EstimatedClosedAmount;

            m_days20Qty = data[1].RugsCount;
            m_days20Amt = data[1].EstimatedClosedAmount;

            m_daysMore20Qty = data[2].RugsCount;
            m_daysMore20Amt = data[2].EstimatedClosedAmount;
        }

        private int m_days5Qty;
        private decimal m_days5Amt;

        private int m_days20Qty;
        private decimal m_days20Amt;

        private int m_daysMore20Qty;
        private decimal m_daysMore20Amt;


        public int Days5Qty
        {
            get { return m_days5Qty; }
            set { m_days5Qty = value; }
        }

        public decimal Days5Amt
        {
            get { return m_days5Amt; }
            set { m_days5Amt = value; }
        }

        public int Days20Qty
        {
            get { return m_days20Qty; }
            set { m_days20Qty = value; }
        }

        public decimal Days20Amt
        {
            get { return m_days20Amt; }
            set { m_days20Amt = value; }
        }

        public int DaysMore20Qty
        {
            get { return m_daysMore20Qty; }
            set { m_daysMore20Qty = value; }
        }

        public decimal DaysMore20Amt
        {
            get { return m_daysMore20Amt; }
            set { m_daysMore20Amt = value; }
        }
    }
}
