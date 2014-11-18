using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.Reports;
using Dalworth.Server.MainForm.Reports;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.ReportConstructionManager
{
    public class ReportConstructionManagerModel : IModel
    {
        #region ConstructionManagers

        private BindingList<ConstructionManager> m_constructionManagers;
        public BindingList<ConstructionManager> ConstructionManagers
        {
            get { return m_constructionManagers; }
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
            m_constructionManagers = new BindingList<ConstructionManager>(
                ConstructionManager.Find(month, year));
        }

        #endregion        
    
        #region GetPrintedData

        public List<ConstructionManagerNotNull> GetPrintedData()
        {
            List<ConstructionManagerNotNull> result = new List<ConstructionManagerNotNull>();

            foreach (ConstructionManager constructionManager in m_constructionManagers)
                result.Add(new ConstructionManagerNotNull(constructionManager));                

            return result;
        }

        #endregion
    }    
}
