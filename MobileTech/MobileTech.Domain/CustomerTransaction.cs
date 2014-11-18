using System;
using System.Data;
using MobileTech.Data;

namespace MobileTech.Domain
{
    public partial class CustomerTransaction
    {

        #region Constructors

        public CustomerTransaction()
        {

        }

        public CustomerTransaction(BusinessTransaction businessTransaction, CustomerVisit visit, CustomerTransactionTypeEnum type)
        {
            BusinessTransaction = businessTransaction;
            Type = type;
            m_customerVisitId = visit.CustomerVisitId;
        }

        #endregion

        #region Extra fields
        BusinessTransaction m_businessTransaction;
        public BusinessTransaction BusinessTransaction
        {
            get 
            {
                if (m_businessTransaction == null)
                {
                    if (m_businessTransactionId == 0 
                        || m_sessionId == 0)
                    {
                        throw new MobileTechException("BusinessTransactionId or Session field not initialized");
                    }

                    m_businessTransaction 
                        = BusinessTransaction.FindByPrimaryKey(
                        m_sessionId, 
                        m_businessTransactionId);
                }

                return m_businessTransaction; 
            }
            set 
            { 
                m_businessTransaction = value;

                if (value != null)
                {
                    m_businessTransactionId = value.BusinessTransactionId;
                    m_sessionId = value.SessionId;
                }
            }
        }

        public CustomerTransactionTypeEnum Type
        {
            get
            {
                return (CustomerTransactionTypeEnum)m_customerTransactionTypeId;
            }
            set
            {
                m_customerTransactionTypeId = (int)value;
            }
        }

        #endregion

        #region Finders

        #region Find by visit

        static String SqlFindByType_Visit_Queue = "Select " +
            "SessionId, " +
            "BusinessTransactionId, " +
            "CustomerTransactionTypeId, " +
            "CustomerVisitId " +
            "From CustomerTransaction Where " +
            " CustomerVisitId = @CustomerVisitId and " +
            " CustomerTransactionTypeId = @CustomerTransactionTypeId";

        public static CustomerTransaction Find(CustomerTransactionTypeEnum type,
            CustomerVisit customerVisit)
        {
            IDbCommand dbCommand = Database.PrepareCommand(SqlFindByType_Visit_Queue);


            Database.PutParameter(dbCommand, "@CustomerVisitId", customerVisit.CustomerVisitId);
            Database.PutParameter(dbCommand, "@CustomerTransactionTypeId", (int)type);

            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    return Load(dataReader);
                }
            }


            return null;
        }

        #endregion

        public static int? FindAll(DataTable dtCustomerTransaction)
        {
            IDbCommand command = Database.PrepareCommand(SqlSelectAll);

            IDataReader dataReader = command.ExecuteReader(CommandBehavior.SingleRow);
            dtCustomerTransaction.Load(dataReader);
            return dtCustomerTransaction.Rows.Count;
        }

        #endregion

        #region Servive
        public static CustomerTransaction Prepare(
            Route route,
            CustomerVisit visit,
            CustomerTransactionTypeEnum type)
        {
            CustomerTransaction transaction = new CustomerTransaction(
                            BusinessTransaction.Prepare(
                            route,
                            new Session(visit.SessionId),
                            BusinessTransactionTypeEnum.Customer),visit,type);

            return transaction;
        }
        #endregion
    }
}
