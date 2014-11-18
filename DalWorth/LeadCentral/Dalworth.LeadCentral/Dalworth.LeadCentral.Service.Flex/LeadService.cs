using System;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service.Flex
{
    public class LeadService
    {
        public Lead GetLead(string ticket, int leadId)
        {
            return Service.LeadService.GetLead(ticket, leadId);
        }

        public Lead[] GetLeads(string ticket, LeadFilter filter)
        {
            return Service.LeadService.GetLeads(ticket, filter).ToArray();
        }

        public Lead[] GetLeadsLimit(string ticket, LeadFilter filter, int offset, int limit)
        {
            return Service.LeadService.GetLeadsLimit(ticket, filter, offset, limit).ToArray();
        }

        public int GetLeadsCount(string ticket, LeadFilter filter)
        {
            return Service.LeadService.GetLeadsCount(ticket, filter);
        }

        public Lead[] GetByLeadSourcesAndDatePeriod(string ticket, LeadSource[] leadSources, DateTime? startDate, DateTime? endDate)
        {
            return Service.LeadService.GetByLeadSourcesAndDatePeriod(ticket, leadSources, startDate, endDate).ToArray();
        }

        public Lead Save(string ticket, Lead lead)
        {
            lead.UpdateNullable();
            return Service.LeadService.Save(ticket, lead);
        }

        public AmountSummary GetSummaryByLeadIds(string ticket, int[] leadIds)
        {
            return Service.LeadService.GetSummaryByLeadIds(ticket, leadIds);
        }

        public LeadAmountSummary[] GetLeadSummariesByLeadIds(string ticket, int[] leadIds)
        {
            return Service.LeadService.GetLeadSummariesByLeadIds(ticket, leadIds).ToArray();
        }
    }
}