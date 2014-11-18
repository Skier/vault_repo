using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Common.Data;
using Dalworth.Common.SDK;


namespace Dalworth.LeadCentral.Domain
{
    public partial class BusinessPartner
    {
        public User RelatedSalesRep { get; set; }

        public List<PartnerPhoneNumber> PhoneNumbers { get; set; }

        public List<Campaign> Campaigns { get; set; }
        public List<User> Users { get; set; }

        public BusinessPartner()
        {
        }

        public string SalesRepStr
        {
            get { return RelatedSalesRep != null ? RelatedSalesRep.Name : string.Empty; }
        }

        private const string SqlSelectAllStatement = @"
            SELECT * 
            FROM BusinessPartner 
            ORDER BY PartnerName
            ";

        public static List<BusinessPartner> FindOrderByPartnerName(IDbConnection connection)
        {
            var result = new List<BusinessPartner>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectAllStatement, connection))
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

        private const string SqlSelectActiveStatement = @"
        SELECT * 
          FROM BusinessPartner 
         WHERE IsRemoved = 0 
        ORDER BY PartnerName
        ";

        public static List<BusinessPartner> FindActive(IDbConnection connection)
        {
            var result = new List<BusinessPartner>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectActiveStatement, connection))
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

        private const string SqlSelectByPeriod = @"
SELECT * 
  FROM BusinessPartner 
 WHERE DateCreated >= ?DateFrom AND DateCreated <= ?DateTo AND IsRemoved = 0
 ORDER BY SalesRepId, DateCreated DESC ";

        public static List<BusinessPartner> GetAllByPeriod(DateTime? dateFrom, DateTime? dateTo, IDbConnection connection)
        {
            var result = new List<BusinessPartner>();

            var from = dateFrom ?? DateTime.MinValue;
            var to = dateTo ?? DateTime.MaxValue;

            using (var dbCommand = Database.PrepareCommand(SqlSelectByPeriod, connection))
            {
                Database.PutParameter(dbCommand, "?DateFrom", from);
                Database.PutParameter(dbCommand, "?DateTo", to); 
                
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

        private const string SqlSelectByCallerNumber = @"
SELECT bp.* 
  FROM BusinessPartner bp
    INNER JOIN PartnerPhoneNumber pn ON pn.BusinessPartnerId = bp.Id
 WHERE ( pn.PhoneDigits = ?CallerNumber OR bp.PhoneDigits = ?CallerNumber ) 
  AND bp.IsRemoved = 0
";

        public static List<BusinessPartner> GetByCallerNumber(string fromPhone, IDbConnection connection)
        {
            var result = new List<BusinessPartner>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectByCallerNumber, connection))
            {
                Database.PutParameter(dbCommand, "?CallerNumber", StringUtil.ExtractLastSevenDigits(fromPhone));
                
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
SELECT *
  FROM BusinessPartner 
 WHERE 1=1
";

        public static List<BusinessPartner> LoadPartners(PartnerFilter filter, IDbConnection connection)
        {
            var sql = BuildFilterSql(SqlSelectByFilter, filter);

            using (var dbCommand = BuildFilterCommand(sql, filter, connection))
            {
                var result = new List<BusinessPartner>();

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

        private static IDbCommand BuildFilterCommand(string sql, PartnerFilter filter, IDbConnection connection)
        {
            var dbCommand = Database.PrepareCommand(sql, connection);

            if (filter != null)
            {
                if (filter.SalesRepId > 0)
                    Database.PutParameter(dbCommand, "?SalesRepId", filter.SalesRepId);

            }

            return dbCommand;
        }

        private static string BuildFilterSql(string baseSql, PartnerFilter filter)
        {
            var result = baseSql;

            if (filter != null)
            {
                if (filter.SalesRepId > 0)
                    result += String.Format(" AND SalesRepId = ?SalesRepId ");

                if (!filter.ShowRemoved)
                    result += String.Format(" AND IsRemoved = 0 ");
            }

            result += " ORDER BY PartnerName ";

            return result;
        }
    }

    public class PartnerFilter
    {
        public bool ShowRemoved { get; set; }
        public int SalesRepId { get; set; }
    }
}
      