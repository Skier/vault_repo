using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class PartnerSummaryReportItem
    {
        public PartnerSummaryReportItem(){}

        #region GenerateWeeklyReport
        
        private static void GenerateWeeklyReport(DateTime dateStart, DateTime dateEnd)
        {
            List<PartnerSummaryItem> items = PartnerSummaryItem.Find(dateStart, dateEnd, null);

            List<PartnerSummaryReportItem> reportItems = new List<PartnerSummaryReportItem>();
            foreach (PartnerSummaryItem item in items)
            {
                if (item.IsPartnerSummaryRow || item.IsTotalSummaryRow)
                    continue;

                reportItems.Add(new PartnerSummaryReportItem(
                    dateEnd.Date, item.CallSourceId,
                    item.AdvertisingSourceId.HasValue ? string.Empty : item.TrackingNumber,
                    item.AdvertisingSourceId.HasValue ? item.TrackingNumber : string.Empty,
                    item.CallCount, item.BookCount, item.ShopperCount, item.OtherActionsCount,
                    item.CancelCount, item.InProcessCount, item.CompletedCount, item.ClosedAmount, false));
            }

            BackgroundJobPending backgroundJob = new BackgroundJobPending();
            backgroundJob.BackgroundJobType = BackgroundJobTypeEnum.PartnerSiteSummaryReport;

            try
            {
                Database.Begin();
                Insert(reportItems);
                BackgroundJobPending.Insert(backgroundJob);
                Database.Commit();
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }            
        }

        public static void GenerateWeeklyReport()
        {
            DateTime lastSaturday = DateTime.Now.Date.AddDays(-(int) DateTime.Now.DayOfWeek - 1);
            if (FindByWeek(lastSaturday).Count != 0)
                return;

            DateTime weekStart = lastSaturday.AddDays(-6);
            DateTime reportStart = new DateTime(lastSaturday.Year, lastSaturday.Month, 1);
            DateTime reportEnd = lastSaturday.AddHours(23).AddMinutes(59).AddSeconds(59);

            if (weekStart.Month != reportEnd.Month)
            {
                DateTime prevMonthReportStart = reportStart.AddMonths(-1);
                int daysInMonth = DateTime.DaysInMonth(prevMonthReportStart.Year, prevMonthReportStart.Month);
                GenerateWeeklyReport(prevMonthReportStart,
                    new DateTime(prevMonthReportStart.Year, prevMonthReportStart.Month, daysInMonth)
                    .AddHours(23).AddMinutes(59).AddSeconds(59));                
            } 
            
            GenerateWeeklyReport(reportStart, reportEnd);
        }

        #endregion

        #region FindByWeek

        private const String SqlFindByWeek =
            @"select * from PartnerSummaryReportItem
                where GenerateDate >= ?DateStart and GenerateDate <= ?DateEnd";

        public static List<PartnerSummaryReportItem> FindByWeek(DateTime dateInWeek)
        {
            DateTime weekStart = dateInWeek.Date.AddDays(-(int)dateInWeek.DayOfWeek);
            DateTime weekEnd = weekStart.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59);

            List<PartnerSummaryReportItem> result = new List<PartnerSummaryReportItem>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWeek))
            {
                Database.PutParameter(dbCommand, "?DateStart", weekStart);
                Database.PutParameter(dbCommand, "?DateEnd", weekEnd);
                
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion        

        #region FindNotSent

        private const String SqlFindNotSent =
            @"select * from PartnerSummaryReportItem
                where IsSent = 0
               order by GenerateDate, OrderSourceId, PhoneNumber = '', PhoneNumber, AdsourceName";

        public static List<PartnerSummaryReportItem> FindNotSent()
        {
            List<PartnerSummaryReportItem> result = new List<PartnerSummaryReportItem>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindNotSent))
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

        #region SourceText

        public string SourceText
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(PhoneNumber))
                        return AdsourceName;
                    return String.Format("{0:(###) ###-####}", Int64.Parse(PhoneNumber));
                }
                catch (FormatException)
                {
                    return AdsourceName;
                }
            }
        }

        #endregion
    }

    public class PartnerSummaryItem
    {
        #region Constructors

        public PartnerSummaryItem() { }

        public PartnerSummaryItem(int callSourceId, string callSourceName, string trackingNumber, int callCount,
            int bookCount, int otherActionsCount, int shopperCount, int cancelCount, int inProcessCount, 
            int completedCount, decimal closedAmount, int? advertisingSourceId, string trackingUrl)
        {
            CallSourceId = callSourceId;
            CallSourceName = callSourceName;
            TrackingNumber = trackingNumber;
            CallCount = callCount;
            BookCount = bookCount;
            CancelCount = cancelCount;
            OtherActionsCount = otherActionsCount;
            ShopperCount = shopperCount;
            InProcessCount = inProcessCount;
            CompletedCount = completedCount;
            ClosedAmount = closedAmount;
            AdvertisingSourceId = advertisingSourceId;
            TrackingUrl = trackingUrl;
        }

        #endregion

        #region Properties

        public int CallSourceId { get; set; }
        public string CallSourceName { get; set; }
        public string TrackingNumber { get; set; }
        public int CallCount { get; set; }
        public int BookCount { get; set; }
        public int CancelCount { get; set; }
        public int OtherActionsCount { get; set; }
        public int ShopperCount { get; set; }
        public int InProcessCount { get; set; }
        public int CompletedCount { get; set; }
        public decimal ClosedAmount { get; set; }

        public int PartnerRecordsCount { get; set; }
        public bool IsPartnerSummaryRow { get; set; }
        public bool IsTotalSummaryRow { get; set; }
        public bool IsSummaryRow
        {
            get { return IsPartnerSummaryRow || IsTotalSummaryRow; }
        }
        public string TrackingUrl { get; set; }

        #endregion


        #region TrackingNumberText

        public string TrackingNumberText
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(TrackingNumber))
                        return " ";
                    return String.Format("{0:(###) ###-####}", Int64.Parse(TrackingNumber));
                }
                catch (FormatException)
                {
                    return TrackingNumber;
                }
            }
        }

        #endregion

        #region IsAdvertisingSource

        public bool IsAdvertisingSource
        {
            get
            {
                return AdvertisingSourceId.HasValue;
            }
        }

        #endregion


        #region ClosedAmountText

        public string ClosedAmountText
        {
            get
            {
                return ClosedAmount.ToString("C");
            }
        }

        #endregion

        public int? AdvertisingSourceId { get; set; }

        #region Find

        private const String SqlFind =
        @"select * from ((select if (TblCallCount.CallSourceId is null, TblComplete.CallSourceId, TblCallCount.CallSourceId) CallSourceId,
