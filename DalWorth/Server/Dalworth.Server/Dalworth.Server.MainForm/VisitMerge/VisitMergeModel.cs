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
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;
using Dalworth.Server.MainForm.CreateVisit;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using Task=Dalworth.Server.Domain.Task;

namespace Dalworth.Server.MainForm.VisitMerge
{
    public class VisitMergeModel : IModel
    {
        #region MergedVisitsInput

        private List<Visit> m_mergedVisitsInput;
        public List<Visit> MergedVisitsInput
        {
            get { return m_mergedVisitsInput; }
            set { m_mergedVisitsInput = value; }
        }

        #endregion

        #region MainVisitInput

        private Visit m_mainVisitInput;
        public Visit MainVisitInput
        {
            get { return m_mainVisitInput; }
            set { m_mainVisitInput = value; }
        }

        #endregion

        #region MergedVisits

        private BindingList<VisitWrapper> m_mergedVisits;
        public BindingList<VisitWrapper> MergedVisits
        {
            get { return m_mergedVisits; }
            set { m_mergedVisits = value; }
        }

        #endregion

        #region Customer

        private Customer m_customer;
        public Customer Customer
        {
            get { return m_customer; }
        }

        #endregion

        #region IsDashboardVisitsMerge

        private bool m_isDashboardVisitsMerge;
        public bool IsDashboardVisitsMerge
        {
            get { return m_isDashboardVisitsMerge; }
        }

        #endregion        

        #region Init

        public void Init()
        {
            m_customer = Domain.Customer.FindByPrimaryKey(m_mainVisitInput.CustomerId.Value);

            m_mergedVisits = new BindingList<VisitWrapper>();
            foreach (Visit visit in m_mergedVisitsInput)
            {
                VisitWrapper wrapper;

                if (visit.VisitStatus != VisitStatusEnum.Pending)
                {
                    m_isDashboardVisitsMerge = true;
                    Work work = Work.FindByVisit(visit);
                    wrapper = new VisitWrapper(Task.FindByVisit(visit), 
                        visit, Employee.FindByPrimaryKey(work.TechnicianEmployeeId));                    

                } else
                {
                    wrapper = new VisitWrapper(
                        Task.FindByVisit(visit), visit);                    
                }
                    
                             
                m_mergedVisits.Add(wrapper);
            }
        }

        #endregion

        #region IsAnyVisitSelected

        public bool IsAnyVisitSelected()
        {
            foreach (VisitWrapper visit in m_mergedVisits)
            {
                if (visit.IsSelected)
                    return true;
            }

            return false;
        }

        #endregion

        #region GetSelectedVisits

        public List<Visit> GetSelectedVisits()
        {
            List<Visit> result = new List<Visit>();

            foreach (VisitWrapper visit in m_mergedVisits)
            {
                if (visit.IsSelected)
                    result.Add(visit.Visit);
            }

            return result;
        }

        #endregion

    }

    public class VisitWrapper
    {
        private List<Task> m_tasks;

        #region Visit

        private Visit m_visit;
        public Visit Visit
        {
            get { return m_visit; }
            set { m_visit = value; }
        }

        #endregion

        #region Technician

        private Employee m_technician;
        public Employee Technician
        {
            get { return m_technician; }
            set { m_technician = value; }
        }

        #endregion

        #region Constructor

        public VisitWrapper(List<Task> tasks, Visit visit)
        {
            m_tasks = tasks;
            m_visit = visit;
        }

        public VisitWrapper(List<Task> tasks, Visit visit, Employee technician) : this(tasks, visit)
        {
            m_technician = technician;
        }

        #endregion

        #region IsSelected

        private bool m_isSelected;
        public bool IsSelected
        {
            get { return m_isSelected; }
            set { m_isSelected = value; }
        }

        #endregion

        #region ID

        public int ID
        {
            get
            {
                return m_visit.ID;
            }
        }

        #endregion

        #region TasksText

        public string TasksText
        {
            get
            {                
                string result = string.Empty;

                foreach (Task task in m_tasks)
                    result += task.TaskTypeText + ", ";

                return result.TrimEnd(", ".ToCharArray());
            }
        }

        #endregion

        #region ServiceDate

        public string ServiceDate
        {
            get
            {
                if (m_visit.ServiceDate.HasValue)
                    return m_visit.ServiceDate.Value.ToShortDateString();
                return string.Empty;
            }
        }

        #endregion

        #region TimeFrame

        public string TimeFrame
        {
            get
            {
                return m_visit.TimeFrameText;
            }
        }

        #endregion

        #region Notes

        public string Notes
        {
            get
            {
                return m_visit.Notes;
            }
        }

        #endregion

        #region TechnicianName

        public string TechnicianName
        {
            get
            {
                if (m_technician == null)
                    return string.Empty;
                return m_technician.DisplayName;
            }
        }

        #endregion
    }
}
