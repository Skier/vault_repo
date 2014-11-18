using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Dalworth.Server.Controls;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;
using Dalworth.Server.MainForm.MainForm;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;
using Task=Dalworth.Server.Domain.Task;

namespace Dalworth.Server.MainForm.Dashboard
{
    public class DashboardModel : IModel
    {
        #region CurrentDate

        private DateTime? m_currentDate = null;
        private DateTime? m_previousCurrentDate = null;

        public DateTime CurrentDate
        {
            get { return m_currentDate.Value; }
            set
            {
                m_previousCurrentDate = m_currentDate;
                m_currentDate = value;
            }
        }

        #endregion 

        #region Dispatcher

        private Employee m_dispatcher;
        public Employee Dispatcher
        {
            get { return m_dispatcher; }
        }

        #endregion

        #region MainFormModel

        private MainFormModel m_mainFormModel;
        public MainFormModel MainFormModel
        {
            get { return m_mainFormModel; }
            set { m_mainFormModel = value; }
        }

        #endregion        

        #region PendingVisits

        private BindingList<VisitPackage> m_pendingVisits;
        public BindingList<VisitPackage> PendingVisits
        {
            get { return m_pendingVisits; }
            set { m_pendingVisits = value; }
        }

        #endregion        

        #region Appointments

        private BindingList<AppointmentWrapper> m_appointments;
        public BindingList<AppointmentWrapper> Appointments
        {
            get { return m_appointments; }
        }

        #endregion

        #region LeadsInfo

        private LeadsInfo m_leadsInfo;
        public LeadsInfo LeadsInfo
        {
            get { return m_leadsInfo; }
        }

        #endregion

        #region ProjectFeedbackCount

        private int m_ProjectFeedbackCount;
        public int ProjectFeedbackCount
        {
            get { return m_ProjectFeedbackCount; }
        }

        #endregion

        #region Resources

        private BindingList<ResourceWrapper> m_resources;
        public BindingList<ResourceWrapper> Resources
        {
            get { return m_resources; }
        }

        #endregion


        #region Init

        public void Init()
        {
            m_dispatcher = Employee.FindByPrimaryKey(Configuration.CurrentDispatchId);
            CurrentDate = DateTime.Now.Date;
            RefreshData(PendingVisitTypeEnum.Current, string.Empty);
        }

        #endregion

        #region CurrentDateChanged
        
        public bool IsCurrentDateChanged()
        {
            if (!m_previousCurrentDate.HasValue && m_currentDate.HasValue)
                return true;

            if (m_previousCurrentDate.HasValue && m_currentDate.HasValue &&  (m_previousCurrentDate != m_currentDate))
                return true;

            return false;
        }

        public void ResetCurrentDateChanged()
        {
            m_previousCurrentDate = m_currentDate;
        }

        #endregion

        #region RefreshAppointments

        public void RefreshAppointments(IDbConnection connection)
        {
            BindingList<AppointmentWrapper> appointments  = new BindingList<AppointmentWrapper>();

            List<TechnicianPackage> technicianPackages = TechnicianPackage.FindTechnicianPackages(
                CurrentDate, Configuration.CurrentDispatchId, connection);

            BindingList<ResourceWrapper> resources = new BindingList<ResourceWrapper>();
            Dictionary<int, string> equipmentMap = EquipmentTransactionDetail.FindVansEquipment(CurrentDate);
            
            int currentIndex = 0;
            foreach (TechnicianPackage package in technicianPackages)
            {
                for (int i = 0; i < package.Visits.Count; i++)
                {
                    VisitPackage visitPackage = package.Visits[i];
                    List<Task> tasks = new List<Task>();
                    foreach (TaskPackage task in visitPackage.Tasks)
                        tasks.Add(task.Task);

                    appointments.Add(new AppointmentWrapper(
                                           visitPackage.Visit, 
                                           package.WorkDetails[i], 
                                           package.Work,
                                           visitPackage.Customer,
                                           visitPackage.ServiceAddress,
                                           tasks));
                }

                ResourceWrapper resourceWrapper = new ResourceWrapper(                
                    package.Technician, package.Work, package.Van, currentIndex++);

                if (package.Van == null || !equipmentMap.ContainsKey(package.Van.ID))
                    resourceWrapper.VanEquipment = string.Empty;
                else
                    resourceWrapper.VanEquipment = equipmentMap[package.Van.ID];                    

                resources.Add(resourceWrapper);
            }

            if (IsCurrentDateChanged())
            {
                m_resources = resources;
                m_appointments = appointments;
                return;
            }

            if (m_appointments == null)
                m_appointments = appointments;
            else
                MergeAppointments(appointments);

            if (m_resources == null)
                m_resources = resources;
            else
                MergeResources(resources);
            
        }

        public void RefreshResources(DateTime date)
        {
            List<TechnicianPackage> technicianPackages = TechnicianPackage.FindTechnicianPackages(
                date, Configuration.CurrentDispatchId, null);
            m_resources = new BindingList<ResourceWrapper>();

            int currentIndex = 0;
            foreach (TechnicianPackage package in technicianPackages)
            {
                m_resources.Add(new ResourceWrapper(
                    package.Technician, package.Work, package.Van, currentIndex++));
            }            
        }

