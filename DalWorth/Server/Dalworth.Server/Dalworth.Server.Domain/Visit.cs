using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Xml.Serialization;
using Dalworth.Server.Data;
using Dalworth.Server.Domain.package;
using Configuration=Dalworth.Server.SDK.Configuration;
using Database = Dalworth.Server.Data.Database;

namespace Dalworth.Server.Domain
{
    public enum VisitUndoOperationEnum
    {
        Unavailable,
        Confirm,
        Dispatch,
        Arrive,
        Complete
    }

    public enum DispatchNotificationReasonEnum
    {
        OutOfTimeFrame,
        LeftMessage,
        Busy,
        CallOnTheWay
    }

    public partial class Visit
    {
        public Visit(){ }

        public void Init ()
        {
            m_salesRep = string.Empty;
            m_advertisingSource = string.Empty;

            List<Project> projects = Project.FindByVisitId(ID, null);
            
            m_advertisingSource = string.Empty;

            foreach (Project project in projects)
            {
                QbSalesRep salesRep = null;
                Employee employee  = null;

                if (!string.IsNullOrEmpty(project.QbSalesRepListId))
                   salesRep = QbSalesRep.FindByPrimaryKey(project.QbSalesRepListId);
                    
                if (project.AdvertisingTechnicianId.HasValue)
                    employee = Employee.FindByPrimaryKey(project.AdvertisingTechnicianId.Value);

                if (employee != null)
                    m_salesRep += (string.IsNullOrEmpty(m_advertisingSource) ? string.Empty : ":") + employee.DisplayName;
                else if (salesRep != null)
                    m_salesRep += (string.IsNullOrEmpty(m_advertisingSource) ? string.Empty : ":")  + salesRep.LastName + ", " + salesRep.FirstName;

                if (!string.IsNullOrEmpty(project.QbCustomerTypeListId))
                {
                    QbCustomerType customerType = QbCustomerType.FindByPrimaryKey(project.QbCustomerTypeListId);
                    m_advertisingSource += (string.IsNullOrEmpty(m_advertisingSource) ? string.Empty : ":") + customerType.Name;
                }
            }  
        }

        #region SalesRep

        private string m_salesRep;
        public string SalesRep
        {
            get { return m_salesRep; }
        }

        #endregion

        #region AdvertisingSource

        private string m_advertisingSource;
        public string AdvertisingSource
        {
            get { return m_advertisingSource;}

        }

        #endregion

        #region VisitStatus

        [XmlIgnore]
        public VisitStatusEnum VisitStatus
        {
            get { return (VisitStatusEnum)m_visitStatusId; }
            set { m_visitStatusId = (int)value; }
        }

        #endregion

        #region VisitStatusText

        public string VisitStatusText
        {
            get
            {
                return Domain.VisitStatus.GetText(VisitStatus);
            }
        }

        #endregion

        #region TimeFrameText

        public string TimeFrameText
        {
            get
            {
                if (PreferedTimeFrom.HasValue && PreferedTimeTo.HasValue)
                {
                    if (PreferedTimeFrom.Value.Hour == 0 && PreferedTimeTo.Value.Hour == 12)
                        return "In AM";

                    if (PreferedTimeFrom.Value.Hour == 12 && PreferedTimeTo.Value.Hour == 23)
                        return "In PM";

                    return PreferedTimeFrom.Value.ToString("h tt") + " - " + PreferedTimeTo.Value.ToString("h tt");
                }                    
                else if (PreferedTimeFrom.HasValue)
                    return "After " + PreferedTimeFrom.Value.ToString("h tt");
                else if (PreferedTimeTo.HasValue)
                    return "Before " + PreferedTimeTo.Value.ToString("h tt");

                return string.Empty;
            }
        }

        #endregion

        #region IsEditAllowed

        public bool IsEditAllowed
        {
            get
            {
                return VisitStatus == VisitStatusEnum.Pending
                       || VisitStatus == VisitStatusEnum.Assigned;
            }
        }

        #endregion

        #region IsConfirmed

        public bool IsConfirmed
        {
            get { return ConfirmDateTime.HasValue; }
        }

        #endregion

        #region ConfirmedTimeFrameText

        public string ConfirmedTimeFrameText
        {
            get
            {
                return string.Format("{0} - {1}",
                     ConfirmedFrameBegin != null ? ConfirmedFrameBegin.Value.ToString("h tt") : "NA",
                     ConfirmedFrameEnd != null ? ConfirmedFrameEnd.Value.ToString("h tt") : "NA");
            }
        }

        #endregion

        #region ConfirmedTimeFrameShortText

        public string ConfirmedTimeFrameShortText
        {
            get
            {
                return GetTimeFrameShortText(ConfirmedFrameBegin, ConfirmedFrameEnd);
            }
        }

        public static string GetTimeFrameShortText(DateTime? start, DateTime? end)
        {
            return string.Format("{0} - {1}",
                 start != null ? start.Value.ToString("h tt").TrimEnd(" AMPM".ToCharArray()) : "NA",
                 end != null ? end.Value.ToString("h tt").TrimEnd(" AMPM".ToCharArray()) : "NA");
        }

        #endregion

        #region DurationText

        public string DurationText
        {
            get
            {
                if (m_durationMin == null)
                    return "0 minutes";
                if (m_durationMin == 1)
                    return "1 minute";                
                if (m_durationMin < 60)
                    return m_durationMin + " minutes";                
                if (m_durationMin == 60)
                    return "1 hour";
                if (m_durationMin < 720) // 12 hours
                    return decimal.Divide(m_durationMin.Value, 60).ToString("#.#") + " hours";
                if (m_durationMin == 1440) // 24 hours
                    return "1 day";
                return decimal.Divide(m_durationMin.Value, 1440).ToString("#.#") + " days";                    
            }
        }

        #endregion

        #region GetDuration

        public static int GetDuration(string durationText)
        {
            if (durationText.Contains("minute"))
                return (int)decimal.Parse(durationText.Split(new char[1] { ' ' })[0]);
            if (durationText.Contains("hour"))
                return (int)(decimal.Parse(durationText.Split(new char[1] { ' ' })[0]) * 60);
            if (durationText.Contains("day"))
                return (int)(decimal.Parse(durationText.Split(new char[1] { ' ' })[0]) * 1440);
            return 0;
        }

        #endregion

        #region IsServiceDateTodayOrTomorrow

        public bool IsServiceDateTodayOrTomorrow
        {
            get
            {
                if (!ServiceDate.HasValue)
                    return false;

                if (ServiceDate.Value.Date == DateTime.Now.Date
                    || ServiceDate.Value.Date == DateTime.Now.AddDays(1).Date)
                {
                    return true;
                }

                if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday
                    && ServiceDate.Value.Date == DateTime.Now.AddDays(2).Date)
                {
                    return true;
                }

                return false;
            }
        }

        #endregion

        #region IsRescheduleAllowed

        public bool IsRescheduleAllowed
        {
            get
            {
                if (VisitStatus == VisitStatusEnum.Arrived
                    || VisitStatus == VisitStatusEnum.Completed)
                {
                    return false;
                }

                return true;                
            }
        }

        #endregion

