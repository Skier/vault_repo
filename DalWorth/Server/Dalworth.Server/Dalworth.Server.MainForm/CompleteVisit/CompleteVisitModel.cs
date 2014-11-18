using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;
using Dalworth.Server.MainForm.CreateVisit;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using Task=Dalworth.Server.Domain.Task;

namespace Dalworth.Server.MainForm.CompleteVisit
{
    public class CompleteVisitModel : IModel
    {
        private int m_newTaskIdCounter = -1;

        #region Customer

        private Customer m_customer;
        public Customer Customer
        {
            get { return m_customer; }
        }

        #endregion

        #region WorkDetail

        private WorkDetail m_workDetail;
        public WorkDetail WorkDetail
        {
            get { return m_workDetail; }
        }

        #endregion

        #region Work

        private Work m_work;
        public Work Work
        {
            get { return m_work; }
            set { m_work = value; }
        }

        #endregion

        #region Visit

        private Visit m_visit;
        public Visit Visit
        {
            get { return m_visit; }
            set { m_visit = value; }
        }

        #endregion        

        #region Tasks

        private BindingList<TaskProjectWrapperComplete> m_tasks;
        public BindingList<TaskProjectWrapperComplete> Tasks
        {
            get { return m_tasks; }
        }

        #endregion

        #region DefaultCompletionTime

        private DateTime m_defaultCompletionTime;
        public DateTime DefaultCompletionTime
        {
            get { return m_defaultCompletionTime; }
        }

        #endregion

        #region Equipment Data

        private Dictionary<int, int> m_dropOffEquipment;
        public Dictionary<int, int> DropOffEquipment
        {
            get { return m_dropOffEquipment; }
            set { m_dropOffEquipment = value; }
        }

        private Dictionary<int, int> m_pickupEquipment;
        public Dictionary<int, int> PickupEquipment
        {
            get { return m_pickupEquipment; }
            set { m_pickupEquipment = value; }
        }

        private Dictionary<int, int> m_vanEquipment;
        public Dictionary<int, int> VanEquipment
        {
            get { return m_vanEquipment; }
        }

        private Dictionary<int, int> m_customerEquipment;
        public Dictionary<int, int> CustomerEquipment
        {
            get { return m_customerEquipment; }
        }

        private Dictionary<int, int> m_vanEquipmentInitial;
        private Dictionary<int, int> m_customerEquipmentInitial;

        #endregion

        #region ServiceAddress

        private Address m_serviceAddress;
        public Address ServiceAddress
        {
            get { return m_serviceAddress; }
        }

        #endregion

        #region IsPreviousPaymentExists

        private bool m_isPreviousPaymentExists;
        public bool IsPreviousPaymentExists
        {
            get { return m_isPreviousPaymentExists; }
        }

        #endregion

        #region Init

        public void Init()
        {
            InitTasks();

            if (m_visit.CustomerId.HasValue)
                m_customer = Domain.Customer.FindByPrimaryKey(m_visit.CustomerId.Value);
            m_workDetail = WorkDetail.FindByWorkAndVisit(m_work, m_visit, null);
            if (m_visit.ServiceAddressId.HasValue)
                m_serviceAddress = Address.FindByPrimaryKey(m_visit.ServiceAddressId.Value);

            InitDefaultCompletionTime();
            InitEquipment();
        }

        #endregion              

        #region FindNotValidProject

        public TaskProjectWrapperComplete FindNotValidProject()
        {
            foreach (TaskProjectWrapperComplete task in m_tasks)
            {
                if (task.IsProject && task.IsIncludedInVisit)
                {
                    if (string.IsNullOrEmpty(task.Task.Project.QbSalesRepListId) || string.IsNullOrEmpty(task.Task.Project.QbCustomerTypeListId) )
                        return task;
                }
            }

            return null;
        }

        #endregion

        #region Task Validation

        public TaskProjectWrapperComplete FindInvalidClosedAmountTask()
        {
            foreach (TaskProjectWrapperComplete task in m_tasks)
            {
                if (IsClosedAmountRequired(task) && task.Task.ClosedAmount == decimal.Zero)
                    return task;
            }

            return null;            
        }

        public TaskProjectWrapperComplete FindInvalidReadingsTask()
        {
            foreach (TaskProjectWrapperComplete task in m_tasks)
            {
                if (!task.IsProject && task.Task.TaskType == TaskTypeEnum.Monitoring
                    && task.IsIncludedInVisit 
                    && task.TaskActionId == (int)TaskActionEnum.Complete
                    && !task.Task.MonitoringDetail.IsNoReadings)
                {                    
                    if (task.Task.MonitoringReadings.Count == 0)
                        return task;

                    foreach (MonitoringReading reading in task.Task.MonitoringReadings)
                    {
                        if (!reading.IsValid)
                            return task;
                    }                                    
                }

            }

            return null;
        }

