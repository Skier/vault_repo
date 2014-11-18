using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;

namespace Dalworth.Server.Web.Partner.Models
{
    public class CallLog
    {
        public CallLog(){}

        #region LoadLogItems

        public void LoadLogItems()
        {
            DateTime? start = null;
            DateTime? end = null;

            if (!string.IsNullOrEmpty(TrackingNumber) && PartnerId.HasValue)
            {
                CallerIdNumber = string.Empty;
                IncomingDid = string.Empty;
                DateRangePreset = "Custom";                
                
                List<OrderSourceOwnPhone> partnerOwnPhones = new List<OrderSourceOwnPhone>();
                using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
                {
                    connection.Open();
                    CallSource = OrderSource.FindByPrimaryKey(PartnerId.Value, connection).Name;
                    partnerOwnPhones = OrderSourceOwnPhone.FindByOrderSource(PartnerId.Value, connection);
                }            
                
                foreach (var ownPhone in partnerOwnPhones)
                {
                    if (ownPhone.Number == TrackingNumber)
                    {
                        CallerIdNumber = TrackingNumber;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(CallerIdNumber))
                    IncomingDid = TrackingNumber;
            }

            DateRangePreset currentPreset = DateRangePresets[DateRangePreset];

            if (currentPreset.Name != "Custom")
            {
                start = currentPreset.DateStart;
                end = currentPreset.DateEnd;               
            }
            else
            {
                try
                {
                    start = DateTime.Parse(DateStart);
                }
                catch (Exception){}                
                
                try
                {
                    end = DateTime.Parse(DateEnd);
                }
                catch (Exception){}                
            }

            DateTime? timeStart = null;
            if (!string.IsNullOrEmpty(TimeStartHours) && !string.IsNullOrEmpty(TimeStartMinutes) && !string.IsNullOrEmpty(TimeStartAmPm))
                timeStart = DateTime.Parse(string.Format("{0}:{1} {2}", TimeStartHours, TimeStartMinutes, TimeStartAmPm));
            if (timeStart.HasValue && timeStart.Value.TimeOfDay.TotalSeconds == 0)
                timeStart = null;
            
            WebUser user = PartnerMembershipProvider.GetCurrentUser();
            int totalItemsCount;            
            List<CallLogItem> allCalls = CallLogItem.Find(null, start, end, CallerIdNumber, IncomingDid, CallSource, TicketNumber,
                user.IsOwner ? (int?)null : user.OrderSourceId.Value, PageNonZero - 1, UserName, 
                CustomerName, Extension, timeStart,
                CallDirection == "Incoming", CallDirection == "Outcoming", 
                ReportFilterId.HasValue ? (CallLogReportFilterEnum?)ReportFilterId.Value : null, AdvertisingSourceId, 
                PartnerId.HasValue, out totalItemsCount);
            TotalItemCount = totalItemsCount;

            if (!PartnerId.HasValue || allCalls.Count < 50)
                CallLogItems = allCalls;
            else
            {                
                int itemsCount = 50;
                if ((PageNonZero - 1) * 50 + 50 > allCalls.Count)
                    itemsCount = allCalls.Count - (PageNonZero - 1)*50;
                CallLogItems = allCalls.GetRange((PageNonZero - 1) * 50, itemsCount);
            }
                
            CalculateStatistics(allCalls);
        }

        #endregion


        public List<CallLogItem> CallLogItems { get; set; }

        [RegularExpression(@"^(?:(?:(?:0?[13578]|1[02])(\/|-|\.)31)\1|(?:(?:0?[13-9]|1[0-2])(\/|-|\.)(?:29|30)\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:0?2(\/|-|\.)29\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:(?:0?[1-9])|(?:1[0-2]))(\/|-|\.)(?:0?[1-9]|1\d|2[0-8])\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$",
            ErrorMessage = "Start Date is not valid")]
        public string DateStart { get; set; }

        [RegularExpression(@"^(?:(?:(?:0?[13578]|1[02])(\/|-|\.)31)\1|(?:(?:0?[13-9]|1[0-2])(\/|-|\.)(?:29|30)\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:0?2(\/|-|\.)29\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:(?:0?[1-9])|(?:1[0-2]))(\/|-|\.)(?:0?[1-9]|1\d|2[0-8])\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$",
            ErrorMessage = "End Date is not valid")]
        public string DateEnd { get; set; }

        [RegularExpression(@"^([0-9]( |-)?)?(\(?[0-9]{3}\)?|[0-9]{3})( |-)?([0-9]{3}( |-)?[0-9]{4}|[a-zA-Z0-9]{7})$",
            ErrorMessage = "Invalid Caller Number phone number")]
        public string CallerIdNumber { get; set; }

        [RegularExpression(@"^([0-9]( |-)?)?(\(?[0-9]{3}\)?|[0-9]{3})( |-)?([0-9]{3}( |-)?[0-9]{4}|[a-zA-Z0-9]{7})$",
            ErrorMessage = "Invalid Incoming Did phone number")]
        public string IncomingDid { get; set; }

        public string CallSource { get; set; }

        [StringLength(6, ErrorMessage = "Ticket number should be 6 characters", MinimumLength = 6)]
        public string TicketNumber { get; set; }

        public string UserName { get; set; }
        
        public string CustomerName { get; set; }

        [RegularExpression(@"^([0-9]{3})$", ErrorMessage = "Extension should have 3 numeric characters")]
        public string Extension { get; set; }

        public string TimeStartHours { get; set; }
        public string TimeStartMinutes { get; set; }
        public string TimeStartAmPm { get; set; }
        public string CallDirection { get; set; }

        public int ExternalCalls { get; set; }
        public int TransferredCalls { get; set; }
        public int TotalCalls { get; set; }
        public int TransactionsCount { get; set; }

        public int Page { get; set; }
        public int PageNonZero
        {
            get
            {
                if (Page < 1)
                    return 1;
                return Page;
            }            
        }

        public int TotalItemCount { get; set; }

        public string TrackingNumber { get; set; }
        public int? PartnerId { get; set; }
        public int? ReportFilterId { get; set; }
        public int? AdvertisingSourceId { get; set; }

        public string TrackingNumberText
        {
            get
            {
                try
                {
                    return String.Format("{0:(###) ###-####}", Int64.Parse(TrackingNumber));
                }
                catch (FormatException)
                {
                    return TrackingNumber;
                }
            }
        }

        public Dictionary<string, DateRangePreset> DateRangePresets
        {
            get
            {
                Dictionary<string, DateRangePreset> result = new Dictionary<string, DateRangePreset>();
                result.Add("Today", new DateRangePreset("Today", DateTime.Now, DateTime.Now));
                result.Add("Yesterday", new DateRangePreset("Yesterday", DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-1)));
                result.Add("Last 7 days", new DateRangePreset("Last 7 days", DateTime.Now.AddDays(-7), null));
                result.Add("Last 15 days", new DateRangePreset("Last 15 days", DateTime.Now.AddDays(-15), null));
                result.Add("Last 30 days", new DateRangePreset("Last 30 days", DateTime.Now.AddDays(-30), null));
                result.Add("Custom", new DateRangePreset("Custom", null, null));
                return result;                
            }
        }

