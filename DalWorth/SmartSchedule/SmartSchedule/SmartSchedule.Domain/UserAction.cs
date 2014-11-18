using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using SmartSchedule.Data;
using SmartSchedule.Domain.WCF;

namespace SmartSchedule.Domain
{
    [DataContract]
    public enum SortField
    {
        [EnumMember]
        User,
        [EnumMember]
        Action,
        [EnumMember]
        Technician,
        [EnumMember]
        ActionDate,
        [EnumMember]
        DashboardDate,
        [EnumMember]
        Ticket 
    }

    public partial class UserAction
    {
        public UserAction(){}

        #region User

        private User m_user;        
        public User User
        {
            get { return m_user; }
            set
            {
                m_user = value;
                if (m_user != null)
                {
                    UserName = m_user.Login;
                    UserId = m_user.ID;
                }
                else
                    UserId = 0;
            }
        }

        #endregion

        #region Technician

        private TechnicianDefault m_technician;
        public TechnicianDefault Technician
        {
            get { return m_technician; }
            set
            {
                m_technician = value;
                if (m_technician != null)
                    TechnicianName = m_technician.Name;
            }
        }

        #endregion

        #region UserName

        [DataMember]
        public string UserName { get; set; }

        #endregion

        #region ActionTypeText

        public string ActionTypeText
        {
            get { return Domain.UserActionType.GetText(UserActionType); }
        }

        #endregion

        #region TechnicianName

        [DataMember]
        public string TechnicianName { get; set; }

        #endregion

        #region UserActionType

        public UserActionTypeEnum UserActionType
        {
            get { return (UserActionTypeEnum) m_userActionTypeId; }
            set { m_userActionTypeId = (int) value; }
        }

        #endregion

        #region WriteAction

        public static void WriteAction(User user, UserActionTypeEnum actionType, int? technicianDefaultId,
            DateTime? dashboardDate, string text)
        {
            WriteAction(user, actionType, technicianDefaultId, string.Empty, dashboardDate, text);
        }

        private static void WriteAction(User user, UserActionTypeEnum actionType, int? technicianDefaultId, 
            string ticketNumber, DateTime? dashboardDate, string text)
        {
            UserAction action = new UserAction(0, user.ID, (int)actionType, technicianDefaultId, ticketNumber,
                dashboardDate, DateTime.Now, text);
            Insert(action);
        }

        public static void WriteActionEx(User user, UserActionTypeEnum actionType, Technician technician, Visit visit, string text)
        {
            string ticketNumber = string.Empty;
            if (!visit.IsBlockout)
                ticketNumber = visit.TicketNumber;
            WriteAction(user, actionType, technician != null ? technician.TechnicianDefaultId : (int?)null, ticketNumber, visit.ScheduleDate, text);
        }

        public static void WriteAction(User user, UserActionTypeEnum actionType, Visit visit, string text)
        {
            WriteActionEx(user, actionType, null, visit, text);
        }

        #endregion

        #region GetTechnicianSettingsChangeText

