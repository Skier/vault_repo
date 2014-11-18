
using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Common.Data;


namespace Dalworth.LeadCentral.Domain
{
    public partial class TrackingPhone
    {
        public Campaign AssignedCampaign { get; set; }

        public TrackingPhone()
        {
        }

        private const String SqlSelectByPhoneNumber = @"
SELECT * 
  FROM TrackingPhone
 WHERE PhoneNumber = ?PhoneNumber; ";

        public static TrackingPhone GetByPhoneNumber(string phoneNumber, IDbConnection connection)
        {
            using (var dbCommand = Database.PrepareCommand(SqlSelectByPhoneNumber, connection))
            {
                Database.PutParameter(dbCommand, "?PhoneNumber", phoneNumber);

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        private const string SqlSelectByCampaignId = @"
SELECT DISTINCT p.* 
  FROM TrackingPhone p
    INNER JOIN CompaignTrackingPhone cp ON cp.TrackingPhoneId = p.Id
 WHERE cp.CampaignId = ?CampaignId
  AND ((cp.DateReleased IS NULL) OR (cp.DateReleased > Now()))
";

        public static List<TrackingPhone> GetByCampaign(int campaignId, IDbConnection connection)
        {
            var result = new List<TrackingPhone>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectByCampaignId, connection))
            {
                Database.PutParameter(dbCommand, "?CampaignId", campaignId);
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

        private const string SqlSelectAllByPartnerId = @"
SELECT DISTINCT p.* 
  FROM TrackingPhone p
    INNER JOIN CompaignTrackingPhone cp ON cp.TrackingPhoneId = p.Id
    INNER JOIN Campaign c ON cp.CampaignId = c.Id
 WHERE c.BusinessPartnerId = ?BusinessPartnerId
  AND ((cp.DateReleased IS NULL) OR (cp.DateReleased > Now()))
";

        public static List<TrackingPhone> GetAll(int businessPartnerId, IDbConnection connection)
        {
            var result = new List<TrackingPhone>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectAllByPartnerId, connection))
            {
                Database.PutParameter(dbCommand, "?BusinessPartnerId", businessPartnerId);
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

        private const string SqlSelectAllActive = @"
SELECT * 
  FROM TrackingPhone
 WHERE IsRemoved = 0
";

        public static List<TrackingPhone> GetAllActive(IDbConnection connection)
        {
            var result = new List<TrackingPhone>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectAllActive, connection))
            {
                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }
            }

            foreach (var phone in result)
            {
                phone.AssignedCampaign = Campaign.GetCampaignByTrackingPhoneAndDate(phone.Id, DateTime.Now, connection);
            }

            return result;
        }

        private const string SqlSelectAllActiveByPartnerId = @"
SELECT DISTINCT p.* 
  FROM TrackingPhone p
    INNER JOIN CompaignTrackingPhone cp ON cp.TrackingPhoneId = p.Id
    INNER JOIN Campaign c ON cp.CampaignId = c.Id
 WHERE c.BusinessPartnerId = ?BusinessPartnerId
   AND IsSuspended = 0 AND IsRemoved = 0
  AND ((cp.DateReleased IS NULL) OR (cp.DateReleased > Now()))
";

        public static List<TrackingPhone> GetAllActive(int businessPartnerId, IDbConnection connection)
        {
            var result = new List<TrackingPhone>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectAllActiveByPartnerId, connection))
            {
                Database.PutParameter(dbCommand, "?BusinessPartnerId", businessPartnerId);

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }
            }

            foreach (var phone in result)
            {
                phone.AssignedCampaign = Campaign.GetCampaignByTrackingPhoneAndDate(phone.Id, DateTime.Now, connection);
            }

            return result;
        }

        private const string SqlSelectUnassigned = @"
SELECT DISTINCT p.*
  FROM TrackingPhone p
    LEFT OUTER JOIN CompaignTrackingPhone cp ON cp.TrackingPhoneId = p.Id
 WHERE p.IsRemoved = 0 AND ((cp.Id) IS NULL
    OR (p.Id not in (
                 select trackingphoneid
                   from compaignTrackingPhone
                  where DateReleased IS NULL
                     OR DateReleased > Now())))
";

        public static List<TrackingPhone> GetUnassignedPhones(IDbConnection connection)
        {
            var result = new List<TrackingPhone>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectUnassigned, connection))
            {
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
      