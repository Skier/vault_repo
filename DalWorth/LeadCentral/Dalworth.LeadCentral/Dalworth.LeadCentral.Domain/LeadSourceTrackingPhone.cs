using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.LeadCentral.Data;

namespace Dalworth.LeadCentral.Domain
{
    public partial class LeadSourceTrackingPhone
    {
        public LeadSourceTrackingPhone()
        {
        }

        private const String SqlDeleteByLeadSourceId = @"
DELETE FROM LeadSourceTrackingPhone 
 WHERE LeadSourceId = ?LeadSourceId ;";

        public static void RemoveByLeadSourceId(int leadSourceId, IDbConnection connection)
        {
            using (var dbCommand = Database.PrepareCommand(SqlDeleteByLeadSourceId, connection))
            {
                Database.PutParameter(dbCommand, "?LeadSourceId", leadSourceId);

                dbCommand.ExecuteNonQuery();
            }
        }

        private const String SqlFindByTrackingPhoneId = @"
SELECT * FROM LeadSourceTrackingPhone 
 WHERE TrackingPhoneId = ?TrackingPhoneId ;";

        public static List<LeadSourceTrackingPhone> GetByTrackingPhoneId(int phoneId, IDbConnection connection)
        {
            var result = new List<LeadSourceTrackingPhone>();

            using (var dbCommand = Database.PrepareCommand(SqlFindByTrackingPhoneId, connection))
            {
                Database.PutParameter(dbCommand, "?TrackingPhoneId", phoneId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }
    }
}
      