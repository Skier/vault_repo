using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;
using SmartSchedule.Data;
using SmartSchedule.SDK;
using System.Linq;

namespace SmartSchedule.Domain
{
    public class Point
    {
        private int m_x, m_y;

        #region Constructor

        public Point(int x, int y)
        {
            m_x = x;
            m_y = y;
        }

        #endregion        

        #region Properties

        public int X
        {
            get { return m_x; }
        }                

        public int Y
        {
            get { return m_y; }
        }

        #endregion

        #region Text

        public string Text
        {
            get { return string.Format("({0}/{1})", X, Y); }
        }

        #endregion

        #region Distance

        public double Distance(Point point)
        {
            double dx = Math.Abs(X - point.X);
            double dy = Math.Abs(Y - point.Y);

            return Math.Sqrt(dx*dx + dy*dy);
        }

        #endregion
    }

    public partial class Visit : INotifyPropertyChanged
    {
        private static int m_tempId;

        private const double SHORT_DISTANCE_THRESHOLD = 5;
        public event PropertyChangedEventHandler PropertyChanged;        

        [DataMember]
        public DateTime TimeStart
        {
            get { return m_timeStart; }
            set
            {
                bool isChanged = m_timeStart != value;
                m_timeStart = value;

                if (isChanged && PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("TimeStart"));
            }
        }

        public DateTime ScheduleDate
        {
            get
            {
                return TimeStart.Date;
            }
        }

        #region TechnicianId

        [DataMember]
        public int? TechnicianId
        {
            get { return m_technicianId; }
            set
            {
                bool isChanged = m_technicianId != value;

                if (isChanged && PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("TechnicianId"));

                m_technicianId = value;

                if (isChanged && PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("TechnicianId"));
            }
        }

        #endregion

        #region Constructors

        public Visit(){}

        public Visit(Technician technician, DateTime timeStart, 
            TimeFrame confirmedTimeFrame, float latitude, float longitude, string zip, decimal cost, 
            int? exclusiveCompanyId, int? exclusiveTechnicianDefaultId, int? tempExclusiveTechnicianId, 
            bool isTempIgnoreExclusivity, int? forbiddenTechnicianDefaultId, string ticketNumber, 
            List<VisitDetail> details, string customerName, string street, string address2, string city, string state, 
            DateTime? callDateTime, bool isEstimate, bool isEstimateAndDo, bool isRework, string mapsco,
            string homePhone, string businessPhone, bool isCalledCustomer, bool isFixed, string area,
            int servType, string adsourceAcronym, int customerRank, int? originatedTechnicianDefaultId,
            DateTime? originatedCompleteDate, string originatedTicketNumber, int? customerExclusiveTechnicianDefaultId,
            string note, bool expCred, string specName, int? servmanBaseTimeFrameId, decimal sdPercent, 
            decimal taxPercent, decimal durationCost, bool isBlockout)
        {
            Technician = technician;
            m_timeStart = timeStart;
            m_cost = cost;
            UpdateTimeEndByTechnician(technician);
            m_confirmedTimeFrame = confirmedTimeFrame;
            m_timeFrameId = m_confirmedTimeFrame.ID;
            m_latitude = latitude;
            m_longitude = longitude;
            m_zip = zip;
            m_exclusiveCompanyId = exclusiveCompanyId;
            m_exclusiveTechnicianDefaultId = exclusiveTechnicianDefaultId;
            m_tempExclusiveTechnicianId = tempExclusiveTechnicianId;
            m_isTempIgnoreExclusivity = isTempIgnoreExclusivity;
            m_forbiddenTechnicianDefaultId = forbiddenTechnicianDefaultId;
            m_ticketNumber = ticketNumber;
            m_details = details;
            m_customerName = customerName;
            m_street = street;
            m_address2 = address2;
            m_city = city;
            m_state = state;
            m_callDateTime = callDateTime;
            m_isEstimate = isEstimate;
            m_isEstimateAndDo = isEstimateAndDo;
            m_isRework = isRework;
            m_mapsco = mapsco;
            m_homePhone = homePhone;
            m_businessPhone = businessPhone;
            m_isCalledCustomer = isCalledCustomer;
            m_isFixed = isFixed;
            m_area = area;
            m_servType = servType;
            m_adsourceAcronym = adsourceAcronym;
            m_customerRank = customerRank;
            m_originatedTechnicianDefaultId = originatedTechnicianDefaultId;
            m_originatedCompleteDate = originatedCompleteDate;
            m_originatedTicketNumber = originatedTicketNumber;
            m_customerExclusiveTechnicianDefaultId = customerExclusiveTechnicianDefaultId;
            m_note = note;
            m_expCred = expCred;
            m_specName = specName;
            m_servmanBaseTimeFrameId = servmanBaseTimeFrameId;
            m_sdPercent = sdPercent;
            m_taxPercent = taxPercent;
            m_durationCost = durationCost;
            m_isBlockout = isBlockout;
        }

        #endregion

        #region Technician

        public Technician Technician
        {
            get
            {
                if (m_technicianId == null)
                    return null;

                return Technician.GetTechnician(m_technicianId.Value);
            }
            set
            {
                if (value != null)
                    TechnicianId = value.ID;
                else
                    TechnicianId = null;
            }
        }

        #endregion                

        #region ExclusiveCompany

        public Company ExclusiveCompany
        {
            get
            {
                if (m_exclusiveCompanyId != null)
                    return Company.GetCompany(m_exclusiveCompanyId.Value);
                return null;
            }
            set
            {
                if (value != null)
                    m_exclusiveCompanyId = value.ID;
                else
                    m_exclusiveCompanyId = null;
            }
        }

        #endregion

        #region UserFrindlyId

        public string UserFrindlyIdLong
        {
            get
            {
                if (IsBlockout)
                    return string.Format("Blockout {0}-{1}",
                        m_timeStart.ToShortTimeString(), m_timeEnd.ToShortTimeString());
                return string.Format("Visit {0}", TicketNumber);
            }
        }

        #endregion


        #region ExclusiveTechnicianDefault

        public TechnicianDefault ExclusiveTechnicianDefault
        {
            get
            {                
                if (m_exclusiveTechnicianDefaultId != null)
                    return Technician.GetTechnicianDefault(m_exclusiveTechnicianDefaultId.Value);
                return null;
            }
        }

        public Technician ExclusiveTechnician
        {
            get
            {                
                if (m_exclusiveTechnicianDefaultId != null)
                    return Technician.GetTechnician(ScheduleDate, m_exclusiveTechnicianDefaultId.Value);
                return null;
            }
        }

