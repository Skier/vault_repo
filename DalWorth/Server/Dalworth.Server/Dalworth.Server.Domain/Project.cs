using System;
using System.ComponentModel;
using System.Data;
using System.Collections.Generic;
using System.Xml.Serialization;

using Dalworth.Server.Data;
using Dalworth.Server.SDK;
using Database = Dalworth.Server.Data.Database;

namespace Dalworth.Server.Domain
{
    public partial class Project
    {
        public Project () { }

        #region ProjectType

        [XmlIgnore]
        public ProjectTypeEnum ProjectType
        {
            get { return (ProjectTypeEnum)m_projectTypeId; }
            set { m_projectTypeId = (int)value; }
        }

        #endregion

        #region ExpectedSalesRepListId

        private string m_expectedQbSalresRepListId;
        public string ExpectedQbSalesRepListId
        {
            get { return m_expectedQbSalresRepListId; }
            set { m_expectedQbSalresRepListId = value; }
        }

        #endregion 

        #region ProjectTypeText

        [XmlIgnore]
        public string ProjectTypeText
        {
            get { return Domain.ProjectType.GetText(ProjectType); }
        }

        #endregion

        #region ProjectStatus

        [XmlIgnore]
        public ProjectStatusEnum ProjectStatus
        {
            get { return (ProjectStatusEnum)m_projectStatusId; }
            set { m_projectStatusId = (int)value; }
        }

        #endregion

        #region ProjectStatus

        [XmlIgnore]
        public string ProjectStatusText
        {
            get { return Domain.ProjectStatus.GetText(ProjectStatus); }
        }

        #endregion

        #region FindByCustomerId

        private const String SqlFindByCustomer =
                @"Select * from Project
                where CustomerId = ?CustomerId";

