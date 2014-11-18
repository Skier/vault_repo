using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Intuit.Sb.Cdm;
using Servman.Domain;

namespace Servman.Service
{
    public class LeadService
    {
        public static Lead Save(Lead lead)
        {
            return Save(lead, ContextHelper.GetCurrentCustomer());
        }

        public static Lead Save(Lead lead, ServmanCustomer servmanCustomer)
        {
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                if (lead.DateContacted == null)
                    lead.DateContacted = DateTime.Now;
                lead.DateLastUpdated = DateTime.Now;
                Lead.Save(lead, connection);
            }
            return lead;
        }

        public static Lead GetLead(int leadId)
        {
            Lead lead;
            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                lead = Lead.FindByPrimaryKey(leadId, connection);
                UpdateRelated(lead, connection);
            }
            return lead;
        }

        public static List<Lead> GetAll()
        {
            List<Lead> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                result = Lead.Find(connection);
                foreach (var lead in result)
                {
                    UpdateRelated(lead, connection);
                }
            }
            return result;
        }

        public static List<Lead> GetLeads(LeadFilter filter)
        {
            List<Lead> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                result = Lead.GetLeads(filter, connection);
                foreach (var lead in result)
                {
                    UpdateRelated(lead, connection);
                }
            }
            return result;
        }

        public static List<Lead> GetAllPending()
        {
            List<Lead> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                result = Lead.FindAllPending(connection);
                foreach (var lead in result)
                {
                    UpdateRelated(lead, connection);
                }
            }
            return result;
        }

        public static List<Lead> GetByLeadSourcesAndDatePeriod(LeadSource[] leadSources, DateTime? startDate, DateTime? endDate)
        {
            List<Lead> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                result = Lead.FindByLeadSourcesAndDatePeriod(leadSources, startDate, endDate, connection);
                foreach (var lead in result)
                {
                    UpdateRelated(lead, connection);
                }
            }
            return result;
        }

        private static void UpdateRelated(Lead lead, IDbConnection connection)
        {
            if (lead.PhoneCallId != null)
                lead.RelatedPhoneCall = PhoneCall.FindByPrimaryKey(lead.PhoneCallId.Value, connection);
            if (lead.PhoneSmsId != null)
                lead.RelatedSms = PhoneSms.FindByPrimaryKey(lead.PhoneSmsId.Value, connection);
            if (lead.WebFormId != null)
                lead.RelatedForm = LeadForm.FindByPrimaryKey(lead.WebFormId.Value, connection);

            lead.RelatedQbInvoices = QbInvoice.GetByLeadId(lead.Id, connection).ToArray();
        }

        public static AmountSummary GetSummaryByLeadIds(int[] leadIds)
        {
            var result = new AmountSummary();

            var qbInvoices = QbInvoiceService.GetByLeadIds(leadIds);
            var invoiceIds = new List<IdType>();
            foreach (var qbInvoice in qbInvoices)
            {
                invoiceIds.Add(IdTypeUtil.ParseQbIdString(qbInvoice.QbInvoiceId));
            }

            var invoices = IdsInvoiceService.GetByIdList(invoiceIds);
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

        public static List<LeadAmountSummary> GetLeadSummariesByLeadIds(int[] leadIds)
        {
            var summaries = new List<LeadAmountSummary>();
            var summaryHash = new Hashtable();

            var qbInvoices = QbInvoiceService.GetByLeadIds(leadIds);
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

            var invoices = IdsInvoiceService.GetByIdList(invoiceIds);
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

        public static List<Lead> GetLeadsLimit(LeadFilter filter, int offset, int limit)
        {
            List<Lead> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                result = Lead.GetLeads(filter, offset, limit, connection);
                foreach (var lead in result)
                {
                    UpdateRelated(lead, connection);
                }
            }
            return result;
        }

        public static int GetLeadsCount(LeadFilter filter)
        {
            using (IDbConnection connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                return Lead.GetLeadsCount(filter, connection);
            }
        }
    }
}
