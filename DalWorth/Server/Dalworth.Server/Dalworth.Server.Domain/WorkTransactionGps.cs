using System;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class WorkTransactionGps
    {
        public WorkTransactionGps(){}

        #region FindByWorkTransaction

        private const string SqlFindByWork =
            @"SELECT *
            FROM WorkTransactionGps
                WHERE WorkTransactionId = ?WorkTransactionId";


        public static WorkTransactionGps FindByWorkTransaction(WorkTransaction transaction)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWork))
            {
                Database.PutParameter(dbCommand, "?WorkTransactionId", transaction.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }
            throw new DataNotFoundException("WorkTransactionEtc not found");

        }

        #endregion
    }
}
      