using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SmartSchedule.Domain.WCF
{
    interface IWcfSubscriber
    {
        [OperationContract(IsOneWay = true)]
        void OnViewModelChanged(CallbackInfo info);

        [OperationContract(IsOneWay = true)]
        void OnOptimizationRequested(Schedule schedule);

        [OperationContract(IsOneWay = true)]
        void ForceSync(SyncTypeEnum syncType);
    }

    public class WcfSubscriberInfo
    {        
        private bool m_isBusy;
        private DateTime m_busyStartTime;
        private bool m_isAllDatesInBucket;

        #region WcfSubscriberInfo

        public WcfSubscriberInfo(WcfSubscriberTypeEnum type, DateTime scheduleDate, User user, bool isAllDatesInBucket)
        {
            m_type = type;
            m_scheduleDate = scheduleDate;
            m_user = user;
            m_isAllDatesInBucket = isAllDatesInBucket;
        }

        #endregion

        #region Type

        private WcfSubscriberTypeEnum m_type;
        public WcfSubscriberTypeEnum Type
        {
            get { return m_type; }
            set { m_type = value; }
        }

        #endregion

        #region ScheduleDate

        private DateTime m_scheduleDate;
        public DateTime ScheduleDate
        {
            get { return m_scheduleDate; }
            set { m_scheduleDate = value; }
        }

        #endregion

        #region SetBusy

        public void SetBusy(DateTime scheduleDate)
        {
            m_isBusy = true;
            m_busyStartTime = DateTime.Now;
            m_scheduleDate = scheduleDate.Date;
        }

        #endregion

        #region SetIdle

        public void SetIdle()
        {
            m_isBusy = false;
        }

        #endregion

        #region IsBusy

        public bool IsBusy
        {
            get { return m_isBusy; }
        }

        #endregion

        #region IsNotResponding

        public bool IsNotResponding
        {
            get { return m_isBusy && DateTime.Now.Subtract(m_busyStartTime).TotalMinutes > 20; }
        }

        #endregion

        #region IsAllDatesInBucket

        public bool IsAllDatesInBucket
        {
            get { return m_isAllDatesInBucket; }
            set { m_isAllDatesInBucket = value; }
        }

        #endregion

        #region User

        private User m_user;
        public User User
        {
            get { return m_user; }
            set { m_user = value; }
        }

        #endregion
    }

    [DataContract]
    public enum WcfSubscriberTypeEnum
    {
        [EnumMember]
        Dashboard,
        [EnumMember]
        Service,
        [EnumMember]
        Optimizer,        
        [EnumMember]
        Sync
    }

    [DataContract]
    public enum CallbackType
    {
        [EnumMember]
        TechnicianWorkHours,
        [EnumMember]
        Technician,
        [EnumMember]
        Technicians,
        [EnumMember]
        VisitsFull,
        [EnumMember]
        Visit,
        [EnumMember]
        Visits,
        [EnumMember]
        Empty
    }

    [DataContract]
    public enum SyncTypeEnum
    {
        [EnumMember]
        Visits,
        [EnumMember]
        TechnicianSettings
    }

    [DataContract]
    public class CallbackInfo
    {
        private static Dictionary<DateTime, long> m_idMap = new Dictionary<DateTime, long>();

        private CallbackInfo(long id, DateTime date, CallbackType changeType)
        {
            Id = id;
            Date = date;
            ChangeType = changeType;
        }

        public static CallbackInfo Create(DateTime date, CallbackType changeType)
        {
            if (!m_idMap.ContainsKey(date.Date))
                m_idMap.Add(date.Date, 0);

            m_idMap[date.Date]++;
            return new CallbackInfo(m_idMap[date.Date], date.Date, changeType);
        }

        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public CallbackType ChangeType { get; set; }
        [DataMember]
        public TechnicianWorkHoursChangeDetail TechnicianWorkHoursChange { get; set; }
        [DataMember]
        public Technician TechnicianChange { get; set; }
        [DataMember]
        public List<Technician> TechniciansChange { get; set; }
        [DataMember]
        public VisitsFullChangeDetail VisitsFullChange { get; set; }
        [DataMember]
        public Visit VisitChange { get; set; }
        [DataMember]
        public VisitsChangeDetail VisitsChange { get; set; }

        public static long GetLastProcessedCallbackId(DateTime date)
        {
            if (!m_idMap.ContainsKey(date.Date))
                return 0;
            return m_idMap[date.Date];
        }

        public CallbackInfo Copy(CallbackType callbackType)
        {
            return new CallbackInfo(Id, Date, callbackType);
        }
    }

    [DataContract]
    public class TechnicianWorkHoursChangeDetail
    {
        public TechnicianWorkHoursChangeDetail(int technicianId, List<TechnicianWorkTime> workingIntervals)
        {
            TechnicianId = technicianId;
            WorkingIntervals = workingIntervals;
        }

        [DataMember]
        public int TechnicianId { get; set; }
        [DataMember]
        public List<TechnicianWorkTime> WorkingIntervals { get; set; }
    }

    [DataContract]
    public class VisitsFullChangeDetail
    {
        public VisitsFullChangeDetail(List<Visit> dashboardVisits, List<Visit> bucketVisits, 
            List<Visit> temporaryAssignedVisits, bool isRecommendationsSuspended,
            long lastProcessedCallbackId)
        {
            DashboardVisits = dashboardVisits;
            BucketVisits = bucketVisits;
            TemporaryAssignedVisits = temporaryAssignedVisits;
            IsRecommendationsSuspended = isRecommendationsSuspended;
            LastProcessedCallbackId = lastProcessedCallbackId;
        }

        [DataMember]
        public List<Visit> DashboardVisits { get; set; }
        [DataMember]
        public List<Visit> BucketVisits { get; set; }
        [DataMember]
        public List<Visit> TemporaryAssignedVisits { get; set; }
        [DataMember]
        public bool IsRecommendationsSuspended { get; set; }
        [DataMember]
        public long LastProcessedCallbackId { get; set; }
    }

    [DataContract]
    public class VisitsChangeDetail
    {
        public VisitsChangeDetail()
        {
            DashboardAddedVisits = new List<Visit>();
            DashboardRemovedVisits = new List<Visit>();
            DashboardModifiedVisits = new List<Visit>();
            IsOperationFailed = false;
        }

        [DataMember]
        public List<Visit> DashboardAddedVisits { get; set; }
        [DataMember]
        public List<Visit> DashboardRemovedVisits { get; set; }
        [DataMember]
        public List<Visit> DashboardModifiedVisits { get; set; }
        [DataMember]
        public List<Visit> BucketVisits { get; set; }
        [DataMember]
        public List<Visit> TemporaryAssignedVisits { get; set; }
        [DataMember]
        public bool IsOperationFailed { get; set; }

        public List<DateTime> AffectedDates { get; set; }

        public void AddAddedVisit(Visit visit)
        {
            if (!DashboardAddedVisits.Contains(visit))
                DashboardAddedVisits.Add(visit);
        }

        public void AddRemovedVisit(Visit visit)
        {
            if (!DashboardRemovedVisits.Contains(visit))
                DashboardRemovedVisits.Add(visit);
        }

        public void AddModifiedVisit(Visit visit)
        {
            if (!DashboardModifiedVisits.Contains(visit))
                DashboardModifiedVisits.Add(visit);
        }
    }
}
