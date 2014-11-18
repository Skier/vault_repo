using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class ProjectPayment
    {
        public ProjectPayment() {}

        #region FindBy WorkTransaction

        private const string SqlFindByWorkTransaction =
            @"SELECT *
            FROM ProjectPayment
                WHERE WorkTransactionId = ?WorkTransactionId";

        public static List<ProjectPayment> FindBy(WorkTransaction workTransaction)
        {
            List<ProjectPayment> result = new List<ProjectPayment>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWorkTransaction))
            {
                Database.PutParameter(dbCommand, "?WorkTransactionId", workTransaction.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));                        
                }
            }

            return result;
        }

        #endregion
    }
}
      