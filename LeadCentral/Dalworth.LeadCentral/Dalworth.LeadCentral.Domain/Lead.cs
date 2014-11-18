using System;
using System.Collections.Generic;
using System.Data;

using Dalworth.Common.Data;
using Dalworth.Common.SDK;


namespace Dalworth.LeadCentral.Domain
{
    public partial class Lead
    {
        public List<QbInvoice> QbInvoices { get; set; }

        #region Properties 

        public Source RelatedSource { get; private set; }
        public Campaign RelatedCampaign { get; private set; }
        public BusinessPartner RelatedBusinessPartner { get; private set; }
        public decimal Amount { get; set; }

        public string CampaignStr
        {
            get { return RelatedCampaign != null ? RelatedCampaign.CampaignName : string.Empty; }
        }

        public string BusinessPartnerStr
        {
            get { return RelatedBusinessPartner != null ? RelatedBusinessPartner.PartnerName : string.Empty; }
        }

        public string PhoneStr
        {
            get
            {
                var phone = string.Empty;

                if (RelatedSource == null || (RelatedSource.RelatedPhoneCall == null && RelatedSource.RelatedPhoneSms == null))
                    return phone;

                if (RelatedSource.RelatedPhoneCall != null)
                    phone = RelatedSource.RelatedPhoneCall.FromPhone;
                else if (RelatedSource.RelatedPhoneSms != null)
                    phone = RelatedSource.RelatedPhoneSms.FromPhone;
                try
                {
                    var phoneNum = StringUtil.ExtractLastDigits(phone, 10);
                    return string.Format("{0:(###) ###-####}", Int64.Parse(phoneNum));
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

        public string StatusStr
        {
            get { return ((LeadStatusEnum)LeadStatusId).ToString(); }
        }

        public string DateCreatedStr
        {
            get { return string.Format("{0:g}", DateCreated); }
        }

        public bool IsRealLead
        {
            get { return (LeadStatusId == (int)LeadStatusEnum.Pending || LeadStatusId == (int)LeadStatusEnum.Converted); }
        }

        public string Name
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        #endregion 

        #region Constructor 

        public Lead()
        {
            QbInvoices = new List<QbInvoice>();
        }

        #endregion 

        #region GetFullLeads

        private const String SqlSelectFullLeads = @"
        SELECT l.*, s.*, phc.*, phs.*, web.*, u.*, c.*, bp.*
        FROM Lead l
        LEFT OUTER JOIN `Source` s ON s.Id = l.SourceId
         LEFT OUTER JOIN `PhoneCall` phc ON phc.Id = s.PhoneCallId
         LEFT OUTER JOIN `PhoneSms` phs ON phs.Id = s.PhoneSmsId
         LEFT OUTER JOIN `WebForm` web ON web.Id = s.WebFormId
         LEFT OUTER JOIN `User` u ON u.Id = s.UserId
         LEFT OUTER JOIN `Campaign` c ON c.Id = l.CampaignId
         LEFT OUTER JOIN `BusinessPartner` bp ON bp.Id = l.BusinessPartnerId
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

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var lead = Load(dataReader);

                        var count = FieldsCount;
                        
                        lead.RelatedSource = Source.Load(dataReader, count);
                        count += Source.FieldsCount;

                        if (!dataReader.IsDBNull(count))
                        {
                            lead.RelatedSource.RelatedPhoneCall = PhoneCall.Load(dataReader, count);
                        }
                        count += PhoneCall.FieldsCount;

                        if (!dataReader.IsDBNull(count))
                            lead.RelatedSource.RelatedPhoneSms = PhoneSms.Load(dataReader, count);
                        count += PhoneSms.FieldsCount;

                        if (!dataReader.IsDBNull(count))
                            lead.RelatedSource.RelatedWebForm = WebForm.Load(dataReader, count);
                        count += WebForm.FieldsCount;

                        if (!dataReader.IsDBNull(count))
                            lead.RelatedSource.RelatedUser = User.Load(dataReader, count);
                        count += User.FieldsCount;

                        if (!dataReader.IsDBNull(count))
                            lead.RelatedCampaign = Campaign.Load(dataReader, count);
                        count += Campaign.FieldsCount;

                        if (!dataReader.IsDBNull(count))
                            lead.RelatedBusinessPartner = BusinessPartner.Load(dataReader, count);

                        result.Add(lead);
                    }
                }

                return result;
            }
        }

        #endregion

        #region GetLeadCount

