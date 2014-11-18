using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;

namespace Dalworth.Server.Web.Partner.Models
{
    public class CallLogComplete
    {
        #region LoadLogItems

        public void LoadLogItems()
        {
            DateTime start;
            DateTime end;

            try
            {
                start = DateTime.Parse(DateStart);
            }
            catch (Exception)
            {
                start = new DateTime(1900, 1, 1);
            }

            try
            {
                end = DateTime.Parse(DateEnd);
            }
            catch (Exception)
            {
                end = DateTime.Now;
            }

            int totalItemsCount;

            CallLogCompleteReportFilterEnum? filter = null;
            if (ReportFilterId.HasValue)
                filter = (CallLogCompleteReportFilterEnum)ReportFilterId.Value;

            CallLogItems = CallLogItem.FindCompleted(PageNonZero - 1, start, end, TrackingNumber, PartnerId, 
                AdvertisingSourceId, filter, out totalItemsCount);

            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                PartnerName = OrderSource.FindByPrimaryKey(PartnerId, connection).Name;
            }            
            
            TotalItemCount = totalItemsCount;
        }

        #endregion

        public List<CallLogItem> CallLogItems { get; set; }

        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string TrackingNumber { get; set; }
        public int PartnerId { get; set; }
        public string PartnerName { get; set; }
        public int? AdvertisingSourceId { get; set; }
        public int? ReportFilterId { get; set; }

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

    }
}