using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.Works
{
    public class WorksModel : IModel
    {
        #region Works

        private BindingList<WorkWrapper> m_works;
        public BindingList<WorkWrapper> Works
        {
            get { return m_works; }
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

            UpdateWorks(null, string.Empty, null, string.Empty, string.Empty, 
                WorkStatusEnum.Pending, null);
        }

        #endregion

        #region UpdateWorks

        public void UpdateWorks(int? exactWorkId, string workId, int? technicianId, string dispatch,
            string van, WorkStatusEnum? status, DateRange dateRange)
        {
            if (exactWorkId != null)
            {
                m_works = Work.FindWorkWrappers(exactWorkId, workId, technicianId,
                    dispatch, van, status, dateRange);                
                return;
            }

            if (workId == string.Empty
                && technicianId == null
                && dispatch == string.Empty
                && van == string.Empty
                && status == null
                && (dateRange == null || dateRange.IsNull))
            {
                m_works = new BindingList<WorkWrapper>();
            } else
            {
                m_works = Work.FindWorkWrappers(null, workId, technicianId,
                    dispatch, van, status, dateRange);                
            }
        }

        #endregion

        #region FindVisits

        public BindingList<VisitWrapper> FindVisits(int workId)
        {
            return Visit.FindVisitWrappers(null, workId, null, null, null, null, null, null, null, null);                     
        }

        #endregion

    }
}