        #region FindBy VisitStatus

        public static List<Visit> FindBy(VisitStatusEnum visitStatus, PendingVisitTypeEnum visitType, 
            string customerName, IDbConnection connection)
        {
            string SqlFindByVisitStatus =
                @"SELECT v.* FROM Visit v
                    left join Customer c on c.ID = v.CustomerId
                    WHERE VisitStatusId = ?VisitStatusId ";

            if (customerName.Trim() != string.Empty)
                SqlFindByVisitStatus += "AND c.LastName like ?CustomerLastName";
            else if (visitType == PendingVisitTypeEnum.Current)
                SqlFindByVisitStatus += "AND ServiceDate is not null AND DATE(ServiceDate) <= DATE(CURDATE())";
            else if (visitType == PendingVisitTypeEnum.Tomorrow)
                SqlFindByVisitStatus += "AND ServiceDate is not null AND DATE(ServiceDate) = DATE(DATE_ADD(CURDATE(), INTERVAL 1 DAY))";
            else if (visitType == PendingVisitTypeEnum.Future)
                SqlFindByVisitStatus += "AND ServiceDate is not null AND DATE(ServiceDate) > DATE(DATE_ADD(CURDATE(), INTERVAL 1 DAY))";
            else if (visitType == PendingVisitTypeEnum.Unscheduled)
                SqlFindByVisitStatus += "AND ServiceDate is null";

            List<Visit> visits = new List<Visit>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByVisitStatus, connection))
            {                
                Database.PutParameter(dbCommand, "?VisitStatusId", (int)visitStatus);
                if (customerName.Trim() != string.Empty)
                    Database.PutParameter(dbCommand, "?CustomerLastName", "%" + customerName.Trim() + "%");

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        visits.Add(Load(dataReader));
                    }
                }
            }
            return visits;
        }

        public static List<Visit> FindBy(VisitStatusEnum visitStatus, PendingVisitTypeEnum visitType, string customerName)
        {
            return FindBy(visitStatus, visitType, customerName, null);
        }

        #endregion                        

        #region FindBy Work

        private const string SqlFindByWork =
            @"select * from Visit
                where Id in (select VisitId from WorkDetail where WorkId = ?WorkId)";

        public static List<Visit> FindBy(Work work, IDbConnection connection)
        {
            List<Visit> visits = new List<Visit>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWork, connection))
            {
                Database.PutParameter(dbCommand, "?WorkId", work.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        visits.Add(Load(dataReader));
                    }
                }
            }
            return visits;
        }

        public static List<Visit> FindBy(Work work)
        {
            return FindBy(work, null);
        }

        #endregion

        #region FindLatestVisit by Task

        private const string SqlFindLatestVisit =
            @"select * from Visit v
                inner join VisitTask vt on vt.VisitId = v.ID
                inner join Task t on vt.TaskId = t.ID
                where t.ID = ?TaskId
                order by v.ID desc
                limit 1";

        public static Visit FindLatestVisitByTask(Task task)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindLatestVisit))
            {
                Database.PutParameter(dbCommand, "?TaskId", task.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        return Load(dataReader);
                    }
                }
            }

            throw new DataNotFoundException("Visit not found by task");
        }

        #endregion

        #region Find by Task

        private const string SqlFindByTask =
            @"select * from Visit v
                inner join VisitTask vt on vt.VisitId = v.ID
                inner join Task t on vt.TaskId = t.ID
                where t.ID = ?TaskId";

        public static List<Visit> FindByTask(Task task)
        {
            List<Visit> result = new List<Visit>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTask))
            {
                Database.PutParameter(dbCommand, "?TaskId", task.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }
            }

            return result;
        }

        #endregion

        #region Find by Task Last
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="findLast">true - last, false - first</param>
        /// <returns></returns>
        public static Visit FindByTaskLastFirst(Task task, bool findLast)
        {
            string SqlFindByTaskLast =
            @"select * from Visit v
                inner join VisitTask vt on vt.VisitId = v.ID
                where vt.TaskId = ?TaskId
                order by ID {0}
                limit 1";

            SqlFindByTaskLast = string.Format(SqlFindByTaskLast, findLast ? "desc" : "asc");

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTaskLast))
            {
                Database.PutParameter(dbCommand, "?TaskId", task.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("Last Visit not found, search by Task");
        }

        #endregion

        #region IsExistRecreatedVisitInProcess

        public bool IsExistRecreatedVisitInProcess()
        {
            List<Task> tasks = Task.FindByVisit(this);
            foreach (Task task in tasks)
            {
                if (task.TaskStatus == TaskStatusEnum.Completed
                    || task.TaskStatus == TaskStatusEnum.RugDeliveryCreated)
                {
                    return true;
                }

                if (task.TaskStatus == TaskStatusEnum.NotCompleted)
                {
                    List<Visit> visits = FindByTask(task);
                    foreach (Visit recreatedVisits in visits)
                    {
                        if (recreatedVisits.VisitStatus != VisitStatusEnum.NoGo
                            && recreatedVisits.VisitStatus != VisitStatusEnum.Declined)
                        {
                            return true;
                        }
                    }                    
                }
            }

            return false;
        }

        #endregion        

        #region Arrive

        public static void Arrive(int technicianId, int visitId, DateTime time)
        {
            CreateWorkPerformStartDayIfNecessary(visitId);

            Work work = Work.FindWorkByTechAndDate(technicianId, DateTime.Now);
            Visit visit = FindByPrimaryKey(visitId);                        

            if (visit.VisitStatus == VisitStatusEnum.AssignedForExecution
                || visit.VisitStatus == VisitStatusEnum.Assigned)
            {
                visit.VisitStatus = VisitStatusEnum.Arrived;
                Update(visit);
            }

            try
            {
                WorkTransaction arriveTransaction
                    = WorkTransaction.FindBy(visit.ID, WorkTransactionTypeEnum.VisitArrived);
                arriveTransaction.TransactionDate = DateTime.Now;
                arriveTransaction.EmployeeId = Configuration.CurrentDispatchId;
                WorkTransaction.Update(arriveTransaction);
            }
            catch (DataNotFoundException)
            {
                WorkTransaction workTransaction = new WorkTransaction(0, work.ID, 0, visitId,
                    0, DateTime.Now, decimal.Zero, false);
                workTransaction.EmployeeId = Configuration.CurrentDispatchId;
                workTransaction.WorkTransactionType = WorkTransactionTypeEnum.VisitArrived;
                WorkTransaction.Insert(workTransaction);
            }            

            WorkDetail workDetail = WorkDetail.FindByWorkAndVisit(work, visit, null);
            if (!workDetail.TimeBeginAssigned.HasValue)
                workDetail.TimeBeginAssigned = workDetail.TimeBegin;
            if (!workDetail.TimeEndAssigned.HasValue)
                workDetail.TimeEndAssigned = workDetail.TimeEnd;
            if (time > workDetail.TimeEnd)
                workDetail.TimeEnd = time;
            workDetail.TimeArrive = time;                
            WorkDetail.UpdateAndLog(workDetail);           
        }

        #endregion

        #region UndoArrive

        private static void UndoArrive(Visit visit, Work work)
        {
            try
            {
                WorkTransaction.FindBy(visit.ID, WorkTransactionTypeEnum.VisitDispatched);
                visit.VisitStatus = VisitStatusEnum.AssignedForExecution;
            }
            catch (DataNotFoundException)
            {
                visit.VisitStatus = VisitStatusEnum.Assigned;
            }
            Update(visit);

            WorkDetail workDetail = WorkDetail.FindByWorkAndVisit(work, visit, null);
            workDetail.TimeArrive = null;            
            if (visit.VisitStatus == VisitStatusEnum.Assigned)
            {
                workDetail.TimeBegin = workDetail.TimeBeginAssigned.Value;
                workDetail.TimeEnd = workDetail.TimeEndAssigned.Value;
                workDetail.TimeBeginAssigned = null;
                workDetail.TimeEndAssigned = null;
            } else
            {
                workDetail.TimeEnd = workDetail.TimeDispatch.Value.Add(
                    workDetail.TimeEndAssigned.Value - workDetail.TimeBeginAssigned.Value);
            }
            WorkDetail.UpdateAndLog(workDetail);

            WorkTransaction arriveTransaction 
                = WorkTransaction.FindBy(visit.ID, WorkTransactionTypeEnum.VisitArrived);
            WorkTransaction.Delete(arriveTransaction);
        }

        #endregion

        #region AssignVisitExecution

        public static void AssignVisitExecution(int technicianId, int visitId, DateTime time)
        {
            CreateWorkPerformStartDayIfNecessary(visitId);

            Visit visit = FindByPrimaryKey(visitId);
            if (visit.VisitStatus == VisitStatusEnum.Assigned)
            {
                visit.VisitStatus = VisitStatusEnum.AssignedForExecution;
                Update(visit);                
            }

            Work work = Work.FindWorkByTechAndDate(technicianId, DateTime.Now);

            try
            {
                WorkTransaction dispatchTransaction
                    = WorkTransaction.FindBy(visit.ID, WorkTransactionTypeEnum.VisitDispatched);
                dispatchTransaction.TransactionDate = DateTime.Now;
                dispatchTransaction.EmployeeId = Configuration.CurrentDispatchId;
                WorkTransaction.Update(dispatchTransaction);
            }
            catch (DataNotFoundException)
            {
                WorkTransaction dispatchTransaction = new WorkTransaction(0, work.ID, 0, visitId,
                    0, DateTime.Now, 0, false);
                dispatchTransaction.EmployeeId = Configuration.CurrentDispatchId;
                dispatchTransaction.WorkTransactionType = WorkTransactionTypeEnum.VisitDispatched;
                WorkTransaction.Insert(dispatchTransaction);
            }            

            WorkDetail workDetail = WorkDetail.FindByWorkAndVisit(work, visit, null);
            if (!workDetail.TimeBeginAssigned.HasValue)
                workDetail.TimeBeginAssigned = workDetail.TimeBegin;
            if (!workDetail.TimeEndAssigned.HasValue)
                workDetail.TimeEndAssigned = workDetail.TimeEnd;
            TimeSpan duration = workDetail.TimeEnd - workDetail.TimeBegin;
            workDetail.TimeBegin = time;
            workDetail.TimeDispatch = time;

            if (!workDetail.TimeArrive.HasValue && !workDetail.TimeComplete.HasValue)
                workDetail.TimeEnd = workDetail.TimeBegin + duration;

            if (workDetail.TimeEnd.Date > workDetail.TimeBegin.Date)
            {
                workDetail.TimeEnd = new DateTime(
                    workDetail.TimeBegin.Year,
                    workDetail.TimeBegin.Month,
                    workDetail.TimeBegin.Day, 23, 59, 59);
            }
            WorkDetail.UpdateAndLog(workDetail);
        }

        #endregion

        #region UndoAssignVisitExecution

        private static void UndoAssignVisitExecution(Visit visit, Work work)
        {
            visit.VisitStatus = VisitStatusEnum.Assigned;
            Update(visit);

            WorkDetail workDetail = WorkDetail.FindByWorkAndVisit(work, visit, null);
            workDetail.TimeDispatch = null;
            workDetail.TimeBegin = workDetail.TimeBeginAssigned.Value;
            workDetail.TimeEnd = workDetail.TimeEndAssigned.Value;
            workDetail.TimeBeginAssigned = null;
            workDetail.TimeEndAssigned = null;
            WorkDetail.UpdateAndLog(workDetail);

            WorkTransaction dispatchTransaction
                = WorkTransaction.FindBy(visit.ID, WorkTransactionTypeEnum.VisitDispatched);
            WorkTransaction.Delete(dispatchTransaction);
            
        }

        #endregion

        #region NoGo

        public static void NoGo(int technicianId, int visitId, bool isRealTimeMode, 
            Work work, IDbConnection connection)
        {
            Work currentWork;
            if (isRealTimeMode)
                currentWork = Work.FindWorkByTechAndDate(technicianId, DateTime.Now, connection);
            else
                currentWork = work;

            Visit visit = FindByPrimaryKey(visitId, connection);

//            if (isRealTimeMode)
//            {
//                WorkDetail workDetail = WorkDetail.FindByWorkAndVisit(currentWork, visit, connection);
//                if (visit.VisitStatus == VisitStatusEnum.Assigned
//                    || visit.VisitStatus == VisitStatusEnum.AssignedForExecution)
//                {
//                    WorkDetail.SetVisitStartTime(workDetail, DateTime.Now, connection);
//                }                    
//                WorkDetail.ChangeVisitDuration(workDetail, DateTime.Now - workDetail.TimeBegin, connection);
//            }

            visit.VisitStatus = VisitStatusEnum.NoGo;
            Update(visit, connection);

            WorkTransaction workTransaction = new WorkTransaction(0, currentWork.ID,
                0, visitId, 0, DateTime.Now, decimal.Zero, false);
            workTransaction.EmployeeId =
                connection == null ? Configuration.CurrentDispatchId : technicianId;
            workTransaction.WorkTransactionType = WorkTransactionTypeEnum.NoGo;
            WorkTransaction.Insert(workTransaction, connection);
        }

        public static void NoGo(int technicianId, int visitId, bool isRealTimeMode,
            Work work)
        {
            NoGo(technicianId, visitId, isRealTimeMode, work, null);            
        }

        #endregion

        #region Etc

        public static void Etc(int technicianId, int visitId, decimal saleAmount, DateTime endTime, string notes, IDbConnection connection)
        {
            Work work = Work.FindWorkByTechAndDate(technicianId, DateTime.Now, connection);

            WorkTransaction workTransaction = new WorkTransaction(0, work.ID, 0,
                visitId, 0, DateTime.Now, decimal.Zero, false);
            workTransaction.EmployeeId =
                connection == null ? Configuration.CurrentDispatchId : technicianId;
            workTransaction.WorkTransactionType = WorkTransactionTypeEnum.SubmitETC;
            WorkTransaction.Insert(workTransaction, connection);

            WorkDetail workDetail = WorkDetail.FindByWorkAndVisit(work, new Visit(visitId), connection);
            workDetail.TimeEnd = endTime;
            WorkDetail.UpdateAndLog(workDetail);

            WorkTransactionEtc etc = new WorkTransactionEtc(workTransaction.ID, saleAmount,
                endTime.Hour, endTime.Minute, notes);
            WorkTransactionEtc.Insert(etc, connection);
        }

        #endregion      

        #region FindVisitWrappers
        public static BindingList<VisitWrapper> FindVisitWrappers(int? exactVisitId)
        {
            return FindVisitWrappers(exactVisitId,
                                    string.Empty, string.Empty, string.Empty, string.Empty, 
                                    string.Empty, string.Empty, string.Empty, string.Empty, 
                                    null, null, null);
        }

        public static BindingList<VisitWrapper> FindVisitWrappersByProjectId(int projectId)
        {
            string sql =
                @"select distinct v.*, t.*, c.*, ca.*, sa.*, wd.*, e.* from visit v
                        join visitTask vt on vt.visitid = v.id
                        join task t on vt.taskId = t.id
                        left join Customer c on v.CustomerId = c.ID
                        left join Address ca on c.AddressId = ca.ID
                        left join Address sa on v.ServiceAddressId = sa.ID
                        left join WorkDetail wd on wd.VisitId = v.ID
                        left join Work w on w.Id = wd.WorkID
                        left join Employee e on w.TechnicianEmployeeId = e.ID
                   where t.projectid = ?ProjectId
                   order by v.id";

            using (IDbCommand dbCommand = Database.PrepareCommand(sql))
            {
                Database.PutParameter(dbCommand, "?ProjectId", projectId);

                BindingList<VisitWrapper> wrappers = new BindingList<VisitWrapper>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    
                    while (dataReader.Read())
                    {
                        int offset = 0;

                        Visit visit = Load(dataReader, offset);
                        offset += Visit.FieldsCount;

                        Task task = Task.Load(dataReader, offset);
                        offset += Task.FieldsCount;

                        Customer customerObj = null;
                        Address customerAddress = null;
                        Address serviceAddress = null;

                        if (!dataReader.IsDBNull(FieldsCount))
                        {
                            customerObj = Customer.Load(dataReader, offset);
                            offset += Customer.FieldsCount;

                            customerAddress = Address.Load(dataReader,
                                                           offset);
                        }
                        offset += Address.FieldsCount;

                        if (!dataReader.IsDBNull(offset))
                        {
                            serviceAddress = Address.Load(dataReader,
                                                          offset);
                        }
                        offset += Address.FieldsCount;

                        WorkDetail workDetail = null;
                        if (!dataReader.IsDBNull(offset))
                        {
                            workDetail = WorkDetail.Load(dataReader,
                                                         offset);
                        }
                        offset += WorkDetail.FieldsCount;

                        Employee technicianObj = null;
                        if (!dataReader.IsDBNull(offset))
                        {
                            technicianObj = Employee.Load(dataReader, offset);

                        }

                        if (wrappers.Count == 0 ||
                            (visit.ID != wrappers[wrappers.Count - 1].Visit.ID))
                        {
                            BindingList<Task> tasks = new BindingList<Task>();
                            tasks.Add(task);
                            wrappers.Add(new VisitWrapper(visit, customerObj, customerAddress,
                                                      serviceAddress, technicianObj, tasks, workDetail));
                        }
                        else
                        {
                            wrappers[wrappers.Count - 1].Tasks.Add(task);

                        }
                    }       
                }

                bool defloodFound = false;
                foreach (VisitWrapper wrapper  in wrappers)
                {
                    Task mainTask = null;
                    foreach (Task task in wrapper.Tasks)
                    {
                        if (task.TaskType == TaskTypeEnum.Deflood)
                        {
                            if (defloodFound)
                                continue;

                            mainTask = task;
                            defloodFound = true;
                            break;
                        }

                        mainTask = task;   
                    }
                    wrapper.MainTaskName = mainTask.TaskTypeText;
                }

                return wrappers;
            }

            
        }

        public static BindingList<VisitWrapper> FindVisitWrappers(int? exactVisitId, 
            string servmanTicket, string firstName, string lastName, string phoneNo,
            string city, string zip, string street, string block, 
            VisitStatusEnum? status, int? technicianId, DateRange dateRange)
        {
            string SqlFindVisitWrappers =
                @"select distinct v.*, c.*, ca.*, sa.*, wd.*, e.* from visit v
                        left join Customer c on v.CustomerId = c.ID
                        left join Address ca on c.AddressId = ca.ID
                        left join Address sa on v.ServiceAddressId = sa.ID
                        left join WorkDetail wd on wd.VisitId = v.ID
                        left join Work w on w.Id = wd.WorkID
                        left join Employee e on w.TechnicianEmployeeId = e.ID  
                        {0}                      
                where";

            if (exactVisitId != null)
            {
                SqlFindVisitWrappers += " v.ID = ?VisitId and";
                SqlFindVisitWrappers = string.Format(SqlFindVisitWrappers, string.Empty);
            }
            else
            {
                if (servmanTicket != null && servmanTicket != string.Empty)
                {
                    SqlFindVisitWrappers = string.Format(SqlFindVisitWrappers,
                                                         "inner join VisitTask vt on vt.VisitId = v.ID inner join Task t on t.ID = vt.TaskId");

                    SqlFindVisitWrappers += " t.ServmanOrderNum = ?ServmanOrderNum and";
                }
                else
                    SqlFindVisitWrappers = string.Format(SqlFindVisitWrappers, string.Empty);

                if (firstName != null && firstName != string.Empty)
                    SqlFindVisitWrappers += " c.FirstName like ?FirstName and";
                if (lastName != null && lastName != string.Empty)
                    SqlFindVisitWrappers += " c.LastName like ?LastName and";
                if (phoneNo != null && phoneNo != string.Empty)
                    SqlFindVisitWrappers += " (c.Phone1 = ?PhoneNumber or c.Phone2 = ?PhoneNumber) and";
                if (city != null && city != string.Empty)
                    SqlFindVisitWrappers += " sa.City like ?City and";
                if (zip != null && zip != string.Empty)
                    SqlFindVisitWrappers += " sa.Zip = ?Zip and";
                if (street != null && street != string.Empty)
                    SqlFindVisitWrappers += " sa.Street like ?Street and";
                if (block != null && block != string.Empty)
                    SqlFindVisitWrappers += " sa.Block = ?Block and";
                if (technicianId != null)
                    SqlFindVisitWrappers += " e.ID = ?TechnicianId and";
                if (status != null)
                    SqlFindVisitWrappers += " v.VisitStatusId = ?VisitStatusId and ";
                if (dateRange != null)
                {
                    if (dateRange.StartDate.HasValue)
                        SqlFindVisitWrappers += " v.ServiceDate >= ?ServiceDateStart and";
                    if (dateRange.EndDate.HasValue)
                        SqlFindVisitWrappers += " v.ServiceDate <= ?ServiceDateEnd and";
                }
            }

            SqlFindVisitWrappers += " 1=1 order by v.ID limit 200";

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindVisitWrappers))
            {
                if (exactVisitId != null)
                    Database.PutParameter(dbCommand, "?VisitId", exactVisitId);
                else
                {
                    if (servmanTicket != null)
                        Database.PutParameter(dbCommand, "?ServmanOrderNum", servmanTicket);
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
                    if (technicianId != null)
                        Database.PutParameter(dbCommand, "?TechnicianId", technicianId);
                    if (status != null)
                        Database.PutParameter(dbCommand, "?VisitStatusId", (int) status);
                    if (dateRange != null)
                    {
                        if (dateRange.StartDate.HasValue)
                            Database.PutParameter(dbCommand, "?ServiceDateStart",
                                                  new DateTime(dateRange.StartDate.Value.Year,
                                                               dateRange.StartDate.Value.Month,
                                                               dateRange.StartDate.Value.Day, 0, 0, 0));
                        if (dateRange.EndDate.HasValue)
                            Database.PutParameter(dbCommand, "?ServiceDateEnd",
                                                  new DateTime(dateRange.EndDate.Value.Year,
                                                               dateRange.EndDate.Value.Month,
                                                               dateRange.EndDate.Value.Day, 23, 59, 59));
                    }
                }

                BindingList<VisitWrapper> wrappers = new BindingList<VisitWrapper>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Visit visit = Load(dataReader);

                        Customer customerObj = null;
                        Address customerAddress = null;
                        Address serviceAddress = null;

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

                        WorkDetail workDetail = null;
                        if (!dataReader.IsDBNull(FieldsCount + Customer.FieldsCount + 2*Address.FieldsCount))
                        {
                            workDetail = WorkDetail.Load(dataReader,
                                                         FieldsCount + Customer.FieldsCount + 2*Address.FieldsCount);
                        }

                        int technicianOffset = FieldsCount + Customer.FieldsCount + 2*Address.FieldsCount
                                               + WorkDetail.FieldsCount;
                        Employee technicianObj = null;
                        if (!dataReader.IsDBNull(technicianOffset))
                            technicianObj = Employee.Load(dataReader, technicianOffset);

                        wrappers.Add(new VisitWrapper(visit, customerObj, customerAddress,
                                                      serviceAddress, technicianObj, null, workDetail));
                    }
                }
                return wrappers;
            }
        }

        public static BindingList<VisitWrapper> FindVisitWrappers(int? exactVisitId, int? workId,
            string visitId, string servmanTicket, string mapsco, string customer, 
            string address, string technician, VisitStatusEnum? status, DateRange dateRange)
        {
            string SqlFindVisitWrappers =
                    @"select distinct v.*, c.*, ca.*, sa.*, wd.*, e.* from visit v
                        left join Customer c on v.CustomerId = c.ID
                        left join Address ca on c.AddressId = ca.ID
                        left join Address sa on v.ServiceAddressId = sa.ID
                        left join WorkDetail wd on wd.VisitId = v.ID
                        left join Work w on w.Id = wd.WorkID
                        left join Employee e on w.TechnicianEmployeeId = e.ID  
                        {0}                      
                where";

            if (exactVisitId != null)
            {
                SqlFindVisitWrappers += " v.ID = ?VisitId and";
                SqlFindVisitWrappers = string.Format(SqlFindVisitWrappers, string.Empty);
            }                
            else
            {
                if (visitId != null && visitId != string.Empty)
                    SqlFindVisitWrappers += " v.ID like ?VisitId and";

                if (servmanTicket != null && servmanTicket != string.Empty)
                {
                    SqlFindVisitWrappers = string.Format(SqlFindVisitWrappers, 
                        "inner join VisitTask vt on vt.VisitId = v.ID inner join Task t on t.ID = vt.TaskId");

                    SqlFindVisitWrappers += " t.ServmanOrderNum = ?ServmanOrderNum and";
                }                    
                else
                    SqlFindVisitWrappers = string.Format(SqlFindVisitWrappers, string.Empty);

                if (workId != null)
                    SqlFindVisitWrappers += " w.ID = ?WorkId and";
                if (mapsco != null && mapsco != string.Empty)
                    SqlFindVisitWrappers += " CONCAT(sa.MapPage, sa.MapLetter) like ?Map and";
                if (customer != null && customer != string.Empty)
                    SqlFindVisitWrappers += " (c.FirstName like ?Customer or c.LastName like ?Customer) and";
                if (address != null && address != string.Empty)
                    SqlFindVisitWrappers += " (sa.Block like ?Address or sa.Street like ?Address or sa.Address2 like ?Address or sa.City like ?Address or sa.State like ?Address or sa.Zip like ?Address) and";
                if (technician != null && technician != string.Empty)
                    SqlFindVisitWrappers += " (e.FirstName like ?Technician or e.LastName like ?Technician) and";
                if (status != null)
                    SqlFindVisitWrappers += " v.VisitStatusId = ?VisitStatusId and ";
                if (dateRange != null)
                {
                    if (dateRange.StartDate.HasValue)
                        SqlFindVisitWrappers += " v.ServiceDate >= ?ServiceDateStart and";
                    if (dateRange.EndDate.HasValue)
                        SqlFindVisitWrappers += " v.ServiceDate <= ?ServiceDateEnd and";
                }                
            } 

            SqlFindVisitWrappers += " 1=1 order by v.ID limit 200";

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindVisitWrappers))
            {
                if (exactVisitId != null)
                    Database.PutParameter(dbCommand, "?VisitId", exactVisitId);
                else
                {
                    if (visitId != null)
                        Database.PutParameter(dbCommand, "?VisitId", visitId + "%");
                    if (servmanTicket != null)
                        Database.PutParameter(dbCommand, "?ServmanOrderNum", servmanTicket);
                    if (workId != null)
                        Database.PutParameter(dbCommand, "?WorkId", workId);
                    if (mapsco != null && mapsco != string.Empty)
                        Database.PutParameter(dbCommand, "?Map", mapsco + "%");
                    if (customer != null && customer != string.Empty)
                        Database.PutParameter(dbCommand, "?Customer", customer + "%");
                    if (address != null && address != string.Empty)
                        Database.PutParameter(dbCommand, "?Address", "%" + address + "%");
                    if (technician != null && technician != string.Empty)
                        Database.PutParameter(dbCommand, "?Technician", technician + "%");
                    if (status != null)
                        Database.PutParameter(dbCommand, "?VisitStatusId", (int)status);
                    if (dateRange != null)
                    {
                        if (dateRange.StartDate.HasValue)
                            Database.PutParameter(dbCommand, "?ServiceDateStart",
                                new DateTime(dateRange.StartDate.Value.Year,
                                dateRange.StartDate.Value.Month,
                                dateRange.StartDate.Value.Day, 0, 0, 0));
                        if (dateRange.EndDate.HasValue)
                            Database.PutParameter(dbCommand, "?ServiceDateEnd",
                                new DateTime(dateRange.EndDate.Value.Year,
                                dateRange.EndDate.Value.Month,
                                dateRange.EndDate.Value.Day, 23, 59, 59));
                    }                       
                }             

                BindingList<VisitWrapper> wrappers = new BindingList<VisitWrapper>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {                                                
                        Visit visit = Load(dataReader);

                        Customer customerObj = null;
                        Address customerAddress = null;
                        Address serviceAddress = null;

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

                        WorkDetail workDetail = null;
                        if (!dataReader.IsDBNull(FieldsCount + Customer.FieldsCount + 2 * Address.FieldsCount))
                        {
                            workDetail = WorkDetail.Load(dataReader,
                                FieldsCount + Customer.FieldsCount + 2*Address.FieldsCount);
                        }

                        int technicianOffset = FieldsCount + Customer.FieldsCount + 2 * Address.FieldsCount
                                + WorkDetail.FieldsCount;
                        Employee technicianObj = null;
                        if (!dataReader.IsDBNull(technicianOffset))
                            technicianObj = Employee.Load(dataReader, technicianOffset);

                        wrappers.Add(new VisitWrapper(visit, customerObj, customerAddress,
                            serviceAddress, technicianObj, null, workDetail));
                    }

                }
                return wrappers;
            }
            
        }

        #endregion

        #region UndoConfirm

        private static void UndoConfirm(Visit visit)
        {
            Visit visitFounded = FindByPrimaryKey(visit.ID);
            visitFounded.ConfirmDateTime = null;
            visitFounded.ConfirmedFrameBegin = null;
            visitFounded.ConfirmedFrameEnd = null;
            visitFounded.ConfirmLeftMessage = false;
            visitFounded.ConfirmBusy = false;
            visitFounded.IsCallOnYourWay = false;
            Update(visitFounded);
        }

        #endregion        

        #region UndoLastOperation

        public static void UndoLastOperation(Visit visit, Work work)
        {
            if (visit.VisitStatus == VisitStatusEnum.Completed)
                VisitCompletePackage.UndoCompleteVisit(visit, work, false);
            else if (visit.VisitStatus == VisitStatusEnum.Arrived)
                UndoArrive(visit, work); //OK
            else if (visit.VisitStatus == VisitStatusEnum.AssignedForExecution)
                UndoAssignVisitExecution(visit, work); //OK
            else //Undo Confirm
                UndoConfirm(visit);            
        }

        #endregion

        #region GetVisitEditUndoAllowance

        public static string GetVisitEditUndoAllowance(Visit visit, List<Task> tasksToBeModified, 
            bool isUndo, bool isEdit)
        {
            if (visit.VisitStatus != VisitStatusEnum.Completed)
                return string.Empty;

            string result = string.Empty;

            WorkTransaction completeTransaction =
                WorkTransaction.FindBy(visit.ID, WorkTransactionTypeEnum.VisitCompleted);

            List<Task> generatedTasks = Task.FindGeneratedTasksOnCompletion(visit, tasksToBeModified);
            foreach (Task task in generatedTasks)
            {
                if (task.TaskType == TaskTypeEnum.RugDelivery && task.IsReady)
                {
                    result +=
                        string.Format("Rug Pickup task completion cannot be {0} because its Rug Delivery task is marked ready\n",
                        isUndo ? "undone" : "edited");                    
                }

                if (task.TaskType == TaskTypeEnum.Monitoring)
                {
                    Visit visitWithNextMonitoring = FindNextVisit(task, visit);

                    if (visitWithNextMonitoring.VisitStatus != VisitStatusEnum.Pending)
                    {
                        result +=
                            string.Format("Monitoring task completion cannot be {0} because visit which contains next monitoring task is placed on dashboard\n",
                            isUndo ? "undone" : "edited");
                    }
                }
            }

            List<Task> transferredTasks = Task.FindTransferredTasksOnCompletion(visit, tasksToBeModified);
            foreach (Task task in transferredTasks)
            {                
                WorkTransactionTask taskTransaction =
                    WorkTransactionTask.FindByPrimaryKey(completeTransaction.ID, task.ID);

                if (taskTransaction.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.FailMustReturn)
                {
                    Visit visitWithTransferredTask = FindNextVisit(task, visit);

                    if (visitWithTransferredTask.VisitStatus != VisitStatusEnum.Pending)
                    {
                        result +=
                            string.Format(task.TaskTypeText
                            + " task action cannot be {0} because it was processed with Fail Must Return and next visit containing this task is placed on dashboard\n",
                            isUndo ? "undone" : "edited");                                            
                    }
                }

                if (taskTransaction.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.Cancel)
                {
                    result +=
                        string.Format(task.TaskTypeText
                        + " task action cannot be {0} because it was Cancelled and later was included in another visit\n",
                        isUndo ? "undone" : "edited");                    
                }

                if (taskTransaction.WorkTransactionTaskAction == WorkTransactionTaskActionEnum.Complete)
                {
                    result +=
                        string.Format(task.TaskTypeText
                        + " task completion cannot be {0} because it was reopened later and included in another visit\n",
                        isUndo ? "undone" : "edited");                    
                }
            }

            List<Task> bookedTasks = Task.FindBookedTasksOnCompletion(visit, tasksToBeModified);
            foreach (Task task in bookedTasks)
            {                
                Visit bookedVisit = FindByTaskLastFirst(task, false);

                if (bookedVisit.VisitStatus != VisitStatusEnum.Pending)
                {
                    result +=
                        string.Format(task.TaskTypeText
                        + " task booking cannot be {0} because next visit containing this task is placed on dashboard\n",
                        isUndo ? "undone" : "edited");
                }                
            }


            return result;
        }

        #endregion

        #region GetConfirmationReasons

        public List<DispatchNotificationReasonEnum> GetConfirmationReasons(DateTime dispatchTime)
        {
            List<DispatchNotificationReasonEnum> result = new List<DispatchNotificationReasonEnum>();

            if (ConfirmedFrameBegin.HasValue
                && ConfirmedFrameEnd.HasValue
                && (dispatchTime.AddMinutes(30) < ConfirmedFrameBegin.Value
                || dispatchTime.AddMinutes(30) > ConfirmedFrameEnd.Value))
            {
                result.Add(DispatchNotificationReasonEnum.OutOfTimeFrame);
            }

            if (ConfirmLeftMessage)
                result.Add(DispatchNotificationReasonEnum.LeftMessage);

            if (ConfirmBusy)
                result.Add(DispatchNotificationReasonEnum.Busy);

            if (IsCallOnYourWay)
                result.Add(DispatchNotificationReasonEnum.CallOnTheWay);

            return result;
        }

        #endregion

        #region CreateWorkPerformStartDayIfNecessary

        public static void CreateWorkPerformStartDayIfNecessary(int visitId)
        {
            Work work = Work.FindByVisit(new Visit(visitId));

            if (work.WorkStatus == WorkStatusEnum.Pending)
                WorkPackage.MoveWorkToReadyForStartDayDefault(work);
            
            if (work.WorkStatus == WorkStatusEnum.ReadyForStartDay)
                StartDayDonePackage.SaveStartDayDoneDefault(work);                        
        }

        #endregion

        #region FindNotPrintedBySyncTool

        private const string SqlFindNotPrintedBySyncTool =
            @"select * from Visit
                where DATE(ServiceDate) = ?ServiceDate
                    and SyncToolPrintDate is null";

        public static List<Visit> FindNotPrintedBySyncTool(DateTime date)
        {
            List<Visit> visits = new List<Visit>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindNotPrintedBySyncTool))
            {
                Database.PutParameter(dbCommand, "?ServiceDate", date.Date);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        visits.Add(Load(dataReader));
                }
            }
            return visits;
        }

        #endregion

        #region FindNextVisit

        private const string SqlFindNextVisit =
            @"select * from Visit v
                inner join VisitTask vt on vt.VisitId = v.ID
                inner join Task t on vt.TaskId = t.ID
                where t.ID = ?TaskId and v.ID > ?PreviousVisitId                     
                order by v.ID
                limit 1";

        public static Visit FindNextVisit(Task task, Visit previousVisit)
        {            
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindNextVisit))
            {
                Database.PutParameter(dbCommand, "?TaskId", task.ID);
                Database.PutParameter(dbCommand, "?PreviousVisitId", previousVisit.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("Next visit not found");
        }

        #endregion

        #region IsSplitPossible

        public static bool IsSplitPossible(Visit visit)
        {
            List<Task> tasks = Task.FindByVisit(visit);

            foreach (Task task in tasks)
            {
                if (task.TaskType == TaskTypeEnum.Deflood)
                {
                    tasks.Remove(task);
                    break;
                }
            }

            return tasks.Count > 1;
        }

        #endregion

        #region FindMergablePendingVisits

        private const string SqlFindMergablePendingVisits =
            @"select * from Visit
                where (ServiceDate is null or DATE(ServiceDate) = ?ServiceDate)
                    and ServiceAddressId = ?ServiceAddressId
                    and VisitStatusId = 1
                    and ID != ?MainVisitId";

        public static List<Visit> FindMergablePendingVisits(Visit visit, DateTime serviceDate)
        {   
            List<Visit> result = new List<Visit>();

            if (!visit.ServiceAddressId.HasValue)
                return result;

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindMergablePendingVisits))
            {
                Database.PutParameter(dbCommand, "?ServiceDate", serviceDate.Date);
                Database.PutParameter(dbCommand, "?ServiceAddressId", visit.ServiceAddressId.Value);
                Database.PutParameter(dbCommand, "?MainVisitId", visit.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return RemoveUnsatisfiedMergableVisit(result);
        }

        #endregion        

        #region FindMergableDashboardVisits

        private const string SqlFindMergableDashboardVisits =
            @"SELECT v.* FROM WorkDetail wd
                inner join Visit v on v.ID = wd.VisitId
            where
                  DATE(TimeBegin) = ?ScheduleDate              
              and v.VisitStatusId != 2
              and v.ServiceAddressId = ?ServiceAddressIs
              and VisitId != ?MainVisitId";

        public static List<Visit> FindMergableDashboardVisits(Visit visit, DateTime scheduleDate)
        {
            List<Visit> result = new List<Visit>();

            if (!visit.ServiceAddressId.HasValue)
                return result;

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindMergableDashboardVisits))
            {
                Database.PutParameter(dbCommand, "?ScheduleDate", scheduleDate.Date);
                Database.PutParameter(dbCommand, "?ServiceAddressIs", visit.ServiceAddressId.Value);
                Database.PutParameter(dbCommand, "?MainVisitId", visit.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return RemoveUnsatisfiedMergableVisit(result);
        }

        #endregion

        #region RemoveUnsatisfiedMergableVisit

        private static List<Visit> RemoveUnsatisfiedMergableVisit(List<Visit> visits)
        {
            List<Visit> unsatisfiedVisits = new List<Visit>();
            foreach (Visit visit in visits)
            {
                List<Task> tasks = Task.FindByVisit(visit);

                foreach (Task task in tasks)
                {
                    if (!task.IsReady || task.TaskType == TaskTypeEnum.Help)
                    {
                        unsatisfiedVisits.Add(visit);
                        break;
                    }
                }
            }

            foreach (Visit visit in unsatisfiedVisits)
                visits.Remove(visit);

            return visits;
        }

        #endregion

        #region GetTaskTypes

        public string GetTaskTypes(List<Task> tasks)
        {
            string taskTypes = string.Empty;
            foreach (Task task in Task.GetTasksFiltered(tasks, this))
                taskTypes += task.TaskTypeText + " - Job#" +task.ProjectId;

            return taskTypes;
        }

        #endregion

        #region MergeVisits

        public static void MergeVisits(Visit mainVisit, List<Visit> visitsToMerge)
        {
            Visit freshMainVisit = FindByPrimaryKey(mainVisit.ID);
            List<string> combinedNotes = new List<string>();

            foreach (Visit visit in visitsToMerge)
            {
                List<VisitTask> visitTasks = VisitTask.FindBy(visit);

                foreach (VisitTask visitTask in visitTasks)
                {
                    VisitTask.Delete(visitTask);
                    VisitTask.Insert(new VisitTask(freshMainVisit.ID, visitTask.TaskId));
                }

                combinedNotes.Add(visit.Notes);
                WorkDetail.DeleteBy(visit);
                Delete(visit);
            }

            freshMainVisit.Notes = Utils.JoinStrings("\r\n", combinedNotes.ToArray());
            Update(freshMainVisit);
        }

        #endregion

        #region IsNeedToPrint

        private bool IsServiceDateSatisfiesPrinting()
        {
            VisitSummaryPackage summaryPackage = new VisitSummaryPackage(this);

            Visit dummyVisit = new Visit();
            dummyVisit.ServiceDate = summaryPackage.GetAdjustedServiceDate(); 

            if (dummyVisit.IsServiceDateTodayOrTomorrow)
                return true;

            return false;
        }

        private bool IsContainsRugDelivery()
        {
            List<Task> tasks = Task.FindByVisit(this);
            foreach (Task task in tasks)
            {
                if (task.TaskType == TaskTypeEnum.RugDelivery)
                    return true;
            }

            return false;
        }

        public bool IsNeedToPrint(VisitSummaryPackage originalSummaryPackage, bool isNewlyGeneratedVisit)
        {
            if (!Configuration.AutomatedVisitPrint)
                return false;

            bool isContainsRugDelivery = IsContainsRugDelivery();

            if (isNewlyGeneratedVisit && isContainsRugDelivery)
                return true;

            if (isContainsRugDelivery)
            {
                if (originalSummaryPackage == null)
                    return true;

                VisitSummaryPackage modifiedSummaryPackage = new VisitSummaryPackage(this);
                if (!modifiedSummaryPackage.IsPrintDataEqual(originalSummaryPackage))
                    return true;
                
            } else 
            {
                if (originalSummaryPackage == null)
                    return IsServiceDateSatisfiesPrinting();

                if (!IsServiceDateSatisfiesPrinting())
                    return false;

                VisitSummaryPackage modifiedSummaryPackage = new VisitSummaryPackage(this);
                if (!modifiedSummaryPackage.IsPrintDataEqual(originalSummaryPackage))
                    return true;
            }

            return false;
        }

        #endregion

        #region GetSuggestedConfirmTimeStart

        public static DateTime GetSuggestedConfirmTimeStart(DateTime estimatedArrivalTime)
        {
            if (estimatedArrivalTime.Minute >= 0 && estimatedArrivalTime.Minute <= 45)
            {
                return new DateTime(estimatedArrivalTime.Year, estimatedArrivalTime.Month,
                                    estimatedArrivalTime.Day, estimatedArrivalTime.Hour, 0, 0);
            }
            else
            {
                int adjustedHour;

                if (estimatedArrivalTime.Hour != 23)
                    adjustedHour = estimatedArrivalTime.Hour + 1;
                else
                    adjustedHour = estimatedArrivalTime.Hour;

                return new DateTime(estimatedArrivalTime.Year, estimatedArrivalTime.Month,
                                    estimatedArrivalTime.Day, adjustedHour, 0, 0);
            }
        }

        #endregion

        #region FindByTaskAndProcessDate

        private const string SqlFindByTaskAndProcessDate =
            @"SELECT v.* FROM worktransactiontask wtt
                inner join Task t on t.ID = wtt.TaskId
                inner join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
                inner join Work w on w.ID = wt.WorkId
                inner join Visit v on v.ID = wt.VisitId
                inner join WorkDetail wd on wd.WorkId = w.ID and wd.VisitId = v.ID
            where TaskId = ?TaskId
                and (
                       (t.TaskTypeId != 4 and t.TaskStatusId = 2 and wtt.WorkTransactionTaskActionId = 1) -- completed for all not deflood
                    or (t.TaskTypeId = 4  and (t.TaskStatusId = 2 or t.TaskStatusId = 4 ) and wtt.WorkTransactionTaskActionId = 2) -- in process
                    or (t.TaskStatusId = 1 and t.TaskFailTypeId is not null and t.TaskFailTypeId = 1 and wtt.WorkTransactionTaskActionId = 3) -- fail must return
                    or (t.TaskStatusId = 1 and t.TaskFailTypeId is not null and t.TaskFailTypeId = 2 and wtt.WorkTransactionTaskActionId = 4) -- fail may return
                    or (t.TaskStatusId = 1 and t.TaskFailTypeId is not null and t.TaskFailTypeId = 3 and wtt.WorkTransactionTaskActionId = 5) -- fail may return
                    )
                and DATE(w.StartDate) = ?ProcessDate
            order by wd.TimeEnd";

        public static Visit FindByTaskAndProcessDate(Task task, DateTime processDate)
        {
            List<Visit> candidates = new List<Visit>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTaskAndProcessDate))
            {
                Database.PutParameter(dbCommand, "?TaskId", task.ID);
                Database.PutParameter(dbCommand, "?ProcessDate", processDate.Date);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        candidates.Add(Load(dataReader));
                }
            }

            if (candidates.Count == 1)
                return candidates[0];
            else if (candidates.Count > 1)
            {
                if (task.TaskType == TaskTypeEnum.Deflood)
                    return candidates[0];
                else
                    return candidates[candidates.Count - 1];
            }

            throw new DataNotFoundException("Visit not found");
        }

        #endregion  
    }

    public class VisitWrapper
    {
        private Visit m_visit;
        private Customer m_customer;
        private Address m_customerAddress;
        private Address m_serviceAddress;
        private Employee m_technician;
        private BindingList<Task> m_tasks;
        private WorkDetail m_workDetail;

        #region Constructor

        public VisitWrapper(Visit visit, Customer customer, Address customerAddress,
                              Address serviceAddress, Employee technician, BindingList<Task> tasks, 
            WorkDetail workDetail)
        {
            m_visit = visit;
            m_customer = customer;
            m_customerAddress = customerAddress;
            m_serviceAddress = serviceAddress;
            m_technician = technician;
            m_tasks = tasks;
            m_workDetail = workDetail;            
        }

        #endregion

        #region Visit

        public Visit Visit
        {
            get { return m_visit; }
            set { m_visit = value; }
        }

        #endregion

        #region Customer

        public Customer Customer
        {
            get { return m_customer; }
        }

        #endregion

        #region MainTask

        private string m_mainTask;
        public string MainTaskName
        {
            get { return m_mainTask; }
            set { m_mainTask = value; }
        }

        #endregion 

        #region Technician

        public Employee Technician
        {
            get { return m_technician; }
        }

        #endregion

        #region CustomerAddress

        public Address CustomerAddress
        {
            get { return m_customerAddress; }
        }

        #endregion

        #region ServiceAddress

        public Address ServiceAddress
        {
            get { return m_serviceAddress; }
            set { m_serviceAddress = value; }
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
            get { return m_visit.ID; }
        }

        #endregion        

        #region VisitStatus

        public VisitStatusEnum VisitStatus
        {
            get { return m_visit.VisitStatus; }
        }

        #endregion

        #region VisitStatusText

        public string VisitStatusText
        {
            get { return m_visit.VisitStatusText; }
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

        #region CustomerId

        public string CustomerId
        {
            get
            {
                if (m_customer == null)
                    return string.Empty;
                return m_customer.ID.ToString();
            }
        }

        #endregion

        #region TechnicianName

        public string TechnicianName
        {
            get
            {
                if (m_technician != null)
                    return m_technician.DisplayName;
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

        #region Duration

        public string Duration
        {
            get { return m_visit.DurationText; }
            set
            {
                m_visit.DurationMin = Visit.GetDuration(value);
                UpdateVisit();
            }
        }

        #endregion

        #region TimeFrame

        public string TimeFrame
        {
            get { return m_visit.TimeFrameText; }
        }

        #endregion

        #region CreateDate

        public DateTime CreateDate
        {
            get
            {
                return m_visit.CreateDate;
            }
        }

        #endregion

        #region Date

        public DateTime? Date
        {
            get
            {
                if (m_workDetail != null)
                    return m_workDetail.TimeBegin.Date;
                return m_visit.ServiceDate;
            }
        }

        #endregion

        #region Notes

        public string Notes
        {
            get { return m_visit.Notes; }
            set
            {
                m_visit.Notes = value;
                UpdateVisit();
            }
        }

        #endregion

        #region UpdateVisit

        private void UpdateVisit()
        {
            Visit.Update(m_visit);

            if (m_visit.VisitStatus == VisitStatusEnum.Pending)
                PendingTaskGridState.MakePendingTaskGridDirty(Configuration.CurrentDispatchId);
            else
                DashboardState.MakeDashboardDirty(Configuration.CurrentDispatchId);
        }

        #endregion

        #region DashboardLink

        public string DashboardLink
        {
            get { return "Dashboard"; }
        }

        #endregion

        #region VisitLink

        public string VisitLink
        {
            get { return "Visit"; }
        }

        #endregion

        #region IsRecreateAllowed

        public bool IsRecreateAllowed
        {
            get
            {
                if (VisitStatus == VisitStatusEnum.NoGo
                    || VisitStatus == VisitStatusEnum.Declined)
                {
                    return true;
                }
                return false;
            }
        }

        #endregion

        #region ClosedAmount

        public decimal ClosedAmount
        {
            get
            {
                return m_visit.ClosedDollarAmount;
            }
        }

        #endregion

        #region StartTime

        public DateTime? StartTime
        {
            get
            {
                if (m_workDetail != null)
                    return m_workDetail.TimeBegin;
                return null;
            }
        }

        #endregion

        }
}
      