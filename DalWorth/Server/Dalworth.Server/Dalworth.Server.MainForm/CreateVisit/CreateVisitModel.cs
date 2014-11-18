using System;
using System.Collections.Generic;
using System.ComponentModel;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;
using Dalworth.Server.Domain.ServerSyncService;
using Dalworth.Server.Domain.Sync;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using DefloodDetail=Dalworth.Server.Domain.DefloodDetail;
using Item=Dalworth.Server.Domain.Item;
using MonitoringDetail=Dalworth.Server.Domain.MonitoringDetail;
using MonitoringReading=Dalworth.Server.Domain.MonitoringReading;
using Project=Dalworth.Server.Domain.Project;
using Task=Dalworth.Server.Domain.Task;
using Visit=Dalworth.Server.Domain.Visit;

namespace Dalworth.Server.MainForm.CreateVisit
{
    public class CreateVisitException : DalworthException
    {
        public CreateVisitException(string message) : base(message) {}
    }

    public enum CreateVisitResultEnum
    {
        PrintVisit,
        DoNotPrintVisit
    }

    public enum IsPromptForServiceDateEnum
    {
        NoPrompt,
        MonitoringPrompt,
        DefloodPrompt
    }

    public class CreateVisitModel : IModel
    {
        private int m_newTaskIdCounter = -1;
        private VisitSummaryPackage m_existingVisitSummaryPackage;

        #region IsGeneratedVisitAdjustment

        private bool m_isGeneratedVisitAdjustment;
        public bool IsGeneratedVisitAdjustment
        {
            get { return m_isGeneratedVisitAdjustment; }
            set { m_isGeneratedVisitAdjustment = value; }
        }

        #endregion

        #region IsReschedule

        private bool m_isReschedule;
        public bool IsReschedule
        {
            get { return m_isReschedule; }
            set { m_isReschedule = value; }
        }

        #endregion

        #region HistoryOrders

        private List<h_order> m_historyOrders;
        public List<h_order> HistoryOrders
        {
            get { return m_historyOrders; }
        }

        #endregion

        #region ExistingVisit

        private Visit m_existingVisit;
        public Visit ExistingVisit
        {
            get { return m_existingVisit; }
            set
            {
                m_existingVisit = Visit.FindByPrimaryKey(value.ID);
            }
        }

        #endregion

        #region IsReadOnly

        private bool? m_isReadOnly;
        public bool IsReadOnly
        {
            get
            {
                if (m_isReadOnly == null)
                {
                    if (ExistingVisit != null && (ExistingVisit.VisitStatus == VisitStatusEnum.Completed
                                                  || ExistingVisit.VisitStatus == VisitStatusEnum.Declined))
                    {
                        m_isReadOnly = true;
                    } else
                    {
                        m_isReadOnly = false;
                    }                                        
                }
                return m_isReadOnly.Value;
            }
        }

        #endregion

        #region CurrentAddress

        private Address m_currentAddress;
        public Address CurrentAddress
        {
            get { return m_currentAddress; }
            set { m_currentAddress = value; }
        }

        #endregion

        #region CurrentCustomer

        private Customer m_currentCustomer;
        public Customer CurrentCustomer
        {
            get { return m_currentCustomer; }
            set { m_currentCustomer = value; }
        }
        
        #endregion 

        #region BaseVisit

        private Visit m_baseVisit;
        public Visit BaseVisit
        {
            get { return m_baseVisit; }
            set { m_baseVisit = value; }
        }

        #endregion

        #region BaseLead

        private LeadWrapper m_baseLead;
        public LeadWrapper BaseLead
        {
            get { return m_baseLead; }
            set { m_baseLead = value; }
        }

        #endregion

        #region Tasks

        private BindingList<TaskProjectWrapper> m_tasks;
        public BindingList<TaskProjectWrapper> Tasks
        {
            get { return m_tasks; }
        }

        #endregion

        #region Init

        public void Init()
        {   
            m_tasks = new BindingList<TaskProjectWrapper>();
            m_historyOrders = new List<h_order>();
            if (ExistingVisit != null)
                m_existingVisitSummaryPackage = new VisitSummaryPackage(ExistingVisit);
        }

        #endregion

        #region GetTasksShouldInTree

        //bool value indicates whether this task is exist in visit
        private Dictionary<int, bool> GetTasksShouldInTree()
        {
            Dictionary<int, bool> visitTasksIds = new Dictionary<int, bool>();
            if (ExistingVisit != null)
            {
                List<Task> tasksOnVisit = Task.FindByVisit(ExistingVisit);
                foreach (Task task in tasksOnVisit)
                    visitTasksIds.Add(task.ID, true);

                int i = 0;
                while (i < tasksOnVisit.Count)
                {
                    if (tasksOnVisit[i].ParentTaskId.HasValue
                        && !visitTasksIds.ContainsKey(tasksOnVisit[i].ParentTaskId.Value))
                    {
                        Task taskToBeInserted = Task.FindByPrimaryKey(tasksOnVisit[i].ParentTaskId.Value);
                        tasksOnVisit.Add(taskToBeInserted);
                        visitTasksIds.Add(taskToBeInserted.ID, false);
                    }

                    i++;
                }
            }

            return visitTasksIds;
        }

