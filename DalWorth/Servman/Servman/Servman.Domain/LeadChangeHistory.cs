using System;
using System.Collections.Generic;
using System.Data;
using Servman.Data;


namespace Servman.Domain
{
    public partial class LeadChangeHistory
    {
        public LeadChangeHistory()
        {

        }

        private const String SqlSelectByLeadId = "Select * From LeadChangeHistory " +
                                                         " Where LeadId = ?LeadId; ";

        public static List<LeadChangeHistory> FindByLeadId(int leadId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByLeadId, connection))
            {
                Database.PutParameter(dbCommand, "?LeadId", leadId);

                var result = new List<LeadChangeHistory>();
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }
                return result;
            }
        }
    }
}
      