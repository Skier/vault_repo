using System;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class WorkTransactionEtc
    {
        public WorkTransactionEtc(){ }

        #region FindByWorkTransaction

        private const string SqlFindByWork =
            @"SELECT *
            FROM WorkTransactionEtc
                WHERE WorkTransactionId = ?WorkTransactionId";


        public static WorkTransactionEtc FindByWorkTransaction(WorkTransaction transaction)
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

        #region FindLastETC

        private const string SqlFindLastETC =
            @"SELECT wtc.* FROM WorkTransaction wt
                inner join WorkTransactionEtc wtc on wt.ID = wtc.WorkTransactionId
            where wt.WorkTransactionTypeId = 7 and VisitId = ?VisitId
            order by wtc.WorkTransactionId desc
            limit 1";

        public static WorkTransactionEtc FindLastETC(Visit visit)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindLastETC))
            {
                Database.PutParameter(dbCommand, "?VisitId", visit.ID);

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
      