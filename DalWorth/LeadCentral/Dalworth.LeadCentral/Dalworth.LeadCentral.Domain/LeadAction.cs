using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.LeadCentral.Data;

namespace Dalworth.LeadCentral.Domain
{
    public partial class LeadAction
    {
        public LeadAction()
        {
        }

        private const String SqlSelectByLeadStatusId = "Select * From LeadAction " +
                                                         " Where FromLeadStatusId = ?LeadStatusId" + 
                                                         " Order by Sequence; ";

        public static List<LeadAction> FindByLeadStatusId(int id, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByLeadStatusId, connection))
            {
                Database.PutParameter(dbCommand, "?LeadStatusId", id);

                var result = new List<LeadAction>();
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
      