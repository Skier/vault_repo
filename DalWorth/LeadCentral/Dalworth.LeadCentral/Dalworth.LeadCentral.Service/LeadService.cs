using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security;
using Dalworth.LeadCentral.Notification;
using Intuit.Platform.Client.Core;
using Intuit.Sb.Cdm;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service
{
    public class LeadService
    {
        public static Lead Save(string ticket, Lead lead)
        {
            var user = ContextHelper.GetCurrentUser(ticket);

            if (user.IsBusinessPartner())
                throw new SecurityException();

            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            return Save(servmanCustomer, lead);
        }

        public static Lead Save(ServmanCustomer servmanCustomer, Lead lead)
        {
            NotifyMessage notifyMessage = null;
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                if (lead.Id > 0)
                {
                    lead.DateLastUpdated = DateTime.Now;
                    Lead.Update(lead, connection);
                }
                else
                {
                    lead.DateCreated = DateTime.Now;
                    Lead.Insert(lead, connection);

                    notifyMessage = NotificationService.GetCreateLeadMessage(servmanCustomer, lead, connection);
                }
                UpdateRelated(lead, connection);
            }

            if (notifyMessage != null && notifyMessage.To.Count > 0)
            {
                NotificationService.SendNotification(notifyMessage);
            }

            return lead;
        }

        public static Lead GetLead(string ticket, int leadId)
        {
            var user = ContextHelper.GetCurrentUser(ticket);
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            Lead lead;
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                lead = Lead.FindByPrimaryKey(leadId, connection);
                UpdateRelated(lead, connection);
            }

            if (lead.RelatedLeadSource.OwnedByUserId == user.Id || lead.RelatedLeadSource.UserId == user.Id)
                return lead;

            throw new SecurityException();
        }

        public static List<Lead> GetAll(ServmanCustomer servmanCustomer)
        {
            List<Lead> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = Lead.Find(connection);
                UpdateRelated(result, connection);
            }
            return result;
        }

        public static List<Lead> GetLeads(string ticket, LeadFilter filter)
        {
            var user = ContextHelper.GetCurrentUser(ticket);
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            if (user.IsBusinessPartner())
                filter.UserId = user.Id;

            List<Lead> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = Lead.GetFullLeads(filter, connection);
                UpdateRelated(result, connection);
            }
            return result;
        }

        public static List<Lead> GetByLeadSourcesAndDatePeriod(string ticket, LeadSource[] leadSources, DateTime? startDate, DateTime? endDate)
        {
            var user = ContextHelper.GetCurrentUser(ticket);
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            List<LeadSource> filteredLeadSources;
            if (user.IsAdmin())
            {
                filteredLeadSources = new List<LeadSource>(leadSources);
            }
            else
            {
                filteredLeadSources = new List<LeadSource>();
                foreach (var leadSource in leadSources)
                {
                    if (leadSource.OwnedByUserId == user.Id || leadSource.UserId == user.Id)
                        filteredLeadSources.Add(leadSource);
                }
            }

            List<Lead> result;
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = Lead.FindByLeadSourcesAndDatePeriod(filteredLeadSources.ToArray(), startDate, endDate, connection);
                foreach (var lead in result)
                {
                    UpdateRelated(lead, connection);
                }
            }
            return result;
        }

        private static void UpdateRelated(List<Lead> leads, IDbConnection connection)
        {
            foreach (var lead in leads)
            {
                UpdateRelated(lead, connection);
            }
        }

        private static void UpdateRelated(Lead lead, IDbConnection connection)
        {
            if (lead.PhoneCallId != null)
                lead.RelatedPhoneCall = PhoneCall.FindByPrimaryKey(lead.PhoneCallId.Value, connection);
            else if (lead.PhoneSmsId != null)
                lead.RelatedSms = PhoneSms.FindByPrimaryKey(lead.PhoneSmsId.Value, connection);
            else if (lead.WebFormId != null)
                lead.RelatedForm = LeadForm.FindByPrimaryKey(lead.WebFormId.Value, connection);
            else if (lead.LeadSourceId != null)
                lead.RelatedLeadSource = LeadSource.FindByPrimaryKey(lead.LeadSourceId.Value, connection);

            lead.RelatedQbInvoices = QbInvoice.GetByLeadId(lead.Id, connection).ToArray();
        }

        public static AmountSummary GetSummaryByLeadIds(string ticket, int[] leadIds)
        {
            var user = ContextHelper.GetCurrentUser(ticket);
            var context = ContextHelper.GetCurrentQbContext(ticket);
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            var result = new AmountSummary();

            var qbInvoices = QbInvoiceService.GetByLeadIds(ticket, leadIds);
            var invoiceIds = new List<IdType>();
            foreach (var qbInvoice in qbInvoices)
            {
                invoiceIds.Add(IdTypeUtil.ParseQbIdString(qbInvoice.QbInvoiceId));
            }

            var invoices = IdsInvoiceService.GetByIdList(context, servmanCustomer.RealmId, invoiceIds);
            if (invoices != null)
            {
                foreach (var invoice in invoices)
                {
                    if (invoice.Header != null)
                    {
                        result.SubTotalAmt += invoice.Header.SubTotalAmt;
                        result.TaxAmt += invoice.Header.TaxAmt;
                        result.TotalAmt += invoice.Header.TotalAmt;
                    }
                }
            }

            return result;
        }

        public static List<LeadAmountSummary> GetLeadSummariesByLeadIds(string ticket, int[] leadIds)
        {
            var user = ContextHelper.GetCurrentUser(ticket);
            var context = ContextHelper.GetCurrentQbContext(ticket);
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            var summaries = new List<LeadAmountSummary>();
            var summaryHash = new Hashtable();

            var qbInvoices = QbInvoiceService.GetByLeadIds(ticket, leadIds);
            var qbInvoicesHash = new Hashtable();

            foreach (var leadId in leadIds)
            {
                var summary = new LeadAmountSummary { LeadId = leadId };
                summaries.Add(summary);
                summaryHash[leadId] = summary;
            }

            var invoiceIds = new List<IdType>();
            foreach (var qbInvoice in qbInvoices)
            {
                qbInvoicesHash[qbInvoice.QbInvoiceId] = qbInvoice;
                invoiceIds.Add(IdTypeUtil.ParseQbIdString(qbInvoice.QbInvoiceId));
            }

            var invoices = IdsInvoiceService.GetByIdList(context, servmanCustomer.RealmId, invoiceIds);
            if (invoices != null)
            {
                foreach (var invoice in invoices)
                {
                    if (invoice.Header != null)
                    {
                        var qbInvoice = (QbInvoice) qbInvoicesHash[IdTypeUtil.GetQbIdString(invoice.Id)];
                        var summary = (LeadAmountSummary)summaryHash[qbInvoice.LeadId];
                        summary.IsInvoiced = true;
                        summary.SubTotalAmt += invoice.Header.SubTotalAmt;
                        summary.TaxAmt += invoice.Header.TaxAmt;
                        summary.TotalAmt += invoice.Header.TotalAmt;
                        summary.JobStatus = summary.JobStatus == null ? invoice.Header.Status : 
                                        (invoice.Header.Status != null && invoice.Header.Status.CompareTo(summary.JobStatus) == 0)
                                                ? invoice.Header.Status
                                                : "";
                    }
                }
            }

            return summaries;
        }

        public static List<Lead> GetLeadsLimit(ServmanCustomer servmanCustomer, LeadFilter filter, int? offset, int? limit)
        {
            List<Lead> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = Lead.GetFullLeads(filter, offset, limit, connection);
            }
            return result;
        }

        public static List<Lead> GetLeadsLimit(string ticket, LeadFilter filter, int? offset, int? limit)
        {
            var user = ContextHelper.GetCurrentUser(ticket);

            if (user.IsBusinessPartner())
                filter.UserId = user.Id;

            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            return GetLeadsLimit(servmanCustomer, filter, offset, limit);
        }

        public static int GetLeadsCount(string ticket, LeadFilter filter)
        {
            var user = ContextHelper.GetCurrentUser(ticket);
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            if (user.IsBusinessPartner())
                filter.UserId = user.Id;

            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                return Lead.GetLeadsCount(filter, connection);
            }
        }
    }
}