        private const String SqlSelectLeadsCount = @"
        SELECT COUNT(*)
        FROM Lead l
        LEFT OUTER JOIN `Source` s ON s.Id = l.SourceId
        LEFT OUTER JOIN `PhoneCall` phc ON phc.Id = s.PhoneCallId
        LEFT OUTER JOIN `PhoneSms` phs ON phs.Id = s.PhoneSmsId
        LEFT OUTER JOIN `WebForm` web ON web.Id = s.WebFormId
        LEFT OUTER JOIN `User` u ON u.Id = s.UserId
        LEFT OUTER JOIN `Campaign` c ON c.Id = l.CampaignId
        LEFT OUTER JOIN `BusinessPartner` bp ON bp.Id = l.BusinessPartnerId
        WHERE 1=1
        ";

        public static int Count(LeadFilter filter, IDbConnection connection)
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

        #endregion

        #region BuildFilterCommand and Sql

        private static IDbCommand BuildFilterCommand(string sql, LeadFilter filter, IDbConnection connection)
        {
            var dbCommand = Database.PrepareCommand(sql, connection);

            if (filter != null)
            {
                if (filter.LeadId != null)
                {
                    Database.PutParameter(dbCommand, "?LeadId", filter.LeadId.Value);
                } else
                {
                    if (filter.PhoneCallId > 0)
                        Database.PutParameter(dbCommand, "?PhoneCallId", filter.PhoneCallId);

                    if (filter.PhoneSmsId > 0)
                        Database.PutParameter(dbCommand, "?PhoneSmsId", filter.PhoneSmsId);

                    if (filter.SalesRepId > 0)
                        Database.PutParameter(dbCommand, "?SalesRepId", filter.SalesRepId);

                    if (filter.BusinessPartnerId > 0)
                        Database.PutParameter(dbCommand, "?BusinessPartnerId", filter.BusinessPartnerId);

                    if (filter.CampaignId > 0)
                        Database.PutParameter(dbCommand, "?CampaignId", filter.CampaignId);

                    if (filter.TrackingPhoneId > 0)
                        Database.PutParameter(dbCommand, "?TrackingPhoneId", filter.TrackingPhoneId);

                    if (filter.DateCreatedFrom != null)
                        Database.PutParameter(dbCommand, "?DateCreatedFrom", filter.DateCreatedFrom);

                    if (filter.DateCreatedTo != null)
                        Database.PutParameter(dbCommand, "?DateCreatedTo", filter.DateCreatedTo);
                }
            }

            return dbCommand;
        }

        private static string BuildFilterSql(string baseSql, LeadFilter filter)
        {
            var result = baseSql;

            if (filter != null)
            {
                if (filter.LeadId != null)
                    return baseSql + " AND l.Id = ?LeadId ";

                if (filter.PhoneCallId > 0)
                    result += String.Format(" AND s.PhoneCallId = ?PhoneCallId ");

                if (filter.PhoneSmsId > 0)
                    result += String.Format(" AND s.PhoneSmsId = ?PhoneSmsId ");

                if (filter.SalesRepId > 0)
                    result += String.Format(" AND bp.SalesRepId = ?SalesRepId ");

                if (filter.BusinessPartnerId > 0)
                    result += String.Format(" AND l.BusinessPartnerId = ?BusinessPartnerId ");

                if (filter.CampaignId > 0)
                    result += String.Format(" AND l.CampaignId = ?CampaignId ");

                if (filter.DateCreatedFrom != null)
                    result += String.Format(" AND l.DateCreated > ?DateCreatedFrom ");

                if (filter.DateCreatedTo != null)
                    result += String.Format(" AND l.DateCreated < ?DateCreatedTo ");

                if (filter.TrackingPhoneId > 0)
                    result += String.Format(" AND ( phc.TrackingPhoneId = ?TrackingPhoneId OR phs.TrackingPhoneId = ?TrackingPhoneId)");

                if (filter.CallerNumber != null)
                {
                    result += String.Format(" AND ( phc.FromPhone LIKE '%{0}%' OR phs.FromPhone LIKE '%{0}%')", StringUtil.ExtractDigits(filter.CallerNumber));
                }

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

        #endregion

        #region FindByCallId

        public static Lead FindByCallId(int phoneCallId, IDbConnection connection)
        {
            var filter = new LeadFilter {PhoneCallId = phoneCallId};
            var leads = GetFullLeads(filter, connection);
            
            if (leads != null && leads.Count > 0)
                return leads[0];

            return null;
        }

        #endregion

        #region FindBySmsId

        public static Lead FindBySmsId(int phoneSmsId, IDbConnection connection)
        {
            var filter = new LeadFilter { PhoneSmsId = phoneSmsId };
            var leads = GetFullLeads(filter, connection);

            if (leads != null && leads.Count > 0)
                return leads[0];

            return null;
        }

        #endregion

        #region GetPhoneLeadsByPeriod

        private const String SqlSelectPhoneLeadsByPeriod = @"
        SELECT l.*, s.*, phc.*, c.*, bp.*, u.*
        FROM Lead l
        INNER JOIN Source s ON s.Id = l.SourceId
        INNER JOIN PhoneCall phc ON phc.Id = s.PhoneCallId
        LEFT OUTER JOIN Campaign c ON c.Id = l.CampaignId
        LEFT OUTER JOIN BusinessPartner bp ON bp.Id = l.BusinessPartnerId
        LEFT OUTER JOIN User u ON u.Id = bp.SalesRepId
        WHERE l.DateCreated >= ?StartDate AND l.DateCreated <= ?EndDate
        ORDER BY u.Id, bp.Id, c.Id, l.DateCreated
        ";

        public static List<Lead> FindPhoneLeadsByPeriod(DateTime startDate, DateTime endDate, IDbConnection connection)
        {
            using (var dbCommand = Database.PrepareCommand(SqlSelectPhoneLeadsByPeriod, connection))
            {
                var result = new List<Lead>();

                Database.PutParameter(dbCommand, "?StartDate", startDate);
                Database.PutParameter(dbCommand, "?EndDate", endDate); 
                
                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var lead = Load(dataReader);

                        var count = FieldsCount;

                        lead.RelatedSource = Source.Load(dataReader, count);
                        count += Source.FieldsCount;

                        lead.RelatedSource.RelatedPhoneCall = PhoneCall.Load(dataReader, count);
                        count += PhoneCall.FieldsCount;

                        if (!dataReader.IsDBNull(count))
                            lead.RelatedCampaign = Campaign.Load(dataReader, count);
                        count += Campaign.FieldsCount;

                        if (!dataReader.IsDBNull(count))
                            lead.RelatedCampaign.RelatedBusinessPartner = BusinessPartner.Load(dataReader, count);
                        count += BusinessPartner.FieldsCount;

                        if (!dataReader.IsDBNull(count))
                            lead.RelatedCampaign.RelatedBusinessPartner.RelatedSalesRep = User.Load(dataReader, count);

                        result.Add(lead);
                    }
                }

                return result;
            }
        }

