using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Configuration = Dalworth.Server.SDK.Configuration;

namespace Dalworth.Server.Web.Partner.Models
{
    public enum CallLogReportFilterEnum
    {
        Book = 1,
        OtherActions = 2,
        Shopper = 3,
        Cancel = 4,
        InProcess = 5,
        AdSourceCalls = 6,
        AsSourceBook = 7
    }

    public enum CallLogCompleteReportFilterEnum
    {
        Cancel = 1,
        InProcess = 2,
        Complete = 3
    }

    public class CallLogItem
    {
        static CallLogItem(){}

        public CallLogItem(){}

        public CallLogItem(DateTime? callDate, string callerIdName, string customerName, string callerIdNumber,
            string incomingDid, string callSource, string action, string ticketNumber, int ticketStatus,
            decimal closedAmount, int durationSec, string wavFileUrl, Int64? id, string userName, 
            string extension, bool? isIncoming, int compType, int? matchCriteria, bool isIntermediateCall)
        {
            CallDate = callDate;
            CallerIdName = callerIdName;
            CustomerName = customerName;
            CallerIdNumber = callerIdNumber;
            IncomingDid = incomingDid;
            CallSource = callSource;
            Action = action;
            TicketNumber = ticketNumber;
            TicketStatus = ticketStatus;
            ClosedAmount = closedAmount;
            DurationSec = durationSec;
            WavFileUrl = wavFileUrl;
            Id = id;
            UserName = userName;
            Extension = extension;
            IsIncoming = isIncoming;
            CompType = compType;
            MatchCriteria = matchCriteria;
            IsIntermediateCall = isIntermediateCall;            
        }

        public CallLogItem(DateTime? callDate, string callerIdName, string action, string ticketNumber,
            decimal closedAmount, int durationSec, string wavFileUrl, Int64? id, int compType, int ticketStatus)
        {
            CallDate = callDate;
            CallerIdName = callerIdName;
            Action = action;
            TicketNumber = ticketNumber;
            ClosedAmount = closedAmount;
            DurationSec = durationSec;
            WavFileUrl = wavFileUrl;
            Id = id;
            CompType = compType;
            TicketStatus = ticketStatus;                        
        }

        public DateTime? CallDate { get; set; }
        public string CallerIdName { get; set; }
        public string CustomerName { get; set; }
        public string CallerIdNumber { get; set; }
        public string IncomingDid { get; set; }
        public string CallSource { get; set; }
        public string Action { get; set; }
        public string TicketNumber { get; set; }
        public int TicketStatus { get; set; }
        public decimal ClosedAmount { get; set; }
        public int DurationSec { get; set; }
        public string WavFileUrl { get; set; }
        public Int64? Id { get; set; }
        public string UserName { get; set; }
        public string Extension { get; set; }
        public bool? IsIncoming { get; set; }
        public int CompType { get; set; }
        public DateTime? BookDate { get; set; }
        public int? MatchCriteria { get; set; }
        public bool IsIntermediateCall { get; set; }

        public int RowSpan { get; set; }
        public bool IsSpanned { get; set; }


        public string UserNameText
        {
            get
            {
                if (string.IsNullOrEmpty(UserName))
                    return Extension;
                return UserName;
            }
        }

        public string CallerNumberText
        {
            get
            {
                try
                {
                    return String.Format("{0:(###) ###-####}", Int64.Parse(CallerIdNumber));
                }
                catch (Exception)
                {
                    return CallerIdNumber;
                }
            }
        }

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
        
        public string DirectionText
        {
            get
            {
                if (!IsIncoming.HasValue)
                    return string.Empty;
                string result = IsIncoming.Value ? "IN" : "OUT";
                if (IsIntermediateCall)
                    result += " TRF";
                return result;
            }
        }            

        public string TicketStatusText
        {
            get
            {
                if (string.IsNullOrEmpty(TicketNumber))
                    return string.Empty;

                if ((CompType == 0 || CompType == 1) && TicketStatus == 1)
                    return "Pending";
                if (CompType == 3)
                    return "Cancelled";
                if (TicketStatus <= 5)
                    return "In process";
                if (TicketStatus > 5)
                    return "Accounting";
                return string.Empty;
            }
        }

        public string ClosedAmountText
        {
            get
            {
                if (TicketStatus == 6 || TicketStatus == 7)
                    return ClosedAmount.ToString("C");
                return string.Empty;
            }
        }        