        public void RefreshProjectFeedbackCount (IDbConnection connection)
        {
            this.m_ProjectFeedbackCount = ProjectFeedback.GetNewCount();
        }

        public void RefreshLeads(IDbConnection connection)
        {
            m_leadsInfo = Lead.GetLeadsInfo(connection);
        }

        public void RefreshAppointments()
        {
            RefreshAppointments(null);
        }

        #endregion

        #region RefreshPendingVisits

        public void RefreshPendingVisits(PendingVisitTypeEnum visitType, string customerName, 
            IDbConnection connection)
        {
            PendingVisits = new BindingList<VisitPackage>(
                VisitPackage.FindVisits(null, VisitStatusEnum.Pending, visitType, customerName, connection));
        }

        public void RefreshPendingVisits(PendingVisitTypeEnum visitType, string customerName)
        {
            RefreshPendingVisits(visitType, customerName, null);
        }

        #endregion

        #region RefreshData

        public void RefreshData(PendingVisitTypeEnum visitType, string customerName,
            IDbConnection connection)
        {
            RefreshAppointments(connection);
            RefreshPendingVisits(visitType, customerName, connection);
            RefreshLeads(connection);
        }

        public void RefreshData(PendingVisitTypeEnum visitType, string customerName)
        {
            RefreshData(visitType, customerName, null);
        }

        #endregion

        #region SendVisitToPager