        public static string GetTechnicianSettingsChangeText(TechnicianDetail oldDetail, TechnicianDetail newDetail, bool isDefaultSettings)
        {
            string result = string.Empty;
            if (oldDetail.Technician.DepotAddress != newDetail.Technician.DepotAddress)
                result += string.Format("new depot address - {0}; ", newDetail.Technician.DepotAddress.Replace("\r\n", string.Empty));
            if (oldDetail.Technician.DriveTimeMinutes != newDetail.Technician.DriveTimeMinutes)
                result += string.Format("new drive time - {0} min; ", newDetail.Technician.DriveTimeMinutes);
            if (oldDetail.Technician.MaxNonExclusiveVisitsCount != newDetail.Technician.MaxNonExclusiveVisitsCount
                || oldDetail.Technician.MaxVisitsCount != newDetail.Technician.MaxVisitsCount)
            {
                result += string.Format("new Max NCO/Jobs - {0}/{1}; ", newDetail.Technician.MaxNonExclusiveVisitsCount,
                    newDetail.Technician.MaxVisitsCount);
            }

            if (!isDefaultSettings && (oldDetail.Technician.WorkingIntervals[0].TimeStart != newDetail.Technician.WorkingIntervals[0].TimeStart
                || oldDetail.Technician.WorkingIntervals[0].TimeEnd != newDetail.Technician.WorkingIntervals[0].TimeEnd))
            {
                result += string.Format("new working hours {0}-{1}; ", newDetail.Technician.WorkingIntervals[0].TimeStart.ToShortTimeString(),
                    newDetail.Technician.WorkingIntervals[0].TimeEnd.ToShortTimeString());                
            }

            if (isDefaultSettings)
            {
                Dictionary<int, TechnicianWorkTimeDefaultPreset> oldPresets = new Dictionary<int, TechnicianWorkTimeDefaultPreset>();
                foreach (var preset in oldDetail.WorkingHoursPresets)
                    oldPresets.Add(preset.PresetNumber, preset);

                string presetsChange = string.Empty;
                foreach (var newPreset in newDetail.WorkingHoursPresets)
                {
                    TechnicianWorkTimeDefaultPreset oldPreset = oldPresets[newPreset.PresetNumber];

                    if (oldPreset.TimeStart == null && newPreset.TimeStart != null)
                    {
                        presetsChange += string.Format("new preset added {0}-{1}, ", newPreset.TimeStart.Value.ToShortTimeString(),
                            newPreset.TimeEnd.Value.ToShortTimeString());                        
                    }
                    else if (oldPreset.TimeStart != null && newPreset.TimeStart == null)
                    {
                        presetsChange += string.Format("preset deleted {0}-{1}, ", oldPreset.TimeStart.Value.ToShortTimeString(),
                             oldPreset.TimeEnd.Value.ToShortTimeString());                        
                    }
                    else if (oldPreset.TimeStart != newPreset.TimeStart || oldPreset.TimeEnd != newPreset.TimeEnd)
                    {
                        presetsChange += string.Format("preset {0} changed to {1}-{2}, ", newPreset.PresetNumber,
                            newPreset.TimeStart.Value.ToShortTimeString(), newPreset.TimeEnd.Value.ToShortTimeString());                        
                    }
                }

                if (presetsChange != string.Empty)
                {
                    presetsChange = presetsChange.Substring(0, presetsChange.Length - 2);
                    result += string.Format("work time presets change: {0}; ", presetsChange);
                }                
            }

            if (oldDetail.Technician.HourlyRate != newDetail.Technician.HourlyRate)
                result += string.Format("new hourly rate (less than $150) {0}; ", newDetail.Technician.HourlyRate.ToString("C"));                
            if (oldDetail.Technician.HourlyRate150to300 != newDetail.Technician.HourlyRate150to300)
                result += string.Format("new hourly rate (from $150 to $300) {0}; ", newDetail.Technician.HourlyRate150to300.ToString("C"));                
            if (oldDetail.Technician.HourlyRateMore300 != newDetail.Technician.HourlyRateMore300)
                result += string.Format("new hourly rate (more than $300) {0}; ", newDetail.Technician.HourlyRateMore300.ToString("C"));                

            if (oldDetail.PrimaryZipCodesText != newDetail.PrimaryZipCodesText)
                result += string.Format("new primary zip codes {0}; ", newDetail.PrimaryZipCodesText);                
            if (oldDetail.SecondaryZipCodesText != newDetail.SecondaryZipCodesText)
                result += string.Format("new secondary zip codes {0}; ", newDetail.SecondaryZipCodesText);                

            Dictionary<int, TechnicianService.ServiceAllowanceEnum> oldServiceMap = new Dictionary<int, TechnicianService.ServiceAllowanceEnum>();
            foreach (var service in oldDetail.Services)
                oldServiceMap.Add(service.Service.ID, service.ServiceAllowance);

            string serviceChanges = string.Empty;
            foreach (var newService in newDetail.Services)
            {
                if (!oldServiceMap.ContainsKey(newService.Service.ID))
                    continue;
                if (oldServiceMap[newService.Service.ID] != newService.ServiceAllowance)
                    serviceChanges += string.Format("service {0} set as {1}, ", newService.Name, newService.ServiceAllowance);
            }

            if (serviceChanges != string.Empty)
            {
                serviceChanges = serviceChanges.Substring(0, serviceChanges.Length - 2);
                result += string.Format("service changes: {0}; ", serviceChanges);
            }

            if (result != string.Empty)
                result = result.Substring(0, result.Length - 2);

            return result;
        }