        public string CallDurationText
        {
            get
            {
                int minutes = DurationSec/60;
                int seconds = DurationSec - minutes*60;

                return string.Format("{0}:{1}", minutes, seconds.ToString("00"));
            }
        }                
        
        public string AmountText
        {
            get
            {
                return ClosedAmount.ToString("C");
            }
        }                  
      
        public string MatchCriteriaText
        {
            get
            {
                Transaction transaction = new Transaction();
                transaction.MatchCriteria = MatchCriteria;
                string result = string.Empty;
                if (transaction.IsCustomerPhoneMatch)
                    result += "PHN,";
                if (transaction.IsCallerIdMatch)
                    result += "CID,";
                if (transaction.IsManualCallerIdMatch)
                    result += "CSR,";
                if (transaction.IsStartedDuringCall)
                    result += "T,";

                if (result.EndsWith(","))
                    result = result.TrimEnd(',');
                return result;
            }
        }

        private static string GetFullWavFilePath(string fileName)
        {
            string fullFileName = string.Empty;
            if (fileName != string.Empty)
                fullFileName = string.Format("{0}{1}/{2}",
                    WebConfigurationManager.AppSettings["VoiceFilesVirtualDirectory"],
                    fileName.Substring(0, 10), fileName);
            return fullFileName;
        }

        #region Find

        private const int ItemsPerPage = 50;