        public void SendVisitToPager(Work work, Visit visit)
        {
            Work freshWork = Work.FindByPrimaryKey(work.ID);
            Van van = Van.FindByPrimaryKey(freshWork.VanId.Value);
            VisitSummaryPackage summaryPackage = new VisitSummaryPackage(visit);

            while (true)
            {
                try
                {
                    PagerMessage.SendViaWebService(van.ServmanTruckId, summaryPackage.GetPagerMessageText());
                    return;
                }
                catch (Exception ex)
                {
                    Host.Trace("DashboardModel:SendVisitToPager", "Error: " + ex);

                    if (XtraMessageBox.Show("Unable to send pager message. Try again?", "Pager send error", 
                        MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }
            
        }

        #endregion

        #region IsSendMessageAllowed

        public bool IsSendMessageAllowed(int technicianId)
        {
            try
            {
                Work work = Work.FindWorkByTechAndDate(technicianId, DateTime.Now.Date);
                Employee technician = Employee.FindByPrimaryKey(technicianId);

                return IsSendMessageAllowed(work, technician);
            }
            catch (DataNotFoundException)
            {
                return false;
            }
        }

        public bool IsSendMessageAllowed(Work work, Employee technician)
        {
            if (work == null)
                return false;

            if (work.StartDate.Value.Date != DateTime.Now.Date)
                return false;

            if (work.WorkStatus == WorkStatusEnum.Completed || work.WorkStatus == WorkStatusEnum.Pending)
                return false;

            if (technician.IsUnknown)
                return false;

            return true;
        }

        #endregion

        #region UndoAllVisitOperations

        public void UndoAllVisitOperations(AppointmentWrapper wrapper)
        {
            while(wrapper.IsUndoVisitAllowed)
            {
                Visit.UndoLastOperation(wrapper.Visit, wrapper.Work);                
                wrapper.Visit = Visit.FindByPrimaryKey(wrapper.Visit.ID);                
                wrapper.Work = Work.FindByPrimaryKey(wrapper.Work.ID);                
            }
        }

        #endregion

        #region MergeAppointments

        private void MergeAppointments(BindingList<AppointmentWrapper> latestAppointments)
        {
            Dictionary<int, AppointmentWrapper> latestAppointmentDictionary = new Dictionary<int, AppointmentWrapper>();
            foreach (AppointmentWrapper latestAppointment in latestAppointments)
            {
                if (!latestAppointmentDictionary.ContainsKey(latestAppointment.Visit.ID))
                    latestAppointmentDictionary.Add(latestAppointment.Visit.ID, latestAppointment);
            }

            List<AppointmentWrapper> removalList = new List<AppointmentWrapper>();

            for (int i = 0; i < m_appointments.Count; i++)
            {
                AppointmentWrapper existingAppointment = m_appointments[i];

                if (!latestAppointmentDictionary.ContainsKey(existingAppointment.Visit.ID))
                {
                    removalList.Add(existingAppointment);
                    continue;
                }

                AppointmentWrapper latestAppointment = latestAppointmentDictionary[existingAppointment.Visit.ID];

                if (!existingAppointment.ValueEquals(latestAppointment))
                    m_appointments[i] = latestAppointment;

                latestAppointmentDictionary.Remove(latestAppointment.Visit.ID);
            }

            foreach (AppointmentWrapper appointment in removalList)
            {
                m_appointments.Remove(appointment);
            }

            foreach (AppointmentWrapper appointment in latestAppointmentDictionary.Values)
            {
                m_appointments.Add(appointment);
            }
        }

        #endregion 

        #region MergeResources

        private void MergeResources(BindingList<ResourceWrapper> latestResources)
        {
            if (latestResources.Count != m_resources.Count)
            {
                m_resources.Clear();
                foreach (ResourceWrapper latestResource in latestResources)
                    m_resources.Add(latestResource);
                return;
            }

            for (int i = 0; i < m_resources.Count; i++)
            {
                if (m_resources[i].ID != latestResources[i].ID)
                {
                    m_resources.Clear();
                    foreach (ResourceWrapper latestResource in latestResources)
                        m_resources.Add(latestResource);
                    return;
                }
            }

            for (int i = 0; i < m_resources.Count; i++)
            {
                if (!m_resources[i].ValueEquals(latestResources[i]))
                    m_resources[i] = latestResources[i];
            }        
        }
        #endregion 
    }

    public class AppointmentWrapper : ICloneable
    {
        #region Private Members

        private bool m_isQbFinalized;
        private Visit   m_visit;
        private WorkDetail m_workDetail;
        private Work m_work;
        private Customer m_customer;
        private Address m_serviceAddress;
        private List<Task> m_tasks;
        private int? m_newTechnicianId;
        private Employee m_employeePerformedStartDay;

        #endregion

        #region IsTechnicianUnknown

        private static List<int> m_unknownTechnicianIds;

        private static bool IsTechnicianUnknown(int technicianId)
        {
            if (m_unknownTechnicianIds == null)
            {
                List<Employee> unknownTechnicians = Employee.FindUnknownTechnicians();
                m_unknownTechnicianIds = new List<int>();
                foreach (Employee technician in unknownTechnicians)
                    m_unknownTechnicianIds.Add(technician.ID);
            }

            return m_unknownTechnicianIds.Contains(technicianId);
        }

        #endregion

        #region ValueEquals

        public bool ValueEquals(AppointmentWrapper obj)
        {
            if (obj == null) return false;
               
            if (ReferenceEquals(this, obj))
                return true;

            if (m_isQbFinalized != obj.m_isQbFinalized)
                return false;

            if (m_visit != null) 
            {
                if (!m_visit.ValueEquals(obj.m_visit))
                return false;
            }
            else
            {
                if (obj.m_visit != null)
                    return false;
            }

            if (m_workDetail != null)
            {
                if(!m_workDetail.ValueEquals(obj.m_workDetail))
                    return false;
            }
            else
            {
                    if (obj.m_workDetail != null)
                        return false;
            }

            if (m_work != null)
            {   
                if (!m_work.ValueEquals(obj.m_work))
                return false;
            }
            else
            {
                if (obj.m_workDetail != null)
                    return false;
            }

            if (m_customer != null)
            {
                if (!m_customer.ValueEquals(obj.m_customer))
                return false;
            }
            else
            {
                if (obj.m_customer != null)
                    return false;
            }

            if (m_serviceAddress != null)
            {
                if (!m_serviceAddress.ValueEquals(obj.m_serviceAddress))
                    return false;
            }
            else
            {
                if (obj.m_serviceAddress != null)
                    return false;
            }

            if (m_newTechnicianId != obj.m_newTechnicianId)
                return false;

            if (m_employeePerformedStartDay != null)
            {
                if (!m_employeePerformedStartDay.ValueEquals(obj.m_employeePerformedStartDay))
                    return false;
            }
            else
            {
                if (obj.m_employeePerformedStartDay != null)
                    return false;
            }
                
            if ((m_tasks == null && obj.m_tasks != null) || (m_tasks != null && obj.m_tasks == null))
                return false;

            if (m_tasks != null)
            {
                if (m_tasks.Count != obj.m_tasks.Count)
                    return false;

                for (int i = 0; i < m_tasks.Count; i++)
                {
                    if (!m_tasks[i].ValueEquals(obj.m_tasks[i]))
                        return false;
                }
            }

            return true;

        }
        #endregion

        #region Clone

        public object Clone()
        {
            List<Task> clonedTasks = new List<Task>();
            foreach (Task task in m_tasks)
                clonedTasks.Add((Task)task.Clone());

            AppointmentWrapper result = new AppointmentWrapper(
                (Visit)m_visit.Clone(),
                (WorkDetail)m_workDetail.Clone(),
                m_work == null ? null : (Work)m_work.Clone(),
                m_customer == null? null : (Customer)m_customer.Clone(),
                m_serviceAddress == null ? null : (Address)m_serviceAddress.Clone(),
                clonedTasks);
            return result;
        }

        #endregion

        #region Constructor

        public AppointmentWrapper(Visit visit, WorkDetail workDetail, Work work, Customer customer, 
            Address serviceAddress, List<Task> tasks)
        {
            m_visit = visit;
            m_workDetail = workDetail;
            m_work = work;
            m_customer = customer;
            m_serviceAddress = serviceAddress;
            m_tasks = tasks;

            Dictionary<int, Project> projects = new Dictionary<int, Project>();
            foreach (Task task in tasks)
            {
                if (!projects.ContainsKey(task.ProjectId))
                {
                    Project project = Project.FindByPrimaryKey(task.ProjectId);
                    projects.Add(project.ID, project);
                }
            }

            m_isQbFinalized = IsQbFinalized(visit, tasks, projects);
        }

        #endregion

        #region Refresh

        public void Refresh()
        {
            m_visit = Visit.FindByPrimaryKey(m_visit.ID);
            m_workDetail = WorkDetail.FindByPrimaryKey(m_workDetail.ID);
            m_work = Work.FindByPrimaryKey(m_work.ID);
        }

        #endregion

        #region EmployeePerformedStartDay

        public Employee EmployeePerformedStartDay
        {
            get
            {
                if (m_work == null)
                    m_employeePerformedStartDay = null;
                else if (m_employeePerformedStartDay == null)
                    m_employeePerformedStartDay = Employee.FindEmployeePerformedStartDay(m_work);
                return m_employeePerformedStartDay;
            }
        }

        #endregion

        #region Visit

        public Visit Visit
        {
            get { return m_visit; }
            set { m_visit = value; }
        }

        #endregion

        #region WorkDetail

        public WorkDetail WorkDetail
        {
            get { return m_workDetail; }
            set { m_workDetail = value; }
        }

        #endregion

        #region Work

        public Work Work
        {
            get { return m_work; }
            set { m_work = value; }
        }

        #endregion

        #region Customer

        public Customer Customer
        {
            get { return m_customer; }
        }

        #endregion

        #region ServiceAddress

        public Address ServiceAddress
        {
            get { return m_serviceAddress; }
        }

        #endregion

        #region Tasks

        public List<Task> Tasks
        {
            get { return m_tasks; }
        }

        #endregion

        #region IsOutOfConfirmedTimeFrame

        public bool IsOutOfConfirmedTimeFrame
        {
            get
            {
                if (m_visit.IsConfirmed
                    && (m_workDetail.TimeBegin.AddMinutes(30) < m_visit.ConfirmedFrameBegin.Value
                        || m_workDetail.TimeBegin.AddMinutes(30) > m_visit.ConfirmedFrameEnd.Value))
                {
                    return true;
                }

                return false;
            }
        }

        #endregion

        #region Description

        public string Description
        {
            get
            {
                return m_visit.VisitStatusText;
            }
        }

        #endregion

        #region Start & End

        public DateTime Start
        {
            get { return m_workDetail.TimeBegin; }
            set
            {
                m_workDetail.TimeBegin = value;
            }
        }

        public DateTime End
        {
            get { return m_workDetail.TimeEnd; }
            set { m_workDetail.TimeEnd = value; }
        }

        #endregion

        #region Label

        public int Label
        {
            get { return m_visit.VisitStatusId; }
        }

        #endregion

        #region ResourceId

        public int ResourceId
        {
            get { return m_work.TechnicianEmployeeId; }
            set { m_newTechnicianId = value; }
        }

        #endregion

        #region Status

        public int Status
        {
            get { return IsOutOfConfirmedTimeFrame ? 3 : 2; }
        }

        #endregion        

        #region Subject

        public string Subject
        {
            get
            {
                string result = string.Empty;

                if (m_visit.IsConfirmed)
                    result += m_visit.ConfirmedTimeFrameShortText;
                else
                {
                    DateTime suggestedConfirmStart = Visit.GetSuggestedConfirmTimeStart(
                        m_workDetail.TimeBegin.AddMinutes(30));

                    result += Visit.GetTimeFrameShortText(
                        suggestedConfirmStart, suggestedConfirmStart.AddHours(2));
                }
                               
                if (m_serviceAddress.Map.Trim() != string.Empty)
                    result += ", " + m_serviceAddress.Map.Trim();

                if (m_serviceAddress.City.Trim() != string.Empty)
                    result += ", " + m_serviceAddress.City.Trim().ToUpper();

                result += "\n" + m_visit.ID + " - ";
                result += (m_customer == null ? "[No Customer]" : m_customer.DisplayName) + "\n";

                result += m_workDetail.TimeBegin.ToShortTimeString() + "-" + m_workDetail.TimeEnd.ToShortTimeString() + " ";
                if (m_workDetail.TimeArrive.HasValue)
                    result += "ARR-" + m_workDetail.TimeArrive.Value.ToShortTimeString();

                result += "\n";                                               
                result += m_visit.GetTaskTypes(m_tasks);

                return result;
            }
        }

        #endregion

        #region Type

        public AppointmentType Type
        {
            get { return AppointmentType.Normal; }
        }

        #endregion

        #region IsConfirmed

        public bool IsConfirmed
        {
            get { return m_visit.IsConfirmed; }
        }

        #endregion

        #region ToolTip

        public string ToolTipTitle
        {
            get
            {
                return "Visit #" + m_visit.ID + ", "
                  + m_workDetail.TimeBegin.ToShortTimeString() 
                  + "-" + m_workDetail.TimeEnd.ToShortTimeString(); ;
            }
        }

        public ToolTipIconType ToolTipIcon
        {
            get
            {
                return ToolTipIconType.Information;
            }
        }

        public string ToolTipText
        {
            get
            {
                string result = string.Empty;
                m_visit.Init();
                if (!string.IsNullOrEmpty(m_visit.AdvertisingSource))
                    result += "Ad: " + m_visit.AdvertisingSource + "\n";

                if(!string.IsNullOrEmpty(m_visit.SalesRep))
                    result += "Sales Rep: " + m_visit.SalesRep + "\n";

                if (!string.IsNullOrEmpty(m_visit.AdvertisingSource) || !string.IsNullOrEmpty(m_visit.SalesRep))
                    result += "\n";

                string addressFirstLine = m_serviceAddress == null ? string.Empty : m_serviceAddress.AddressFirstLine;
                string addressSecondLine = m_serviceAddress == null ? string.Empty : m_serviceAddress.AddressSecondLine;

                if (m_customer != null)
                {
                    result += m_customer.DisplayName.Trim().ToUpper();

                    if (m_customer.PhonesText != string.Empty)
                        result += "\n" + m_customer.PhonesText;
                }                    

                if (addressFirstLine != string.Empty)
                    result += "\n" + addressFirstLine.Trim();
                if (addressSecondLine != string.Empty)
                    result += "\n" + addressSecondLine.Trim();
                
                result += "\n";

                if (m_visit.PreferedTimeFrom.HasValue || m_visit.PreferedTimeTo.HasValue)
                    result += "\nPreffered time frame:  " + m_visit.TimeFrameText;
                if (m_visit.IsConfirmed)
                    result += "\nConfirmed time frame: " + m_visit.ConfirmedTimeFrameText;
                if (m_serviceAddress != null && m_serviceAddress.Map != string.Empty)
                    result += "\nMap:                             " + m_serviceAddress.Map.Trim();

                result += "\nTask(s):                        " + m_visit.GetTaskTypes(m_tasks);
                result += "\nStatus:                         " + m_visit.VisitStatusText;

                foreach (Task task in m_tasks)
                {
                    if (task.TaskType == TaskTypeEnum.RugPickup
                        && task.TaskStatus == TaskStatusEnum.Completed)
                    {
                        if (task.Project.ProjectType == ProjectTypeEnum.RugCleaning)
                            result += "\nPU Estimate:                " + task.EstimatedClosedAmount.ToString("C");
                        else if (task.Project.ProjectType == ProjectTypeEnum.Deflood)
                            result += "\nPU on Deflood Amt:     " + task.ClosedAmount.ToString("C");

                    }
                    else if (task.TaskType == TaskTypeEnum.RugDelivery
                        && task.Project.ProjectType == ProjectTypeEnum.RugCleaning)
                    {
                        if (task.TaskStatus == TaskStatusEnum.Completed)
                            result += "\nDelivery Closed Amt:   " + task.ClosedAmount.ToString("C");
                        else
                            result += "\nPU Estimate:                " + task.EstimatedClosedAmount.ToString("C");                            
                    }                    
                }

                
                if (m_visit.VisitStatus == VisitStatusEnum.Completed)
                    result += "\nClosed Amt:                 " + m_visit.ClosedDollarAmount.ToString("C");                    

                if (m_visit.Notes.Trim() != string.Empty)
                {
                    result += "\n";
                    List<string> noteList = Utils.DivideText(m_visit.Notes.Trim(), 50);
                    foreach (string note in noteList)
                        result += "\n" + note;
                }

                return result;                
            }
        }


        #endregion        

        #region IsInThePast

        public bool IsInThePast
        {
            get
            {
                if (m_workDetail.TimeBegin < DateTime.Now)
                    return true;
                return false;
            }
        }

        #endregion

        #region DurationCorrected

        public TimeSpan DurationCorrected
        {
            get
            {
                if (m_visit.DurationMin == null || m_visit.DurationMin == 0)
                    return new TimeSpan(1, 0, 0);
                else
                    return new TimeSpan(0, m_visit.DurationMin.Value, 0);
            }
        }

        #endregion

        #region Save
        //returns list of resources that should be updated
        public List<ResourceWrapper> Save(int dispatchId)
        {
            List<ResourceWrapper> result = new List<ResourceWrapper>();

            try
            {                
                Database.Begin(IsolationLevel.Serializable);

                Visit visit = Visit.FindByPrimaryKey(m_visit.ID);
                if (!visit.IsEditAllowed)
                    throw new DataOutdatedException("Visit cannot be updated");

                if (m_newTechnicianId != null && m_newTechnicianId != ResourceId)//Assignment to new tech
                {
                    Work sourceTechWork
                        = Work.FindWorkByTechAndDate(ResourceId, m_work.StartDate.Value);
                    Work targetTechWork
                        = Work.FindWorkByTechAndDate(m_newTechnicianId.Value, m_work.StartDate.Value);

                    Employee sourceTech = Employee.FindByPrimaryKey(ResourceId);
                    Employee targetTech = Employee.FindByPrimaryKey(m_newTechnicianId.Value);

                    Van sourceTechVan = null;
                    Van targetTechVan = null;

                    if (sourceTechWork.VanId.HasValue)
                        sourceTechVan = Van.FindByPrimaryKey(sourceTechWork.VanId.Value);
                    

                    //Process target
                    if (targetTechWork == null)
                    {
                        targetTechWork = new Work(0,
                            dispatchId,
                            m_newTechnicianId.Value,
                            null,
                            m_work.StartDate.Value,
                            0,
                            string.Empty, string.Empty, string.Empty, 
                            false,
                            DateTime.Now, null, null, decimal.Zero);
                        targetTechWork.WorkStatus = WorkStatusEnum.Pending;
                        Work.Insert(targetTechWork);
                    }
                    else if (targetTechWork.VanId.HasValue)
                    {                        
                        targetTechVan = Van.FindByPrimaryKey(targetTechWork.VanId.Value);                        
                    }

                    m_workDetail.WorkId = targetTechWork.ID;
                    WorkDetail.UpdateAndLog(m_workDetail);

                    //Process source
                    List<WorkDetail> sourceTechWorkDetails
                        = WorkDetail.FindBy(sourceTechWork);

                    if (sourceTechWork.WorkStatus == WorkStatusEnum.Pending
                        && sourceTechWorkDetails.Count == 0) //This was last visit assigned to this work
                    {
                        Work.Delete(sourceTechWork);
                        sourceTechWork = null;
                    }
                        
                    m_work = targetTechWork;
                    m_newTechnicianId = null;
                    
                    result.Add(new ResourceWrapper(sourceTech, sourceTechWork, sourceTechVan, -1));
                    result.Add(new ResourceWrapper(targetTech, targetTechWork, targetTechVan, -1));
                }
                WorkDetail.UpdateAndLog(m_workDetail);
                Database.Commit();               
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }

            return result;
        }

        #endregion

        #region IsWorkExist

        public bool IsWorkExist
        {
            get { return m_work != null; }
        }

        #endregion

        #region IsUnassignAllowed

        public bool IsUnassignAllowed
        {
            get
            {
                return m_visit.VisitStatus == VisitStatusEnum.Assigned
                    && !m_visit.IsConfirmed;
            }
        }

        #endregion

        #region IsRescheduleAllowed

        public bool IsRescheduleAllowed
        {
            get
            {
                return m_visit.IsRescheduleAllowed;
            }
        }

        #endregion

        #region IsDispatchRedispatchVisitAllowed

        public bool IsDispatchRedispatchVisitAllowed
        {
            get
            {
                return (IsWorkExist    
                        && !IsTechnicianUnknown(m_work.TechnicianEmployeeId)
                        && m_work.StartDate.Value.Date == DateTime.Now.Date);
            }
        }

        #endregion

        #region DispatchRedispatchMenuText

        public string DispatchRedispatchMenuText
        {
            get
            {
                if (!IsDispatchRedispatchVisitAllowed)
                    return "Dispatch";

                if (m_workDetail.TimeDispatch.HasValue)
                    return "Edit Dispatch";
                return "Dispatch";
            }
        }

        #endregion

        #region ConfirmReconfirmMenuText

        public string ConfirmReconfirmMenuText
        {
            get
            {
                return IsConfirmed ? "Reconfirm" : "Confirm";
            }
        }

        #endregion        

        #region IsConfirmReconfirmVisitAllowed

        public bool IsConfirmReconfirmVisitAllowed
        {
            get
            {
                return (IsWorkExist
                    && (m_visit.VisitStatus == VisitStatusEnum.Assigned
                        || m_visit.VisitStatus == VisitStatusEnum.AssignedForExecution)
                    && m_work.StartDate.Value.Date >= DateTime.Now.Date);
            }
        }

        #endregion        

        #region IsArriveRearriveVisitAllowed

        public bool IsArriveRearriveVisitAllowed
        {
            get
            {
                return (IsWorkExist
                    && !IsTechnicianUnknown(m_work.TechnicianEmployeeId)
                    && m_work.StartDate.Value.Date == DateTime.Now.Date);
            }
        }

        #endregion        

        #region ArriveRearriveMenuText

        public string ArriveRearriveMenuText
        {
            get
            {
                if (!IsArriveRearriveVisitAllowed)
                    return "Arrive";

                if (m_workDetail.TimeArrive.HasValue)
                    return "Edit Arrive";
                return "Arrive";
            }
        }

        #endregion        

        #region IsSubminEtcVisitAllowed

        public bool IsSubmitEtcVisitAllowed
        {
            get
            {
                return (IsWorkExist
                    && m_visit.VisitStatus == VisitStatusEnum.Arrived
                    && m_work.StartDate.Value.Date == DateTime.Now.Date);
            }
        }

        #endregion

        #region IsCompleteRecompleteVisitAllowed

        public bool IsCompleteRecompleteVisitAllowed
        {
            get
            {
                return ( IsWorkExist
                        && (m_work.WorkStatus == WorkStatusEnum.StartDayDone
                            || m_work.WorkStatus == WorkStatusEnum.Completed)
                        && (
                           (m_work.StartDate.Value.Date == DateTime.Now.Date)
                           ||
                           (m_work.StartDate.Value.Date < DateTime.Now.Date)
                           ));
            }
        }

        #endregion

        #region CompleteRecompleteMenuText

        public string CompleteRecompleteMenuText
        {
            get
            {
                if (!IsCompleteRecompleteVisitAllowed)
                    return "Complete";

                if (m_workDetail.TimeComplete.HasValue)
                    return "Edit Complete";
                return "Complete";
            }
        }

        #endregion

        #region IsTimeAssignmentAllowed

        public bool IsTimeAssignmentAllowed
        {
            get
            {
                if (m_workDetail.TimeBegin.Date <= DateTime.Now.Date
                    && m_work != null)
                {
                    if (m_work.StartDayDate.HasValue 
                        && m_workDetail.TimeBegin < m_work.StartDayDate)
                    {
                        return false;
                    }

                    if (m_work.EndDayDate.HasValue
                        && m_workDetail.TimeEnd > m_work.EndDayDate)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        #endregion


        #region IsUndoVisitAllowed

        public bool IsUndoVisitAllowed
        {
            get
            {
                return !m_isQbFinalized && (
                        IsConfirmed
                       || m_visit.VisitStatus == VisitStatusEnum.AssignedForExecution
                       || m_visit.VisitStatus == VisitStatusEnum.Arrived
                       || m_visit.VisitStatus == VisitStatusEnum.Completed);
            }
        }

        #endregion

        #region CurrentVisitUndoOperation

        public VisitUndoOperationEnum CurrentVisitUndoOperation
        {
            get
            {
                if (!IsUndoVisitAllowed)
                    return VisitUndoOperationEnum.Unavailable;

                if (m_visit.VisitStatus == VisitStatusEnum.Completed)
                    return VisitUndoOperationEnum.Complete;
                if (m_visit.VisitStatus == VisitStatusEnum.Arrived)
                    return VisitUndoOperationEnum.Arrive;
                if (m_visit.VisitStatus == VisitStatusEnum.AssignedForExecution)
                    return VisitUndoOperationEnum.Dispatch;
                if (IsConfirmed)
                    return VisitUndoOperationEnum.Confirm;

                throw new DalworthException(string.Empty);
            }
        }

        #endregion

        #region CurrentVisitUndoMenuText

        public string CurrentVisitUndoMenuText
        {
            get
            {
                VisitUndoOperationEnum operation = CurrentVisitUndoOperation;

                if (operation == VisitUndoOperationEnum.Unavailable)
                    return "Undo";
                if (operation == VisitUndoOperationEnum.Complete)
                    return "Undo Complete";
                if (operation == VisitUndoOperationEnum.Arrive)
                    return "Undo Arrive";
                if (operation == VisitUndoOperationEnum.Dispatch)
                    return "Undo Dispatch";
                if (operation == VisitUndoOperationEnum.Confirm)
                    return "Undo Confirm";

                throw new DalworthException(string.Empty);
            }
        }

        #endregion

        #region IsContainsRugDelivery

        public bool IsContainsRugDelivery()
        {
            foreach (Task task in m_tasks)
            {
                if (task.TaskType == TaskTypeEnum.RugDelivery)
                    return true;
            }
            return false;
        }

        #endregion        

        #region IsQbFinalized

        private bool IsQbFinalized(Visit visit, List<Task> tasks, Dictionary<int, Project> projects)
        {
            bool result = false;

            foreach (Project project in projects.Values)
            {
                if (project.ProjectType == ProjectTypeEnum.Deflood)
                {
                    Task firstDefloodTask = tasks.Find(delegate(Task t)
                    {
                        return t.TaskType == TaskTypeEnum.Monitoring && Task.IsFirstMonitoring(t);
                    });

                    if (firstDefloodTask != null)
                    {
                        List<QbInvoice> invoices = QbInvoice.FindByProjectId(project.ID, null);
                        QbInvoice finalInvoice = invoices.Find(delegate(QbInvoice i) { return !i.IsPending && !i.IsVoid; });

                        if (finalInvoice != null) 
                            result = true;
                    }
                }
                else if (project.ProjectType == ProjectTypeEnum.RugCleaning)
                {
                    List<QbInvoice> invoices = QbInvoice.FindByProjectId(project.ID, null);
                    QbInvoice finalInvoice = invoices.Find(delegate(QbInvoice i) { return !i.IsPending && !i.IsVoid; });
                    if (finalInvoice != null)
                        result = true;
                }
            }

            return result;
        }

        #endregion 
    }

    public class ResourceWrapper
    {
        private Employee m_technician;
        private Work m_work;
        private Van m_van;
        private int m_resourceIndex;

        #region Technician

        public Employee Technician
        {
            get { return m_technician; }
            set { m_technician = value; }
        }

        #endregion

        #region Work

        public Work Work
        {
            get { return m_work; }
            set { m_work = value; }
        }

        #endregion

        #region ResourceIndex

        public int ResourceIndex
        {
            get { return m_resourceIndex; }
            set { m_resourceIndex = value; }
        }

        #endregion

        #region Constructor

        public ResourceWrapper(Employee technician, Work work, Van van, int resourceIndex)
        {
            m_technician = technician;
            m_work = work;
            m_van = van;
            m_resourceIndex = resourceIndex;
        }

        #endregion

        #region ValueEquals 

        public bool ValueEquals(ResourceWrapper obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (m_technician != null)
            {
                if (!m_technician.ValueEquals(obj.m_technician))
                return false;
            }
            else
            {
                if (obj.m_technician != null)
                    return false;
            }

            if (m_work != null)
            {
                if (!m_work.ValueEquals(obj.m_work))
                    return false;
            }
            else
            {
                if (obj.m_work != null)
                    return false;
            }

            if (m_van != null)
            {
                if (!m_van.ValueEquals(obj.m_van))
                    return false;
            }
            else
            {
                if (obj.m_van != null)
                    return false;
            }

            if (m_resourceIndex != obj.m_resourceIndex)
                return false;

            return true; 
        }

        #endregion 

        #region VanEquipment

        private string m_vanEquipment;
        public string VanEquipment
        {
            get { return m_vanEquipment; }
            set { m_vanEquipment = value; }
        }

        #endregion

        #region Caption

        public string Caption
        {
            get
            {
                string caption = m_resourceIndex + 1 + ": " + m_technician.DisplayName;

                if (m_work == null)
                    caption += " \n No Work";
                else
                {
                    if (m_work.WorkStatus == WorkStatusEnum.StartDayDone
                        || m_work.WorkStatus == WorkStatusEnum.Completed)
                    {
                        caption += " [" + m_work.ClosedDollarAmount.ToString("C") + "]";
                    }

                    caption += " \n " + m_work.WorkStatusUserFriendlyText;
                    if (m_work.WorkStatus != WorkStatusEnum.Pending)
                        caption += ", " + m_van.LicensePlateNumber + ": " + VanEquipment;
                }

                return caption;                
            }
        }

        #endregion

        #region ID

        public int ID
        {
            get { return m_technician.ID; }
        }

        #endregion

        #region IsWorkCompleted

        public bool IsWorkCompleted
        {
            get { return IsWorkExist && m_work.WorkStatus == WorkStatusEnum.Completed; }
        }

        #endregion

        #region IsWorkExist

        public bool IsWorkExist
        {
            get { return m_work != null; }
        }

        #endregion                

        #region IsResourceLocationMenuEnabled

        public bool IsResourceLocationMenuEnabled
        {
            get
            {
                return (IsWorkExist && m_work.WorkStatus == WorkStatusEnum.StartDayDone
                    && m_work.StartDate.Value.Date == DateTime.Now.Date);
            }
        }

        #endregion

        #region IsCreateRecreateWorkAllowed

        public bool IsCreateRecreateWorkAllowed
        {
            get
            {
                return !m_technician.IsUnknown;
            }
        }

        #endregion

        #region CreateRecreateWorkMenuText

        public string CreateRecreateWorkMenuText
        {
            get
            {
                if (IsWorkExist && m_work.WorkStatus != WorkStatusEnum.Pending)
                    return "Edit Create Work";
                return "Create Work";
            }
        }

        #endregion

        #region IsStartRestartDayAllowed

        public bool IsStartRestartDayAllowed
        {
            get
            {
                return (IsWorkExist 
                    && (m_work.WorkStatus == WorkStatusEnum.ReadyForStartDay
                        || m_work.WorkStatus == WorkStatusEnum.StartDayDone
                        || m_work.WorkStatus == WorkStatusEnum.Completed)
                    && m_work.StartDate.Value.Date <= DateTime.Now.Date);
            }
        }

        #endregion

        #region StartRestartDayMenuText

        public string StartRestartDayMenuText
        {
            get
            {
                if (IsWorkExist 
                    && (m_work.WorkStatus == WorkStatusEnum.StartDayDone
                        || m_work.WorkStatus == WorkStatusEnum.Completed))
                    return "Edit Start Day";
                return "Start Day";
            }
        }

        #endregion

        #region IsCompleteRecompleteWorkAllowed

        public bool IsCompleteRecompleteWorkAllowed
        {
            get
            {
                return (IsWorkExist 
                    && (m_work.WorkStatus == WorkStatusEnum.StartDayDone
                        || m_work.WorkStatus == WorkStatusEnum.Completed)
                    && m_work.StartDate.Value.Date <= DateTime.Now.Date);
            }
        }

        #endregion

        #region CompleteRecompleteWorkMenuText

        public string CompleteRecompleteWorkMenuText
        {
            get
            {
                if (IsWorkExist && m_work.WorkStatus == WorkStatusEnum.Completed)
                    return "Edit Complete Work";
                return "Complete Work";
            }
        }

        #endregion

        #region IsUndoWorkAllowed

        public bool IsUndoWorkAllowed
        {
            get
            {
                return IsWorkExist && m_work.WorkStatus != WorkStatusEnum.Pending;
            }
        }

        #endregion

        #region CurrentWorkUndoOperation

        public WorkUndoOperationEnum CurrentWorkUndoOperation
        {
            get
            {
                if (!IsUndoWorkAllowed)
                    return WorkUndoOperationEnum.Unavailable;
                if (m_work.WorkStatus == WorkStatusEnum.ReadyForStartDay)
                    return WorkUndoOperationEnum.CreateWork;
                if (m_work.WorkStatus == WorkStatusEnum.StartDayDone)
                    return WorkUndoOperationEnum.StartDay;
                if (m_work.WorkStatus == WorkStatusEnum.Completed)
                    return WorkUndoOperationEnum.CompleteWork;

                throw new DalworthException(string.Empty);
            }
        }

        #endregion

        #region CurrentWorkUndoMenuText

        public string CurrentWorkUndoMenuText
        {
            get
            {
                WorkUndoOperationEnum operation = CurrentWorkUndoOperation;

                if (operation == WorkUndoOperationEnum.Unavailable)
                    return "Undo";
                if (operation == WorkUndoOperationEnum.CreateWork)
                    return "Undo Create Work";
                if (operation == WorkUndoOperationEnum.StartDay)
                    return "Undo Start Day";
                if (operation == WorkUndoOperationEnum.CompleteWork)
                    return "Undo Complete Work";

                throw new DalworthException(string.Empty);
            }
        }

        #endregion
    }
}