        #endregion

        #region FindBy

        private const string SqlFindBy =
            @"SELECT * FROM UserAction ua
                inner join User on User.ID = ua.UserId
                left join TechnicianDefault td on td.ID = ua.TechnicianDefaultId
            where {0}              
              limit 1000";

        public static List<UserAction> FindBy(int? userId, UserActionTypeEnum? actionType, int? technicianDefaultId,
            string ticket, DateTime? dashboardDate, TimeInterval actionDateInterval, SortField sortField, bool isSortAscending)
        {
            string filterConditions = string.Empty;
            if (userId != null)
                filterConditions += " ua.UserId = " + userId;
            if (actionType != null)
                filterConditions += " and ua.UserActionTypeId = " + (int)actionType;
            if (technicianDefaultId != null)
                filterConditions += " and ua.TechnicianDefaultId = " + technicianDefaultId;
            if (!string.IsNullOrEmpty(ticket))
                filterConditions += string.Format(" and ua.TicketNumber like '%{0}%'", ticket);
            if (dashboardDate != null)
                filterConditions += " and ua.DashboardDate = ?DashboardDate";
            if (actionDateInterval != null)
                filterConditions += " and ua.ActionDate >= ?ActionDateStart and ua.ActionDate <= ?ActionDateEnd";

            if (sortField == SortField.Action)
                filterConditions += " order by ua.UserActionTypeId " + (!isSortAscending ? " desc" : string.Empty);
            else if (sortField == SortField.ActionDate)
                filterConditions += " order by ua.ActionDate " + (!isSortAscending ? " desc" : string.Empty);
            else if (sortField == SortField.DashboardDate)
                filterConditions += " order by ua.DashboardDate " + (!isSortAscending ? " desc" : string.Empty);
            else if (sortField == SortField.Technician)
                filterConditions += " order by td.Name " + (!isSortAscending ? " desc" : string.Empty);
            else if (sortField == SortField.Ticket)
                filterConditions += " order by ua.TicketNumber " + (!isSortAscending ? " desc" : string.Empty);
            else if (sortField == SortField.User)
                filterConditions += " order by User.Login " + (!isSortAscending ? " desc" : string.Empty);

            if (filterConditions.StartsWith(" and "))
                filterConditions = filterConditions.Substring(4, filterConditions.Length - 4);

            List<UserAction> result = new List<UserAction>();

            using (IDbCommand dbCommand = Database.PrepareCommand(string.Format(SqlFindBy, filterConditions)))
            {
                if (dashboardDate != null)
                    Database.PutParameter(dbCommand, "?DashboardDate", dashboardDate.Value);
                if (actionDateInterval != null)
                {
                    Database.PutParameter(dbCommand, "?ActionDateStart", actionDateInterval.Start.Date);
                    Database.PutParameter(dbCommand, "?ActionDateEnd", actionDateInterval.End.Date.AddHours(23).AddMinutes(59).AddSeconds(59));
                }

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        UserAction action = Load(dataReader);
                        action.User = User.Load(dataReader, FieldsCount);
                        if (!dataReader.IsDBNull(FieldsCount + User.FieldsCount))
                            action.Technician = TechnicianDefault.Load(dataReader, FieldsCount + User.FieldsCount);
                        result.Add(action);
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
      