        private const String SqlFind =
        @"select SQL_CALC_FOUND_ROWS * from ((SELECT d.TimeCreated, d.CallerName, MAX(CONCAT_WS(', ', c.LastName, c.FirstName)), d.CallerIdNumber, d.IncomingDid, MAX(os.Name), MIN(t.TransactionCode),
            MAX(t.TicketNumber), MAX(o.TransactionStatus), MAX(o.Amount), d.DurationSec, d.ID, t.UserName, d.VoiceFileName, d.Extension, d.TimeTalkStarted, t.TimeCreated as TransactionTimeCreated, d.IsIncoming, o.CompletionType, MAX(t.MatchCriteria), d.IsIntermediateCall FROM digiumlogitem d
            left join OrderSource os on os.ID = d.CallSourceId
            left join `Transaction` t on t.DigiumLogItemId = d.ID
            left join `order` o on o.TicketNumber = t.TicketNumber
            left join Customer c on c.ID = o.CustomerId
        where t.DigiumLogItemId is not null and d.TimeCreated >= ?DateStart and Date(d.TimeCreated) <= ?DateEnd {0}
        group by t.ServmanWorkflowId)
        union
        (SELECT d.TimeCreated, d.CallerName, '', d.CallerIdNumber, d.IncomingDid, os.Name, null,
            null, null, null, d.DurationSec, d.ID, null, d.VoiceFileName, d.Extension, d.TimeTalkStarted, t.TimeCreated as TransactionTimeCreated, d.IsIncoming, null, null, d.IsIntermediateCall FROM digiumlogitem d
            left join OrderSource os on os.ID = d.CallSourceId
            left join `Transaction` t on t.DigiumLogItemId = d.ID
        where t.DigiumLogItemId is null and d.TimeCreated >= ?DateStart and Date(d.TimeCreated) <= ?DateEnd {1})) querySet
        order by TimeCreated, TimeTalkStarted, TransactionTimeCreated";

        private const String SqlFindAdsource =
        @"select SQL_CALC_FOUND_ROWS * from (SELECT d.TimeCreated, d.CallerName, MAX(CONCAT_WS(', ', c.LastName, c.FirstName)), d.CallerIdNumber, d.IncomingDid, MAX(os.Name), MIN(t.TransactionCode),
          MAX(t.TicketNumber), MAX(o.TransactionStatus), MAX(o.Amount), d.DurationSec, d.ID, t.UserName, d.VoiceFileName, d.Extension, d.TimeTalkStarted, t.TimeCreated as TransactionTimeCreated, d.IsIncoming, o.CompletionType, null, d.IsIntermediateCall FROM `Order` o
        inner join `Transaction` t on t.TicketNumber = o.TicketNumber and (t.TransactionCode = 'BOOK' or t.TransactionCode = 'ESTM')
        left join DigiumLogItem d on d.ID = t.DigiumLogItemId
        left join OrderSourceAdvertisingSource osas on osas.AdvertisingSourceId = o.AdvertisingSourceId
        left join OrderSource os on os.ID = osas.OrderSourceId
        left join Customer c on c.ID = o.CustomerId
        where o.OrderSourceId is null and o.AdvertisingSourceId = ?AdvertisingSourceId  and Date(t.TimeCreated) >= ?DateStart and Date(t.TimeCreated) <= ?DateEnd
                {0}
            group by t.ServmanWorkflowId) querySet
        order by TimeCreated, TimeTalkStarted, TransactionTimeCreated";


        public static List<CallLogItem> Find(int? id, DateTime? dateStart, DateTime? dateEnd, string callerNumber, 
            string incomingDid, string callSource, string ticketNumber, int? callSourceId, int? pageNumber, 
            string userName, string customerName, string extension, DateTime? startTime, bool incomingOnly, bool outgoingOnly, 
            CallLogReportFilterEnum? filter, int? advetrisingSourceId, bool loadAllItems, out int totalItemsCount)
        {
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();

                string linkedTicket = string.Empty;
                if (!string.IsNullOrEmpty(ticketNumber))
                    linkedTicket = Domain.Order.FindLinkedTicket(ticketNumber, connection);

                if (!string.IsNullOrEmpty(callerNumber))
                    callerNumber = string.Join(null, System.Text.RegularExpressions.Regex.Split(callerNumber, "[^\\d]"));
                if (!string.IsNullOrEmpty(incomingDid))
                    incomingDid = string.Join(null, System.Text.RegularExpressions.Regex.Split(incomingDid, "[^\\d]"));

                string[] customerNameParts = new string[0];
                if (!string.IsNullOrEmpty(customerName))
                    customerNameParts = customerName.Split(new string[] { " ", ", ", "," }, StringSplitOptions.RemoveEmptyEntries);

                string reportFilterString = string.Empty;
                string reportFilterString2 = string.Empty;
                if (filter.HasValue)
                {
                    reportFilterString2 = " and 1 = 0 ";

                    if (filter.Value == CallLogReportFilterEnum.Book)
                        reportFilterString = " and (t.TransactionCode = 'BOOK' or t.TransactionCode = 'ESTM') ";
                    else if (filter.Value == CallLogReportFilterEnum.OtherActions)
                    {
                        reportFilterString = " and ((t.TransactionCode is null) or (t.TransactionCode is not null and t.TransactionCode != 'BOOK' and t.TransactionCode != 'SHPR' and t.TransactionCode != 'ESTM')) ";
                        reportFilterString2 = string.Empty;
                    }                        
                    else if (filter.Value == CallLogReportFilterEnum.Shopper)
                        reportFilterString = " and t.TransactionCode = 'SHPR' and t.TicketNumber != '' ";
                    else if (filter.Value == CallLogReportFilterEnum.Cancel)
                        reportFilterString = " and o.CompletionType = 3 and t.TransactionCode = 'BOOK' ";
                    else if (filter.Value == CallLogReportFilterEnum.InProcess)
                        reportFilterString = " and (o.TransactionStatus = 2 or o.TransactionStatus = 3 or o.TransactionStatus = 4) and t.TransactionCode = 'BOOK' ";
                }

                string query = string.Format(SqlFind,
                    (!id.HasValue ? string.Empty : " and d.ID = ?ID")
                    + (string.IsNullOrEmpty(callerNumber) ? string.Empty : " and d.CallerIdNumber = ?CallerIdNumber")
                    + (string.IsNullOrEmpty(incomingDid) ? string.Empty : " and d.IncomingDid = ?IncomingDid")
                    + (string.IsNullOrEmpty(callSource) ? string.Empty : " and os.Name like ?CallSourceName")
                    + (!string.IsNullOrEmpty(linkedTicket) && !string.IsNullOrEmpty(ticketNumber) ? " and (t.TicketNumber = ?TicketNumber or t.TicketNumber = ?LinkedTicketNumber)" : string.Empty)
                    + (string.IsNullOrEmpty(linkedTicket) && !string.IsNullOrEmpty(ticketNumber) ? " and t.TicketNumber = ?TicketNumber" : string.Empty)                    
                    + (!callSourceId.HasValue ? string.Empty : " and d.CallSourceId = ?CallSourceId")
                    + (string.IsNullOrEmpty(userName) ? string.Empty : " and t.UserName like ?UserName")
                    + (customerNameParts.Length >= 1 ? " and (c.FirstName like ?NamePart1 or c.LastName like ?NamePart1)" : string.Empty)
                    + (customerNameParts.Length >= 2 ? " and (c.FirstName like ?NamePart2 or c.LastName like ?NamePart2)" : string.Empty)
                    + (string.IsNullOrEmpty(extension) ? string.Empty : " and d.Extension = ?Extension")
                    + (incomingOnly ? " and d.IsIncoming = 1 " : string.Empty)
                    + (outgoingOnly ? " and d.IsIncoming = 0 " : string.Empty)
                    + reportFilterString,

                    (!string.IsNullOrEmpty(ticketNumber) || !string.IsNullOrEmpty(userName)
                    || !string.IsNullOrEmpty(customerName)) ? " and 0 = 1 " :

                    (!id.HasValue ? string.Empty : " and d.ID = ?ID")
                    + (string.IsNullOrEmpty(callerNumber) ? string.Empty : " and d.CallerIdNumber = ?CallerIdNumber")
                    + (string.IsNullOrEmpty(incomingDid) ? string.Empty : " and d.IncomingDid = ?IncomingDid")
                    + (string.IsNullOrEmpty(callSource) ? string.Empty : " and os.Name like ?CallSourceName")
                    + (!callSourceId.HasValue ? string.Empty : " and d.CallSourceId = ?CallSourceId")
                    + (string.IsNullOrEmpty(extension) ? string.Empty : " and d.Extension = ?Extension")
                    + (incomingOnly ? " and d.IsIncoming = 1 " : string.Empty)
                    + (outgoingOnly? " and d.IsIncoming = 0 " : string.Empty)
                    + reportFilterString2);

                if (filter == CallLogReportFilterEnum.AdSourceCalls || filter == CallLogReportFilterEnum.AsSourceBook)
                {
                    if (filter == CallLogReportFilterEnum.AdSourceCalls)
                        query = string.Format(SqlFindAdsource, "and d.ID is not null");
                    else
                        query = string.Format(SqlFindAdsource, "and 1=1");
                }
                    

                if (pageNumber.HasValue && !loadAllItems)
                    query = query + string.Format(" limit {0}, {1}", pageNumber * ItemsPerPage, ItemsPerPage);

                List<CallLogItem> result = new List<CallLogItem>();

                using (IDbCommand dbCommand = Database.PrepareCommand(query, connection))
                {
                    PutDbParameters(dbCommand, id, dateStart, dateEnd, callerNumber, incomingDid,
                        callSource, ticketNumber, linkedTicket, callSourceId, userName, customerNameParts,
                        extension, startTime, advetrisingSourceId);

                    using (IDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            CallLogItem item = Load(dataReader);
                            item.WavFileUrl = GetFullWavFilePath(item.WavFileUrl);
                            result.Add(item);
                        }
                            
                    }
                }

                totalItemsCount = 0;
                if (pageNumber.HasValue)
                {
                    using (IDbCommand dbCommand = Database.PrepareCommand("SELECT FOUND_ROWS()", connection))
                    {
                        using (IDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            if (dataReader.Read())
                                totalItemsCount = dataReader.GetInt32(0);
                        }
                    }
                }

                return result;
            }            
        }

        private static void PutDbParameters(IDbCommand dbCommand, int? id, DateTime? dateStart, DateTime? dateEnd, string callerNumber,
            string incomingDid, string callSource, string ticketNumber, string linkedTicket, int? callSourceId, string userName,
            string[] customerNameParts, string extension, DateTime? timeStart, int? advetrisingSourceId)
        {
            DateTime startDate;
            if (dateStart.HasValue)
                startDate = dateStart.Value.Date;
            else
                startDate = new DateTime(1900, 1, 1);

            if (timeStart.HasValue)
                startDate = startDate.Add(timeStart.Value.TimeOfDay);

            Database.PutParameter(dbCommand, "?DateStart", startDate);
            Database.PutParameter(dbCommand, "?DateEnd", dateEnd.HasValue ? dateEnd.Value.Date : DateTime.MaxValue);

            if (id.HasValue)
                Database.PutParameter(dbCommand, "?ID", id.Value);
            if (!string.IsNullOrEmpty(callerNumber))
                Database.PutParameter(dbCommand, "?CallerIdNumber", callerNumber);
            if (!string.IsNullOrEmpty(incomingDid))
                Database.PutParameter(dbCommand, "?IncomingDid", incomingDid);
            if (!string.IsNullOrEmpty(callSource))
                Database.PutParameter(dbCommand, "?CallSourceName", callSource + "%");
            if (!string.IsNullOrEmpty(ticketNumber))
                Database.PutParameter(dbCommand, "?TicketNumber", ticketNumber);
            if (!string.IsNullOrEmpty(linkedTicket))
                Database.PutParameter(dbCommand, "?LinkedTicketNumber", linkedTicket);
            if (callSourceId.HasValue)
                Database.PutParameter(dbCommand, "?CallSourceId", callSourceId.Value);
            if (!string.IsNullOrEmpty(userName))
                Database.PutParameter(dbCommand, "?UserName", string.Format("%{0}%", userName));
            if (customerNameParts.Length >= 1)
                Database.PutParameter(dbCommand, "?NamePart1", string.Format("{0}%", customerNameParts[0]));
            if (customerNameParts.Length >= 2)
                Database.PutParameter(dbCommand, "?NamePart2", string.Format("{0}%", customerNameParts[1]));
            if (!string.IsNullOrEmpty(extension))
                Database.PutParameter(dbCommand, "?Extension", extension);
            if (advetrisingSourceId.HasValue)
                Database.PutParameter(dbCommand, "?AdvertisingSourceId", advetrisingSourceId.Value);
        }

        #endregion

        #region FindCompleted

        private const String SqlFindcompleted =
        @"select SQL_CALC_FOUND_ROWS * from (select d.TimeCreated, d.CallerName, MIN(t.TransactionCode), MAX(t.TicketNumber) as Ticket,
            MAX(o.Amount), d.DurationSec, d.VoiceFileName, d.ID, o.CompletionType, o.TransactionStatus from `Transaction` t
            inner join 
            (select t.TicketNumber from `Transaction` t
                inner join (SELECT o.TicketNumber, min(t.ID) TransactionId FROM `order` o
                left join `Transaction` t on t.TicketNumber = o.TicketNumber
                 where Date(o.DateCompleted) >= ?DateStart and Date(o.DateCompleted) <= ?DateEnd
                   and o.OrderSourceId = ?PartnerId {0}
                group by o.TicketNumber) FirstTransaction on FirstTransaction.TransactionId = t.ID
                inner join DigiumLogItem d on d.ID = t.DigiumLogItemId
                where d.IncomingDid = ?TrackingPhone1 or d.CallerIdNumber = ?TrackingPhone2) ClosedTickets on t.TicketNumber = ClosedTickets.TicketNumber
            inner join DigiumLogItem d on d.ID = t.DigiumLogItemId
            inner join `Order` o on o.TicketNumber = t.TicketNumber
            group by t.ServmanWorkflowId  order by Ticket, d.TimeCreated) tt";

        private const String SqlFindCompletedAdSource =
        @"select SQL_CALC_FOUND_ROWS * from (select t.TimeCreated, d.CallerName, MIN(t.TransactionCode), MAX(t.TicketNumber) as Ticket,
            MAX(o.Amount), d.DurationSec, d.VoiceFileName, d.ID, o.CompletionType, o.TransactionStatus from `Transaction` t
            inner join 
            (SELECT o.TicketNumber FROM `order` o
            where Date(o.DateCompleted) >= ?DateStart and Date(o.DateCompleted) <= ?DateEnd
            and o.OrderSourceId is null and o.AdvertisingSourceId = ?AdvertisingSourceId
                {0}
            ) ClosedTickets on ClosedTickets.TicketNumber = t.TicketNumber
            inner join `Order` o on o.TicketNumber = ClosedTickets.TicketNumber            
            left join DigiumLogItem d on d.ID = t.DigiumLogItemId            
            group by t.ServmanWorkflowId  order by Ticket, t.TimeCreated) tt";

        public static List<CallLogItem> FindCompleted(int? pageNumber, DateTime start, DateTime end,
            string trackingNumber, int partnerId, int? advertisingSourceId, CallLogCompleteReportFilterEnum? filter, 
            out int totalItemsCount)
        {
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();

                string query = advertisingSourceId.HasValue ? SqlFindCompletedAdSource : SqlFindcompleted;
                if (filter.HasValue)
                {
                    if (filter.Value == CallLogCompleteReportFilterEnum.Cancel)
                        query = string.Format(query, " and o.CompletionType = 3 ");
                    else if (filter.Value == CallLogCompleteReportFilterEnum.InProcess)
                        query = string.Format(query, " and o.CompletionType = 2 and o.TransactionStatus = 5 ");
                    else if (filter.Value == CallLogCompleteReportFilterEnum.Complete)
                        query = string.Format(query, " and o.CompletionType = 2 and o.TransactionStatus > 5 ");
                }

                if (pageNumber.HasValue)
                    query = query + string.Format(" limit {0}, {1}", pageNumber * ItemsPerPage, ItemsPerPage);

                List<CallLogItem> result = new List<CallLogItem>();

                using (IDbCommand dbCommand = Database.PrepareCommand(query, connection))
                {
                    Database.PutParameter(dbCommand, "?DateStart", start.Date);
                    Database.PutParameter(dbCommand, "?DateEnd", end.Date);
                    if (advertisingSourceId.HasValue)
                    {
                        Database.PutParameter(dbCommand, "?AdvertisingSourceId", advertisingSourceId.Value);
                    } else
                    {
                        Database.PutParameter(dbCommand, "?PartnerId", partnerId);
                        Database.PutParameter(dbCommand, "?TrackingPhone1", trackingNumber);
                        Database.PutParameter(dbCommand, "?TrackingPhone2", trackingNumber);                        
                    }

                    using (IDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            CallLogItem item = new CallLogItem(
                                dataReader.IsDBNull(0) ? (DateTime?)null : dataReader.GetDateTime(0),
                                dataReader.IsDBNull(1) ? string.Empty : dataReader.GetString(1),
                                dataReader.IsDBNull(2) ? string.Empty : dataReader.GetString(2),
                                dataReader.IsDBNull(3) ? string.Empty : dataReader.GetString(3),
                                dataReader.IsDBNull(4) ? 0 : dataReader.GetDecimal(4),
                                dataReader.IsDBNull(5) ? 0 : dataReader.GetInt32(5),
                                dataReader.IsDBNull(6) ? string.Empty : dataReader.GetString(6),
                                dataReader.IsDBNull(7) ? (Int64?)null : dataReader.GetInt32(7),
                                dataReader.IsDBNull(8) ? -1 : dataReader.GetInt32(8),
                                dataReader.IsDBNull(9) ? -1 : dataReader.GetInt32(9));
                            item.WavFileUrl = GetFullWavFilePath(item.WavFileUrl);
                            result.Add(item);
                        }
                    }
                }

                totalItemsCount = 0;
                if (pageNumber.HasValue)
                {
                    using (IDbCommand dbCommand = Database.PrepareCommand("SELECT FOUND_ROWS()", connection))
                    {
                        using (IDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            if (dataReader.Read())
                                totalItemsCount = dataReader.GetInt32(0);
                        }
                    }
                }


                foreach (CallLogItem item in result)
                {
                    if (item.BookDate == null)
                    {
                        item.RowSpan = 0;
                        int i = result.IndexOf(item);
                        while (i < result.Count && result[i].TicketNumber == item.TicketNumber)
                        {
                            result[i].BookDate = item.CallDate;
                            item.RowSpan++;
                            if (item.RowSpan > 1)
                                result[i].IsSpanned = true;
                            i++;
                        }
                    }
                }

                return result;
            }
        }

        #endregion

        #region Load

        private static CallLogItem Load(IDataReader dataReader)
        {          
            return new CallLogItem(
                dataReader.IsDBNull(0) ? (DateTime?)null : dataReader.GetDateTime(0),
                dataReader.IsDBNull(1) ? string.Empty : dataReader.GetString(1),
                dataReader.GetString(2),
                dataReader.IsDBNull(3) ? string.Empty : dataReader.GetString(3),
                dataReader.IsDBNull(4) ? string.Empty : dataReader.GetString(4),
                dataReader.IsDBNull(5) ? string.Empty : dataReader.GetString(5),
                dataReader.IsDBNull(6) ? string.Empty : dataReader.GetString(6),
                dataReader.IsDBNull(7) ? string.Empty : dataReader.GetString(7),
                dataReader.IsDBNull(8) ? 0 : dataReader.GetInt32(8),
                dataReader.IsDBNull(9) ? 0 : dataReader.GetDecimal(9),
                dataReader.IsDBNull(10) ? 0 : dataReader.GetInt32(10),
                dataReader.IsDBNull(13) ? string.Empty : dataReader.GetString(13),
                dataReader.IsDBNull(11) ? (Int64?)null : dataReader.GetInt64(11),
                dataReader.IsDBNull(12) ? string.Empty : dataReader.GetString(12),
                dataReader.IsDBNull(14) ? string.Empty : dataReader.GetString(14),
                dataReader.IsDBNull(17) ? (bool?)null : dataReader.GetBoolean(17),
                dataReader.IsDBNull(18) ? -1 : dataReader.GetInt32(18),
                dataReader.IsDBNull(19) ? (int?)null : dataReader.GetInt32(19),
                dataReader.IsDBNull(20) ? false : dataReader.GetBoolean(20));
        }

        #endregion        
    }
}