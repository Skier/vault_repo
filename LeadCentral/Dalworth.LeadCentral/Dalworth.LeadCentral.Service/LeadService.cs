using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using Intuit.Sb.Cdm;
using Dalworth.Common.Data;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service
{
    public class LeadService
    {
        #region Create

        public static void Create(Lead lead, IDbConnection connection)
        {
            var user = ContextHelper.GetCurrentUser();

            if (user.IsBusinessPartner)
                throw new SecurityException();

            var source = new Source
                             {
                                 UserId = user.Id
                             };
           
            Source.Insert(source, connection);
            lead.SourceId = source.Id;
            lead.DateCreated = DateTime.Now;
            Lead.Insert(lead, connection);
        }

        #endregion

        #region FindByPrimaryKey

        public static Lead FindByPrimaryKey(int leadId, User currentUser, IDbConnection connection, bool withInvoices = true)
        {
            var filter = new LeadFilter { LeadId = leadId };
            var leads = Find(filter, currentUser, connection);

            if (leads.Count != 1)
                throw new DataNotFoundException("Lead Not Found ID" + leadId);

            var result = leads[0];

            if (withInvoices)
            {
                result.QbInvoices = QbInvoiceService.FindByLeadId(leadId, connection);
                result.Amount = result.QbInvoices.Sum(qbInvoice => qbInvoice.Amount);
            }

            return result;
        }

        #endregion

        #region Find

        public static List<Lead> Find(LeadFilter filter, User currentUser, IDbConnection connection)
        {

            if (currentUser.IsBusinessPartner)
                filter.BusinessPartnerId = currentUser.BusinessPartnerId != null ? currentUser.BusinessPartnerId.Value : Int32.MaxValue;

            List<Lead> result = Lead.GetFullLeads(filter, connection);
            UpdateLeadInvoices(result, connection);
            
            foreach (var lead in result)
            {
                lead.Amount = 0;
                foreach (var qbInvoice in lead.QbInvoices)
                {
                    lead.Amount += qbInvoice.Amount;
                }
            }

            return result;
        }

        #endregion

        #region FindPhoneLeadsByPeriod

        public static List<Lead> FindPhoneLeadsByPeriod(DateTime startDate, DateTime endDate, IDbConnection connection)
        {
            return Lead.FindPhoneLeadsByPeriod(startDate, endDate, connection);
        }

        #endregion

        #region GetChart

        public static ChartResult GetChart(string range, IDbConnection connection)
        {
            switch (range)
            {
                case "day":
                    return DayChart(connection);
                case "week":
                    return WeekChart(connection);
                case "month":
                    return MonthChart(connection);
                case "lastmonth":
                    return LastMonthChart(connection);
                case "year":
                    return YearChart(connection);
                default:
                    return DayChart(connection);
            }
        }

        public static ChartResult DayChart(IDbConnection connection)
        {
            var now = DateTime.Now;
            var currentDate = new DateTime(now.Year, now.Month, now.Day);

            var result = new ChartResult();

            var key = 1;
            while (currentDate < now)
            {
                result.Xaxis.Add(key.ToString(), currentDate.Hour.ToString());
                result.CompletedLeads.Data.Add(key.ToString(), CalculateCountCompletedLeadsByTimePeriod(currentDate, currentDate.AddHours(1), connection));
                result.TotalLeads.Data.Add(key.ToString(), FindLeadsByTimePeriod(currentDate, currentDate.AddHours(1), connection));

                currentDate = currentDate.AddHours(1);
                key++;
            }

            return result;
        }

        public static ChartResult WeekChart(IDbConnection connection)
        {
            var now = DateTime.Now;
            var currentDate = now.AddDays(-(int)now.DayOfWeek);

            var result = new ChartResult();

            var key = 1;
            while (currentDate <= now)
            {
                result.Xaxis.Add(key.ToString(), currentDate.DayOfWeek.ToString());
                result.CompletedLeads.Data.Add(key.ToString(), CalculateCountCompletedLeadsByTimePeriod(currentDate, currentDate.AddDays(1), connection));
                result.TotalLeads.Data.Add(key.ToString(), FindLeadsByTimePeriod(currentDate, currentDate.AddDays(1), connection));
                
                currentDate = currentDate.AddDays(1);
                key++;
            }

            return result;
        }

        public static ChartResult MonthChart(IDbConnection connection)
        {
            var now = DateTime.Now;
            var currentDate = new DateTime(now.Year, now.Month, 1);

            var result = new ChartResult();

            var key = 1;
            while (currentDate < now)
            {
                result.Xaxis.Add(key.ToString(), currentDate.Day.ToString());
                result.CompletedLeads.Data.Add(key.ToString(), CalculateCountCompletedLeadsByTimePeriod(currentDate, currentDate.AddDays(1), connection));
                result.TotalLeads.Data.Add(key.ToString(), FindLeadsByTimePeriod(currentDate, currentDate.AddDays(1), connection));

                currentDate = currentDate.AddDays(1);
                key++;
            }

            return result;
        }

        public static ChartResult LastMonthChart(IDbConnection connection)
        {
            var now = DateTime.Now;
            var endDate = new DateTime(now.Year, now.Month, 1);
            var currentDate = endDate.AddMonths(-1);

            var result = new ChartResult();

            var key = 1;
            while (currentDate < endDate)
            {
                result.Xaxis.Add(key.ToString(), currentDate.Day.ToString());
                result.CompletedLeads.Data.Add(key.ToString(), CalculateCountCompletedLeadsByTimePeriod(currentDate, currentDate.AddDays(1), connection));
                result.TotalLeads.Data.Add(key.ToString(), FindLeadsByTimePeriod(currentDate, currentDate.AddDays(1), connection));

                currentDate = currentDate.AddDays(1);
                key++;
            }

            return result;
        }

        public static ChartResult YearChart(IDbConnection connection)
        {
            var now = DateTime.Now;
            var currentDate = new DateTime(now.Year, 1, 1);

            var result = new ChartResult();

            var key = 1;
            while (currentDate < now)
            {
                result.Xaxis.Add(key.ToString(), currentDate.Month.ToString());
                result.CompletedLeads.Data.Add(key.ToString(), CalculateCountCompletedLeadsByTimePeriod(currentDate, currentDate.AddMonths(1), connection));
                result.TotalLeads.Data.Add(key.ToString(), FindLeadsByTimePeriod(currentDate, currentDate.AddMonths(1), connection));

                currentDate = currentDate.AddMonths(1);
                key++;
            }

            return result;
        }

        #endregion

        #region FindLeadsByTimePeriod

        private static decimal FindLeadsByTimePeriod(DateTime startTime, DateTime endTime, IDbConnection connection)
        {
            var filter = new LeadFilter {DateCreatedFrom = startTime, DateCreatedTo = endTime};

            var allStatuses = new List<int>
                                  {
                                      (int) LeadStatusEnum.New,
                                      (int) LeadStatusEnum.Pending,
                                      (int) LeadStatusEnum.Converted,
                                      (int) LeadStatusEnum.Cancelled
                                  };

            filter.LeadStatuses = allStatuses.ToArray();

            return Count(filter, connection);
        }

        #endregion

        #region CalculateCountCompletedLeadsByTimePeriod

        public static decimal CalculateCountCompletedLeadsByTimePeriod(DateTime startTime, DateTime endTime, IDbConnection connection)
        {
            var filter = new LeadFilter {DateCreatedFrom = startTime, DateCreatedTo = endTime};

            var completedStatuses = new List<int> { (int)LeadStatusEnum.Converted };

            filter.LeadStatuses = completedStatuses.ToArray();

            return Count(filter, connection);
        }

        #endregion

        #region 

        public static List<Lead> FindInvoicedLeads(DateTime startDate, DateTime endDate, int salesRepId, IDbConnection connection)
        {
            var result = Lead.GetInvoicedLeads(startDate, endDate, salesRepId, connection);
            UpdateLeadInvoices(result, connection);
           
            return result;
        }

        #endregion

        #region Private

        private static void UpdateLeadInvoices(List<Lead> leads, IDbConnection connection)
        {
            //TODO: Move this to service
            return;

            var context = ContextHelper.GetCurrentQbContext();
            var servmanCustomer = ContextHelper.GetCurrentCustomer();

            var leadIds = new List<int>();
            var leadsHash = new Hashtable();
            foreach (var lead in leads)
            {
                leadIds.Add(lead.Id);
                leadsHash[lead.Id] = lead;
            }

            var qbInvoices = QbInvoiceService.GetByLeadIds(leadIds.ToArray(), connection);
            var qbInvoicesByQbIdHash = new Hashtable();

            var qbInvoiceIds = new List<IdType>();
            foreach (var qbInvoice in qbInvoices)
            {
                qbInvoicesByQbIdHash[qbInvoice.QbInvoiceId] = qbInvoice;
                qbInvoiceIds.Add(IdTypeUtil.ParseQbIdString(qbInvoice.QbInvoiceId));

                var lead = (Lead)leadsHash[qbInvoice.LeadId];
                lead.QbInvoices.Add(qbInvoice);
            }

            var invoices = IdsInvoiceService.GetByIdList(context, servmanCustomer.RealmId, qbInvoiceIds);
            if (invoices == null)
                return;

            foreach (var invoice in invoices)
            {
                if (invoice.Header == null)
                    continue;

                var qbInvoice = (QbInvoice)qbInvoicesByQbIdHash[IdTypeUtil.GetQbIdString(invoice.Id)];
                qbInvoice.Amount = invoice.Header.SubTotalAmt;
                qbInvoice.TaxAmount = invoice.Header.TaxAmt;
                qbInvoice.TotalAmount = invoice.Header.TotalAmt;
                qbInvoice.InvoiceStatus = invoice.Header.Status;
                qbInvoice.RelatedIdsInvoice = invoice;
            }
        }

        private static int Count(LeadFilter filter, IDbConnection connection)
        {
            var user = ContextHelper.GetCurrentUser();

            if (user.IsBusinessPartner )
                filter.BusinessPartnerId = user.BusinessPartnerId.Value;

            return Lead.Count(filter, connection);
        }

        #endregion
    }

    public class ChartResult
    {
        #region Constructor 

        public ChartResult()
        {
            Xaxis = new Dictionary<string, string>();
            TotalLeads = new ChartSerie{Label = "Total Leads", Data = new Dictionary<string, decimal>()};
            CompletedLeads = new ChartSerie { Label = "Completed Leads", Data = new Dictionary<string, decimal>() };
        }

        #endregion 

        #region Properties 

        public Dictionary<string, string> Xaxis { get; set; }
        public ChartSerie TotalLeads { get; set; }
        public ChartSerie CompletedLeads { get; set; }

        public string AxisStr
        {
            get
            {
                var result = "[";
                foreach (var point in Xaxis)
                {
                    result += result == "[" ? "" : ",";
                    result += string.Format("['{0}', '{1}']", point.Key, point.Value);
                }
                result += "]";

                return result;
            }
        }

        public string TotalLeadsStr
        {
            get
            {
                var result = "{'label':'Total Leads','data':[";
                foreach (var point in TotalLeads.Data)
                {
                    result += result == "[" ? "" : ",";
                    result += string.Format("['{0}', {1}]", point.Key, point.Value);
                }
                result += "]}";

                return result;
            }
        }

        public string CompletedLeadsStr
        {
            get
            {
                var result = "{'label':'Completed Leads','data':[";
                foreach (var point in CompletedLeads.Data)
                {
                    result += result == "[" ? "" : ",";
                    result += string.Format("['{0}', {1}]", point.Key, point.Value);
                }
                result += "]}";

                return result;
            }
        }

        #endregion 
    }

    public class ChartSerie
    {
        public string Label { get; set; }
        public Dictionary<string, decimal> Data { get; set; }
    }

   
}