if (TblCallCount.Name is null, TblComplete.Name, TblCallCount.Name) Name,
if (TblCallCount.TrackingNumber is null, TblComplete.TrackingNumber, TblCallCount.TrackingNumber) TrackingNumber,
TblCallCount.CallsCount, TblBookCount.BookCount, 
TblOtherActionsCount.OtherActionsCount as OtherActions, 
TblShopperCount.ShopperCount ShopperCount,
TblComplete.CancelCount, TblComplete.InProcessCount, TblComplete.CompleteCount, TblComplete.CompleteAmount, null as AdSourceId, '' as TrackingUrl from
(SELECT d.CallSourceId, os.Name, if (osp.Number is not null, osp.Number, d.IncomingDid) TrackingNumber, count(d.ID) CallsCount
        FROM DigiumLogItem d
            inner join OrderSource os on os.ID = d.CallSourceId
            left join OrderSourceOwnPhone osp on osp.OrderSourceId = d.CallSourceId and osp.Number = d.CallerIdNumber
        where Date(d.TimeCreated) >= ?DateStart and Date(d.TimeCreated) <= ?DateEnd and d.IsIntermediateCall = 0
        group by d.CallSourceId, TrackingNumber) TblCallCount
left join (SELECT d.CallSourceId, if (osp.Number is not null, osp.Number, d.IncomingDid) TrackingNumber,
            count(distinct t.ServmanWorkflowId) BookCount
        FROM DigiumLogItem d
            inner join OrderSource os on os.ID = d.CallSourceId
            left join OrderSourceOwnPhone osp on osp.OrderSourceId = d.CallSourceId and osp.Number = d.CallerIdNumber
            left join `Transaction` t on t.DigiumLogItemId = d.ID and (t.TransactionCode = 'BOOK' or t.TransactionCode = 'ESTM' )
        where Date(d.TimeCreated) >= ?DateStart and Date(d.TimeCreated) <= ?DateEnd 
        group by d.CallSourceId, TrackingNumber) TblBookCount on TblBookCount.CallSourceId = TblCallCount.CallSourceId and TblBookCount.TrackingNumber = TblCallCount.TrackingNumber
