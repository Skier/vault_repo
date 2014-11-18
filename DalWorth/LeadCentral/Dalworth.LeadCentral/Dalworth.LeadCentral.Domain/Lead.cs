using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Dalworth.LeadCentral.Data;

namespace Dalworth.LeadCentral.Domain
{
    public partial class Lead
    {
        public LeadSource RelatedLeadSource { get; set; }

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
            else
            {
                return new List<Lead>();
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
SELECT l.*, ls.* FROM Lead l
     LEFT OUTER JOIN LeadSource ls on ls.Id = l.LeadSourceId
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
     LEFT OUTER JOIN LeadSource ls on ls.Id = l.LeadSourceId
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

                if (filter.UserId > 0)
                    Database.PutParameter(dbCommand, "?UserId", filter.UserId);

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
                    result += String.Format(" AND l.LeadTypeId = ?LeadTypeId ");

                if (filter.LeadSourceId > 0)
                    result += String.Format(" AND l.LeadSourceId = ?LeadSourceId ");

                if (filter.UserId > 0)
                    result += String.Format(" AND ( ls.UserId = ?UserId OR ls.OwnedByUserId = ?UserId ) ");

                if (filter.AssignedToUserId > 0)
                    result += String.Format(" AND l.AssignedToUser = ?AssignedToUser ");

                if (filter.CreatedByUserId > 0)
                    result += String.Format(" AND l.CreatedByUserId = ?CreatedByUserId ");

                if (filter.DateFrom != null)
                    result += String.Format(" AND l.DateCreated > ?DateFrom ");

                if (filter.DateTo != null)
                    result += String.Format(" AND l.DateCreated < ?DateTo ");

                if (filter.LeadStatuses != null && filter.LeadStatuses.Length > 0)
                {
                    result += " AND l.LeadStatusId IN ( 0 ";

                    foreach (var statusId in filter.LeadStatuses)
                    {
                        result += String.Format(" , {0} ", statusId);
                    }

                    result += " ) ";
                }
            }

            result += " ORDER BY l.LeadStatusId, l.DateCreated DESC ";

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

/*
        private const String SqlSelectFullLeads = @"
SELECT l.*, p.*, s.*, f.*, i.* 
  FROM Lead l
     LEFT OUTER JOIN PhoneCall p on p.Id = l.PhoneCallId
     LEFT OUTER JOIN PhoneSms s on s.Id = l.PhoneSmsId
     LEFT OUTER JOIN LeadForm f on f.Id = l.WebFormId
     LEFT OUTER JOIN QbInvoice i on i.LeadId = l.Id
";
*/

        private const String SqlSelectFullLeads = @"
SELECT l.*, p.*, s.*, f.*, ls.*
  FROM Lead l
     LEFT OUTER JOIN PhoneCall p on p.Id = l.PhoneCallId
     LEFT OUTER JOIN PhoneSms s on s.Id = l.PhoneSmsId
     LEFT OUTER JOIN LeadForm f on f.Id = l.WebFormId
     LEFT OUTER JOIN LeadSource ls on ls.Id = l.LeadSourceId
  WHERE 1=1
";
        public static List<Lead> GetFullLeads(LeadFilter filter, IDbConnection connection)
        {
            return GetFullLeads(filter, null, null, connection);
        }

        public static List<Lead> GetFullLeads(LeadFilter filter, int? offset, int? limit, IDbConnection connection)
        {
            var sql = BuildFilterSql(SqlSelectFullLeads, filter);

            if (offset != null && limit != null)
                sql += string.Format(" LIMIT {0}, {1} ", offset, limit);

            using (var dbCommand = BuildFilterCommand(sql, filter, connection))
            {
                var result = new List<Lead>();

                var leadsHashtable = new Hashtable();
                var leadIds = new List<int>();

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var lead = Load(dataReader);

                        var count = FieldsCount;
                        if (!dataReader.IsDBNull(count))
                            lead.RelatedPhoneCall = PhoneCall.Load(dataReader, FieldsCount);

                        count += PhoneCall.FieldsCount;
                        if (!dataReader.IsDBNull(count))
                            lead.RelatedSms = PhoneSms.Load(dataReader, count);

                        count += PhoneSms.FieldsCount;
                        if (!dataReader.IsDBNull(count))
                            lead.RelatedForm = LeadForm.Load(dataReader, count);

                        count += LeadForm.FieldsCount;
                        if (!dataReader.IsDBNull(count))
                            lead.RelatedLeadSource = LeadSource.Load(dataReader, count);

                        leadsHashtable[lead.Id] = lead;
                        leadIds.Add(lead.Id);

                        result.Add(lead);
                    }
                }
                var qbInvoices = QbInvoice.GetByLeadIds(leadIds.ToArray(), connection);
                var invoiceListsHashtable = new Hashtable();

                foreach (var qbInvoice in qbInvoices)
                {
                    var list = (List<QbInvoice>)invoiceListsHashtable[qbInvoice.LeadId];
                    if (list == null)
                        list = new List<QbInvoice>();

                    list.Add(qbInvoice);
                }

                foreach (var lead in result)
                {
                    var list = (List<QbInvoice>)invoiceListsHashtable[lead.Id];
                    if (list != null)
                        lead.RelatedQbInvoices = list.ToArray();
                    else
                        lead.RelatedQbInvoices = null;
                }

                return result;
            }
        }
    }

    public enum LeadTransportEnum
    {
        PhoneCall = 1,
        PhoneSms = 2,
        HtmlForm = 3,
        Direct = 4
    }

}
