using System; 
using System.Collections.Generic;
using System.Data;
using Servman.Data;


namespace Servman.Domain
{
    public partial class Lead
    {
        public PhoneCall RelatedPhoneCall { get; set; }
        public PhoneSms RelatedSms { get; set; }
        public LeadForm RelatedForm { get; set; }
        public QbInvoice[] RelatedQbInvoices { get; set; }

        public Lead()
        {
        }

        private const String SqlSelectLeadsBase = @"
SELECT * FROM Lead 
 WHERE 1=1 ";

        public static List<Lead> FindByLeadSourcesAndDatePeriod(LeadSource[] leadSources, DateTime? startDate, DateTime? endDate, IDbConnection connection)
        {
            var sql = SqlSelectLeadsBase;

            if (leadSources != null && leadSources.Length > 0)
            {
                sql += " AND LeadSourceId IN ( ";
                sql += CreateInContent(leadSources);
                sql += " ) ";
            }

            if (startDate != null)
                sql += " AND DateCreated > ?StartDate ";

            if (endDate != null)
                sql += " AND DateCreated < ?EndDate ";

            using (IDbCommand dbCommand = Database.PrepareCommand(sql, connection))
            {
                if (startDate != null)
                    Database.PutParameter(dbCommand, "?StartDate", startDate);

                if (endDate != null)
                    Database.PutParameter(dbCommand, "?EndDate", endDate);

                var result = new List<Lead>();
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

        private const String SqlSelectByPeriod = @"
SELECT l.* From Lead l
 WHERE ( 1 = 1 ) ";

        public static List<Lead> FindByPeriod(DateTime? startDate, DateTime? endDate, IDbConnection connection)
        {
            var sql = SqlSelectByPeriod;

            if (startDate != null)
                sql += " AND DateCreated > ?StartDate ";

            if (endDate != null)
                sql += " AND DateCreated < ?EndDate ";

            using (var dbCommand = Database.PrepareCommand(sql, connection))
            {
                if (startDate != null)
                    Database.PutParameter(dbCommand, "?StartDate", startDate);

                if (endDate != null)
                    Database.PutParameter(dbCommand, "?EndDate", endDate);

                var result = new List<Lead>();
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

        private const String SqlSelectPending = @"
SELECT * FROM Lead l
  LEFT OUTER JOIN Job j ON j.LeadId = l.Id
 WHERE j.Id IS NULL ";

        public static List<Lead> FindAllPending(IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectPending, connection))
            {
                List<Lead> result = new List<Lead>();
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

        private const String SqlSelectLeads = @"
SELECT * FROM Lead l
 WHERE 1=1 ";

        public static List<Lead> GetLeads(LeadFilter filter, IDbConnection connection)
        {
            var sql = BuildFilterSql(SqlSelectLeads, filter);

            using (var dbCommand = BuildFilterCommand(sql, filter, connection))
            {
                var result = new List<Lead>();

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

        public static List<Lead> GetLeads(LeadFilter filter, int offset, int count, IDbConnection connection)
        {
            var sql = BuildFilterSql(SqlSelectLeads, filter);

            sql += string.Format(" LIMIT {0}, {1} ", offset, count);

            using (var dbCommand = BuildFilterCommand(sql, filter, connection))
            {
                var result = new List<Lead>();

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

        private const String SqlSelectLeadsCount = @"
SELECT COUNT(*) FROM Lead l
 WHERE 1=1 ";

        public static int GetLeadsCount(LeadFilter filter, IDbConnection connection)
        {
            var sql = BuildFilterSql(SqlSelectLeadsCount, filter);

            using (var dbCommand = BuildFilterCommand(sql, filter, connection))
            {
                using (var dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read() && !dataReader.IsDBNull(0))
                        return dataReader.GetInt32(0);

                    return 0;
                }
            }
        }

        private static IDbCommand BuildFilterCommand(string sql, LeadFilter filter, IDbConnection connection)
        {
            var dbCommand = Database.PrepareCommand(sql, connection);

            if (filter != null)
            {
                if (filter.LeadTypeId > 0)
                    Database.PutParameter(dbCommand, "?LeadTypeId", filter.LeadTypeId);

                if (filter.LeadSourceId > 0)
                    Database.PutParameter(dbCommand, "?LeadSourceId", filter.LeadSourceId);

                if (filter.AssignedToUserId > 0)
                    Database.PutParameter(dbCommand, "?AssignedToUser", filter.AssignedToUserId);

                if (filter.CreatedByUserId > 0)
                    Database.PutParameter(dbCommand, "?CreatedByUserId", filter.CreatedByUserId);

                if (filter.DateFrom != null)
                    Database.PutParameter(dbCommand, "?DateFrom", filter.DateFrom);

                if (filter.DateTo != null)
                    Database.PutParameter(dbCommand, "?DateTo", filter.DateTo);
            }

            return dbCommand;
        }

        private static string BuildFilterSql(string baseSql, LeadFilter filter)
        {
            var result = baseSql;

            if (filter != null)
            {
                if (filter.LeadTypeId > 0)
                    result += String.Format(" AND LeadTypeId = ?LeadTypeId ");

                if (filter.LeadSourceId > 0)
                    result += String.Format(" AND LeadSourceId = ?LeadSourceId ");

                if (filter.AssignedToUserId > 0)
                    result += String.Format(" AND AssignedToUser = ?AssignedToUser ");

                if (filter.CreatedByUserId > 0)
                    result += String.Format(" AND CreatedByUserId = ?CreatedByUserId ");

                if (filter.DateFrom != null)
                    result += String.Format(" AND DateCreated > ?DateFrom ");

                if (filter.DateTo != null)
                    result += String.Format(" AND DateCreated < ?DateTo ");

                if (filter.LeadStatuses != null && filter.LeadStatuses.Length > 0)
                {
                    result += " AND LeadStatusId IN ( 0 ";

                    foreach (var statusId in filter.LeadStatuses)
                    {
                        result += String.Format(" , {0} ", statusId);
                    }

                    result += " ) ";
                }
            }

            result += " ORDER BY LeadStatusId, DateCreated DESC ";

            return result;
        }

        public void UpdateNullable()
        {
            if (AssignedToUser == 0) AssignedToUser = null;
            if (LeadSourceId == 0) LeadSourceId = null;
            if (CreatedByUserId == 0) CreatedByUserId = null;
            if (PhoneCallId == 0) PhoneCallId = null;
            if (PhoneSmsId == 0) PhoneSmsId = null;
            if (WebFormId == 0) WebFormId = null;
        }

    }
}
