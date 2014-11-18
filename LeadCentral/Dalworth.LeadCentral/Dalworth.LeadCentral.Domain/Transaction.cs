using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Common.Data;


namespace Dalworth.LeadCentral.Domain
{
    public partial class Transaction
    {
        public string TransactionType{
            get
            {
                var type = (TransactionTypeEnum)TransactionTypeId;
                return type.ToString();
            }
        }
        
        public Transaction()
        {
        }

        private const string SqlSelectTransactionsSum = @"
SELECT SUM(Amount) 
  FROM Transaction; 
";

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

        private const string SqlSelectTransactions = @"
SELECT *
  FROM Transaction 
 ORDER BY DateCreated DESC ;
";

        public static List<Transaction> GetAll(IDbConnection connection)
        {
            var result = new List<Transaction>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectTransactions, connection))
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
    
        private const string SqlSelectByFilter = @"
SELECT *
  FROM Transaction
 WHERE 1=1
";

        public static List<Transaction> FindTransactions(TransactionFilter filter, IDbConnection connection)
        {
            var sql = BuildFilterSql(SqlSelectByFilter, filter);

            using (var dbCommand = BuildFilterCommand(sql, filter, connection))
            {
                var result = new List<Transaction>();

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }

                return result;
            }
        }

        private static IDbCommand BuildFilterCommand(string sql, TransactionFilter filter, IDbConnection connection)
        {
            var dbCommand = Database.PrepareCommand(sql, connection);

            if (filter != null)
            {
                if (filter.CreatedFrom != null)
                    Database.PutParameter(dbCommand, "?CreatedFrom", filter.CreatedFrom.Value);

                if (filter.CreatedTo != null)
                    Database.PutParameter(dbCommand, "?CreatedTo", filter.CreatedTo.Value);
            }

            return dbCommand;
        }

        private static string BuildFilterSql(string baseSql, TransactionFilter filter)
        {
            var result = baseSql;

            if (filter != null)
            {
                if (filter.CreatedFrom != null)
                    result += String.Format(" AND DateCreated > ?CreatedFrom ");

                if (filter.CreatedTo != null)
                    result += String.Format(" AND DateCreated < ?CreatedTo ");
            }

            result += " ORDER BY DateCreated DESC ";

            return result;
        }
    }

    public class TransactionFilter
    {
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
    }
}
      