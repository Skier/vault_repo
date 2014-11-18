using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Domain
{
    public enum CollisionErrorEnum
    {
        ProcessedVisit,
        StartDay,
        EndDay,
        NoCollision
    }

    public class WorkDetailOccupiedTime
    {
        #region WorkDetailId

        private int m_workDetailId;
        public int WorkDetailId
        {
            get { return m_workDetailId; }
            set { m_workDetailId = value; }
        }

        #endregion

        #region TimeBegin

        private DateTime m_timeBegin;
        public DateTime TimeBegin
        {
            get { return m_timeBegin; }
            set { m_timeBegin = value; }
        }

        #endregion

        #region TimeEnd

        private DateTime? m_timeEnd;
        public DateTime? TimeEnd
        {
            get { return m_timeEnd; }
            set { m_timeEnd = value; }
        }

        #endregion
    }

    public partial class WorkDetail 
    {
        public WorkDetail(){ }

        #region Visit

        private Visit m_visit;
        public Visit Visit
        {
            get { return m_visit; }
            set { m_visit = value; }
        }

        #endregion

        #region FindBy Work

        private const string SqlFindByWork =
            @"SELECT *
            FROM WorkDetail
                WHERE WorkId = ?WorkId";

        public static List<WorkDetail> FindBy(Work work, IDbConnection connection)
        {
            List<WorkDetail> workDetails = new List<WorkDetail>();
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWork, connection))
            {
                Database.PutParameter(dbCommand, "?WorkId", work.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        workDetails.Add(Load(dataReader));
                    }
                }
            }
            return workDetails;
        }

        public static List<WorkDetail> FindBy(Work work)
        {
            return FindBy(work, null);
        }

        #endregion

        #region FindByWorkAssigned

        private const string SqlFindByWorkAssigned =
            @"SELECT wd.* FROM WorkDetail wd
                inner join Visit v on wd.VisitId = v.ID
                WHERE v.VisitStatusId = 3 and WorkId = ?WorkId";

        public static List<WorkDetail> FindByWorkAssigned(Work work)
        {
            List<WorkDetail> workDetails = new List<WorkDetail>();
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWorkAssigned))
            {
                Database.PutParameter(dbCommand, "?WorkId", work.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        workDetails.Add(Load(dataReader));
                    }
                }
            }
            return workDetails;
        }

        #endregion

        #region FindByWorkProcessed

        private const string SqlFindByWorkProcessed =
            @"SELECT * FROM WorkDetail wd
                inner join Visit v on wd.VisitId = v.ID
                WHERE v.VisitStatusId != 1 and v.VisitStatusId != 3  and WorkId = ?WorkId";

        public static List<WorkDetail> FindByWorkProcessed(Work work)
        {
            List<WorkDetail> workDetails = new List<WorkDetail>();
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWorkProcessed))
            {
                Database.PutParameter(dbCommand, "?WorkId", work.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        WorkDetail detail = Load(dataReader);
                        detail.Visit = Visit.Load(dataReader, FieldsCount);
                        workDetails.Add(detail);
                    }
                }
            }
            return workDetails;
        }

        #endregion

        #region FindBy Work and Visit

        private const string SqlFindByWorkAndVisit =
            @"SELECT *
            FROM WorkDetail
                WHERE WorkId = ?WorkId
                    and VisitId = ?VisitId";

        public static WorkDetail FindByWorkAndVisit(Work work, Visit visit, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWorkAndVisit, connection))
            {
                Database.PutParameter(dbCommand, "?WorkId", work.ID);
                Database.PutParameter(dbCommand, "?VisitId", visit.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }
            throw new DataNotFoundException("FindBy Work and Visit - WorkDetail not found");
        }

        #endregion

        #region Exists

        public static bool Exists(Work work, Visit visit, IDbConnection connection)
        {
            try
            {
                FindByWorkAndVisit(work, visit, connection);
                return true;
            }
            catch (DataNotFoundException)
            {
                return false;
            }
        }

        #endregion

        #region FindBy Visit

        private const string SqlFindByVisit =
            @"SELECT *
            FROM WorkDetail
                WHERE VisitId = ?VisitId";

        public static WorkDetail FindByVisit(Visit visit)
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
            throw new DataNotFoundException("FindBy Work and Visit - WorkDetail not found");
        }

        #endregion

        #region GetSortedWorkDetails

