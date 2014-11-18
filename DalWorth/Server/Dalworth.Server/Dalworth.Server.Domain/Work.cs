using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Xml.Serialization;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Domain
{
    public enum WorkUndoOperationEnum
    {
        Unavailable,
        CreateWork,
        StartDay,
        CompleteWork
    }

    public enum WorkUndoAllwanceEnum
    {
        Allowed,
        NotAllowedProcessedVisitsExist
    }

    public partial class Work 
    {
        public Work(){}        

        #region WorkStatus

        [XmlIgnore]
        public WorkStatusEnum WorkStatus
        {
            get { return (WorkStatusEnum)m_workStatusId; }
            set { m_workStatusId = (int)value; }
        }

        #endregion

        #region WorkStatusText

        public string WorkStatusText
        {
            get
            {
                if (WorkStatus == WorkStatusEnum.Completed)
                    return "Completed";
                else if (WorkStatus == WorkStatusEnum.Pending)
                    return "Pending";
                else if (WorkStatus == WorkStatusEnum.ReadyForStartDay)
                    return "Start Day ready";
                else if (WorkStatus == WorkStatusEnum.StartDayDone)
                    return "Start Day done";

                return string.Empty;
            }
        }

        #endregion

        #region WorkStatusUserFriendlyText

        public string WorkStatusUserFriendlyText
        {
            get
            {
                if (WorkStatus == WorkStatusEnum.Pending)
                    return "Pending";
                if (WorkStatus == WorkStatusEnum.ReadyForStartDay)
                    return "Ready";
                if (WorkStatus == WorkStatusEnum.StartDayDone)
                    return "Working";
                if (WorkStatus == WorkStatusEnum.Completed)
                    return "Completed";

                return string.Empty;
            }
        }

        #endregion

        #region FindPendingWork

        private const string SqlFindByEmployeeAndDate =
            @"SELECT *
            FROM Work
                WHERE TechnicianEmployeeId = ?TechnicianEmployeeId
                    AND StartDate >= ?StartDateStart
                    AND StartDate < ?StartDateEnd
                    AND WorkStatusId = 1";


        public static Work FindPendingWork(int technicianEmployeeId, DateTime startDate, IDbConnection connection)
        {
            List<Work> workList = new List<Work>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByEmployeeAndDate, connection))
            {
                Database.PutParameter(dbCommand, "?TechnicianEmployeeId", technicianEmployeeId);
                DateTime now = DateTime.Now;
                Database.PutParameter(dbCommand, "?StartDateStart", new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, 0));
                Database.PutParameter(dbCommand, "?StartDateEnd", new DateTime(now.Year, now.Month, now.Day, 23, 59, 59, 999));

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        workList.Add(Load(dataReader));
                    }
                }
            }

            if (workList.Count == 0)
                return null;
            else if (workList.Count > 1)
                throw new DataNotFoundException("Multiple works for this date");
            else
                return workList[0];
        }

        #endregion        

        #region FindWorkByTechAndDate

        private const string SqlFindWorkByTechAndDate =
            @"SELECT *
            FROM Work
                WHERE TechnicianEmployeeId = ?TechnicianEmployeeId
                    AND StartDate >= ?StartDateStart
                    AND StartDate < ?StartDateEnd";


        public static Work FindWorkByTechAndDate(int technicianEmployeeId, DateTime startDate, IDbConnection connection)
        {
            List<Work> workList = new List<Work>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindWorkByTechAndDate, connection))
            {
                Database.PutParameter(dbCommand, "?TechnicianEmployeeId", technicianEmployeeId);
                Database.PutParameter(dbCommand, "?StartDateStart", new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0, 0));
                Database.PutParameter(dbCommand, "?StartDateEnd", new DateTime(startDate.Year, startDate.Month, startDate.Day, 23, 59, 59, 999));

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        workList.Add(Load(dataReader));
                    }
                }
            }

            if (workList.Count == 0)
                return null;
            else if (workList.Count > 1)
                throw new DataNotFoundException("Multiple works for this date");
            else
                return workList[0];
        }

        public static Work FindWorkByTechAndDate(int technicianEmployeeId, DateTime startDate)
        {
            return FindWorkByTechAndDate(technicianEmployeeId, startDate, null);
        }

        #endregion        

        #region FindWorkByVanAndDate

        private const string SqlFindWorkByVanAndDate =
            @"SELECT *
            FROM Work
                WHERE VanId = ?VanId
                    AND StartDate >= ?StartDateStart
                    AND StartDate < ?StartDateEnd";


        public static Work FindWorkByVanAndDate(int vanId, DateTime startDate)
        {
            List<Work> workList = new List<Work>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindWorkByVanAndDate))
            {
                Database.PutParameter(dbCommand, "?VanId", vanId);
                Database.PutParameter(dbCommand, "?StartDateStart", new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0, 0));
                Database.PutParameter(dbCommand, "?StartDateEnd", new DateTime(startDate.Year, startDate.Month, startDate.Day, 23, 59, 59, 999));

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        workList.Add(Load(dataReader));
                    }
                }
            }

            if (workList.Count == 0)
                return null;
            else if (workList.Count > 1)
                throw new DataNotFoundException("Multiple works for this date");
            else
                return workList[0];
        }

        #endregion        

        #region FindBy Date

        private const string SqlFindByDate =
            @"SELECT *
            FROM Work
                WHERE StartDate >= ?StartDateStart
                    AND StartDate < ?StartDateEnd";


        public static List<Work> FindBy(DateTime startDate)
        {
            List<Work> workList = new List<Work>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByDate))
            {
                Database.PutParameter(dbCommand, "?StartDateStart", new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0, 0));
                Database.PutParameter(dbCommand, "?StartDateEnd", new DateTime(startDate.Year, startDate.Month, startDate.Day, 23, 59, 59, 999));

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        workList.Add(Load(dataReader));
                    }
                }
            }
            return workList;
        }

        #endregion

        #region FindByVisit

        private const string SqlFindByVisit =
            @"SELECT w.*
                FROM WorkDetail wd
                inner join work w on w.ID = wd.WorkId
                where VisitId = ?VisitId";


        public static Work FindByVisit(Visit visit)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByVisit))
            {
                Database.PutParameter(dbCommand, "?VisitId", visit.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("Work not found");
        }

        #endregion        

        #region CompleteWork

        public static void CompleteWork(int workId, DateTime? endDayDate, IDbConnection connection)
        {
            Work work = FindByPrimaryKey(workId, connection);
            work.WorkStatus = WorkStatusEnum.Completed;
            if (endDayDate == null)
                work.EndDayDate = DateTime.Now;
            else
                work.EndDayDate = endDayDate.Value;
            Update(work, connection);

            WorkTransaction workTransaction = null;
            try
            {
                workTransaction =
                    WorkTransaction.FindBy(work, WorkTransactionTypeEnum.Completed);
            }
            catch (DataNotFoundException) { }

            if (workTransaction != null)
            {
                workTransaction.EmployeeId = Configuration.CurrentDispatchId;
                workTransaction.TransactionDate = DateTime.Now;
                WorkTransaction.Update(workTransaction);
            } else
            {
                workTransaction = new WorkTransaction(0, workId,
                    Configuration.CurrentDispatchId, null, 0,
                    DateTime.Now, 0, false);
                workTransaction.WorkTransactionType = WorkTransactionTypeEnum.Completed;
                WorkTransaction.Insert(workTransaction, connection);                
            }
        }

        #endregion

        #region UndoCompleteWork

        private static void UndoCompleteWork(Work work)
        {
            WorkTransaction completeWork
                = WorkTransaction.FindBy(work, WorkTransactionTypeEnum.Completed);

            WorkTransaction.Delete(completeWork);

            work.WorkStatus = WorkStatusEnum.StartDayDone;
            work.EndDayDate = null;
            Update(work);
        }

        #endregion

        #region FindNotSentWorks

        private const string SqlFindNotSentWorks =
            @"SELECT *
            FROM Work
                WHERE IsSentToServman = 0
                    and WorkStatusId != 3
                ORDER BY CreateDate";


        public static List<Work> FindNotSentWorks()
        {
            List<Work> workList = new List<Work>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindNotSentWorks))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        workList.Add(Load(dataReader));
                    }
                }
            }
            return workList;
            
        }

        #endregion

        #region AddVisit

        public WorkDetail AddVisit(Visit visit, WorkDetail workDetailTemplate)
        {
            visit.VisitStatus = VisitStatusEnum.Assigned;
            Visit.Update(visit);

            workDetailTemplate.WorkId = ID;
            workDetailTemplate.VisitId = visit.ID;
            WorkDetail.InsertAndLog(workDetailTemplate);
            return workDetailTemplate;
        }

        #endregion

        #region FindWorkWrappers

        public static BindingList<WorkWrapper> FindWorkWrappers(int? exactWorkId,
            string workId, int? technicianId, string dispatch, string van, 
            WorkStatusEnum? status, DateRange dateRange)
        {
            string SqlFindWorkWrappers =
                    @"SELECT * FROM work w
                        inner join employee d on d.ID = w.DispatchEmployeeId
                        inner join employee t on t.ID = w.TechnicianEmployeeId
                        left join van v on v.ID = w.VanId
                where";

            if (exactWorkId != null)
                SqlFindWorkWrappers += " w.ID = ?WorkId and";
            else
            {
                if (workId != null && workId != string.Empty)
                    SqlFindWorkWrappers += " w.ID like ?WorkId and";
                if (dispatch != null && dispatch != string.Empty)
                    SqlFindWorkWrappers += " (d.FirstName like ?Dispatch or d.LastName like ?Dispatch) and";
                if (technicianId != null)
                    SqlFindWorkWrappers += " t.ID = ?TechnicianId and";
                if (van != null && van != string.Empty)
                    SqlFindWorkWrappers += " v.LicensePlateNumber like ?Van and";
                if (status != null)
                    SqlFindWorkWrappers += " w.WorkStatusId = ?WorkStatusId and";
                if (dateRange != null)
                {
                    if (dateRange.StartDate.HasValue)
                        SqlFindWorkWrappers += " DATE(w.StartDate) >= ?DateStart and";
                    if (dateRange.EndDate.HasValue)
                        SqlFindWorkWrappers += " DATE(w.StartDate) <= ?DateEnd and";
                }                
            }
            SqlFindWorkWrappers += " 1=1 order by w.ID limit 200";

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindWorkWrappers))
            {
                if (exactWorkId != null)
                    Database.PutParameter(dbCommand, "?WorkId", exactWorkId);
                else
                {
                    if (workId != null)
                        Database.PutParameter(dbCommand, "?WorkId", workId + "%");
                    if (dispatch != null && dispatch != string.Empty)
                        Database.PutParameter(dbCommand, "?Dispatch", dispatch + "%");
                    if (technicianId != null)
                        Database.PutParameter(dbCommand, "?TechnicianId", technicianId.Value);
                    if (van != null && van != string.Empty)
                        Database.PutParameter(dbCommand, "?Van", van + "%");
                    if (status != null)
                        Database.PutParameter(dbCommand, "?WorkStatusId", (int)status);
                    if (dateRange != null)
                    {
                        if (dateRange.StartDate.HasValue)
                            Database.PutParameter(dbCommand, "?DateStart", dateRange.StartDate.Value.Date);
                        if (dateRange.EndDate.HasValue)
                            Database.PutParameter(dbCommand, "?DateEnd", dateRange.EndDate.Value.Date);
                    }                    
                } 

                BindingList<WorkWrapper> wrappers = new BindingList<WorkWrapper>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Work work = Load(dataReader);
                        Employee dispatchObj = Employee.Load(dataReader, FieldsCount);
                        Employee technicianObj = Employee.Load(dataReader, FieldsCount + Employee.FieldsCount);

                        int vanOffset = FieldsCount + 2 * Employee.FieldsCount;
                        Van vanObj = null;
                        if (!dataReader.IsDBNull(vanOffset))
                            vanObj = Van.Load(dataReader, vanOffset);

                        wrappers.Add(new WorkWrapper(work, dispatchObj, technicianObj, vanObj));
                    }

                }
                return wrappers;
            }

        }

        #endregion

        #region GetWorkUndoAllowance

        public static WorkUndoAllwanceEnum GetWorkUndoAllowance(Work work)
        {
            if (work.WorkStatus == WorkStatusEnum.StartDayDone)
            {
                if (WorkDetail.FindByWorkProcessed(work).Count > 0)
                    return WorkUndoAllwanceEnum.NotAllowedProcessedVisitsExist;
            }

            return WorkUndoAllwanceEnum.Allowed;
        }

        #endregion

        #region UndoLastOperation

        public static void UndoLastOperation(Work work)
        {
            if (work.WorkStatus == WorkStatusEnum.Completed)
                UndoCompleteWork(work);
            else if (work.WorkStatus == WorkStatusEnum.StartDayDone)
                StartDayDonePackage.UndoSaveStartDayDone(work);
            else if (work.WorkStatus == WorkStatusEnum.ReadyForStartDay)
                WorkPackage.UndoMoveWorkToReadyForStartDay(work);
        }

        #endregion

        #region FindDayClosedAmount

        private const string SqlFindDayClosedAmount =
            @"SELECT sum(ClosedDollarAmount) FROM Work
                where DATE(StartDate) = ?WorkDate";

        public static decimal FindDayClosedAmount(DateTime workDate)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindDayClosedAmount))
            {
                Database.PutParameter(dbCommand, "?WorkDate", workDate.Date);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        if (!dataReader.IsDBNull(0))
                            return dataReader.GetDecimal(0);
                    }
                }
            }

            return decimal.Zero;
        }

        #endregion

        #region FindDepartmentClosedAmounts

        private const string SqlFindRugCleaningAmount =
            @"SELECT sum(t.ClosedAmount) FROM WorkTransactionTask wtt
                inner join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
                inner join Work w on w.ID = wt.WorkId
                inner join Task t on t.ID = wtt.TaskId
                inner join Project p on p.ID = t.ProjectId
                where (p.ProjectTypeId = 1 or (p.ProjectTypeId = 3 and t.IsRugCleaningDepartment = 1))
                and WorkTransactionTaskActionId = 1 and Date(w.StartDate) = ?WorkDate";

        public static DepartmentClosedAmounts FindDepartmentClosedAmounts(DateTime workDate)
        {
            DepartmentClosedAmounts result = new DepartmentClosedAmounts();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindRugCleaningAmount))
            {
                Database.PutParameter(dbCommand, "?WorkDate", workDate.Date);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        result.RugCleaningAmount = dataReader.IsDBNull(0) ? 0 : dataReader.GetDecimal(0);
                }
            }

            result.RestorationAmount = FindDayClosedAmount(workDate) - result.RugCleaningAmount;           
            return result;
        }

        #endregion
    }

    public class DepartmentClosedAmounts
    {
        private decimal m_restorationAmount;
        private decimal m_rugCleaningAmount;

        public decimal RestorationAmount
        {
            get { return m_restorationAmount; }
            set { m_restorationAmount = value; }
        }

        public decimal RugCleaningAmount
        {
            get { return m_rugCleaningAmount; }
            set { m_rugCleaningAmount = value; }
        }
    }

    public class WorkWrapper
    {
        private Work m_work;
        private Employee m_technician;
        private Employee m_dispatch;
        private Van m_van;
        private BindingList<VisitWrapper> m_visits;

        #region Constructor

        public WorkWrapper(Work work, Employee dispatch, Employee technician, Van van)
        {
            m_work = work;
            m_technician = technician;
            m_dispatch = dispatch;
            m_van = van;
        }

        #endregion

        #region Work

        public Work Work
        {
            get { return m_work; }
        }

        #endregion        

        #region Technician

        public Employee Technician
        {
            get { return m_technician; }
        }

        #endregion

        #region Dispatch

        public Employee Dispatch
        {
            get { return m_dispatch; }
        }

        #endregion

        #region Van

        public Van Van
        {
            get { return m_van; }
        }

        #endregion

        #region Visits

        public BindingList<VisitWrapper> Visits
        {
            get { return m_visits; }
            set { m_visits = value; }
        }

        #endregion

        #region ID

        public int ID
        {
            get { return m_work.ID; }
        }

        #endregion

        #region WorkStatus

        public WorkStatusEnum WorkStatus
        {
            get { return m_work.WorkStatus; }
        }

        #endregion

        #region WorkStatusText

        public string WorkStatusText
        {
            get { return m_work.WorkStatusUserFriendlyText; }
        }

        #endregion        

        #region TechnicianName

        public string TechnicianName
        {
            get { return m_technician.DisplayName; }
        }

        #endregion

        #region DispatchName

        public string DispatchName
        {
            get { return m_dispatch.DisplayName; }
        }

        #endregion

        #region VanName

        public string VanName
        {
            get
            {
                if (m_van != null)
                    return m_van.LicensePlateNumber;
                return string.Empty;
            }
        }

        #endregion

        #region StartDate

        public DateTime? StartDate
        {
            get { return m_work.StartDate; }
        }

        #endregion

        #region StartMessage

        public string StartMessage
        {
            get { return m_work.StartMessage; }            
        }

        #endregion

        #region EquipmentNotes

        public string EquipmentNotes
        {
            get { return m_work.EquipmentNotes; }
        }

        #endregion
    }

}
      