        #endregion        


        #region TempExclusiveTechnician

        public Technician TempExclusiveTechnician
        {
            get
            {
                if (m_tempExclusiveTechnicianId != null)
                    return Technician.GetTechnician(m_tempExclusiveTechnicianId.Value);
                return null;
            }
        }

        #endregion        


        #region ForbiddenTechnician

        public Technician ForbiddenTechnician
        {
            get
            {
                if (m_forbiddenTechnicianDefaultId != null)
                    return Technician.GetTechnician(ScheduleDate, m_forbiddenTechnicianDefaultId.Value);
                return null;
            }
        }

        #endregion

        #region OnSerialize

        [DataMember]
        public string ExclusiveTechnicianName { get; set; }
        [DataMember]
        public string TempExclusiveTechnicianName { get; set; }
        [DataMember]
        public string ForbiddenTechnicianName { get; set; }
        [DataMember]
        public string OriginatedTechnicianName { get; set; }
        [DataMember]
        public string CustomerExclusiveTechnicianName { get; set; }
        [DataMember]
        public string ServmanBaseTimeFrameText { get; set; }

        [OnSerializing]
        internal void OnSerialize(StreamingContext context)
        {
            if (Configuration.IsClientApplication || Configuration.IsOptimizer)
                return;

            ExclusiveTechnicianName = ExclusiveTechnicianDefault != null ? ExclusiveTechnicianDefault.Name : string.Empty;
            TempExclusiveTechnicianName = TempExclusiveTechnician != null ? TempExclusiveTechnician.Name : string.Empty;
            ForbiddenTechnicianName = ForbiddenTechnician != null ? ForbiddenTechnician.Name : string.Empty;
            
            if (OriginatedTechnicianDefaultId.HasValue)
                OriginatedTechnicianName = Technician.GetTechnicianDefault(
                    OriginatedTechnicianDefaultId.Value).Name;
            
            if (CustomerExclusiveTechnicianDefaultId.HasValue)
                CustomerExclusiveTechnicianName = Technician.GetTechnicianDefault(
                    CustomerExclusiveTechnicianDefaultId.Value).Name;

            if (ServmanBaseTimeFrameId.HasValue)
                ServmanBaseTimeFrameText = TimeFrame.TimeFrames[ServmanBaseTimeFrameId.Value].Text;
        }

        #endregion

        public int StatusId { get; set; }

        #region AssignTemporaryId

        public void AssignTemporaryId()
        {
            ID = ++m_tempId;
        }

        public static void SetTemporaryIdCounter(int tempId)
        {
            m_tempId = tempId;
        }

        #endregion

        #region Details

        private List<VisitDetail> m_details;
        [DataMember]
        public List<VisitDetail> Details
        {
            get { return m_details; }
            set { m_details = value; }
        }

        #endregion

        #region Option

        public string Option
        {
            get
            {
                if (IsEstimate)
                    return "E";
                if (IsEstimateAndDo)
                    return "ED";
                if (IsRework)
                    return "R";

                return string.Empty;
            }
        }

        #endregion        

        #region Distance

        public double Distance(Visit visit)
        {
            if (IsBlockout || visit.IsBlockout)
                return 0;
            return Utils.Distance(Latitude, Longitude, visit.Latitude, visit.Longitude);
        }

        public double Distance(Technician technician)
        {
            return technician.Distance(this);
        }

        #endregion

        #region UpdateTimeEndByTechnician

        public void UpdateTimeEndByTechnician(Technician technician)
        {
            m_timeEnd = GetTimeEnd(technician ?? Technician);
            if (m_timeEnd.Date > m_timeStart.Date)
            {
                m_timeEnd = new DateTime(m_timeStart.Year, m_timeStart.Month, m_timeStart.Day,
                    23, 45, 00);
            }                
        }

        public void UpdateTimeEndByTechnician()
        {
            UpdateTimeEndByTechnician(null);
        }

        #endregion        

        #region SetStartTimeFixDuration

        public void SetTimeStartFixDuration(DateTime timeStart)
        {
            TimeStart = timeStart;
            UpdateTimeEndByTechnician();
        }

        #endregion                

        #region DurationMin

        public double DurationMin
        {
            get { return m_timeEnd.Subtract(m_timeStart).TotalMinutes; }
        }

        #endregion

        #region GetDurationOnTechnician

        public double GetDurationOnTechnician(Technician technician)
        {
            if (IsBlockout)
                return DurationMin;

            double durationHours;

            if (DurationCost <= 150)
                durationHours = ((double)(DurationCost / technician.HourlyRate));
            else if (DurationCost > 150 && DurationCost < 300)
                durationHours = ((double)(DurationCost / technician.HourlyRate150to300));
            else
                durationHours = ((double)(DurationCost / technician.HourlyRateMore300));

            if (durationHours < 0.5)
                durationHours = 0.5;

            double duration = Math.Round(durationHours * 60 + technician.DriveTimeMinutes);

            int remainderBy15 = (int)duration % 15;
            if (remainderBy15 != 0)
            {
                duration = ((int)(duration / 15)) * 15;
                if (remainderBy15 >= 8)
                    duration += 15;
            }

            return duration;
        }

        #endregion

        #region UpdateDurationCostByDuration

        public void UpdateDurationCostByDuration(double newDurationMinutes)
        {
            double durationMinutes = newDurationMinutes - Technician.DriveTimeMinutes;

            double resultLess150 = (durationMinutes / 60) * (double)Technician.HourlyRate;
            double result150To300 = (durationMinutes / 60) * (double)Technician.HourlyRate150to300;
            double resultMore300 = (durationMinutes / 60) * (double)Technician.HourlyRateMore300; 

            if (resultLess150 <= 150)
                DurationCost = (decimal) resultLess150;
            else if (result150To300 > 150 && result150To300 < 300)
                DurationCost = (decimal) result150To300;
            else
                DurationCost = (decimal) resultMore300;
        }

        #endregion

        #region TimeArrival

        public DateTime TimeArrival
        {
            get { return m_timeStart.AddMinutes(Technician.DriveTimeMinutes); }            
        }

        #endregion

        #region Caption