//        private static List<WorkDetail> GetSortedWorkDetails(Work work, IDbConnection connection)
//        {
//            List<WorkDetail> workDetails = FindBy(work, connection);
//            workDetails.Sort(delegate(WorkDetail x, WorkDetail y)
//                             {
//                                 return x.m_timeBegin.CompareTo(y.m_timeBegin);                     
//                             });
//
//            return workDetails;
//        }

        #endregion

        #region SetVisitStartTime
        /// <summary>
        /// And also adjest another visits to avoid collision
        /// </summary>
//        public static void SetVisitStartTime(WorkDetail workDetail, DateTime time, IDbConnection connection)
//        {
//            List<WorkDetail> workDetails = GetSortedWorkDetails(new Work(workDetail.WorkId), connection);
//
//            //Find first visit in sequence to be shifted
//            int? firstVisitToBeShifted = null;
//            for (int i = 0; i < workDetails.Count; i++)
//            {
//                if (workDetails[i].ID != workDetail.ID
//                    && (workDetails[i].TimeBegin > time
//                    || (workDetails[i].TimeBegin <= time && workDetails[i].TimeEnd > time)))
//                {
//                    firstVisitToBeShifted = i;
//                    break;
//                }
//            }
//
//            TimeSpan currentVisitDuration = workDetail.TimeEnd - workDetail.TimeBegin;
//            workDetail.TimeBegin = time;
//            workDetail.TimeEnd = time + currentVisitDuration;
//            Update(workDetail, connection);
//
//            if (firstVisitToBeShifted.HasValue 
//                && workDetail.TimeEnd > workDetails[firstVisitToBeShifted.Value].TimeBegin)
//            {
//                TimeSpan shiftValue = workDetail.TimeEnd - workDetails[firstVisitToBeShifted.Value].TimeBegin;
//
//                for (int i = firstVisitToBeShifted.Value; i < workDetails.Count; i++)
//                {
//                    if (workDetails[i].ID != workDetail.ID)
//                    {
//                        workDetails[i].TimeBegin += shiftValue;
//                        workDetails[i].TimeEnd += shiftValue;
//                        Update(workDetails[i], connection);                        
//                    }
//                }                
//            }
//        }

        #endregion

        #region ChangeVisitDuration
        /// <summary>
        /// And also adjest another visits to avoid collision
        /// Returns true if visits in queue are shifted, otherwise false
        /// </summary>
