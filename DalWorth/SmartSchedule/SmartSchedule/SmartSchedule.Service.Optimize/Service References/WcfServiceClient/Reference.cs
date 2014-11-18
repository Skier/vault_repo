﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartSchedule.Service.Optimize.WcfServiceClient {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WcfServiceClient.IWcfService", CallbackContract=typeof(SmartSchedule.Service.Optimize.WcfServiceClient.IWcfServiceCallback))]
    public interface IWcfService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/ProcessPendingOptimizations", ReplyAction="http://tempuri.org/IWcfService/ProcessPendingOptimizationsResponse")]
        void ProcessPendingOptimizations();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/ApplyOptimizationResult", ReplyAction="http://tempuri.org/IWcfService/ApplyOptimizationResultResponse")]
        void ApplyOptimizationResult(SmartSchedule.Domain.Schedule schedule);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/CanInsertVisit", ReplyAction="http://tempuri.org/IWcfService/CanInsertVisitResponse")]
        SmartSchedule.Domain.VisitAddResult CanInsertVisit(int visitId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/GetRecommendations", ReplyAction="http://tempuri.org/IWcfService/GetRecommendationsResponse")]
        SmartSchedule.Domain.Sync.RecommendationResponseItem[] GetRecommendations(SmartSchedule.Domain.Sync.RecommendationRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/RunSync", ReplyAction="http://tempuri.org/IWcfService/RunSyncResponse")]
        void RunSync(SmartSchedule.Domain.WCF.SyncTypeEnum syncType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/ApplyServmanData", ReplyAction="http://tempuri.org/IWcfService/ApplyServmanDataResponse")]
        void ApplyServmanData(SmartSchedule.Domain.Sync.Order[] orders);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/ModifyPredictionIgnoreDate", ReplyAction="http://tempuri.org/IWcfService/ModifyPredictionIgnoreDateResponse")]
        void ModifyPredictionIgnoreDate(bool isSuspend);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/CreateBlockout", ReplyAction="http://tempuri.org/IWcfService/CreateBlockoutResponse")]
        string CreateBlockout(int technicianDefaultId, System.DateTime timeStart, System.DateTime timeEnd, string note);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/GetFullViewInfo", ReplyAction="http://tempuri.org/IWcfService/GetFullViewInfoResponse")]
        SmartSchedule.Domain.WCF.VisitsFullChangeDetail GetFullViewInfo(System.DateTime date);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/GetDefaultTechnicians", ReplyAction="http://tempuri.org/IWcfService/GetDefaultTechniciansResponse")]
        SmartSchedule.Domain.Technician[] GetDefaultTechnicians();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/GetTechnicians", ReplyAction="http://tempuri.org/IWcfService/GetTechniciansResponse")]
        SmartSchedule.Domain.Technician[] GetTechnicians(System.DateTime date, bool defaultSettings, bool eliminateDetails);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/SaveTechnicianArrangement", ReplyAction="http://tempuri.org/IWcfService/SaveTechnicianArrangementResponse")]
        void SaveTechnicianArrangement(SmartSchedule.Domain.Technician[] orderedTechnicians, bool defaultSettings);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/GetTechnicianDetails", ReplyAction="http://tempuri.org/IWcfService/GetTechnicianDetailsResponse")]
        SmartSchedule.Domain.WCF.TechnicianDetail[] GetTechnicianDetails(int technicianDefaultId, bool defaultSettings);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/SaveTechnicianDetail", ReplyAction="http://tempuri.org/IWcfService/SaveTechnicianDetailResponse")]
        SmartSchedule.Domain.WCF.TechnicianDetailValidationError[] SaveTechnicianDetail(SmartSchedule.Domain.WCF.TechnicianDetail[] affectedTechnicianDetails, System.DateTime[] removedDates, int defaultTechnicianId, bool defaultSettings);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/GetZipCodes", ReplyAction="http://tempuri.org/IWcfService/GetZipCodesResponse")]
        System.Collections.Generic.Dictionary<string, SmartSchedule.Domain.ZipCode> GetZipCodes();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/InsertZipCode", ReplyAction="http://tempuri.org/IWcfService/InsertZipCodeResponse")]
        void InsertZipCode(SmartSchedule.Domain.ZipCode zip);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/GecocodeAddress", ReplyAction="http://tempuri.org/IWcfService/GecocodeAddressResponse")]
        SmartSchedule.Domain.Coordinate GecocodeAddress(string address);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/GecocodeZip", ReplyAction="http://tempuri.org/IWcfService/GecocodeZipResponse")]
        SmartSchedule.Domain.Coordinate GecocodeZip(string zip);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/GetExistingSnapshotDate", ReplyAction="http://tempuri.org/IWcfService/GetExistingSnapshotDateResponse")]
        System.Nullable<System.DateTime> GetExistingSnapshotDate();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/CreateSnapshot", ReplyAction="http://tempuri.org/IWcfService/CreateSnapshotResponse")]
        void CreateSnapshot();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/GetSnapshotChanges", ReplyAction="http://tempuri.org/IWcfService/GetSnapshotChangesResponse")]
        SmartSchedule.Domain.WCF.VisitChangeItem[] GetSnapshotChanges();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/GetServices", ReplyAction="http://tempuri.org/IWcfService/GetServicesResponse")]
        SmartSchedule.Domain.Service[] GetServices();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/MarkTimeAs", ReplyAction="http://tempuri.org/IWcfService/MarkTimeAsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(SmartSchedule.Domain.WCF.WcfServiceBusinessException), Action="http://tempuri.org/IWcfService/MarkTimeAsWcfServiceBusinessExceptionFault", Name="WcfServiceBusinessException", Namespace="http://schemas.datacontract.org/2004/07/SmartSchedule.Domain.WCF")]
        void MarkTimeAs(SmartSchedule.Domain.TimeInterval interval, int technicianId, bool markAsWorking);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/UnscheduleVisit", ReplyAction="http://tempuri.org/IWcfService/UnscheduleVisitResponse")]
        void UnscheduleVisit(string ticketNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/Subscribe", ReplyAction="http://tempuri.org/IWcfService/SubscribeResponse")]
        void Subscribe(SmartSchedule.Domain.WCF.WcfSubscriberTypeEnum subscriberType, System.DateTime scheduleDate, bool allDatesInBucket);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/Unsubscribe", ReplyAction="http://tempuri.org/IWcfService/UnsubscribeResponse")]
        void Unsubscribe();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/UpdateVisit", ReplyAction="http://tempuri.org/IWcfService/UpdateVisitResponse")]
        string UpdateVisit(SmartSchedule.Domain.Visit visit, bool callbackCaller, bool allowCollisions);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/BookDelayedVisit", ReplyAction="http://tempuri.org/IWcfService/BookDelayedVisitResponse")]
        bool BookDelayedVisit(int visitId, SmartSchedule.Domain.Sync.RecommendationResponseItem recommendationItem);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/GetBucketProcessingOptions", ReplyAction="http://tempuri.org/IWcfService/GetBucketProcessingOptionsResponse")]
        SmartSchedule.Domain.WCF.BucketProcessingOptions GetBucketProcessingOptions(int bucketVisitId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/ProcessDelayedVisitTempExclusivity", ReplyAction="http://tempuri.org/IWcfService/ProcessDelayedVisitTempExclusivityResponse")]
        void ProcessDelayedVisitTempExclusivity(SmartSchedule.Domain.WCF.DelayedVisitSaveInfo saveInfo, int tempExclusiveTechnicianId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/ProcessDelayedVisitIgnoreExclusivity", ReplyAction="http://tempuri.org/IWcfService/ProcessDelayedVisitIgnoreExclusivityResponse")]
        void ProcessDelayedVisitIgnoreExclusivity(SmartSchedule.Domain.WCF.DelayedVisitSaveInfo saveInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/ProcessDelayedVisitChangeFrame", ReplyAction="http://tempuri.org/IWcfService/ProcessDelayedVisitChangeFrameResponse")]
        void ProcessDelayedVisitChangeFrame(SmartSchedule.Domain.WCF.DelayedVisitSaveInfo saveInfo, SmartSchedule.Domain.VisitAddResult frameChangeOption);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/ProcessDelayedVisitExtendWorkingHours", ReplyAction="http://tempuri.org/IWcfService/ProcessDelayedVisitExtendWorkingHoursResponse")]
        void ProcessDelayedVisitExtendWorkingHours(SmartSchedule.Domain.WCF.DelayedVisitSaveInfo saveInfo, SmartSchedule.Domain.WorkingHoursExtensionResult extension);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/SaveDelayedVisit", ReplyAction="http://tempuri.org/IWcfService/SaveDelayedVisitResponse")]
        void SaveDelayedVisit(SmartSchedule.Domain.WCF.DelayedVisitSaveInfo saveInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/KeepAliveDummy", ReplyAction="http://tempuri.org/IWcfService/KeepAliveDummyResponse")]
        void KeepAliveDummy();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/EnqueueOptimization", ReplyAction="http://tempuri.org/IWcfService/EnqueueOptimizationResponse")]
        void EnqueueOptimization(System.DateTime date);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWcfServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IWcfService/OnViewModelChanged")]
        void OnViewModelChanged(SmartSchedule.Domain.WCF.CallbackInfo info);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IWcfService/OnOptimizationRequested")]
        void OnOptimizationRequested(SmartSchedule.Domain.Schedule schedule);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IWcfService/ForceSync")]
        void ForceSync(SmartSchedule.Domain.WCF.SyncTypeEnum syncType);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWcfServiceChannel : SmartSchedule.Service.Optimize.WcfServiceClient.IWcfService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WcfServiceClient : System.ServiceModel.DuplexClientBase<SmartSchedule.Service.Optimize.WcfServiceClient.IWcfService>, SmartSchedule.Service.Optimize.WcfServiceClient.IWcfService {
        
        public WcfServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public WcfServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public WcfServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public WcfServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public WcfServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void ProcessPendingOptimizations() {
            base.Channel.ProcessPendingOptimizations();
        }
        
        public void ApplyOptimizationResult(SmartSchedule.Domain.Schedule schedule) {
            base.Channel.ApplyOptimizationResult(schedule);
        }
        
        public SmartSchedule.Domain.VisitAddResult CanInsertVisit(int visitId) {
            return base.Channel.CanInsertVisit(visitId);
        }
        
        public SmartSchedule.Domain.Sync.RecommendationResponseItem[] GetRecommendations(SmartSchedule.Domain.Sync.RecommendationRequest request) {
            return base.Channel.GetRecommendations(request);
        }
        
        public void RunSync(SmartSchedule.Domain.WCF.SyncTypeEnum syncType) {
            base.Channel.RunSync(syncType);
        }
        
        public void ApplyServmanData(SmartSchedule.Domain.Sync.Order[] orders) {
            base.Channel.ApplyServmanData(orders);
        }
        
        public void ModifyPredictionIgnoreDate(bool isSuspend) {
            base.Channel.ModifyPredictionIgnoreDate(isSuspend);
        }
        
        public string CreateBlockout(int technicianDefaultId, System.DateTime timeStart, System.DateTime timeEnd, string note) {
            return base.Channel.CreateBlockout(technicianDefaultId, timeStart, timeEnd, note);
        }
        
        public SmartSchedule.Domain.WCF.VisitsFullChangeDetail GetFullViewInfo(System.DateTime date) {
            return base.Channel.GetFullViewInfo(date);
        }
        
        public SmartSchedule.Domain.Technician[] GetDefaultTechnicians() {
            return base.Channel.GetDefaultTechnicians();
        }
        
        public SmartSchedule.Domain.Technician[] GetTechnicians(System.DateTime date, bool defaultSettings, bool eliminateDetails) {
            return base.Channel.GetTechnicians(date, defaultSettings, eliminateDetails);
        }
        
        public void SaveTechnicianArrangement(SmartSchedule.Domain.Technician[] orderedTechnicians, bool defaultSettings) {
            base.Channel.SaveTechnicianArrangement(orderedTechnicians, defaultSettings);
        }
        
        public SmartSchedule.Domain.WCF.TechnicianDetail[] GetTechnicianDetails(int technicianDefaultId, bool defaultSettings) {
            return base.Channel.GetTechnicianDetails(technicianDefaultId, defaultSettings);
        }
        
        public SmartSchedule.Domain.WCF.TechnicianDetailValidationError[] SaveTechnicianDetail(SmartSchedule.Domain.WCF.TechnicianDetail[] affectedTechnicianDetails, System.DateTime[] removedDates, int defaultTechnicianId, bool defaultSettings) {
            return base.Channel.SaveTechnicianDetail(affectedTechnicianDetails, removedDates, defaultTechnicianId, defaultSettings);
        }
        
        public System.Collections.Generic.Dictionary<string, SmartSchedule.Domain.ZipCode> GetZipCodes() {
            return base.Channel.GetZipCodes();
        }
        
        public void InsertZipCode(SmartSchedule.Domain.ZipCode zip) {
            base.Channel.InsertZipCode(zip);
        }
        
        public SmartSchedule.Domain.Coordinate GecocodeAddress(string address) {
            return base.Channel.GecocodeAddress(address);
        }
        
        public SmartSchedule.Domain.Coordinate GecocodeZip(string zip) {
            return base.Channel.GecocodeZip(zip);
        }
        
        public System.Nullable<System.DateTime> GetExistingSnapshotDate() {
            return base.Channel.GetExistingSnapshotDate();
        }
        
        public void CreateSnapshot() {
            base.Channel.CreateSnapshot();
        }
        
        public SmartSchedule.Domain.WCF.VisitChangeItem[] GetSnapshotChanges() {
            return base.Channel.GetSnapshotChanges();
        }
        
        public SmartSchedule.Domain.Service[] GetServices() {
            return base.Channel.GetServices();
        }
        
        public void MarkTimeAs(SmartSchedule.Domain.TimeInterval interval, int technicianId, bool markAsWorking) {
            base.Channel.MarkTimeAs(interval, technicianId, markAsWorking);
        }
        
        public void UnscheduleVisit(string ticketNumber) {
            base.Channel.UnscheduleVisit(ticketNumber);
        }
        
        public void Subscribe(SmartSchedule.Domain.WCF.WcfSubscriberTypeEnum subscriberType, System.DateTime scheduleDate, bool allDatesInBucket) {
            base.Channel.Subscribe(subscriberType, scheduleDate, allDatesInBucket);
        }
        
        public void Unsubscribe() {
            base.Channel.Unsubscribe();
        }
        
        public string UpdateVisit(SmartSchedule.Domain.Visit visit, bool callbackCaller, bool allowCollisions) {
            return base.Channel.UpdateVisit(visit, callbackCaller, allowCollisions);
        }
        
        public bool BookDelayedVisit(int visitId, SmartSchedule.Domain.Sync.RecommendationResponseItem recommendationItem) {
            return base.Channel.BookDelayedVisit(visitId, recommendationItem);
        }
        
        public SmartSchedule.Domain.WCF.BucketProcessingOptions GetBucketProcessingOptions(int bucketVisitId) {
            return base.Channel.GetBucketProcessingOptions(bucketVisitId);
        }
        
        public void ProcessDelayedVisitTempExclusivity(SmartSchedule.Domain.WCF.DelayedVisitSaveInfo saveInfo, int tempExclusiveTechnicianId) {
            base.Channel.ProcessDelayedVisitTempExclusivity(saveInfo, tempExclusiveTechnicianId);
        }
        
        public void ProcessDelayedVisitIgnoreExclusivity(SmartSchedule.Domain.WCF.DelayedVisitSaveInfo saveInfo) {
            base.Channel.ProcessDelayedVisitIgnoreExclusivity(saveInfo);
        }
        
        public void ProcessDelayedVisitChangeFrame(SmartSchedule.Domain.WCF.DelayedVisitSaveInfo saveInfo, SmartSchedule.Domain.VisitAddResult frameChangeOption) {
            base.Channel.ProcessDelayedVisitChangeFrame(saveInfo, frameChangeOption);
        }
        
        public void ProcessDelayedVisitExtendWorkingHours(SmartSchedule.Domain.WCF.DelayedVisitSaveInfo saveInfo, SmartSchedule.Domain.WorkingHoursExtensionResult extension) {
            base.Channel.ProcessDelayedVisitExtendWorkingHours(saveInfo, extension);
        }
        
        public void SaveDelayedVisit(SmartSchedule.Domain.WCF.DelayedVisitSaveInfo saveInfo) {
            base.Channel.SaveDelayedVisit(saveInfo);
        }
        
        public void KeepAliveDummy() {
            base.Channel.KeepAliveDummy();
        }
        
        public void EnqueueOptimization(System.DateTime date) {
            base.Channel.EnqueueOptimization(date);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WcfServiceClient.IWcfServiceWeb")]
    public interface IWcfServiceWeb {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfServiceWeb/GetDefaultTechniciansWeb", ReplyAction="http://tempuri.org/IWcfServiceWeb/GetDefaultTechniciansWebResponse")]
        SmartSchedule.Domain.Technician[] GetDefaultTechniciansWeb();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfServiceWeb/GetTechnicianDetailsByServmanIdWeb", ReplyAction="http://tempuri.org/IWcfServiceWeb/GetTechnicianDetailsByServmanIdWebResponse")]
        SmartSchedule.Domain.WCF.TechnicianDetail[] GetTechnicianDetailsByServmanIdWeb(string technicianServmanId, bool defaultSettings);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfServiceWeb/GetTechnicianDetailsWeb", ReplyAction="http://tempuri.org/IWcfServiceWeb/GetTechnicianDetailsWebResponse")]
        SmartSchedule.Domain.WCF.TechnicianDetail[] GetTechnicianDetailsWeb(int technicianDefaultId, bool defaultSettings);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfServiceWeb/SaveTechnicianDetailWeb", ReplyAction="http://tempuri.org/IWcfServiceWeb/SaveTechnicianDetailWebResponse")]
        SmartSchedule.Domain.WCF.TechnicianDetailValidationError[] SaveTechnicianDetailWeb(SmartSchedule.Domain.WCF.TechnicianDetail[] affectedTechnicianDetails, System.DateTime[] removedDates, int defaultTechnicianId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfServiceWeb/ValidateTechnicianDetailsWeb", ReplyAction="http://tempuri.org/IWcfServiceWeb/ValidateTechnicianDetailsWebResponse")]
        SmartSchedule.Domain.WCF.TechnicianDetailValidationError[] ValidateTechnicianDetailsWeb(SmartSchedule.Domain.WCF.TechnicianDetail[] affectedTechnicianDetails, System.DateTime[] removedDates, int defaultTechnicianId, bool deepValidation);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfServiceWeb/GetRecommendationsWeb", ReplyAction="http://tempuri.org/IWcfServiceWeb/GetRecommendationsWebResponse")]
        SmartSchedule.Domain.Sync.RecommendationResponseItem[] GetRecommendationsWeb(SmartSchedule.Domain.Sync.RecommendationRequest request);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWcfServiceWebChannel : SmartSchedule.Service.Optimize.WcfServiceClient.IWcfServiceWeb, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WcfServiceWebClient : System.ServiceModel.ClientBase<SmartSchedule.Service.Optimize.WcfServiceClient.IWcfServiceWeb>, SmartSchedule.Service.Optimize.WcfServiceClient.IWcfServiceWeb {
        
        public WcfServiceWebClient() {
        }
        
        public WcfServiceWebClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WcfServiceWebClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WcfServiceWebClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WcfServiceWebClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public SmartSchedule.Domain.Technician[] GetDefaultTechniciansWeb() {
            return base.Channel.GetDefaultTechniciansWeb();
        }
        
        public SmartSchedule.Domain.WCF.TechnicianDetail[] GetTechnicianDetailsByServmanIdWeb(string technicianServmanId, bool defaultSettings) {
            return base.Channel.GetTechnicianDetailsByServmanIdWeb(technicianServmanId, defaultSettings);
        }
        
        public SmartSchedule.Domain.WCF.TechnicianDetail[] GetTechnicianDetailsWeb(int technicianDefaultId, bool defaultSettings) {
            return base.Channel.GetTechnicianDetailsWeb(technicianDefaultId, defaultSettings);
        }
        
        public SmartSchedule.Domain.WCF.TechnicianDetailValidationError[] SaveTechnicianDetailWeb(SmartSchedule.Domain.WCF.TechnicianDetail[] affectedTechnicianDetails, System.DateTime[] removedDates, int defaultTechnicianId) {
            return base.Channel.SaveTechnicianDetailWeb(affectedTechnicianDetails, removedDates, defaultTechnicianId);
        }
        
        public SmartSchedule.Domain.WCF.TechnicianDetailValidationError[] ValidateTechnicianDetailsWeb(SmartSchedule.Domain.WCF.TechnicianDetail[] affectedTechnicianDetails, System.DateTime[] removedDates, int defaultTechnicianId, bool deepValidation) {
            return base.Channel.ValidateTechnicianDetailsWeb(affectedTechnicianDetails, removedDates, defaultTechnicianId, deepValidation);
        }
        
        public SmartSchedule.Domain.Sync.RecommendationResponseItem[] GetRecommendationsWeb(SmartSchedule.Domain.Sync.RecommendationRequest request) {
            return base.Channel.GetRecommendationsWeb(request);
        }
    }
}
