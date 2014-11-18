using System;
using Servman.Domain;
using Servman.Service;

namespace Servman.Intuit.Weborb
{
    public class LeadService
    {
        public Lead[] GetAll()
        {
            return Service.LeadService.GetAll(ContextHelper.GetCurrentCustomer()).ToArray();
        }

        public Lead[] GetLeads(LeadFilter filter)
        {
            return Service.LeadService.GetLeads(filter, ContextHelper.GetCurrentCustomer()).ToArray();
        }

        public Lead[] GetByBusinessPartnerId(int id, DateTime? startDate, DateTime? endDate)
        {
            return Service.LeadService.GetByBusinessPartnerId(id, startDate, endDate).ToArray();
        }

        public Lead[] GetBySalesRepId(int id, DateTime? startDate, DateTime? endDate)
        {
            return Service.LeadService.GetBySalesRepId(id, startDate, endDate).ToArray();
        }

        public Lead[] GetByPeriod(DateTime? startDate, DateTime? endDate)
        {
            return Service.LeadService.GetByPeriod(startDate, endDate).ToArray();
        }

        public Lead[] GetAllPending()
        {
            return Service.LeadService.GetAllPending(ContextHelper.GetCurrentCustomer()).ToArray();
        }

        public Lead Save(Lead lead)
        {
            lead.UpdateNullable();
            return Service.LeadService.Save(lead, ContextHelper.GetCurrentCustomer());
        }

        public LeadChangeHistory[] GetChangeHistory(int leadId)
        {
            return Service.LeadService.GetChangeHistory(leadId, ContextHelper.GetCurrentCustomer()).ToArray();
        }

        public Lead SaveLeadChangeHistory(Lead lead, LeadChangeHistory[] historyItems)
        {
            lead.UpdateNullable();
            return Service.LeadService.SaveLeadChangeHistory(lead, historyItems, ContextHelper.GetCurrentCustomer());
        }
    }
}