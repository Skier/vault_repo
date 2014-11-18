using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;

namespace Dalworth.Server.Web.Partner.Models
{
    public class AccountingReport
    {
        public AccountingReport() { }

        #region DateRangePresets

        public Dictionary<string, DateRangePreset> DateRangePresets
        {
            get
            {
                DateTime startOfCurrentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                Dictionary<string, DateRangePreset> result = new Dictionary<string, DateRangePreset>();
                result.Add("Month to Date", new DateRangePreset("Month to Date", startOfCurrentMonth, startOfCurrentMonth.AddMonths(1)));
                result.Add("Last Month", new DateRangePreset("Last Month", startOfCurrentMonth.AddMonths(-1), startOfCurrentMonth));
                result.Add("Custom", new DateRangePreset("Custom", null, null));
                return result;
            }
        }

        #endregion

        #region DateRangePreset

        private string m_dateRangePreset;
        public string DateRangePreset
        {
            get
            {
                if (string.IsNullOrEmpty(m_dateRangePreset))
                    return "Month to Date";
                return m_dateRangePreset;
            }
            set { m_dateRangePreset = value; }
        }

        #endregion

        #region DateStart

        [RegularExpression(@"^(?:(?:(?:0?[13578]|1[02])(\/|-|\.)31)\1|(?:(?:0?[13-9]|1[0-2])(\/|-|\.)(?:29|30)\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:0?2(\/|-|\.)29\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:(?:0?[1-9])|(?:1[0-2]))(\/|-|\.)(?:0?[1-9]|1\d|2[0-8])\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$",
            ErrorMessage = "Start Date is not valid")]
        public string DateStart { get; set; }

        #endregion

        #region DateEnd

        [RegularExpression(@"^(?:(?:(?:0?[13578]|1[02])(\/|-|\.)31)\1|(?:(?:0?[13-9]|1[0-2])(\/|-|\.)(?:29|30)\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:0?2(\/|-|\.)29\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:(?:0?[1-9])|(?:1[0-2]))(\/|-|\.)(?:0?[1-9]|1\d|2[0-8])\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$",
            ErrorMessage = "End Date is not valid")]
        public string DateEnd { get; set; }

        #endregion

        #region DateStartNoPreset

        public string DateStartNoPreset
        {
            get
            {
                DateTime? result = null;
                DateRangePreset currentPreset = DateRangePresets[DateRangePreset];

                if (currentPreset.Name != "Custom")
                    result = currentPreset.DateStart;
                else
                {
                    try
                    {
                        result = DateTime.Parse(DateStart);
                    }
                    catch (Exception) { }
                }

                if (result != null)
                    return result.Value.ToString("d");
                return string.Empty;
            }
        }

        #endregion

        #region DateEndNoPreset

        public string DateEndNoPreset
        {
            get
            {
                DateTime? result = null;
                DateRangePreset currentPreset = DateRangePresets[DateRangePreset];

                if (currentPreset.Name != "Custom")
                    result = currentPreset.DateEnd;
                else
                {
                    try
                    {
                        result = DateTime.Parse(DateEnd);
                    }
                    catch (Exception) { }
                }

                if (result != null)
                    return result.Value.ToString("d");
                return string.Empty;
            }
        }

        #endregion

        public string CurrentPartner { get; set; }

        public List<AccountingReportTicketItem> TicketItems { get; set; }
        public List<AccountingReportAdsourceItem> AdsourceItems { get; set; }

        public AccountingReportAdsourceItem m_partnerAdsSummary;
        public AccountingReportAdsourceItem PartnerAdsSummary
        {
            get { return m_partnerAdsSummary; }
            set { m_partnerAdsSummary = value; }
        }

        public AccountingReportAdsourceItem m_otherAdsSummary;
        public AccountingReportAdsourceItem OtherAdsSummary
        {
            get { return m_otherAdsSummary; }
            set { m_otherAdsSummary = value; }
        }

        public AccountingReportAdsourceItem m_totalSummary;
        public AccountingReportAdsourceItem TotalSummary
        {
            get { return m_totalSummary; }
            set { m_totalSummary = value; }
        }

        #region LoadItems

        public void LoadItems()
        {
            DateRangePreset currentPreset = DateRangePresets[DateRangePreset];

            DateTime? start = null;
            DateTime? end = null;

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
                catch (Exception) { }

                try
                {
                    end = DateTime.Parse(DateEnd);
                }
                catch (Exception) { }

            }

            if (string.IsNullOrEmpty(CurrentPartner))
                CurrentPartner = GetPartners()[0].Value;

