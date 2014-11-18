
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Dalworth.Common.Data;


namespace Dalworth.LeadCentral.Domain
{
    public partial class CompaignTrackingPhone
    {
        public CompaignTrackingPhone()
        {
        }

        private const string SqlSelectAssignment = @"
SELECT * 
  FROM CompaignTrackingPhone
 WHERE CampaignId = ?CampaignId AND TrackingPhoneId = ?TrackingPhoneId
  AND ((DateReleased IS NULL) OR (DateReleased > Now()))
";

        public static CompaignTrackingPhone GetCurrentAssignment(int trackingPhoneId, int campaignId, IDbConnection connection)
        {
            CompaignTrackingPhone result = null;

            using (var dbCommand = Database.PrepareCommand(SqlSelectAssignment, connection))
            {
                Database.PutParameter(dbCommand, "?CampaignId", campaignId);
                Database.PutParameter(dbCommand, "?TrackingPhoneId", trackingPhoneId);

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        result = Load(dataReader);
                }
            }

            return result;
        }

        private const string SqlSelectAssignments = @"
SELECT * 
  FROM CompaignTrackingPhone
 WHERE TrackingPhoneId = ?TrackingPhoneId
  AND ((DateReleased IS NULL) OR (DateReleased > Now()))
";

        public static List<CompaignTrackingPhone>GetCurrentAssignments(int trackingPhoneId, IDbConnection connection)
        {
            var result = new List<CompaignTrackingPhone>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectAssignments, connection))
            {
                Database.PutParameter(dbCommand, "?TrackingPhoneId", trackingPhoneId);

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
      