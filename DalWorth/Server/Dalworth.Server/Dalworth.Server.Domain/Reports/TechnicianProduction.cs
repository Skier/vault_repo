using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain.Reports
{
    public class TechnicianProduction
    {
        #region Constructor

        public TechnicianProduction(DateTime date, bool isWorkExist, int? floodClosedQty, int? helpClosedQty, 
            int? floodCancelledQty, decimal? restorationMiscAmount, int? monitoringClosedQty, 
            decimal? restorationDepartmentAmount, int? rugPickupQty, decimal? rugPickupEstimatedAmount, 
            decimal? rugPickupOptionsAmount, int? rugDeliveryClosedQty, decimal? rugCleaningDepartmentAmount, 
            decimal? totalAmount, decimal? workingHours)
        {
            m_date = date;
            m_isWorkExist = isWorkExist;
            m_floodClosedQty = floodClosedQty;
            m_helpClosedQty = helpClosedQty;
            m_floodCancelledQty = floodCancelledQty;
            m_restorationMiscAmount = restorationMiscAmount;
            m_monitoringClosedQty = monitoringClosedQty;
            m_restorationDepartmentAmount = restorationDepartmentAmount;
            m_rugPickupQty = rugPickupQty;
            m_rugPickupEstimatedAmount = rugPickupEstimatedAmount;
            m_rugPickupOptionsAmount = rugPickupOptionsAmount;
            m_rugDeliveryClosedQty = rugDeliveryClosedQty;
            m_rugCleaningDepartmentAmount = rugCleaningDepartmentAmount;
            m_totalAmount = totalAmount;
            m_workingHours = workingHours;
        }

        #endregion


        #region Date

        private DateTime m_date;
        public DateTime Date
        {
            get { return m_date; }
            set { m_date = value; }
        }

        #endregion

        #region IsWorkExist

        private bool m_isWorkExist;
        public bool IsWorkExist
        {
            get { return m_isWorkExist; }
            set { m_isWorkExist = value; }
        }

        #endregion

        #region FloodClosedQty

        private int? m_floodClosedQty;
        public int? FloodClosedQty
        {
            get { return m_floodClosedQty; }
            set { m_floodClosedQty = value; }
        }

        #endregion

        #region HelpClosedQty

        private int? m_helpClosedQty;
        public int? HelpClosedQty
        {
            get { return m_helpClosedQty; }
            set { m_helpClosedQty = value; }
        }

        #endregion

        #region FloodCancelledQty

        private int? m_floodCancelledQty;
        public int? FloodCancelledQty
        {
            get { return m_floodCancelledQty; }
            set { m_floodCancelledQty = value; }
        }

        #endregion

        #region RestorationMiscAmount

        private decimal? m_restorationMiscAmount;
        public decimal? RestorationMiscAmount
        {
            get { return m_restorationMiscAmount; }
            set { m_restorationMiscAmount = value; }
        }

        #endregion

        #region MonitoringClosedQty

        private int? m_monitoringClosedQty;
        public int? MonitoringClosedQty
        {
            get { return m_monitoringClosedQty; }
            set { m_monitoringClosedQty = value; }
        }

        #endregion

        #region RestorationDepartmentAmount

        private decimal? m_restorationDepartmentAmount;
        public decimal? RestorationDepartmentAmount
        {
            get { return m_restorationDepartmentAmount; }
            set { m_restorationDepartmentAmount = value; }
        }

        #endregion

        #region RugPickupQty

        private int? m_rugPickupQty;
        public int? RugPickupQty
        {
            get { return m_rugPickupQty; }
            set { m_rugPickupQty = value; }
        }

        #endregion

        #region RugPickupEstimatedAmount

        private decimal? m_rugPickupEstimatedAmount;
        public decimal? RugPickupEstimatedAmount
        {
            get { return m_rugPickupEstimatedAmount; }
            set { m_rugPickupEstimatedAmount = value; }
        }

        #endregion

        #region RugPickupOptionsAmount

        private decimal? m_rugPickupOptionsAmount;
        public decimal? RugPickupOptionsAmount
        {
            get { return m_rugPickupOptionsAmount; }
            set { m_rugPickupOptionsAmount = value; }
        }

        #endregion

        #region RugDeliveryClosedQty

        private int? m_rugDeliveryClosedQty;
        public int? RugDeliveryClosedQty
        {
            get { return m_rugDeliveryClosedQty; }
            set { m_rugDeliveryClosedQty = value; }
        }

        #endregion

        #region RugCleaningDepartmentAmount

        private decimal? m_rugCleaningDepartmentAmount;
        public decimal? RugCleaningDepartmentAmount
        {
            get { return m_rugCleaningDepartmentAmount; }
            set { m_rugCleaningDepartmentAmount = value; }
        }

        #endregion

        #region TotalAmount

        private decimal? m_totalAmount;
        public decimal? TotalAmount
        {
            get { return m_totalAmount; }
            set { m_totalAmount = value; }
        }

        #endregion

        #region WorkingHours

        private decimal? m_workingHours;
        public decimal? WorkingHours
        {
            get { return m_workingHours; }
            set { m_workingHours = value; }
        }

        #endregion

        #region Find

        private const string SqlFind =
            @"call GetIncrementedDates(?StartDate, ?EndDate);

            select idl.ID,
            COALESCE(IsWorkExistValue, 0) as ValueIsWorkExist,
            COALESCE(DefloodClosedQty, 0) as QtyDefloodClosed,
            COALESCE(HelpCompletedCount, 0) as QtyHelpCompleted,
            COALESCE(DefloodCancelledCount, 0) as QtyDefloodCancelled,
            COALESCE(RelayCleanAmt, 0) as AmtRelayClean,
            COALESCE(MonitorCompletedCount, 0) as QtyMonitorCompleted,
            COALESCE(RestClosedAmt, 0) as AmtRestClosed,
            COALESCE(PickupCompletedCount, 0) as QtyPickupCompleted,
            COALESCE(PickupAmount, 0) as AmtPickup,
            COALESCE(OversaleAmount, 0) as AmtOversale,
            COALESCE(DeliveryCompletedQty, 0) as QtyDeliveryCompleted,
            COALESCE(CleaningClosedAmtount, 0) as AmtCleaningClosed,
            COALESCE(RestClosedAmt, 0) + COALESCE(CleaningClosedAmtount, 0) as AmtDayClosed,
            0 as WorkingHours

            from  TmpIncrementedDateList idl

            left join (
                select Date(w.StartDate) as IsWorkExistDate, 1 as IsWorkExistValue from Work w
                    where TechnicianEmployeeId = ?TechnicianId
                ) IsWorkExist on IsWorkExist.IsWorkExistDate = idl.ID

            left join (
                select Date(w.StartDate) as DefloodClosedQtyDate, count(FirstProcessFlood.TaskId) as DefloodClosedQty from
                    (SELECT wtt.TaskId, min(wtt.WorkTransactionId) WorkTransactionId FROM WorkTransactionTask wtt
                        inner join Task t on t.ID = wtt.TaskId
                        where t.TaskTypeId = 4 and (wtt.WorkTransactionTaskActionId = 1 or wtt.WorkTransactionTaskActionId = 2)
                    group by wtt.TaskId) FirstProcessFlood
                inner join WorkTransaction wt on wt.ID = FirstProcessFlood.WorkTransactionId
                inner join Work w on w.ID = wt.WorkId
                where w.TechnicianEmployeeId = ?TechnicianId
                group by Date(w.StartDate)) DefloodClosed on DefloodClosed.DefloodClosedQtyDate = idl.ID

            left join (
                SELECT Date(w.StartDate) as HelpCompletedCountDate, count(wtt.TaskId) as HelpCompletedCount FROM WorkTransactionTask wtt
                inner join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
                inner join Work w on w.ID = wt.WorkId
                inner join Task t on t.ID = wtt.TaskId
                where WorkTransactionTaskActionId = 1 and w.TechnicianEmployeeId = ?TechnicianId
                and t.TaskTypeId = 7 
                group by Date(w.StartDate)) HelpCompleted on HelpCompleted.HelpCompletedCountDate = idl.ID

            left join (
                SELECT Date(w.StartDate) as DefloodCancelledCountDate, count(wtt.TaskId) as DefloodCancelledCount FROM WorkTransactionTask wtt
                inner join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
                inner join Work w on w.ID = wt.WorkId
                inner join Task t on t.ID = wtt.TaskId
                where WorkTransactionTaskActionId = 5 and w.TechnicianEmployeeId = ?TechnicianId
                and t.TaskTypeId = 4
                group by Date(w.StartDate)) DefloodCancelled on DefloodCancelled.DefloodCancelledCountDate = idl.ID

            left join (
                SELECT Date(w.StartDate) as RelayCleanAmtDate, sum(t.ClosedAmount) as RelayCleanAmt FROM WorkTransactionTask wtt
                inner join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
                inner join Work w on w.ID = wt.WorkId
                inner join Task t on t.ID = wtt.TaskId
                inner join Project p on p.ID = t.ProjectId
                where t.TaskTypeId = 6 and w.TechnicianEmployeeId = ?TechnicianId
                and (p.ProjectTypeId = 2 or (p.ProjectTypeId = 3 and t.IsRugCleaningDepartment = 0))
                and WorkTransactionTaskActionId = 1
                group by Date(w.StartDate)) RelayClean on RelayClean.RelayCleanAmtDate = idl.ID

            left join (
                SELECT Date(w.StartDate) as MonitorCompletedDate, count(wtt.TaskId) as MonitorCompletedCount FROM WorkTransactionTask wtt
                inner join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
                inner join Work w on w.ID = wt.WorkId
                inner join Task t on t.ID = wtt.TaskId
                where WorkTransactionTaskActionId = 1 and w.TechnicianEmployeeId = ?TechnicianId
                and t.ID <> t.ParentTaskId + 1
                and t.TaskTypeId = 5
                group by Date(w.StartDate)) MonitorCompleted on MonitorCompleted.MonitorCompletedDate = idl.ID

            left join (
                select ClosedTotalDate as RestClosedAmtDate, COALESCE(ClosedTotalAmt, 0) - COALESCE(ClosedRugAmt, 0) as RestClosedAmt
                from (SELECT Date(w2.StartDate) as ClosedTotalDate, sum(w2.ClosedDollarAmount) as ClosedTotalAmt FROM Work w2
                  where w2.TechnicianEmployeeId = ?TechnicianId
                group by Date(w2.StartDate)) TotalClosedTbl
                left join
                    (SELECT Date(w.StartDate) as ClosedRugDate, sum(t.ClosedAmount) as ClosedRugAmt FROM WorkTransactionTask wtt
                    inner join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
                    inner join Work w on w.ID = wt.WorkId
                    inner join Task t on t.ID = wtt.TaskId
                    inner join Project p on p.ID = t.ProjectId
                    where (p.ProjectTypeId = 1 or (p.ProjectTypeId = 3 and t.IsRugCleaningDepartment = 1))
                    and WorkTransactionTaskActionId = 1 and w.TechnicianEmployeeId = ?TechnicianId
                    group by Date(w.StartDate)) RugClosedTbl on RugClosedTbl.ClosedRugDate = TotalClosedTbl.ClosedTotalDate
                ) RestClosed on RestClosed.RestClosedAmtDate = idl.ID

            left join (
                SELECT Date(w.StartDate) as PickupCompletedDate, count(wtt.TaskId) as PickupCompletedCount FROM WorkTransactionTask wtt
                inner join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
                inner join Work w on w.ID = wt.WorkId
                inner join Task t on t.ID = wtt.TaskId
                inner join Project p on p.ID = t.ProjectId
                where WorkTransactionTaskActionId = 1 and w.TechnicianEmployeeId = ?TechnicianId
                and t.TaskTypeId = 1 and p.ProjectTypeId = 1
                group by Date(w.StartDate)) PickupCompleted on PickupCompleted.PickupCompletedDate = idl.ID

            left join (
                SELECT Date(w.StartDate) as PickupAmountDate, sum(t.EstimatedClosedAmount) as PickupAmount FROM WorkTransactionTask wtt
                inner join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
                inner join Work w on w.ID = wt.WorkId
                inner join Task t on t.ID = wtt.TaskId
                inner join Project p on p.ID = t.ProjectId
                where WorkTransactionTaskActionId = 1 and w.TechnicianEmployeeId = ?TechnicianId
                and t.TaskTypeId = 1 and p.ProjectTypeId = 1
                group by Date(w.StartDate)) PickupAmount on PickupAmount.PickupAmountDate = idl.ID

            left join (
                SELECT Date(w.StartDate) as OversaleAmountDate,
                    sum(i.ProtectorCost) +  sum(i.PaddingCost) + sum(i.MothRepelCost) + sum(i.RapCost) + sum(i.OtherCost) as OversaleAmount
                FROM WorkTransactionTask wtt
                inner join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
                inner join Work w on w.ID = wt.WorkId
                inner join Task t on t.ID = wtt.TaskId
                inner join Item i on i.TaskId = t.ID
                inner join Project p on p.ID = t.ProjectId
                where WorkTransactionTaskActionId = 1 and w.TechnicianEmployeeId = ?TechnicianId
                and t.TaskTypeId = 1 and p.ProjectTypeId = 1
                group by Date(w.StartDate)) Oversale on Oversale.OversaleAmountDate = idl.ID

            left join (
                SELECT Date(w.StartDate) as DeliveryCompletedDate, count(wtt.TaskId) as DeliveryCompletedQty FROM WorkTransactionTask wtt
                inner join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
                inner join Work w on w.ID = wt.WorkId
                inner join Task t on t.ID = wtt.TaskId
                inner join Project p on p.ID = t.ProjectId
                where WorkTransactionTaskActionId = 1
                and t.TaskTypeId = 2 and p.ProjectTypeId = 1 and w.TechnicianEmployeeId = ?TechnicianId
                group by Date(w.StartDate)) DeliveryCompleted on DeliveryCompleted.DeliveryCompletedDate = idl.ID

            left join (
                SELECT Date(w.StartDate) as CleaningClosedAmtountDate, sum(t.ClosedAmount) as CleaningClosedAmtount FROM WorkTransactionTask wtt
                inner join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
                inner join Work w on w.ID = wt.WorkId
                inner join Task t on t.ID = wtt.TaskId
                inner join Project p on p.ID = t.ProjectId
                where (p.ProjectTypeId = 1 or (p.ProjectTypeId = 3 and t.IsRugCleaningDepartment = 1))
                and WorkTransactionTaskActionId = 1 and w.TechnicianEmployeeId = ?TechnicianId
                group by Date(w.StartDate)) CleaningClosed on CleaningClosed.CleaningClosedAmtountDate = idl.ID;

            DROP TABLE IF EXISTS TmpIncrementedDateList;";

        public static List<TechnicianProduction> Find(Employee technician, DateTime startDate, DateTime endDate)
        {
            List<TechnicianProduction> result = new List<TechnicianProduction>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFind))
            {
                Database.PutParameter(dbCommand, "?TechnicianId", technician.ID);
                Database.PutParameter(dbCommand, "?StartDate", startDate.Date);
                Database.PutParameter(dbCommand, "?EndDate", endDate.Date);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        bool isWorkExist = dataReader.GetBoolean(1);

                        TechnicianProduction technicianProduction = new TechnicianProduction(
                            dataReader.GetDateTime(0),
                            isWorkExist,
                            !isWorkExist ? (int?)null : dataReader.GetInt32(2),
                            !isWorkExist ? (int?)null : dataReader.GetInt32(3),
                            !isWorkExist ? (int?)null : dataReader.GetInt32(4),
                            !isWorkExist ? (decimal?)null : dataReader.GetDecimal(5),
                            !isWorkExist ? (int?)null : dataReader.GetInt32(6),
                            !isWorkExist ? (decimal?)null : dataReader.GetDecimal(7),
                            !isWorkExist ? (int?)null : dataReader.GetInt32(8),
                            !isWorkExist ? (decimal?)null : dataReader.GetDecimal(9),
                            !isWorkExist ? (decimal?)null : dataReader.GetDecimal(10),
                            !isWorkExist ? (int?)null : dataReader.GetInt32(11),
                            !isWorkExist ? (decimal?)null : dataReader.GetDecimal(12),
                            !isWorkExist ? (decimal?)null : dataReader.GetDecimal(13),
                            !isWorkExist ? (decimal?)null : dataReader.GetDecimal(14));

                        result.Add(technicianProduction);
                    }
                }
            }

            return result;
        }

        #endregion
    }

    public class TechnicianProductionNotNull
    {
        private TechnicianProduction m_originalData;

        #region Constructor

        public TechnicianProductionNotNull(TechnicianProduction technicianProduction)
        {
            m_originalData = technicianProduction;
        }

        #endregion

        #region IsWorkExist
        
        public bool IsWorkExist
        {
            get { return m_originalData.IsWorkExist; }
        }

        #endregion

        #region Date

        public DateTime Date
        {
            get { return m_originalData.Date; }
        }

        #endregion        

        #region FloodClosedQty

        public int FloodClosedQty
        {
            get { return m_originalData.FloodClosedQty == null ? 0 : m_originalData.FloodClosedQty.Value; }
        }

        #endregion

        #region HelpClosedQty

        public int HelpClosedQty
        {
            get { return m_originalData.HelpClosedQty == null ? 0 : m_originalData.HelpClosedQty.Value; }
        }

        #endregion

        #region FloodCancelledQty

        public int FloodCancelledQty
        {
            get { return m_originalData.FloodCancelledQty == null ? 0 : m_originalData.FloodCancelledQty.Value; }
        }

        #endregion

        #region RestorationMiscAmount

        public decimal RestorationMiscAmount
        {
            get { return m_originalData.RestorationMiscAmount == null ? decimal.Zero : m_originalData.RestorationMiscAmount.Value; }
        }

        #endregion

        #region MonitoringClosedQty

        public int MonitoringClosedQty
        {
            get { return m_originalData.MonitoringClosedQty == null ? 0 : m_originalData.MonitoringClosedQty.Value; }
        }

        #endregion

        #region RestorationDepartmentAmount

        public decimal RestorationDepartmentAmount
        {
            get { return m_originalData.RestorationDepartmentAmount == null ? decimal.Zero : m_originalData.RestorationDepartmentAmount.Value; }
        }

        #endregion

        #region RugPickupQty
        
        public int RugPickupQty
        {
            get { return m_originalData.RugPickupQty == null ? 0 : m_originalData.RugPickupQty.Value; }
        }

        #endregion

        #region RugPickupEstimatedAmount

        public decimal RugPickupEstimatedAmount
        {
            get { return m_originalData.RugPickupEstimatedAmount == null ? decimal.Zero : m_originalData.RugPickupEstimatedAmount.Value; }
        }

        #endregion

        #region RugPickupOptionsAmount

        public decimal RugPickupOptionsAmount
        {
            get { return m_originalData.RugPickupOptionsAmount == null ? decimal.Zero : m_originalData.RugPickupOptionsAmount.Value; }
        }

        #endregion

        #region RugDeliveryClosedQty
        
        public int RugDeliveryClosedQty
        {
            get { return m_originalData.RugDeliveryClosedQty == null ? 0 : m_originalData.RugDeliveryClosedQty.Value; }
        }

        #endregion

        #region RugCleaningDepartmentAmount

        public decimal RugCleaningDepartmentAmount
        {
            get { return m_originalData.RugCleaningDepartmentAmount == null ? decimal.Zero : m_originalData.RugCleaningDepartmentAmount.Value; }
        }

        #endregion

        #region TotalAmount

        public decimal TotalAmount
        {
            get { return m_originalData.TotalAmount == null ? decimal.Zero : m_originalData.TotalAmount.Value; }
        }

        #endregion

        #region WorkingHours

        public decimal WorkingHours
        {
            get { return m_originalData.WorkingHours == null ? decimal.Zero : m_originalData.WorkingHours.Value; }
        }

        #endregion        
    }
}
