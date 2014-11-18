
using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.LeadCentral.Data;


namespace Dalworth.LeadCentral.Domain
{
    public partial class Transaction
    {
        public Transaction()
        {
        }
        private const string SqlSelectTransactionsSum = @"
SELECT SUM(Amount) 
  FROM Transaction; ";

        public static decimal GetCurrentBalance(IDbConnection connection)
        {
            decimal result = 0;
            using (var dbCommand = Database.PrepareCommand(SqlSelectTransactionsSum, connection))
            {
                using (var dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read() && !dataReader.IsDBNull(0))
                        result = dataReader.GetDecimal(0);
                }
            }

            return result;
        }

        private const string SqlSelectTransactionsCount = @"
SELECT COUNT(*)
  FROM Transaction;
";

        public static int GetTransactionsCount(IDbConnection connection)
        {
            var result = 0;
            using (var dbCommand = Database.PrepareCommand(SqlSelectTransactionsCount, connection))
            {
                using (var dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read() && !dataReader.IsDBNull(0))
                        result = dataReader.GetInt32(0);
                }
            }

            return result;
        }

        private const string SqlSelectTransactionsLimit = @"
SELECT *
  FROM Transaction
  ORDER BY TransactionDate DESC
  LIMIT {0}, {1}
";

        public static List<Transaction> GetTransactions(int offset, int limit, IDbConnection connection)
        {
            var result = new List<Transaction>();
            using (var dbCommand = Database.PrepareCommand(string.Format(SqlSelectTransactionsLimit, offset, limit), connection))
            {
                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while(dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }
            }
            return result;
        }
    
    }
}
      