        public string Caption
        {
            get
            {
                if (IsBlockout)
                    return "Blockout, Created " + CallDateTime.Value.ToShortDateString();

                string ticketString = string.Empty;
                if (TicketNumber != string.Empty)
                {
                    if (Option != string.Empty)
                        ticketString = Option + " ";
                    
                    ticketString += TicketNumber + ", ";
                }

                string arrivalTimeString;

                if (TechnicianId.HasValue)
                    arrivalTimeString = ", ARR: " + TimeArrival.ToShortTimeString();
                else
                    arrivalTimeString = string.Empty;

                string originalTimeFrameText = string.Empty;
                if (ServmanBaseTimeFrameId.HasValue)
                    originalTimeFrameText = ", ORIGINAL: " + ServmanBaseTimeFrameText;

                return ticketString + Cost.ToString("C") + ", " + ConfirmedTimeFrame.Text + originalTimeFrameText
                     + arrivalTimeString + ", MAP: " + Mapsco + ", Zip: " + Zip
                     + (ExclusiveCompanyId != null ? "\r\nEX. " + ExclusiveCompany.Name : string.Empty)
                     + (ExclusiveTechnicianName != string.Empty ? "\r\nEX. " + ExclusiveTechnicianName : string.Empty)
                     + (ForbiddenTechnicianName != string.Empty ? "\r\nFORB. " + ForbiddenTechnicianName : string.Empty);
            }
        }

        #endregion

        #region ToolTipText

        public string ToolTipText
        {
            get
            {
                string addressPart;
                if (TicketNumber != string.Empty)
                    addressPart = CustomerName + "\r\n" + Address + " " + Zip + "\r\n";
                else
                    addressPart = Zip + "\r\n";

                string servicesText = string.Empty;
                if (Details != null && Details.Count > 0)
                {
                    servicesText = "\n\nServices:\n";

                    foreach (string serviceName in GetUniqueServiceNames())
                        servicesText += serviceName + "\n";

                    servicesText = servicesText.Substring(0, servicesText.Length - 1);
                }

                string notes = string.Empty;
                if (!string.IsNullOrEmpty(Note))
                    notes = "\n\nNotes:\n" + Note.Trim();

                if (IsBlockout)
                    return "Notes:\n" + Note;
                return addressPart + Caption + servicesText + notes;
            }
        }

        #endregion

        #region GetUniqueServiceNames

        private List<string> GetUniqueServiceNames()
        {
            List<string> result = new List<string>();

            foreach (VisitDetail detail in Details)
            {
                if (!result.Contains(detail.ServiceName))
                    result.Add(detail.ServiceName);
            }

            return result;
        }

        #endregion        

        #region LabelId

        public int LabelId
        {
            get
            {
                if (!CanTechnicianServiceVisit(Technician))
                    return 4;

                if (IsTemporaryAssigned)
                    return 2;

                if (IsBlockout)
                    return 5;

                if (ExclusiveTechnicianDefaultId != null && ExclusiveTechnicianDefaultId == Technician.TechnicianDefaultId)
                    return 3;

//                if (ExclusiveCompanyId != null && ExclusiveCompanyId == Technician.CompanyId)
//                    return 3;                

                if (IsInPrimaryArea)
                    return 0;

                if (IsInSecondaryArea)
                    return 1;

                return 4;
            }
        }

        #endregion        

        #region ConfirmedTimeFrame

        private TimeFrame m_confirmedTimeFrame;
        [DataMember]
        public TimeFrame ConfirmedTimeFrame
        {
            get { return m_confirmedTimeFrame; }
            set
            {
                m_confirmedTimeFrame = value;
                m_timeFrameId = m_confirmedTimeFrame.ID;
            }
        }

        #endregion

        #region IsWithinAllowedInterval

        public bool IsWithinAllowedInterval
        {
            get
            {
                TimeInterval interval = GetAllowedInterval(Technician);
                if (interval == null)
                    return false;
                return VisitInterval.IsWithin(interval);                
            }
        }

        #endregion

        #region VisitInterval

        public TimeInterval VisitInterval
        {
            get
            {
                return new TimeInterval(m_timeStart, m_timeEnd);
            }
        }

        #endregion


        #region IsInPrimaryArea

        public bool IsInPrimaryArea
        {
            get
            {
                return IsPrimaryTechnician(Technician);
            }
        }

        #endregion

        #region CanTechnicianServiceVisit

        public bool CanTechnicianServiceVisit(Technician technician)
        {
            if (m_details == null)
                return true;

            bool isExclusiveVisit = ExclusiveCompanyId.HasValue || ExclusiveTechnicianDefaultId.HasValue;

            foreach (VisitDetail detail in m_details)
            {
                if (!technician.CanService(detail.ServiceId, isExclusiveVisit))
                    return false;
            }

            return true;
        }

        public bool IsPrimaryTechnician(Technician technician)
        {
            if (Configuration.IsClientApplication || Configuration.IsOptimizer)
                return IsPrimaryTechnicianClient(technician);

            return PrimaryTechnicians.Contains(technician);
        }

        #endregion

        #region IsSecondaryTechnician

        public bool IsSecondaryTechnician(Technician technician)
        {
            if (Configuration.IsClientApplication || Configuration.IsOptimizer)
                return IsSecondaryTechnicianClient(technician);

            return SecondaryTechnicians.Contains(technician);
        }

        #endregion

        #region IsTemporaryAssigned

        public bool IsTemporaryAssigned
        {
            get
            {
                if ((IsTempIgnoreExclusivity && TechnicianId.HasValue)
                    || (TempExclusiveTechnicianId != null && TechnicianId == TempExclusiveTechnicianId))
                {
                    return true;
                }                                       

                return false;
            }
        }

        #endregion



        #region IsInSecondaryArea

        public bool IsInSecondaryArea
        {
            get
            {
                return IsSecondaryTechnician(Technician);
            }
        }

        #endregion

        #region IsTechnicianAllowed

        public bool IsTechnicianAllowed(Technician technician)
        {
            if (IsPrimaryTechnician(technician) || IsSecondaryTechnician(technician))
                return true;
            return false;
        }

        private bool IsPrimaryTechnicianClient(Technician technician)
        {
            if (ForbiddenTechnicianDefaultId.HasValue && ForbiddenTechnicianDefaultId == technician.TechnicianDefaultId)
                return false;

            if (!CanTechnicianServiceVisit(technician))
                return false;

            if (IsFixed && technician.ID != TechnicianId)
                return false;

            if (IsTempIgnoreExclusivity && TempExclusiveTechnicianId.HasValue)
            {
                if (technician.ID == TempExclusiveTechnicianId)
                    return true;
                return false;
            }

            if (ExclusiveCompanyId.HasValue && technician.CompanyId == ExclusiveCompanyId)
                return true;

            if (!IsTempIgnoreExclusivity && ExclusiveTechnicianDefaultId.HasValue 
                && ExclusiveTechnicianDefaultId == technician.TechnicianDefaultId)
            {
                return true;
            }                

            if (technician.PrimaryZipCodes.Contains(Zip))
                return true;
            return false;
        }

