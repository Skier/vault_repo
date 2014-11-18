using System;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service.Flex
{
    public class QbInvoiceService
    {
        public QbInvoice[] GetUnmatchedInvoicesByLead(string ticket, Lead lead, DateTime? dateFrom)
        {
            return Service.QbInvoiceService.GetUnmatchedByLead(ticket, lead, dateFrom).ToArray();
        }

        public QbInvoice[] GetInvoicesByLead(string ticket, int leadId)
        {
            return Service.QbInvoiceService.GetByLeadId(ticket, leadId).ToArray();
        }

        public void MatchToLead(string ticket, QbInvoice invoice, Lead lead)
        {
            Service.QbInvoiceService.MatchToLead(ticket, invoice, lead);
        }

        public void UnMatchFromLead(string ticket, QbInvoice invoice, Lead lead)
        {
            Service.QbInvoiceService.UnMatchFromLead(ticket, invoice, lead);
        }
    }
}