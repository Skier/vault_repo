using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using SmartSchedule.Domain.Servman;

namespace SmartSchedule.Domain.Sync
{
    public enum OrderChangeTypeEnum
    {
        NoChange,
        Change,        
        MustRescheduleChange
    }

    [DataContract]
    public class Order
    {
        #region Constructor

        public Order(string ticket_num, string customer, string block, string prefix, string street,
            string suffix, string city, string state, string zip, DateTime d_schedule, string t_schedule,
            decimal amount, string tech_refer, DateTime d_1st_call, string t_1st_call,
            int tran_type, int spec_id, string mapsco, string tech_id_original, string homePhone,
            string businessPhone, string address2, string area, int servType, string adsourceAcronym,
            int customerRank, DateTime? originatedCompleteDate,
            string originatedTicketNumber, string customerExclusiveTechId,
            string note, bool expCred, string specName, decimal sdPercent,
            decimal taxPercent)
        {
            m_ticketNumber = ticket_num.Trim();
            m_cutomer = customer.Trim();
            m_street = GetStreet(block, prefix, street, suffix);
            m_city = city.Trim();
            m_state = state.Trim();
            m_zip = zip;
            m_dateSchedule = d_schedule.Date;
            m_timeFrameId = Utils.GetTimeFrameId(t_schedule.Trim());
            m_cost = amount;
            m_durationCost = m_cost;
            if (tech_refer != null)
                m_exclusiveTechnicianDefaultServmanId = tech_refer.Trim();

            m_callDateTime = d_1st_call.Date.
                AddHours(int.Parse(t_1st_call.Substring(0, 2))).
                AddMinutes(int.Parse(t_1st_call.Substring(2, 2)));

            if (tran_type == 2) //Rework
            {
                if (tech_id_original != null)
                {
                    m_exclusiveTechnicianDefaultServmanId = tech_id_original.Trim();
                    m_originatedTechnicianServmanId = tech_id_original.Trim();
                }
                    
                m_isRework = true;
            }
            else if (tran_type == 4)
            {
                if (spec_id == 2186) //Estimate & Do
                {
                    m_durationCost = 200;
                    m_isEstimateAndDo = true;                    
                } else
                    m_isEstimate = true; //Estimate
            }

            m_mapsco = mapsco.Trim();
            m_homePhone = homePhone.Trim();
            m_businessPhone = businessPhone.Trim();
            m_address2 = address2.Trim();
            m_areaId = area.Trim();
            m_servType = servType;
            m_adsourceAcronym = adsourceAcronym.Trim();
            m_customerRank = customerRank;

            m_originatedCompleteDate = originatedCompleteDate;
            m_originatedTicketNumber = originatedTicketNumber.Trim();

            if (customerExclusiveTechId != null)
                m_customerExclusiveTechnicianServmanId = customerExclusiveTechId.Trim();

            m_note = note.Trim();
            m_expCred = expCred;
            m_specName = specName.Trim();
            m_sdPercent = sdPercent;
            m_taxPercent = taxPercent;
        }

        #endregion        

        #region TicketNumber

        private string m_ticketNumber;
        [DataMember]
        public string TicketNumber
        {
            get { return m_ticketNumber; }
            set { m_ticketNumber = value; }
        }

        #endregion

        #region Cutomer

        private string m_cutomer;
        [DataMember]
        public string Cutomer
        {
            get { return m_cutomer; }
            set { m_cutomer = value; }
        }

        #endregion

        #region Street

        private string m_street;
        [DataMember]
        public string Street
        {
            get { return m_street; }
            set { m_street = value; }
        }

        #endregion

        #region Address2

        private string m_address2;
        [DataMember]
        public string Address2
        {
            get { return m_address2; }
            set { m_address2 = value; }
        }

        #endregion

        #region City

        private string m_city;
        [DataMember]
        public string City
        {
            get { return m_city; }
            set { m_city = value; }
        }

        #endregion

        #region State

        private string m_state;
        [DataMember]
        public string State
        {
            get { return m_state; }
            set { m_state = value; }
        }

        #endregion

        #region Zip

        private string m_zip;
        [DataMember]
        public string Zip
        {
            get { return m_zip; }
            set { m_zip = value; }
        }

        #endregion

        #region Latitude

        private float m_latitude;
        [DataMember]
        public float Latitude
        {
            get { return m_latitude; }
            set { m_latitude = value; }
        }

        #endregion

        #region Longitude

        private float m_longitude;
        [DataMember]
        public float Longitude
        {
            get { return m_longitude; }
            set { m_longitude = value; }
        }

        #endregion

        #region DateSchedule

        private DateTime m_dateSchedule;
        [DataMember]
        public DateTime DateSchedule
        {
            get { return m_dateSchedule; }
            set { m_dateSchedule = value; }
        }

        #endregion

        #region TimeFrameId

        private int m_timeFrameId;
        [DataMember]
        public int TimeFrameId
        {
            get { return m_timeFrameId; }
            set { m_timeFrameId = value; }
        }

        #endregion

        #region Cost

        private decimal m_cost;
        [DataMember]
        public decimal Cost
        {
            get { return m_cost; }
            set { m_cost = value; }
        }

