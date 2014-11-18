
using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Common.Data;


namespace Dalworth.LeadCentral.Domain
{
    public partial class TrackingPhoneRotation
    {
        public TrackingPhoneRotation()
        {
        }

        private const String SqlSelectRelatedById = @"
SELECT * FROM TrackingPhoneRotation 
 WHERE SessionUid = (SELECT SessionUid FROM TrackingPhoneRotation WHERE Id = ?TrackingPhoneRotationId LIMIT 1) 
 ORDER BY TimeDisplay ;
";

        public static List<TrackingPhoneRotation> FindByPhoneCall(PhoneCall phoneCall, IDbConnection connection)
        {
            var result = new List<TrackingPhoneRotation>();
            if (phoneCall.TrackingPhoneRotationId == null)
                return result;

            using (var dbCommand = Database.PrepareCommand(SqlSelectRelatedById, connection))
            {
                Database.PutParameter(dbCommand, "?TrackingPhoneRotationId", phoneCall.TrackingPhoneRotationId);

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }
            }
            return result;
        }

        public static List<TrackingPhoneRotation> FindByPhoneSms(PhoneSms phoneSms, IDbConnection connection)
        {
            var result = new List<TrackingPhoneRotation>();
            if (phoneSms.TrackingPhoneRotationId == null)
                return result;

            using (var dbCommand = Database.PrepareCommand(SqlSelectRelatedById, connection))
            {
                Database.PutParameter(dbCommand, "?TrackingPhoneRotationId", phoneSms.TrackingPhoneRotationId);

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }
            }
            return result;
        }

        private const String SqlSelectLastPhoneRotationByPhoneId = @"
SELECT * 
  FROM TrackingPhoneRotation
 WHERE TrackingPhoneId = ?TrackingPhoneId 
 ORDER BY TimeRotation DESC
 LIMIT 1; ";

        public static TrackingPhoneRotation GetLastPhoneRotationByPhoneId(int trackingPhoneId, IDbConnection connection)
        {
            using (var dbCommand = Database.PrepareCommand(SqlSelectLastPhoneRotationByPhoneId, connection))
            {
                Database.PutParameter(dbCommand, "?TrackingPhoneId", trackingPhoneId);

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        private const String SqlSelectLastByUserHost = @"
SELECT * 
  FROM TrackingPhoneRotation
 WHERE UserHostAddress = ?UserHostAddress 
 ORDER BY TimeRotation DESC
 LIMIT 1; ";

        public static string GetSessionIdByHostAddress(string userHostAddress, int sessionTimeoutMin, IDbConnection connection)
        {
            TrackingPhoneRotation rotation = null;
            using (var dbCommand = Database.PrepareCommand(SqlSelectLastByUserHost, connection))
            {
                Database.PutParameter(dbCommand, "?UserHostAddress", userHostAddress);

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        rotation = Load(dataReader);
                }
            }

            if (rotation != null && rotation.TimeRotation.Add(new TimeSpan(0, sessionTimeoutMin, 0)) > DateTime.Now)
                return rotation.SessionUid;

            return null;
        }
    }
}
      