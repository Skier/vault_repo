using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Xml.Serialization;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Domain
{
    public partial class Task
    {
        public Task(){ }        

        #region Project

        private Project m_project;
        public Project Project
        {
            get { return m_project; }
            set { m_project = value; }
        }

        #endregion

        #region DefloodDetail

        private DefloodDetail m_defloodDetail;        
        public DefloodDetail DefloodDetail
        {
            get { return m_defloodDetail; }
            set { m_defloodDetail = value; }
        }

        #endregion

        #region MonitoringDetail

        private MonitoringDetail m_monitoringDetail;
        public MonitoringDetail MonitoringDetail
        {
            get { return m_monitoringDetail; }
            set { m_monitoringDetail = value; }
        }

        #endregion

        #region MonitoringReadings

        private List<MonitoringReading> m_monitoringReadings;
        public List<MonitoringReading> MonitoringReadings
        {
            get { return m_monitoringReadings; }
            set { m_monitoringReadings = value; }
        }

        #endregion

        #region TaskType

        [XmlIgnore]
        public TaskTypeEnum TaskType
        {
            get { return (TaskTypeEnum)m_taskTypeId; }
            set { m_taskTypeId = (int)value; }
        }

        #endregion

        #region TaskTypeText
        
        public string TaskTypeText
        {
            get
            {
                return Domain.TaskType.GetText(TaskType);
            }
        }

        #endregion

        #region TaskStatus

        [XmlIgnore]
        public TaskStatusEnum TaskStatus
        {
            get { return (TaskStatusEnum)m_taskStatusId; }
            set { m_taskStatusId = (int)value; }
        }

        #endregion

        #region TaskStatusText

        public string TaskStatusText
        {
            get
            {
                return Domain.TaskStatus.GetText(TaskStatus);
            }
        }

        #endregion

        #region TaskFailType

        [XmlIgnore]
        public TaskFailTypeEnum? TaskFailType
        {
            get { return (TaskFailTypeEnum?)m_taskFailTypeId; }
            set { m_taskFailTypeId = (int?)value; }
        }

        #endregion

        #region TaskFailTypeText

        public string TaskFailTypeText
        {
            get
            {
                return Domain.TaskFailType.GetText(TaskFailType.Value);
            }
        }

        #endregion

        #region VisitLink

        public string VisitLink
        {
            get
            {
                return "Visit";
            }
        }

        #endregion

        #region DashboardLink

        public string DashboardLink
        {
            get
            {
                return "Dashboard";
            }
        }

        #endregion

        #region ProjectLink

        public string ProjectLink
        {
            get
            {
                return "Project";
            }
        }

        #endregion

        #region IsPreviousNotesInitialized

        private bool m_isPreviousNotesInitialized;        
        public bool IsPreviousNotesInitialized
        {
            get { return m_isPreviousNotesInitialized; }
            set { m_isPreviousNotesInitialized = value; }
        }

        #endregion

        #region PreviousNotes

        private string m_previousNotes;
        public string PreviousNotes
        {
            get { return m_previousNotes; }
            set { m_previousNotes = value; }
        }

        #endregion

        #region FindByServmanOrderNum

        private const string SqlFindByServmanOrder =
            @"SELECT *
            FROM Task
                WHERE ServmanOrderNum = ?ServmanOrderNum";

        public static Task FindByServmanOrderNum(string servmanOrderNum)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByServmanOrder))
            {
                Database.PutParameter(dbCommand, "?ServmanOrderNum", servmanOrderNum);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }
            throw new DataNotFoundException("Order not found");
        }

        #endregion

        #region FindTasksToBeExported

        private const string SqlFindTasksToBeExported =
            @"SELECT *
            FROM Task
                WHERE 
                    (TaskTypeId = 1 or TaskTypeId = 2 or TaskTypeId = 5 or TaskTypeId = 6 or TaskTypeId = 7)
                    and (Modified > LastSyncDate or LastSyncDate is null) and DumpedTaskId is null
            order by TaskTypeId, ID";

        public static List<Task> FindTasksToBeExported()
        {
            List<Task> result = new List<Task>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindTasksToBeExported))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion

        #region IsFirstMonitoring

        public static bool IsFirstMonitoring(Task monitoring)
        {
            if (!monitoring.ParentTaskId.HasValue)
                return false;

            Task deflood = FindByPrimaryKey(monitoring.ParentTaskId.Value);
            List<Task> monitorings = FindMonitorings(deflood);

            foreach (Task task in monitorings)
            {
                if (task.ID < monitoring.ID)
                    return false;
            }

            return true;
        }

        #endregion

        #region IsFirstTimeDeflood

        public bool IsFirstTimeDeflood
        {
            get
            {
                return TaskType == TaskTypeEnum.Deflood
                       && TaskStatus == TaskStatusEnum.NotCompleted;
            }
        }

        #endregion

        #region IsAmountNotKnown

        private bool m_isAmountNotKnown;
        public bool IsAmountNotKnown
        {
            get { return m_isAmountNotKnown; }
            set { m_isAmountNotKnown = value; }
        }

        #endregion

        #region GetTasksFiltered
        //filter Deflood and Monitoring tasks depending on which visit
        public static List<Task> GetTasksFiltered(IList<Task> tasks, Visit visit)
        {
            List<Task> result = new List<Task>();
            
            foreach (Task task in tasks)
            {
                if (visit.VisitStatus != VisitStatusEnum.Completed && task.TaskType == TaskTypeEnum.Deflood)
                {
                    if (task.IsFirstTimeDeflood)
                        result.Add(task);
                    else
                        result.Add(FindMonitoringByDeflood(tasks, task));
                }
                else if (visit.VisitStatus == VisitStatusEnum.Completed && task.TaskType == TaskTypeEnum.Monitoring)
                {
                    if (IsFirstMonitoring(task))
                        result.Add(FindDefloodByMonitoring(tasks, task));
                    else
                        result.Add(task);
                }
                else if (task.TaskType != TaskTypeEnum.Monitoring && task.TaskType != TaskTypeEnum.Deflood)
                    result.Add(task);
            }

            return result;
        }

        #endregion

        #region Find Deflood by monitoring and vice versa

        private static Task FindDefloodByMonitoring(IList<Task> tasks, Task monitoring)
        {
            foreach (Task task in tasks)
            {
                if (task.TaskType == TaskTypeEnum.Deflood && monitoring.ParentTaskId == task.ID)
                    return task;
            }

            return null;
        }

        private static Task FindMonitoringByDeflood(IList<Task> tasks, Task deflood)
        {
            foreach (Task task in tasks)
            {
                if (task.TaskType == TaskTypeEnum.Monitoring && task.ParentTaskId == deflood.ID)
                    return task;
            }

            return null;
        }

        #endregion

        #region InitPreviousTaskNotes

        public void InitPreviousTaskNotes()
        {
            if (TaskType == TaskTypeEnum.Monitoring || TaskType == TaskTypeEnum.RugDelivery)
            {
                if (!IsPreviousNotesInitialized && ID > 0)
                {
                    if (TaskType == TaskTypeEnum.RugDelivery)
                    {
                        Task rugPickup = FindRugPickup(this);
                        PreviousNotes = rugPickup.Notes;

                    }
                    else if (TaskType == TaskTypeEnum.Monitoring)
                    {
                        try
                        {
                            Task prevMonitoring
                                = FindPrevMonitoring(new Task(ParentTaskId.Value), this);
                            PreviousNotes = prevMonitoring.Notes;
                        }
                        catch (DataNotFoundException) { }
                    }
                }                
            }

            m_isPreviousNotesInitialized = true;
        }

        #endregion

        #region FindByNumber

        private const string SqlFindByNumber =
            @"SELECT *
            FROM Task
                WHERE Number = ?Number";

        public static Task FindByNumber(string number)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByNumber))
            {
                Database.PutParameter(dbCommand, "?Number", number);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }
            throw new DataNotFoundException("Order not found");
        }

        #endregion

        #region FindByVisit

        private const string SqlFindByVisit =
            @"select t.*, p.* from Task t
                inner join VisitTask vt on vt.TaskId = t.ID
                inner join Visit v on v.ID = vt.VisitId
                inner join Project p on p.ID = t.ProjectId
                where v.ID = ?VisitId
                order by t.ID";

        public static List<Task> FindByVisit(Visit visit, IDbConnection connection)
        {
            List<Task> tasks = new List<Task>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByVisit, connection))
            {
                Database.PutParameter(dbCommand, "?VisitId", visit.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Task task = Load(dataReader);
                        task.Project = Project.Load(dataReader, FieldsCount);
                        tasks.Add(task);
                        
                    }                        
                }
            }
            return tasks;
        }

        public static List<Task> FindByVisit(Visit visit)
        {
            return FindByVisit(visit, null);
        }

        #endregion

        #region FindNotIncludedParents

        public static List<Task> FindNotIncludedParents(Visit visit)
        {
            List<Task> result = new List<Task>();
            List<Task> includedTasks = FindByVisit(visit);
            List<int> includedTaskIds = new List<int>();

            foreach (Task includedTask in includedTasks)
                includedTaskIds.Add(includedTask.ID);

            foreach (Task includedTask in includedTasks)
            {
                if (includedTask.ParentTaskId.HasValue 
                    && !includedTaskIds.Contains(includedTask.ParentTaskId.Value))
                {
                    Task firstParentTask = FindByPrimaryKey(includedTask.ParentTaskId.Value);
                    firstParentTask.Project = Domain.Project.FindByPrimaryKey(firstParentTask.ProjectId);
                    List<Task> parentTasks = FindAllParents(firstParentTask);
                    parentTasks.Add(firstParentTask);

                    foreach (Task parentTask in parentTasks)
                    {
                        if (!includedTaskIds.Contains(parentTask.ID))
                        {
                            result.Add(parentTask);
                            includedTaskIds.Add(parentTask.ID);
                        }
                    }
                }
            }

            return result;
        }

        #endregion

        #region FindAllParents

        public static List<Task> FindAllParents(Task task)
        {
            List<Task> result = new List<Task>();
            if (!task.ParentTaskId.HasValue)
                return result;
            else
            {
                Task parentTask = FindByPrimaryKey(task.ParentTaskId.Value);
                parentTask.Project = Domain.Project.FindByPrimaryKey(parentTask.ProjectId);
                result.Add(parentTask);
                result.AddRange(FindAllParents(parentTask));
            }

            return result;
        }

        #endregion

        #region FindMonitorings

        private const string SqlFindMonitorings =
            @"SELECT *
            FROM Task
                WHERE ParentTaskId = ?ParentTaskId
                    and TaskTypeId = 5
                    and DumpedTaskId is null
            order by ID desc";


        public static List<Task> FindMonitorings(Task defloodTask)
        {
            List<Task> tasks = new List<Task>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindMonitorings))
            {
                Database.PutParameter(dbCommand, "?ParentTaskId", defloodTask.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        tasks.Add(Load(dataReader));
                }
            }
            return tasks;
        }

        #endregion       

        #region FindNextMonitoring

        private const string SqlFindNextMonitoring =
            @"SELECT *
            FROM Task
                WHERE ParentTaskId = ?ParentTaskId
                    and TaskTypeId = 5
                    and DumpedTaskId is null
                    and ID > ?PrevousMonitoringId
            order by ID
            limit 1";

        public static Task FindNextMonitoring(Task defloodTask, Task previousMonitoring)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindNextMonitoring))
            {
                Database.PutParameter(dbCommand, "?ParentTaskId", defloodTask.ID);
                Database.PutParameter(dbCommand, "?PrevousMonitoringId", previousMonitoring.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("Next monitoring task not found");
        }

        #endregion       

        #region FindPrevMonitoring

        private const string SqlFindPrevMonitoring =
            @"SELECT *
            FROM Task
                WHERE ParentTaskId = ?ParentTaskId
                    and TaskTypeId = 5
                    and DumpedTaskId is null
                    and ID < ?MonitoringId
            order by ID desc
            limit 1";

        public static Task FindPrevMonitoring(Task defloodTask, Task monitoring)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindPrevMonitoring))
            {
                Database.PutParameter(dbCommand, "?ParentTaskId", defloodTask.ID);
                Database.PutParameter(dbCommand, "?MonitoringId", monitoring.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("Previous monitoring task not found");
        }

        #endregion       

        #region MarkTasksModifiedBy

        private const string SqlMarkTaskModifiedBy =
            @"UPDATE Task t, (SELECT TaskId FROM VisitTask WHERE VisitTask.VisitId = ?VisitId) t2
	            SET t.Modified = NOW()
	        WHERE t.ID = t2.TaskId";


        public static void MarkTasksModifiedBy(Visit visit)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlMarkTaskModifiedBy))
            {
                Database.PutParameter(dbCommand, "?VisitId", visit.ID);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion

        #region MarkTaskModified

        private const string SqlMarkTaskModified =
            @"UPDATE Task SET Task.Modified = NOW() 
                WHERE ID = ?ID";

        public static void MarkTaskModified(Task task)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlMarkTaskModified))
            {
                Database.PutParameter(dbCommand, "?ID", task.ID);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion

        #region FindByProject

        private const string SqlFindByProject =
            @"SELECT *
            FROM Task
                WHERE ProjectId = ?ProjectId";

        public static List<Task> FindByProject(Project project)
        {
            return FindByProject(project, null);
        }

        public static List<Task> FindByProject(Project project, IDbConnection connection)
        {
            List<Task> tasks = new List<Task>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByProject, connection))
            {
                Database.PutParameter(dbCommand, "?ProjectId", project.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        tasks.Add(Load(dataReader));
                }
            }
            return tasks;
        }

        #endregion

        #region FindByProject

        private const string SqlFindByProjectTypeAndStatus =
            @"SELECT *
            FROM Task
                WHERE ProjectId = ?ProjectId
                 and TaskTypeId = ?TaskTypeId
                 and TaskStatusId = ?TaskStatusId";

        public static List<Task> FindByProjectTypeAndStatus(Project project, TaskTypeEnum type, TaskStatusEnum status)
        {
            List<Task> tasks = new List<Task>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByProjectTypeAndStatus))
            {
                Database.PutParameter(dbCommand, "?ProjectId", project.ID);
                Database.PutParameter(dbCommand, "?TaskTypeId", (int)type);
                Database.PutParameter(dbCommand, "?TaskStatusId", (int)status);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        tasks.Add(Load(dataReader));
                }
            }
            return tasks;
        }

        #endregion

        #region FindByServiceAddress

        public static List<Task> FindByServiceAddress(Address serviceAddress)
        {
            string SqlFindByServiceAddress =
                @"select * from Task t
                    inner join Project p on p.ID = t.ProjectId
                    where p.ServiceAddressId = ?ServiceAddressId ";

            List<Task> tasks = new List<Task>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByServiceAddress))
            {
                Database.PutParameter(dbCommand, "?ServiceAddressId", serviceAddress.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Task task = Load(dataReader);
                        task.Project = Project.Load(dataReader, FieldsCount);
                        tasks.Add(task);
                    }
                        
                }
            }
            return tasks;
        }

        #endregion

        #region Find By Type and Status

        private const string SqlFindByTypeAndStatus =
            @"SELECT *
            FROM Task
                WHERE TaskTypeId = ?TaskTypeId
                    and TaskStatusId = ?TaskStatusId";

        public static List<Task> FindBy(TaskTypeEnum type, TaskStatusEnum status)
        {
            List<Task> tasks = new List<Task>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTypeAndStatus))
            {
                Database.PutParameter(dbCommand, "?TaskTypeId", (int)type);
                Database.PutParameter(dbCommand, "?TaskStatusId", (int)status);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        tasks.Add(Load(dataReader));
                }
            }
            return tasks;
        }

        #endregion

        #region GetLatestImportedTask

        private const string SqlGetLatestImportedTask =
            @"SELECT *
            FROM Task
                Order by ServmanOrderNum desc
            LIMIT 1";


        public static Task GetLatestImportedTask()
        {
            Task task = null;

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlGetLatestImportedTask))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        task = Load(dataReader);
                    }
                }
            }

            if (task == null || task.ServmanOrderNum == null || task.ServmanOrderNum == string.Empty)
                throw new DataNotFoundException("No imported tasks were found");

            return task;

        }

        #endregion

        #region InsertWithDetails

        public static void InsertWithDetails(Task task, IDbConnection connection)
        {
            Insert(task, connection);
            if (task.TaskType == TaskTypeEnum.Deflood)
            {
                task.DefloodDetail.DefloodTaskId = task.ID;
                DefloodDetail.Insert(task.DefloodDetail, connection);
            }                
            else if (task.TaskType == TaskTypeEnum.Monitoring)
            {
                task.MonitoringDetail.MonitoringTaskId = task.ID;
                MonitoringDetail.Insert(task.MonitoringDetail, connection);

                if (task.MonitoringReadings != null)
                    foreach (MonitoringReading reading in task.MonitoringReadings)
                    {
                        reading.MonitoringTaskId = task.ID;
                        MonitoringReading.Insert(reading, connection);
                    }
                    
            }                
        }

        public static void InsertWithDetails(Task task)
        {
            InsertWithDetails(task, null);
        }

        #endregion

        #region UpdateWithDetails

        public static void UpdateWithDetails(Task task, IDbConnection connection)
        {
            if (task.IsReady && task.ReadyDate == null)
                task.ReadyDate = DateTime.Now;
            else if (!task.IsReady)
                task.ReadyDate = null;

            Update(task, connection);

            if (task.TaskType == TaskTypeEnum.Deflood)
                DefloodDetail.Update(task.DefloodDetail, connection);
            else if (task.TaskType == TaskTypeEnum.Monitoring)
            {
                MonitoringDetail.Update(task.MonitoringDetail, connection);

                List<MonitoringReading> existingReadings = MonitoringReading.FindBy(task, connection);
                if (task.MonitoringReadings != null)
                {

                    foreach (MonitoringReading reading in task.MonitoringReadings)
                    {
                        MonitoringReading existingReading = null;

                        foreach (MonitoringReading existingReadingEx in existingReadings)
                        {
                            if (existingReadingEx.ID == reading.ID)
                            {
                                existingReading = existingReadingEx;
                                break;                                
                            }
                        }

                        reading.MonitoringTaskId = task.ID;

                        if (existingReading == null)
                            MonitoringReading.Insert(reading, connection);
                        else
                        {
                            MonitoringReading.Update(reading, connection);
                            existingReadings.Remove(existingReading);
                        }                                                    
                    }

                    foreach (MonitoringReading readingToBeDeleted in existingReadings)
                        MonitoringReading.Delete(readingToBeDeleted, connection);
                        
                } else
                {
                    foreach (MonitoringReading reading in existingReadings)
                        MonitoringReading.Delete(reading, connection);
                }
                    
            }
        }

        public static void UpdateWithDetails(Task task)
        {
            UpdateWithDetails(task, null);
        }

        #endregion        

        #region Dump

        public static Task Dump(Task task, int? workTransactionId, int dumpedProjectId)
        {
            Task taskToInsert = FindByPrimaryKey(task.ID);

            if (taskToInsert.TaskType == TaskTypeEnum.Deflood)
                taskToInsert.DefloodDetail = Domain.DefloodDetail.FindByPrimaryKey(taskToInsert.ID);
            else if (taskToInsert.TaskType == TaskTypeEnum.Monitoring)
            {
                taskToInsert.MonitoringDetail = MonitoringDetail.FindByPrimaryKey(taskToInsert.ID);
                taskToInsert.MonitoringReadings = MonitoringReading.FindBy(taskToInsert, null);
            }

            List<Item> items = Item.FindByTask(taskToInsert);

            taskToInsert.ProjectId = dumpedProjectId;
            taskToInsert.DumpedTaskId = taskToInsert.ID;
            taskToInsert.DumpWorkTransactionId = workTransactionId;
            InsertWithDetails(taskToInsert);            
            
            foreach (Item item in items)
            {
                item.TaskId = taskToInsert.ID;
                Item.Insert(item);
            }
            
            return taskToInsert;
        }

        #endregion

        #region Restore

        public static void Restore(Task dumpedTask)
        {
            Task freshOriginalTask = FindByPrimaryKey(dumpedTask.DumpedTaskId.Value);

            if (dumpedTask.TaskType == TaskTypeEnum.Deflood)
            {
                DefloodDetail defloodDetail = DefloodDetail.FindByPrimaryKey(dumpedTask.ID);
                defloodDetail.DefloodTaskId = dumpedTask.DumpedTaskId.Value;
                DefloodDetail.Update(defloodDetail);
            }                
            else if (dumpedTask.TaskType == TaskTypeEnum.Monitoring)
            {
                MonitoringDetail monitoringDetail = MonitoringDetail.FindByPrimaryKey(dumpedTask.ID);
                monitoringDetail.MonitoringTaskId = dumpedTask.DumpedTaskId.Value;
                MonitoringDetail.Update(monitoringDetail);

                MonitoringReading.DeleteBy(new Task(dumpedTask.DumpedTaskId.Value), null);
                List<MonitoringReading> monitoringReadings = MonitoringReading.FindBy(dumpedTask, null);
                foreach (MonitoringReading reading in monitoringReadings)
                {
                    reading.MonitoringTaskId = dumpedTask.DumpedTaskId.Value;
                    MonitoringReading.Insert(reading);
                }
            }

            List<Item> itemsOld = Item.FindByTask(new Task(dumpedTask.DumpedTaskId.Value));
            foreach (Item item in itemsOld)
                Item.Delete(item);

            List<Item> itemsNew;
            Task rugPickup = null;
            if (freshOriginalTask.TaskType == TaskTypeEnum.RugDelivery)
            {
                rugPickup = FindRugPickup(freshOriginalTask);
                itemsNew = Item.FindByTask(rugPickup);
            } else 
                itemsNew = Item.FindByTask(dumpedTask);
            
            foreach (Item item in itemsNew)
            {
                item.TaskId = dumpedTask.DumpedTaskId.Value;
                Item.Insert(item);
            }


            Project dumpedProject = Project.FindByPrimaryKey(dumpedTask.ProjectId);            

            Task originalTask = (Task) dumpedTask.Clone();
            originalTask.ServmanOrderNum = freshOriginalTask.ServmanOrderNum;
            originalTask.ID = dumpedTask.DumpedTaskId.Value;
            originalTask.ProjectId = dumpedProject.DumpedProjectId.Value;
            originalTask.DumpedTaskId = null;
            originalTask.DumpWorkTransactionId = null;

            if (rugPickup != null && dumpedProject.ProjectType == ProjectTypeEnum.RugCleaning)
            {
                originalTask.IsClosedAmountAutoCalculated = rugPickup.IsClosedAmountAutoCalculated;
                originalTask.DiscountPercentage = rugPickup.DiscountPercentage;
                originalTask.EstimatedClosedAmount = rugPickup.EstimatedClosedAmount;
            }

            Update(originalTask);   
        }

        #endregion

        #region FindDumpedTask

        private const string SqlFindDumpedTask =
            @"SELECT *
            FROM Task
                WHERE DumpedTaskId = ?DumpedTaskId
                    and DumpWorkTransactionId = ?DumpWorkTransactionId";

        public static Task FindDumpedTask(Task task, int workTransactionId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindDumpedTask))
            {
                Database.PutParameter(dbCommand, "?DumpedTaskId", task.ID);
                Database.PutParameter(dbCommand, "?DumpWorkTransactionId", workTransactionId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("dumped task not found");
        }

        #endregion

        #region FindBy WorkTransaction and Operation

        private const string SqlFindByWorkTransactionAndOperation =
            @"select t.* from WorkTransactionTask wtt
                inner join Task t on t.ID = wtt.TaskId
                WHERE WorkTransactionId = ?WorkTransactionId            
                    and IsModified = ?IsModified
                    and IsCreated = ?IsCreated";

        public static List<Task> FindBy(WorkTransaction transaction,
            bool isModified, bool isCreated)
        {
            List<Task> result = new List<Task>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWorkTransactionAndOperation))
            {
                Database.PutParameter(dbCommand, "?WorkTransactionId", transaction.ID);
                Database.PutParameter(dbCommand, "?IsModified", isModified);
                Database.PutParameter(dbCommand, "?IsCreated", isCreated);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion

        #region DeleteDeep
        
        public static void DeleteDeep(Task task)
        {
            List<Item> items = Item.FindByTask(task);
            foreach (Item item in items)
                Item.Delete(item);

            if (task.TaskType == TaskTypeEnum.Deflood)
            {
                Domain.DefloodDetail.Delete(Domain.DefloodDetail.FindByPrimaryKey(task.ID));
            }                
            else if (task.TaskType == TaskTypeEnum.Monitoring)
            {
                MonitoringDetail.Delete(MonitoringDetail.FindByPrimaryKey(task.ID));
                MonitoringReading.DeleteBy(task, null);
            }

            VisitTask.DeleteBy(task);
            if (task.DumpedTaskId == null 
                && task.TaskType != TaskTypeEnum.RugDelivery 
                && !string.IsNullOrEmpty(task.ServmanOrderNum))
            {
                DeletedTask deletedTask = new DeletedTask(task.ServmanOrderNum, null);
                DeletedTask.Insert(deletedTask);
            }
            Delete(task);            
        }

        public static void DeleteDeep(List<Task> tasks)
        {
            List<Task> tasksCopy = new List<Task>(tasks);

            int index = 0;
            while (tasksCopy.Count > 0)
            {
                bool isDeleteAllowed = true;

                foreach (Task taskInner in tasksCopy)
                {
                    if (taskInner.ParentTaskId.HasValue && tasksCopy[index].ID == taskInner.ParentTaskId)
                    {
                        isDeleteAllowed = false;
                        break;
                    }
                }

                if (isDeleteAllowed)
                {
                    DeleteDeep(tasksCopy[index]);
                    tasksCopy.Remove(tasksCopy[index]);
                }
                else
                    index++;

                if (index > tasksCopy.Count - 1)
                    index = 0;
            }
            
        }

        #endregion

        #region FindRugDelivery

        public static Task FindRugDelivery(Task rugPickupTask)
        {
            Project project = Domain.Project.FindByPrimaryKey(rugPickupTask.ProjectId);
            List<Task> projectTasks = FindByProject(project);

            foreach (Task task in projectTasks)
            {
                if (task.TaskType == TaskTypeEnum.RugDelivery)
                    return task;
            }

            throw new DataNotFoundException("Delivery task not found");
        }

        #endregion

        #region FindRugPickup

        public static Task FindRugPickup(Task rugDeliveryTask)
        {
            Project project = Domain.Project.FindByPrimaryKey(rugDeliveryTask.ProjectId);
            List<Task> projectTasks = FindByProject(project);

            foreach (Task task in projectTasks)
            {
                if (task.TaskType == TaskTypeEnum.RugPickup)
                    return task;
            }

            throw new DataNotFoundException("RugPickup task not found");
        }

        #endregion

        #region GetEstimatedClosedAmount

        public decimal GetEstimatedClosedAmount(List<Item> items, bool isAutocalculated, 
            decimal discountPercentage)
        {
            if ((TaskType == TaskTypeEnum.RugPickup || TaskType == TaskTypeEnum.RugDelivery)
                && Project.ProjectType == ProjectTypeEnum.RugCleaning)
            {
                return isAutocalculated ? Item.GetItemsCost(items, false, discountPercentage) : EstimatedClosedAmount;
            }

//            if (TaskType == TaskTypeEnum.RugDelivery
//                && Project.ProjectType == ProjectTypeEnum.RugCleaning)
//            {
//                return EstimatedClosedAmount;
//            }

            if ((TaskType == TaskTypeEnum.RugPickup || TaskType == TaskTypeEnum.RugDelivery)
                && Project.ProjectType == ProjectTypeEnum.Deflood)
            {
                return isAutocalculated ? Item.GetItemsCost(items, true, discountPercentage) : ClosedAmount;
            }

            return decimal.Zero;
        }

        #endregion

        #region GetPrintedAmount

        public decimal GetPrintedAmount(List<Item> items)
        {
            if (TaskType == TaskTypeEnum.RugPickup
                && Project.ProjectType == ProjectTypeEnum.RugCleaning)
            {
                return EstimatedClosedAmount;
            }

            if (TaskType == TaskTypeEnum.RugDelivery
                && Project.ProjectType == ProjectTypeEnum.RugCleaning)
            {
                if (TaskStatus == TaskStatusEnum.Completed)
                    return ClosedAmount;
                return EstimatedClosedAmount;
            }

            if (TaskType == TaskTypeEnum.RugPickup
                && Project.ProjectType == ProjectTypeEnum.Deflood)
            {
                if (TaskStatus == TaskStatusEnum.Completed)
                    return ClosedAmount;
                return EstimatedClosedAmount;
            }

            return ClosedAmount;
        }

        #endregion

        #region FindGeneratedTasksOnCompletion

        public static List<Task> FindGeneratedTasksOnCompletion(Visit visit)
        {
            return FindGeneratedTasksOnCompletion(visit, FindByVisit(visit));
        }

        #endregion

        #region FindGeneratedTasksOnCompletion

        public static List<Task> FindGeneratedTasksOnCompletion(Visit visit, List<Task> tasksToProcess)
        {
            List<Task> result = new List<Task>();
            
            WorkTransaction completeTransaction =
                WorkTransaction.FindBy(visit.ID, WorkTransactionTypeEnum.VisitCompleted);

            foreach (Task task in tasksToProcess)
            {
                WorkTransactionTask taskTransaction =
                    WorkTransactionTask.FindByPrimaryKey(completeTransaction.ID, task.ID);

                if (task.TaskType == TaskTypeEnum.RugPickup
                    && taskTransaction.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.Complete)
                {
                    result.Add(FindRugDelivery(task));
                }

                if (task.TaskType == TaskTypeEnum.Monitoring
                    && taskTransaction.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.Complete)
                {
                    try
                    {
                        Task nextMonitoring = FindNextMonitoring(
                            new Task(task.ParentTaskId.Value), task);
                        result.Add(nextMonitoring);
                    }
                    catch (DataNotFoundException) { }
                }
            }

            return result;
        }

        #endregion

        #region FindTransferredTasksOnCompletion

        public static List<Task> FindTransferredTasksOnCompletion(Visit visit)
        {
            return FindTransferredTasksOnCompletion(visit, FindByVisit(visit));
        }

        #endregion

        #region FindTransferredTasksOnCompletion

        public static List<Task> FindTransferredTasksOnCompletion(Visit visit, List<Task> tasksToProcess)
        {
            List<Task> result = new List<Task>();
            WorkTransaction completeTransaction =
                WorkTransaction.FindBy(visit.ID, WorkTransactionTypeEnum.VisitCompleted);

            foreach (Task task in tasksToProcess)
            {
                WorkTransactionTask taskTransaction =
                    WorkTransactionTask.FindByPrimaryKey(completeTransaction.ID, task.ID);

                if (taskTransaction.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.FailMustReturn)
                    result.Add(task);

                if (taskTransaction.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.Cancel
                    || taskTransaction.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.Complete
                    || taskTransaction.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.InProcess)
                {
                    try
                    {
                        Visit.FindNextVisit(task, visit);
                        result.Add(task);
                    }
                    catch (DataNotFoundException) { }
                }
            }

            return result;
        }

        #endregion

        #region FindBookedTasksOnCompletion

        public static List<Task> FindBookedTasksOnCompletion(Visit visit)
        {
            WorkTransaction completeTransaction =
                WorkTransaction.FindBy(visit.ID, WorkTransactionTypeEnum.VisitCompleted);

            List<WorkTransactionTask> taskTransactions =
                WorkTransactionTask.FindBy(completeTransaction, WorkTransactionTaskActionEnum.Booked);

            List<Task> result = new List<Task>();
            foreach (WorkTransactionTask taskTransaction in taskTransactions)
            {
                result.Add(FindByPrimaryKey(taskTransaction.TaskId));
            }

            return result;
        }

        public static List<Task> FindBookedTasksOnCompletion(Visit visit, List<Task> tasksToProcess)
        {
            WorkTransaction completeTransaction =
                WorkTransaction.FindBy(visit.ID, WorkTransactionTypeEnum.VisitCompleted);

            List<WorkTransactionTask> taskTransactions =
                WorkTransactionTask.FindBy(completeTransaction, WorkTransactionTaskActionEnum.Booked);

            List<Task> result = new List<Task>();

            foreach (Task tasksToProces in tasksToProcess)
            {
                foreach (WorkTransactionTask taskTransaction in taskTransactions)
                {
                    if (tasksToProces.ID == taskTransaction.TaskId)
                        result.Add(tasksToProces);
                }
            }

            return result;
        }


        #endregion

        #region FindMaxImportedServmanOrderNumber

        private const string SqlFindMaxImportedServmanOrderNumber =
            @"SELECT ServmanOrderNum FROM Task
                WHERE ServmanOrderNum != ''
              order by ServmanOrderNum desc
              limit 1";

        public static string FindMaxImportedServmanOrderNumber()
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindMaxImportedServmanOrderNumber))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return dataReader.GetString(0);
                        
                }
            }

            throw new DataNotFoundException("Max imported servman order number not found");
        }

        #endregion

        #region FindTaskWrappersOnProjectsScreen

        private const string SqlFindTaskWrappersOnProjectsScreenCompletedAndProcessedDefloods =
            @"SELECT t.*, min(w.StartDate) FROM task t
                left join WorkTransactionTask wtt on wtt.TaskId = t.ID
                    and (t.TaskStatusId in( 2, 4) and wtt.WorkTransactionTaskActionId in (1, 2) -- in process and complete                    
                    )
                left join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
                left join Work w on w.ID = wt.WorkId
            where ProjectId = ?ProjectId and t.TaskTypeId = 4 and t.TaskStatusId != 1
            group by t.ID
            order by t.ID";

        private const string SqlFindTaskWrappersOnProjectsScreen =
            @"SELECT t.*, max(w.StartDate) FROM task t
                left join WorkTransactionTask wtt on wtt.TaskId = t.ID
                    and ((t.TaskStatusId = 2 and wtt.WorkTransactionTaskActionId = 1) -- completed
                        or (t.TaskStatusId = 1 and t.TaskFailTypeId is not null and t.TaskFailTypeId = 1 and wtt.WorkTransactionTaskActionId = 3) -- fail must return
                        or (t.TaskStatusId = 1 and t.TaskFailTypeId is not null and t.TaskFailTypeId = 2 and wtt.WorkTransactionTaskActionId = 4) -- fail may return
                        or (t.TaskStatusId = 1 and t.TaskFailTypeId is not null and t.TaskFailTypeId = 3 and wtt.WorkTransactionTaskActionId = 5) -- fail may return
                    )
                left join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
                left join Work w on w.ID = wt.WorkId
            where ProjectId = ?ProjectId
                and ((t.TaskTypeId = 4 and t.TaskStatusId = 1) or t.TaskTypeId != 4) -- completed and inprocess defloods will be processed in separate query
            group by t.ID
            order by t.ID";

        public static BindingList<TaskWrapperOnProjectsScreen> FindTaskWrappersOnProjectsScreen(Project project)
        {
            BindingList<TaskWrapperOnProjectsScreen> result = new BindingList<TaskWrapperOnProjectsScreen>();

            if (project.ProjectType == ProjectTypeEnum.Deflood)
            {
                using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindTaskWrappersOnProjectsScreenCompletedAndProcessedDefloods))
                {
                    Database.PutParameter(dbCommand, "?ProjectId", project.ID);

                    using (IDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                            result.Add(TaskWrapperOnProjectsScreen.Load(dataReader));
                    }
                }
            }

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindTaskWrappersOnProjectsScreen))
            {
                Database.PutParameter(dbCommand, "?ProjectId", project.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(TaskWrapperOnProjectsScreen.Load(dataReader));
                }
            }

            return result;            
        }

        #endregion

        #region FindInvoiceDate

        private const string SqlFindInvoceDateDeflood =
            @"select min(wd.TimeEnd)
                from task
                join worktransactiontask wtt on wtt.taskid = task.id and wtt.worktransactiontaskactionid in (2,1)
                join worktransaction wt on wt.id = wtt.worktransactionid
                join visit on wt.visitid = visit.id and visit.visitstatusid = 2
                join workdetail wd on wd.visitid = visit.id
                where task.tasktypeid = 4
                and task.taskstatusid in (2,4)
                and task.taskfailtypeid is null
                and task.id = ?TaskId
            ";

        private const string SqlFindInvoceDate =
            @"select max(wd.TimeEnd)
              from task
              join worktransactiontask wtt on wtt.taskid = task.id and wtt.worktransactiontaskactionid  = 1
              join worktransaction wt on wt.id = wtt.worktransactionid
              join visit on wt.visitid = visit.id and visit.visitstatusid = 2
              join workdetail wd on wd.visitid = visit.id
             where task.taskstatusid = 2
                   and task.taskfailtypeid is null
                   and task.id = ?TaskId
            ";

        public DateTime FindInvoiceDate(IDbConnection connection)
        {
            string sql;

            if (TaskType == TaskTypeEnum.Deflood)
                sql = SqlFindInvoceDateDeflood;
            else
                sql = SqlFindInvoceDate;

            using (IDbCommand dbCommand = Database.PrepareCommand(sql, 
                 connection))
            {
                Database.PutParameter(dbCommand, "?TaskId", ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        DateTime result = dataReader.GetDateTime(0);
                        return result;
                    }
                }
            }

            throw new DataNotFoundException("");
        }

        #endregion
    }

    public enum TaskActionEnum
    {
        Complete = 1,
        InProcess = 2,
        Fail = 3,
        Book = 5
    }

    public class TaskProjectWrapperComplete
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

        public TaskProjectWrapperComplete() {}

        public TaskProjectWrapperComplete(Task task, bool isProject, bool isIncludedInVisit)
        {
            m_task = task;
            m_isProject = isProject;
            m_isIncludedInVisit = isIncludedInVisit;

            if (!isProject && isIncludedInVisit)
                m_isDefloodFirstTimeService = task.IsFirstTimeDeflood;                
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
                else
                    return ProjectIdPreffix + m_task.ProjectId;
            }
        }

        #endregion

        #region IsProject

        private bool m_isProject;
        public bool IsProject
        {
            get { return m_isProject; }
            set { m_isProject = value; }
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

        private bool? m_isNewlyAdded;
        public bool IsNewlyAdded
        {
            get
            {
                if (m_isNewlyAdded.HasValue)
                    return m_isNewlyAdded.Value;

                if (IsProject)
                {
                    if (int.Parse(ID.Replace(ProjectIdPreffix, string.Empty)) < 0)
                        m_isNewlyAdded = true;
                    else
                        m_isNewlyAdded = false;
                } else
                    m_isNewlyAdded = m_task.ID < 0;

                return m_isNewlyAdded.Value;
            }
            set { m_isNewlyAdded = value; }
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
                return 2;
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
//                return true;
            }
        }

        #endregion

        #region IsDeleteAllowed

        public bool IsDeleteAllowed
        {
            get
            {
                return IsNewlyAdded && m_task.TaskType != TaskTypeEnum.Monitoring;
            }
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

        #region TaskActionId

        private int m_taskActionId;
        public int TaskActionId
        {
            get
            {
                if (m_taskActionId == 0)
                {
                    if (IsProject || !IsIncludedInVisit)
                        m_taskActionId = -1;
                    else if (Task.TaskType == TaskTypeEnum.Deflood)
                        m_taskActionId = (int)TaskActionEnum.InProcess;
                    else
                        m_taskActionId = (int)TaskActionEnum.Complete;
                }

                return m_taskActionId;
            }
            set
            {
                m_taskActionId = value;
            }
        }

        #endregion

        #region TaskAction
        
        public TaskActionEnum TaskAction
        {
            get { return (TaskActionEnum) TaskActionId; }
            set { TaskActionId = (int)value; }
        }

        #endregion

        #region IsTaskActionEditAllowed

        public bool IsTaskActionEditAllowed
        {
            get
            {
                if (IsProject || !IsIncludedInVisit)
                    return false;
                return true;
            }
        }

        #endregion

        #region IsDefloodFirstTimeService

        private bool m_isDefloodFirstTimeService;
        public bool IsDefloodFirstTimeService
        {
            get { return m_isDefloodFirstTimeService; }
            set { m_isDefloodFirstTimeService = value; }
        }

        #endregion

        #region IsMonitoringFirstTimeService

        private bool m_isMonitoringFirstTimeService;
        public bool IsMonitoringFirstTimeService
        {
            get { return m_isMonitoringFirstTimeService || IsNewlyAdded; }
            set { m_isMonitoringFirstTimeService = value; }
        }

        #endregion

        #region GetItemsCost

        private decimal GetItemsCost()
        {
            decimal result = decimal.Zero;

            if (m_items == null)
                return result;

            foreach (Item item in m_items)
                result += item.SubTotalCost;
            return result;
        }

        #endregion

        #region ProjectClosedAmountWithoutListedTasks

        private decimal m_projectClosedAmountWithoutListedTasks;
        public decimal ProjectClosedAmountWithoutListedTasks
        {
            get { return m_projectClosedAmountWithoutListedTasks; }
            set { m_projectClosedAmountWithoutListedTasks = value; }
        }

        #endregion

        #region CostText

        public string CostText
        {
            get
            {

                if (IsProject)
                    return m_task.Project.ClosedAmount.ToString("C");

                if (m_task.TaskType == TaskTypeEnum.Help || m_task.ClosedAmount == Decimal.Zero)
                    return string.Empty;
                return m_task.ClosedAmount.ToString("C");
            }
        }

        #endregion        
    }

    public class TaskWrapperOnProjectsScreen
    {
        private Task m_task;
        private DateTime? m_processDate;

        #region Task

        public Task Task
        {
            get { return m_task; }
        }

        #endregion


        #region Constructor

        public TaskWrapperOnProjectsScreen(Task task, DateTime? processDate)
        {
            m_task = task;
            m_processDate = processDate;
        }

        #endregion

        #region Load

        public static TaskWrapperOnProjectsScreen Load(IDataReader dataReader)
        {
            Task task = Task.Load(dataReader);
            DateTime? processDate = null;

            if (!dataReader.IsDBNull(Task.FieldsCount))
                processDate = dataReader.GetDateTime(Task.FieldsCount);

            if (task.TaskFailTypeId.HasValue)
                processDate = task.LastFailDate;

            return new TaskWrapperOnProjectsScreen(task, processDate);
        }

        #endregion

        #region Number

        public string Number
        {
            get { return m_task.Number; }
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
                if (m_task.TaskStatus == TaskStatusEnum.InProcess)
                    return "Completed";
                if (m_task.TaskStatus == TaskStatusEnum.Completed)
                    return m_task.TaskStatusText;                

                if (m_task.TaskStatus == TaskStatusEnum.NotCompleted)
                {
                    if (m_task.TaskFailType == null)
                        return m_task.TaskStatusText;

                    return m_task.TaskFailTypeText;                    
                }

                throw new DalworthException("Task status not found");
            }
        }

        #endregion

        #region ProcessDate

        public DateTime? ProcessDate
        {
            get { return m_processDate; }
        }

        #endregion

        #region ClosedAmount

        public decimal? ClosedAmount
        {
            get { return m_task.ClosedAmount == decimal.Zero ? (decimal?)null : m_task.ClosedAmount; }
        }

        #endregion

        #region VisitLink

        public string VisitLink
        {
            get { return "Visit"; }
        }

        #endregion

        #region DashboardLink

        public string DashboardLink
        {
            get { return "Dashboard"; }
        }

        #endregion

        #region ServmanOrderNum

        public string ServmanOrderNum
        {
            get { return m_task.ServmanOrderNum; }
        }

        #endregion
    }
}
      