        #endregion

        #region GetInvoicedLeads

        private const String SqlSelectInvoicedLeadsByPeriod = @"
        SELECT DISTINCT l.*, c.*, bp.*, u.*
          FROM Lead l
             INNER JOIN QbInvoice i ON i.LeadId = l.Id
             LEFT OUTER JOIN Campaign c ON c.Id = l.CampaignId
             LEFT OUTER JOIN BusinessPartner bp ON bp.Id = l.BusinessPartnerId
             LEFT OUTER JOIN User u ON u.Id = bp.SalesRepId
          WHERE l.DateCreated >= ?StartDate AND l.DateCreated <= ?EndDate
             ORDER BY u.Id, bp.Id, c.Id, l.DateCreated
        ";

        public static List<Lead> GetInvoicedLeads(DateTime startDate, DateTime endDate, int salesRepId, IDbConnection connection)
        {
            using (var dbCommand = Database.PrepareCommand(SqlSelectInvoicedLeadsByPeriod, connection))
            {
                var result = new List<Lead>();

                Database.PutParameter(dbCommand, "?StartDate", startDate);
                Database.PutParameter(dbCommand, "?EndDate", endDate);

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var lead = Load(dataReader);

                        var count = FieldsCount;

                        if (!dataReader.IsDBNull(count))
                            lead.RelatedCampaign = Campaign.Load(dataReader, count);
                        count += Campaign.FieldsCount;

                        if (!dataReader.IsDBNull(count))
                            lead.RelatedCampaign.RelatedBusinessPartner = BusinessPartner.Load(dataReader, count);
                        count += BusinessPartner.FieldsCount;

                        if (!dataReader.IsDBNull(count))
                            lead.RelatedCampaign.RelatedBusinessPartner.RelatedSalesRep = User.Load(dataReader, count);

                        result.Add(lead);
                    }
                }

                return result;
            }
        }

        #endregion
    }


    public class LeadFilter
    {
        public int? LeadId { get; set; }
        public DateTime? DateCreatedFrom { get; set; }
        public DateTime? DateCreatedTo { get; set; }
        public int TrackingPhoneId { get; set; }
        public int SalesRepId { get; set; }
        public int BusinessPartnerId { get; set; }
        public int CampaignId { get; set; }
        public int[] LeadStatuses { get; set; }
        public string CallerNumber { get; set; }
        public int PhoneCallId { get; set; }
        public int PhoneSmsId { get; set; }
    }
}
      