using System;
using System.Collections;
using System.Collections.Generic;
using Intuit.Sb.Cdm;
using Servman.Domain;
using IntuitCore = Intuit.Platform.Client.Core;
using Cdm = Intuit.Sb.Cdm;
using Job = Intuit.Sb.Cdm.Job;


namespace Servman.Service
{
    public class IdsJobService
    {
        public static Job Save(Job job)
        {
            var jobService = new Cdm.QB.JobService();
            if (job.Id != null && !string.IsNullOrEmpty(job.Id.Value))
            {
                job = jobService.UpdateJob(ContextHelper.GetCurrentQbContext(),
                                           ContextHelper.GetRealmId(),
                                           job);
            }
            else
            {
                job = jobService.AddJob(ContextHelper.GetCurrentQbContext(),
                                        ContextHelper.GetRealmId(),
                                        job);
            }

            return job;
        }

        public static List<Job> GetAfterDate(DateTime date)
        {
            var service = new Cdm.QB.JobService();
            var startDates = new List<object> { date };
            var itemElementName = new List<ItemsChoiceType4> { ItemsChoiceType4.StartCreatedTMS };

            var jobQuery = new JobQuery();
            jobQuery.Items = startDates.ToArray();
            jobQuery.ItemsElementName = itemElementName.ToArray();

            return service.GetJobs(ContextHelper.GetCurrentQbContext(), ContextHelper.GetCurrentCustomer().RealmId, jobQuery);
        }

        public static List<Job> GetByCustomer(Customer customer)
        {
            var service = new Cdm.QB.JobService();

            var customerIds = new List<IdType> { customer.Id };

            var jobQuery = new JobQuery();
            jobQuery.CustomerIdSet = customerIds.ToArray();

            return service.GetJobs(ContextHelper.GetCurrentQbContext(), ContextHelper.GetCurrentCustomer().RealmId, jobQuery);
        }

        public static Hashtable GetHashtableByJobsList(List<Job> jobs)
        {
            var result = new Hashtable();
            if (jobs != null)
            {
                foreach (var job in jobs)
                {
                    result[IdTypeUtil.GetQbIdString(job.Id)] = job;
                }
            }
            return result;
        }

        public static List<Job> GetByIdList(List<IdType> idList)
        {
            var service = new Cdm.QB.JobService();
            
            var result = new List<Job>();
            foreach (var id in idList)
            {
                Job job = null;
                try
                {
                    job = service.FindById(ContextHelper.GetCurrentQbContext(),
                                                ContextHelper.GetCurrentCustomer().RealmId, id);
                }
                catch (Exception){}
                
/*
                {
                    throw new DalworthException("Job with Id " + IdTypeUtil.GetQbIdString(id) + " doesn't exists in the QB IPP database.");
                }
*/

                if (job != null)
                    result.Add(job);
            }
            return result;
        }

/*
        public static List<Job> GetByIdList(List<IdType> idList)
        {
            var service = new IntuitCore.IDS.JobService();

            var idSets = new List<IdSet> { new IdSet { Id = idList.ToArray() } };

            var itemElementName = new List<ItemsChoiceType4>();
            itemElementName.Add(ItemsChoiceType4.ListIdSet);

            var jobQuery = new JobQuery
            {
                Items = idSets.ToArray(),
                ItemsElementName = itemElementName.ToArray()
            };

            return service.GetJobs(ContextHelper.GetCurrentQbContext(), ContextHelper.GetCurrentCustomer().RealmId, jobQuery);
        }
*/

        public static AmountSummary GetSummaryByIdsJobId(string jobId)
        {
            var result = new AmountSummary();

            var qbJobIds = new List<IdType> {IdTypeUtil.ParseQbIdString(jobId)};

            var invoices = IdsInvoiceService.GetByJobList(qbJobIds);
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
    }
}
