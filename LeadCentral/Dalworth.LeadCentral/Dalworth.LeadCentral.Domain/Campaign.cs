using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dalworth.Common.Data;


namespace Dalworth.LeadCentral.Domain
{
    public partial class Campaign
    {

        public BusinessPartner RelatedBusinessPartner { get; set; }
        public User RelatedUser { get; set; }

        public List<TrackingPhone> TrackingPhones { get; set; }

        public string PartnerName
        {
            get { return RelatedBusinessPartner != null ? RelatedBusinessPartner.PartnerName : string.Empty; }
        }

        public string UserName
        {
            get { return RelatedUser != null ? RelatedUser.ScreenName : string.Empty; }
        }

        public string StatusStr
        {
            get { return DateEnd == null ? 
                CampaignStatusEnum.Active.ToString() 
                : (string.Format("{0} ({1:d})", CampaignStatusEnum.Closed.ToString(), DateEnd)); }
        }

        public string TrackingPhonesStr
        {
            get 
            { 
                if (TrackingPhones != null && TrackingPhones.Count > 0)
                    return string.Join(", ", TrackingPhones.Select(phone => phone.FriendlyNumber).ToArray());

                return string.Empty;
            }
        }

        public Campaign()
        {
        }

        private const string SqlSelectAllStatement = @"
SELECT * 
  FROM Campaign
 WHERE 1=1 ";

        private const string SqlStatementActiveForDate = @"
  AND ( ( `DateEnd` IS NULL AND `DateStart` <= ?CurrentDate ) OR ( CAST( ?CurrentDate AS DATETIME) BETWEEN `DateStart` AND `DateEnd` ) ) ";

        private const string SqlStatementByPartnerId = @"
  AND BusinessPartnerId = ?BusinessPartnerId ";


        public static List<Campaign> GetAll(IDbConnection connection)
        {
            return GetAll(null, connection);
        }

        public static List<Campaign> GetAll(int? businessPartnerId, IDbConnection connection)
        {
            var sql = SqlSelectAllStatement;
            if (businessPartnerId != null)
                sql += SqlStatementByPartnerId;

            sql += " ORDER BY CampaignName ";

            var result = new List<Campaign>();

            using (var dbCommand = Database.PrepareCommand(sql, connection))
            {
                if (businessPartnerId != null)
                    Database.PutParameter(dbCommand, "?BusinessPartnerId", businessPartnerId.Value);

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

        public static List<Campaign> GetAllActive(IDbConnection connection)
        {
            return GetAllActive(null, connection);
        }

        public static List<Campaign> GetAllActive(DateTime date, IDbConnection connection)
        {
            return GetAllActive(date, null, connection);
        }

        public static List<Campaign> GetAllActive(int? businessPartnerId, IDbConnection connection)
        {
            var date = DateTime.Now;
            return GetAllActive(date, businessPartnerId, connection);
        }

        public static List<Campaign> GetAllActive(DateTime date, int? businessPartnerId, IDbConnection connection)
        {
            var sql = SqlSelectAllStatement;
            sql += SqlStatementActiveForDate;

            if (businessPartnerId != null)
                sql += SqlStatementByPartnerId;

            sql += " ORDER BY CampaignName ";

            var result = new List<Campaign>();

            using (var dbCommand = Database.PrepareCommand(sql, connection))
            {
                Database.PutParameter(dbCommand, "?CurrentDate", date);

                if (businessPartnerId != null)
                    Database.PutParameter(dbCommand, "?BusinessPartnerId", businessPartnerId.Value);

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

        private const string SqlSelectCampaignByTrackingPhoneAndDate = @"
SELECT c.*
  FROM Campaign c 
    INNER JOIN CompaignTrackingPhone cp ON c.Id = cp.CampaignId
 WHERE cp.TrackingPhoneId = ?TrackingPhoneId 
   AND cp.DateAssigned < ?DateTime 
   AND ( cp.DateReleased IS NULL OR cp.DateReleased > ?DateTime )
";

        public static Campaign GetCampaignByTrackingPhoneAndDate(int trackingPhoneId, DateTime dateTime, IDbConnection connection)
        {
            using (var dbCommand = Database.PrepareCommand(SqlSelectCampaignByTrackingPhoneAndDate, connection))
            {
                Database.PutParameter(dbCommand, "?TrackingPhoneId", trackingPhoneId);
                Database.PutParameter(dbCommand, "?DateTime", dateTime);

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        private const string SqlSelectByBusinessPartnerId = @"
SELECT *
  FROM Campaign
 WHERE BusinessPartnerId = ?BusinessPartnerId 
   AND DateEnd IS NULL
";

        public static List<Campaign> GetByBusinessPartnerId(int id, IDbConnection connection)
        {
            var result = new List<Campaign>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectByBusinessPartnerId, connection))
            {
                Database.PutParameter(dbCommand, "?BusinessPartnerId", id);

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

        private const string SqlSelectByFilter = @"
SELECT c.*
  FROM Campaign c
  LEFT OUTER JOIN BusinessPartner bp ON bp.Id = c.BusinessPartnerId
 WHERE 1=1
";

        public static List<Campaign> GetCampaigns(CampaignFilter filter, IDbConnection connection)
        {
            var sql = BuildFilterSql(SqlSelectByFilter, filter);

            using (var dbCommand = BuildFilterCommand(sql, filter, connection))
            {
                var result = new List<Campaign>();

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }

                return result;
            }
        }

        private static IDbCommand BuildFilterCommand(string sql, CampaignFilter filter, IDbConnection connection)
        {
            var dbCommand = Database.PrepareCommand(sql, connection);

            if (filter != null)
            {
                if (filter.SalesRepId > 0)
                    Database.PutParameter(dbCommand, "?SalesRepId", filter.SalesRepId);

                if (filter.PartnerId > 0)
                    Database.PutParameter(dbCommand, "?BusinessPartnerId", filter.PartnerId);
            }

            return dbCommand;
        }

        private static string BuildFilterSql(string baseSql, CampaignFilter filter)
        {
            var result = baseSql;

            if (filter != null)
            {
                if (filter.SalesRepId > 0)
                    result += String.Format(" AND ( bp.SalesRepId = ?SalesRepId OR c.UserId = ?SalesRepId ) ");

                if (filter.PartnerId > 0)
                    result += String.Format(" AND c.BusinessPartnerId = ?BusinessPartnerId ");

                if (!filter.ShowClosed)
                    result += String.Format(" AND c.DateEnd IS NULL ");
            }

            result += " ORDER BY c.CampaignName ";

            return result;
        }

    }

    public class CampaignFilter
    {
        public int SalesRepId { get; set; }
        public int PartnerId { get; set; }
        public bool ShowClosed { get; set; }
    }

    public enum CampaignStatusEnum
    {
        Active,
        Closed
    }
}
      