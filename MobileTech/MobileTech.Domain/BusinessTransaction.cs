using System;
using System.Data;
using MobileTech.Data;

namespace MobileTech.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BusinessTransaction:ICounterField
    {
        public const int DOCUMENT_NUMBER_LENGTH = 30;

        #region Constructors

        public BusinessTransaction()
        {

        }

        #endregion

        #region Extra fields

        public BusinessTransactionStatusEnum Status
        {
            get
            {
                return (BusinessTransactionStatusEnum)m_businessTransactionStatusId;
            }
            set
            {
                m_businessTransactionStatusId = (int)value;
            }
        }

        public BusinessTransactionTypeEnum Type
        {
            get
            {
                return (BusinessTransactionTypeEnum)m_businessTransactionTypeId;
            }
            set
            {
                m_businessTransactionTypeId = (int)value;
            }
        }

        #endregion

        #region ICounterField Members

        public int CounterValue
        {
            get
            {
                return m_businessTransactionId;
            }
            set
            {
                m_businessTransactionId = value;
            }
        }

        private static String counterName = "BusinessTransaction";

        public string CounterName
        {
            get
            {
                return counterName;
            }
        }

        #endregion

        #region Finders
        public static int? FindAll(DataTable dtBusinessTransaction)
        {
            IDbCommand command = Database.PrepareCommand(SqlSelectAll);

            IDataReader dataReader = command.ExecuteReader(CommandBehavior.SingleRow);
            dtBusinessTransaction.Load(dataReader);
            return dtBusinessTransaction.Rows.Count;
        }
        #endregion

        #region Service

        #region SqlStatements
        static String SqlFindMaxPeriodIndex = "Select Max(PeriodIndex) " +
            " From BusinessTransaction";
        #endregion

        #region Find max period index

        public static int FindMaxPeriodIndex()
        {
            IDbCommand dbCommand = Database.PrepareCommand(SqlFindMaxPeriodIndex);

            Object o = dbCommand.ExecuteScalar();

            if (o is long)
                return (int)(long)o;

            return 0;
        }

        #endregion

        #region Prepare transaction

        public static BusinessTransaction Prepare(BusinessTransactionTypeEnum type)
        {
            return Prepare(Route.FindCurrent(), Session.FindActive(), type);
        }

        public static BusinessTransaction Prepare(Route route,
            Session session,
            BusinessTransactionTypeEnum type)
        {
            BusinessTransaction transaction = new BusinessTransaction();

            transaction.DateCreated = DateTime.Now;
            transaction.RouteNumber = route.RouteNumber;
            transaction.Status = BusinessTransactionStatusEnum.Created;
            transaction.Type = type;
            transaction.EmployeeId = route.EmployeeId; // must be current logined employee
            transaction.DocumentNumber = String.Format("{0}", Guid.NewGuid()); // temporary document number
            transaction.PeriodIndex = FindMaxPeriodIndex();
            transaction.SessionId = session.SessionId;
            transaction.LocationId = route.LocationId;
            transaction.Password = Domain.Password.EmptyValue;

            return transaction;
        }

        #endregion

        #region Assign document number
        [TransactionRequired]
        public static void AssignDocumentNumber(BusinessTransaction businessTransaction)
        {

            if (Connection.Instance.Transaction == null)
                throw new MobileTechException("Transaction context required");


            Route route = Route.FindByPrimaryKey(businessTransaction.LocationId,
                businessTransaction.RouteNumber);

            route.DocumentNumberSequence++;

            Route.Update(route);

            int prefixLength = route.DocumentNumberPrefix.ToString().Length;
            int sequenceLength = route.DocumentNumberSequence.ToString().Length;
            int zeroStringLength = DOCUMENT_NUMBER_LENGTH - prefixLength - sequenceLength;

            String zeroString = zeroStringLength > 0 ? new string('0', zeroStringLength) : String.Empty;

            businessTransaction.DocumentNumber = String.Format("{0}{1}{2}",
                route.DocumentNumberPrefix,
                zeroString,
                route.DocumentNumberSequence);

            BusinessTransaction.Update(businessTransaction);
        }
        #endregion

        #endregion

    }
}