        #endregion

        #region IsClosedAmountRequired

        public bool IsClosedAmountRequired(TaskProjectWrapperComplete task)
        {
            if (!IsClosedAmountAllowed(task))
                return false;

            if (task.Task.TaskType == TaskTypeEnum.Deflood && task.IsDefloodFirstTimeService
                && !task.Task.IsAmountNotKnown)
            {
                return true;
            }                

//            if (task.Task.TaskType == TaskTypeEnum.RugDelivery
//                && task.Task.Project.ProjectType == ProjectTypeEnum.RugCleaning)
//            {
//                return true;
//            }

            if (task.Task.TaskType == TaskTypeEnum.RugPickup
                && task.Task.Project.ProjectType == ProjectTypeEnum.Deflood)
            {
                return true;
            }

            return false;
        }

        #endregion

        #region IsClosedAmountAllowed

        public bool IsClosedAmountAllowed(TaskProjectWrapperComplete task)
        {
            if (task.IsProject)
                return false;

            if (!task.IsIncludedInVisit
                || task.TaskAction == TaskActionEnum.Fail || task.TaskAction == TaskActionEnum.Book)
            {
                return false;
            }                

            if (task.Task.TaskType == TaskTypeEnum.Deflood && !task.IsDefloodFirstTimeService)
                return false;

            if (task.Task.TaskType == TaskTypeEnum.Deflood && task.Task.IsAmountNotKnown)
                return false;

//            if (task.Task.TaskType == TaskTypeEnum.RugPickup 
//                && task.Task.Project.ProjectType == ProjectTypeEnum.RugCleaning)
//            {
//                return false;
//            }

            if (task.Task.TaskType == TaskTypeEnum.RugDelivery
                && task.Task.Project.ProjectType == ProjectTypeEnum.Deflood)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region InitDefaultCompletionTime

        private void InitDefaultCompletionTime()
        {
            if (DateTime.Now.Date == Work.StartDate.Value.Date
                && Visit.VisitStatus != VisitStatusEnum.Completed
                && DateTime.Now - WorkDetail.TimeEnd <= new TimeSpan(2, 0, 0))
            {
                m_defaultCompletionTime = DateTime.Now;                
            } else
                m_defaultCompletionTime = WorkDetail.TimeEnd;   
         
            if (Visit.VisitStatus != VisitStatusEnum.Completed
                && Domain.WorkDetail.IsExistCollision(WorkDetail, WorkDetail.TimeBegin, m_defaultCompletionTime) != CollisionErrorEnum.NoCollision)
            {
                List<WorkDetail> processedDetails = Domain.WorkDetail.FindByWorkProcessed(Work);
                processedDetails.Sort(delegate(WorkDetail x, WorkDetail y) { return x.TimeBegin.CompareTo(y.TimeBegin); });

                foreach (WorkDetail detail in processedDetails)
                {
                    if (detail.ID == WorkDetail.ID)
                        continue;

                    if (WorkDetail.TimeBegin < detail.TimeBegin
                        && m_defaultCompletionTime > detail.TimeBegin)
                    {
                        m_defaultCompletionTime = detail.TimeBegin;
                        break;
                    }
                }

            }
        }

        #endregion

        #region DeleteProjectIfEmpty

        private void DeleteProjectIfEmpty(string projectId)
        {
            foreach (TaskProjectWrapperComplete task in m_tasks)
            {
                if (task.ParentId == projectId)
                    return;
            }

            foreach (TaskProjectWrapperComplete task in m_tasks)
            {
                if (task.ID == projectId)
                {
                    m_tasks.Remove(task);
                    return;
                }

            }

        }

        #endregion

        #region DeleteTask

        public void DeleteTask(TaskProjectWrapperComplete task)
        {
            List<TaskProjectWrapperComplete> itemsToBeRemoved = new List<TaskProjectWrapperComplete>();
            itemsToBeRemoved.Add(task);
            itemsToBeRemoved.AddRange(FindChilds(task));

            foreach (TaskProjectWrapperComplete item in itemsToBeRemoved)
                Tasks.RemoveAt(Tasks.IndexOf(item));

            if (!task.IsProject)
                DeleteProjectIfEmpty(TaskProjectWrapper.ProjectIdPreffix + task.Task.ProjectId);
        }

        private List<TaskProjectWrapperComplete> FindChilds(TaskProjectWrapperComplete parent)
        {
            List<TaskProjectWrapperComplete> result = new List<TaskProjectWrapperComplete>();
            foreach (TaskProjectWrapperComplete task in m_tasks)
            {
                if (task.ParentId == parent.ID)
                {
                    result.Add(task);
                    result.AddRange(FindChilds(task));
                }                                    
            }

            return result;
        }

        #endregion

        #region IsNextVisitExists

        public bool IsNextVisitExists(VisitCompletePackage completePackage)
        {
            List<TaskProjectWrapperComplete> tasks = completePackage.FindTasksToBeAddedToNewVisit();
            
            foreach (TaskProjectWrapperComplete task in tasks)
            {
                try
                {
                    Visit nextVisit = Visit.FindNextVisit(task.Task, m_visit);

                    if (nextVisit.VisitStatus == VisitStatusEnum.Pending)
                        return true;
                }
                catch (DataNotFoundException)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        //Equipment

        #region GetExistingEquipmentTransaction

        private EquipmentTransaction GetExistingEquipmentTransaction()
        {
            if (Visit.VisitStatus == VisitStatusEnum.Completed)
            {
                WorkTransaction workTransaction =
                    WorkTransaction.FindBy(Visit.ID, WorkTransactionTypeEnum.VisitEquipmentTransfer);
                return EquipmentTransaction.FindByWorkTransaction(workTransaction);
            }

            return null;
        }

        #endregion


        #region InitEquipment

        public void InitEquipment()
        {
            if (m_visit.ServiceAddressId == null)
                return;

            List<EquipmentType> equipmentTypes = EquipmentType.Find();
            m_dropOffEquipment = new Dictionary<int, int>();
            m_pickupEquipment = new Dictionary<int, int>();
            foreach (EquipmentType equipmentType in equipmentTypes)
            {
                m_dropOffEquipment.Add(equipmentType.ID, 0);
                m_pickupEquipment.Add(equipmentType.ID, 0);
            }

            EquipmentTransaction existingEquipmentTransaction = GetExistingEquipmentTransaction();
            int? existingTransactionId = null;
            if (existingEquipmentTransaction != null)
            {
                existingTransactionId = existingEquipmentTransaction.ID;                

                List<EquipmentTransactionDetail> existingDetails
                    = EquipmentTransactionDetail.FindByTransaction(existingEquipmentTransaction);

                foreach (EquipmentTransactionDetail detail in existingDetails)
                {
                    if (!detail.AddressId.HasValue)
                        continue;

                    if (detail.QuantityChange > 0)
                        m_dropOffEquipment[detail.EquipmentTypeId] = detail.QuantityChange;
                    else
                        m_pickupEquipment[detail.EquipmentTypeId] = -detail.QuantityChange;
                }
            }
                
            m_vanEquipment = new Dictionary<int, int>();
            m_customerEquipment = new Dictionary<int, int>();
            List<EquipmentTransactionDetail> vanEquipment = EquipmentTransactionDetail.FindOnDate(
                GetInitialEquipmentDate(), m_work.VanId.Value, null, existingTransactionId);
            List<EquipmentTransactionDetail> customerEquipment = EquipmentTransactionDetail.FindOnDate(
                GetInitialEquipmentDate(), null, m_visit.ServiceAddressId.Value, existingTransactionId);
            foreach (EquipmentTransactionDetail vanDetail in vanEquipment)
                m_vanEquipment.Add(vanDetail.EquipmentTypeId, vanDetail.Quantity);
            foreach (EquipmentTransactionDetail cutomerDetail in customerEquipment)
                m_customerEquipment.Add(cutomerDetail.EquipmentTypeId, cutomerDetail.Quantity);

            m_vanEquipmentInitial = new Dictionary<int, int>(m_vanEquipment);
            m_customerEquipmentInitial = new Dictionary<int, int>(m_customerEquipment);
        }

        #endregion

        #region GetInitialEquipmentDate

        //Date for which equipment is prepopulated on UI
        private DateTime GetInitialEquipmentDate()
        {            
            if (Visit.VisitStatus == VisitStatusEnum.Completed)
                return m_workDetail.TimeComplete.Value;
            return m_defaultCompletionTime;            
        }

        #endregion

        #region GetCompletePackage

        public VisitCompletePackage GetCompletePackage()
        {
            List<EquipmentType> equipmentTypes = EquipmentType.Find();            
            Dictionary<int, int> vanEquipmentDropOff = new Dictionary<int, int>();
            foreach (EquipmentType equipmentType in equipmentTypes)
                vanEquipmentDropOff.Add(equipmentType.ID, 0);

            if (m_visit.ServiceAddressId.HasValue)
            {
                foreach (KeyValuePair<int, int> dropOffPair in m_dropOffEquipment)
                {
                    if (dropOffPair.Value > 0)
                        vanEquipmentDropOff[dropOffPair.Key] = dropOffPair.Value;
                }

                foreach (KeyValuePair<int, int> pickUpPair in m_pickupEquipment)
                {
                    if (pickUpPair.Value > 0)
                        vanEquipmentDropOff[pickUpPair.Key] = -pickUpPair.Value;
                }                
            }

            return new VisitCompletePackage(
                Work.TechnicianEmployeeId,
                Visit,
                new List<TaskProjectWrapperComplete>(m_tasks),
                Work,
                new List<WorkTransactionPayment>(),
                new List<Project>(),
                vanEquipmentDropOff);
        }

        #endregion

        #region RecalculateEquipmentQuantities

        public void RecalculateEquipmentQuantities(bool isEditingDropOff)
        {
            if (isEditingDropOff)
            {
                foreach (KeyValuePair<int, int> dropOffPair in m_dropOffEquipment)
                {
                    if (dropOffPair.Value > 0 && m_pickupEquipment[dropOffPair.Key] > 0)
                        m_pickupEquipment[dropOffPair.Key] = 0;
                }                
            }
            else
            {
                foreach (KeyValuePair<int, int> pickupPair in m_pickupEquipment)
                {
                    if (pickupPair.Value > 0 && m_dropOffEquipment[pickupPair.Key] > 0)
                        m_dropOffEquipment[pickupPair.Key] = 0;
                }                                
            }

            foreach (KeyValuePair<int, int> dropOffPair in m_dropOffEquipment)
            {
                m_vanEquipment[dropOffPair.Key] = m_vanEquipmentInitial[dropOffPair.Key] - dropOffPair.Value;
                m_customerEquipment[dropOffPair.Key] = m_customerEquipmentInitial[dropOffPair.Key] + dropOffPair.Value;
            }

            foreach (KeyValuePair<int, int> pickupPair in m_pickupEquipment)
            {
                m_vanEquipment[pickupPair.Key] = m_vanEquipment[pickupPair.Key] + pickupPair.Value;
                m_customerEquipment[pickupPair.Key] = m_customerEquipment[pickupPair.Key] - pickupPair.Value;
            }
        }

        #endregion

        
        //Tasks

        #region InitTasks

        private void InitTasks()
        {
            List<Task> tasks = Task.FindByVisit(Visit);
            List<Task> notIncludedTasks = Task.FindNotIncludedParents(Visit);
            tasks.AddRange(notIncludedTasks);            

            m_tasks = new BindingList<TaskProjectWrapperComplete>();
            Dictionary<int, Task> projects = new Dictionary<int, Task>();

            List<int> addedTaskIds = new List<int>();
            List<int> addedProjectIds = new List<int>();

            WorkTransaction completeTransaction = null;

            if (Visit.VisitStatus == VisitStatusEnum.Completed)
            {
                completeTransaction =                
                    WorkTransaction.FindBy(Visit.ID, WorkTransactionTypeEnum.VisitCompleted);

                List<WorkTransactionTask> bookedTransactionTasks = WorkTransactionTask.FindBy(
                    completeTransaction, WorkTransactionTaskActionEnum.Booked);
                foreach (WorkTransactionTask transactionTask in bookedTransactionTasks)
                {
                    Task bookedTask = Task.FindByPrimaryKey(transactionTask.TaskId);
                    bookedTask.Project = Project.FindByPrimaryKey(bookedTask.ProjectId);
                    tasks.Add(bookedTask);
                }

                m_isPreviousPaymentExists = ProjectPayment.FindBy(completeTransaction).Count > 0;

                List<Task> addedTasks = Task.FindBy(completeTransaction, false, true);
                List<Project> addedProjects = Project.FindBy(completeTransaction, false, true);

                foreach (Task task in addedTasks)
                    addedTaskIds.Add(task.ID);

                foreach (Project project in addedProjects)
                    addedProjectIds.Add(project.ID);
            }


            foreach (Task task in tasks)
            {
                TaskProjectWrapperComplete wrapper = new TaskProjectWrapperComplete(task, false,
                    !notIncludedTasks.Contains(task));
                m_tasks.Add(wrapper);                

                if (Visit.VisitStatus == VisitStatusEnum.Completed && wrapper.IsIncludedInVisit)
                {
                    WorkTransactionTask workTransactionTask
                        = WorkTransactionTask.FindBy(completeTransaction, task);

                    if (workTransactionTask.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.Complete)
                        wrapper.TaskAction = TaskActionEnum.Complete;
                    else if (workTransactionTask.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.InProcess)
                        wrapper.TaskAction = TaskActionEnum.InProcess;
                    else if (workTransactionTask.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.Booked)
                        wrapper.TaskAction = TaskActionEnum.Book;
                    else if (workTransactionTask.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.Cancel)
                    {
                        wrapper.TaskAction = TaskActionEnum.Fail;
                        wrapper.Task.TaskFailType = TaskFailTypeEnum.Cancel;
                    }
                    else if (workTransactionTask.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.FailMustReturn)
                    {
                        wrapper.TaskAction = TaskActionEnum.Fail;
                        wrapper.Task.TaskFailType = TaskFailTypeEnum.MustReturn;
                    }

                    //Don't know what is this for
//                    try
//                    {
//                        task.TaskStatus = Task.FindDumpedTask(task, completeTransaction.ID).TaskStatus;
//                    }
//                    catch (DataNotFoundException)
//                    {
//                        task.TaskStatus = TaskStatusEnum.NotCompleted;
//                    }

                    if (addedTaskIds.Contains(task.ID))
                        wrapper.IsNewlyAdded = true;
                }                

                if (task.TaskType == TaskTypeEnum.RugDelivery
                    || task.TaskType == TaskTypeEnum.RugPickup)
                {
                    wrapper.Items = Item.FindByTask(task);
                }
                else if (task.TaskType == TaskTypeEnum.Deflood)
                {
                    task.DefloodDetail = DefloodDetail.FindByPrimaryKey(task.ID);
                }
                else if (task.TaskType == TaskTypeEnum.Monitoring)
                {
                    task.MonitoringDetail = MonitoringDetail.FindByPrimaryKey(task.ID);
                    task.MonitoringReadings = MonitoringReading.FindBy(task, null);

                    List<MonitoringReading> previousReadings
                        = MonitoringReading.FindPreviousMonitoringReadings(task);

                    for (int i = 0; i < previousReadings.Count; i++)
                    {
                        if (task.MonitoringReadings.Count - 1 < i)
                        {
                            MonitoringReading customReading = new MonitoringReading();
                            customReading.MonitoringReadingType = previousReadings[i].MonitoringReadingType;
                            customReading.EquipmentSerialNumber = previousReadings[i].EquipmentSerialNumber;
                            customReading.IsRemoveAllowed = false;
                            task.MonitoringReadings.Add(customReading);                            
                        }

                        task.MonitoringReadings[i].PreviousTemperature 
                            = previousReadings[i].Temperature;
                        task.MonitoringReadings[i].PreviousRelativeHumidity 
                            = previousReadings[i].RelativeHumidity;
                        task.MonitoringReadings[i].PreviousGpp 
                            = previousReadings[i].Gpp;
                    }

                    if (task.MonitoringReadings.Count == 0)
                        task.MonitoringReadings = MonitoringReading.DefaultReadings;
                }

                if (!projects.ContainsKey(task.ProjectId))
                    projects.Add(task.ProjectId, task);
            }

            foreach (Task task in projects.Values)
            {
                TaskProjectWrapperComplete wrapper
                    = new TaskProjectWrapperComplete(task, true, !notIncludedTasks.Contains(task));

                if (addedProjectIds.Contains(task.ProjectId))
                    wrapper.IsNewlyAdded = true;                    

                m_tasks.Add(wrapper);
            }


            if (completeTransaction != null)
            {
                foreach (TaskProjectWrapperComplete task in m_tasks)
                {
                    if (!task.IsProject && task.IsIncludedInVisit 
                        && task.Task.TaskType == TaskTypeEnum.Deflood)
                    {
                        if (task.IsNewlyAdded)
                            task.IsDefloodFirstTimeService = true;
                        else
                        {
                            Task dumpedTask = Task.FindDumpedTask(task.Task, completeTransaction.ID);
                            task.IsDefloodFirstTimeService = dumpedTask.TaskStatus == TaskStatusEnum.NotCompleted;                            
                        }
                    }
                }                
            }

            foreach (TaskProjectWrapperComplete task in m_tasks)
            {
                if (!task.IsProject && task.IsIncludedInVisit && task.Task.TaskType == TaskTypeEnum.Monitoring)
                {
                    TaskProjectWrapperComplete monitoring = task;

                    foreach (TaskProjectWrapperComplete defloodCandidate in m_tasks)
                    {
                        if (defloodCandidate.Task.TaskType == TaskTypeEnum.Deflood
                            && monitoring.Task.ParentTaskId == defloodCandidate.Task.ID)
                        {
                            monitoring.IsMonitoringFirstTimeService = defloodCandidate.IsDefloodFirstTimeService;
                            break;
                        }
                    }
                }

                if (!task.IsProject && task.IsIncludedInVisit 
                    && task.Task.TaskType == TaskTypeEnum.Deflood && task.IsDefloodFirstTimeService
                    && Visit.VisitStatus == VisitStatusEnum.Completed
                    && task.Task.ClosedAmount == decimal.Zero)
                {
                    task.Task.IsAmountNotKnown = true;
                }
            }

            foreach (TaskProjectWrapperComplete taskOrProject in m_tasks)
            {
                if (taskOrProject.IsProject)
                {
                    taskOrProject.ProjectClosedAmountWithoutListedTasks 
                        = taskOrProject.Task.Project.ClosedAmount;

                    foreach (TaskProjectWrapperComplete task in FindTasksByProject(taskOrProject.Task.Project.ID))
                        taskOrProject.ProjectClosedAmountWithoutListedTasks -= task.Task.ClosedAmount;
                }
            }


            //set default closed amount for standalone rug delivery
            foreach (TaskProjectWrapperComplete task in m_tasks)
            {
                if (!task.IsProject
                    && task.IsIncludedInVisit
                    && task.Task.TaskStatus == TaskStatusEnum.NotCompleted
                    && task.Task.ClosedAmount == decimal.Zero)
                {
                    if (task.Task.TaskType == TaskTypeEnum.RugPickup
                        && task.Task.Project.ProjectType == ProjectTypeEnum.RugCleaning)
                    {
                        task.Task.EstimatedClosedAmount = task.Task.GetEstimatedClosedAmount(
                            task.Items, task.Task.IsClosedAmountAutoCalculated, task.Task.DiscountPercentage);
                    }
                    else
                    {
                        task.Task.ClosedAmount = task.Task.GetEstimatedClosedAmount(
                            task.Items, task.Task.IsClosedAmountAutoCalculated, task.Task.DiscountPercentage);
                    }

                    RecalculateProjectClosedAmount(task.Task.Project.ID);
                }                
            }
        }

        #endregion

        #region FindTasksByProject

        private List<TaskProjectWrapperComplete> FindTasksByProject(int projectId)
        {
            List<TaskProjectWrapperComplete> result = new List<TaskProjectWrapperComplete>();

            foreach (TaskProjectWrapperComplete task in m_tasks)
            {
                if (!task.IsProject && task.Task.ProjectId == projectId)
                    result.Add(task);
            }
            return result;
        }

        #endregion

        #region RecalculateProjectClosedAmount

        public TaskProjectWrapperComplete RecalculateProjectClosedAmount(int projectId)
        {
            foreach (TaskProjectWrapperComplete taskOrProject in m_tasks)
            {
                if (taskOrProject.IsProject && taskOrProject.Task.Project.ID == projectId)
                {
                    taskOrProject.Task.Project.ClosedAmount = taskOrProject.ProjectClosedAmountWithoutListedTasks;

                    foreach (TaskProjectWrapperComplete task in FindTasksByProject(projectId))
                    {
                        taskOrProject.Task.Project.ClosedAmount += task.Task.ClosedAmount;
                    }

                    return taskOrProject;
                }
            }

            return null;
        }

        #endregion

        #region AddNewTask

        public void AddNewTask(TaskTypeEnum taskType, TaskProjectWrapperComplete parent)
        {
            m_newTaskIdCounter--;

            Task task = new Task(
                m_newTaskIdCounter,
                null,
                string.Empty,
                m_newTaskIdCounter,
                (int)taskType,
                (int)TaskStatusEnum.NotCompleted,
                null, true, string.Empty, null, null, null, null,
                string.Empty, string.Empty, string.Empty, string.Empty, false, 
                decimal.Zero, taskType == TaskTypeEnum.RugPickup ? true : false,
                decimal.Zero, taskType == TaskTypeEnum.RugPickup ? true : false, 
                false, DateTime.Now, null, null, null, 0, null, false, 0, null);

            if (taskType == TaskTypeEnum.Deflood)
                task.DefloodDetail = new DefloodDetail();
            else if (taskType == TaskTypeEnum.Monitoring)
            {
                task.MonitoringDetail = new MonitoringDetail();
                task.MonitoringReadings = new List<MonitoringReading>();
            }

            if (parent == null)
            {
                task.Project = new Project(task.ProjectId);
                task.Project.Description = string.Empty;
                task.Project.CreateDate = DateTime.Now;
                if (taskType == TaskTypeEnum.Deflood)
                    task.Project.ProjectType = ProjectTypeEnum.Deflood;
                else if (taskType == TaskTypeEnum.RugPickup)
                    task.Project.ProjectType = ProjectTypeEnum.RugCleaning;
                else if (taskType == TaskTypeEnum.Miscellaneous)
                    task.Project.ProjectType = ProjectTypeEnum.Miscellaneous;
                m_tasks.Add(new TaskProjectWrapperComplete(task, true, true));
            }
            else
            {
                task.ProjectId = parent.Task.Project.ID;
                task.Project = parent.Task.Project;
                if (!parent.IsProject)
                    task.ParentTaskId = parent.Task.ID;
            }

            TaskProjectWrapperComplete newlyAddedTask = new TaskProjectWrapperComplete(task, false, true);
            m_tasks.Add(newlyAddedTask);

            if (parent == null && taskType == TaskTypeEnum.Deflood)
                AddNewTask(TaskTypeEnum.Monitoring, newlyAddedTask);
        }

        #endregion

        #region IsAddDefloodAllowed

        public bool IsAddDefloodAllowed()
        {
            foreach (TaskProjectWrapperComplete task in m_tasks)
                if (task.Task.TaskType == TaskTypeEnum.Deflood)
                    return false;

            return true;
        }

        #endregion        

        #region IsAddRugPickupPartOfDefloodAllowed

        public bool IsAddRugPickupPartOfDefloodAllowed(TaskProjectWrapperComplete project)
        {
            if (!project.IsProject)
                return false;
            if (project.Task.Project.ProjectType != ProjectTypeEnum.Deflood)
                return false;

            foreach (TaskProjectWrapperComplete task in m_tasks)
            {
                if (task.Task.ProjectId == project.Task.Project.ID
                    && task.Task.TaskType == TaskTypeEnum.RugPickup
                    && task.IsIncludedInVisit)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region IsProjectEmpty

        public bool IsProjectEmpty(string projectId)
        {
            foreach (TaskProjectWrapperComplete task in m_tasks)
            {
                if (task.ParentId == projectId)
                    return false;
            }

            return true;
        }

        #endregion

        #region FindMonitoringTask

        public TaskProjectWrapperComplete FindMonitoringTask(TaskProjectWrapperComplete deflood)
        {
            foreach (TaskProjectWrapperComplete task in m_tasks)
            {
                if (!task.IsProject && task.IsIncludedInVisit
                    && task.Task.TaskType == TaskTypeEnum.Monitoring
                    && task.Task.ParentTaskId == deflood.Task.ID)
                {
                    return task;
                }
            }

            return null;            
        }

        #endregion

        #region ModifyMonitoringByDeflood

        public void ModifyMonitoringByDeflood(TaskProjectWrapperComplete deflood)
        {
            TaskProjectWrapperComplete monitoring = FindMonitoringTask(deflood);

            if (deflood.TaskActionId == (int)TaskActionEnum.Complete)
            {
                monitoring.TaskActionId = (int)TaskActionEnum.Complete;
                monitoring.Task.TaskFailType = null;
            }
            else if (deflood.TaskActionId == (int)TaskActionEnum.InProcess)
            {
                if (deflood.IsDefloodFirstTimeService)
                {
                    monitoring.TaskActionId = (int)TaskActionEnum.Complete;
                    monitoring.Task.TaskFailType = null;
                } else
                {
                    if (monitoring.TaskActionId == (int)TaskActionEnum.Fail)
                        monitoring.Task.TaskFailType = TaskFailTypeEnum.MustReturn;
                    else if (monitoring.TaskActionId == (int)TaskActionEnum.Book)
                        monitoring.TaskActionId = (int)TaskActionEnum.Complete;                    
                }

            } else if (deflood.TaskActionId == (int)TaskActionEnum.Book)
            {
                monitoring.TaskActionId = (int)TaskActionEnum.Book;
                monitoring.Task.TaskFailType = null;                
            }
            else if (deflood.TaskActionId == (int)TaskActionEnum.Fail)
            {
                monitoring.TaskActionId = (int)TaskActionEnum.Fail;
                monitoring.Task.TaskFailType = deflood.Task.TaskFailType;
                monitoring.Task.ClosedAmount = decimal.Zero;
            }

            m_tasks.ResetItem(m_tasks.IndexOf(monitoring));
        }

        #endregion

        #region FindDefloodTask

        public TaskProjectWrapperComplete FindDefloodTask(TaskProjectWrapperComplete monitoring)
        {
            foreach (TaskProjectWrapperComplete task in m_tasks)
            {
                if (!task.IsProject && task.IsIncludedInVisit
                    && task.Task.TaskType == TaskTypeEnum.Deflood
                    && monitoring.Task.ParentTaskId == task.Task.ID)
                {
                    return task;
                }
            }

            return null;
        }

        #endregion

        #region ModifyDefloodByMonitoring

        public void ModifyDefloodByMonitoring(TaskProjectWrapperComplete monitoring)
        {
            TaskProjectWrapperComplete defolood = FindDefloodTask(monitoring);

            if (monitoring.TaskActionId == (int)TaskActionEnum.Complete)
            {
                if (defolood.TaskActionId == (int)TaskActionEnum.Fail
                    || defolood.TaskActionId == (int)TaskActionEnum.Book)
                {
                    defolood.TaskActionId = (int) TaskActionEnum.InProcess;
                    defolood.Task.TaskFailType = null;
                }
            }
            else if (monitoring.TaskActionId == (int)TaskActionEnum.Fail)
            {
                if (monitoring.IsMonitoringFirstTimeService)
                {
                    defolood.TaskActionId = (int)TaskActionEnum.Fail;
                    defolood.Task.TaskFailType = monitoring.Task.TaskFailType;                                                                
                } else
                {
                    defolood.TaskActionId = (int)TaskActionEnum.InProcess;
                    defolood.Task.TaskFailType = null;
                }
            }
            else if (monitoring.TaskActionId == (int)TaskActionEnum.Book)
            {
                if (defolood.TaskActionId != (int)TaskActionEnum.Book)
                {
                    defolood.TaskActionId = (int)TaskActionEnum.Book;
                    defolood.Task.TaskFailType = null;
                }                
            }

            m_tasks.ResetItem(m_tasks.IndexOf(defolood));
        }

        #endregion

        #region GetVisitEditAllowance

        public string GetVisitEditAllowance()
        {
            if (Visit.VisitStatus != VisitStatusEnum.Completed)
                return string.Empty;

            Dictionary<int, TaskProjectWrapperComplete> wrappers = new Dictionary<int, TaskProjectWrapperComplete>();
            foreach (TaskProjectWrapperComplete wrapper in Tasks)
            {
                if (!wrapper.IsProject && wrapper.Task.ID > 0)
                    wrappers.Add(wrapper.Task.ID, wrapper);
            }

            List<Task> existingTasks = Task.FindByVisit(Visit);
            existingTasks.AddRange(Task.FindBookedTasksOnCompletion(Visit));

            List<Task> modifiedTasks = new List<Task>();

            WorkTransaction completeTransaction =
                WorkTransaction.FindBy(Visit.ID, WorkTransactionTypeEnum.VisitCompleted);

            foreach (Task task in existingTasks)
            {
                if (!wrappers.ContainsKey(task.ID))
                    modifiedTasks.Add(task);
                else
                {
                    TaskProjectWrapperComplete wrapper = wrappers[task.ID];

                    WorkTransactionTask taskTransaction =
                        WorkTransactionTask.FindByPrimaryKey(completeTransaction.ID, task.ID);

                    if (taskTransaction.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.Complete
                        && wrapper.TaskAction != TaskActionEnum.Complete)
                    {
                        modifiedTasks.Add(task);
                    }
                    else if (taskTransaction.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.InProcess
                        && wrapper.TaskAction != TaskActionEnum.InProcess)
                    {
                        modifiedTasks.Add(task);
                    } 
                    else if (taskTransaction.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.Booked
                        && wrapper.TaskAction != TaskActionEnum.Book)
                    {
                        modifiedTasks.Add(task);
                    } 
                    else if (taskTransaction.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.FailMustReturn)
                    {
                        modifiedTasks.Add(task);
                    } 
                    else if (taskTransaction.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.Cancel
                        && (wrapper.TaskAction != TaskActionEnum.Fail || wrapper.Task.TaskFailType != TaskFailTypeEnum.Cancel))
                    {
                        modifiedTasks.Add(task);
                    } 
                                       
                }
            }

            return Visit.GetVisitEditUndoAllowance(Visit, modifiedTasks, false, true);
        }

        #endregion

        #region IsShowCallbackSurvey

        public bool IsShowCallbackSurvey()
        {
            foreach (TaskProjectWrapperComplete task in m_tasks)
            {
                if ((task.Task.TaskType == TaskTypeEnum.RugDelivery && task.TaskAction == TaskActionEnum.Complete)
                    || (task.Task.TaskType == TaskTypeEnum.RugPickup 
                        && task.TaskAction == TaskActionEnum.Fail 
                        && task.Task.TaskFailType == TaskFailTypeEnum.Cancel))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
        
        #region IsMustArrangeNextVisit 
       
        public bool IsMustArrangeNextVisit()
        {
            foreach (TaskProjectWrapperComplete task in this.Tasks)
            {
                if (task.Task.TaskType == TaskTypeEnum.Deflood && task.IsIncludedInVisit
                    && (task.TaskActionId == (int)TaskActionEnum.InProcess || 
                    (task.TaskActionId == (int)TaskActionEnum.Fail && task.Task.TaskFailType == TaskFailTypeEnum.MustReturn) ||
                     task.TaskActionId == -1)
                    )
                    return true;
            }

            return false;
        }

        #endregion

        #region IsAskIfVisitShouldBeClosed

        public bool IsAskIfVisitShouldBeClosed()
        {
            foreach (int quantity in m_customerEquipment.Values)
            {
                if (quantity != 0)
                    return false;
            }

            foreach (TaskProjectWrapperComplete task in Tasks)
            {
                if (task.Task.TaskType == TaskTypeEnum.Deflood && task.IsIncludedInVisit
                    && task.TaskActionId == (int)TaskActionEnum.InProcess)
                    return true;
            }

            return false;
        }

        #endregion

        #region IsWarnFromClosingVisit

        public bool IsWarnFromClosingVisit()
        {
            bool isCustomerHasEquipment = false;
            foreach (int quantity in m_customerEquipment.Values)
            {
                if (quantity > 0)
                {
                    isCustomerHasEquipment = true;
                    break;
                }                    
            }

            foreach (TaskProjectWrapperComplete task in Tasks)
            {
                if (task.Task.TaskType == TaskTypeEnum.Deflood && task.IsIncludedInVisit
                    && task.TaskActionId == (int)TaskActionEnum.Complete && isCustomerHasEquipment)
                    return true;
            }

            return false;
        }

        #endregion

    }
}
