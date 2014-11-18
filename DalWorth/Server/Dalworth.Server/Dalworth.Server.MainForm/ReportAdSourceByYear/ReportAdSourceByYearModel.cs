using System;
using System.ComponentModel;
using Dalworth.Server.Domain.Reports;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.ReportAdSourceByYear
{
    public class ReportAdSourceByYearModel : IModel
    {
        private int m_previousYear = 0;
        private int m_previousProjectTypeId = 0;

        #region Year

        private int m_year;
        public int Year
        {
            get {return  m_year; }
        }

        #endregion

        #region ProjectType

        private int m_projectTypeId;
        public string ProjectType
        {   
            get 
            {
                if (m_projectTypeId == 0)
                    return "All";
                else
                    return Enum.ToObject(typeof(ProjectTypeEnum), m_projectTypeId).ToString();
            }
        }

        #endregion

        #region AdSources

        private BindingList<AdSourceByYear> m_adSources;
        public BindingList<AdSourceByYear> AdSources
        {
            get { return m_adSources; }
        }

        #endregion

        #region Init

        public void Init()
        {
        }

        #endregion

        #region RefreshReportData

        public void RefreshReportData(int year, int projectTypeId)
        {
            if (year < 2000 || year > 2030)
                return;
            if (year == m_previousYear && projectTypeId == m_previousProjectTypeId)
                return;

            m_year = year;
            m_projectTypeId = projectTypeId;

            m_previousYear = year;
            m_previousProjectTypeId = projectTypeId;

            m_adSources = new BindingList<AdSourceByYear>(
                AdSourceByYear.Find(year, projectTypeId));
        }

        #endregion
    }
}