        #endregion        

        #region ExclusiveTechnicianDefault

        private string m_exclusiveTechnicianDefaultServmanId;
        [DataMember]
        public string ExclusiveTechnicianDefaultServmanId
        {
            get { return m_exclusiveTechnicianDefaultServmanId; }
            set { m_exclusiveTechnicianDefaultServmanId = value; }
        }

        private TechnicianDefault m_exclusiveTechnicianDefault;        
        public TechnicianDefault ExclusiveTechnicianDefault
        {
            get { return m_exclusiveTechnicianDefault; }
            set { m_exclusiveTechnicianDefault = value; }
        }

        public int? ExclusiveTechnicianDefaultId
        {
            get
            {
                if (m_exclusiveTechnicianDefault == null)
                    return null;
                return m_exclusiveTechnicianDefault.ID;
            }
        }

        #endregion

        #region IsEstimate

        private bool m_isEstimate;
        [DataMember]
        public bool IsEstimate
        {
            get { return m_isEstimate; }
            set { m_isEstimate = value; }
        }

        #endregion

        #region IsEstimateAndDo

        private bool m_isEstimateAndDo;
        [DataMember]
        public bool IsEstimateAndDo
        {
            get { return m_isEstimateAndDo; }
            set { m_isEstimateAndDo = value; }
        }

        #endregion

        #region IsRework

        private bool m_isRework;
        [DataMember]
        public bool IsRework
        {
            get { return m_isRework; }
            set { m_isRework = value; }
        }

        #endregion

        #region CallDateTime

        private DateTime m_callDateTime;
        [DataMember]
        public DateTime CallDateTime
        {
            get { return m_callDateTime; }
            set { m_callDateTime = value; }
        }

        #endregion

        #region OrderDetails

        private List<OrderDetail> m_orderDetails;
        [DataMember]
        public List<OrderDetail> OrderDetails
        {
            get { return m_orderDetails; }
            set { m_orderDetails = value; }
        }

        #endregion

        #region Mapsco

        private string m_mapsco;
        [DataMember]
        public string Mapsco
        {
            get { return m_mapsco; }
            set { m_mapsco = value; }
        }

        #endregion

        #region HomePhone

        private string m_homePhone;
        [DataMember]
        public string HomePhone
        {
            get { return m_homePhone; }
            set { m_homePhone = value; }
        }

        #endregion

        #region BusinessPhone

        private string m_businessPhone;
        [DataMember]
        public string BusinessPhone
        {
            get { return m_businessPhone; }
            set { m_businessPhone = value; }
        }

        #endregion

        #region AreaId

        protected string m_areaId;
        [DataMember]
        public string AreaId
        {
            get { return m_areaId; }
            set { m_areaId = value; }
        }

        #endregion

        #region ServType

        protected int m_servType;
        [DataMember]
        public int ServType
        {
            get { return m_servType; }
            set { m_servType = value; }
        }

        #endregion

        #region AdsourceAcronym

        protected string m_adsourceAcronym;
        [DataMember]
        public string AdsourceAcronym
        {
            get { return m_adsourceAcronym; }
            set { m_adsourceAcronym = value; }
        }

        #endregion

        #region CustomerRank

        protected int m_customerRank;
        [DataMember]
        public int CustomerRank
        {
            get { return m_customerRank; }
            set { m_customerRank = value; }
        }

        #endregion

        #region OriginatedTechnicianId

        private string m_originatedTechnicianServmanId;
        [DataMember]
        public string OriginatedTechnicianServmanId
        {
            get { return m_originatedTechnicianServmanId; }
            set { m_originatedTechnicianServmanId = value; }
        }

        private int? m_originatedTechnicianDefaultId;
        public int? OriginatedTechnicianDefaultId
        {
            get { return m_originatedTechnicianDefaultId; }
            set { m_originatedTechnicianDefaultId = value; }
        }

        #endregion

        #region OriginatedCompleteDate

        protected DateTime? m_originatedCompleteDate;
        [DataMember]
        public DateTime? OriginatedCompleteDate
        {
            get { return m_originatedCompleteDate; }
            set { m_originatedCompleteDate = value; }
        }

        #endregion

        #region OriginatedTicketNumber

        protected string m_originatedTicketNumber;
        [DataMember]
        public string OriginatedTicketNumber
        {
            get { return m_originatedTicketNumber; }
            set { m_originatedTicketNumber = value; }
        }

        #endregion

        #region CustomerExclusiveTechnicianDefaultId

        protected string m_customerExclusiveTechnicianServmanId;
        [DataMember]
        public string CustomerExclusiveTechnicianServmanId
        {
            get { return m_customerExclusiveTechnicianServmanId; }
            set { m_customerExclusiveTechnicianServmanId = value; }
        }

        protected int? m_customerExclusiveTechnicianDefaultId;
        public int? CustomerExclusiveTechnicianDefaultId
        {
            get { return m_customerExclusiveTechnicianDefaultId; }
            set { m_customerExclusiveTechnicianDefaultId = value; }
        }

        #endregion

        #region Note