left join (SELECT d.CallSourceId, if (osp.Number is not null, osp.Number, d.IncomingDid) TrackingNumber,
            count(distinct d.ID) OtherActionsCount
        FROM DigiumLogItem d
            inner join OrderSource os on os.ID = d.CallSourceId
            left join OrderSourceOwnPhone osp on osp.OrderSourceId = d.CallSourceId and osp.Number = d.CallerIdNumber
            left join `Transaction` t on t.DigiumLogItemId = d.ID
        where Date(d.TimeCreated) >= ?DateStart and Date(d.TimeCreated) <= ?DateEnd and d.IsIntermediateCall = 0
          and ((t.DigiumLogItemId is null) or (t.DigiumLogItemId is not null and t.TransactionCode != 'BOOK' and t.TransactionCode != 'SHPR' and t.TransactionCode != 'ESTM'))
        group by d.CallSourceId, TrackingNumber) TblOtherActionsCount on TblOtherActionsCount.CallSourceId = TblCallCount.CallSourceId and TblOtherActionsCount.TrackingNumber = TblCallCount.TrackingNumber
left join (SELECT d.CallSourceId, if (osp.Number is not null, osp.Number, d.IncomingDid) TrackingNumber,
            count(distinct t.ServmanWorkflowId) ShopperCount
        FROM DigiumLogItem d
            inner join OrderSource os on os.ID = d.CallSourceId
            left join OrderSourceOwnPhone osp on osp.OrderSourceId = d.CallSourceId and osp.Number = d.CallerIdNumber
            left join `Transaction` t on t.DigiumLogItemId = d.ID and t.TransactionCode = 'SHPR'
        where Date(d.TimeCreated) >= ?DateStart and Date(d.TimeCreated) <= ?DateEnd and TRIM(t.TicketNumber) != ''
        group by d.CallSourceId, TrackingNumber)  TblShopperCount on TblShopperCount.CallSourceId = TblCallCount.CallSourceId and TblShopperCount.TrackingNumber = TblCallCount.TrackingNumber
left join (SELECT d.CallSourceId, os.Name, if (osp.Number is not null, osp.Number, d.IncomingDid) TrackingNumber,
  count(distinct o2.TicketNumber) as CancelCount, count(distinct o3.TicketNumber) as InProcessCount,
count(distinct o4.TicketNumber) as CompleteCount, sum(o4.Amount) as CompleteAmount FROM DigiumLogItem d
            inner join OrderSource os on os.ID = d.CallSourceId
            left join OrderSourceOwnPhone osp on osp.OrderSourceId = d.CallSourceId and osp.Number = d.CallerIdNumber
            left join 
            (select t.* from `Transaction` t
                inner join (SELECT o.TicketNumber, min(t.ID) TransactionId FROM `order` o
                left join `Transaction` t on t.TicketNumber = o.TicketNumber
                  where Date(o.DateCompleted) >= ?DateStart and Date(o.DateCompleted) <= ?DateEnd
                group by o.TicketNumber) FirstTransactionId on FirstTransactionId.TransactionId = t.ID) 
        t on t.DigiumLogItemId = d.ID
            left join `Order` o on o.TicketNumber = t.TicketNumber  and DateCompleted >= ?DateStart and DateCompleted <= ?DateEnd
                and o.OrderSourceId = os.ID and d.CallSourceId = o.OrderSourceId
            left join `order` o2 on o2.TicketNumber = o.TicketNumber and o2.CompletionType = 3
            left join `order` o3 on o3.TicketNumber = o.TicketNumber and o3.CompletionType = 2 and o3.TransactionStatus = 5
            left join `order` o4 on o4.TicketNumber = o.TicketNumber and o4.CompletionType = 2 and o4.TransactionStatus > 5
        group by d.CallSourceId, TrackingNumber) TblComplete  on TblComplete.CallSourceId = TblCallCount.CallSourceId and TblComplete.TrackingNumber = TblCallCount.TrackingNumber)
