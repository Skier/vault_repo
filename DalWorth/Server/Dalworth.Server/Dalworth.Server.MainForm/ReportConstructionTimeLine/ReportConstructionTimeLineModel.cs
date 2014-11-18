using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.Reports;
using Dalworth.Server.MainForm.Reports;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.ReportConstructionTimeLine
{
    public class ReportConstructionTimeLineModel : IModel
    {
        #region ConstructionTimeLines

        private BindingList<ConstructionTimeLine> m_constructionTimeLines;
        public BindingList<ConstructionTimeLine> ConstructionTimeLines
        {
            get { return m_constructionTimeLines; }
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
            m_constructionTimeLines = new BindingList<ConstructionTimeLine>(
                ConstructionTimeLine.Find(month, year));
        }

        #endregion

        #region GetPrintedData

        public List<ConstructionTimeLineNotNull> GetPrintedData()
        {
            List<ConstructionTimeLineNotNull> result = new List<ConstructionTimeLineNotNull>();

            foreach (ConstructionTimeLine item in m_constructionTimeLines)
                result.Add(new ConstructionTimeLineNotNull(item));

            return result;
        }

        #endregion
    }    
}