//        public static bool ChangeVisitDuration(WorkDetail workDetail, TimeSpan duration, IDbConnection connection)
//        {
//            List<WorkDetail> workDetails = GetSortedWorkDetails(new Work(workDetail.WorkId), connection);
//            workDetail.TimeEnd = workDetail.TimeBegin + duration;
//            Update(workDetail, connection);
//
//            //Find first visit in sequence to be shifted
//            int? firstVisitToBeShifted = null;
//            TimeSpan? shiftValue = null;
//            for (int i = 0; i < workDetails.Count; i++)
//            {
//                if (workDetails[i].ID != workDetail.ID
//                    && workDetails[i].TimeBegin > workDetail.TimeBegin)                    
//                {
//                    firstVisitToBeShifted = i;
//                    shiftValue = workDetail.TimeEnd - workDetails[i].TimeBegin;
//                    break;
//                }
//            }
//
//            if (firstVisitToBeShifted.HasValue)
//            {
//                for (int i = firstVisitToBeShifted.Value; i < workDetails.Count; i++)
//                {
//                    if (workDetails[i].ID != workDetail.ID)
//                    {
//                        workDetails[i].TimeBegin += shiftValue.Value;
//                        workDetails[i].TimeEnd += shiftValue.Value;
//                        Update(workDetails[i], connection);
//                    }
//                }
//            }
//
//            return firstVisitToBeShifted.HasValue;
//        }

        #endregion

        #region IsExistCollision

        //Checks collision with processed vitits, start day, end day
        public static CollisionErrorEnum IsExistCollision(WorkDetail workDetail, DateTime time)
        {
            return IsExistCollision(workDetail, time, null);
        }

        public static CollisionErrorEnum IsExistCollision(WorkDetail workDetail, DateTime startTime, DateTime endTime)
        {
            return IsExistCollision(workDetail, startTime, (DateTime?)endTime);
        }

        private static CollisionErrorEnum IsExistCollision(WorkDetail workDetail, DateTime startTime, DateTime? endTime)
        {
            Work work = Work.FindByPrimaryKey(workDetail.WorkId);
            List<WorkDetail> workDetails = FindByWorkProcessed(work);

            List<WorkDetailOccupiedTime> occupiedDetails = new List<WorkDetailOccupiedTime>();
            foreach (WorkDetail detail in workDetails)
            {                
                WorkDetailOccupiedTime occupiedTime = new WorkDetailOccupiedTime();
                occupiedTime.WorkDetailId = detail.ID;

                if (detail.Visit.VisitStatus == VisitStatusEnum.AssignedForExecution)
                    occupiedTime.TimeBegin = detail.TimeDispatch.Value;
                else if (detail.Visit.VisitStatus == VisitStatusEnum.Arrived)
                {
                    occupiedTime.TimeBegin = detail.TimeBegin;
                    occupiedTime.TimeEnd = detail.TimeArrive;
                }
                else if (detail.Visit.VisitStatus == VisitStatusEnum.Completed)
                {
                    occupiedTime.TimeBegin = detail.TimeBegin;
                    occupiedTime.TimeEnd = detail.TimeComplete;                    
                } else
                {
                    occupiedTime.TimeBegin = detail.TimeBegin;
                    occupiedTime.TimeEnd = detail.TimeEnd;                                        
                }
                
                occupiedDetails.Add(occupiedTime);
            }

            foreach (WorkDetailOccupiedTime detail in occupiedDetails)
            {
                if (detail.WorkDetailId == workDetail.ID)
                    continue;

                if (startTime == detail.TimeBegin)
                    return CollisionErrorEnum.ProcessedVisit;

                if (startTime > detail.TimeBegin && startTime < detail.TimeEnd)
                    return CollisionErrorEnum.ProcessedVisit;

                if (!endTime.HasValue)
                    continue;

                if (startTime < detail.TimeBegin && endTime > detail.TimeBegin)
                    return CollisionErrorEnum.ProcessedVisit;

                if (endTime < detail.TimeBegin && endTime > detail.TimeBegin)
                    return CollisionErrorEnum.ProcessedVisit;

                if (detail.TimeEnd.HasValue && endTime == detail.TimeEnd)
                    return CollisionErrorEnum.ProcessedVisit;

                if (endTime.Value > detail.TimeBegin && endTime.Value < detail.TimeEnd)
                    return CollisionErrorEnum.ProcessedVisit;

                if (startTime <= detail.TimeBegin && endTime >= detail.TimeEnd)
                    return CollisionErrorEnum.ProcessedVisit;

                if (startTime >= detail.TimeBegin && endTime <= detail.TimeEnd)
                    return CollisionErrorEnum.ProcessedVisit;
            }

            if (work.StartDayDate.HasValue && startTime < work.StartDayDate.Value)
                return CollisionErrorEnum.StartDay;

            if (endTime.HasValue && work.StartDayDate.HasValue && endTime.Value < work.StartDayDate.Value)
                return CollisionErrorEnum.StartDay;

            if (work.EndDayDate.HasValue && startTime > work.EndDayDate.Value)
                return CollisionErrorEnum.EndDay;

            if (endTime.HasValue && work.EndDayDate.HasValue && endTime.Value > work.EndDayDate.Value)
                return CollisionErrorEnum.EndDay;

            return CollisionErrorEnum.NoCollision;
        }


        #endregion

        #region DeleteBy Visit

        private const string SqlDeleteByVisit =
            @"DELETE
            FROM WorkDetail
                WHERE VisitId = ?VisitId";

        public static void DeleteBy(Visit visit)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteByVisit))
            {
                Database.PutParameter(dbCommand, "?VisitId", visit.ID);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion

        #region UpdateAndLog

        public static void UpdateAndLog(WorkDetail workDetail)
        {
            UpdateAndLog(workDetail, null);
        }

        public static void UpdateAndLog(WorkDetail workDetail, IDbConnection connection)
        {
            Update(workDetail, connection);
            WorkDetailLog.Insert(workDetail, connection);
        }

        #endregion 

        #region InsertAndLog

        public static void InsertAndLog (WorkDetail workDetail)
        {
            InsertAndLog(workDetail, null);
        }

        public static void InsertAndLog (WorkDetail workDetail, IDbConnection connection)
        {
            Insert(workDetail, connection);
            WorkDetailLog.Insert(workDetail, connection);
        }

        public static void InsertAndLog(List<WorkDetail>  workDetailList)
        {
            InsertAndLog(workDetailList, null);
        }

        public static void InsertAndLog(List<WorkDetail>  workDetailList, IDbConnection connection)
        {
            foreach (WorkDetail workDetail in workDetailList)
            {
                InsertAndLog(workDetail, connection);
            }
        }

        #endregion 
    }
}
      