        private bool IsSecondaryTechnicianClient(Technician technician)
        {
            if (ForbiddenTechnicianDefaultId.HasValue && ForbiddenTechnicianDefaultId == technician.TechnicianDefaultId)
                return false;

            if (!CanTechnicianServiceVisit(technician))
                return false;

            if (IsFixed && technician.ID != TechnicianId)
                return false;

            if (IsTempIgnoreExclusivity && TempExclusiveTechnicianId.HasValue)
                return false;

            if (!IsTempIgnoreExclusivity && ExclusiveTechnicianDefaultId.HasValue)
                return false;

            if (technician.IsContractor || technician.SecondaryZipCodes.Contains(Zip))
                return true;
            return false;
        }



        #endregion

        #region RemoveNotAllowedTechnicians

        //Removes technicians because of services and technicians day off
        private void RemoveNotAllowedTechnicians(List<Technician> technicians)
        {
            for (int i = technicians.Count - 1; i >= 0; i--)
            {
                if (technicians[i] == null || !CanTechnicianServiceVisit(technicians[i]))
                    technicians.RemoveAt(i);
            }
        }

        public List<Technician> PrimaryTechnicians
        {
            get
            {
                List<Technician> result = new List<Technician>();
                
                if (IsFixed)
                {
                    Debug.Assert(Technician != null, "Fixed visit cannot be in bucket");

                    if (Technician.GetPrimaryTechniciansByZip(Zip, ScheduleDate).Contains(Technician))
                        result.Add(Technician);
                    return result;
                }

                if (IsTempIgnoreExclusivity && TempExclusiveTechnicianId != null)
                {
                    result.Add(TempExclusiveTechnician);
                    RemoveNotAllowedTechnicians(result);
                    return result;
                }

//                if (!IsTempIgnoreExclusivity && ExclusiveCompanyId != null)
//                {
//                    result.AddRange(Technician.GetTechnicians(ExclusiveCompanyId.Value, ScheduleDate));
//                    RemoveNotAllowedTechnicians(result);
//                    return result;
//                }

                if (!IsTempIgnoreExclusivity && ExclusiveTechnicianDefaultId != null)
                {
                    if (ExclusiveTechnician != null)
                        result.Add(ExclusiveTechnician);
                    RemoveNotAllowedTechnicians(result);
                    return result;
                }

                result.AddRange(Technician.GetPrimaryTechniciansByZip(Zip, ScheduleDate));

                if (ForbiddenTechnicianDefaultId != null && result.Contains(ForbiddenTechnician))
                    result.Remove(ForbiddenTechnician);
                RemoveNotAllowedTechnicians(result);

                return result.OrderBy(technician => technician.Distance(this)).ToList();
            }
        }

        #endregion

        #region SecondaryTechnicians

        public List<Technician> SecondaryTechnicians
        {
            get
            {
                if (!IsTempIgnoreExclusivity &&
                    (ExclusiveCompanyId != null || ExclusiveTechnicianDefaultId != null))
                {
                    return new List<Technician>();
                }

                if (IsTempIgnoreExclusivity && TempExclusiveTechnicianId != null)
                    return new List<Technician>();

                List<Technician> result = new List<Technician>();

                if (IsFixed)
                {
                    Debug.Assert(Technician != null, "Fixed visit cannot be in bucket");

                    if (Technician.GetSecondaryTechniciansByZip(Zip, ScheduleDate).Contains(Technician))
                        result.Add(Technician);
                    return result;
                }

                result = Technician.GetSecondaryTechniciansByZip(Zip, ScheduleDate);

                if (ForbiddenTechnicianDefaultId != null && result.Contains(ForbiddenTechnician))
                    result.Remove(ForbiddenTechnician);
                RemoveNotAllowedTechnicians(result);

                return result.OrderBy(technician => technician.Distance(this)).ToList();
            }
        }

        #endregion


        #region GetAllowedInterval

        public TimeInterval GetAllowedInterval(Technician technician)
        {
            return GetAllowedInterval(technician, null);
        }

        //if ignoreWorkingHours is null then use global settings 
        public TimeInterval GetAllowedInterval(Technician technician, bool? ignoreWorkingHours)
        {
            if (IsBlockout)
                return VisitInterval;

            double visitDuration = GetDurationOnTechnician(technician);
            DateTime start = ScheduleDate.Add(ConfirmedTimeFrame.GetMinVisitStartTime(technician).TimeOfDay);

            DateTime maxTimeEnd = ConfirmedTimeFrame.GetMaxVisitStartTime(technician)
                .AddMinutes(visitDuration);
            if (maxTimeEnd.Date > ConfirmedTimeFrame.Date)
                maxTimeEnd = ConfirmedTimeFrame.Date.AddHours(23).AddMinutes(45);
            DateTime end = ScheduleDate.Add(maxTimeEnd.TimeOfDay);

            TimeInterval unrestrictedInterval = new TimeInterval(start, end);

            bool ignoreWorkingHoursTemp;
            if (ignoreWorkingHours.HasValue)
                ignoreWorkingHoursTemp = ignoreWorkingHours.Value;
            else
            {
                ignoreWorkingHoursTemp = BookingEngine.IgnoreWorkingHoursMode;                
            }

            if (ignoreWorkingHoursTemp)
                return unrestrictedInterval;

            foreach (TechnicianWorkTime workTime in technician.WorkingIntervals)
            {
                TimeInterval interval = workTime.GetInterval();
                TimeInterval intersection = unrestrictedInterval.GetIntersectingInterval(interval);

                if (intersection.DurationMin >= visitDuration)
                    return intersection;
            }

            return null;
        }

        #endregion

        #region GetMaxTimeStart

        public DateTime? GetMaxTimeStart(Technician technician)
        {
            return GetMaxTimeStart(technician, null);
        }

        public DateTime? GetMaxTimeStart(Technician technician, bool? ignoreWorkingHours)
        {
            TimeInterval allowedInterval = GetAllowedInterval(technician, ignoreWorkingHours);
            if (allowedInterval != null)
                return allowedInterval.End.AddMinutes(-GetDurationOnTechnician(technician));
            return null;
        }

        #endregion

        #region GetMinTimeStart

