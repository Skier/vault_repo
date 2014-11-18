
using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.LeadCentral.Data;


namespace Dalworth.LeadCentral.Domain
{
    public partial class LeadSourcePhone
    {
        public LeadSourcePhone()
        {
        }

        private const String SqlFindByLeadSourceId = @"
SELECT * FROM LeadSourcePhone 
 WHERE LeadSourceId = ?LeadSourceId 
   AND IsRemoved = 0 ;";

        public static List<LeadSourcePhone> GetByLeadSourceId(int leadSourceId, IDbConnection connection)
        {
            var result = new List<LeadSourcePhone>();
            using (var dbCommand = Database.PrepareCommand(SqlFindByLeadSourceId, connection))
            {
                Database.PutParameter(dbCommand, "?LeadSourceId", leadSourceId);

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }
            return result;
        }

    }
}
      