        protected string m_note;
        [DataMember]
        public string Note
        {
            get { return m_note; }
            set { m_note = value; }
        }

        #endregion

        #region ExpCred

        protected bool m_expCred;
        [DataMember]
        public bool ExpCred
        {
            get { return m_expCred; }
            set { m_expCred = value; }
        }

        #endregion

        #region SpecName

        protected string m_specName;
        [DataMember]
        public string SpecName
        {
            get { return m_specName; }
            set { m_specName = value; }
        }

        #endregion

        #region SdPercent

        protected decimal m_sdPercent;
        [DataMember]
        public decimal SdPercent
        {
            get { return m_sdPercent; }
            set { m_sdPercent = value; }
        }

        #endregion

        #region TaxPercent

        protected decimal m_taxPercent;
        [DataMember]
        public decimal TaxPercent
        {
            get { return m_taxPercent; }
            set { m_taxPercent = value; }
        }

        #endregion

        #region DurationCost

        protected decimal m_durationCost;
        [DataMember]
        public decimal DurationCost
        {
            get { return m_durationCost; }
            set { m_durationCost = value; }
        }

        #endregion


        #region GetStreet

        private static string GetStreet(string block, string prefix, string street, string suffix)
        {
            string result = string.Format("{0} {1} {2} {3}", 
                block.Trim(), prefix.Trim(), street.Trim(), suffix.Trim());
            return Regex.Replace(result, @"\s{2,}", " ");
        }

        #endregion

        #region GetOrderChangeType

        public OrderChangeTypeEnum GetOrderChangeType(Visit visit)
        {
            if (DateSchedule.Date != visit.ScheduleDate
                || m_cost != visit.Cost
                || m_zip != visit.Zip)
            {
                return OrderChangeTypeEnum.MustRescheduleChange;
            }

            if (!visit.ServmanBaseTimeFrameId.HasValue
                && m_timeFrameId != visit.ConfirmedTimeFrame.ID)
            {
                return OrderChangeTypeEnum.MustRescheduleChange;
            }

            if (visit.ServmanBaseTimeFrameId.HasValue && m_timeFrameId != visit.ConfirmedTimeFrame.ID
                && m_timeFrameId != visit.ServmanBaseTimeFrameId.Value)
            {
                return OrderChangeTypeEnum.MustRescheduleChange;
            }

            if ((m_exclusiveTechnicianDefault == null && visit.ExclusiveTechnicianDefaultId != null)
                || (m_exclusiveTechnicianDefault != null && visit.ExclusiveTechnicianDefaultId == null)
                || (m_exclusiveTechnicianDefault != null && visit.ExclusiveTechnicianDefaultId != null
                    && m_exclusiveTechnicianDefault.ID != visit.ExclusiveTechnicianDefaultId.Value))
            {
                return OrderChangeTypeEnum.MustRescheduleChange;
            }

            if (OrderDetail.AreOrderDetailsChanged(OrderDetails, visit.Details))
                return OrderChangeTypeEnum.MustRescheduleChange;
         

            if (m_cutomer != visit.CustomerName
                || m_street != visit.Street
                || m_address2 != visit.Address2
                || m_city != visit.City
                || m_state != visit.State                
                || m_callDateTime != visit.CallDateTime.Value
                || m_areaId != visit.Area
                || m_servType != visit.ServType
                || m_adsourceAcronym != visit.AdsourceAcronym
                || m_customerRank != visit.CustomerRank
                || m_originatedTechnicianDefaultId != visit.OriginatedTechnicianDefaultId
                || m_originatedCompleteDate != visit.OriginatedCompleteDate
                || m_originatedTicketNumber != visit.OriginatedTicketNumber
                || m_customerExclusiveTechnicianDefaultId != visit.CustomerExclusiveTechnicianDefaultId
                || m_note != visit.Note
                || m_expCred != visit.ExpCred
                || m_specName != visit.SpecName
                || m_sdPercent != visit.SdPercent
                || m_taxPercent != visit.TaxPercent)
            {
                return OrderChangeTypeEnum.Change;
            }

            return OrderChangeTypeEnum.NoChange;
        }

        #endregion

        #region Geocode

        public void Geocode()
        {
            if (Latitude != 0 && Longitude != 0)
                return; //Already geocoded

            string addressString = Street + "," + City + "," + State + "," + Zip;

            Coordinate coordinate = GoogleGeocoder.Geocode(addressString);
            if (!coordinate.IsValid)
                Host.Trace("Batch Geocoding", "Address " + addressString + " is invalid. " 
                    + coordinate.ErrorText);

            Latitude = coordinate.Latitude;
            Longitude = coordinate.Longitude;
        }

        #endregion

        private double m_minDrive;        
        public double MinDrive
        {
            get { return m_minDrive; }
            set { m_minDrive = value; }
        }

        private bool m_isSeed;
        public bool IsSeed
        {
            get { return m_isSeed; }
            set { m_isSeed = value; }
        }

        private Technician m_forceTechnician;
        public Technician ForceTechnician
        {
            get { return m_forceTechnician; }
            set { m_forceTechnician = value; }
        }
    }
}
