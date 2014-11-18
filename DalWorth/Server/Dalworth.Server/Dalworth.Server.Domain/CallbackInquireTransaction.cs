using System;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class CallbackInquireTransaction
    {
        public CallbackInquireTransaction(){ }

        #region IsExistTransactionAfter

        private const string SqlIsExistTransactionAfter =
            @"SELECT * FROM CallbackInquireTransaction
                where CustomerId = ?CustomerId
                    and (VisitId is null or (VisitId is not null and VisitId <> ?VisitId))
                    and CallbackInquireDate > ?CallbackInquireDate";

        public static bool IsExistTransactionAfter(int customerId, int? ignoreVisitId, 
            DateTime inquireDate)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlIsExistTransactionAfter))
            {
                Database.PutParameter(dbCommand, "?CustomerId", customerId);
                Database.PutParameter(dbCommand, "?VisitId", ignoreVisitId ?? 0);
                Database.PutParameter(dbCommand, "?CallbackInquireDate", inquireDate);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return true;
                }
            }

            return false;
        }

        #endregion

        #region FindLastTransaction

        private const string SqlFindLastTransaction =
            @"SELECT * FROM CallbackInquireTransaction
                where CustomerId = ?CustomerId                    
              order by CallbackInquireDate desc
              limit 1";

        public static CallbackInquireTransaction FindLastTransaction(int customerId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindLastTransaction))
            {
                Database.PutParameter(dbCommand, "?CustomerId", customerId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("Last CallbackInquireTransaction not found");
        }

        #endregion

        #region FindByCustomerAndVisit

        private const string SqlFindByCustomerAndVisit =
            @"SELECT * FROM CallbackInquireTransaction
                where CustomerId = ?CustomerId                    
                    and VisitId = ?VisitId";

        public static CallbackInquireTransaction FindByCustomerAndVisit(int customerId, int visitId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByCustomerAndVisit))
            {
                Database.PutParameter(dbCommand, "?CustomerId", customerId);
                Database.PutParameter(dbCommand, "?VisitId", visitId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("CallbackInquireTransaction not found by CustomerId and VisitId");
        }

        #endregion

        #region DeleteTransactional

        public static void DeleteTransactional(int customerId, int visitId)
        {
            try
            {
                CallbackInquireTransaction transaction = FindByCustomerAndVisit(customerId, visitId);
                Delete(transaction);
            }
            catch (DataNotFoundException)
            {
                return;
            }

            Customer customer = Customer.FindByPrimaryKey(customerId);

            try
            {
                CallbackInquireTransaction lastTransaction = FindLastTransaction(customerId);

                customer.CallbackInquireDate = lastTransaction.CallbackInquireDate;
                customer.CallbackDaysInterval = lastTransaction.CallbackDaysInterval;
                customer.CallbackExactDate = lastTransaction.CallbackExactDate;
                customer.CallbackDoNotCall = lastTransaction.DoNotCall;
                
            }
            catch (DataNotFoundException)
            {
                customer.CallbackInquireDate = null;
                customer.CallbackDaysInterval = null;
                customer.CallbackExactDate = null;
                customer.CallbackDoNotCall = false;
            }

            Customer.Update(customer);
        }

        #endregion
    }
}
      