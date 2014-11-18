
using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.LeadCentral.Data;


namespace Dalworth.LeadCentral.Domain
{
    public partial class TrackingPhoneRotation
    {
        public PhoneCall RelatedPhoneCall { get; set; }
        public PhoneSms RelatedPhoneSms { get; set; }
        public LeadForm RelatedWebForm { get; set; }

        public TrackingPhoneRotation()
        {
        }

        private const String SqlSelectLastPhoneRotationByPhoneId = @"
SELECT * 
  FROM TrackingPhoneRotation
 WHERE TrackingPhoneId = ?TrackingPhoneId 
 ORDER BY TimeDisplay DESC
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

        private const String SqlSelectLastPhoneRotationByReferral = @"
SELECT * 
  FROM TrackingPhoneRotation
 WHERE ReferralUri = ?ReferralUri 
 ORDER BY TimeDisplay DESC
 LIMIT 1; ";

        public static TrackingPhoneRotation GetLastPhoneRotationByReferral(string referralUri, IDbConnection connection)
        {
            using (var dbCommand = Database.PrepareCommand(SqlSelectLastPhoneRotationByReferral, connection))
            {
                Database.PutParameter(dbCommand, "?ReferralUri", referralUri);

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
 ORDER BY TimeDisplay DESC
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

            if (rotation != null && rotation.TimeDisplay.Add(new TimeSpan(0, sessionTimeoutMin, 0)) > DateTime.Now)
                return rotation.SessionIdUid;

            return null;
        }

        private const String SqlSelectRotationsBase = @"
SELECT * FROM TrackingPhoneRotation 
 WHERE 1=1 ";

        public static List<TrackingPhoneRotation> FindByLeadSourcesAndDatePeriod(LeadSource[] leadSources, DateTime? startDate, DateTime? endDate, IDbConnection connection)
        {
            var sql = SqlSelectRotationsBase;

            if (leadSources != null && leadSources.Length > 0)
            {
                sql += " AND LeadSourceId IN ( ";
                sql += CreateInContent(leadSources);
                sql += " ) ";
            }

            if (startDate != null)
                sql += " AND TimeDisplay > ?StartDate ";

            if (endDate != null)
                sql += " AND TimeDisplay < ?EndDate ";

            sql += " ORDER BY TimeDisplay ;";

            using (IDbCommand dbCommand = Database.PrepareCommand(sql, connection))
            {
                if (startDate != null)
                    Database.PutParameter(dbCommand, "?StartDate", startDate);

                if (endDate != null)
                    Database.PutParameter(dbCommand, "?EndDate", endDate);

                var result = new List<TrackingPhoneRotation>();
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

        private static string CreateInContent(IEnumerable<LeadSource> leadSources)
        {
            var result = string.Empty;
            foreach (var source in leadSources)
            {
                if (!string.IsNullOrEmpty(result))
                    result += ", ";
                result += source.Id.ToString();
            }
            return result;
        }

        private const String SqlSelectByPhoneCallId = @"
SELECT * FROM TrackingPhoneRotation 
 WHERE SessionIdUid = (SELECT SessionIdUid FROM TrackingPhoneRotation WHERE PhoneCallId = ?PhoneCallId LIMIT 1) 
 ORDER BY TimeDisplay ;";

        public static List<TrackingPhoneRotation> FindByPhoneCallId(int phoneCallId, IDbConnection connection)
        {
            var result = new List<TrackingPhoneRotation>();
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPhoneCallId, connection))
            {
                Database.PutParameter(dbCommand, "?PhoneCallId", phoneCallId);

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

        private const String SqlSelectByPhoneSmsId = @"
SELECT * FROM TrackingPhoneRotation 
 WHERE SessionIdUid = (SELECT SessionIdUid FROM TrackingPhoneRotation WHERE PhoneSmsId = ?PhoneSmsId LIMIT 1) 
 ORDER BY TimeDisplay ;";

        public static List<TrackingPhoneRotation> FindByPhoneSmsId(int phoneSmsId, IDbConnection connection)
        {
            var result = new List<TrackingPhoneRotation>();
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPhoneSmsId, connection))
            {
                Database.PutParameter(dbCommand, "?PhoneSmsId", phoneSmsId);

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

        private const String SqlSelectByLeadFormId = @"
SELECT * FROM TrackingPhoneRotation 
 WHERE SessionIdUid = (SELECT SessionIdUid FROM TrackingPhoneRotation WHERE LeadFormId = ?LeadFormId LIMIT 1) 
 ORDER BY TimeDisplay ;";

        public static List<TrackingPhoneRotation> FindByLeadFormIdId(int leadFormId, IDbConnection connection)
        {
            var result = new List<TrackingPhoneRotation>();
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByLeadFormId, connection))
            {
                Database.PutParameter(dbCommand, "?LeadFormId", leadFormId);

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
    }
}
      