
using System;
using System.Data;
using Dalworth.LeadCentral.Data;


namespace Dalworth.LeadCentral.Domain
{
    public partial class QbmsTransaction
    {
        public QbmsTransaction()
        {
        }

        private const string SqlSelectByOpId = "SELECT * FROM QbmsTransaction WHERE OpId = ?OpId ;";

        public static QbmsTransaction GetByOpId(string opId)
        {
            using (var dbCommand = Database.PrepareCommand(SqlSelectByOpId))
            {
                Database.PutParameter(dbCommand, "?OpId", opId);

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }
    }
}
      