        #endregion

        #region RefreshTasks

        public void RefreshTasks()
        {
            if (m_currentAddress == null && ExistingVisit == null)
            {
                m_tasks = new BindingList<TaskProjectWrapper>();
                return;
            }

            Dictionary<int, bool> visitTasksIds = GetTasksShouldInTree();

            List<TaskProjectWrapper> tasksToBeLeft = new List<TaskProjectWrapper>();
            foreach (TaskProjectWrapper task in m_tasks)
            {
                if (task.IsProject && task.IsNewlyAdded)
                    tasksToBeLeft.Add(task);
                else if (task.IsNewlyAdded && task.Task.ProjectId <= 0)
                    tasksToBeLeft.Add(task);
            }

            List<Task> tasks;
            tasks = m_currentAddress == null ? Task.FindByVisit(ExistingVisit) : Task.FindByServiceAddress(m_currentAddress);

            List<Task> oldFailedTasks = new List<Task>();
            foreach (Task task in tasks)
            {
                if (task.TaskFailType == TaskFailTypeEnum.Cancel
                    && task.CreateDate < DateTime.Now.AddDays(-30))
                {
                    oldFailedTasks.Add(task);
                }                    
            }

            foreach (Task task in oldFailedTasks)
                tasks.Remove(task);

            m_tasks = new BindingList<TaskProjectWrapper>();
            Dictionary<int, Task> projects = new Dictionary<int, Task>();
            foreach (Task task in tasks)
            {
                TaskProjectWrapper wrapper = new TaskProjectWrapper(task, false);

                if (visitTasksIds.ContainsKey(task.ID))
                {
                    wrapper.IsExistInVisit = visitTasksIds[task.ID];
                    wrapper.IsExistInVisitInitially = wrapper.IsExistInVisit;
                    m_tasks.Add(wrapper);

                    if (!projects.ContainsKey(task.ProjectId))
                        projects.Add(task.ProjectId, task);

                } else if (!IsReadOnly)
                {
                    m_tasks.Add(wrapper);

                    if (!projects.ContainsKey(task.ProjectId))
                        projects.Add(task.ProjectId, task);
                }
                                    

                if (task.TaskType == TaskTypeEnum.RugDelivery
                    || task.TaskType == TaskTypeEnum.RugPickup)
                {
                    wrapper.Items = Item.FindByTask(task);

                } else if (task.TaskType == TaskTypeEnum.Deflood)
                {
                    task.DefloodDetail = DefloodDetail.FindByPrimaryKey(task.ID);

                } else if (task.TaskType == TaskTypeEnum.Monitoring)
                {
                    task.MonitoringDetail = MonitoringDetail.FindByPrimaryKey(task.ID);
                    task.MonitoringReadings = MonitoringReading.FindBy(task, null);
                }
            }

            foreach (Task task in projects.Values)
                m_tasks.Add(new TaskProjectWrapper(task, true));
            SetIncludedExcludedProjects();

            foreach (TaskProjectWrapper wrapper in tasksToBeLeft)
                m_tasks.Add(wrapper);
        }

        #endregion

        #region AddNew

        public void AddNew(TaskTypeEnum taskType, TaskProjectWrapper parent)
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
                decimal.Zero, true,
                decimal.Zero, false,
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
                task.Project.CreateDate = DateTime.Now;
                task.Project.Description = string.Empty;
                if (taskType == TaskTypeEnum.Deflood)
                    task.Project.ProjectType = ProjectTypeEnum.Deflood;
                else if (taskType == TaskTypeEnum.RugPickup)
                    task.Project.ProjectType = ProjectTypeEnum.RugCleaning;
                else if (taskType == TaskTypeEnum.Miscellaneous)
                    task.Project.ProjectType = ProjectTypeEnum.Miscellaneous;

                if (m_baseLead != null)
                    task.Project.QbCustomerTypeListId = m_baseLead.BusinessPartner.QbCustomerTypeListId;
                
                if (CurrentCustomer != null)
                {
                    try
                    {
                        QbCustomer qbcustomer = QbCustomer.FindParent(CurrentCustomer.ID,null);

                        if (!string.IsNullOrEmpty(qbcustomer.QbCustomerTypeListId))
                            task.Project.QbCustomerTypeListId = qbcustomer.QbCustomerTypeListId;

                        if (!string.IsNullOrEmpty(qbcustomer.QbSalesRepListId))
                            task.Project.QbSalesRepListId = qbcustomer.QbSalesRepListId;
                    }
                    catch (DataNotFoundException){}
                }