            int callSourceId = int.Parse(CurrentPartner);
            TicketItems = AccountingReportTicketItem.Find(callSourceId, start, end);

            AdsourceItems = AccountingReportAdsourceItem.Find(callSourceId, start, end, out m_partnerAdsSummary,
                out m_otherAdsSummary, out m_totalSummary);
        }

        #endregion

        #region GetPartners

        public List<SelectListItem> GetPartners()
        {
            List<SelectListItem> result = new List<SelectListItem>();

            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                foreach (OrderSource orderSource in OrderSource.Find(connection).OrderBy(source => source.Name))
                {
                    SelectListItem item = new SelectListItem();
                    item.Text = orderSource.Name;
                    item.Value = orderSource.ID.ToString();
                    result.Add(item);
                }
            }

            return result;
        }

        #endregion

        #region GetCurrentUserPartnerName

        public string GetCurrentUserPartnerName()
        {
            WebUser user = PartnerMembershipProvider.GetCurrentUser();
            if (!user.OrderSourceId.HasValue)
                return string.Empty;
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                return OrderSource.FindByPrimaryKey(user.OrderSourceId.Value, connection).Name;
            }

        }

        #endregion
    }

    #region AccountingReportTicketItem class

    public class AccountingReportTicketItem
    {
        public AccountingReportTicketItem(string adsourceAcronym, string partnerName, string incomingDid, 
            bool isTrackingPhone, string ticketNumber, DateTime bookDate, string ticketStatus, 
            DateTime? firstCallDate, decimal? bookAmount, decimal? completeAmount, decimal? bookAmountNotIncluded, 
            decimal? completeAmountNotIncluded, string voiceFileName)
        {
            AdsourceAcronym = adsourceAcronym;
            PartnerName = partnerName;
            IncomingDid = incomingDid;
            IsTrackingPhone = isTrackingPhone;
            TicketNumber = ticketNumber;
            BookDate = bookDate;
            TicketStatus = ticketStatus;
            FirstCallDate = firstCallDate;
            BookAmount = bookAmount;
            CompleteAmount = completeAmount;
            BookAmountNotIncluded = bookAmountNotIncluded;
            CompleteAmountNotIncluded = completeAmountNotIncluded;
            VoiceFileName = voiceFileName;
        }

        public string AdsourceAcronym { get; set; }
        public string PartnerName { get; set; }
        public string IncomingDid { get; set; }
        public bool IsTrackingPhone { get; set; }
        public string TicketNumber { get; set; }
        public DateTime BookDate { get; set; }
        public string TicketStatus { get; set; }
        public DateTime? FirstCallDate { get; set; }
        public decimal? BookAmount { get; set; }
        public decimal? CompleteAmount { get; set; }
        public decimal? BookAmountNotIncluded { get; set; }
        public decimal? CompleteAmountNotIncluded { get; set; }
        public string VoiceFileName { get; set; }

        public string IncomingDidText
        {
            get
            {
                try
                {
                    return String.Format("{0:(###) ###-####}", Int64.Parse(IncomingDid));
                }
                catch (Exception)
                {
                    return IncomingDid;
                }
            }
        }

        public string IsTrackingPhoneText
        {
            get { return IsTrackingPhone ? "Yes" : "No"; }
        }

        private const String SqlFind =
            @"select 
if (osas.OrderSourceId = 4, CompletedOrdersGroup.Acronym, concat(CompletedOrdersGroup.Acronym, ' - NP')),
CompletedOrdersGroup.Partner, d.IncomingDid,
if (CompletedOrdersGroup.OrderSourceId = ?OrderSourceId, true, false) as IsTrackingPhone, CompletedOrdersGroup.TicketNumber,
CompletedOrdersGroup.MinTimeCreated as BookDate, if (CompletedOrdersGroup.TransactionStatus = 5, 'Pending', 'Accounting') as TicketStatus,
d.TimeCreated as FirstCallDate,
if (CompletedOrdersGroup.PartnerId = ?OrderSourceId and CompletedOrdersGroup.TransactionStatus = 5, CompletedOrdersGroup.Amount, null) as BookedAmount,
if (CompletedOrdersGroup.PartnerId = ?OrderSourceId and CompletedOrdersGroup.TransactionStatus = 6, CompletedOrdersGroup.Amount, null) as CompletedAmount,
if (CompletedOrdersGroup.PartnerId != ?OrderSourceId and CompletedOrdersGroup.TransactionStatus = 5, CompletedOrdersGroup.Amount, null) as BookedAmountNotInc,
if (CompletedOrdersGroup.PartnerId != ?OrderSourceId and CompletedOrdersGroup.TransactionStatus = 6, CompletedOrdersGroup.Amount, null) as CompletedAmountNotInc,
d.VoiceFileName, if (osas.OrderSourceId = 4, true, false) as IsPartnerAdsource
from (select CompletedOrders.TicketNumber, ads.Acronym, if (os.Name is null, os2.Name, os.Name) as Partner,
min(t.TimeCreated) as MinTimeCreated, CompletedOrders.OrderSourceId, CompletedOrders.TransactionStatus,
CompletedOrders.Amount, if (os.ID is null, os2.ID, os.ID) as PartnerId, ads.ID as AdsourceId from (SELECT * FROM `Order` o
where o.OrderSourceId = ?OrderSourceId and o.TransactionStatus >= 5 and Date(o.DateCompleted) >= ?DateStart and Date(o.DateCompleted) <= ?DateEnd
union
SELECT o.* FROM `Order` o
inner join OrderSourceAdvertisingSource osas on osas.AdvertisingSourceId = o.AdvertisingSourceId and osas.OrderSourceId = ?OrderSourceId
where (o.OrderSourceId != ?OrderSourceId or o.OrderSourceId is null)  and o.TransactionStatus >= 5 and Date(o.DateCompleted) >= ?DateStart and Date(o.DateCompleted) <= ?DateEnd
) CompletedOrders
left join AdvertisingSource ads on ads.ID = CompletedOrders.AdvertisingSourceId
left join OrderSource os on os.ID = CompletedOrders.OrderSourceId
left join OrderSourceAdvertisingSource osas on osas.AdvertisingSourceId = CompletedOrders.AdvertisingSourceId and CompletedOrders.OrderSourceId is null
left join OrderSource os2 on os2.ID = osas.OrderSourceId
left join `Transaction` t on t.TicketNumber = CompletedOrders.TicketNumber and (t.TransactionCode = 'BOOK' or t.TransactionCode = 'SHPR' or t.TransactionCode = 'ESTM')
 group by t.TicketNumber
having min(t.TimeCreated) is not null) CompletedOrdersGroup
inner join `Transaction` t on t.TicketNumber = CompletedOrdersGroup.TicketNumber and t.TimeCreated = CompletedOrdersGroup.MinTimeCreated
left join DigiumLogItem d on d.ID = t.DigiumLogItemId
left join OrderSourceAdvertisingSource osas on osas.AdvertisingSourceId = CompletedOrdersGroup.AdsourceId
order by IsPartnerAdsource desc, CompletedOrdersGroup.Acronym, CompletedOrdersGroup.Partner, IsTrackingPhone";

        public static List<AccountingReportTicketItem> Find(int orderSourceId, DateTime? dateStart, DateTime? dateEnd)
        {
            List<AccountingReportTicketItem> result = new List<AccountingReportTicketItem>();

            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();

                using (IDbCommand dbCommand = Database.PrepareCommand(SqlFind, connection))
                {
                    Database.PutParameter(dbCommand, "?OrderSourceId", orderSourceId);
                    Database.PutParameter(dbCommand, "?DateStart", dateStart.HasValue ? dateStart.Value.Date : new DateTime(1900, 1, 1));
                    Database.PutParameter(dbCommand, "?DateEnd", dateEnd.HasValue ? dateEnd.Value.Date : DateTime.MaxValue);                                            

                    using (IDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                            result.Add(Load(dataReader));
                    }
                }
            }

            return result;
        }

        private static AccountingReportTicketItem Load(IDataReader dataReader)
        {
            string fileName = dataReader.IsDBNull(12) ? string.Empty : dataReader.GetString(12);
            string fullFileName = string.Empty;
            if (fileName != string.Empty)
                fullFileName = string.Format("{0}{1}/{2}",
                    WebConfigurationManager.AppSettings["VoiceFilesVirtualDirectory"],
                    fileName.Substring(0, 10), fileName);   

            return new AccountingReportTicketItem(
                dataReader.IsDBNull(0) ? string.Empty : dataReader.GetString(0),
                dataReader.GetString(1),
                dataReader.IsDBNull(2) ? string.Empty : dataReader.GetString(2),
                dataReader.GetBoolean(3),
                dataReader.GetString(4),
                dataReader.GetDateTime(5),
                dataReader.GetString(6),
                dataReader.IsDBNull(7) ? (DateTime?)null : dataReader.GetDateTime(7),
                dataReader.IsDBNull(8) ? (decimal?)null : dataReader.GetDecimal(8),
                dataReader.IsDBNull(9) ? (decimal?)null : dataReader.GetDecimal(9),
                dataReader.IsDBNull(10) ? (decimal?)null : dataReader.GetDecimal(10),
                dataReader.IsDBNull(11) ? (decimal?)null : dataReader.GetDecimal(11),
                fullFileName);
        }
    }

    #endregion

    #region AccountingReportAdsourceItem class

    public class AccountingReportAdsourceItem
    {
        public AccountingReportAdsourceItem(){}

        public AccountingReportAdsourceItem(string adsourceAcronym, int ticketsCount, decimal totalBookedAmount, 
            decimal totalCompletedAmount, int ticketsCountAssigned, int ticketsCountNotAssigned, 
            decimal totalBookedAmountAssigned, decimal totalBookedAmountNotAssigned, decimal totalCompletedAmountAssigned, 
            decimal totalCompletedAmountNotAssigned, int? adsourceId)
        {
            AdsourceAcronym = adsourceAcronym;
            TicketsCount = ticketsCount;
            TotalBookedAmount = totalBookedAmount;
            TotalCompletedAmount = totalCompletedAmount;
            TicketsCountAssigned = ticketsCountAssigned;
            TicketsCountNotAssigned = ticketsCountNotAssigned;
            TotalBookedAmountAssigned = totalBookedAmountAssigned;
            TotalBookedAmountNotAssigned = totalBookedAmountNotAssigned;
            TotalCompletedAmountAssigned = totalCompletedAmountAssigned;
            TotalCompletedAmountNotAssigned = totalCompletedAmountNotAssigned;
            AdsourceId = adsourceId;
        }

        public string AdsourceAcronym { get; set; }
        public int TicketsCount { get; set; }
        public decimal TotalBookedAmount { get; set; }
        public decimal TotalCompletedAmount { get; set; }
        public int TicketsCountAssigned { get; set; }
        public int TicketsCountNotAssigned { get; set; }
        public decimal TotalBookedAmountAssigned { get; set; }
        public decimal TotalBookedAmountNotAssigned { get; set; }
        public decimal TotalCompletedAmountAssigned { get; set; }
        public decimal TotalCompletedAmountNotAssigned { get; set; }
        public int? AdsourceId { get; set; }


        public bool IsGroupSummaryRow { get; set; }
        public bool IsTotalSummaryRow { get; set; }
        public bool IsPartnerAdsource { get; set; }

        private const String SqlFind =
            @"select Acronym, count(TicketNumber) TicketsCount,
if (sum(BookedAmount) is null, 0, sum(BookedAmount)) + if (sum(BookedAmountNotInc) is null, 0, sum(BookedAmountNotInc)) as TotalBookAmt,
if (sum(CompletedAmount) is null, 0, sum(CompletedAmount)) + if (sum(CompletedAmountNotInc) is null, 0, sum(CompletedAmountNotInc)) as TotalCompleteAmt,
count(BookedAmount) + count(CompletedAmount) as NumberTicketsAssigned,
count(BookedAmountNotInc) + count(CompletedAmountNotInc) as NumberTicketsNotAssigned,
if (sum(BookedAmount) is null, 0, sum(BookedAmount)) as TotalBookedAssigned,
if (sum(BookedAmountNotInc) is null, 0, sum(BookedAmountNotInc)) as TotalBookedNotAssigned,
if (sum(CompletedAmount) is null, 0, sum(CompletedAmount)) as TotalCompletedAssigned,
if (sum(CompletedAmountNotInc) is null, 0, sum(CompletedAmountNotInc)) as TotalCompletedNotAssigned,
AdsourceId
 from
(select CompletedOrdersGroup.Acronym, CompletedOrdersGroup.Partner, d.IncomingDid,
if (CompletedOrdersGroup.OrderSourceId = ?OrderSourceId, true, false) as IsTrackingPhone, CompletedOrdersGroup.TicketNumber,
CompletedOrdersGroup.MinTimeCreated as BookDate, if (CompletedOrdersGroup.TransactionStatus = 5, 'Pending', 'Accounting') as TicketStatus,
d.TimeCreated as FirstCallDate,
if (CompletedOrdersGroup.PartnerId = ?OrderSourceId and CompletedOrdersGroup.TransactionStatus = 5, CompletedOrdersGroup.Amount, null) as BookedAmount,
if (CompletedOrdersGroup.PartnerId = ?OrderSourceId and CompletedOrdersGroup.TransactionStatus = 6, CompletedOrdersGroup.Amount, null) as CompletedAmount,
if (CompletedOrdersGroup.PartnerId != ?OrderSourceId and CompletedOrdersGroup.TransactionStatus = 5, CompletedOrdersGroup.Amount, null) as BookedAmountNotInc,
if (CompletedOrdersGroup.PartnerId != ?OrderSourceId and CompletedOrdersGroup.TransactionStatus = 6, CompletedOrdersGroup.Amount, null) as CompletedAmountNotInc,
CompletedOrdersGroup.AdsourceId, CompletedOrdersGroup.PartnerId

from (select CompletedOrders.TicketNumber, ads.Acronym, if (os.Name is null, os2.Name, os.Name) as Partner,
min(t.TimeCreated) as MinTimeCreated, CompletedOrders.OrderSourceId, CompletedOrders.TransactionStatus,
CompletedOrders.Amount, if (os.ID is null, os2.ID, os.ID) as PartnerId, ads.ID as AdsourceId from (SELECT * FROM `Order` o
where o.OrderSourceId = ?OrderSourceId and o.TransactionStatus >= 5 and Date(o.DateCompleted) >= ?DateStart and Date(o.DateCompleted) <= ?DateEnd
union
SELECT o.* FROM `Order` o
inner join OrderSourceAdvertisingSource osas on osas.AdvertisingSourceId = o.AdvertisingSourceId and osas.OrderSourceId = ?OrderSourceId
where (o.OrderSourceId != ?OrderSourceId or o.OrderSourceId is null)  and o.TransactionStatus >= 5 and Date(o.DateCompleted) >= ?DateStart and Date(o.DateCompleted) <= ?DateEnd
) CompletedOrders
left join AdvertisingSource ads on ads.ID = CompletedOrders.AdvertisingSourceId
left join OrderSource os on os.ID = CompletedOrders.OrderSourceId
left join OrderSourceAdvertisingSource osas on osas.AdvertisingSourceId = CompletedOrders.AdvertisingSourceId and CompletedOrders.OrderSourceId is null
left join OrderSource os2 on os2.ID = osas.OrderSourceId
left join `Transaction` t on t.TicketNumber = CompletedOrders.TicketNumber and (t.TransactionCode = 'BOOK' or t.TransactionCode = 'SHPR' or t.TransactionCode = 'ESTM')
 group by t.TicketNumber
having min(t.TimeCreated) is not null) CompletedOrdersGroup
inner join `Transaction` t on t.TicketNumber = CompletedOrdersGroup.TicketNumber and t.TimeCreated = CompletedOrdersGroup.MinTimeCreated
left join DigiumLogItem d on d.ID = t.DigiumLogItemId) Report
group by AdsourceId";

        public static List<AccountingReportAdsourceItem> Find(int orderSourceId, DateTime? dateStart, DateTime? dateEnd,
            out AccountingReportAdsourceItem partnerAdsSummary, out AccountingReportAdsourceItem otherAdsSummary, 
            out AccountingReportAdsourceItem totalSummary)
        {
            List<AccountingReportAdsourceItem> result = new List<AccountingReportAdsourceItem>();
            List<int> partnerAdsources = new List<int>();            

            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();

                List<Domain.AdvertisingSource> advertisingSources = Domain.AdvertisingSource.FindByPartner(
                    orderSourceId, connection);
                partnerAdsources.AddRange(advertisingSources.Select(advertisingSource => advertisingSource.ID));

                using (IDbCommand dbCommand = Database.PrepareCommand(SqlFind, connection))
                {
                    Database.PutParameter(dbCommand, "?OrderSourceId", orderSourceId);
                    Database.PutParameter(dbCommand, "?DateStart", dateStart.HasValue ? dateStart.Value.Date : new DateTime(1900, 1, 1));
                    Database.PutParameter(dbCommand, "?DateEnd", dateEnd.HasValue ? dateEnd.Value.Date : DateTime.MaxValue);

                    using (IDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                            result.Add(Load(dataReader));
                    }
                }
            }

            partnerAdsSummary = new AccountingReportAdsourceItem();
            otherAdsSummary = new AccountingReportAdsourceItem();
            totalSummary = new AccountingReportAdsourceItem();

            if (result.Count == 0)
                return result;

            foreach (var item in result)
            {
                if (item.AdsourceId.HasValue && partnerAdsources.Contains(item.AdsourceId.Value))
                    item.IsPartnerAdsource = true;
            }

            result = result.OrderByDescending(item => item.IsPartnerAdsource)
                .ThenBy(item => item.AdsourceAcronym).ToList();

            partnerAdsSummary.AdsourceAcronym = "PARTNER ADS SUMMARY";
            otherAdsSummary.AdsourceAcronym = "OTHER ADS SUMMARY";
            totalSummary.AdsourceAcronym = "SUMMARY";

            partnerAdsSummary.IsGroupSummaryRow = true;
            otherAdsSummary.IsGroupSummaryRow = true;
            totalSummary.IsTotalSummaryRow = true;

            foreach (var item in result)
            {
                if (item.IsPartnerAdsource)
                {
                    partnerAdsSummary.TicketsCount += item.TicketsCount;
                    partnerAdsSummary.TicketsCountAssigned += item.TicketsCountAssigned;
                    partnerAdsSummary.TicketsCountNotAssigned += item.TicketsCountNotAssigned;
                    partnerAdsSummary.TotalBookedAmount += item.TotalBookedAmount;
                    partnerAdsSummary.TotalBookedAmountAssigned += item.TotalBookedAmountAssigned;
                    partnerAdsSummary.TotalBookedAmountNotAssigned += item.TotalBookedAmountNotAssigned;
                    partnerAdsSummary.TotalCompletedAmount += item.TotalCompletedAmount;
                    partnerAdsSummary.TotalCompletedAmountAssigned += item.TotalCompletedAmountAssigned;
                    partnerAdsSummary.TotalCompletedAmountNotAssigned += item.TotalCompletedAmountNotAssigned;                    
                } else
                {
                    otherAdsSummary.TicketsCount += item.TicketsCount;
                    otherAdsSummary.TicketsCountAssigned += item.TicketsCountAssigned;
                    otherAdsSummary.TicketsCountNotAssigned += item.TicketsCountNotAssigned;
                    otherAdsSummary.TotalBookedAmount += item.TotalBookedAmount;
                    otherAdsSummary.TotalBookedAmountAssigned += item.TotalBookedAmountAssigned;
                    otherAdsSummary.TotalBookedAmountNotAssigned += item.TotalBookedAmountNotAssigned;
                    otherAdsSummary.TotalCompletedAmount += item.TotalCompletedAmount;
                    otherAdsSummary.TotalCompletedAmountAssigned += item.TotalCompletedAmountAssigned;
                    otherAdsSummary.TotalCompletedAmountNotAssigned += item.TotalCompletedAmountNotAssigned;                    
                }

                totalSummary.TicketsCount += item.TicketsCount;
                totalSummary.TicketsCountAssigned += item.TicketsCountAssigned;
                totalSummary.TicketsCountNotAssigned += item.TicketsCountNotAssigned;
                totalSummary.TotalBookedAmount += item.TotalBookedAmount;
                totalSummary.TotalBookedAmountAssigned += item.TotalBookedAmountAssigned;
                totalSummary.TotalBookedAmountNotAssigned += item.TotalBookedAmountNotAssigned;
                totalSummary.TotalCompletedAmount += item.TotalCompletedAmount;
                totalSummary.TotalCompletedAmountAssigned += item.TotalCompletedAmountAssigned;
                totalSummary.TotalCompletedAmountNotAssigned += item.TotalCompletedAmountNotAssigned;
            }

            bool isPartnerAdInserted = false;
            foreach (var item in result)
            {
                if (!item.IsPartnerAdsource)
                {
                    result.Insert(result.IndexOf(item), partnerAdsSummary);
                    isPartnerAdInserted = true;
                    break;
                }
            }

            if (!isPartnerAdInserted)
                result.Add(partnerAdsSummary);
            result.Add(otherAdsSummary);
            result.Add(totalSummary);

            return result;
        }

        private static AccountingReportAdsourceItem Load(IDataReader dataReader)
        {
            return new AccountingReportAdsourceItem(
                dataReader.IsDBNull(0) ? string.Empty : dataReader.GetString(0),
                dataReader.GetInt32(1),
                dataReader.GetDecimal(2),
                dataReader.GetDecimal(3),
                dataReader.GetInt32(4),
                dataReader.GetInt32(5),
                dataReader.GetDecimal(6),
                dataReader.GetDecimal(7),
                dataReader.GetDecimal(8),
                dataReader.GetDecimal(9),
                dataReader.IsDBNull(10) ? (int?)null : dataReader.GetInt32(10));
        }
    }

    #endregion

}