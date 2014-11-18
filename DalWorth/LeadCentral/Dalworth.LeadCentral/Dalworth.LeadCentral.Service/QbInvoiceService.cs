using System;
using System.Collections.Generic;
using Intuit.Platform.Client.Core;
using Intuit.Sb.Cdm;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.SDK;
using IntuitCore = Intuit.Platform.Client.Core;

namespace Dalworth.LeadCentral.Service
{
    public class QbInvoiceService
    {
        public static List<QbInvoice> GetUnmatchedByLead(string ticket, Lead lead, DateTime? dateFrom)
        {
            var context = ContextHelper.GetCurrentQbContext(ticket);
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            var result = new List<QbInvoice>();

            var date = dateFrom ?? lead.DateCreated;

            var invoices = IdsInvoiceService.GetAfterDate(context, servmanCustomer.RealmId, date);
            if (invoices == null)
                return result;

            var customerIds = new List<IdType>();
            foreach (var invoice in invoices)
            {
                customerIds.Add(invoice.Header.CustomerId);
            }
            var customersHash = IdsCustomerService.GetCustomersHashtableByIdList(context, servmanCustomer.RealmId, customerIds);

            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                foreach (var invoice in invoices)
                {
                    var qbInvoice = QbInvoice.GetByQbJobRecordId(IdTypeUtil.GetQbIdString(invoice.Id), connection);
                    if (qbInvoice == null)
                    {
                        qbInvoice = new QbInvoice
                        {
                            QbInvoiceId = IdTypeUtil.GetQbIdString(invoice.Id),
                            IsMatched = false,
                            RelatedIdsInvoice = invoice,
                            RelatedIdsCustomer = customersHash[IdTypeUtil.GetQbIdString(invoice.Header.CustomerId)] as Customer
                        };
                        SetInvoiceMatchLevel(qbInvoice, lead);
                        result.Add(qbInvoice);
                    }
                }
            }

            return result;
        }

        public static List<QbInvoice> GetByLeadId(string ticket, int leadId)
        {
            var context = ContextHelper.GetCurrentQbContext(ticket);
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            var result = QbInvoice.GetByLeadId(leadId, ServmanCustomerService.GetConnection(servmanCustomer));

            if (result == null || result.Count == 0)
                return result;

            var invoiceIds = new List<IdType>();
            foreach (var qbInvoice in result)
            {
                invoiceIds.Add(IdTypeUtil.ParseQbIdString(qbInvoice.QbInvoiceId));
            }
            var invoices = IdsInvoiceService.GetByIdList(context, servmanCustomer.RealmId, invoiceIds);
            var invoicesHashtable = IdsInvoiceService.GetInvoicesHashtable(invoices);

            var customerIds = new List<IdType>();
            foreach(Invoice invoice in invoices)
            {
                customerIds.Add(invoice.Header.CustomerId);
            }
            var customersHashtable = IdsCustomerService.GetCustomersHashtableByIdList(context, servmanCustomer.RealmId, customerIds);

            foreach (var qbInvoice in result)
            {
                qbInvoice.RelatedIdsInvoice = invoicesHashtable[qbInvoice.QbInvoiceId] as Invoice;
                if (qbInvoice.RelatedIdsInvoice != null)
                {
                    qbInvoice.IsMatched = true;
                    qbInvoice.Amount = qbInvoice.RelatedIdsInvoice.Header.SubTotalAmt;
                    qbInvoice.TaxAmount = qbInvoice.RelatedIdsInvoice.Header.TaxAmt;
                    qbInvoice.TotalAmount = qbInvoice.RelatedIdsInvoice.Header.TotalAmt;
                    qbInvoice.RelatedIdsCustomer = 
                        customersHashtable[IdTypeUtil.GetQbIdString(qbInvoice.RelatedIdsInvoice.Header.CustomerId)] as Customer;
                }
            }

            return result;
        }

        private static void SetInvoiceMatchLevel(QbInvoice qbInvoice, Lead lead)
        {
            bool phoneMatched = false;
            bool firstNameMatched = false;
            bool lastNameMatched = false;

            qbInvoice.MatchLevel = 0;

            if (lead == null)
                return;

            if (qbInvoice.RelatedIdsInvoice == null)
                return;

            if (qbInvoice.RelatedIdsCustomer != null)
            {
                var parentCustomer = qbInvoice.RelatedIdsCustomer;
                if (parentCustomer.Phone != null)
                {
                    foreach (var phone in parentCustomer.Phone)
                    {
                        if (StringUtil.ExtractLastSevenDigits(lead.Phone) == StringUtil.ExtractLastSevenDigits(phone.FreeFormNumber))
                        {
                            phoneMatched = true;
                            break;
                        }
                    }
                }
            }

            if (qbInvoice.RelatedIdsCustomer != null 
                && qbInvoice.RelatedIdsCustomer.GivenName != null 
                && lead.FirstName != null
                && qbInvoice.RelatedIdsCustomer.GivenName.Trim().ToUpper() == lead.FirstName.Trim().ToUpper())
                firstNameMatched = true;

            if (qbInvoice.RelatedIdsCustomer != null
                && qbInvoice.RelatedIdsCustomer.FamilyName != null
                && lead.LastName != null
                && qbInvoice.RelatedIdsCustomer.FamilyName.Trim().ToUpper() == lead.LastName.Trim().ToUpper())
                lastNameMatched = true;
            
            if (lastNameMatched && !firstNameMatched && !phoneMatched)
                qbInvoice.MatchLevel = 1;

            if (lastNameMatched && firstNameMatched && !phoneMatched)
                qbInvoice.MatchLevel = 2;

            if (!lastNameMatched && phoneMatched)
                qbInvoice.MatchLevel = 3;

            if (lastNameMatched && phoneMatched)
                qbInvoice.MatchLevel = 4;
        }

        public static void MatchToLead(string ticket, QbInvoice qbInvoice, Lead lead)
        {
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                qbInvoice.LeadId = lead.Id;
                QbInvoice.Insert(qbInvoice, connection);

                if (lead.LeadStatusId == (int)LeadStatusEnum.Converted)
                    return;

                lead.LeadStatusId = (int)LeadStatusEnum.Converted;
                lead.UpdateNullable();
                Lead.Update(lead, connection);
            }
        }

        public static List<QbInvoice> GetByLeadIds(string ticket, int[] leadIds)
        {
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                return QbInvoice.GetByLeadIds(leadIds, connection);
            }
        }

        public static void UnMatchFromLead(string ticket, QbInvoice qbInvoice, Lead lead)
        {
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                QbInvoice.Delete(qbInvoice, connection);
            }
        }
    }
}
