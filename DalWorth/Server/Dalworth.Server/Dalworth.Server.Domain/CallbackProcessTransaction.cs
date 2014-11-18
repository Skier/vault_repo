using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class CallbackProcessTransaction
    {
        public CallbackProcessTransaction(){ }

        #region CallbackProcessTransactionStatus

        [XmlIgnore]
        public CallbackProcessTransactionStatusEnum CallbackProcessTransactionStatus
        {
            get { return (CallbackProcessTransactionStatusEnum)m_callbackProcessTransactionStatusId; }
            set { m_callbackProcessTransactionStatusId = (int)value; }
        }

        #endregion
    }

    public class CallbackReportWrapper : IComparable
    {        
        #region Constructor

        public CallbackReportWrapper(Customer customer, CallbackProcessTransaction processTransaction)
        {
            m_customer = customer;
            m_processTransaction = processTransaction;
        }

        #endregion

        #region Customer

        private Customer m_customer;
        public Customer Customer
        {
            get { return m_customer; }
            set { m_customer = value; }
        }

        #endregion

        #region ProcessTransaction

        private CallbackProcessTransaction m_processTransaction;
        public CallbackProcessTransaction ProcessTransaction
        {
            get { return m_processTransaction; }
            set { m_processTransaction = value; }
        }

        #endregion

        #region Find

        private const string SqlFind =
            @"(select * from Customer c
                left join CallbackProcessTransaction cpt on DATE(cpt.TransactionDate) = DATE(Now()) and cpt.CustomerId = c.ID
                where CallbackInquireDate is not null and CallbackDoNotCall = false and cpt.CustomerId is null

                    and c.ID not in (SELECT distinct c.ID FROM Task t
                        inner join Project p on p.ID = t.ProjectId
                        inner join Customer c on c.ID = p.CustomerId
                        where (TaskTypeId = 1 or TaskTypeId = 2) and DumpedTaskId is null and TaskStatusId = 1 and TaskFailTypeId <> 3)

                    and ((CallbackExactDate is not null and Date(Now()) >= Date(CallbackExactDate))
                        or (CallbackDaysInterval is not null and DATE(ADDDATE(CallbackInquireDate, CallbackDaysInterval)) <= DATE(Now()))
                       )
              )
            union
            (SELECT c.*, cpt.* FROM CallbackProcessTransaction cpt
                inner join Customer c on c.ID = cpt.CustomerId
            where DATE(TransactionDate) = Date(Now()))";

        public static List<CallbackReportWrapper> Find()
        {
            List<CallbackReportWrapper> result = new List<CallbackReportWrapper>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFind))
            {                
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Customer customer = Domain.Customer.Load(dataReader);
                        CallbackProcessTransaction processTransaction = null;
                        if (!dataReader.IsDBNull(Customer.FieldsCount))
                            processTransaction = CallbackProcessTransaction.Load(dataReader, Customer.FieldsCount);

                        result.Add(new CallbackReportWrapper(customer, processTransaction));
                    }                        
                }
            }

            return result;
        }

        #endregion


        #region IsProcessed

        public bool IsProcessed
        {
            get { return m_processTransaction != null; }
        }

        #endregion

        #region Rank

        //higher rank - more importance
        public int RankPrimary
        {
            get
            {
                if (m_processTransaction == null)
                {
                    if (m_customer.CallbackExactDate.HasValue)
                        return 9;

                    if (m_customer.CallbackDaysInterval.HasValue)
                        return 7;

                    
                    return 6;
                } else
                {
                    if (m_processTransaction.CallbackProcessTransactionStatus == CallbackProcessTransactionStatusEnum.VisitCreated
                        ||  m_processTransaction.CallbackProcessTransactionStatus == CallbackProcessTransactionStatusEnum.NotIntrested
                        ||  m_processTransaction.CallbackProcessTransactionStatus == CallbackProcessTransactionStatusEnum.DoNotCall
                        ||  m_processTransaction.CallbackProcessTransactionStatus == CallbackProcessTransactionStatusEnum.CallReschedule)
                    {
                        return 0;
                    }
                    
                    if (m_processTransaction.CallbackProcessTransactionStatus == CallbackProcessTransactionStatusEnum.LeftMessage)
                    {
                        if (m_customer.CallbackExactDate.HasValue)
                            return 3;

                        if (m_customer.CallbackDaysInterval.HasValue)
                            return 2;

                        return 1;                        
                    }

                    if (m_processTransaction.CallbackProcessTransactionStatus == CallbackProcessTransactionStatusEnum.Busy)
                    {
                        if (m_customer.CallbackExactDate.HasValue)
                            return 8;

                        if (m_customer.CallbackDaysInterval.HasValue)
                            return 5;

                        return 4;                        
                    }

                }

                return 0;
            }
        }

        public int RankSecondary
        {
            get
            {
                if (m_customer.CallbackExactDate.HasValue)
                {
                    return DateTime.Now.Subtract(m_customer.CallbackExactDate.Value).Days;
                }

                if (m_customer.CallbackDaysInterval.HasValue)
                {
                    return DateTime.Now.Subtract(m_customer.CallbackInquireDate.Value).Days 
                           - m_customer.CallbackDaysInterval.Value;
                }

                return DateTime.Now.Subtract(m_customer.CallbackInquireDate.Value).Days;
            }
        }

        #endregion


        #region StatusImageIndex

        public int StatusImageIndex
        {
            get
            {
                if (m_processTransaction != null)
                {
                    if (m_processTransaction.CallbackProcessTransactionStatus == CallbackProcessTransactionStatusEnum.Busy)
                        return 3;

                    if (m_processTransaction.CallbackProcessTransactionStatus == CallbackProcessTransactionStatusEnum.LeftMessage)
                        return 2;

                    return 1;
                }

                return 0;
            }
        }

        #endregion

        #region CustomerName

        public string CustomerName
        {
            get { return m_customer.DisplayName; }
        }

        #endregion

        #region CallbackReason

        public string CallbackReason
        {
            get
            {
                if (m_processTransaction != null)
                    return string.Empty;

                if (m_customer.CallbackExactDate.HasValue)
                    return "Request to call " + m_customer.CallbackExactDate.Value.ToShortDateString();

                if (m_customer.CallbackDaysInterval.HasValue)
                {
                    if (m_customer.CallbackDaysInterval.Value == 365)
                        return "Call in 1 year";

                    if (m_customer.CallbackDaysInterval.Value % 365 == 0)
                        return "Call in " + m_customer.CallbackDaysInterval.Value / 365 + " years";

                    if (m_customer.CallbackDaysInterval.Value == 30)
                        return "Call in 1 month";

                    if (m_customer.CallbackDaysInterval.Value == 60)
                        return "Call in 2 months";

                    if (m_customer.CallbackDaysInterval.Value == 91)
                        return "Call in 3 months";

                    if (m_customer.CallbackDaysInterval.Value == 121)
                        return "Call in 4 months";

                    if (m_customer.CallbackDaysInterval.Value == 152)
                        return "Call in 5 months";

                    if (m_customer.CallbackDaysInterval.Value == 182)
                        return "Call in 6 months";

                    if (m_customer.CallbackDaysInterval.Value == 212)
                        return "Call in 7 months";

                    if (m_customer.CallbackDaysInterval.Value == 243)
                        return "Call in 8 months";

                    if (m_customer.CallbackDaysInterval.Value == 273)
                        return "Call in 9 months";

                    if (m_customer.CallbackDaysInterval.Value == 304)
                        return "Call in 10 months";

                    if (m_customer.CallbackDaysInterval.Value == 334)
                        return "Call in 11 months";

                    if (m_customer.CallbackDaysInterval.Value == 1)
                        return "Call in 1 day";

                    return "Call in " + m_customer.CallbackDaysInterval.Value + " days";
                }

                return "Regular 6 months callback";
            }
        }

        #endregion

        #region LeftMessageBusyCount

        public string LeftMessageBusyCount
        {
            get 
            { 
                if (m_customer.CallbackLeftMessageCount == 0
                    && m_customer.CallbackBusyCount == 0)
                {
                    return string.Empty;
                }

                return m_customer.CallbackLeftMessageCount + "/" + m_customer.CallbackBusyCount;
            }
        }

        #endregion

        #region CallbackLastAttemptDate

        public DateTime? CallbackLastAttemptDate
        {
            get { return m_customer.CallbackLastAttemptDate; }
        }

        #endregion

        #region ProcessingDescription

        public string ProcessingDescription
        {
            get
            {
                if (m_processTransaction == null)
                    return string.Empty;

                if (m_processTransaction.CallbackProcessTransactionStatus == CallbackProcessTransactionStatusEnum.Busy)
                    return "Busy";

                if (m_processTransaction.CallbackProcessTransactionStatus == CallbackProcessTransactionStatusEnum.CallReschedule)
                    return "Call rescheduled";

                if (m_processTransaction.CallbackProcessTransactionStatus == CallbackProcessTransactionStatusEnum.DoNotCall)
                    return "Do not call";

                if (m_processTransaction.CallbackProcessTransactionStatus == CallbackProcessTransactionStatusEnum.LeftMessage)
                    return "Left a message";

                if (m_processTransaction.CallbackProcessTransactionStatus == CallbackProcessTransactionStatusEnum.NotIntrested)
                    return "Not interested";

                if (m_processTransaction.CallbackProcessTransactionStatus == CallbackProcessTransactionStatusEnum.VisitCreated)
                    return "Visit created";

                return string.Empty;
            }
        }

        #endregion

        #region ProcessLinkText

        public string ProcessLinkText
        {
            get { return "Process"; }
        }

        #endregion


        #region IComparable

        public int CompareTo(object obj)
        {
            CallbackReportWrapper comparable = (CallbackReportWrapper)obj;

            if (RankPrimary.CompareTo(comparable.RankPrimary) == 0)
                return -RankSecondary.CompareTo(comparable.RankSecondary);

            return -RankPrimary.CompareTo(comparable.RankPrimary);
        }

        #endregion

    }
}
      