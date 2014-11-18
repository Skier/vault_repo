using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using SmartSchedule.Domain.Sync;

namespace SmartSchedule.Domain.WCF
{
    [ServiceContract]
    public interface IWcfServiceWeb
    {
        [OperationContract(Name = "GetDefaultTechniciansWeb")]
        IEnumerable<Technician> GetDefaultTechnicians();

        [OperationContract]
        List<TechnicianDetail> GetTechnicianDetailsByServmanIdWeb(string technicianServmanId, bool defaultSettings);

        [OperationContract(Name = "GetTechnicianDetailsWeb")]
        List<TechnicianDetail> GetTechnicianDetails(int technicianDefaultId, bool defaultSettings);

        [OperationContract]
        List<TechnicianDetailValidationError> SaveTechnicianDetailWeb(List<TechnicianDetail> affectedTechnicianDetails,
            List<DateTime> removedDates, int defaultTechnicianId);

        [OperationContract]
        List<TechnicianDetailValidationError> ValidateTechnicianDetailsWeb(
            List<TechnicianDetail> affectedTechnicianDetails, List<DateTime> removedDates,
            int defaultTechnicianId, bool deepValidation);

        [OperationContract]
        List<RecommendationResponseItem> GetRecommendationsWeb(RecommendationRequest request);
    }

    [ServiceContract(CallbackContract = typeof(IWcfSubscriber))]
    public interface IWcfService
    {
        [OperationContract]
        VisitsFullChangeDetail GetFullViewInfo(DateTime date);

        [OperationContract]
        IEnumerable<Technician> GetDefaultTechnicians();

        [OperationContract]
        IEnumerable<Technician> GetTechnicians(DateTime date, bool defaultSettings, bool eliminateDetails);

        [OperationContract]
        void SaveTechnicianArrangement(List<Technician> orderedTechnicians, bool defaultSettings);

        [OperationContract]
        List<TechnicianDetail> GetTechnicianDetails(int technicianDefaultId, bool defaultSettings);

        [OperationContract]
        List<TechnicianDetailValidationError> SaveTechnicianDetail(List<TechnicianDetail> affectedTechnicianDetails,
            List<DateTime> removedDates, int defaultTechnicianId, bool defaultSettings);

        [OperationContract]
        Dictionary<string, ZipCode> GetZipCodes();

        [OperationContract]
        void InsertZipCode(ZipCode zip);


        [OperationContract]
        Coordinate GecocodeAddress(string address);
        [OperationContract]
        Coordinate GecocodeZip(string zip);

        [OperationContract]
        DateTime? GetExistingSnapshotDate();

        [OperationContract]
        void CreateSnapshot();
        [OperationContract]
        List<VisitChangeItem> GetSnapshotChanges();
        [OperationContract]
        List<Service> GetServices();

        [OperationContract]
        [FaultContract(typeof(WcfServiceBusinessException))]
        void MarkTimeAs(TimeInterval interval, int technicianId, bool markAsWorking);

        [OperationContract]
        void UnscheduleVisit(string ticketNumber);

        [OperationContract]
        void Subscribe(WcfSubscriberTypeEnum subscriberType, DateTime scheduleDate, User user, bool allDatesInBucket);

        [OperationContract]
        void Unsubscribe();

        [OperationContract]
        string UpdateVisit(Visit visit, bool callbackCaller, bool allowCollisions);

        [OperationContract]
        bool BookDelayedVisit(int visitId, RecommendationResponseItem recommendationItem);

        [OperationContract]
        BucketProcessingOptions GetBucketProcessingOptions(int bucketVisitId);

        [OperationContract]
        void ProcessDelayedVisitTempExclusivity(DelayedVisitSaveInfo saveInfo, 
            int tempExclusiveTechnicianId);

        [OperationContract]
        void ProcessDelayedVisitIgnoreExclusivity(DelayedVisitSaveInfo saveInfo);

        [OperationContract]
        void ProcessDelayedVisitChangeFrame(DelayedVisitSaveInfo saveInfo, VisitAddResult frameChangeOption);

        [OperationContract]
        void ProcessDelayedVisitExtendWorkingHours(DelayedVisitSaveInfo saveInfo,
            WorkingHoursExtensionResult extension);

        [OperationContract]
        void SaveDelayedVisit(DelayedVisitSaveInfo saveInfo);

        [OperationContract]
        void KeepAliveDummy();

        [OperationContract]
        void EnqueueOptimization(DateTime date);

        [OperationContract]
        void ProcessPendingOptimizations();

        [OperationContract]
        void ApplyOptimizationResult(Schedule schedule);

        [OperationContract]
        VisitAddResult CanInsertVisit(int visitId);

        [OperationContract]
        List<RecommendationResponseItem> GetRecommendations(RecommendationRequest request);

        [OperationContract]
        void RunSync(SyncTypeEnum syncType);

        [OperationContract]
        void ApplyServmanData(List<Order> orders);

        [OperationContract]
        void ModifyPredictionIgnoreDate(bool isSuspend);

        [OperationContract]
        string CreateBlockout(int technicianDefaultId, DateTime timeStart, DateTime timeEnd, string note);

        [OperationContract]
        void SendErrorEmail();

        [OperationContract]
        UserResult FindUser(string passwordHash);

        [OperationContract]
        List<User> FindUsers();

        [OperationContract]
        List<UserAction> FindUserActions(int? userId, UserActionTypeEnum? actionType, int? technicianDefaultId,
            string ticket, DateTime? dashboardDate, TimeInterval actionDateInterval, SortField sortField, bool isSortAscending);

        [OperationContract]
        string AddEditUser(User user);
    }
}
