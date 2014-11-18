using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Domain
{
    public partial class Transaction
    {
        public Transaction(){ }

        #region Import

        public static void Import(DateTime? fromDate = null)
        {
            DateTime startDate;
            if (!fromDate.HasValue)
            {
                Transaction lastImportedTransaction = FindLastImportedTransaction();
                if (lastImportedTransaction != null)
                    startDate = lastImportedTransaction.TimeCreated.AddMinutes(
                        -Configuration.DigiumTransactionImportDelayMin);
                else
                    startDate = DateTime.Now.AddMinutes(-Configuration.DigiumTransactionImportDelayMin);
            }
            else
                startDate = fromDate.Value;


            List<Transaction> currentTransactions;
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection(ConnectionKeyEnum.Servman))
            {
                connection.Open();
                currentTransactions = FindServmanTransactions(startDate, connection);
            }

            Host.Trace("Digium", " Import New Transactions Count:" + currentTransactions.Count + " fromDate :" + startDate);

            int i = 0;
            foreach (var currentTransaction in currentTransactions)
            {
                i++;
                try
                {
                    if (!string.IsNullOrEmpty(currentTransaction.TicketNumber))
                        Order.InsertUpdateOrder(currentTransaction.TicketNumber);

                    try
                    {
                        var transactionInDb = FindByServmanTransactionId(currentTransaction.ServmanTransactionId, null);
                        currentTransaction.ID = transactionInDb.ID;
                        currentTransaction.MatchCriteria = transactionInDb.MatchCriteria;
                        currentTransaction.DigiumLogItemId = transactionInDb.DigiumLogItemId; 
                        Update(currentTransaction);
                    }
                    catch (DataNotFoundException)
                    {
                        Insert(currentTransaction);     
                    }
                    
                    Host.Trace("Digium", "Imported transaction ServmanTransactionId: " + currentTransaction.ServmanTransactionId + "-------" + i + "/" + currentTransactions.Count + "------------",
                        HostTraceLevelEnum.Debug);
                }
                catch (Exception ex)
                {
                    if (!fromDate.HasValue)
                        throw;
                    Host.Trace("Digium", "Failed to import transaction ID:" + currentTransaction.ID + " exception: " + ex);
                    Host.SendErrorEmail("Failed to import transaction ID:" + currentTransaction.ID + " exception: " + ex);
                }
            }
        }

        #endregion

        #region FindServmanTransactions

        private const String SqlFindServmanTransactions =
            @"Select * from [transact]
                where Date > ? or (Date = ? and Time > ?)
              order by Trans_id";

        private static List<Transaction> FindServmanTransactions(DateTime fromDate, IDbConnection connection)
        {
            List<Transaction> transactions = new List<Transaction>();
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindServmanTransactions, connection))
            {
                Database.PutParameter(dbCommand, "@Date", fromDate.Date);
                Database.PutParameter(dbCommand, "@Date", fromDate.Date);
                Database.PutParameter(dbCommand, "@Time", fromDate.ToString("HH:MM:ss"));

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        transactions.Add(LoadServmanTransaction(dataReader));
                }
            }

            return transactions;
        }

        #endregion        

        #region LoadServmanTransaction

        private static Transaction LoadServmanTransaction(IDataReader dataReader)
        {
            string servmanCustId = dataReader.GetString(1);
            int? customerId = null;
            if (servmanCustId.Trim() != string.Empty)
            {
                try
                {
                    customerId = Customer.FindBy(servmanCustId.Trim()).ID;
                }
                catch (DataNotFoundException){}
            }

            DateTime dateTime = dataReader.GetDateTime(8);
            TimeSpan timeSpan = TimeSpan.Parse(dataReader.GetString(9));

            var ticketNumber = dataReader.IsDBNull(0) ? null : dataReader.GetString(0).Trim();
            if (ticketNumber == string.Empty)
                ticketNumber = null;

            var transaction = new Transaction(iD:0,
                servmanTransactionId:dataReader.GetString(10),
                servmanWorkflowId:dataReader.GetString(11),
                manualRecordedCallerId:dataReader.GetString(12),
                ticketNumber: ticketNumber,
                customerId:customerId,
                customerName:dataReader.GetString(4),
                serviceType:(int)dataReader.GetDecimal(2),
                transactionCode:dataReader.GetString(3),
                userName:dataReader.GetString(5),
                extension:dataReader.GetString(6),                
                timeCreated:dateTime.Add(timeSpan), 
                digiumLogItemId:null, 
                matchCriteria:null);

            return transaction;
        }

        #endregion

        #region FindLastImportedTransaction

        private const String SqlFindLastImportedTransaction =
            @"SELECT * FROM Transaction
                order by ID desc
                limit 1";

        private static Transaction FindLastImportedTransaction()
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindLastImportedTransaction))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        #endregion

        #region FindByServmanTransactionId

        private static string SqlFindByServmanTransactionId =
            SqlSelectAll + " where servmantransactionid = ?ServmanTransactionId";

        public static Transaction FindByServmanTransactionId(string servmanTransactionId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByServmanTransactionId, connection))
            {
                Database.PutParameter(dbCommand, "?ServmanTransactionId", servmanTransactionId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }
            throw new DataNotFoundException("Transaction not found, search by primary key");
        }

        #endregion

        #region FindLastAssignedOrderSourceToOrder

        private const String SqlFindLastAssignedOrderSourceToOrder =
            @"SELECT max(ServmanTransactionId) FROM `Transaction` t
                inner join `Order` o on o.TicketNumber = t.TicketNumber
                where t.TicketNumber <> '' and o.OrderSourceId is not null";

        public static string FindLastAssignedOrderSourceToOrder()
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindLastAssignedOrderSourceToOrder))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return dataReader.GetString(0);                        
                }
            }

            return string.Empty;
        }

        #endregion

        #region FindByExtension

        private const String SqlFindByExtension =
            @"SELECT * FROM Transaction
                where TimeCreated > ?TimeCreatedStart and TimeCreated < ?TimeCreatedEnd
                    and Extension = ?Extension
               order by TimeCreated";

        public static List<Transaction> FindByExtension(string extension, DateTime timeRangeStart, DateTime timeRangeEnd)
        {
            List<Transaction> result = new List<Transaction>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByExtension))
            {
                Database.PutParameter(dbCommand, "?TimeCreatedStart", timeRangeStart);
                Database.PutParameter(dbCommand, "?TimeCreatedEnd", timeRangeEnd);
                Database.PutParameter(dbCommand, "?Extension", extension);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));                                                
                }
            }

            return result;
        }

        #endregion

        #region FindByWorkflow

        private const String SqlFindByWorkflow =
            @"SELECT * FROM Transaction
                where ServmanWorkflowId = ?ServmanWorkflowId
              order by TimeCreated";

        public static List<Transaction> FindByWorkflow(string servmanWorkflowId)
        {
            List<Transaction> result = new List<Transaction>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWorkflow))
            {
                Database.PutParameter(dbCommand, "?ServmanWorkflowId", servmanWorkflowId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion

        #region FindLatestServmanWorkflowIds

        private const String SqlFindLatestServmanWorkflowIds =
           @"select servmanWorkFlowId 
             from transaction t
             where TimeCreated > ?TimeCreated 
             group by servmanWorkFlowId
            ";

        public static List<string> FindLatestServmanWorkflowIds(DateTime timeStart)
        {
            var result = new List<string>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindLatestServmanWorkflowIds))
            {
                Database.PutParameter(dbCommand, "?TimeCreated", timeStart);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(dataReader.GetString(0));
                }
            }

            return result;
        }

        #endregion

        #region FindLatestTransactions

        private const String SqlFindLatestTransactions2 =
            @"SELECT * FROM `Transaction` t
                inner join DigiumLogItem d on d.ID = t.DigiumLogItemId
            where t.TicketNumber <> '' and t.DigiumLogItemId is not null and d.CallSourceId is not null
                and t.ServmanTransactionId > ?ServmanTransactionId and t.TimeCreated < ?TimeCreated
            order by ServmanTransactionId";

        public static List<Transaction> FindLatestTransactions(string minServmanTransactionId, DateTime timeEnd)
        {
            List<Transaction> result = new List<Transaction>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindLatestTransactions2))
            {
                Database.PutParameter(dbCommand, "?ServmanTransactionId", minServmanTransactionId);
                Database.PutParameter(dbCommand, "?TimeCreated", timeEnd);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion

        #region IsFirstTransactionForTicket

        private const String SqlIsFirstTransactionForTicket =
            @"SELECT * FROM `Transaction`
                where TicketNumber = ?TicketNumber and ServmanTransactionId < ?ServmanTransactionId
                and ServmanWorkflowId <> ?ServmanWorkflowId
            limit 1";

        public static bool IsFirstTransactionForTicket(Transaction transaction)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlIsFirstTransactionForTicket))
            {
                Database.PutParameter(dbCommand, "?TicketNumber", transaction.TicketNumber);
                Database.PutParameter(dbCommand, "?ServmanTransactionId", transaction.ServmanTransactionId);
                Database.PutParameter(dbCommand, "?ServmanWorkflowId", transaction.ServmanWorkflowId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    return !dataReader.Read();
                }
            }
        }

        #endregion

        #region GetTimeDistance

        public double GetTimeDistance(DateTime time)
        {
            return Math.Abs(TimeCreated.Subtract(time).TotalSeconds);
        }

        #endregion

        #region FindMatchedCall

        public static DigiumLogItem FindMatchedCall(List<Transaction> transactionGroup)
        {
            var possibleCalls = FindPossibleCalls(transactionGroup);

            Host.Trace("Digium", "FindMatchedCall possible calls count: " + possibleCalls.Count, HostTraceLevelEnum.Debug);
            if (possibleCalls.Count == 0)
                return null;

            var customer = FindCustomer(transactionGroup);

            MatchOutgoingCalls(possibleCalls.FindAll(call => !call.IsIncoming), customer, transactionGroup);
            MatchIncomingCalls(possibleCalls.FindAll(call => call.IsIncoming), customer, transactionGroup);
            
            possibleCalls.Sort(delegate(DigiumLogItem item1, DigiumLogItem item2)
                                   {
                                       if (item1.MatchRank == item2.MatchRank)
                                           return
                                               transactionGroup[0].GetTimeDistance(item1.TimeTalkStarted).CompareTo(
                                                   transactionGroup[0].GetTimeDistance(item2.TimeTalkStarted));
                                       return item2.MatchRank.CompareTo(item1.MatchRank);
                                   });

            return possibleCalls.Count > 0 && possibleCalls[0].MatchRank > 0 ? possibleCalls[0] : null;
        }

        private static Customer FindCustomer (List<Transaction> transactionGroup)
        {
            Customer customer = null;
            var transactionWithCustomer = transactionGroup.Find(transaction => transaction.CustomerId.HasValue);
            if (transactionWithCustomer != null)
                customer = Customer.FindByPrimaryKey(transactionWithCustomer.CustomerId.Value);

            return customer;
        }

        private static void MatchIncomingCalls (List<DigiumLogItem> possibleIncomingCalls, Customer customer, List<Transaction> transactionGroup)
        {
            foreach (var call in possibleIncomingCalls)
            {
                Host.Trace("Digium", "MatchIncomingCalls MATCH call ID:" + call.ID + " call.TimeTalkStarted " + call.TimeTalkStarted + " call.CallerIdNumber: " + call.CallerIdNumber + " customer.Phone1 " + (customer !=null && customer.Phone1 != null? customer.Phone1:string.Empty) + "customer.Phone2" + (customer !=null && customer.Phone2!=null? customer.Phone2:string.Empty) + " call.CallerName" + call.CallerName, 
                    HostTraceLevelEnum.Debug);
                SetMatchBasedOnCallerId(call, customer);
                SetIsManualCallerIdMatch(call, transactionGroup);
                SetIsStartedDuringCall(call, transactionGroup);
            }    
        }

        private static void MatchOutgoingCalls(List<DigiumLogItem> possibleOutgoingCalls, Customer customer, List<Transaction> transactionGroup)
        {
            foreach (var outgoingCall in possibleOutgoingCalls)
            {
                outgoingCall.IsCustomerPhoneMatch = false;

                if (customer != null && transactionGroup[0].TimeCreated > outgoingCall.TimeTalkStarted.AddMinutes(-10)
                    && transactionGroup[0].TimeCreated < outgoingCall.TimeTalkStarted.AddMinutes(10)
                    && (outgoingCall.CallerIdNumber == customer.Phone1 || outgoingCall.CallerIdNumber == customer.Phone2))
                    {
                        
                        outgoingCall.IsCustomerPhoneMatch = true;
                        Host.Trace("Digium", "MatchOutgoingCalls MATCH call ID:" + outgoingCall.ID + " transactionGroup[0].TimeCreated" + transactionGroup[0].TimeCreated + " outgoingCall.TimeTalkStarted " + outgoingCall.TimeTalkStarted + " outgoingCall.CallerIdNumber: " + outgoingCall.CallerIdNumber + " customer.Phone1 " + customer.Phone1 + "customer.Phone2" + customer.Phone2, 
                            HostTraceLevelEnum.Debug);    
                    }
                else
                {
                    Host.Trace("Digium", "MatchOutgoingCalls NO MATCH call ID:" + outgoingCall.ID + " transactionGroup[0].TimeCreated" + transactionGroup[0].TimeCreated + " outgoingCall.TimeTalkStarted " + outgoingCall.TimeTalkStarted + " outgoingCall.CallerIdNumber: " + outgoingCall.CallerIdNumber + " customer.Phone1 " + (customer != null ? customer.Phone1 : string.Empty) + "customer.Phone2" + ( customer != null ? customer.Phone2 : string.Empty), 
                        HostTraceLevelEnum.Debug);    
                }
            }
        }

        private static List<DigiumLogItem> FindPossibleCalls (List<Transaction> transactionGroup)
        {
            var possibleCalls = new List<DigiumLogItem>();

            foreach (var transaction in transactionGroup)
            {
                List<DigiumLogItem> transactionPossibleCalls = DigiumLogItem.FindByExtension(
                transaction.Extension, transaction.TimeCreated.AddMinutes(-30), transaction.TimeCreated.AddMinutes(30));

                foreach (var transactionCall in transactionPossibleCalls)
                {
                    var duplicateCall = possibleCalls.Find(call => call.ID == transactionCall.ID);
                    if (duplicateCall == null)
                        possibleCalls.Add(transactionCall);
                }
            }

            return possibleCalls;
        }

        private static void SetMatchBasedOnCallerId(DigiumLogItem digiumLogItem, Customer customer)
        {

            digiumLogItem.IsCallerIdMatch = false;

            if (customer == null)
                return;

            if (digiumLogItem.CallerIdNumber == customer.Phone1 || digiumLogItem.CallerIdNumber == customer.Phone2)
            {
                digiumLogItem.IsCustomerPhoneMatch = true;
                Host.Trace("Digium", "SetMatchBasedOnCallerId IsCustomerPhoneMatch is MATCH call ID:" + digiumLogItem.ID + " digiumLogItem.CallerIdNumber: " + digiumLogItem.CallerIdNumber + " customer.Phone2 " + customer.Phone2 + " customer.Phone1: " + customer.Phone1, 
                    HostTraceLevelEnum.Debug);    
            }

            string[] words = digiumLogItem.CallerName.Split(' ', ',');
            foreach (var word in words)
            {
                if (word.Length > 3 && customer.DisplayName.Contains(word))
                {
                    digiumLogItem.IsCallerIdMatch = true;
                    break;
                }

                if (word.Length <= 3 && (word == customer.FirstName || word == customer.LastName))
                {
                    digiumLogItem.IsCallerIdMatch = true;

                    break;
                }
            }

            if (digiumLogItem.IsManualCallerIdMatch)
                Host.Trace("Digium", "SetMatchBasedOnCallerId digiumLogItem.IsManualCallerIdMatch is MATCH call ID:" + digiumLogItem.ID + " digiumLogItem.CallerName: " + digiumLogItem.CallerName + " customer.DisplayName " + customer.DisplayName, 
                    HostTraceLevelEnum.Debug);
            else
                Host.Trace("Digium", "SetMatchBasedOnCallerId digiumLogItem.IsManualCallerIdMatch is NOT MATCH call ID:" + digiumLogItem.ID + " digiumLogItem.CallerName: " + digiumLogItem.CallerName + " customer.DisplayName " + customer.DisplayName, 
                    HostTraceLevelEnum.Debug);    

            return;
        }

        private static void SetIsManualCallerIdMatch(DigiumLogItem digiumLogItem, List<Transaction> transactionGroup)
        {
            digiumLogItem.IsManualCallerIdMatch = false;

            foreach (var transaction in transactionGroup)
            {
                Host.Trace("Digium", "SetIsManualCallerIdMatch call ID:" + digiumLogItem.ID + " digiumLogItem.CallerName: " + digiumLogItem.CallerName + " transaction1.ManualRecordedCallerId " + transaction.ManualRecordedCallerId, 
                    HostTraceLevelEnum.Debug);

                if (transaction.ManualRecordedCallerId.Trim() == string.Empty)
                    continue;

                string digits = string.Join(null, System.Text.RegularExpressions.Regex.Split(transaction.ManualRecordedCallerId, "[^\\d]"));

                if (!string.IsNullOrEmpty(digits) && digits.Length > 3)
                {
                    if (digiumLogItem.CallerIdNumber.EndsWith(digits))
                    {
                        digiumLogItem.IsManualCallerIdMatch = true;
                        Host.Trace("Digium",
                                   "SetIsManualCallerIdMatch MATCH FOUND call ID:" + digiumLogItem.ID +
                                   " digiumLogItem.CallerName: " + digiumLogItem.CallerName +
                                   " transaction1.ManualRecordedCallerId " + transaction.ManualRecordedCallerId, 
                                   HostTraceLevelEnum.Debug);
                        break;
                    }

                    continue;
                }

                string[] words = transaction.ManualRecordedCallerId.Trim().Split(' ', ',');

                foreach (var word in words)
                {
                    if (word.Length < 3)
                        continue;

                    if (digiumLogItem.CallerName.Contains(word))
                    {
                        digiumLogItem.IsManualCallerIdMatch = true;
                        Host.Trace("Digium", "SetIsManualCallerIdMatch MATCH FOUND call ID:" + digiumLogItem.ID + " digiumLogItem.CallerName: " + digiumLogItem.CallerName + " transaction1.ManualRecordedCallerId " + transaction.ManualRecordedCallerId, 
                            HostTraceLevelEnum.Debug);
                        break;
                    }
                }
            }
        }

        private static void SetIsStartedDuringCall(DigiumLogItem digiumLogItem, List<Transaction> transactionGroup)
        {
            Host.Trace("Digium", "SetIsStartedDuringCall call ID:" + digiumLogItem.ID + "digiumLogItem.TimeTalkStarted:" + digiumLogItem.TimeTalkStarted + " digiumLogItem.CallerName: " + digiumLogItem.CallerName, 
                HostTraceLevelEnum.Debug);

            digiumLogItem.IsStartedDuringCall = false;

            foreach (var transaction in transactionGroup)
            {
                if (digiumLogItem.TimeTalkStarted < transaction.TimeCreated && digiumLogItem.TimeTalkEnd > transaction.TimeCreated)
                {
                    digiumLogItem.IsStartedDuringCall = true;
                    Host.Trace("Digium",
                               "SetIsStartedDuringCall IsStartedDuringCall=TRUE call ID:" + digiumLogItem.ID +
                               " transaction.ID:" + transaction.ID + " digiumLogItem.TimeTalkStarted " +
                               digiumLogItem.TimeTalkStarted + " digiumLogItem.TimeTalkEnd: " +
                               digiumLogItem.TimeTalkEnd + " transaction.TimeCreated :" + transaction.TimeCreated, 
                               HostTraceLevelEnum.Debug);
                    return;
                }
               
                Host.Trace("Digium",
                               "SetIsStartedDuringCall IsStartedDuringCall=FALSE call ID:" + digiumLogItem.ID +
                               " transaction.ID:" + transaction.ID + " digiumLogItem.TimeTalkStarted " +
                               digiumLogItem.TimeTalkStarted + " digiumLogItem.TimeTalkEnd: " +
                               digiumLogItem.TimeTalkEnd + " transaction.TimeCreated :" + transaction.TimeCreated, 
                               HostTraceLevelEnum.Debug);
            }

            if (transactionGroup.Count < 2)
                return;

            if (transactionGroup[0].TimeCreated < digiumLogItem.TimeTalkStarted &&
                transactionGroup[transactionGroup.Count - 1].TimeCreated > digiumLogItem.TimeTalkStarted)
            {
                digiumLogItem.IsStartedDuringCall = true;
                Host.Trace("Digium",
                           "SetIsStartedDuringCall BETWEEN TRANSACTIONS IsStartedDuringCall=TRUE digiumLogItem.ID " + digiumLogItem.ID + " transactionGroup[0].ID " +
                           transactionGroup[0].ID + "transactionGroup[transactionGroup.Count - 1].ID" + transactionGroup[transactionGroup.Count - 1].ID + 
                           " transactionGroup[0].TimeCreated" + transactionGroup[0].TimeCreated + " transactionGroup[transactionGroup.Count - 1].TimeCreated" + transactionGroup[transactionGroup.Count - 1].TimeCreated + 
                           " digiumLogItem.TimeTalkStarted " + digiumLogItem.TimeTalkStarted +  
                               " digiumLogItem.TimeTalkStarted " +
                               digiumLogItem.TimeTalkStarted + " digiumLogItem.TimeTalkEnd: " +
                               digiumLogItem.TimeTalkEnd, HostTraceLevelEnum.Debug);
            }
        }

        #endregion

        #region GetEncodedMatchCriteria

        public static int GetEncodedMatchCriteria(bool isCustomerPhoneMatch, bool isCallerIdMatch,
            bool isManualCallerIdMatch, bool isStartedDuringCall)
        {
            int value = 0;
            if (isCustomerPhoneMatch)
                value |= 1;
            if (isCallerIdMatch)
                value |= 2;
            if (isManualCallerIdMatch)
                value |= 4;
            if (isStartedDuringCall)
                value |= 8;
            return value;
        }

        #endregion

        #region Match criteria decoding

        public bool IsCustomerPhoneMatch
        {
            get
            {
                if (!MatchCriteria.HasValue)
                    return false;
                return (MatchCriteria.Value & 1) == 1;
            }
        }

        public bool IsCallerIdMatch
        {
            get
            {
                if (!MatchCriteria.HasValue)
                    return false;
                return (MatchCriteria.Value & 2) == 2;
            }
        }

        public bool IsManualCallerIdMatch
        {
            get
            {
                if (!MatchCriteria.HasValue)
                    return false;
                return (MatchCriteria.Value & 4) == 4;
            }
        }

        public bool IsStartedDuringCall
        {
            get
            {
                if (!MatchCriteria.HasValue)
                    return false;
                return (MatchCriteria.Value & 8) == 8;
            }
        }

        #endregion
    }
}
      