        public DateTime? GetMinTimeStart(Technician technician)
        {
            return GetMinTimeStart(technician, null);
        }

        public DateTime? GetMinTimeStart(Technician technician, bool? ignoreWorkingHours)
        {
            TimeInterval allowedInterval = GetAllowedInterval(technician, ignoreWorkingHours);
            if (allowedInterval != null)
                return allowedInterval.Start;
            return null;
        }

        #endregion

        #region GetMaxTimeEnd

        public DateTime? GetMaxTimeEnd(Technician technician)
        {
            return GetMaxTimeEnd(technician, null);
        }

        public DateTime? GetMaxTimeEnd(Technician technician, bool? ignoreWorkingHours)
        {
            TimeInterval allowedInterval = GetAllowedInterval(technician, ignoreWorkingHours);
            if (allowedInterval != null)
                return allowedInterval.End;
            return null;
        }

        #endregion

        #region GetMinTimeEnd

        public DateTime? GetMinTimeEnd(Technician technician)
        {
            return GetMinTimeEnd(technician, null);
        }

        public DateTime? GetMinTimeEnd(Technician technician, bool? ignoreWorkingHours)
        {
            TimeInterval allowedInterval = GetAllowedInterval(technician, ignoreWorkingHours);
            if (allowedInterval != null)
                return allowedInterval.Start.AddMinutes(GetDurationOnTechnician(technician));
            return null;
        }

        #endregion

        #region GetTimeEnd

        public DateTime GetTimeEnd(Technician technician)
        {
            return m_timeStart.AddMinutes(GetDurationOnTechnician(technician));
        }

        #endregion

        #region CanFitInterval

        public bool CanFitInterval(Technician technician, TimeInterval interval)
        {
            double duration = GetDurationOnTechnician(technician);
            TimeInterval allowedInterval = GetAllowedInterval(technician);

            if (allowedInterval.GetIntersectingInterval(interval).DurationMin >= duration)
                return true;
            return false;
        }

        #endregion



        #region IsCloseDrive

        public bool IsCloseDrive(Visit visit)
        {
            if (m_tempIgnoreGoodJoints)
                return false;
            return Distance(visit) <= SHORT_DISTANCE_THRESHOLD;
        }

        private static bool m_tempIgnoreGoodJoints;

        public static void SetIgnoreGoodJoints()
        {
            m_tempIgnoreGoodJoints = true;
        }

        public static void DiscardIgnoreGoodJoints()
        {
            m_tempIgnoreGoodJoints = false;
        }

        #endregion   
        
        #region FindWithDetails

        private const string SqlFindWithDetails =
            @"SELECT * FROM visit v
                left join Technician t on t.ID = v.TechnicianId
                inner join TimeFrame tf on tf.ID = v.TimeFrameId
               where 1=1";

