using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.Reports;
using Dalworth.Server.MainForm.Reports;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.ReportTechnicianProduction
{
    public class ReportTechnicianProductionModel : IModel
    {
        #region TechnicianProductions

        private BindingList<TechnicianProduction> m_technicianProductions;
        public BindingList<TechnicianProduction> TechnicianProductions
        {
            get { return m_technicianProductions; }
        }

        #endregion

        #region Technicians

        private List<Employee> m_technicians;
        public List<Employee> Technicians
        {
            get { return m_technicians; }
        }

        #endregion

        #region Init

        public void Init()
        {
            m_technicians = Employee.FindRealTechnicians();
        }

        #endregion

        #region RefreshReportData

        public void RefreshReportData(Employee technician, DateTime startDate, DateTime endDate)
        {            
            m_technicianProductions = new BindingList<TechnicianProduction>(
                TechnicianProduction.Find(technician, startDate, endDate));
        }

        #endregion

        #region GetPrintedData

        public List<TechnicianProductionNotNull> GetPrintedData()
        {
            List<TechnicianProductionNotNull> data = new List<TechnicianProductionNotNull>();

            foreach (TechnicianProduction technicianProduction in m_technicianProductions)
                data.Add(new TechnicianProductionNotNull(technicianProduction));

            return data;
        }

        #endregion
    }    
}