        private string m_dateRangePreset;
        public string DateRangePreset
        {
            get
            {
                if (string.IsNullOrEmpty(m_dateRangePreset))
                    return "Today";
                return m_dateRangePreset;
            }
            set { m_dateRangePreset = value; }
        }
        
        public List<string> HoursSet
        {
            get
            {
                return new List<string>() {"12", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11"};
            }
        }

        public List<string> MinutesSet
        {
            get
            {
                return new List<string>() {"0", "15", "30", "45"};
            }
        }

        public List<string> AmPmSet
        {
            get
            {
                return new List<string>() {"AM", "PM"};
            }
        }

        public List<string> CallDirectionSet
        {
            get
            {
                return new List<string>() { "ALL", "Incoming", "Outcoming" };
            }
        }

        private void CalculateStatistics(List<CallLogItem> allCalls)
        {
            if (!PartnerId.HasValue)
                return;

            if (AdvertisingSourceId.HasValue)
            {
                TransactionsCount = allCalls.Count;
                return;
            }

            if (ReportFilterId == 1 || ReportFilterId > 2)
            {
                TransactionsCount = allCalls.Count;
                return;                
            }

            List<long> processedCalls = new List<long>();
            foreach (var call in allCalls)
            {
                if (processedCalls.Contains(call.Id.Value))
                    continue;
                processedCalls.Add(call.Id.Value);

                if (!call.IsIntermediateCall)
                    ExternalCalls++;
                else
                    TransferredCalls++;
            }

            TotalCalls = ExternalCalls + TransferredCalls;            
        }
    }

    public class DateRangePreset
    {
        public DateRangePreset(string name, DateTime? dateStart, DateTime? dateEnd)
        {
            Name = name;
            DateStart = dateStart;
            DateEnd = dateEnd;            
        }

        public string Name { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
    }
    
}