        public static List<Visit> FindWithDetails(Technician technician, bool findSnapshotOnly, 
            bool findInBucket)
        {
            List<Visit> result = new List<Visit>();

            string queryString = SqlFindWithDetails;

            if (!findInBucket)
                queryString += " and v.TechnicianId is not null";

            if (technician != null)
                queryString += " and v.TechnicianId = " + technician.ID;

            if (findSnapshotOnly)
                queryString += " and SnapshotDate is not null";
            else
                queryString += " and SnapshotDate is null";

            if (Configuration.IsRealtimeMode)
                queryString += " and Date(v.TimeStart) >= Date(Now())";

            using (IDbCommand dbCommand = Database.PrepareCommand(queryString))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Visit visit = Load(dataReader);
                        if (!dataReader.IsDBNull(Visit.FieldsCount))
                            visit.Technician = Technician.Load(dataReader, FieldsCount);
                        visit.ConfirmedTimeFrame = TimeFrame.Load(dataReader, FieldsCount + Technician.FieldsCount);
                        result.Add(visit);
                    }                        
                }
            }

            foreach (Visit visit in result)
                visit.Details = VisitDetail.FindByVisit(visit);                        

            return result;

        }

        public static List<Visit> FindWithDetails()
        {
            return FindWithDetails(null, false, false);
        }

        #endregion        

        #region FindSnapshotDate

        private const string SqlFindSnapshotDate =
            @"SELECT SnapshotDate FROM visit
                where SnapshotDate is not null
              limit 1";

        public static DateTime? FindSnapshotDate()
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindSnapshotDate))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (dataReader.Read())
                        return dataReader.GetDateTime(0);
                }
            }

            return null;
        }



        #endregion        

        #region DeleteSnapshot

        private const string SqlDeleteSnapshotDetails =
            @"delete from VisitDetail where VisitId in 
                (select ID from Visit where SnapshotDate is not null)";

        private const string SqlDeleteSnapshot =
            @"delete from Visit where SnapshotDate is not null";

        private static void DeleteSnapshot()
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteSnapshotDetails))
                dbCommand.ExecuteNonQuery();
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteSnapshot))
                dbCommand.ExecuteNonQuery();
        }

        #endregion        

        #region CreateSnapshot

        public static void CreateSnapshot()
        {
            DeleteSnapshot();

            List<Visit> visits = FindWithDetails(null, false, true);            
            DateTime newSnapshotDate = DateTime.Now;
            foreach (Visit visit in visits)
            {
                visit.SnapshotDate = newSnapshotDate;
                Insert(visit);

                foreach (VisitDetail detail in visit.Details)
                {
                    detail.VisitId = visit.ID;
                    VisitDetail.Insert(detail);
                }
            }

        }

        #endregion        

        #region InsertWithDetails

        public static void InsertWithDetails(Visit visit)
        {
            Insert(visit);

            if (visit.Details != null)
            {
                foreach (VisitDetail detail in visit.Details)
                {
                    detail.VisitId = visit.ID;
                    VisitDetail.Insert(detail);
                }
            }
                
        }

        #endregion

        #region DeleteWithDetails

        public static void DeleteWithDetails(Visit visit)
        {
            VisitDetail.DeleteByVisit(visit);
            Delete(visit);
        }

        #endregion

        #region FindPossibleNotificationsBucket

        private const string SqlFindPossibleNotificationsBucket =
            @"SELECT * FROM Visit
                where Date(TimeStart) > Date(Now()) and Date(TimeStart) <= DATE_ADD(Date(Now()), INTERVAL 30 DAY)
                and TechnicianEmailSentDate is null and ExclusiveTechnicianDefaultId is not null and TechnicianId is null ";

        public static List<Visit> FindPossibleNotificationsBucket()
        {
            List<Visit> result = new List<Visit>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindPossibleNotificationsBucket))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion        

        #region FindPossibleNotificationsOutOfArea

        private const string SqlFindPossibleNotificationsOutOfArea =
            @"SELECT * FROM Visit v
                inner join TechnicianDefault td on td.ID = v.ExclusiveTechnicianDefaultId
                inner join Technician t on t.TechnicianDefaultId = td.ID and t.ScheduleDate = Date(v.TimeStart)
            where TechnicianEmailSentDate is null and ExclusiveTechnicianDefaultId is not null and !v.IsBlockout and !td.IsContractor
                and Date(TimeStart) > Date(Now()) and Date(TimeStart) <= DATE_ADD(Date(Now()), INTERVAL 30 DAY)
                and v.Zip not in (select zip from technicianzip tz where tz.TechnicianId = t.ID)";

        public static List<Visit> FindPossibleNotificationsOutOfArea()
        {
            List<Visit> result = new List<Visit>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindPossibleNotificationsOutOfArea))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion        

        #region FindSecondaryNotificationVisits

        private const string SqlFindSecondaryNotificationVisits =
            @"SELECT * FROM Visit v                
            where TechnicianEmailSentDate is not null and  IsSecondaryEmailSent = 0  
                and DATE_ADD(TechnicianEmailSentDate, INTERVAL 24 HOUR) < Now() ";

        public static List<Visit> FindSecondaryNotificationVisits()
        {
            List<Visit> result = new List<Visit>();            

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindSecondaryNotificationVisits))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion        

        #region ResetIdentity

        private const string SqlResetIdentity =
            @"ALTER TABLE Visit AUTO_INCREMENT = 1";

        public static void ResetIdentity()
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlResetIdentity))
            {
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion


        #region FindDelayedVisits

        private const string SqlFindDelayedVisits =
            @"SELECT v.*, tf.*, a.* FROM visit v                
                inner join TimeFrame tf on tf.ID = v.TimeFrameId
                left join ZipCode zc on zc.Zip = v.Zip
                left join Area a on a.ID = zc.AreaId
            where v.TechnicianId is null and SnapshotDate is null
                {0}";

        public static List<Visit> FindDelayedVisits(DateTime? date)
        {
            string queryString;
            if (date.HasValue)
                queryString = string.Format(SqlFindDelayedVisits, " and Date(v.TimeStart) = ?Date");
            else
            {
                if (Configuration.IsRealtimeMode)
                    queryString = string.Format(SqlFindDelayedVisits, " and Date(v.TimeStart) >= Date(Now())");
                else
                    queryString = string.Format(SqlFindDelayedVisits, string.Empty);
            }
                

            List<Visit> result = new List<Visit>();

            using (IDbCommand dbCommand = Database.PrepareCommand(queryString))
            {
                if (date.HasValue)
                    Database.PutParameter(dbCommand, "?Date", date.Value.Date);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Visit visit = Load(dataReader);
                        visit.ConfirmedTimeFrame = TimeFrame.Load(dataReader, FieldsCount);
                        if (!dataReader.IsDBNull(FieldsCount + TimeFrame.FieldsCount))
                            visit.BucketArea = Domain.Area.Load(dataReader, FieldsCount + TimeFrame.FieldsCount);

                        result.Add(visit);
                    }
                }
            }

            foreach (Visit visit in result)
            {
                int totalCapacity;
                visit.FreeBucketCapacity = BucketConstraint.GetFreeBucketCapacity(
                    visit.Zip, visit.ConfirmedTimeFrame, out totalCapacity);
                visit.TotalBucketCapacity = totalCapacity;
                visit.Details = VisitDetail.FindByVisit(visit);
            }

            return result;
        }

        #endregion

        #region FindTemporaryAssignedVisits

        private const string SqlFindTemporaryAssignedVisits =
            @"SELECT * FROM visit v                
                inner join TimeFrame tf on tf.ID = v.TimeFrameId
            where v.IsTempIgnoreExclusivity = true and SnapshotDate is null
                and Date(v.TimeStart) = ?Date";

        public static List<Visit> FindTemporaryAssignedVisits(DateTime date)
        {
            List<Visit> result = new List<Visit>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindTemporaryAssignedVisits))
            {
                Database.PutParameter(dbCommand, "?Date", date.Date);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Visit visit = Load(dataReader);
                        visit.ConfirmedTimeFrame = TimeFrame.Load(dataReader, FieldsCount);
                        result.Add(visit);
                    }
                }
            }

            foreach (Visit visit in result)
                visit.Details = VisitDetail.FindByVisit(visit);

            return result;

        }

        #endregion

        #region Find By Date

        private const string SqlFindByDate =
            @"SELECT * FROM visit
                Date(TimeStart) = ?Date";

        public static List<Visit> Find(DateTime date)
        {
            List<Visit> result = new List<Visit>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByDate))
            {
                Database.PutParameter(dbCommand, "?Date", date.Date);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion

        #region FindByTicket

        private const string SqlFindByTicketNumber =
            @"SELECT * FROM visit
                where TicketNumber = ?TicketNumber";

        public static Visit FindByTicket(string ticketNumber)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTicketNumber))
            {
                Database.PutParameter(dbCommand, "?TicketNumber", ticketNumber);
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        #endregion

        #region FindModifiedVisits

        private const string SqlFindModifiedVisits =
            @"SELECT * FROM visit v                                
                where SnapshotDate is null 
                    and (IsTempIgnoreExclusivity = true or TempExclusiveTechnicianId is not null)";

        public static List<Visit> FindModifiedVisits()
        {
            List<Visit> result = new List<Visit>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindModifiedVisits))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion

        #region Address

        public string Address
        {
            get
            {
                return Street + ", " + City + ", " + State;
            }
        }

        #endregion

        #region ExclusivityName

        public string ExclusivityName
        {
            get
            {
//                if (ExclusiveCompanyId.HasValue)
//                    return ExclusiveCompany.Name;
                if (ExclusiveTechnicianDefaultId.HasValue)
                    return ExclusiveTechnicianName;
                return string.Empty;
            }
        }

        #endregion        

        #region TimeFrameText

        public string TimeFrameText
        {
            get
            {
                return ConfirmedTimeFrame.Text;
            }
        }

        #endregion

        #region BucketArea

        private Area m_bucketArea;
        public Area BucketArea
        {
            get { return m_bucketArea; }
            set { m_bucketArea = value; }
        }

        #endregion

        #region AreaName

        public string AreaName
        {
            get { return m_bucketArea.Name; }
        }

        #endregion

        #region TechnicianName

        public string TechnicianName
        {
            get { return Technician.Name; }
        }

        #endregion

        #region FreeBucketCapacity

        private int m_freeBucketCapacity;
        public int FreeBucketCapacity
        {
            get { return m_freeBucketCapacity; }
            set { m_freeBucketCapacity = value; }
        }

        #endregion

        #region TotalBucketCapacity

        private int m_totalBucketCapacity;
        public int TotalBucketCapacity
        {
            get { return m_totalBucketCapacity; }
            set { m_totalBucketCapacity = value; }
        }

        #endregion

        #region CapacityText

        public string CapacityText
        {
            get { return m_freeBucketCapacity + "/" + TotalBucketCapacity; }
        }

        #endregion

        #region CanBook

        private bool m_canBook;
        [DataMember]
        public bool CanBook
        {
            get { return m_canBook; }
            set { m_canBook = value; }
        }

        #endregion

        #region GetPrintoutChange

        public string GetPrintoutChange(Visit oldSnapshot)
        {
            string result = string.Empty;

            if (oldSnapshot.TechnicianId == null && TechnicianId != null)
                result += ", Assigned to " + Technician.Name;
            else if (TechnicianId == null && oldSnapshot.TechnicianId != null)
                result += ", Removed to bucket";
            else if (TechnicianId != oldSnapshot.TechnicianId)
                result += ", Reassigned to " + Technician.Name;

            if (TimeFrameId != oldSnapshot.TimeFrameId)
                result += ", Time Frame";

            if (Zip != oldSnapshot.Zip || Street != oldSnapshot.Street
                || Address != oldSnapshot.Address || City != oldSnapshot.City)
            {
                result += ", Address";
            }

            if (Cost != oldSnapshot.Cost)
                result += ", Amount";

            if (ExclusiveTechnicianDefaultId != oldSnapshot.ExclusiveTechnicianDefaultId)
                result += ", Exclusive technician";

            if (HomePhone != oldSnapshot.HomePhone || BusinessPhone != oldSnapshot.BusinessPhone)
                result += ", Phone number";

            if (IsEstimate != oldSnapshot.IsEstimate
                || IsEstimateAndDo != oldSnapshot.IsEstimateAndDo
                || IsRework != oldSnapshot.IsRework)
            {
                result += ", Ticket type";
            }

            if (AdsourceAcronym != oldSnapshot.AdsourceAcronym)
                result += ", Ad Source";

            if (Note != oldSnapshot.Note)
                result += ", Notes";

            if (Details.Count != oldSnapshot.Details.Count)
                result += ", Service item(s)";
            else
            {
                foreach (VisitDetail detail in Details)
                {
                    foreach (VisitDetail oldDetail in oldSnapshot.Details)
                    {
                        if (detail.ServiceId == oldDetail.ServiceId
                            && detail.ItemSequence == oldDetail.ItemSequence)
                        {
                            if (detail.Note != oldDetail.Note
                                || detail.Amount != oldDetail.Amount)
                            {
                                result += ", Service item(s)";
                                break;
                            }
                        }
                    }

                    if (result.Contains(", Service item(s)"))
                        break;
                }
            }

            if (result != string.Empty)
                result = result.Substring(2);
            return result;
        }

        #endregion

        #region Print

        public void Print()
        {
            if (IsBlockout)
                return;

            using (Printer printer = new Printer(Configuration.VisitPrinter))
            {
                printer.Open("Ticket " + TicketNumber);
                printer.Initialize();
                printer.StartNewPage += OnPrinterStartNewPage;

                //Line 1: Header
                printer.StartBoldDoubleWidth();
                printer.WriteLine(string.Format("{0} {1} - {2}", PrintTicketType, Area, PrintServiceType));
                
                //Line 2: Date, mapsco
                printer.Write(string.Format("{0} ", TimeStart.Date.ToString("ddd, MMM dd yyyy")));
                printer.EndBoldDoubleWidth();
                printer.Write(" ");
                printer.StartBoldDoubleWidth();
                printer.WriteLine(string.Format("MAP:{0}", Mapsco.PadLeft(5)));
                printer.EndBoldDoubleWidth();

                //Line 3: Adsource, Rank, Timeframe 
                printer.Write(string.Format("AD: {0} Rank: ", 
                    AdsourceAcronym.PadRight(33 - ConfirmedTimeFrame.Text.Length * 2)));
                printer.StartBoldDoubleWidth();
                printer.Write(CustomerRank.ToString());
                printer.EndBoldDoubleWidth();
                printer.Write(" Time: ");
                printer.StartBoldDoubleWidth();
                printer.WriteLine(ConfirmedTimeFrame.Text);
                printer.EndBoldDoubleWidth();

                if (IsRework) //Line 4,5: Rework info
                {                    
                    printer.WriteLine(string.Format("ORIG. CLNR: {0}", !OriginatedTechnicianDefaultId.HasValue 
                        ? "[ERROR] Not Found" : OriginatedTechnicianName));

                    printer.WriteLine(string.Format("ORIG. DATE: {0}   ORIG. TKT: {1}",
                        OriginatedCompleteDate.Value.ToShortDateString(), OriginatedTicketNumber));
                }

                //Line 6: Customer
                printer.StartBoldDoubleWidth();
                printer.WriteLine(CustomerName);
                printer.EndBoldDoubleWidth();

                //Line 7: Address first line
                printer.WriteLine(Street + (" " + Address2).Trim());

                //Line 8: Address second line, ticket
                printer.Write(string.Format("{0}, {1} {2}", City, State, Zip).PadRight(33));
                printer.StartBoldDoubleWidth();
                printer.WriteLine(string.Format("TKT:{0}", TicketNumber));
                printer.EndBoldDoubleWidth();

                //Line 9: Phones
                printer.WriteLine(string.Format("HM {0} BS {1}", Utils.FormatPhone(HomePhone), 
                    Utils.FormatPhone(BusinessPhone)));

                if (CustomerExclusiveTechnicianDefaultId.HasValue) //Line 10: Customer exclusivity
                {
                    printer.WriteLine(string.Format("EXCL: {0}", CustomerExclusiveTechnicianName));
                }

                //Notes
                List<string> notes = Utils.DivideText(PrintExtendedNote, Printer.PAGE_COLUMNS_COUNT - 7);
                printer.WriteLine(string.Format("Notes: {0}", notes.Count > 0 ? notes[0] : string.Empty));
                if (notes.Count > 0)
                    notes.RemoveAt(0);
                foreach (string note in notes)
                    printer.WriteLine(string.Format("       {0}", note));

                //Exclusive technician
                if (ExclusiveTechnicianDefaultId.HasValue)
                {
                    printer.Write(string.Format("{0}:", PrintExclusivityType));
                    printer.StartBoldDoubleWidth();
                    printer.WriteLine(ExclusiveTechnicianName);
                    printer.EndBoldDoubleWidth();
                }

                printer.WriteLine();

                //Pricing Option
                if (!IsRework && SpecName != string.Empty)
                    printer.WriteLine(string.Format("Pricing option: {0}", SpecName));

                //Ticket details
                foreach (VisitDetail detail in Details)
                {
                    printer.WriteLine(detail.ServiceName);
                    string detailAmount = detail.Amount.ToString("0.00");
                    printer.Write(string.Format("  {0}", detail.Note).PadRight(
                        Printer.PAGE_COLUMNS_COUNT - detailAmount.Length));
                    printer.WriteLine(detailAmount);
                }

                //Discount
                printer.StartItalic();
                printer.WriteLine("DDDDDDDDDDDD".PadLeft(Printer.PAGE_COLUMNS_COUNT));
                printer.EndItalic();
                if (SdPercent > 0)
                {
                    decimal discountAmount = Cost * SdPercent / (100 - SdPercent);
                    string discountString = string.Format("Scheduling Discount: {0}",
                        discountAmount.ToString("0.00").PadLeft(12));
                    printer.WriteLine(discountString.PadLeft(Printer.PAGE_COLUMNS_COUNT));
                }

                //Subtotal
                printer.WriteLine(Cost.ToString("0.00").PadLeft(Printer.PAGE_COLUMNS_COUNT));

                //Tax
                decimal taxAmount = Cost * TaxPercent / 100;
                string taxString = string.Format("Tax: {0}", taxAmount.ToString("0.00").PadLeft(12));
                printer.WriteLine(taxString.PadLeft(Printer.PAGE_COLUMNS_COUNT));

                printer.StartItalic();
                printer.WriteLine("DDDDDDDDDDDD".PadLeft(Printer.PAGE_COLUMNS_COUNT));
                printer.EndItalic();

                //Total
                printer.WriteLine(string.Format("Printed: {0} - {1}{2}", 
                    DateTime.Now.ToString("MM/dd/yy"),
                    DateTime.Now.ToString("hh:mm"),
                    (Cost + taxAmount).ToString("0.00").PadLeft(Printer.PAGE_COLUMNS_COUNT - 25)));

                printer.GoToNextPage();
            }
        }

        private void OnPrinterStartNewPage(Printer printer, int pageNumber)
        {
            printer.WriteLine(string.Format("Ticket number: {0}      Page ({1})", 
                TicketNumber, pageNumber));

            printer.StartBoldDoubleWidth();
            printer.WriteLine(string.Format("{0} {1} - {2}", PrintTicketType, Area, PrintServiceType));
            printer.EndBoldDoubleWidth();
        }

        private string PrintTicketType
        {
            get
            {
                if (IsRework)
                    return "RJ";
                if (IsEstimate)
                    return "ES";
                return "  ";
            }
        }

        private string PrintServiceType
        {
            get
            {
                if (ServType == 1)
                    return "RCC";
                if (ServType == 5)
                    return "CC";
                if (ServType == 4)
                    return "DF";
                if (ServType == 2)
                    return "DC";
                return "???";
            }
        }

        private string PrintExclusivityType
        {
            get
            {
                if (AdsourceAcronym == "DIRREF")
                    return "PREFER";
                if (ExpCred)
                    return "**EXCL";
                return "REFER";
            }
        }

        private string PrintExtendedNote
        {
            get
            {
                string additionalNote = string.Empty;

                if (ServmanBaseTimeFrameId.HasValue)
                {
                    additionalNote += string.Format("SMARTSCHD - ticket was rescheduled, original timeframe is {0}. ",
                        ServmanBaseTimeFrameText);                    
                }

                if (TempExclusiveTechnicianId.HasValue)
                {
                    additionalNote += string.Format("SMARTSCHD - ticket was forced to assign to {0}. ",
                        TempExclusiveTechnicianName);                    
                } 
                else if (IsTempIgnoreExclusivity)
                {
                    additionalNote += "SMARTSCHD - exclusivity was ignored manually. ";                        
                }

                if (IsFixed)
                {
                    additionalNote += string.Format("SMARTSCHD - ticket is fixed on {0} (work hours extension). ",
                        TechnicianName);
                }

                return additionalNote + Note;
            }
        }

        #endregion

        #region Clear

        private const string SqlClearByDate =
            @"delete FROM visit where Date(TimeStart) = ?Date";

        public static void Clear(DateTime date)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlClearByDate))
            {
                Database.PutParameter(dbCommand, "?Date", date.Date);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion

        #region IsChangedReschedulePossible

        public bool IsChangedReschedulePossible(Visit anotherVisitCopy)
        {
            if (ScheduleDate != anotherVisitCopy.ScheduleDate
                || DurationCost != anotherVisitCopy.DurationCost
                || Zip != anotherVisitCopy.Zip
                || TimeFrameId != anotherVisitCopy.TimeFrameId
                || ExclusiveTechnicianDefaultId != anotherVisitCopy.ExclusiveTechnicianDefaultId
                || ForbiddenTechnicianDefaultId != anotherVisitCopy.ForbiddenTechnicianDefaultId
                || !VisitDetail.IsEqual(Details, anotherVisitCopy.Details))
            {
                return true;
            }

            return false;
        }

        #endregion
    }

    public class VisitKey : IComparable<VisitKey>
    {
        #region Constructor

        public VisitKey(int visitId, DateTime timeStart)
        {
            m_visitId = visitId;
            m_timeStart = timeStart;
        }

        #endregion

        #region VisitId

        private int m_visitId;
        public int VisitId
        {
            get { return m_visitId; }
            set { m_visitId = value; }
        }

        #endregion

        #region TimeStart

        private DateTime m_timeStart;
        public DateTime TimeStart
        {
            get { return m_timeStart; }
            set { m_timeStart = value; }
        }

        #endregion

        #region CompareTo

        public int CompareTo(VisitKey other)
        {
            int result = TimeStart.CompareTo(other.TimeStart);
            if (result == 0)
                return VisitId.CompareTo(other.VisitId);
            return result;
        }

        #endregion
    }
}
