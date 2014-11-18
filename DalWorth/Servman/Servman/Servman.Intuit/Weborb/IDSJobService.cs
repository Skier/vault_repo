using System;
using Intuit.Sb.Cdm;
using Servman.Domain;
using Job = Servman.Domain.Job;

namespace Servman.Intuit.Weborb
{
    public class IDSJobService
    {
        public Job[] GetUnmatchedByLead(Lead lead, DateTime? dateFrom)
        {
            return Service.JobService.GetUnmatchedByLead(lead, dateFrom).ToArray();
        }

        public Job[] GetAllByLead(Lead lead)
        {
            return Service.JobService.GetAllByLead(lead).ToArray();
        }

        public Job[] GetByCustomer(Customer customer)
        {
            return Service.JobService.GetByCustomer(customer).ToArray();
        }

    }
}