                m_tasks.Add(new TaskProjectWrapper(task, true));
            } else
            {
                task.ProjectId = parent.Task.Project.ID;
                task.Project = parent.Task.Project;
                if (!parent.IsProject)
                    task.ParentTaskId = parent.Task.ID;
            }

            TaskProjectWrapper newlyAddedTask = new TaskProjectWrapper(task, false);
            m_tasks.Add(newlyAddedTask);
            SetIncludedExcludedProjectByTask(newlyAddedTask);

            if (parent == null && taskType == TaskTypeEnum.Deflood)
                AddNew(TaskTypeEnum.Monitoring, newlyAddedTask);
        }

        #endregion

        #region DeleteProjectIfEmpty

        public void DeleteProjectIfEmpty(string projectId)
        {
            foreach (TaskProjectWrapper task in m_tasks)
            {
                if (task.ParentId == projectId)
                    return;
            }

            foreach (TaskProjectWrapper task in m_tasks)
            {
                if (task.ID == projectId)
                {
                    m_tasks.Remove(task);
                    return;
                }
                    
            }

        }

        #endregion

        #region IsAddMonitoringAllowed

        public bool IsAddMonitoringAllowed(TaskProjectWrapper task)
        {
            if (!task.IsAddMonitoringAllowed)
                return false;

            foreach (TaskProjectWrapper wrapper in m_tasks)
            {
                if (wrapper.ParentId == task.ID && wrapper.Task.TaskType == TaskTypeEnum.Monitoring
                    && wrapper.IsNewlyAdded)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region GetTasksToBeAddedToVisit

        public List<TaskProjectWrapper> GetTasksToBeAddedToVisit()
        {
            List<TaskProjectWrapper> result = new List<TaskProjectWrapper>();

            foreach (TaskProjectWrapper task in m_tasks)
            {
                if (task.IsNewlyAdded || task.IsIncludedInVisit || task.IsExistInVisit)
                    result.Add(task);
            }

            return result;
        }

        #endregion

        #region GetRootTasksOnProject

        private List<TaskProjectWrapper> GetRootTasksOnProject(TaskProjectWrapper project)
        {
            List<TaskProjectWrapper> result = new List<TaskProjectWrapper>();
            foreach (TaskProjectWrapper task in m_tasks)
            {
                if (!task.IsProject
                    && project.Task.ProjectId == task.Task.ProjectId && !task.Task.ParentTaskId.HasValue)
                {
                    result.Add(task);
                }                    
            }

            return result;
        }

        #endregion

        #region InsertUpdateTaskWithChilds

        private void InsertUpdateTaskWithChilds(TaskProjectWrapper initialTask, Visit visit)
        {
            List<TaskProjectWrapper> childTasks = new List<TaskProjectWrapper>();

            foreach (TaskProjectWrapper task in m_tasks)
            {
                if (task.Task.ParentTaskId == initialTask.Task.ID)
                    childTasks.Add(task);
            }

            if (initialTask.IsNewlyAdded)
            {
                initialTask.Task.CreateDate = DateTime.Now;
                initialTask.Task.ServiceDate = visit.ServiceDate;
                Task.InsertWithDetails(initialTask.Task);
                initialTask.Task.Number = initialTask.Task.ID.ToString();
                Task.UpdateWithDetails(initialTask.Task);

                VisitTask visitTask = new VisitTask(visit.ID, initialTask.Task.ID);
                VisitTask.Insert(visitTask);                

            } else if (initialTask.IsIncludedInVisit || initialTask.IsExistInVisit)
            {
                if (initialTask.IsIncludedInVisit)
                {
                    initialTask.Task.ServiceDate = visit.ServiceDate;

                    if (initialTask.IsFailed)
                        initialTask.Task.TaskFailTypeId = null;
                    if (initialTask.IsCompleted)
                    {
                        if (initialTask.Task.TaskType == TaskTypeEnum.Deflood)
                            initialTask.Task.TaskStatus = TaskStatusEnum.InProcess;
                        else
                            initialTask.Task.TaskStatus = TaskStatusEnum.NotCompleted;
                    }

                    initialTask.Task.IsReincluded = true;                    
                } 
                
                Task.UpdateWithDetails(initialTask.Task);

                VisitTask visitTask = new VisitTask(visit.ID, initialTask.Task.ID);
                VisitTask.Insert(visitTask);
            }
            else if (initialTask.IsExistInVisitInitially && !initialTask.IsExistInVisit)
            {
                if (initialTask.Task.TaskFailTypeId.HasValue)
                {
                    initialTask.Task.FailCount++;
                    initialTask.Task.LastFailDate = DateTime.Now;
                }

                Task.UpdateWithDetails(initialTask.Task);
            }
                
            foreach (TaskProjectWrapper childTask in childTasks)
            {
                childTask.Task.ParentTaskId = initialTask.Task.ID;
                childTask.Task.ProjectId = initialTask.Task.ProjectId;
                InsertUpdateTaskWithChilds(childTask, visit);
            }

        }

        #endregion

        #region CreateVisit

        public CreateVisitResultEnum CreateVisit(Visit visit)
        {            
            if (ExistingVisit != null)
            {
                Visit.Update(visit);                
                VisitTask.DeleteBy(visit);
            }
            else
                Visit.Insert(visit);

            foreach (TaskProjectWrapper task in m_tasks)
            {
                if (task.IsProject)
                {
                    List<TaskProjectWrapper> projectTasks = GetRootTasksOnProject(task);

                    task.Task.Project.CustomerId = visit.CustomerId;
                    task.Task.Project.ServiceAddressId = visit.ServiceAddressId;

                    if (task.IsNewlyAdded)
                    {
                        task.Task.Project.ProjectStatus = ProjectStatusEnum.Open;

                        if (BaseLead != null)
                            task.Task.Project.LeadId = BaseLead.ID;

                        Project.InsertAndLog(task.Task.Project);
                    }
                    else if (task.IsIncludedInVisit)
                    {
                        Project.UpdateAndLog(task.Task.Project);
                    }

                    foreach (TaskProjectWrapper projectTask in projectTasks)
                    {
                        projectTask.Task.ProjectId = task.Task.Project.ID;
                        InsertUpdateTaskWithChilds(projectTask, visit);
                    }
                }
            }

            if (BaseLead != null)
            {
                BaseLead.Lead.Status = LeadStatusEnum.Converted;
                Lead.Save(BaseLead.Lead);
            }

            foreach (TaskProjectWrapper task in m_tasks)
            {
                if (task.IsProject)
                    continue;

                if (task.Task.TaskType != TaskTypeEnum.RugPickup)
                    continue;

//                if (!(task.IsNewlyAdded || task.IsIncludedInVisit || task.IsExistInVisit))
//                    continue;

                if (task.Items == null)
                    task.Items = new List<Item>();

                List<Item> existingItems = Item.FindByTask(task.Task);
                Dictionary<int, Item> existingItemsMap = new Dictionary<int, Item>();
                foreach (Item item in existingItems)
                    existingItemsMap.Add(item.ID, item);

                foreach (Item item in task.Items)
                {
                    if (item.ID <= 0)
                    {
                        item.ItemType = ItemTypeEnum.Rug;
                        item.TaskId = task.Task.ID;
                        Item.Insert(item);
                    }
                    else // if (task.Task.TaskType == TaskTypeEnum.RugPickup)
                    {
                        Item.Update(item);
                        existingItemsMap.Remove(item.ID);
                    }
                }

                foreach (Item item in existingItemsMap.Values)
                    Item.Delete(item);
            }

            bool isVisitDeleted = false;
            if (ExistingVisit != null)
            {
                List<Task> tasks = Task.FindByVisit(visit);
                if (tasks.Count == 0)
                {
                    List<WorkTransaction> workTransactions = WorkTransaction.FindBy(visit);
                    foreach (WorkTransaction workTransaction in workTransactions)
                    {
                        WorkTransactionEtc.Delete(new WorkTransactionEtc(workTransaction.ID));
                        WorkTransaction.Delete(workTransaction);
                    }

                    WorkDetail.DeleteBy(visit);
                    Visit.Delete(visit);
                    isVisitDeleted = true;
                }

                if (visit.VisitStatus != VisitStatusEnum.Pending)
                {
                    foreach (Task task in tasks)
                    {
                        if (!task.IsReady)
                            throw new CreateVisitException("Visit is placed on dashboard and cannot contain not ready tasks");
                    }
                }
            }

            foreach (TaskProjectWrapper task in m_tasks)
            {
                if (task.IsProject)
                    Project.UpdateStatus(task.Task.Project);
            }

            if (!isVisitDeleted && visit.IsNeedToPrint(m_existingVisitSummaryPackage, m_isGeneratedVisitAdjustment))
                return CreateVisitResultEnum.PrintVisit;
            return CreateVisitResultEnum.DoNotPrintVisit;
        }

        #endregion
       
        #region FindDefloodTask

        public TaskProjectWrapper FindDefloodTask(TaskProjectWrapper monitoring)
        {
            foreach (TaskProjectWrapper task in m_tasks)
            {
                if (!task.IsProject
                    && task.Task.TaskType == TaskTypeEnum.Deflood
                    && monitoring.Task.ParentTaskId == task.Task.ID)
                {
                    return task;
                }
            }

            return null;
        }

        #endregion

        #region FindMonitoringTask

        public TaskProjectWrapper FindMonitoringTask(TaskProjectWrapper deflood)
        {
            foreach (TaskProjectWrapper task in m_tasks)
            {
                if (!task.IsProject
                    && task.Task.TaskType == TaskTypeEnum.Monitoring
                    && task.Task.ParentTaskId == deflood.Task.ID)
                {
                    return task;
                }
            }

            return null;
        }

        #endregion

        #region SetIncludedExcludedProjects

        private void SetIncludedExcludedProjects()
        {
            foreach (TaskProjectWrapper wrapper in m_tasks)
            {
                if (wrapper.IsProject)
                    SetIncludedExcludedProject(wrapper);
            }
        }

        #endregion

        #region SetIncludedExcludedProject

        private void SetIncludedExcludedProject(TaskProjectWrapper project)
        {
            int position = m_tasks.IndexOf(project);

            foreach (TaskProjectWrapper task in m_tasks)
            {
                if (!task.IsProject && task.Task.ProjectId == project.Task.ProjectId
                    && (task.IsIncludedInVisit || task.IsExistInVisit))
                {
                    project.IsIncludedInVisit = true;
                    m_tasks.ResetItem(position);
                    return;
                }
            }
            project.IsIncludedInVisit = false;
            m_tasks.ResetItem(position);
        }

        #endregion

        #region IncludeExcludeInVisit

        private void SetIncludedExcluded(TaskProjectWrapper task, bool isInclude)
        {
            if (task.IsExistInVisitInitially)
                task.IsExistInVisit = isInclude;
            else
                task.IsIncludedInVisit = isInclude;
            SetIncludedExcludedProjectByTask(task);
        }

        public void SetIncludedExcludedProjectByTask(TaskProjectWrapper task)
        {
            if (task.IsProject)
                return;

            foreach (TaskProjectWrapper wrapper in m_tasks)
            {
                if (wrapper.IsProject && task.Task.ProjectId == wrapper.Task.Project.ID)
                {
                    SetIncludedExcludedProject(wrapper);
                    return;
                }
            }
        }

        public void IncludeExcludeInVisit(TaskProjectWrapper task, bool isInclude)
        {
            SetIncludedExcluded(task, isInclude);
            ClearRestoreFailType(task, isInclude);

            if (task.Task.TaskType == TaskTypeEnum.Monitoring)
            {
                TaskProjectWrapper deflood = FindDefloodTask(task);
                SetIncludedExcluded(deflood, isInclude);
                m_tasks.ResetItem(m_tasks.IndexOf(deflood));

            } if (task.Task.TaskType == TaskTypeEnum.Deflood)
            {
                if (task.Task.TaskStatus == TaskStatusEnum.Completed)
                {
                    if (isInclude)
                        AddNew(TaskTypeEnum.Monitoring, task);
                    else
                    {
                        foreach (TaskProjectWrapper wrapper in m_tasks)
                        {
                            if (!wrapper.IsProject && (wrapper.IsNewlyAdded || wrapper.IsExistInVisit)
                                && wrapper.Task.TaskType == TaskTypeEnum.Monitoring
                                && wrapper.Task.ParentTaskId == task.Task.ID)
                            {
                                m_tasks.Remove(wrapper);
                                return;
                            }
                        }
                    }
                    
                } else
                {
                    TaskProjectWrapper monitoring = FindMonitoringTask(task);
                    SetIncludedExcluded(monitoring, isInclude);

                    ClearRestoreFailType(monitoring, isInclude);

                    m_tasks.ResetItem(m_tasks.IndexOf(monitoring));                                    
                }

            }


//            foreach (TaskProjectWrapper subTask in FindAllSubTasks(task))
//            {
//                if (subTask.IsFailed 
//                    && (subTask.Task.TaskFailType == TaskFailTypeEnum.MayReturn
//                        || subTask.Task.TaskFailType == TaskFailTypeEnum.Cancel))
//                {
//                    IncludeExcludeInVisit(subTask, isInclude);                
//                }
//            }                
        }

        private void ClearRestoreFailType(TaskProjectWrapper task, bool isInclude)
        {
            if (isInclude)
            {
                task.FailTypeIdBeforeInclude = task.Task.TaskFailTypeId;
                task.Task.TaskFailTypeId = null;
            }
            else
            {
                task.Task.TaskFailTypeId = task.FailTypeIdBeforeInclude;
                task.FailTypeIdBeforeInclude = null;
            }                            
        }

        #endregion

        #region CancelTask

        public void CancelTaskWithSubtasks(TaskProjectWrapper task)
        {
            foreach (TaskProjectWrapper subTask in FindAllSubTasks(task))
            {
                if (!subTask.IsNewlyAdded)
                    CancelTask(subTask);
            }
                
            CancelTask(task);
        }

        private void CancelTask(TaskProjectWrapper task)
        {
            SetIncludedExcluded(task, false);
            task.Task.TaskFailType = TaskFailTypeEnum.Cancel;
            //task.Task.FailReason = string.Empty;
        }

        #endregion

        #region FindAllSubTasks

        private List<TaskProjectWrapper> FindAllSubTasks(TaskProjectWrapper task)
        {
            List<TaskProjectWrapper> result = new List<TaskProjectWrapper>();

            if (task.IsProject)
            {
                foreach (TaskProjectWrapper wrapper in m_tasks)
                {
                    if (!wrapper.IsProject && wrapper.Task.ProjectId == task.Task.ProjectId)
                    {
                        result.Add(wrapper);
                    }
                }

                return result;
            }


            foreach (TaskProjectWrapper wrapper in m_tasks)
            {
                if (wrapper.Task.ParentTaskId.HasValue && wrapper.Task.ParentTaskId == task.Task.ID)
                {
                    result.Add(wrapper);
                    result.AddRange(FindAllSubTasks(wrapper));
                }                    
            }

            return result;
        }

        #endregion

        #region IsDeleteAllowed

        //do not take care if it has sub tasks or it is project
        private bool IsTaskDeleteAllowed(TaskProjectWrapper task)
        {
            return task.IsNewlyAdded;            
        }

        public bool IsDeleteAllowed(TaskProjectWrapper task)
        {
            if (task.Task.TaskType == TaskTypeEnum.Monitoring)
                return false;

            foreach (TaskProjectWrapper subTask in FindAllSubTasks(task))
            {
                if (!IsTaskDeleteAllowed(subTask))
                    return false;
            }

            if (task.IsProject)
                return true;
            return IsTaskDeleteAllowed(task);            
        }

        #endregion

        #region FindNotValidProject

        public TaskProjectWrapper FindNotValidProject()
        {
            foreach (TaskProjectWrapper task in m_tasks)
            {
                if (task.IsProject && (task.IsIncludedInVisit || task.IsExistInVisit))
                {
                   if (!QbCustomerType.CanBeSelected(task.Task.Project.QbCustomerTypeListId))
                       return task;

                   if (string.IsNullOrEmpty(task.Task.Project.QbCustomerTypeListId))
                       return task;
                }
            }

            return null;
        }

        #endregion

        #region RefreshHistoryOrders

        public void RefreshHistoryOrders(Customer customer)
        {
            m_historyOrders = new List<h_order>();

            if (customer.ServmanCustId == string.Empty)
                return;

            try
            {
                DalworthSyncService syncService = new DalworthSyncService();

                h_order[] ordersServman = syncService.GetCustomerOrderHistory(
                    Configuration.ConnectionKey, customer.ServmanCustId);

                m_historyOrders = new List<h_order>(ordersServman);
            }
            catch (Exception ex)
            {
                Host.Trace("CreateVisitModel:RefreshHistoryOrders", "Error: " + ex);
            }
        }

        #endregion

        #region DeleteTask

        public void DeleteTask(TaskProjectWrapper task)
        {
            List<TaskProjectWrapper> itemsToBeRemoved = new List<TaskProjectWrapper>();
            itemsToBeRemoved.Add(task);
            itemsToBeRemoved.AddRange(FindChilds(task));

            foreach (TaskProjectWrapper item in itemsToBeRemoved)
                Tasks.RemoveAt(Tasks.IndexOf(item));

            if (!task.IsProject)
                DeleteProjectIfEmpty(TaskProjectWrapper.ProjectIdPreffix + task.Task.ProjectId);
        }

        private List<TaskProjectWrapper> FindChilds(TaskProjectWrapper parent)
        {
            List<TaskProjectWrapper> result = new List<TaskProjectWrapper>();
            foreach (TaskProjectWrapper task in m_tasks)
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

        #region IsPromptForServiceDate

        public IsPromptForServiceDateEnum IsPromptForServiceDate(Visit visit)
        {
            if (!visit.ServiceDate.HasValue)
            {
                foreach (TaskProjectWrapper task in Tasks)
                {
                    if (task.IsProject && task.Task.TaskType == TaskTypeEnum.Deflood && task.IsIncludedInVisit)
                    {
                        if (task.Task.TaskStatusId == (int)TaskStatusEnum.InProcess)
                            return IsPromptForServiceDateEnum.MonitoringPrompt;

                        if (task.Task.TaskStatusId == (int)TaskStatusEnum.NotCompleted)
                            return IsPromptForServiceDateEnum.DefloodPrompt;
                    }   
                }
            }

            return IsPromptForServiceDateEnum.NoPrompt;
        }

        #endregion
    }

    public class TaskProjectWrapper
    {
        public static readonly string ProjectIdPreffix = "project";

        #region Task

        private Task m_task;
        public Task Task
        {
            get { return m_task; }
            set { m_task = value; }
        }

        #endregion

        #region Constructor

        public TaskProjectWrapper(Task task, bool isProject)
        {
            m_task = task;
            m_isProject = isProject;
            
            m_originalMessage = task.Message;
        }

        #endregion

        #region ParentId

        public string ParentId
        {
            get
            {
                if (m_isProject)
                    return null;
                if (m_task.ParentTaskId.HasValue)
                    return m_task.ParentTaskId.Value.ToString();
                
                return ProjectIdPreffix + m_task.ProjectId;
            }            
        }

        #endregion

        #region IsProject

        private readonly bool m_isProject;
        public bool IsProject
        {
            get { return m_isProject; }
        }

        #endregion

        #region Name

        public string Name
        {
            get
            {
                if (m_isProject)
                    return m_task.Project.ProjectTypeText + " project";
                return m_task.TaskTypeText;
            }
        }

        #endregion

        #region ID

        public string ID
        {
            get
            {
                if (m_isProject)
                    return ProjectIdPreffix + m_task.Project.ID;
                return m_task.ID.ToString();
            }
        }

        #endregion

        #region Number

        public string Number
        {
            get
            {
                if (m_isProject)
                    return m_task.ProjectId.ToString();

                return string.Empty;
            }
        }

        #endregion

        #region IsNewlyAdded

        public bool IsNewlyAdded
        {
            get
            {
                if (IsProject)
                {
                    if (int.Parse(ID.Replace(ProjectIdPreffix, string.Empty)) < 0)
                        return true;
                    
                    return false;
                }

                return m_task.ID < 0;
            }
        }

        #endregion

        #region IsIncludedInVisit

        private bool m_isIncludedInVisit;
        public bool IsIncludedInVisit
        {
            get { return m_isIncludedInVisit || IsNewlyAdded; }
            set { m_isIncludedInVisit = value; }
        }

        #endregion

        #region IsExistInVisit

        private bool m_isExistInVisit;
        public bool IsExistInVisit
        {
            get { return m_isExistInVisit || m_isIncludedInVisit; }
            set { m_isExistInVisit = value; }
        }

        #endregion

        #region IsExistInVisitInitially

        private bool m_isExistInVisitInitially;
        public bool IsExistInVisitInitially
        {
            get { return m_isExistInVisitInitially; }
            set { m_isExistInVisitInitially = value; }
        }

        #endregion

        #region IsIncludeInVisitAllowed

        public bool IsIncludeInVisitAllowed
        {
            get
            {
                if (IsProject || IsNewlyAdded || IsIncludedInVisit || IsExistInVisit)
                    return false;

                if (IsFailed && m_task.TaskFailType == TaskFailTypeEnum.MustReturn)
                    return false;

                if (m_task.TaskType == TaskTypeEnum.Monitoring)
                    return false;

                if (IsCompleted && (m_task.TaskType == TaskTypeEnum.RugPickup
                    || m_task.TaskType == TaskTypeEnum.RugDelivery || m_task.TaskType == TaskTypeEnum.Monitoring))
                    return false;

                return (IsFailed  || IsCompleted || IsExistInVisitInitially);
            }            
        }

        #endregion

        #region IsExcludeFromVisitAllowed

        public bool IsExcludeFromVisitAllowed
        {
            get
            {
                if (IsProject)
                    return false;
                if (m_task.TaskFailType == TaskFailTypeEnum.MustReturn)
                    return false;
                if (m_task.TaskType == TaskTypeEnum.Deflood && m_task.TaskStatus == TaskStatusEnum.InProcess && !m_task.IsReincluded)
                    return false;
                if (m_task.TaskType == TaskTypeEnum.Monitoring)
                    return false;
                if (IsIncludedInVisit && !IsNewlyAdded)
                    return true;
//                if (IsExistInVisit)
//                    return true;
                return false;
            }
        }

        #endregion

        #region ImageIndex

        public int ImageIndex
        {
            get
            {
                if (IsProject)
                    return 1;
                if (IsNewlyAdded)
                    return 0;
                if (IsIncludedInVisit)
                    return 3;
                if (IsExistInVisit)
                    return 3;
                return 2;
            }
        }

        #endregion

        #region IsAddHelpAllowed

        public bool IsAddHelpAllowed
        {
            get
            {
                if (m_task.TaskType == TaskTypeEnum.Help || IsNewlyAdded || IsExistInVisitInitially || IsIncludedInVisit)
                    return false;
                if (m_task.TaskFailTypeId.HasValue || m_task.TaskStatus == TaskStatusEnum.Completed)
                    return false;
                return true;
            }
        }

        #endregion

        #region IsAddMiscAllowed

        public bool IsAddMiscAllowed
        {
            get
            {
                return m_isProject;

//                if (m_isProject)
//                    return true;
//                if (m_task.TaskType == TaskTypeEnum.Miscellaneous || m_task.TaskType == TaskTypeEnum.Help)
//                    return false;
//                if ((m_task.TaskFailTypeId.HasValue || m_task.TaskStatus == TaskStatusEnum.Completed) && !m_isIncludedInVisit && !IsExistInVisit)
//                    return false;
//                if (IsExistInVisitInitially && !IsExistInVisit)
//                    return false;
//                return true;
            }
        }

        #endregion

        #region IsAddMonitoringAllowed

        public bool IsAddMonitoringAllowed
        {
            get
            {
                if (m_isProject)
                    return false;
                if (m_task.TaskFailTypeId.HasValue || m_task.TaskStatus == TaskStatusEnum.Completed)
                    return false;
                if (m_task.TaskType == TaskTypeEnum.Deflood)
                    return true;
                return false;
            }
        }

        #endregion

        #region IsAddRugPickupAllowed

        public bool IsAddRugPickupAllowed
        {
            get
            {
                if (!m_isProject)
                    return false;
                if (m_task.Project.ProjectType == ProjectTypeEnum.Deflood)
                    return true;
                return false;
            }
        }

        #endregion

        #region IsCancelAllowed

        public bool IsCancelAllowed(Visit visit)
        {
            if (m_isProject)
                return false;

            if (visit != null 
                && (visit.VisitStatus == VisitStatusEnum.Arrived || visit.VisitStatus == VisitStatusEnum.Completed))
            {
                return false;
            }                

            if (m_task.TaskType == TaskTypeEnum.RugDelivery || m_task.TaskType == TaskTypeEnum.Monitoring
                || (m_task.TaskType == TaskTypeEnum.Deflood
                    && (m_task.TaskStatus == TaskStatusEnum.Completed || m_task.TaskStatus == TaskStatusEnum.InProcess)))
            {
                return false;
            }

            if (IsNewlyAdded || !IsExistInVisit || IsIncludedInVisit)
                return false;

            return true;
            
        }

        #endregion

        #region IsFailed

        public bool IsFailed
        {
            get
            {
                return !IsProject && m_task.TaskFailTypeId.HasValue;
            }
        }

        #endregion

        #region IsNotReady

        public bool IsNotReady
        {
            get
            {
                return !IsProject && !m_task.IsReady;
            }
        }

        #endregion

        #region IsCompleted

        public bool IsCompleted
        {
            get
            {
                return !IsProject && m_task.TaskStatus == TaskStatusEnum.Completed;
            }
        }

        #endregion

        #region StatusImageIndex

        public string StatusImageIndex
        {
            get
            {
                if (IsFailed)
                {
                    if (m_task.TaskFailType == TaskFailTypeEnum.MustReturn)
                        return "F-RET";
                    if (m_task.TaskFailType == TaskFailTypeEnum.Cancel)
                        return "F-C";
                }
                    
                if (IsCompleted)
                    return "DONE";
                if (IsNotReady)
                    return "NR";
                return string.Empty;                
            }
        }

        #endregion

        #region FailTypeIdBeforeInclude

        private int? m_failTypeIdBeforeInclude;
        public int? FailTypeIdBeforeInclude
        {
            get { return m_failTypeIdBeforeInclude; }
            set { m_failTypeIdBeforeInclude = value; }
        }

        #endregion

        #region IsVisible

        private bool m_isVisible;
        public bool IsVisible
        {
            get { return m_isVisible; }
            set { m_isVisible = value; }
        }

        #endregion

        #region Items

        private List<Item> m_items;
        public List<Item> Items
        {
            get { return m_items; }
            set { m_items = value; }
        }

        #endregion

        #region OriginalMessage

        private string m_originalMessage;
        public string OriginalMessage
        {
            get { return m_originalMessage; }
        }

        #endregion

        #region FailCount

        public string FailCount
        {
            get
            {
                if (IsProject)
                    return string.Empty;

                return m_task.FailCount == 0 ? string.Empty : m_task.FailCount.ToString();
            }
        }

        #endregion

        #region LastFailDate

        public string LastFailDate
        {
            get 
            {
                if (IsProject)
                    return string.Empty;

                return m_task.LastFailDate.HasValue 
                             ? m_task.LastFailDate.Value.ToShortDateString() : string.Empty; 
            }
        }

        #endregion

        #region Lead

        private Lead m_lead;
        public Lead Lead
        {
            get { return m_lead; }
            set { m_lead = value; }
        }

        #endregion
    }
}
