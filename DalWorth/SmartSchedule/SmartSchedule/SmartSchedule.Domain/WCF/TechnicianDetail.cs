using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace SmartSchedule.Domain.WCF
{
    [DataContract]
    public class TechnicianDetail
    {
        #region Constructor

        public TechnicianDetail(Technician technician, IEnumerable<TechnicianService> services,
            IEnumerable<TechnicianWorkTimeDefaultPreset> workingHoursPresets)
        {
            m_technician = technician;
            m_services = services;
            m_workingHoursPresets = workingHoursPresets;
        }

        #endregion

        #region Technician

        private Technician m_technician;
        [DataMember]
        public Technician Technician
        {
            get { return m_technician; }
            set { m_technician = value; }
        }

        #endregion

        #region Services

        private IEnumerable<TechnicianService> m_services;
        [DataMember]
        public IEnumerable<TechnicianService> Services
        {
            get { return m_services; }
            set { m_services = value; }
        }

        #endregion

        #region WorkingHoursPresets

        private IEnumerable<TechnicianWorkTimeDefaultPreset> m_workingHoursPresets;
        [DataMember]
        public IEnumerable<TechnicianWorkTimeDefaultPreset> WorkingHoursPresets
        {
            get { return m_workingHoursPresets; }
            set { m_workingHoursPresets = value; }
        }

        #endregion

        #region IsDirty

        private bool m_isDirty;
        public bool IsDirty
        {
            get { return m_isDirty; }
            set { m_isDirty = value; }
        }

        #endregion

        #region Email

        private string m_email;
        [DataMember]
        public string Email
        {
            get { return m_email; }
            set { m_email = value; }
        }

        #endregion

        #region PrimaryZipCodesText

        private string m_primaryZipCodesText;
        [DataMember]
        public string PrimaryZipCodesText
        {
            get { return m_primaryZipCodesText; }
            set { m_primaryZipCodesText = value; }
        }

        #endregion

        #region SecondaryZipCodesText

        private string m_secondaryZipCodesText;
        [DataMember]
        public string SecondaryZipCodesText
        {
            get { return m_secondaryZipCodesText; }
            set { m_secondaryZipCodesText = value; }
        }

        #endregion

        #region PopulateZipCodesStrings

        public void PopulateZipCodesStrings()
        {
            m_primaryZipCodesText = GetTechnicianZipString(true);
            m_secondaryZipCodesText = GetTechnicianZipString(false);
        }

        private string GetTechnicianZipString(bool isPrimary)
        {
            string result = string.Empty;

            List<string> zips;
            if (isPrimary)
                zips = new List<string>(m_technician.PrimaryZipCodes);
            else
                zips = new List<string>(m_technician.SecondaryZipCodes);

            zips.Sort(delegate(string s, string s1) { return s.CompareTo(s1); });

            foreach (string zip in zips)
                result += ", " + zip;

            if (result != string.Empty)
                return result.Substring(2);
            return result;
        }

        #endregion

        #region GetTechnicianDefaultClone

        public TechnicianDetail GetTechnicianDefaultClone(DateTime date)
        {
            TechnicianDetail result = new TechnicianDetail(null, null, null);
            result.Technician = (Technician)Technician.Clone();
            result.Technician.ScheduleDate = date.Date;
            result.Technician.PrimaryZipCodes = new List<string>(Technician.PrimaryZipCodes);
            result.Technician.SecondaryZipCodes = new List<string>(Technician.SecondaryZipCodes);
            result.PopulateZipCodesStrings();

            result.Technician.WorkingIntervals = new List<TechnicianWorkTime>();
            foreach (TechnicianWorkTime workTime in Technician.WorkingIntervals)
                result.Technician.WorkingIntervals.Add((TechnicianWorkTime)workTime.Clone());

            List<TechnicianService> servicesCopy = new List<TechnicianService>();
            foreach (TechnicianService service in Services)
                servicesCopy.Add((TechnicianService)service.Clone());

            result.Services = new BindingList<TechnicianService>(servicesCopy);
            return result;
        }

        #endregion
    }

    [DataContract]
    public class TechnicianDetailValidationError
    {
        #region Constructors

        private TechnicianDetailValidationError() {}

        private TechnicianDetailValidationError(DateTime scheduleDate, ErrorFieldEnum errorField, 
            string errorText)
        {
            m_scheduleDate = scheduleDate;
            m_errorField = errorField;
            m_errorText = errorText;
        }

        #endregion


        #region ErrorFieldEnum

        [DataContract]
        public enum ErrorFieldEnum
        {
            [EnumMember]
            General,
            [EnumMember]
            Name,
            [EnumMember]
            ServmanId,
            [EnumMember]
            DepotAddress,
            [EnumMember]
            DriveTimeMinutes,
            [EnumMember]
            MaxNonExclusiveVisitsCount,
            [EnumMember]
            MaxVisitsCount,
            [EnumMember]
            WorkTime,
            [EnumMember]
            WorkTimeStart,
            [EnumMember]
            WorkTimeEnd,
            [EnumMember]
            HourlyRate,
            [EnumMember]
            HourlyRate150To300,
            [EnumMember]
            HourlyRateMore300,
            [EnumMember]
            PrimaryZipCodes,
            [EnumMember]
            SecondaryZipCodes,
            [EnumMember]
            Services,
            [EnumMember]
            Email
        }

        #endregion

        #region ScheduleDate

        private DateTime m_scheduleDate;
        [DataMember]
        public DateTime ScheduleDate
        {
            get { return m_scheduleDate; }
            set { m_scheduleDate = value; }
        }

        #endregion

        #region ErrorField

        private ErrorFieldEnum m_errorField;
        [DataMember]
        public ErrorFieldEnum ErrorField
        {
            get { return m_errorField; }
            set { m_errorField = value; }
        }

        #endregion

        #region ErrorText

        private string m_errorText;
        [DataMember]
        public string ErrorText
        {
            get { return m_errorText; }
            set { m_errorText = value; }
        }

        #endregion

        #region GetErrors

        public static TechnicianDetailValidationError CreateError(DateTime scheduleDate, string error)
        {
            return new TechnicianDetailValidationError(scheduleDate,
                ErrorFieldEnum.General, error);
        }

        public static List<TechnicianDetailValidationError> GetErrors(TechnicianDetail modifiedDetail, bool defaultSettings)
        {
            return GetErrors(modifiedDetail, null, false, defaultSettings, null);
        }

        public static List<TechnicianDetailValidationError> GetErrorsDeep(TechnicianDetail modifiedDetail,
            TechnicianDetail existingDetail, bool defaultSettings, IWcfService service)
        {
            return GetErrors(modifiedDetail, existingDetail, true, defaultSettings, service);
        }

        private static List<TechnicianDetailValidationError> GetErrors(TechnicianDetail modifiedDetail,
            TechnicianDetail existingDetail, bool deepValidation, bool defaultSettings, IWcfService service)
        {
            DateTime scheduleDate = modifiedDetail.Technician.ScheduleDate;

            List<TechnicianDetailValidationError> result = new List<TechnicianDetailValidationError>();
            if (modifiedDetail.Technician.Name.Trim() == string.Empty)
                result.Add(new TechnicianDetailValidationError(scheduleDate, ErrorFieldEnum.Name, 
                    "Please enter Technician name"));

            if (modifiedDetail.Technician.ServmanId == string.Empty)
                result.Add(new TechnicianDetailValidationError(scheduleDate, ErrorFieldEnum.ServmanId, 
                    "Please enter Servman Id (Tech_id)"));

            if (deepValidation && modifiedDetail.Technician.TechnicianDefaultId == 0)
            {
                TechnicianDefault duplicateServmanIdTech = Technician.GetTechnician(modifiedDetail.Technician.ServmanId);

                if (duplicateServmanIdTech != null)
                    result.Add(new TechnicianDetailValidationError(scheduleDate, ErrorFieldEnum.ServmanId,
                        string.Format("This Servman Id is already used by {0}. Please use another Id", duplicateServmanIdTech.Name)));
            }

            if (modifiedDetail.Technician.DepotAddress.Trim() == string.Empty)
                result.Add(new TechnicianDetailValidationError(scheduleDate, ErrorFieldEnum.DepotAddress,
                    "Please enter Start/End address"));

            if (defaultSettings && modifiedDetail.Email == string.Empty)
                result.Add(new TechnicianDetailValidationError(scheduleDate, ErrorFieldEnum.Email,
                    "Please enter email address"));

            if (deepValidation && (existingDetail == null || modifiedDetail.Technician.DepotAddress != existingDetail.Technician.DepotAddress
                || modifiedDetail.Technician.DepotLatitude == 0 || modifiedDetail.Technician.DepotLongitude == 0))
            {
                Coordinate depot = service.GecocodeAddress(modifiedDetail.Technician.DepotAddress);

                if (depot.IsValid)
                {
                    modifiedDetail.Technician.DepotLatitude = depot.Latitude;
                    modifiedDetail.Technician.DepotLongitude = depot.Longitude;
                }
                else
                    result.Add(new TechnicianDetailValidationError(scheduleDate, ErrorFieldEnum.DepotAddress,
                        "Unable to geocode address: " + depot.ErrorText));
            }

            if (modifiedDetail.Technician.DriveTimeMinutes < 0)
                result.Add(new TechnicianDetailValidationError(scheduleDate, ErrorFieldEnum.DriveTimeMinutes,
                    "Drive time should be positive"));

            if (modifiedDetail.Technician.MaxVisitsCount < 0)
                result.Add(new TechnicianDetailValidationError(scheduleDate, ErrorFieldEnum.MaxVisitsCount,
                    "Max jobs count should be positive"));

            if (modifiedDetail.Technician.MaxVisitsCount == 0)
                result.Add(new TechnicianDetailValidationError(scheduleDate, ErrorFieldEnum.MaxVisitsCount,
                    "Max jobs count cannot be zero"));

            if (modifiedDetail.Technician.MaxNonExclusiveVisitsCount < 0)
                result.Add(new TechnicianDetailValidationError(scheduleDate, ErrorFieldEnum.MaxNonExclusiveVisitsCount,
                    "Max NCO count should be positive"));

            if (modifiedDetail.Technician.MaxNonExclusiveVisitsCount > modifiedDetail.Technician.MaxVisitsCount)
            {
                result.Add(new TechnicianDetailValidationError(scheduleDate, ErrorFieldEnum.MaxVisitsCount,
                    "Max jobs count should be greater than NCO"));
                result.Add(new TechnicianDetailValidationError(scheduleDate, ErrorFieldEnum.MaxNonExclusiveVisitsCount,
                    "Max NCO should be less than max jobs count"));                
            }

            if (modifiedDetail.Technician.WorkingIntervals == null || modifiedDetail.Technician.WorkingIntervals.Count == 0)
                result.Add(new TechnicianDetailValidationError(scheduleDate, ErrorFieldEnum.WorkTime,
                    "No working hours found"));
            else
            {
                if (modifiedDetail.Technician.WorkingIntervals[0].TimeStart.Minute % 15 != 0)
                    result.Add(new TechnicianDetailValidationError(scheduleDate, ErrorFieldEnum.WorkTimeStart,
                        "Time minutes should be multiple of 15"));

                if (modifiedDetail.Technician.WorkingIntervals[0].TimeEnd.Minute % 15 != 0)
                    result.Add(new TechnicianDetailValidationError(scheduleDate, ErrorFieldEnum.WorkTimeEnd,
                        "Time minutes should be multiple of 15"));

                if (modifiedDetail.Technician.WorkingIntervals[0].TimeStart.TimeOfDay > modifiedDetail.Technician.WorkingIntervals[0].TimeEnd.TimeOfDay)
                {
                    result.Add(new TechnicianDetailValidationError(scheduleDate, ErrorFieldEnum.WorkTimeStart,
                        "Time Start should be less than Time End"));
                    result.Add(new TechnicianDetailValidationError(scheduleDate, ErrorFieldEnum.WorkTimeEnd,
                        "Time End should be greater than Time Start"));                    
                }                
            }

            if (modifiedDetail.Technician.HourlyRate < 0)
                result.Add(new TechnicianDetailValidationError(scheduleDate, ErrorFieldEnum.HourlyRate,
                    "Invalid rate"));

            if (modifiedDetail.Technician.HourlyRate150to300 < 0)
                result.Add(new TechnicianDetailValidationError(scheduleDate, ErrorFieldEnum.HourlyRate150To300,
                    "Invalid rate"));

            if (modifiedDetail.Technician.HourlyRateMore300 < 0)
                result.Add(new TechnicianDetailValidationError(scheduleDate, ErrorFieldEnum.HourlyRateMore300,
                    "Invalid rate"));

            result.AddRange(GetZipCodesErrors(modifiedDetail, existingDetail, true, deepValidation, service));
            result.AddRange(GetZipCodesErrors(modifiedDetail, existingDetail, false, deepValidation, service));

            if (existingDetail != null && modifiedDetail.Services.Count() != existingDetail.Services.Count())
                result.Add(new TechnicianDetailValidationError(scheduleDate, ErrorFieldEnum.Services,
                    "Not all the technician services found"));

            return result;
        }

        private static List<TechnicianDetailValidationError> GetZipCodesErrors(TechnicianDetail modifiedDetail, 
            TechnicianDetail existingDetail, bool isPrimary, bool deepValidation, IWcfService service)
        {
            List<TechnicianDetailValidationError> result = new List<TechnicianDetailValidationError>();

            DateTime scheduleDate = modifiedDetail.Technician.ScheduleDate;
            string zipCodesString = isPrimary ? modifiedDetail.PrimaryZipCodesText : modifiedDetail.SecondaryZipCodesText;
            string zipCodesStringAnother = !isPrimary ? modifiedDetail.PrimaryZipCodesText : modifiedDetail.SecondaryZipCodesText;
            ErrorFieldEnum fieldEnum = isPrimary ? ErrorFieldEnum.PrimaryZipCodes : ErrorFieldEnum.SecondaryZipCodes;

            foreach (char c in zipCodesString)
            {
                if (!char.IsDigit(c) && c != ' ' && c != ',')
                {
                    result.Add(new TechnicianDetailValidationError(scheduleDate, fieldEnum,
                        string.Format("Zip code list contains invalid character '{0}'", c)));
                    return result;
                }
            }

            string invalidZips = GetInvalidZipCodes(zipCodesString);
            if (invalidZips != string.Empty)
            {
                result.Add(new TechnicianDetailValidationError(scheduleDate, fieldEnum,
                    "The following zip codes are invalid: " + invalidZips));
                return result;                
            }

            string duplicatesWithinCurrentList = GetDuplicateZipCodes(zipCodesString);
            if (duplicatesWithinCurrentList != string.Empty)
            {
                result.Add(new TechnicianDetailValidationError(scheduleDate, fieldEnum,
                    "The following zip codes are duplicated: " + duplicatesWithinCurrentList));
                return result;                
            }

            string duplicatesWithinAnotherList = GetDuplicateZipCodes(zipCodesString, zipCodesStringAnother);
            if (duplicatesWithinAnotherList != string.Empty)
            {
                result.Add(new TechnicianDetailValidationError(scheduleDate, fieldEnum,
                    string.Format("The following zip codes are present in {0} list: {1}",
                        !isPrimary ? "Primary" : "Secondary", duplicatesWithinAnotherList)));
                return result;                
            }
            
            if (deepValidation)
            {
                string zipCodesOldString = string.Empty;
                if (existingDetail != null)
                    zipCodesOldString = isPrimary ? existingDetail.PrimaryZipCodesText : existingDetail.SecondaryZipCodesText;

                if (zipCodesString != zipCodesOldString)
                {
                    string zipCodes = GetNotRecognizedZipCodes(GetZipCodes(zipCodesString).ToList(), service);
                    if (zipCodes != string.Empty)
                        result.Add(new TechnicianDetailValidationError(scheduleDate, fieldEnum,
                            "The following zip codes are not recognized: " + zipCodes));                                    
                }
            }

            if (isPrimary)
                modifiedDetail.Technician.PrimaryZipCodes = GetZipCodes(zipCodesString).ToList();
            else
                modifiedDetail.Technician.SecondaryZipCodes = GetZipCodes(zipCodesString).ToList();

            return result;
        }

        #endregion

        #region Zip Helpers

        private static string GetInvalidZipCodes(string zipsString)
        {
            return GetInvalidZipCodes(GetZipCodes(zipsString));
        }

        private static string GetDuplicateZipCodes(string zipsString)
        {
            return GetDuplicateZipCodes(GetZipCodes(zipsString), null);
        }

        private static string GetDuplicateZipCodes(string zipsString1, string zipsString2)
        {
            return GetDuplicateZipCodes(GetZipCodes(zipsString1), GetZipCodes(zipsString2));
        }

        private static IEnumerable<string> GetZipCodes(string zipsString)
        {
            List<string> result = new List<string>(Regex.Split(zipsString, "[^0-9]+"));

            if (result.Count > 0 && result[0] == string.Empty)
                result.RemoveAt(0);

            if (result.Count > 0 && result[result.Count - 1] == string.Empty)
                result.RemoveAt(result.Count - 1);

            return result;
        }

        private static string GetNotRecognizedZipCodes(List<string> zipCodes, IWcfService service)
        {
            string result = string.Empty;
            Dictionary<string, ZipCode> serverZips;

            serverZips = service.GetZipCodes();

            foreach (string zip in zipCodes)
            {
                if (!serverZips.ContainsKey(zip))
                {
                    Coordinate zipCoordinate = service.GecocodeZip(zip);

                    if (!zipCoordinate.IsValid)
                        result += ", " + zip;
                    else
                    {
                        ZipCode zipCode = new ZipCode(zip, zipCoordinate.Latitude,
                            zipCoordinate.Longitude, 1);

                        service.InsertZipCode(zipCode);
                    }
                }
            }            

            if (result != string.Empty)
                return result.Substring(2);
            return result;
        }

        private static string GetInvalidZipCodes(IEnumerable<string> zipCandidates)
        {
            string result = string.Empty;
            foreach (string candidate in zipCandidates)
            {
                if (candidate.Length != 5)
                    result += ", " + candidate;
            }

            if (result != string.Empty)
                return result.Substring(2);
            return result;
        }

        private static string GetDuplicateZipCodes(IEnumerable<string> zips1, IEnumerable<string> zips2)
        {
            string result = string.Empty;
            List<string> processedZips = new List<string>();

            foreach (string zip in zips1)
            {
                if (processedZips.Contains(zip))
                {
                    if (zips2 == null)
                        result += ", " + zip;
                }
                else
                    processedZips.Add(zip);
            }

            if (zips2 != null)
            {
                foreach (string zip in processedZips)
                {
                    if (zips2.Contains(zip) && !result.Contains(zip))
                        result += ", " + zip;
                }
            }

            if (result != string.Empty)
                return result.Substring(2);
            return result;
        }

        #endregion        

    }

    [DataContract]
    public class TechnicianService : ICloneable
    {
        #region ServiceAllowanceEnum

        [DataContract]
        public enum ServiceAllowanceEnum
        {
            [EnumMember]
            NotAllowed,
            [EnumMember]
            AllowedForExclusive,
            [EnumMember]
            Allowed
        }

        #endregion

        #region TechnicianService

        public TechnicianService(Service service, TechnicianServiceDeny deny)
        {
            m_service = service;

            if (deny == null)
                m_serviceAllowance = ServiceAllowanceEnum.Allowed;
            else if (deny.IsForNonExclusive)
                m_serviceAllowance = ServiceAllowanceEnum.AllowedForExclusive;
            else
                m_serviceAllowance = ServiceAllowanceEnum.NotAllowed;
        }

        public TechnicianService(Service service, ServiceAllowanceEnum allowance)
        {
            m_service = service;
            m_serviceAllowance = allowance;
        }

        #endregion

        #region Service

        private Service m_service;
        [DataMember]
        public Service Service
        {
            get { return m_service; }
            set { m_service = value; }
        }

        #endregion

        #region ServiceAllowance
        
        private ServiceAllowanceEnum m_serviceAllowance;
        [DataMember]
        public ServiceAllowanceEnum ServiceAllowance
        {
            get { return m_serviceAllowance; }
            set { m_serviceAllowance = value; }
        }

        #endregion


        #region Name

        public string Name
        {
            get { return m_service.Name; }
        }

        #endregion

        #region ImageIndex

        public int ImageIndex
        {
            get
            {
                if (m_serviceAllowance == ServiceAllowanceEnum.NotAllowed)
                    return 0;
                if (m_serviceAllowance == ServiceAllowanceEnum.Allowed)
                    return 1;
                return 2;
            }
        }

        #endregion

        #region SetAnotherAllowance

        public void SetAnotherAllowance()
        {
            if (m_serviceAllowance == ServiceAllowanceEnum.NotAllowed)
                m_serviceAllowance = ServiceAllowanceEnum.Allowed;
            else if (m_serviceAllowance == ServiceAllowanceEnum.Allowed)
                m_serviceAllowance = ServiceAllowanceEnum.AllowedForExclusive;
            else if (m_serviceAllowance == ServiceAllowanceEnum.AllowedForExclusive)
                m_serviceAllowance = ServiceAllowanceEnum.NotAllowed;
        }

        #endregion

        #region Clone

        public object Clone()
        {
            return new TechnicianService((Service)m_service.Clone(), m_serviceAllowance);
        }

        #endregion

    }
}
