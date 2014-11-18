using Servman.Domain;
using Servman.Service;

namespace Servman.Intuit.Weborb
{
    public class JobService
    {
        public Job[] GetAll()
        {
            return Service.JobService.GetAll(ContextHelper.GetCurrentCustomer()).ToArray();
        }

        public Job Save(Job project)
        {
            return Service.JobService.Save(project, ContextHelper.GetCurrentCustomer());
        }

        public void MatchToLead(Job job, Lead lead)
        {
            Service.JobService.MatchToLead(job, lead);
        }
    }
}