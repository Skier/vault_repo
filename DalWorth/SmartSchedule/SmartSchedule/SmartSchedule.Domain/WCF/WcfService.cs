using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using SmartSchedule.Data;
using SmartSchedule.Domain.Sync;

namespace SmartSchedule.Domain.WCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public class WcfService : IWcfService, IWcfServiceWeb
    {
        private readonly BookingEngine m_bookingEngine;
        private static readonly Dictionary<IWcfSubscriber, WcfSubscriberInfo> m_callbackSubscribers 
            = new Dictionary<IWcfSubscriber, WcfSubscriberInfo>();
        private static readonly List<DateTime> m_pendingOptimizationDates = new List<DateTime>();

        #region WcfService

        public WcfService()
        {
            m_bookingEngine = new BookingEngine();
        }

        #endregion

        #region GetFullViewInfo

        private List<Visit> GetDelayedVisits(IWcfSubscriber subscriber, DateTime date)
        {
            if (m_callbackSubscribers[subscriber].IsAllDatesInBucket)
                return m_bookingEngine.GetDelayedVisits();
            return m_bookingEngine.GetDelayedVisits(date.Date);
        }

        public VisitsFullChangeDetail GetFullViewInfo(DateTime date)
        {
            IWcfSubscriber subscriber = OperationContext.Current.GetCallbackChannel<IWcfSubscriber>();                 

            return new VisitsFullChangeDetail(m_bookingEngine.Visits.GetVisits(date),
                GetDelayedVisits(subscriber, date), m_bookingEngine.GetTemporaryAssignedVisits(date),
                PredictionIgnore.IsDateIgnored(date), CallbackInfo.GetLastProcessedCallbackId(date));
        }

        #endregion

        #region Technician Arrange

        #region GetTechnicians

        public IEnumerable<Technician> GetDefaultTechnicians()
        {
            return GetTechnicians(DateTime.Now, true, false);            
        }

        public IEnumerable<Technician> GetTechnicians(DateTime date, bool defaultSettings, bool eliminateDetails)
        {
            IEnumerable<Technician> result;

            if (defaultSettings)
            {
                result = from defaultTechnician in Technician.GetTechnicianDefaults()
                         orderby defaultTechnician.DisplaySequence
                         select defaultTechnician.ConvertTo(DateTime.Now);
            }
            else
                result = m_bookingEngine.GetTechnicians(date).ToList();

            if (!eliminateDetails)
                return result;

            List<Technician> resultWithoutDetals = new List<Technician>();
            foreach (var technician in result)
                resultWithoutDetals.Add((Technician)technician.Clone());

            return resultWithoutDetals;
        }

        #endregion

        #region SaveTechnicianArrangement

        public void SaveTechnicianArrangement(List<Technician> orderedTechnicians,
            bool defaultSettings)
        {
            if (orderedTechnicians.Count == 0) 
                return;

            DateTime scheduleDate = orderedTechnicians[0].ScheduleDate;

            try
            {                
                int currentDisplaySequence = 1;
                if (defaultSettings)
                {
                    Database.Begin();
                    foreach (Technician technician in orderedTechnicians)
                    {
                        TechnicianDefault serverTechnician = Technician.GetTechnicianDefault(
                            technician.TechnicianDefaultId);
                        serverTechnician.DisplaySequence = currentDisplaySequence++;
                        TechnicianDefault.Update(serverTechnician);
                    }

                    UserAction.WriteAction(GetCurrentUser(), UserActionTypeEnum.TechnicianSettingsEditDefault,
                        null, null, "Technicians default order changed");
                    Database.Commit();
                }
                else
                {
                    Database.Begin();
                    foreach (Technician technician in orderedTechnicians)
                    {
                        Technician serverTechnician = Technician.GetTechnician(technician.ID);
                        serverTechnician.DisplaySequence = currentDisplaySequence++;
                        Technician.Update(serverTechnician);
                    }

                    UserAction.WriteAction(GetCurrentUser(), UserActionTypeEnum.TechnicianSettingsEditDaily,
                        null, scheduleDate.Date, "Technicians daily order changed");
                    Database.Commit();

                    m_bookingEngine.RefreshTechnicians(scheduleDate);
                    CallbackInfo info = CallbackInfo.Create(scheduleDate, CallbackType.Technicians);
                    info.TechniciansChange = new List<Technician>(m_bookingEngine.GetTechnicians(scheduleDate));
                    SendViewChangeCallbacks(info);                    
                }                    
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }
        }

        #endregion

        #endregion


        #region Technician Edit

        #region GetTechnicianDetail

        public List<TechnicianDetail> GetTechnicianDetailsByServmanIdWeb(string technicianServmanId, bool defaultSettings)
        {
            TechnicianDefault technicianDefault;
            try
            {
                technicianDefault = Technician.GetTechnician(technicianServmanId);
            }
            catch (Exception)
            {
                return new List<TechnicianDetail>();
            }

            return GetTechnicianDetails(technicianDefault.ID, defaultSettings);
        }

        public TechnicianDetail GetTechnicianDetail(int technicianDefaultId, DateTime scheduleDate)
        {
            List<DateTime> dateList = new List<DateTime>();
            dateList.Add(scheduleDate.Date);
            List<TechnicianDetail> details = GetTechnicianDetails(technicianDefaultId, dateList, false);
            if (details.Count == 0)
                return null;
            return details[0];
        }

        public List<TechnicianDetail> GetTechnicianDetails(int technicianDefaultId, bool defaultSettings)
        {            
            return GetTechnicianDetails(technicianDefaultId, Technician.FindScheduleDates(), defaultSettings);
        }

        public List<TechnicianDetail> GetTechnicianDetails(int technicianDefaultId, List<DateTime> scheduleDates, bool defaultSettings)
        {
            try
            {
                Technician.GetTechnicianDefault(technicianDefaultId);
            }
            catch (Exception)
            {
                return new List<TechnicianDetail>();
            }

            IEnumerable<TechnicianWorkTimeDefaultPreset> presets
                = TechnicianWorkTimeDefaultPreset.FindByTechnician(technicianDefaultId);

            List<TechnicianDetail> result = new List<TechnicianDetail>();                        
            if (defaultSettings)
            {
                TechnicianDefault technicianDefault = TechnicianDefault.FindByPrimaryKey(technicianDefaultId);
                Technician technician = technicianDefault.ConvertTo(DateTime.Now);

                technician.WorkingIntervals =
                    TechnicianWorkTimeDefault.FindByTechnician(technician).Select(
                        workTimeDefault => workTimeDefault.Convert()).ToList();

                List<TechnicianZipDefault> zips = TechnicianZipDefault.FindByTechnician(technician);
                technician.PrimaryZipCodes = zips.Where(zip => zip.IsPrimaryZip).Select(zip => zip.Zip).ToList();
                technician.SecondaryZipCodes = zips.Where(zip => !zip.IsPrimaryZip).Select(zip => zip.Zip).ToList();

                technician.DeniedServicesMap = TechnicianServiceDenyDefault.FindByTechnician(technician.TechnicianDefaultId)
                    .ToDictionary(serviceDenyDefault => serviceDenyDefault.ServiceId,
                                  serviceDenyDefault => serviceDenyDefault.Convert());

                TechnicianDetail technicianDetail = new TechnicianDetail(technician, null, presets);
                technicianDetail.Email = technicianDefault.Email;
                result.Add(technicianDetail);                
            } 
            else
            {
                foreach (DateTime date in scheduleDates)
	            {
	                Technician technician = Technician.GetTechnician(date, technicianDefaultId);
                    if (technician != null)
                        result.Add(new TechnicianDetail(technician, null, presets));
	            }                
            }

            List<Service> services = Service.Find();
            foreach (var technicianDetail in result)
            {
                technicianDetail.PopulateZipCodesStrings();
                Technician technician = technicianDetail.Technician;

                technicianDetail.Services = services.Select(
                service => new TechnicianService(service,
                    technician.DeniedServicesMap.ContainsKey(service.ID)
                        ? technician.DeniedServicesMap[service.ID] : null))
                .OrderBy(techService => techService.ServiceAllowance)
                .ThenBy(techService => techService.Service.Name).ToList();
            }

            return result;            
        }

        #endregion        

        #region SaveTechnicianDetail

        public List<TechnicianDetailValidationError> ValidateTechnicianDetailsWeb(
            List<TechnicianDetail> affectedTechnicianDetails, List<DateTime> removedDates, int defaultTechnicianId,
            bool deepValidation)
        {
            return ValidateTechnicianDetails(affectedTechnicianDetails, removedDates, defaultTechnicianId,
                false, deepValidation);
        }


        public List<TechnicianDetailValidationError> ValidateTechnicianDetails(
            List<TechnicianDetail> affectedTechnicianDetails, List<DateTime> removedDates, int defaultTechnicianId,
            bool defaultSettings, bool deepValidation)
        {
            List<TechnicianDetailValidationError> result = new List<TechnicianDetailValidationError>();

            if (affectedTechnicianDetails != null)
            {
                TechnicianDetail defaultDetail = null;
                if (defaultTechnicianId != 0)
                    defaultDetail = GetTechnicianDetails(defaultTechnicianId, true)[0];

                foreach (TechnicianDetail affectedTechnician in affectedTechnicianDetails)
                {
                    TechnicianDetail existingDetail = GetTechnicianDetail(defaultTechnicianId,
                        affectedTechnician.Technician.ScheduleDate.Date);
                    if (existingDetail == null)
                        existingDetail = defaultDetail;

                    List<TechnicianDetailValidationError> errors;
                    if (deepValidation)
                    {
                        errors = TechnicianDetailValidationError.GetErrorsDeep(affectedTechnician,
                            existingDetail, defaultSettings, this);                        
                    }
                    else
                        errors = TechnicianDetailValidationError.GetErrors(affectedTechnician, defaultSettings);
                    result.AddRange(errors);  
                }
            }

            if (deepValidation && removedDates != null)
            {
                List<DateTime> datesWithVisits = new List<DateTime>();
                foreach (DateTime removedDate in removedDates)
                {
                    Technician removedTechnician = Technician.GetTechnician(removedDate.Date, defaultTechnicianId);
                    IList<Visit> tecnicianVisits = m_bookingEngine.GetSortedVisits(removedTechnician);
                    foreach (var visit in tecnicianVisits)
                    {
                        if (!visit.IsBlockout)
                        {
                            datesWithVisits.Add(removedDate.Date);
                            break;
                        }
                    }
                }

                if (datesWithVisits.Count > 0)
                {
                    string dates = string.Empty;
                    foreach (DateTime date in datesWithVisits)
                        dates += date.ToShortDateString() + ", ";
                    dates = dates.TrimEnd(',', ' ');
                    string errorText = string.Format("Unable to remove technician settings for date(s) {0} because of existing visits on these dates", dates);
                    result.Add(TechnicianDetailValidationError.CreateError(datesWithVisits[0], errorText));
                }
            }

            return result;
        }

        public List<TechnicianDetailValidationError> SaveTechnicianDetailWeb(List<TechnicianDetail> affectedTechnicianDetails, 
            List<DateTime> removedDates, int defaultTechnicianId)
        {
            return SaveTechnicianDetail(affectedTechnicianDetails, removedDates, defaultTechnicianId, false);
        }

        public List<TechnicianDetailValidationError> SaveTechnicianDetail(List<TechnicianDetail> affectedTechnicianDetails, 
            List<DateTime> removedDates, int defaultTechnicianId, bool defaultSettings)
        {
            List<TechnicianDetailValidationError> result = ValidateTechnicianDetails(affectedTechnicianDetails,
                removedDates, defaultTechnicianId, defaultSettings, true);
            if (result.Count > 0)
            {
                string validationErrorText = "Validation failed with the following errors:";
                foreach (TechnicianDetailValidationError error in result)
                {
                    validationErrorText += string.Format("{0} - {1} for date {2}; ", 
                        error.ErrorField, error.ErrorText, error.ScheduleDate.ToShortDateString());
                }
                validationErrorText = validationErrorText.Substring(0, validationErrorText.Length - 2);
                UserAction.WriteAction(GetCurrentUser(),
                    defaultSettings ? UserActionTypeEnum.TechnicianSettingsEditDefault : UserActionTypeEnum.TechnicianSettingsEditDaily,
                    defaultTechnicianId != 0 ? defaultTechnicianId : (int?)null, null, validationErrorText);
                return result;
            }
                

            try
            {
                Database.Begin();

                if (defaultSettings)
                {
                    if (defaultTechnicianId == 0)
                    {
                        TechnicianDefault newTechnician = TechnicianDefault.ConvertTo(
                            affectedTechnicianDetails[0].Technician);
                        newTechnician.Email = affectedTechnicianDetails[0].Email;
                        newTechnician.CompanyId = Company.Find()[0].ID;

                        TechnicianDefault.Insert(newTechnician);
                        UserAction.WriteAction(GetCurrentUser(), UserActionTypeEnum.TechnicianSettingsEditDefault,
                            newTechnician.ID, null, string.Format("New technician {0} added", newTechnician.Name));

                        List<TechnicianWorkTimeDefaultPreset> presets =
                            affectedTechnicianDetails[0].WorkingHoursPresets.ToList();
                        TechnicianWorkTimeDefaultPreset defaultPreset = null;
                        foreach (var preset in presets)
                        {
                            preset.TechnicianId = newTechnician.ID;
                            if (preset.PresetNumber == 2)
                                defaultPreset = preset;
                        }
                            
                        TechnicianWorkTimeDefaultPreset.Insert(presets);

                        TechnicianWorkTimeDefault workTimeDefault = TechnicianWorkTimeDefault.Convert(
                            affectedTechnicianDetails[0].Technician.WorkingIntervals[0]);
                        workTimeDefault.TechnicianId = newTechnician.ID;
                        workTimeDefault.TimeStart = defaultPreset.TimeStart.Value;
                        workTimeDefault.TimeEnd = defaultPreset.TimeEnd.Value;
                        TechnicianWorkTimeDefault.Insert(workTimeDefault);

                        foreach (string primaryZip in affectedTechnicianDetails[0].Technician.PrimaryZipCodes)
                            TechnicianZipDefault.Insert(new TechnicianZipDefault(newTechnician.ID, primaryZip, true));
                        foreach (string secondaryZip in affectedTechnicianDetails[0].Technician.SecondaryZipCodes)
                            TechnicianZipDefault.Insert(new TechnicianZipDefault(newTechnician.ID, secondaryZip, false));

                        foreach (TechnicianService service in affectedTechnicianDetails[0].Services)
                        {
                            if (service.ServiceAllowance != TechnicianService.ServiceAllowanceEnum.Allowed)
                            {
                                TechnicianServiceDenyDefault.Insert(new TechnicianServiceDenyDefault(
                                    newTechnician.ID, service.Service.ID,
                                    service.ServiceAllowance == TechnicianService.ServiceAllowanceEnum.AllowedForExclusive));
                            }
                        }

                        Technician.RefreshTechnicianInServerCache(newTechnician);
                    }
                    else
                    {
                        TechnicianDetail oldDetail = GetTechnicianDetails(defaultTechnicianId, true)[0];
                        TechnicianDefault modifiedTechnician = TechnicianDefault.ConvertTo(
                            affectedTechnicianDetails[0].Technician);
                        modifiedTechnician.Email = affectedTechnicianDetails[0].Email;

                        string changeText = UserAction.GetTechnicianSettingsChangeText(oldDetail,
                            affectedTechnicianDetails[0], defaultSettings);
                        if (changeText != string.Empty)
                        {
                            UserAction.WriteAction(GetCurrentUser(), UserActionTypeEnum.TechnicianSettingsEditDefault,
                                defaultTechnicianId, null, changeText);
                        }

                        TechnicianDefault.Update(modifiedTechnician);

                        TechnicianWorkTimeDefaultPreset.DeleteByTechnician(modifiedTechnician);
                        TechnicianWorkTimeDefaultPreset.Insert(affectedTechnicianDetails[0].WorkingHoursPresets.ToList());

                        TechnicianWorkTimeDefault.DeleteByTechnician(modifiedTechnician);
                        foreach (TechnicianWorkTime workingInterval in affectedTechnicianDetails[0].Technician.WorkingIntervals)
                            TechnicianWorkTimeDefault.Insert(TechnicianWorkTimeDefault.Convert(workingInterval));

                        TechnicianZipDefault.DeleteByTechnician(modifiedTechnician);
                        foreach (string primaryZip in affectedTechnicianDetails[0].Technician.PrimaryZipCodes)
                            TechnicianZipDefault.Insert(new TechnicianZipDefault(modifiedTechnician.ID, primaryZip, true));

                        foreach (string secondaryZip in affectedTechnicianDetails[0].Technician.SecondaryZipCodes)
                            TechnicianZipDefault.Insert(new TechnicianZipDefault(modifiedTechnician.ID, secondaryZip, false));

                        TechnicianServiceDenyDefault.DeleteByTechnician(modifiedTechnician);
                        foreach (TechnicianService service in affectedTechnicianDetails[0].Services)
                        {
                            if (service.ServiceAllowance != TechnicianService.ServiceAllowanceEnum.Allowed)
                            {
                                TechnicianServiceDenyDefault.Insert(new TechnicianServiceDenyDefault(
                                    modifiedTechnician.ID, service.Service.ID,
                                    service.ServiceAllowance == TechnicianService.ServiceAllowanceEnum.AllowedForExclusive));
                            }
                        }

                        Technician.RefreshTechnicianInServerCache(modifiedTechnician);
                    }
                }
                else
                {
                    if (removedDates == null)
                        removedDates = new List<DateTime>();

                    foreach (DateTime removedDate in removedDates)
                    {
                        UserAction.WriteAction(GetCurrentUser(), UserActionTypeEnum.TechnicianSettingsEditDaily,
                            defaultTechnicianId, removedDate, "Technician removed from schedule");

                        Technician technician = Technician.GetTechnician(removedDate, defaultTechnicianId);

                        List<Visit> visits = m_bookingEngine.GetSortedVisits(technician);
                        foreach (var visit in visits)
                        {
                            if (visit.IsBlockout)
                                m_bookingEngine.RemoveVisit(visit, false, GetCurrentUser());
                        }

                        TechnicianWorkTime.DeleteByTechnician(technician);
                        TechnicianZip.DeleteByTechnician(technician);
                        TechnicianServiceDeny.DeleteByTechnician(technician);
                        Technician.Delete(technician);

                        m_bookingEngine.RemoveTechinician(technician);
                        Technician.RemoveTechnicianFromServerCache(technician);

                        m_bookingEngine.RefreshTechnicians(removedDate);
                        m_bookingEngine.RefreshBucket(removedDate);

                        CallbackInfo info = CallbackInfo.Create(removedDate, CallbackType.Technicians);
                        info.TechniciansChange = new List<Technician>(m_bookingEngine.GetTechnicians(removedDate));
                        SendViewChangeCallbacks(info);

                        CallbackInfo info2 = CallbackInfo.Create(removedDate, CallbackType.Visits);
                        info2.VisitsChange = new VisitsChangeDetail();
                        SendViewChangeCallbacks(info2);
                    }                        

                    if (affectedTechnicianDetails == null)
                        affectedTechnicianDetails = new List<TechnicianDetail>();

                    foreach (TechnicianDetail affectedTechnician in affectedTechnicianDetails)
                    {
                        Technician serverTechnician = Technician.GetTechnician(
                            affectedTechnician.Technician.ScheduleDate,
                            affectedTechnician.Technician.TechnicianDefaultId);
                        DateTime technicianDate = affectedTechnician.Technician.ScheduleDate;
                        Technician oldTechnician = null;

                        string changeText;
                        if (serverTechnician != null)
                        {
                            TechnicianDetail oldDetail = GetTechnicianDetail(defaultTechnicianId, technicianDate);
                            changeText = UserAction.GetTechnicianSettingsChangeText(oldDetail, affectedTechnician, defaultSettings);

                            oldTechnician = Technician.FindByPrimaryKey(serverTechnician.ID);
                            TechnicianWorkTime.DeleteByTechnician(serverTechnician);
                            TechnicianZip.DeleteByTechnician(serverTechnician);
                            TechnicianServiceDeny.DeleteByTechnician(serverTechnician);

                            affectedTechnician.Technician.ID = serverTechnician.ID;
                            Technician.Update(affectedTechnician.Technician);
                            serverTechnician.Import(affectedTechnician.Technician);
                        }
                        else
                        {
                            changeText = "Technician added to the schedule";
                            Technician.Insert(affectedTechnician.Technician);
                        }

                        if (changeText != string.Empty)
                            UserAction.WriteAction(GetCurrentUser(), UserActionTypeEnum.TechnicianSettingsEditDaily,
                                defaultTechnicianId, technicianDate, changeText);                                
                        
                        foreach (TechnicianWorkTime workTime in affectedTechnician.Technician.WorkingIntervals)
                        {
                            workTime.TechnicianId = affectedTechnician.Technician.ID;
                            workTime.TimeStart = technicianDate.Add(workTime.TimeStart.TimeOfDay);
                            workTime.TimeEnd = technicianDate.Add(workTime.TimeEnd.TimeOfDay);
                        }                            
                        TechnicianWorkTime.Insert(affectedTechnician.Technician.WorkingIntervals[0]);

                        foreach (string primaryZip in affectedTechnician.Technician.PrimaryZipCodes)
                            TechnicianZip.Insert(new TechnicianZip(affectedTechnician.Technician.ID, primaryZip, true));

                        foreach (string secondaryZip in affectedTechnician.Technician.SecondaryZipCodes)
                            TechnicianZip.Insert(new TechnicianZip(affectedTechnician.Technician.ID, secondaryZip, false));

                        foreach (TechnicianService service in affectedTechnician.Services)
                        {
                            if (service.ServiceAllowance != TechnicianService.ServiceAllowanceEnum.Allowed)
                            {
                                TechnicianServiceDeny.Insert(new TechnicianServiceDeny(
                                    affectedTechnician.Technician.ID, service.Service.ID,
                                    service.ServiceAllowance == TechnicianService.ServiceAllowanceEnum.AllowedForExclusive));
                            }
                        }

                        if (serverTechnician != null)
                        {
                            serverTechnician.Import(affectedTechnician.Technician);
                            serverTechnician.LoadTechnicianDetails(true);

                            CallbackInfo info = CallbackInfo.Create(technicianDate, CallbackType.Technician);
                            info.TechnicianChange = serverTechnician;
                            SendViewChangeCallbacks(info);

                            if (IsNeedRearrangeTechnicianSchedule(oldTechnician, serverTechnician))
                            {
                                CallbackInfo info2 = CallbackInfo.Create(technicianDate, CallbackType.Visits);
                                info2.VisitsChange = m_bookingEngine.RearrangeVisits(serverTechnician, GetCurrentUser());
                                SendViewChangeCallbacks(info2);
                            }

                            if (m_bookingEngine.RefreshBucket(technicianDate))
                            {
                                CallbackInfo info2 = CallbackInfo.Create(technicianDate, CallbackType.VisitsFull);
                                info2.VisitsFullChange = GetFullViewInfo(technicianDate);
                                SendViewChangeCallbacks(info2);
                            }
                        }
                        else
                        {
                            serverTechnician = affectedTechnician.Technician;
                            Technician.AddTechnicianToServerCache(serverTechnician, true);

                            m_bookingEngine.RefreshTechnicians(technicianDate);
                            m_bookingEngine.RefreshBucket(technicianDate);

                            CallbackInfo info = CallbackInfo.Create(technicianDate, CallbackType.Technicians);
                            info.TechniciansChange = new List<Technician>(m_bookingEngine.GetTechnicians(technicianDate));
                            SendViewChangeCallbacks(info);

                            CallbackInfo info2 = CallbackInfo.Create(technicianDate, CallbackType.Visits);
                            info2.VisitsChange = new VisitsChangeDetail();
                            SendViewChangeCallbacks(info2);
                        }                            
                    }                     
                }

                Database.Commit();
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }

            return result;
        }

        private bool IsNeedRearrangeTechnicianSchedule(Technician oldTechnician, Technician newTechnician)
        {
            List<Visit> visits = Visit.FindWithDetails(newTechnician, false, false)
                .OrderBy(visit => visit.TimeStart).ToList();

            if (visits.Count == 0)
                return false;
            
            if (newTechnician.WorkingIntervals[0].TimeStart > visits[0].TimeStart
                 || newTechnician.WorkingIntervals[0].TimeEnd < visits[visits.Count - 1].TimeEnd)
            {
                return true;
            }

            if (oldTechnician.DriveTimeMinutes != newTechnician.DriveTimeMinutes
                || oldTechnician.HourlyRate != newTechnician.HourlyRate
                || oldTechnician.HourlyRate150to300 != newTechnician.HourlyRate150to300
                || oldTechnician.HourlyRateMore300 != newTechnician.HourlyRateMore300)
            {
                return true;
            }

            if (newTechnician.MaxNonExclusiveVisitsCount < oldTechnician.MaxNonExclusiveVisitsCount
                || newTechnician.MaxVisitsCount < oldTechnician.MaxVisitsCount)
            {
                List<PathItem> path = new List<PathItem>();
                foreach (var visit in visits)
                    path.Add(new PathItem(visit, visit.TimeStart, newTechnician));

                if (PathAnalyzer.IsTechnicianLimitsExceeded(path, newTechnician))
                    return true;
            }

            foreach (Visit visit in visits)
            {
                if (!visit.IsTechnicianAllowed(newTechnician))
                    return true;
            }

            return false;
        }

        #endregion

        #region GetZipCodes

        public Dictionary<string, ZipCode> GetZipCodes()
        {
            return ZipCode.Zips;
        }

        #endregion

        #region InsertZipCode

        public void InsertZipCode(ZipCode zip)
        {
            ZipCode.Insert(zip);
            ZipCode.Zips.Add(zip.Zip, zip);            
        }

        #endregion

        #endregion

        #region Gecocoding

        public Coordinate GecocodeAddress(string address)
        {
            return GoogleGeocoder.Geocode(address);
        }

        public Coordinate GecocodeZip(string zip)
        {
            return GoogleGeocoder.GeocodeZip(zip);
        }

        #endregion        

        #region ImportTickets

        private void ImportTickets(List<Order> orders)
        {
            try
            {
                bool isFirstSync = !ApplicationSetting.IsContainsData();

                Database.Begin();
                VisitsChangeDetail syncResult = Sync.Sync.Import(m_bookingEngine, orders);
                Database.Commit();


                if (isFirstSync)
                {
                    List<DateTime> subscriberDates = new List<DateTime>();
                    foreach (var subscriber in m_callbackSubscribers)
                    {                        
                        if (!subscriberDates.Contains(subscriber.Value.ScheduleDate))
                            subscriberDates.Add(subscriber.Value.ScheduleDate);
                    }

                    foreach (var date in subscriberDates)
                    {
                        CallbackInfo info = CallbackInfo.Create(date, CallbackType.VisitsFull);
                        info.VisitsFullChange = GetFullViewInfo(date);
                        SendViewChangeCallbacks(info);
                    }
                }
                else
                {
                    foreach (var affectedDate in syncResult.AffectedDates)
                    {
                        VisitsChangeDetail dateDetail = new VisitsChangeDetail();

                        foreach (var addedVisit in syncResult.DashboardAddedVisits)
                        {
                            if (addedVisit.ScheduleDate == affectedDate)
                                dateDetail.AddAddedVisit(addedVisit);
                        }

                        foreach (var removedVisit in syncResult.DashboardRemovedVisits)
                        {
                            if (removedVisit.ScheduleDate == affectedDate)
                                dateDetail.AddRemovedVisit(removedVisit);                            
                        }

                        foreach (var modifiedVisit in syncResult.DashboardModifiedVisits)
                        {
                            if (modifiedVisit.ScheduleDate == affectedDate)
                                dateDetail.AddModifiedVisit(modifiedVisit);                            
                        }

                        CallbackInfo info = CallbackInfo.Create(affectedDate, CallbackType.Visits);
                        info.VisitsChange = dateDetail;
                        SendViewChangeCallbacks(info);
                    }
                }
                Host.Trace("SmartScheduleServmanSync", "Servman Tickets received and changes applied");
            }
            catch (Exception ex)
            {
                Database.Rollback();
                Host.Trace("SmartScheduleServmanSync", "ImportTickets: " + ex.Message + ex.StackTrace);
            }

            List<Visit> visitToBePlacedAutomatically = new List<Visit>();
            foreach (Visit visit in m_bookingEngine.GetDelayedVisits())
            {
                if (visit.CanBook && !m_bookingEngine.IsVisitUnscheduledManually(visit))
                    visitToBePlacedAutomatically.Add(visit);
            }

            visitToBePlacedAutomatically = visitToBePlacedAutomatically.OrderByDescending(
                visit => visit.ExclusiveTechnicianDefaultId.HasValue)
                .ThenByDescending(visit => visit.DurationCost).ToList();

            foreach (Visit visit in visitToBePlacedAutomatically)
                BookDelayedVisit(visit.ID, null);
                     
            EmailSender.SendBucketNotifications(m_bookingEngine);
            EmailSender.SendSecondaryNotifications(m_bookingEngine);
        }

        #endregion

        #region CreateSnapshot

        public DateTime? GetExistingSnapshotDate()
        {
            return Visit.FindSnapshotDate();
        }

        public void CreateSnapshot()
        {
            try
            {
                Database.Begin();
                Visit.CreateSnapshot();
                Database.Commit();
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }
        }

        #endregion

        #region GetSnapshotChanges

        public List<VisitChangeItem> GetSnapshotChanges()
        {
            var result = new List<VisitChangeItem>();

            if (Visit.FindSnapshotDate() == null)
                return result;

            List<Visit> spanshotVisits = Visit.FindWithDetails(null, true, true);
            List<Visit> currentVisits = Visit.FindWithDetails(null, false, true);

            foreach (Visit currentVisit in currentVisits)
            {
                bool isExistInSnapshot = false;

                foreach (Visit spanshotVisit in spanshotVisits)
                {
                    if (currentVisit.TicketNumber == spanshotVisit.TicketNumber)
                        isExistInSnapshot = true;
                    else
                        continue;

                    if (currentVisit.GetPrintoutChange(spanshotVisit) != string.Empty)
                    {
                        result.Add(new VisitChangeItem(spanshotVisit, currentVisit));
                        break;
                    }
                }

                if (!isExistInSnapshot)
                    result.Add(new VisitChangeItem(null, currentVisit));
            }

            foreach (Visit spanshotVisit in spanshotVisits)
            {
                bool isExistInCurrent = false;

                foreach (Visit currentVisit in currentVisits)
                {
                    if (currentVisit.TicketNumber == spanshotVisit.TicketNumber)
                    {
                        isExistInCurrent = true;
                        break;
                    }
                }

                if (!isExistInCurrent)
                    result.Add(new VisitChangeItem(spanshotVisit, null));
            }

            return result;            
        }

        #endregion

        #region GetServices

        public List<Service> GetServices()
        {
            return Service.Find();
        }

        #endregion


        #region MarkTimeAs

        //if markAsWorking false - time interval will be marked as free
        public void MarkTimeAs(TimeInterval interval, int technicianId, bool markAsWorking)
        {
            try
            {
                Database.Begin();

                Technician technician = Technician.GetTechnician(technicianId);
                List<TechnicianWorkTime> workingIntervals = new List<TechnicianWorkTime>(
                    technician.WorkingIntervals);
                workingIntervals.Sort(WorkingIntervalsCompare);
                
                if (markAsWorking)
                {
                    List<TechnicianWorkTime> unionableIntervals = new List<TechnicianWorkTime>();

                    foreach (TechnicianWorkTime existingInterval in workingIntervals)
                    {
                        if (existingInterval.GetInterval().Union(interval).Count == 1)
                            unionableIntervals.Add(existingInterval);
                    }

                    if (unionableIntervals.Count == 0)
                    {
                        TechnicianWorkTime workTime = new TechnicianWorkTime(
                            technician.ID,
                            technician.ScheduleDate.Add(interval.Start.TimeOfDay),
                            technician.ScheduleDate.Add(interval.End.TimeOfDay));
                        TechnicianWorkTime.Insert(workTime);
                        workingIntervals.Add(workTime);
                    }
                    else
                    {
                        foreach (TechnicianWorkTime workTime in unionableIntervals)
                        {
                            TechnicianWorkTime.Delete(workTime);
                            workingIntervals.Remove(workTime);
                        }

                        TimeInterval commonInterval = unionableIntervals[0].GetInterval().Union(interval)[0];

                        for (int i = 1; i < unionableIntervals.Count; i++)
                        {
                            commonInterval = commonInterval.Union(unionableIntervals[i].GetInterval())[0];
                        }

                        TechnicianWorkTime commonWorkTime = new TechnicianWorkTime(technician.ID,
                            commonInterval.Start, commonInterval.End);
                        TechnicianWorkTime.Insert(commonWorkTime);
                        workingIntervals.Add(commonWorkTime);
                    }
                }
                else
                {
                    List<TechnicianWorkTime> intervalsToRemove = new List<TechnicianWorkTime>();
                    List<TechnicianWorkTime> intervalsToInsert = new List<TechnicianWorkTime>();

                    foreach (TechnicianWorkTime workTime in workingIntervals)
                    {
                        List<TimeInterval> substraction = workTime.GetInterval().Substract(interval);

                        if (substraction.Count == 1 && substraction[0].Equals(
                            workTime.GetInterval()))
                        {
                            continue;
                        }

                        intervalsToRemove.Add(workTime);
                        if (substraction.Count > 0)
                        {
                            intervalsToInsert.Add(new TechnicianWorkTime(technician.ID,
                                substraction[0].Start, substraction[0].End));
                        }

                        if (substraction.Count > 1)
                        {
                            intervalsToInsert.Add(new TechnicianWorkTime(technician.ID,
                                substraction[1].Start, substraction[1].End));
                        }
                    }

                    foreach (TechnicianWorkTime intervalToRemove in intervalsToRemove)
                    {
                        TechnicianWorkTime.Delete(intervalToRemove);
                        workingIntervals.Remove(intervalToRemove);
                    }

                    foreach (TechnicianWorkTime intervalToInsert in intervalsToInsert)
                    {
                        TechnicianWorkTime.Insert(intervalToInsert);
                        workingIntervals.Add(intervalToInsert);
                    }
                }

                workingIntervals.Sort(WorkingIntervalsCompare);

                WcfServiceBusinessException exception = null;

                if (workingIntervals.Count == 0)
                    exception = new WcfServiceBusinessException("No working hours error", 
                        "Operation cannot be performed because technician's work day is marked as free.\nIf technician is off this day please put him to Inactive list in Technician Arrange.");

                if (workingIntervals.Count > 1)
                    exception = new WcfServiceBusinessException("Discontinuous working interval error",
                        "Discontinuous working hours intervals are not supported for now.\nTechnician working time should be continuous");

                if (exception != null)
                    throw new FaultException<WcfServiceBusinessException>(exception);

                UserAction.WriteAction(GetCurrentUser(), UserActionTypeEnum.TechnicianSettingsEditDaily,
                    technician.TechnicianDefaultId, technician.ScheduleDate,
                    string.Format("Technician working hours changed interactively {0}-{1}",
                    technician.WorkingIntervals[0].TimeStart.ToShortTimeString(),
                    technician.WorkingIntervals[0].TimeEnd.ToShortTimeString()));                
                Database.Commit();
                technician.WorkingIntervals = workingIntervals;

                CallbackInfo info = CallbackInfo.Create(technician.ScheduleDate, CallbackType.TechnicianWorkHours);
                info.TechnicianWorkHoursChange = new TechnicianWorkHoursChangeDetail(technician.ID,
                    technician.WorkingIntervals);
                SendViewChangeCallbacks(info);

                if (IsNeedRearrangeTechnicianSchedule(technician, technician))
                {
                    CallbackInfo info2 = CallbackInfo.Create(technician.ScheduleDate, CallbackType.Visits);
                    info2.VisitsChange = m_bookingEngine.RearrangeVisits(technician, GetCurrentUser());
                    SendViewChangeCallbacks(info2);                    
                }

                if (m_bookingEngine.RefreshBucket(technician.ScheduleDate))
                {
                    CallbackInfo info2 = CallbackInfo.Create(technician.ScheduleDate, CallbackType.VisitsFull);
                    info2.VisitsFullChange = GetFullViewInfo(technician.ScheduleDate);
                    SendViewChangeCallbacks(info2);
                }
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }
        }

        private int WorkingIntervalsCompare(TechnicianWorkTime x, TechnicianWorkTime y)
        {
            return x.TimeStart.CompareTo(y.TimeStart);
        }

        #endregion

        #region UnscheduleVisit

        public void UnscheduleVisit(string ticketNumber)
        {
            try
            {
                Visit visit = m_bookingEngine.Visits.GetVisitByTicketNumber(ticketNumber);
                if (visit == null)
                {
                    Visit dbVisit = null;
                    try
                    {
                        dbVisit = Visit.FindByTicket(ticketNumber);
                    }
                    catch (DataNotFoundException) {}

                    Host.Trace("UnscheduleVisit", string.Format("ERROR!! Trying to Unschedule Visit {0} which doesn't exist. {1}", ticketNumber,
                        dbVisit == null ? "Visit doesn't exist in database." : "Visit exist in DB, ticket number " + dbVisit.TicketNumber));
                    return;
                }
                    
                List<string> greenBucketTicketsBeforeUnschedule = new List<string>();
                foreach (Visit delayedVisit in m_bookingEngine.GetDelayedVisits(visit.ScheduleDate))
                {
                    if (delayedVisit.CanBook)
                        greenBucketTicketsBeforeUnschedule.Add(delayedVisit.TicketNumber);
                }
                
                Database.Begin();
                string changeInstruction;
                List<Visit> modifiedVisits = m_bookingEngine.RemoveVisit(
                    visit, !visit.IsBlockout, GetCurrentUser());
                Database.Commit();

                m_bookingEngine.RefreshBucket(visit.ScheduleDate);
                CallbackInfo info = CallbackInfo.Create(visit.ScheduleDate, CallbackType.Visits);
                info.VisitsChange = new VisitsChangeDetail();
                info.VisitsChange.AddRemovedVisit(visit);
                info.VisitsChange.DashboardModifiedVisits = modifiedVisits;

                foreach (Visit bucketVisit in m_bookingEngine.GetDelayedVisits(visit.ScheduleDate))
                {
                    if (bucketVisit.CanBook && !greenBucketTicketsBeforeUnschedule.Contains(bucketVisit.TicketNumber))
                        m_bookingEngine.MarkVisitUnscheduledManually(bucketVisit);
                }

                SendViewChangeCallbacks(info);                
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }                       
        }

        #endregion

        #region Subscribe / Unsubscribe

        public void Subscribe(WcfSubscriberTypeEnum subscriberType, DateTime scheduleDate, User user, bool allDatesInBucket)
        {
            IWcfSubscriber subscriber = OperationContext.Current.GetCallbackChannel<IWcfSubscriber>();

            if (!m_callbackSubscribers.ContainsKey(subscriber))
            {
                Host.TraceUserAction("New client subscribed");                
                m_callbackSubscribers.Add(subscriber, new WcfSubscriberInfo(subscriberType, scheduleDate, user, allDatesInBucket));
            }                
            else
            {
                WcfSubscriberInfo oldSubscriberInfo = m_callbackSubscribers[subscriber];
                if (oldSubscriberInfo.ScheduleDate.Date != scheduleDate.Date)
                    Host.TraceUserAction("Date changed " + scheduleDate.Date.ToShortDateString());
                if (oldSubscriberInfo.IsAllDatesInBucket != allDatesInBucket)
                    Host.TraceUserAction("Bucket showing option changed. Show all dates = " + allDatesInBucket);

                m_callbackSubscribers[subscriber] = new WcfSubscriberInfo(subscriberType, scheduleDate, user, allDatesInBucket);
            }                
        }
        
        public void Unsubscribe()
        {
            IWcfSubscriber subscriber = OperationContext.Current.GetCallbackChannel<IWcfSubscriber>();
            Unsubscribe(subscriber);
        }

        private void Unsubscribe(IWcfSubscriber subscriber)
        {
            if (m_callbackSubscribers.ContainsKey(subscriber))
                m_callbackSubscribers.Remove(subscriber);            
        }

        #endregion

        #region SendCallbacks

        private void SendViewChangeCallbacks(CallbackInfo info, bool ignoreCaller)
        {
            IWcfSubscriber subscriberToIgnore = null;
            if (ignoreCaller)
                subscriberToIgnore = OperationContext.Current.GetCallbackChannel<IWcfSubscriber>();

            if (info.ChangeType == CallbackType.Visits)
            {
                info.VisitsChange.BucketVisits = m_bookingEngine.GetDelayedVisits(info.Date);
                info.VisitsChange.TemporaryAssignedVisits = m_bookingEngine.GetTemporaryAssignedVisits(info.Date);                
            }

            List<IWcfSubscriber> subscribersToRemove = new List<IWcfSubscriber>();

            foreach (var subscriber in m_callbackSubscribers.Keys)
            {
                if (((ICommunicationObject)subscriber).State != CommunicationState.Opened)
                {
                    subscribersToRemove.Add(subscriber);
                    continue;
                }

                if (m_callbackSubscribers[subscriber].Type != WcfSubscriberTypeEnum.Dashboard)
                    continue;

                if (subscriberToIgnore != null && subscriberToIgnore == subscriber)
                {
                    CallbackInfo emptyInfo = info.Copy(CallbackType.Empty);
                    subscriber.OnViewModelChanged(emptyInfo);
                    continue;
                }


                if (m_callbackSubscribers[subscriber].IsAllDatesInBucket && info.ChangeType == CallbackType.Visits)
                {
                    CallbackInfo newCallbackInfo =
                        CallbackInfo.Create(m_callbackSubscribers[subscriber].ScheduleDate, CallbackType.Visits);                 

                    if (info.Date != m_callbackSubscribers[subscriber].ScheduleDate)
                    {
                        newCallbackInfo.VisitsChange = new VisitsChangeDetail();                        
                        newCallbackInfo.VisitsChange.TemporaryAssignedVisits = m_bookingEngine.GetTemporaryAssignedVisits(
                            m_callbackSubscribers[subscriber].ScheduleDate);                        
                    }
                    else
                    {
                        newCallbackInfo.VisitsChange = new VisitsChangeDetail();
                        newCallbackInfo.VisitsChange.DashboardAddedVisits = info.VisitsChange.DashboardAddedVisits;
                        newCallbackInfo.VisitsChange.DashboardRemovedVisits = info.VisitsChange.DashboardRemovedVisits;
                        newCallbackInfo.VisitsChange.DashboardModifiedVisits = info.VisitsChange.DashboardModifiedVisits;
                        newCallbackInfo.VisitsChange.IsOperationFailed = info.VisitsChange.IsOperationFailed;
                        newCallbackInfo.VisitsChange.TemporaryAssignedVisits = info.VisitsChange.TemporaryAssignedVisits;                     
                    }

                    newCallbackInfo.VisitsChange.BucketVisits = m_bookingEngine.GetDelayedVisits();
                    subscriber.OnViewModelChanged(newCallbackInfo);
                }
                else if (info.Date != m_callbackSubscribers[subscriber].ScheduleDate)
                    continue;
                    
                subscriber.OnViewModelChanged(info);                
            }

            foreach (var subscriber in subscribersToRemove)
                Unsubscribe(subscriber);
        }

        private void SendViewChangeCallbacks(CallbackInfo info)
        {
            SendViewChangeCallbacks(info, false);
        }

        #endregion

        #region UpdateVisit

        public string UpdateVisit(Visit visit, bool callbackCaller, bool allowCollisions)
        {
            Visit serverVisit = m_bookingEngine.Visits.GetVisitByTicketNumber(visit.TicketNumber);
            if (serverVisit == null)
            {
                Visit dbVisit = Visit.FindByTicket(visit.TicketNumber);
                Host.Trace("UpdateVisit", string.Format("ERROR!! Trying to modify Visit {0} which doesn't exist. {1}", visit.TicketNumber,
                    dbVisit == null ? "Visit doesn't exist in database." : "Visit exist in DB"));
                return string.Empty;
            }

            if (serverVisit.IsBlockout && IsBlockoutCollisionExist(visit.ExclusiveTechnicianDefaultId.Value,
                visit.TimeStart, visit.TimeEnd, visit))
            {
                return "Unable to modify blockout because of collision with existing blockout";
            }
                            
            try
            {               
                Database.Begin();
                if (serverVisit.TechnicianId != visit.TechnicianId || serverVisit.TimeStart != visit.TimeStart
                    || serverVisit.TimeEnd != visit.TimeEnd)
                {
                    string change = string.Empty;
                    if (serverVisit.TimeStart != visit.TimeStart) 
                        change = string.Format("Time Start {0} -> {1}; ", 
                            serverVisit.TimeStart.ToShortTimeString(), visit.TimeStart.ToShortTimeString());

                    if (serverVisit.DurationMin != visit.DurationMin) 
                        change += string.Format("Duration {0} -> {1}; ", serverVisit.DurationMin, visit.DurationMin);

                    if (serverVisit.TechnicianId != visit.TechnicianId)
                        change += string.Format("Technician {0} -> {1}; ", serverVisit.TechnicianName, visit.TechnicianName);

                    if (change != string.Empty)
                        change = change.Substring(0, change.Length - 2);

                    UserAction.WriteActionEx(GetCurrentUser(), 
                        visit.IsBlockout ? UserActionTypeEnum.Blockout : UserActionTypeEnum.Visit,
                        visit.IsBlockout ? visit.Technician : null, 
                        visit,
                        string.Format("{0} changed manually. {1}. {2}",
                        visit.IsBlockout ? "Blockout" : "Ticket", change,                        
                        !visit.IsWithinAllowedInterval ? "OUT OF ALLOWED INTERVAL" : string.Empty));
                }

                serverVisit.TechnicianId = visit.TechnicianId;
                serverVisit.TimeStart = visit.TimeStart;
                serverVisit.TimeEnd = visit.TimeEnd;
                serverVisit.DurationCost = visit.DurationCost;
                if (serverVisit.IsBlockout)
                    serverVisit.Note = visit.Note;
                Visit.Update(visit);
                Database.Commit();
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }

            bool needRearrangement = false;
            if (!allowCollisions)
            {
                List<Visit> technicianVisits = m_bookingEngine.GetSortedVisits(serverVisit.Technician);
                for (int i = 1; i < technicianVisits.Count; i++)
                {
                    if (technicianVisits[i].VisitInterval.IsIntersects(technicianVisits[i - 1].VisitInterval))
                    {
                        needRearrangement = true;
                        m_bookingEngine.RearrangeVisits(serverVisit.Technician, GetCurrentUser());
                        break;
                    }
                }
            }

            if (needRearrangement || m_bookingEngine.RefreshBucket(visit.ScheduleDate))
            {
                CallbackInfo info = CallbackInfo.Create(visit.ScheduleDate, CallbackType.VisitsFull);
                info.VisitsFullChange = GetFullViewInfo(visit.ScheduleDate);                
                SendViewChangeCallbacks(info);                
            }
            else
            {
                CallbackInfo callbackInfo = CallbackInfo.Create(serverVisit.ScheduleDate, CallbackType.Visit);
                callbackInfo.VisitChange = serverVisit;
                SendViewChangeCallbacks(callbackInfo, !callbackCaller);                                
            }

            return string.Empty;
        }

        #endregion

        #region BookDelayedVisit

        public bool BookDelayedVisit(int visitId, RecommendationResponseItem recommendationItem)
        {            
            Visit dbVisit = Visit.FindByPrimaryKey(visitId);
            Visit visit = null;

            foreach (var delayedVisit in m_bookingEngine.GetDelayedVisits(dbVisit.ScheduleDate))
            {
                if (delayedVisit.ID == visitId)
                {
                    visit = delayedVisit;
                    break;
                }
            }

            try
            {
                Database.Begin();
                List<VisitsChangeDetail> changeDetails = m_bookingEngine.BookDelayedVisit(
                    visit, recommendationItem, GetCurrentUser());
                Database.Commit();

                foreach (VisitsChangeDetail changeDetail in changeDetails)
                {
                    CallbackInfo info = CallbackInfo.Create(changeDetail.AffectedDates[0], CallbackType.Visits);
                    info.VisitsChange = changeDetail;
                    SendViewChangeCallbacks(info);                    
                }

                return !changeDetails[0].IsOperationFailed;
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }
        }

        #endregion

        #region GetBucketProcessingOptions

        public BucketProcessingOptions GetBucketProcessingOptions(int bucketVisitId)
        {
            return new BucketProcessingOptions(m_bookingEngine, bucketVisitId);
        }

        #endregion

        #region Bucket processing

        #region ProcessDelayedVisitTempExclusivity

        public void ProcessDelayedVisitTempExclusivity(DelayedVisitSaveInfo saveInfo, int tempExclusiveTechnicianId)
        {
            try
            {
                Database.Begin();

                Technician technician = Technician.GetTechnician(tempExclusiveTechnicianId);
                Visit serverDelayedVisit = FindServerDelayedVisit(saveInfo.DelayedVisit);
                SaveDelayedVisit(saveInfo, serverDelayedVisit);
                Visit newVisit = m_bookingEngine.GetNewVisit(serverDelayedVisit, true, technician);

                Visit.DeleteWithDetails(serverDelayedVisit);
                VisitInsertResult insertResult = m_bookingEngine.InsertVisit(newVisit);

                insertResult.UserAction.User = GetCurrentUser();
                insertResult.UserAction.UserActionType = UserActionTypeEnum.BucketResolve;
                insertResult.UserAction.Text = "Bucket visit resolved with temporary exclusivity. " +
                    insertResult.UserAction.Text;
                UserAction.Insert(insertResult.UserAction);
                Database.Commit();
                SendVisitInsertionCallback(insertResult);
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }
        }

        #endregion

        #region ProcessDelayedVisitIgnoreExclusivity

        public void ProcessDelayedVisitIgnoreExclusivity(DelayedVisitSaveInfo saveInfo)
        {
            try
            {
                Database.Begin();

                Visit serverDelayedVisit = FindServerDelayedVisit(saveInfo.DelayedVisit);
                SaveDelayedVisit(saveInfo, serverDelayedVisit);

                Visit newVisit = m_bookingEngine.GetNewVisit(serverDelayedVisit, true, null);
                Visit.DeleteWithDetails(serverDelayedVisit);
                VisitInsertResult insertResult = m_bookingEngine.InsertVisit(newVisit);

                insertResult.UserAction.User = GetCurrentUser();
                insertResult.UserAction.UserActionType = UserActionTypeEnum.BucketResolve;
                insertResult.UserAction.Text = "Bucket visit resolved with ignoring exclusivity. " +
                    insertResult.UserAction.Text;
                UserAction.Insert(insertResult.UserAction);
                Database.Commit();
                SendVisitInsertionCallback(insertResult);
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }
        }

        #endregion

        #region ProcessDelayedVisitChangeFrame

        public void ProcessDelayedVisitChangeFrame(DelayedVisitSaveInfo saveInfo, VisitAddResult frameChangeOption)
        {
            try
            {
                Database.Begin();

                Visit serverDelayedVisit = FindServerDelayedVisit(saveInfo.DelayedVisit);
                SaveDelayedVisit(saveInfo, serverDelayedVisit);
                VisitInsertResult insertResult;

                Visit newVisit = m_bookingEngine.GetNewVisit(serverDelayedVisit, false, null);
                if (!newVisit.ServmanBaseTimeFrameId.HasValue)
                    newVisit.ServmanBaseTimeFrameId = newVisit.ConfirmedTimeFrame.ID;
                newVisit.ConfirmedTimeFrame = frameChangeOption.OtherFrame;

                Visit.DeleteWithDetails(serverDelayedVisit);
                insertResult = m_bookingEngine.InsertVisit(newVisit);

                insertResult.UserAction.User = GetCurrentUser();
                insertResult.UserAction.UserActionType = UserActionTypeEnum.BucketResolve;
                insertResult.UserAction.Text = string.Format("Bucket resolved with TimeFrame change ({0})->({1}). ", 
                    serverDelayedVisit.ConfirmedTimeFrame.Text, newVisit.ConfirmedTimeFrame.Text) +
                    insertResult.UserAction.Text;
                UserAction.Insert(insertResult.UserAction);

                Database.Commit();

                SendVisitInsertionCallback(insertResult);
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }
        }

        #endregion

        #region ProcessDelayedVisitExtendWorkingHours

        public void ProcessDelayedVisitExtendWorkingHours(DelayedVisitSaveInfo saveInfo, 
            WorkingHoursExtensionResult extension)
        {   
            try
            {
                Database.Begin();

                Visit serverDelayedVisit = FindServerDelayedVisit(saveInfo.DelayedVisit);
                SaveDelayedVisit(saveInfo, serverDelayedVisit);
                Technician extensionTechnician = Technician.GetTechnician(extension.Technician.ID);

                TechnicianWorkTime.Delete(extensionTechnician.WorkingIntervals[0]);
                extensionTechnician.WorkingIntervals[0] = extension.NewWorkInterval;
                TechnicianWorkTime.Insert(extensionTechnician.WorkingIntervals[0]);

                Visit newVisit = m_bookingEngine.GetNewVisit(serverDelayedVisit, false, null);
                Visit.DeleteWithDetails(serverDelayedVisit);
                VisitInsertResult insertResult = m_bookingEngine.InsertVisit(newVisit);

                Visit fixedVisit;
                if (extension.VisitInExtendedHours.TicketNumber == newVisit.TicketNumber)
                    fixedVisit = newVisit;
                else
                    fixedVisit = m_bookingEngine.Visits.GetVisitByTicketNumber(extension.VisitInExtendedHours.TicketNumber);
                fixedVisit.IsFixed = true;
                Visit.Update(fixedVisit);

                insertResult.UserAction.User = GetCurrentUser();
                insertResult.UserAction.UserActionType = UserActionTypeEnum.BucketResolve;
                insertResult.UserAction.TechnicianDefaultId = extension.Technician.TechnicianDefaultId;
                insertResult.UserAction.Text = string.Format("Bucket resolved with working hours extension for {0} {1}. ",
                    extension.Technician.Name, extension.NewWorkInterval.GetInterval()) + insertResult.UserAction.Text;
                UserAction.Insert(insertResult.UserAction);

                Database.Commit();

                CallbackInfo technicianChangeInfo = CallbackInfo.Create(saveInfo.DelayedVisit.ScheduleDate, 
                    CallbackType.Technician);
                technicianChangeInfo.TechnicianChange = extensionTechnician;
                SendViewChangeCallbacks(technicianChangeInfo);

                SendVisitInsertionCallback(insertResult);
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }
        }

        #endregion        

        #region SaveDelayedVisit

        public void SaveDelayedVisit(DelayedVisitSaveInfo saveInfo)
        {
            try
            {
                Database.Begin();
                Visit serverDelayedVisit = FindServerDelayedVisit(saveInfo.DelayedVisit);
                bool isDelayedVisitChanged = SaveDelayedVisit(saveInfo, serverDelayedVisit);
                UserAction.WriteAction(GetCurrentUser(), UserActionTypeEnum.Visit, serverDelayedVisit,
                    string.Format("Delayed visit modified. New Duration Cost {0}", serverDelayedVisit.DurationCost));
                Database.Commit();                

                if (isDelayedVisitChanged)
                {
                    m_bookingEngine.RefreshBucket(serverDelayedVisit.ScheduleDate);
                    CallbackInfo info = CallbackInfo.Create(serverDelayedVisit.ScheduleDate, CallbackType.Visits);
                    info.VisitsChange = new VisitsChangeDetail();
                    SendViewChangeCallbacks(info);
                }
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }
        }

        #endregion


        private void SendVisitInsertionCallback(VisitInsertResult result)
        {
            DateTime scheduleDate = result.InsertionVisit.ScheduleDate;
            CallbackInfo info = CallbackInfo.Create(scheduleDate, CallbackType.Visits);
            info.VisitsChange = new VisitsChangeDetail();
            if (result.IsInsertSucceed)
            {
                info.VisitsChange.AddAddedVisit(result.InsertionVisit);
                info.VisitsChange.DashboardModifiedVisits.AddRange(result.ModifiedVisits);
            }

            m_bookingEngine.RefreshBucket(scheduleDate);
            SendViewChangeCallbacks(info);            
        }

        private bool SaveDelayedVisit(DelayedVisitSaveInfo saveInfo, Visit serverDelayedVisit)
        {
            bool isDurationCostChanged = false;

            if (serverDelayedVisit.DurationCost != saveInfo.DelayedVisit.DurationCost)
            {
                serverDelayedVisit.DurationCost = saveInfo.DelayedVisit.DurationCost;
                Visit.Update(serverDelayedVisit);
                isDurationCostChanged = true;
            }

            foreach (var visitId in saveInfo.DoNotCallVisitIds)
            {
                Visit visit = m_bookingEngine.FindServerVisit(visitId, serverDelayedVisit.ScheduleDate);
                visit.IsCalledCustomer = true;
                Visit.Update(visit);
            }

            return isDurationCostChanged;
        }

        private Visit FindServerDelayedVisit(Visit delayedVisit)
        {
            foreach (var visit in m_bookingEngine.GetDelayedVisits(delayedVisit.ScheduleDate))
            {
                if (visit.ID == delayedVisit.ID)
                    return visit;
            }

            return null;
        }

        #endregion

        #region KeepAliveDummy

        public void KeepAliveDummy()
        {
            return;
        }

        #endregion

        #region Optimization

        public void EnqueueOptimization(DateTime date)
        {   
            EnqueueOptimizationStatic(date);
        }

        public static void EnqueueOptimizationStatic(DateTime date)
        {
            if (!m_pendingOptimizationDates.Contains(date.Date))
            {
                Host.Trace("Optimization", string.Format("Date {0} enqueued", date.Date.ToShortDateString()));
                m_pendingOptimizationDates.Add(date.Date);
            }                            
        }

        public void ProcessPendingOptimizations()
        {
            if (m_pendingOptimizationDates.Count == 0)
                return;

            Dictionary<IWcfSubscriber, WcfSubscriberInfo> subscribersToRemove 
                = new Dictionary<IWcfSubscriber, WcfSubscriberInfo>();

            foreach (var subscriber in m_callbackSubscribers)
            {
                if (subscriber.Value.Type != WcfSubscriberTypeEnum.Optimizer)
                    continue;

                if (((ICommunicationObject)subscriber.Key).State != CommunicationState.Opened)
                {
                    subscribersToRemove.Add(subscriber.Key, subscriber.Value);
                    continue;
                }

                if (subscriber.Value.IsNotResponding)
                {
                    subscribersToRemove.Add(subscriber.Key, subscriber.Value);
                    continue;
                }                    

                if (!subscriber.Value.IsBusy)
                {
                    subscriber.Value.SetBusy(m_pendingOptimizationDates[0]);
                    Schedule schedule = Schedule.Create(m_bookingEngine, m_pendingOptimizationDates[0]);
                    m_pendingOptimizationDates.RemoveAt(0);
                    Host.Trace("Optimization", string.Format("Optimization request sent for date {0}", 
                        schedule.ScheduleDate.ToShortDateString()));
                    subscriber.Key.OnOptimizationRequested(schedule);   
                    break;
                }
            }

            foreach (var subscriber in subscribersToRemove)
            {
                EnqueueOptimization(subscriber.Value.ScheduleDate);
                Unsubscribe(subscriber.Key);
            }
        }

        public void ApplyOptimizationResult(Schedule schedule)
        {
            IWcfSubscriber currentSubscriber = OperationContext.Current.GetCallbackChannel<IWcfSubscriber>();
            m_callbackSubscribers[currentSubscriber].SetIdle();

            if (schedule.IsEmpty)
            {                
                Host.Trace("Optimization", 
                    string.Format("Optimization response received for date {0} - better schedule no found",
                    m_callbackSubscribers[currentSubscriber].ScheduleDate));
                return;
            }                

            if (schedule.IsScheduleOutdated(m_bookingEngine))
            {
                EnqueueOptimization(schedule.ScheduleDate);
                Host.Trace("Optimization",
                    string.Format("Optimization result for date {0} is outdated. This date will be optimized later",
                    m_callbackSubscribers[currentSubscriber].ScheduleDate));
                return;
            }

            try
            {
                Database.Begin();
                schedule.Apply(m_bookingEngine);
                Database.Commit();

                Host.Trace("Optimization",
                    string.Format("Optimization response received for date {0} - new schedule applied",
                    m_callbackSubscribers[currentSubscriber].ScheduleDate));
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }

            CallbackInfo info = CallbackInfo.Create(schedule.ScheduleDate, CallbackType.VisitsFull);
            info.VisitsFullChange = GetFullViewInfo(schedule.ScheduleDate);
            SendViewChangeCallbacks(info);
        }

        #endregion

        #region CanInsertVisit

        public VisitAddResult CanInsertVisit(int visitId)
        {
            Visit dbVisit = Visit.FindByPrimaryKey(visitId);
            Visit visit = null;
            foreach (var delayedVisit in m_bookingEngine.GetDelayedVisits(dbVisit.ScheduleDate))
            {
                if (delayedVisit.ID == visitId)
                {
                    visit = delayedVisit;
                    break;
                }
            }

            visit = m_bookingEngine.GetNewVisit(visit, false, null);
            return m_bookingEngine.CanInsertVisit(visit, false);
        }

        #endregion

        #region GetRecommendations

        public List<RecommendationResponseItem> GetRecommendations(RecommendationRequest request)
        {
            return RecommendationResponseItem.GetResponseItems(request, m_bookingEngine);
        }

        public List<RecommendationResponseItem> GetRecommendationsWeb(RecommendationRequest request)
        {
            return GetRecommendations(request);
        }

        #endregion

        #region RunSync

        public void RunSync(SyncTypeEnum syncType)
        {
            foreach (var subscriber in m_callbackSubscribers)
            {
                if (subscriber.Value.Type != WcfSubscriberTypeEnum.Sync
                    || ((ICommunicationObject)subscriber.Key).State != CommunicationState.Opened)
                {
                    continue;
                }
                    
                subscriber.Key.ForceSync(syncType);
                break;
            }
        }

        #endregion

        #region ApplyServmanData

        public void ApplyServmanData(List<Order> orders)
        {
            if (orders != null)
                ImportTickets(orders);
        }

        #endregion

        #region ModifyPredictionIgnoreDate

        public void ModifyPredictionIgnoreDate(bool isSuspend)
        {
            IWcfSubscriber currentSubscriber = OperationContext.Current.GetCallbackChannel<IWcfSubscriber>();            
            PredictionIgnore.ModifyIgnoreDate(m_callbackSubscribers[currentSubscriber].ScheduleDate, isSuspend);
            UserAction.WriteAction(GetCurrentUser(), UserActionTypeEnum.SuspendRecommendationsModify, 
                null, m_callbackSubscribers[currentSubscriber].ScheduleDate, 
                isSuspend ? "Recommendations suspended" : "Recommendations resumed after suspension");
        }

        #endregion

        #region CreateBlockout

        private bool IsBlockoutCollisionExist(int technicianDefaultId, DateTime timeStart, DateTime timeEnd, 
            Visit ignoreVisit)
        {
            Technician technician = Technician.GetTechnician(timeStart.Date, technicianDefaultId);
            List<Visit> visits = m_bookingEngine.GetSortedVisits(technician);
            TimeInterval newBlockoutInterval = new TimeInterval(timeStart, timeEnd);
            foreach (var visit in visits)
            {
                if (visit.IsBlockout &&
                    (ignoreVisit == null || visit.TicketNumber != ignoreVisit.TicketNumber)
                    && newBlockoutInterval.IsIntersects(visit.VisitInterval))
                {
                    return true;
                }
            }
            return false;
        }

        public string CreateBlockout(int technicianDefaultId, DateTime timeStart, DateTime timeEnd, string note)
        {
            if (IsBlockoutCollisionExist(technicianDefaultId, timeStart, timeEnd, null))
                return "Unable to create new blockout because of collision with existing blockout";

            try
            {
                Database.Begin();                
                Technician technician = Technician.GetTechnician(timeStart.Date, technicianDefaultId);
                
                Visit blockOutVisit = new Visit(technician, timeStart, TimeFrame.TimeFrames[18], 0, 0, string.Empty,
                    decimal.Zero, null, technician.TechnicianDefaultId, null, false, null, Guid.NewGuid().ToString().Substring(0, 10),
                    new List<VisitDetail>(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                    DateTime.Now, false, false, false, string.Empty, string.Empty, string.Empty, false, false, string.Empty,
                    0, string.Empty, 0, null, null, string.Empty, null, note, false, string.Empty, null,
                    decimal.Zero, decimal.Zero, decimal.Zero, true);
                blockOutVisit.TimeEnd = timeEnd;

                bool needsRearrange = !m_bookingEngine.CanInsertVisit(blockOutVisit);
                VisitInsertResult insertResult = m_bookingEngine.InsertVisit(blockOutVisit);
                VisitsChangeDetail rearrangeDetail = null;
                UserAction.WriteAction(GetCurrentUser(), UserActionTypeEnum.Blockout, technician.TechnicianDefaultId,
                    technician.ScheduleDate, string.Format("Blockout created, time interval {0} - {1}",
                    timeStart.ToShortTimeString(), timeEnd.ToShortTimeString()));
                if (needsRearrange)
                    rearrangeDetail = m_bookingEngine.RearrangeVisits(technician, GetCurrentUser());
                Database.Commit();

                if (rearrangeDetail != null)
                {
                    CallbackInfo info = CallbackInfo.Create(technician.ScheduleDate, CallbackType.VisitsFull);
                    info.VisitsFullChange = GetFullViewInfo(technician.ScheduleDate);
                    SendViewChangeCallbacks(info);
                }
                else
                    SendVisitInsertionCallback(insertResult);
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }

            return string.Empty;
        }

        #endregion

        #region SendErrorEmail

        public void SendErrorEmail()
        {
            EmailSender.SendErrorNotifications();
        }

        #endregion

        #region FindUser

        public UserResult FindUser(string passwordHash)
        {
            UserResult userResult = User.FindUser(passwordHash);
            if (userResult.User != null)
            {
                IWcfSubscriber subscriber = OperationContext.Current.GetCallbackChannel<IWcfSubscriber>();
                m_callbackSubscribers[subscriber].User = userResult.User;
            }

            return userResult;
        }

        #endregion

        #region GetCurrentUser

        private User GetCurrentUser()
        {
            IWcfSubscriber subscriber = OperationContext.Current.GetCallbackChannel<IWcfSubscriber>();

            if (m_callbackSubscribers[subscriber].User == null)
                return User.Anonymous;
            return m_callbackSubscribers[subscriber].User;
        }

        #endregion

        #region Action Report

        public List<User> FindUsers()
        {
            List<User> users = User.Find();
            foreach (var user in users)
            {
                if (user.UserRole == UserRoleEnum.Anonymous)
                {
                    users.Remove(user);
                    break;
                }                    
            }

            return users;
        }

        public List<UserAction> FindUserActions(int? userId, UserActionTypeEnum? actionType, int? technicianDefaultId,
            string ticket, DateTime? dashboardDate, TimeInterval actionDateInterval, SortField sortField, bool isSortAscending)
        {
            return UserAction.FindBy(userId, actionType, technicianDefaultId, ticket, dashboardDate, 
                actionDateInterval, sortField, isSortAscending);
        }

        #endregion

        #region AddEditUser

        public string AddEditUser(User user)
        {
            User samePasswordUser = User.FindByPasswordHash(user.Password);
            if (samePasswordUser != null && user.ID != samePasswordUser.ID)
                return "Please use another password";

            try
            {
                Database.Begin();
                if (user.ID == 0)
                    User.Insert(user);
                else
                    User.Update(user);
                Database.Commit();
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }

            return string.Empty;
        }

        #endregion
    }    
}