union
(select TblAdvBook.CallSourceId, TblAdvBook.Name, ads.Name as TrackingNumber, TblAdvBook.CallCount,
TblAdvBook.BookCount, 0 as OtherActions, 0 as NoActionCount, TblAdvComplete.CancelCount,
TblAdvComplete.InProcessCount, TblAdvComplete.CompleteCount, TblAdvComplete.CompleteAmount, TblAdvBook.AdSourceId, TblAdvBook.TrackingUrl from
(SELECT os.ID as CallSourceId, os.Name as Name, ads.ID as AdSourceId, count(d.ID) CallCount, count(t.ID) as BookCount, ads.TrackingUrl as TrackingUrl  FROM `order` o
inner join AdvertisingSource ads on ads.ID = o.AdvertisingSourceId
inner join OrderSourceAdvertisingSource osas on osas.AdvertisingSourceId = ads.ID
inner join OrderSource os on os.ID = osas.OrderSourceId
left join `Transaction` t on t.TicketNumber = o.TicketNumber and (t.TransactionCode = 'BOOK' or t.TransactionCode = 'ESTM' )
left join DigiumLogItem d on d.ID = t.DigiumLogItemId
where Date(t.TimeCreated) >= ?DateStart and Date(t.TimeCreated) <= ?DateEnd and o.OrderSourceId is null
group by os.ID, ads.ID) TblAdvBook
inner join
(SELECT os.ID, ads.ID as AdSourceId, count(o2.TicketNumber) as CancelCount, count(o3.TicketNumber) as InProcessCount,
count(o4.TicketNumber) as CompleteCount, sum(o4.Amount) as CompleteAmount FROM `order` o
inner join AdvertisingSource ads on ads.ID = o.AdvertisingSourceId
inner join OrderSourceAdvertisingSource osas on osas.AdvertisingSourceId = ads.ID
inner join OrderSource os on os.ID = osas.OrderSourceId
left join `order` o2 on o2.TicketNumber = o.TicketNumber and o2.CompletionType = 3 and Date(o2.DateCompleted) >= ?DateStart and Date(o2.DateCompleted) <= ?DateEnd
left join `order` o3 on o3.TicketNumber = o.TicketNumber and o3.CompletionType = 2 and o3.TransactionStatus = 5  and Date(o3.DateCompleted) >= ?DateStart and Date(o3.DateCompleted) <= ?DateEnd
left join `order` o4 on o4.TicketNumber = o.TicketNumber and o4.CompletionType = 2 and o4.TransactionStatus > 5  and Date(o4.DateCompleted) >= ?DateStart and Date(o4.DateCompleted) <= ?DateEnd
where o.OrderSourceId is null
group by os.ID, ads.ID) TblAdvComplete on TblAdvComplete.AdSourceId = TblAdvBook.AdSourceId
inner join AdvertisingSource ads on ads.ID = TblAdvBook.AdSourceId
)) TblResult
where 1=1 {0}
order by Name, AdSourceId, TrackingNumber";

        public static List<PartnerSummaryItem> Find(DateTime? dateStart, DateTime? dateEnd, int? callSourceId)
        {
            string query = string.Format(SqlFind,
                !callSourceId.HasValue ? string.Empty : " and TblResult.CallSourceId = ?CallSourceId");

            List<PartnerSummaryItem> result = new List<PartnerSummaryItem>();

            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();

                using (IDbCommand dbCommand = Database.PrepareCommand(query, connection))
                {
                    Database.PutParameter(dbCommand, "?DateStart", dateStart.HasValue ? dateStart.Value.Date : new DateTime(1900, 1, 1));
                    Database.PutParameter(dbCommand, "?DateEnd", dateEnd.HasValue ? dateEnd.Value.Date : DateTime.MaxValue);
                    if (callSourceId.HasValue)
                        Database.PutParameter(dbCommand, "?CallSourceId", callSourceId.Value);

                    using (IDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                            result.Add(Load(dataReader));
                    }
                }
            }

            if (result.Count == 0)
                return result;

            PartnerSummaryItem currentItem = result[0];
            currentItem.PartnerRecordsCount = 1;
            foreach (PartnerSummaryItem item in result)
            {
                if (item.CallSourceId == currentItem.CallSourceId)
                    currentItem.PartnerRecordsCount++;
                else
                {
                    currentItem = item;
                    currentItem.PartnerRecordsCount = 2;
                }
            }

            //Calculating summaries
            int currentCallSourceId = result[0].CallSourceId;
            PartnerSummaryItem currentPartnerSummaryRow = new PartnerSummaryItem();
            currentPartnerSummaryRow.IsPartnerSummaryRow = true;
            PartnerSummaryItem totalSummaryRow = new PartnerSummaryItem();
            totalSummaryRow.CallSourceName = "Summary";
            totalSummaryRow.IsTotalSummaryRow = true;
            totalSummaryRow.PartnerRecordsCount = 1;
            int i = 0;
            while (true)
            {
                if (i == result.Count || result[i].CallSourceId != currentCallSourceId)
                {
                    result.Insert(i, currentPartnerSummaryRow);
                    totalSummaryRow.CallCount += currentPartnerSummaryRow.CallCount;
                    totalSummaryRow.BookCount += currentPartnerSummaryRow.BookCount;
                    totalSummaryRow.CancelCount += currentPartnerSummaryRow.CancelCount;
                    totalSummaryRow.OtherActionsCount += currentPartnerSummaryRow.OtherActionsCount;
                    totalSummaryRow.ShopperCount += currentPartnerSummaryRow.ShopperCount;
                    totalSummaryRow.InProcessCount += currentPartnerSummaryRow.InProcessCount;
                    totalSummaryRow.CompletedCount += currentPartnerSummaryRow.CompletedCount;
                    totalSummaryRow.ClosedAmount += currentPartnerSummaryRow.ClosedAmount;
                    i++;

                    if (i == result.Count)
                        break;

                    currentCallSourceId = result[i].CallSourceId;
                    currentPartnerSummaryRow = new PartnerSummaryItem();
                    currentPartnerSummaryRow.IsPartnerSummaryRow = true;
                }

                currentPartnerSummaryRow.CallCount += result[i].CallCount;
                currentPartnerSummaryRow.BookCount += result[i].BookCount;
                currentPartnerSummaryRow.CancelCount += result[i].CancelCount;
                currentPartnerSummaryRow.OtherActionsCount += result[i].OtherActionsCount;
                currentPartnerSummaryRow.ShopperCount += result[i].ShopperCount;
                currentPartnerSummaryRow.InProcessCount += result[i].InProcessCount;
                currentPartnerSummaryRow.CompletedCount += result[i].CompletedCount;
                currentPartnerSummaryRow.ClosedAmount += result[i].ClosedAmount;
                i++;
            }

            result.Add(totalSummaryRow);

            return result;
        }

        private static PartnerSummaryItem Load(IDataReader dataReader)
        {
            return new PartnerSummaryItem(
                dataReader.GetInt32(0),
                dataReader.GetString(1),
                dataReader.GetString(2),
                dataReader.IsDBNull(3) ? 0 : dataReader.GetInt32(3),
                dataReader.IsDBNull(4) ? 0 : dataReader.GetInt32(4),
                dataReader.IsDBNull(5) ? 0 : dataReader.GetInt32(5),
                dataReader.IsDBNull(6) ? 0 : dataReader.GetInt32(6),
                dataReader.IsDBNull(7) ? 0 : dataReader.GetInt32(7),
                dataReader.IsDBNull(8) ? 0 : dataReader.GetInt32(8),
                dataReader.IsDBNull(9) ? 0 : dataReader.GetInt32(9),
                dataReader.IsDBNull(10) ? 0 : dataReader.GetDecimal(10),
                dataReader.IsDBNull(11) ? (int?)null : dataReader.GetInt32(11),
                dataReader.IsDBNull(12) ? string.Empty : dataReader.GetString(12));
        }

        #endregion
    }
}
      