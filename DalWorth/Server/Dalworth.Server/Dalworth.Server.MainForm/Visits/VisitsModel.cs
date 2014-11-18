using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Dalworth.Server.Domain;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using Task=Dalworth.Server.Domain.Task;

namespace Dalworth.Server.MainForm.Visits
{
    public class VisitsModel : IModel
    {
        #region Visits

        private BindingList<VisitWrapper> m_visits;
        public BindingList<VisitWrapper> Visits
        {
            get { return m_visits; }
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

            UpdateVisits(null, string.Empty, string.Empty, string.Empty, string.Empty, 
                string.Empty, string.Empty, null, null);
        }

        #endregion

        #region GetTasks

        public BindingList<TaskWrapper> GetTasks(Visit visit)
        {
            List<Task> tasks = Task.FindByVisit(visit);
            tasks = Task.GetTasksFiltered(tasks, visit);

            Dictionary<int, WorkTransactionTask> workTransactionTaskMap = new Dictionary<int, WorkTransactionTask>();
            if (visit.VisitStatus == VisitStatusEnum.Completed)
            {
                WorkTransaction transaction = WorkTransaction.FindBy(
                    visit.ID, WorkTransactionTypeEnum.VisitCompleted);
                List<WorkTransactionTask> workTransactionTasks = WorkTransactionTask.FindBy(transaction);

                foreach (WorkTransactionTask workTransactionTask in workTransactionTasks)
                    workTransactionTaskMap.Add(workTransactionTask.TaskId, workTransactionTask);
            }

            List<TaskWrapper> result = new List<TaskWrapper>();
            foreach (Task task in tasks)
            {
                TaskWrapper wrapper;

                if (visit.VisitStatus == VisitStatusEnum.Completed)
                    wrapper = new TaskWrapper(task, workTransactionTaskMap[task.ID]);
                else
                    wrapper = new TaskWrapper(task, null);      
          
                result.Add(wrapper);
            }

            return new BindingList<TaskWrapper>(result);
        }

        #endregion

        #region UpdateVisits

        public void UpdateVisits(int? exactVisitId, 
            string servmanTicket, string firstName, string lastName, string phoneNo,
            string city, string zip, string street, string block, 
            VisitStatusEnum? status, int? technicianId, DateRange dateRange)
        {
            if (exactVisitId != null)
            {
                m_visits = Visit.FindVisitWrappers(exactVisitId);
                return;
            }

            if (servmanTicket.Trim() == string.Empty
                && firstName.Trim() == string.Empty
                && lastName.Trim() == string.Empty
                && phoneNo.Trim() == string.Empty
                && city.Trim() == string.Empty
                && zip.Trim() == string.Empty
                && street.Trim() == string.Empty
                && block.Trim() == string.Empty
                && status == null
                && technicianId == null 
                && (dateRange == null || dateRange.IsNull))
            {
                m_visits.Clear();

            }
            else
            {
                m_visits = Visit.FindVisitWrappers(null, servmanTicket,
                    firstName, lastName, phoneNo, 
                    city, zip, street, block, 
                    status, technicianId, dateRange);
            }
        }

        public void UpdateVisits(int? exactVisitId, string visitId, string servmanTicket, string mapsco, 
            string customer, string address, string technician, VisitStatusEnum? status, DateRange dateRange)
        {
            if (exactVisitId != null)
            {
                m_visits = Visit.FindVisitWrappers(exactVisitId, null, string.Empty, string.Empty,
                    string.Empty, string.Empty, string.Empty, string.Empty, null, null);
                return;
            }

            if (visitId.Trim() == string.Empty 
                && servmanTicket.Trim() == string.Empty
                && mapsco.Trim() == string.Empty
                && customer.Trim() == string.Empty && address.Trim() == string.Empty
                && technician.Trim() == string.Empty && status == null
                && (dateRange == null || dateRange.IsNull))
            {
                m_visits = new BindingList<VisitWrapper>();

            } else
            {
                m_visits = Visit.FindVisitWrappers(null, null, visitId, servmanTicket, mapsco,
                    customer, address, technician, status, dateRange);                
            }
        }

        #endregion
    }

    public class TaskWrapper
    {
        private Task m_task;
        private WorkTransactionTask m_workTransactionTask;

        #region Task

        public Task Task
        {
            get { return m_task; }
        }

        #endregion

        #region Constructor

        public TaskWrapper(Task task, WorkTransactionTask workTransactionTask)
        {
            m_task = task;
            m_workTransactionTask = workTransactionTask;
        }

        #endregion

        #region Number

        public string Number
        {
            get { return m_task.Number; }
        }

        #endregion

        #region IsReady

        public bool IsReady
        {
            get { return m_task.IsReady; }
        }

        #endregion

        #region TaskTypeText

        public string TaskTypeText
        {
            get { return m_task.TaskTypeText; }
        }

        #endregion

        #region TaskStatusText

        public string TaskStatusText
        {
            get
            {
                if (m_workTransactionTask == null)
                    return m_task.TaskStatusText;

                if (m_workTransactionTask.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.Complete)
                    return "Completed";

                if (m_workTransactionTask.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.InProcess)
                    return "Completed";

                TaskFailTypeEnum? failType = null;

                if (m_workTransactionTask.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.FailMustReturn)
                    failType = TaskFailTypeEnum.MustReturn;
                if (m_workTransactionTask.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.Cancel)
                    failType = TaskFailTypeEnum.Cancel;

                if (!failType.HasValue)
                    throw new DalworthException("Unexpected task status");

                return TaskFailType.GetText(failType.Value);
            }
        }

        #endregion

        #region ProjectLink

        public string ProjectLink
        {
            get { return "Project"; }
        }

        #endregion

        #region ServmanNumber

        public string ServmanNumber
        {
            get { return m_task.ServmanOrderNum; }
        }

        #endregion
    }
}