        public static List<Project> FindByCustomerId(int customerId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByCustomer))
            {
                Database.PutParameter(dbCommand, "?CustomerId", customerId);

                List<Project> rv = new List<Project>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        rv.Add(Load(dataReader));
                    }
                }
                return rv;
            }

        }

        #endregion

        #region FindByVisitId

        private const String SqlFindByVisitId =
                @"select p.*
                    from project p
                    where id in
                    (
                        select t.projectid
                        from task t
                        join visittask vt  on vt.taskid = t.id
                        where vt.visitid = ?VisitId and t.dumpedtaskid is null
                    )
                    and p.DumpedProjectId is null";

        public static List<Project> FindByVisitId(int visitId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByVisitId, connection))
            {
                Database.PutParameter(dbCommand, "?VisitId", visitId);

                List<Project> rv = new List<Project>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        rv.Add(Load(dataReader));
                    }
                }
                return rv;
            }

        }

        #endregion

        #region IsLatest

        private const String SqlIsLatest =
                @"select 'x' from project 
                where customerid = ?CustomerID 
                        and ID > ?ProjectId 
                        and projecttypeid = ?ProjectTypeId";

        public static bool IsLatest(Project project)
        {
            bool result = true;

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlIsLatest))
            {
                Database.PutParameter(dbCommand, "?CustomerId", project.CustomerId);
                Database.PutParameter(dbCommand, "?ProjectId", project.ID);
                Database.PutParameter(dbCommand, "?ProjectTypeId", project.ProjectTypeId);   

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if(dataReader.Read())
                    {
                        result = false;
                    }
                }   
            }

            return result;
        }

        #endregion

        #region GetProjectWrappers

        public static BindingList<ProjectWrapper> FindProjectWrappers(string jobNumber=null, int? exactProjectId = null,
            string servmanId = null, string firstName = null, string lastName = null, string phoneNo = null,
            string city = null, string zip = null, string street =null, string block =null, 
            ProjectStatusEnum? status = null, ProjectTypeEnum? projectType = null, DateRange dateRange = null, int? projectManagerId = null, 
            int? customerId = null, bool isActiveAndRecent = false)
        {
            string sql=
                    @"select p.*, c.*, ca.*, sa.*, pcd.*, ep.*, ea.*, t.* from Project p
                    left join Customer c on p.CustomerId = c.ID
                    left join Address ca on c.AddressId = ca.ID
                    left join Address sa on p.ServiceAddressId = sa.ID
                    left join ProjectConstructionDetail pcd on pcd.ProjectId = p.ID
                    left join Employee ep on ep.ID = pcd.ProjectManagerEmployeeId
                    left join Employee ea on ea.ID = pcd.AccountManagerEmployeeId
                    left join Task t on t.ProjectId = p.ID
                where p.DumpedProjectId is null ";

            if (exactProjectId != null)
                sql += " and p.ID = ?ProjectId";
            else if (!string.IsNullOrEmpty(jobNumber))
            {
                sql += " and pcd.JobNumber = ?JobNumber";
            }
            else
            {
                if (servmanId != null && servmanId != string.Empty)
                    sql += @" and p.ID in (SELECT distinct p2.ID FROM Task t2
                        inner join Project p2 on p2.ID = t2.ProjectId
                        where t2.DumpedTaskId is null and t2.ServmanOrderNum = ?ServmanId)";
                if (firstName != null && firstName != string.Empty)
                    sql += " and c.FirstName like ?FirstName";
                if (lastName != null && lastName != string.Empty)
                    sql += " and c.LastName like ?LastName";
                if (phoneNo != null && phoneNo != string.Empty)
                    sql += " and (c.Phone1 = ?PhoneNumber or c.Phone2 = ?PhoneNumber)";
                if (city != null && city != string.Empty)
                    if (city == street)
                        sql += " and (sa.City like ?City or sa.Street like ?Street)";
                    else
                        sql += " and sa.City like ?City";
                if (zip != null && zip != string.Empty)
                    if (zip == block)
                        sql += " and (sa.Zip = ?Zip or sa.Block = ?Block)";
                    else
                        sql += " and sa.Zip = ?Zip";
                if (street != null && street != string.Empty && street != city)
                    sql += " and sa.Street like ?Street";
                if (block != null && block != string.Empty && block != zip)
                    sql += " and sa.Block = ?Block";
                if (status != null)
                    sql += " and p.ProjectStatusId = ?ProjectStatusId";
                if (projectType != null)
                    sql += " and p.ProjectTypeId = ?ProjectTypeId";
                if (customerId != null)
                    sql += " and c.ID = ?CustomerId";
                if (isActiveAndRecent)
                {
                    sql += " and p.ProjectTypeId in (4,5,6) and ";
                    sql += " (p.ProjectStatusId = 1 or datediff(now(), pcd.lastModifiedDate) < 30)";
                }
                if (dateRange != null)
                {
                    if (dateRange.StartDate.HasValue)
                        sql += " and p.CreateDate >= ?CreateDateStart";
                    if (dateRange.EndDate.HasValue)
                        sql += " and p.CreateDate <= ?CreateDateEnd";
                }
                if (projectManagerId != null)
                    sql += " and pcd.ProjectManagerEmployeeId = ?ProjectManagerEmployeeId";
            }

            sql += " order by p.ID";

            using (IDbCommand dbCommand = Database.PrepareCommand(sql))
            {
                if (exactProjectId != null)
                    Database.PutParameter(dbCommand, "?ProjectId", exactProjectId);
                else if (!string.IsNullOrEmpty(jobNumber))
                    Database.PutParameter(dbCommand, "?JobNumber", jobNumber);
                else
                {
                    if (servmanId != null && servmanId != string.Empty)
                        Database.PutParameter(dbCommand, "?ServmanId", servmanId);
                    if (firstName != null && firstName != string.Empty)
                        Database.PutParameter(dbCommand, "?FirstName", firstName + "%");
                    if (lastName != null && lastName != string.Empty)
                        Database.PutParameter(dbCommand, "?LastName", lastName + "%");
                    if (phoneNo != null && phoneNo != string.Empty)
                        Database.PutParameter(dbCommand, "?PhoneNumber", phoneNo);
                    if (city != null && city != string.Empty)
                        Database.PutParameter(dbCommand, "?City", city + "%");
                    if (zip != null && zip != string.Empty)
                        Database.PutParameter(dbCommand, "?Zip", zip);
                    if (street != null && street != string.Empty)
                        Database.PutParameter(dbCommand, "?Street", street + "%");
                    if (block != null && block != string.Empty)
                        Database.PutParameter(dbCommand, "?Block", block);
                    if (status != null)
                        Database.PutParameter(dbCommand, "?ProjectStatusId", (int)status);
                    if (projectType != null)
                        Database.PutParameter(dbCommand, "?ProjectTypeId", (int)projectType);
                    if (dateRange != null)
                    {
                        if (dateRange.StartDate.HasValue)
                            Database.PutParameter(dbCommand, "?CreateDateStart",
                                dateRange.StartDate.Value.Date);
                        if (dateRange.EndDate.HasValue)
                            Database.PutParameter(dbCommand, "?CreateDateEnd",
                                dateRange.EndDate.Value.Date.AddSeconds(86399));
                    }
                    if (projectManagerId != null)
                        Database.PutParameter(dbCommand, "?ProjectManagerEmployeeId", projectManagerId.Value);
                    if (customerId != null)
                        Database.PutParameter(dbCommand, "?CustomerId", customerId);
                }

                BindingList<ProjectWrapper> wrappers = new BindingList<ProjectWrapper>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (!dataReader.Read())
                        return wrappers;

                    while (true)
                    {
                        Project project = Load(dataReader);

                        Customer customerObj = null;
                        Address customerAddress = null;
                        Address serviceAddress = null;
                        ProjectConstructionDetail constructionDetail = null;
                        Employee projectManager = null;
                        Employee accountManager = null;

                        if (!dataReader.IsDBNull(FieldsCount))
                        {
                            customerObj = Customer.Load(dataReader, FieldsCount);
                            customerAddress = Address.Load(dataReader,
                                FieldsCount + Customer.FieldsCount);
                        }

                        if (!dataReader.IsDBNull(FieldsCount + Customer.FieldsCount + Address.FieldsCount))
                        {
                            serviceAddress = Address.Load(dataReader,
                                FieldsCount + Customer.FieldsCount + Address.FieldsCount);
                        }

                        if (!dataReader.IsDBNull(FieldsCount + Customer.FieldsCount + 2 * Address.FieldsCount))
                        {
                            constructionDetail = ProjectConstructionDetail.Load(dataReader,
                                FieldsCount + Customer.FieldsCount + 2 * Address.FieldsCount);
                        }

                        if (!dataReader.IsDBNull(FieldsCount + Customer.FieldsCount + 2 * Address.FieldsCount + ProjectConstructionDetail.FieldsCount))
                        {
                            projectManager = Employee.Load(dataReader,
                                FieldsCount + Customer.FieldsCount + 2 * Address.FieldsCount
                                        + ProjectConstructionDetail.FieldsCount);
                        }

                        if (!dataReader.IsDBNull(FieldsCount + Customer.FieldsCount + 2 * Address.FieldsCount + ProjectConstructionDetail.FieldsCount + Employee.FieldsCount))
                        {
                            accountManager = Employee.Load(dataReader,
                                FieldsCount + Customer.FieldsCount + 2 * Address.FieldsCount
                                        + ProjectConstructionDetail.FieldsCount + Employee.FieldsCount);
                        }

                        int taskOffset = FieldsCount + Customer.FieldsCount + 2 * Address.FieldsCount
                                        + ProjectConstructionDetail.FieldsCount + 2 * Employee.FieldsCount;

                        BindingList<Task> tasks = new BindingList<Task>();

                        if (!dataReader.IsDBNull(taskOffset))
                        {
                            Task task = Task.Load(dataReader, taskOffset);
                            tasks.Add(task);
                        }

                        bool isNextRowExists = dataReader.Read();
                        while (isNextRowExists && dataReader.GetInt32(0) == project.ID)
                        {
                            Task task = Task.Load(dataReader, taskOffset);
                            tasks.Add(task);
                            isNextRowExists = dataReader.Read();
                        }

                        wrappers.Add(new ProjectWrapper(project, customerObj, customerAddress,
                            serviceAddress, constructionDetail, projectManager, accountManager, tasks));

                        if (!isNextRowExists)
                            break;
                    }

                }

                return wrappers;
            }
        }

        #endregion

        #region Dump

        public static Project Dump(Project project, int? workTransactionId)
        {
            Project projectToInsert = FindByPrimaryKey(project.ID);
            projectToInsert.CustomerId = null;
            projectToInsert.ServiceAddressId = null;
            projectToInsert.DumpedProjectId = project.ID;
            projectToInsert.DumpWorkTransactionId = workTransactionId;
            Insert(projectToInsert);
            return projectToInsert;
        }

        #endregion

        #region InsertAndLog

        public static void InsertAndLog (Project project)
        {
            InsertAndLog(project, null);    
        }
        
        public static void InsertAndLog (Project project, IDbConnection connection)
        {
            Insert(project, connection);
            ProjectLog.Insert(project, connection);
        }

        #endregion

        #region UpdateAndLog

        public static void UpdateAndLog(Project project)
        {
            UpdateAndLog(project, null);
        }

        public static void UpdateAndLog(Project project, IDbConnection connection)
        {
            Update(project, connection);
            ProjectLog.Insert(project, connection);
        }

        #endregion

        #region Restore

        public static void Restore(Project dumpedProject)
        {
            Project projectToUpdate = (Project) dumpedProject.Clone();
            Project originalProject = FindByPrimaryKey(dumpedProject.DumpedProjectId.Value);

            projectToUpdate.ID = originalProject.ID;
            projectToUpdate.CustomerId = originalProject.CustomerId;
            projectToUpdate.ServiceAddressId = originalProject.ServiceAddressId;
            projectToUpdate.DumpedProjectId = null;
            projectToUpdate.DumpWorkTransactionId = null;
            Update(projectToUpdate);
        }

        #endregion

        #region FindDumpedProject

        private const string SqlFindDumpedProject =
            @"SELECT *
            FROM Project
                WHERE DumpedProjectId = ?DumpedProjectId
                    and DumpWorkTransactionId = ?DumpWorkTransactionId";

        public static Project FindDumpedProject(Project project, int workTransactionId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindDumpedProject))
            {
                Database.PutParameter(dbCommand, "?DumpedProjectId", project.ID);
                Database.PutParameter(dbCommand, "?DumpWorkTransactionId", workTransactionId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("dumped project not found");
        }

        #endregion

        #region FindBy WorkTransaction and Operation

        private const string SqlFindByWorkTransactionAndOperation =
            @"select p.* from WorkTransactionProject wtp
                inner join Project p on p.ID = wtp.ProjectId
                WHERE WorkTransactionId = ?WorkTransactionId            
                    and IsModified = ?IsModified
                    and IsCreated = ?IsCreated";

        public static List<Project> FindBy(WorkTransaction transaction,
            bool isModified, bool isCreated)
        {
            List<Project> result = new List<Project>();

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

        #region UpdateStatus

        private const string SqlUpdateStatus =
            @"update Project p
                set p.ProjectStatusId =
                  IF ((select count(*) from Task t where
                    t.ProjectId = ?ProjectId and
                    t.TaskStatusId != 2 and
                    (t.TaskFailTypeId is null or (t.TaskFailTypeId is not null and t.TaskFailTypeId = 1))
                    ) > 0, 1, 2)
                where p.ID = ?ProjectId";

        public static void UpdateStatus(Project project)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlUpdateStatus))
            {
                Database.PutParameter(dbCommand, "?ProjectId", project.ID);
                dbCommand.ExecuteNonQuery();
            }

            Project project1 = FindByPrimaryKey(project.ID);
            ProjectLog.Insert(project1, null);
        }

        #endregion

        #region GetCompletionDate

        private const string SqlGetCompletionDate =
            @"select max(wt.transactiondate)
                from project p
                join task t on t.projectid = p.id
                join worktransactiontask wtt on wtt.taskid = t.id
                join worktransaction wt on wt.id = wtt.worktransactionid
                where p.projectstatusid = 2 and
                wtt.worktransactionTaskActionid = 1
                and t.taskstatusid = 2 and t.taskFailTypeid is null
                and p.id  = ?ProjectId";

        public static DateTime GetCompletionDate(Project project)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlGetCompletionDate))
            {
                Database.PutParameter(dbCommand, "?ProjectId", project.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        if (!dataReader.IsDBNull(0))
                            return dataReader.GetDateTime(0);
                    }
                }
            }
            return DateTime.MinValue;
        }
        #endregion 
    }

    public class ProjectWrapper : ICloneable
    {
        private Customer m_customer;
        private Address m_customerAddress;
        private Address m_serviceAddress;
        private BindingList<Task> m_tasks;
        private ProjectConstructionDetail m_constructionDetail;
        private Employee m_projectManager;
        private Employee m_accountManager;

        #region Constructor

        public ProjectWrapper() {}

        public ProjectWrapper(Project project, Customer customer, Address customerAddress,
                              Address serviceAddress, ProjectConstructionDetail constructionDetail,
                              Employee projectManager, Employee accountManager,
                              BindingList<Task> tasks)
        {
            m_project = project;
            m_customer = customer;
            m_customerAddress = customerAddress;
            m_serviceAddress = serviceAddress;
            m_constructionDetail = constructionDetail;
            m_projectManager = projectManager;
            m_accountManager = accountManager;
            m_tasks = tasks;
        }

        public ProjectWrapper (CustomerProjectWrapper customerProjectWrapper)
        {
            if (customerProjectWrapper.IsCustomer)
                throw new ArgumentException("Customer Project Wrapper missing a project");

            if (customerProjectWrapper.Project.ProjectType != ProjectTypeEnum.BasementSystems && 
                customerProjectWrapper.Project.ProjectType != ProjectTypeEnum.Construction && 
                customerProjectWrapper.Project.ProjectType != ProjectTypeEnum.Content)
                throw new ArgumentException("Unsupported project Type");

            m_project = customerProjectWrapper.Project;
            m_customer = customerProjectWrapper.Customer;
            m_customerAddress = Address.FindByPrimaryKey(m_customer.AddressId.Value);
            m_constructionDetail = ProjectConstructionDetail.FindByPrimaryKey(m_project.ID);
            m_tasks = new BindingList<Task>(Task.FindByProject(m_project));
            
            if (m_project.ServiceAddressId.HasValue)
                m_serviceAddress = Address.FindByPrimaryKey(m_project.ServiceAddressId.Value);

            if (m_constructionDetail.ProjectManagerEmployeeId.HasValue)
                m_projectManager = Employee.FindByPrimaryKey(m_constructionDetail.ProjectManagerEmployeeId.Value);

            if (m_constructionDetail.AccountManagerEmployeeId.HasValue)
                m_projectManager = Employee.FindByPrimaryKey(m_constructionDetail.AccountManagerEmployeeId.Value);
        }

        #endregion

        #region JobNumber

        public string JobNumber
        {
            get
            {
                if (m_constructionDetail != null && !string.IsNullOrEmpty(m_constructionDetail.JobNumber))
                    return m_constructionDetail.JobNumber;
                else
                    return m_project.ID.ToString();
            }
        }

        #endregion

        #region Project

        private Project m_project;
        public Project Project
        {
            get { return m_project; }
            set { m_project = value; }
        }

        #endregion

        #region ProjectInsurance

        public ProjectInsurance ProjectInsurance { get; set; }

        #endregion

        #region Customer

        public Customer Customer
        {
            get { return m_customer; }
            set { m_customer = value; }
        }

        #endregion

        #region CustomerAddress

        public Address CustomerAddress
        {
            get { return m_customerAddress; }
            set { m_customerAddress = value; }
        }

        #endregion

        #region ServiceAddress

        public Address ServiceAddress
        {
            get { return m_serviceAddress; }
            set { m_serviceAddress = value; }
        }

        #endregion

        #region ConstructionDetail

        public ProjectConstructionDetail ConstructionDetail
        {
            get { return m_constructionDetail; }
            set { m_constructionDetail = value; }
        }

        #endregion

        #region ProjectManager

        public Employee ProjectManager
        {
            get { return m_projectManager; }
            set { m_projectManager = value; }
        }

        #endregion

        #region AccountManager

        public Employee AccountManager
        {
            get { return m_accountManager; }
            set { m_accountManager = value; }
        }

        #endregion

        #region Tasks

        public BindingList<Task> Tasks
        {
            get { return m_tasks; }
            set { m_tasks = value; }
        }

        #endregion

        #region ID

        public int ID
        {
            get { return m_project.ID; }
        }

        #endregion

        #region ServmanId

        private string m_servmanId;
        public string ServmanId
        {
            get
            {
                if (m_servmanId == null || m_servmanId == string.Empty)
                {
                    foreach (Task task in m_tasks)
                    {
                        if (task.ServmanOrderNum != null && task.ServmanOrderNum != string.Empty)
                            m_servmanId = task.ServmanOrderNum;
                    }
                }
                return m_servmanId;
            }
        }

        #endregion

        #region ProjectType

        public ProjectTypeEnum ProjectType
        {
            get { return m_project.ProjectType; }
        }

        #endregion

        #region ProjectTypeText

        public string ProjectTypeText
        {
            get { return m_project.ProjectTypeText; }
        }

        #endregion        

        #region ProjectStatusText

        public string ProjectStatusText
        {
            get { return m_project.ProjectStatusText; }
        }

        #endregion

        #region Progress

        private string m_progress;

        public string Progress
        {
            get
            {
                if (m_progress == null || m_progress == string.Empty)
                {
                    if (m_project.ProjectType == ProjectTypeEnum.RugCleaning)
                    {
                        Task rugDelivery = GetTask(TaskTypeEnum.RugDelivery);
                        if (rugDelivery != null)
                        {
                            if (rugDelivery.TaskStatus == TaskStatusEnum.Completed)
                            {
                                m_progress = "Delivery Done";
                                return m_progress;
                            }

                            if (rugDelivery.IsReady)
                            {
                                m_progress = "Delivery Ready";
                                return m_progress;
                            }

                            m_progress = "Rugs Picked Up";
                            return m_progress;
                                
                        } else
                        {
                            m_progress = "Not Started";
                            return m_progress;
                        }
                    } else if (m_project.ProjectType == ProjectTypeEnum.Deflood)
                    {
                        Task deflood = GetTask(TaskTypeEnum.Deflood);

                        if (deflood.TaskStatus == TaskStatusEnum.NotCompleted)
                        {
                            m_progress = "Not Started";
                            return m_progress;                            
                        }

                        if (deflood.TaskStatus == TaskStatusEnum.InProcess)
                        {
                            m_progress = "Monitoring";
                            return m_progress;                            
                        }

                        if (deflood.TaskStatus == TaskStatusEnum.Completed)
                        {
                            m_progress = "Deflood Done";
                            return m_progress;                            
                        }
                    }
                    else if (m_project.ProjectType == ProjectTypeEnum.Miscellaneous)
                    {
                        bool isAllDone = true;
                        bool isAtLeastOneDone = false;

                        foreach (Task task in m_tasks)
                        {
                            if (task.TaskStatus == TaskStatusEnum.Completed)
                                isAtLeastOneDone = true;
                            else
                                isAllDone = false;
                        }

                        if (isAllDone)
                            m_progress = "Done";
                        else if (isAtLeastOneDone)
                            m_progress = "In Progress";
                        else
                            m_progress = "Not Started";

                        return m_progress;
                    }
                    else if (m_project.ProjectType == ProjectTypeEnum.Construction ||
                                m_project.ProjectType == ProjectTypeEnum.Content ||
                                m_project.ProjectType == ProjectTypeEnum.BasementSystems
                                )
                    {
                        return ProjectConstructionProgress.GetText(m_constructionDetail.ProjectConstructionProgress);
                    }                    
                }

                return m_progress;
            }
        }

        private Task GetTask(TaskTypeEnum taskType)
        {
            foreach (Task task in m_tasks)
            {
                if (task.TaskType == taskType)
                    return task;
            }

            return null;
        }

        #endregion

        #region CustomerName
        
        public string CustomerName
        {
            get
            {
                if (m_customer == null)
                    return "[No Customer]";
                return m_customer.DisplayName;
            }
        }

        #endregion

        #region ProjectManagerName

        public string ProjectManagerName
        {
            get
            {
                if (m_projectManager != null)
                    return m_projectManager.DisplayName;
                return string.Empty;
            }
        }

        #endregion

        #region AccountManagerName

        public string AccountManagerName
        {
            get
            {
                if (m_accountManager != null)
                    return m_accountManager.DisplayName;
                return string.Empty;
            }
        }

        #endregion

        #region Mapsco
        
        public string Mapsco
        {
            get
            {
                if (m_serviceAddress == null)
                    return string.Empty;
                return m_serviceAddress.Map;
            }
        }

        #endregion

        #region ClosedAmount

        public decimal ClosedAmount
        {
            get { return m_project.ClosedAmount; }
        }

        #endregion

        #region AmountCollected

        private decimal m_amountCollected;

        public decimal AmountCollected
        {
            get { return m_amountCollected; }
        }

        #endregion

        #region DateCreated

        public DateTime DateCreated
        {
            get
            {
                return m_project.CreateDate;
            }
        }

        #endregion        

        #region Description
        
        public string Description
        {
            get { return m_project.Description; }
        }

        #endregion

        #region IsConstructionJobTicket

        public bool IsConstructionJobTicket
        {
            get
            {
                if (m_constructionDetail == null)
                    return false;

                return m_constructionDetail.SignUpDate.HasValue                       
                       || m_constructionDetail.LastBillingDate.HasValue
                       || m_constructionDetail.JobCost != decimal.Zero
                       || m_constructionDetail.LastPaymentDate.HasValue
                       || m_project.ClosedAmount != decimal.Zero
                       || m_project.PaidAmount != decimal.Zero;
            }
        }

        #endregion

        #region IsConstructionLeadTicket

        public bool IsConstructionLeadTicket
        {
            get
            {
                if (m_constructionDetail == null)
                    return false;

                return !IsConstructionJobTicket;
            }
        }

        #endregion

        #region DateCreated

        public DateTime LastModifiedDate
        {
            get { return ConstructionDetail.LastModifiedDate; }
        }

        #endregion

        #region Clone

        public object Clone()
        {
            BindingList<Task> tasks = new BindingList<Task>();
            foreach (Task task in m_tasks)
            {
                tasks.Add((Task)task.Clone());
            }

            Project project = m_project != null? (Project)m_project.Clone():null;
            Customer customer = m_customer != null ? (Customer)m_customer.Clone() : null;
            Address customerAddress = m_customerAddress != null ? (Address)m_customerAddress.Clone() : null;
            ProjectConstructionDetail constructionDetail = m_constructionDetail != null ? (ProjectConstructionDetail)m_constructionDetail.Clone() : null;
            Employee projectManager = m_projectManager != null ? (Employee)m_projectManager.Clone() : null;
            Employee accountManager = m_accountManager != null ? (Employee)m_accountManager.Clone() : null; 

            return new ProjectWrapper(project, customer,
                customerAddress, (Address)m_serviceAddress.Clone(),
                constructionDetail,projectManager,
                accountManager, tasks);
        }

        #endregion

        #region IsNeedPrint

        public bool IsNeedPrint(ProjectWrapper originalProjectWrapper)
        {
            if (!Configuration.AutomatedVisitPrint)
                return false;

            if (originalProjectWrapper == null)
                return true;

            if(m_project.ProjectType != ProjectTypeEnum.Construction &&
                m_project.ProjectType != ProjectTypeEnum.Content &&
                m_project.ProjectType != ProjectTypeEnum.BasementSystems)
                return false;

            if (originalProjectWrapper.Progress!= Progress)
            {
                if(m_constructionDetail.ProjectConstructionProgress == ProjectConstructionProgressEnum.Job ||
                    m_constructionDetail.ProjectConstructionProgress == ProjectConstructionProgressEnum.Lead)
                return true;
            }

            return false;
        }

        #endregion
    }

    public class CustomerProjectWrapper
    {
        private Customer m_customer;
        private Project m_project;
        private ProjectConstructionDetail m_projectConstructionDetail;
        private bool m_isCustomer;
        private decimal? m_customerBalance;
        private decimal? m_projectBalance;
        private Address m_customerAddress;
        private Address m_serviceAddress;

        #region CustomerProjectWrapper

        public CustomerProjectWrapper(Customer customer, Project project, ProjectConstructionDetail projectConstructionDetail, bool isCustomer, 
            decimal? customerBalance, decimal? projectBalance, Address customerAddress, Address serviceAddress
            )
        {
            m_customer = customer;
            m_project = project;
            m_isCustomer = isCustomer;
            m_customerBalance = customerBalance;
            m_projectBalance = projectBalance;
            m_projectConstructionDetail = projectConstructionDetail;
            m_customerAddress = customerAddress;
            m_serviceAddress = serviceAddress;
        }

        #endregion

        #region IsCustomer

        public bool IsCustomer
        {
            get { return m_isCustomer; }
        }

        #endregion

        #region Customer

        public Customer Customer
        {
            get { return m_customer; }
        }

        #endregion

        #region Customer Address 

        public Address CustomerAddress
        {
            get { return m_customerAddress; }
        }

        #endregion 

        #region Project

        public Project Project
        {
            get { return m_project; }
        }

        #endregion

        #region ServiceAddress

        public Address ServiceAddress
        {
            get { return m_serviceAddress; }
        }

        #endregion

        #region ProjectConstructionDetail

        public ProjectConstructionDetail ProjectConstructionDetail
        {
            get { return m_projectConstructionDetail;}
        }

        #endregion

        #region ProjectId

        public int ProjectId
        {
            get { return m_project.ID; }
        }

        #endregion

        #region Caption

        public string Caption
        {
            get
            {
                if (IsCustomer)
                    return m_customer.DisplayName;
                if (ProjectConstructionDetail != null && !string.IsNullOrEmpty(ProjectConstructionDetail.JobNumber))
                {
                    var jobNumber = ProjectConstructionDetail.JobNumber.Trim();
                    if (!string.IsNullOrEmpty(jobNumber))
                        return "      " + jobNumber;
                }
                return "      " + m_project.ID;
            }
        }

        #endregion

        #region CustomerBalance

        public decimal? CustomerBalance
        {
            get { return m_customerBalance; }
        }

        #endregion

        #region ProjectBalance

        public decimal? ProjectBalance
        {
            get { return m_projectBalance; }
        }

        #endregion

        #region Balance

        public decimal? Balance
        {
            get { return IsCustomer? m_customerBalance:m_projectBalance; }
        }

        #endregion

        #region CustomerName

        public string CustomerName
        {
            get { return m_customer.DisplayName; }
        }

        #endregion

        #region Find
       
        private const string SqlFindParameters =
            @"
            select c.*, p.*, pcd.*, qc.*, qp.*, ca.*, pa.*
            from customer c
            join address ca on c.addressid = ca.id
            {0} join Project p on p.CustomerId = c.ID and p.dumpedProjectId is null
            {1} join address pa on p.serviceaddressid =  pa.id
            left join ProjectConstructionDetail pcd on pcd.ProjectId = p.ID
            left join QbCustomer qc on qc.customerid = c.id and  qc.sublevel=0
            left join QbCustomer qp on qp.projectid = p.id and qp.sublevel=1
            where 1=1 {2}
            order by c.id";

        public static List<CustomerProjectWrapper> Find(
            string jobNumber = null, int? projectId = null, string customerLastName = null, string customerFirstName = null, 
            DateRange dateRange = null,  string qbSalesRepListId = null, string qbCustomerType = null, ProjectTypeEnum? projectType = null,
            string block = null, string street = null, string city = null, string zip = null, string phoneNumber=null, 
            ProjectStatusEnum? projectStatus = null, int? projectManagerId = null)
        {
            var result = new List<CustomerProjectWrapper>();

            if ((!projectStatus.HasValue || projectStatus.Value != ProjectStatusEnum.Open) && string.IsNullOrEmpty(jobNumber) && projectId == null && string.IsNullOrEmpty(customerLastName) 
                && string.IsNullOrEmpty(street) && string.IsNullOrEmpty(phoneNumber))
                return result;

            var additionalQuery = string.Empty;

            if (jobNumber != null)
            {
                additionalQuery += " and c.id in (select customerid from project join projectConstructionDetail on projectConstructionDetail.projectid = project.id where projectConstructionDetail.jobnumber = ?JobNumber)";
            }
            else if (projectId.HasValue)
                additionalQuery += " and c.id in (select customerid from project where id = ?ProjectId )";
            else if (!string.IsNullOrEmpty(customerLastName))
            {
                    additionalQuery += " and c.LastName like ?LastName";

                    if (!string.IsNullOrEmpty(customerFirstName))
                        additionalQuery += " and c.FirstName like ?FirstName";

                    if (!string.IsNullOrEmpty(street))
                    {
                        additionalQuery +=
                             " and (ca.street like ?Street";
                        if (!string.IsNullOrEmpty(city))
                            additionalQuery +=
                            " and ca.city like ?City";
                        if (!string.IsNullOrEmpty(block))
                            additionalQuery +=
                            " and ca.block = ?Block";
                        if (!string.IsNullOrEmpty(zip))
                            additionalQuery +=
                            " and ca.zip = ?Zip";
                        additionalQuery += ")";
                    }

                    if (!string.IsNullOrEmpty(phoneNumber))
                        additionalQuery += " and (c.phone1 = ?PhoneNumber1 || c.phone2 = ?PhoneNumber2)";

                    if (projectStatus.HasValue)
                        additionalQuery += " and p.projectStatusId = ?ProjectStatusId";
                    
            }
            else if (!string.IsNullOrEmpty(street))
            {
                    additionalQuery +=
                         " and (ca.street like ?Street";
                    if (!string.IsNullOrEmpty(city))
                        additionalQuery +=
                        " and ca.city like ?City";
                    if (!string.IsNullOrEmpty(block))
                        additionalQuery +=
                        " and ca.block = ?Block";
                    if (!string.IsNullOrEmpty(zip))
                        additionalQuery +=
                        " and ca.zip = ?Zip";
                    additionalQuery += ")";

                    if (!string.IsNullOrEmpty(phoneNumber))
                    {
                        additionalQuery += " and (c.phone1 = ?PhoneNumber1 || c.phone2 = ?PhoneNumber2)";
                    }
             }
            else if (!string.IsNullOrEmpty(phoneNumber))
            {
                additionalQuery += " and (c.phone1 = ?PhoneNumber1 || c.phone2 = ?PhoneNumber2)";
            }
            else
            {
                if (projectStatus.HasValue)
                    additionalQuery += " and p.projectStatusId = ?ProjectStatusId";

                if (projectManagerId.HasValue)
                {
                    additionalQuery += " and pcd.ProjectManagerEmployeeId = ?ProjectManagerEmployeeId";
                }

                if (dateRange != null && dateRange.StartDate.HasValue)
                {
                    additionalQuery +=
                        " and date(p.createdate) >= ?StartDate and date(p.createdate) <= ?EndDate";
                }

                if (!string.IsNullOrEmpty(qbSalesRepListId))
                {
                    additionalQuery +=
                        " and p.QbSalesRepListId = ?SalesRepListId";
                }

                if (!string.IsNullOrEmpty(qbCustomerType))
                {
                    additionalQuery +=
                        " and p.QbCustomerTypeListId = ?QbCustomerTypeListId";
                }

                if (projectType != null)
                {
                    additionalQuery +=
                        " and p.projectTypeId = ?ProjectTypeId";
                }
            }

            var sql = string.IsNullOrEmpty(customerLastName)?string.Format(SqlFindParameters, string.Empty,string.Empty,additionalQuery):
                        string.Format(SqlFindParameters, "left", "left", additionalQuery);

            using (IDbCommand dbCommand = Database.PrepareCommand(sql))
            {
                if (jobNumber != null)
                    Database.PutParameter(dbCommand, "?JobNumber", jobNumber);
                else if (projectId.HasValue)
                    Database.PutParameter(dbCommand, "?ProjectId", projectId.Value);
                else if (!string.IsNullOrEmpty(customerLastName))
                {
                    if (projectStatus.HasValue)
                        Database.PutParameter(dbCommand, "?ProjectStatusId", (int)projectStatus.Value);
                    
                    if (!string.IsNullOrEmpty(phoneNumber))
                    {
                        Database.PutParameter(dbCommand, "?PhoneNumber1", phoneNumber);
                        Database.PutParameter(dbCommand, "?PhoneNumber2", phoneNumber);
                    }

                    if (!customerLastName.EndsWith("%"))
                        customerLastName += "%";

                    Database.PutParameter(dbCommand, "?LastName", customerLastName);

                    if (!string.IsNullOrEmpty(customerFirstName))
                    {
                        if (!customerFirstName.EndsWith("%"))
                            customerFirstName += "%";

                        Database.PutParameter(dbCommand, "?FirstName", customerFirstName);
                    }

                    if (!string.IsNullOrEmpty(street))
                    {
                        if (!street.EndsWith("%"))
                            street += "%";
                        Database.PutParameter(dbCommand, "?Street", street);
                    }

                    if (!string.IsNullOrEmpty(city))
                    {
                        if (!city.EndsWith("%"))
                            city += "%";
                        Database.PutParameter(dbCommand, "?City", city);
                    }

                    if (!string.IsNullOrEmpty(block))
                    {
                        Database.PutParameter(dbCommand, "?Block", block);
                    }

                    if (!string.IsNullOrEmpty(zip))
                    {
                        Database.PutParameter(dbCommand, "?Zip", zip);
                    }
                }
                else if (!string.IsNullOrEmpty(street))
                {
                    if (!string.IsNullOrEmpty(phoneNumber))
                    {
                        Database.PutParameter(dbCommand, "?PhoneNumber1", phoneNumber);
                        Database.PutParameter(dbCommand, "?PhoneNumber2", phoneNumber);
                    }

                    if (!street.EndsWith("%"))
                        street += "%";
                    Database.PutParameter(dbCommand, "?Street", street);

                    if (!string.IsNullOrEmpty(city))
                    {
                        if (!city.EndsWith("%"))
                            city += "%";
                        Database.PutParameter(dbCommand, "?City", city);
                    }

                    if (!string.IsNullOrEmpty(block))
                    {
                        Database.PutParameter(dbCommand, "?Block", block);
                    }

                    if (!string.IsNullOrEmpty(zip))
                    {
                        Database.PutParameter(dbCommand, "?Zip", zip);
                    }
                }
                else if (!string.IsNullOrEmpty(phoneNumber))
                {
                    Database.PutParameter(dbCommand, "?PhoneNumber1", phoneNumber);
                    Database.PutParameter(dbCommand, "?PhoneNumber2", phoneNumber);
                }
                else
                {
                    if (projectManagerId.HasValue)
                        Database.PutParameter(dbCommand, "ProjectManagerEmployeeId", projectManagerId.Value);

                    if (projectStatus.HasValue)
                        Database.PutParameter(dbCommand, "?ProjectStatusId", (int)projectStatus.Value);

                    if (dateRange != null)
                    {
                        Database.PutParameter(dbCommand, "?StartDate", dateRange.StartDate);
                        Database.PutParameter(dbCommand, "?EndDate", dateRange.EndDate);
                    }

                    if (!string.IsNullOrEmpty(qbSalesRepListId))
                        Database.PutParameter(dbCommand, "?SalesRepListId", qbSalesRepListId);

                    if (!string.IsNullOrEmpty(qbCustomerType))
                        Database.PutParameter(dbCommand, "?QbCustomerTypeListId", qbCustomerType);

                    if (projectType != null)
                        Database.PutParameter(dbCommand, "?ProjectTypeId", (int)projectType);
                }
               
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        int offset = 0;

                        QbCustomer qbCustomer = null;
                        Project project = null;
                        ProjectConstructionDetail projectConstructionDetail = null;
                        QbCustomer qbProject = null;
                        Address customerAddress = null;
                        Address serviceAddress = null;
                        
                        Customer customer = Customer.Load(dataReader);
                        offset += Customer.FieldsCount;

                        if (!dataReader.IsDBNull(offset))
                            project = Project.Load(dataReader, offset);
                        offset += Project.FieldsCount;

                        if (!dataReader.IsDBNull(offset))
                            projectConstructionDetail = ProjectConstructionDetail.Load(dataReader, offset);
                        offset += ProjectConstructionDetail.FieldsCount;

                        if (!dataReader.IsDBNull(offset))
                            qbCustomer = QbCustomer.Load(dataReader, offset);
                        offset += QbCustomer.FieldsCount;

                        if (!dataReader.IsDBNull(offset))
                            qbProject = QbCustomer.Load(dataReader, offset);
                        offset += QbCustomer.FieldsCount;
                        
                        if (!dataReader.IsDBNull(offset))
                            customerAddress = Address.Load(dataReader, offset);
                        offset += Address.FieldsCount;

                        if (!dataReader.IsDBNull(offset))
                            serviceAddress = Address.Load(dataReader, offset);

                        decimal? customerBalance = null;
                        if (qbCustomer != null)
                            customerBalance = qbCustomer.Balance;

                        decimal? projectBalance = null;
                        if (qbProject != null)
                            projectBalance = qbProject.Balance;

                        if (result.Count == 0 || result[result.Count - 1].Customer.ID != customer.ID)
                            result.Add(new CustomerProjectWrapper(customer, null, null, true, customerBalance, null, customerAddress, null));

                        if (project != null)
                        {
                            CustomerProjectWrapper projectWrapper = new CustomerProjectWrapper(customer, project, projectConstructionDetail, 
                                false, customerBalance, projectBalance, customerAddress, serviceAddress);
                            result.Add(projectWrapper);
                        }
                    }
                }
            }

            // customers with latest projects show up first
            List<CustomerProjectWrapper> sortedResult = new List<CustomerProjectWrapper>();
            List<CustomerProjectWrapper> customers = result.FindAll(delegate(CustomerProjectWrapper temp)
                                                                     { return temp.IsCustomer; });
            Dictionary<int, List<CustomerProjectWrapper>> projectMap = new Dictionary<int, List<CustomerProjectWrapper>>();
            Dictionary<int, DateTime> maxDateMap = new Dictionary<int, DateTime>();
            
            foreach (CustomerProjectWrapper customer in customers)
            {
                
                List<CustomerProjectWrapper> projects = result.FindAll(delegate(CustomerProjectWrapper temp)
                                                                     { return !temp.IsCustomer && temp.Customer.ID == customer.Customer.ID; });

                projects.Sort(delegate(CustomerProjectWrapper temp1, CustomerProjectWrapper temp2)
                                  {
                                      if (temp1.ProjectId > temp2.ProjectId)
                                          return -1;
                                      if (temp1.ProjectId == temp2.ProjectId)
                                          return 0;
                                      return 1;
                                  });
                projectMap.Add(customer.Customer.ID, projects);

                DateTime maxDate = DateTime.MinValue;
                foreach(CustomerProjectWrapper project in projects)
                {
                    if (project.Project.CreateDate > maxDate)
                        maxDate = project.Project.CreateDate;
                }
                maxDateMap.Add(customer.Customer.ID, maxDate);
            }

            customers.Sort(delegate(CustomerProjectWrapper temp1, CustomerProjectWrapper temp2)
            {
                var compareResult = temp1.Customer.LastName.CompareTo(temp2.Customer.LastName);

                if (compareResult == 0 && temp1.Customer.FirstName != null && temp2.Customer.FirstName != null)
                    compareResult = temp1.Customer.FirstName.CompareTo(temp2.Customer.FirstName);

                return compareResult;
            });

            foreach (CustomerProjectWrapper customer in customers)
            {
                sortedResult.Add(customer);
                sortedResult.AddRange(projectMap[customer.Customer.ID]);
            }

            return sortedResult;
        }

        #endregion
    }
}
      