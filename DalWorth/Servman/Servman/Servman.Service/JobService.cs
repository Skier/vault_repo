using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Intuit.Sb.Cdm;
using Servman.Domain;
using Servman.SDK;
using Job = Servman.Domain.Job;
using IntuitCore = Intuit.Platform.Client.Core;

namespace Servman.Service
{
    public class JobService
    {
        public static List<Job> GetAll(ServmanCustomer servmanCustomer)
        {
            List<Job> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = Job.Find(connection);
            }
            return result;
        }

        public static Job Save(Job job, ServmanCustomer servmanCustomer)
        {
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                Job.Save(job, connection);
            }
            return job;
        }

        public static List<Job> GetUnmatchedByLead(Lead lead, DateTime? dateFrom)
        {
            var servmanCustomer = ContextHelper.GetCurrentCustomer();

            var result = new List<Job>();

            var date = dateFrom ?? lead.DateCreated;

            var idsJobs = IDSJobService.GetAfterDate(date);
            if (idsJobs == null)
                return result;

            var customersHash = IDSCustomerService.GetCustomersHashtableByJobList(idsJobs);

            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                foreach (var idsJob in idsJobs)
                {
                    if (idsJob.JobParentId != null)
                    {
                        var job = Job.GetByQbJobRecordId(IdTypeUtil.GetQbIdString(idsJob.Id), connection);
                        if (job == null)
                        {
                            job = new Job
                            {
                                QbJobRecordId = IdTypeUtil.GetQbIdString(idsJob.Id),
                                IsMatched = false,
                                RelatedIdsJob = idsJob,
                                RelatedIdsCustomer = customersHash[IdTypeUtil.GetQbIdString(idsJob.CustomerId)] as Customer
                            };
                            SetJobMatchLevel(job, lead);
                            result.Add(job);
                        }
                    }
                }
            }

            return result;
        }

        public static List<Job> GetAllByLead(Lead lead)
        {
            var servmanCustomer = ContextHelper.GetCurrentCustomer();
            var result = Job.GetByLead(lead, ServmanCustomerService.GetConnection(servmanCustomer));
            var ids = new List<IdType>();
            foreach (var job in result)
            {
                ids.Add(IdTypeUtil.ParseQbIdString(job.QbJobRecordId));
            }

            var qbJobs = IDSJobService.GetByIdList(ids);
            var qbJobsHashtable = IDSJobService.GetHashtableByJobsList(qbJobs);
            var qbCustomersHashtable = IDSCustomerService.GetCustomersHashtableByJobList(qbJobs);

            foreach (var job in result)
            {
                var qbJob = qbJobsHashtable[job.QbJobRecordId] as Intuit.Sb.Cdm.Job;
                job.IsMatched = true;
                job.RelatedIdsJob = qbJob;
                if (qbJob != null)
                    job.RelatedIdsCustomer = qbCustomersHashtable[IdTypeUtil.GetQbIdString(qbJob.CustomerId)] as Customer;
            }

            return result;
        }

        public static List<Job> GetByCustomer(Customer customer)
        {
            var servmanCustomer = ContextHelper.GetCurrentCustomer();

            var result = new List<Job>();
            
            var idsJobs = IDSJobService.GetByCustomer(customer);
            if (idsJobs == null)
                return result;

            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                foreach (var idsJob in idsJobs)
                {
                    if (idsJob.JobParentId.Value == customer.Id.Value || idsJob.JobParentId == null)
                    {
                        var job = Job.GetByQbJobRecordId(IdTypeUtil.GetQbIdString(idsJob.Id), connection);
                        if (job == null)
                        {
                            job = new Job();
                            job.QbJobRecordId = IdTypeUtil.GetQbIdString(idsJob.Id);
                            job.IsMatched = false;
                        } else
                        {
                            job.IsMatched = true;
                        }
                        job.RelatedIdsJob = idsJob;
                        job.RelatedIdsCustomer = customer;

                        result.Add(job);
                    }
                }
            }

            return result;
        }

        private static void SetJobMatchLevel(Job job, Lead lead)
        {
            bool phoneMatched = false;
            bool firstNameMatched = false;
            bool lastNameMatched = false;

            job.MatchLevel = 0;

            if (lead == null)
                return;

            if (job.RelatedIdsJob == null)
                return;

            if (job.RelatedIdsJob.Phone != null)
            {
                foreach (var phone in job.RelatedIdsJob.Phone)
                {
                    if (StringUtil.ExtractLastSevenDigits(lead.Phone) == StringUtil.ExtractLastSevenDigits(phone.FreeFormNumber))
                    {
                        phoneMatched = true;
                        break;
                    }
                }
            }

            if (!phoneMatched && job.RelatedIdsCustomer != null)
            {
                var parentCustomer = job.RelatedIdsCustomer;
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

            if (job.RelatedIdsJob.GivenName != null && lead.FirstName != null 
                && job.RelatedIdsJob.GivenName.Trim().ToUpper() == lead.FirstName.Trim().ToUpper())
                firstNameMatched = true;

            if (!firstNameMatched 
                && job.RelatedIdsCustomer != null 
                && job.RelatedIdsCustomer.GivenName != null 
                && lead.FirstName != null
                && job.RelatedIdsCustomer.GivenName.Trim().ToUpper() == lead.FirstName.Trim().ToUpper())
                firstNameMatched = true;

            if (job.RelatedIdsJob.FamilyName != null 
                && lead.LastName != null 
                && job.RelatedIdsJob.FamilyName.Trim().ToUpper() == lead.LastName.Trim().ToUpper())
                lastNameMatched = true;

            if (!lastNameMatched 
                && job.RelatedIdsCustomer != null
                && job.RelatedIdsCustomer.FamilyName != null
                && lead.LastName != null
                && job.RelatedIdsCustomer.FamilyName.Trim().ToUpper() == lead.LastName.Trim().ToUpper())
                lastNameMatched = true;
            
            if (lastNameMatched && !firstNameMatched && !phoneMatched)
                job.MatchLevel = 1;

            if (lastNameMatched && firstNameMatched && !phoneMatched)
                job.MatchLevel = 2;

            if (!lastNameMatched && phoneMatched)
                job.MatchLevel = 3;

            if (lastNameMatched && phoneMatched)
                job.MatchLevel = 4;
        }

        public static void MatchToLead(Job job, Lead lead)
        {
            if (job.RelatedIdsCustomer != null)
            {
                job.RelatedIdsCustomer = IDSCustomerService.Save(job.RelatedIdsCustomer);

                if (job.RelatedIdsJob != null)
                {
                    job.RelatedIdsJob.CustomerId = job.RelatedIdsCustomer.Id;
                    job.RelatedIdsJob.JobParentId = job.RelatedIdsCustomer.Id;

                    job.RelatedIdsJob = IDSJobService.Save(job.RelatedIdsJob);

                    job.QbJobRecordId = IdTypeUtil.GetQbIdString(job.RelatedIdsJob.Id);
                }
            }

            var servmanCustomer = ContextHelper.GetCurrentCustomer();

            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                job.LeadId = lead.Id;
                job.UpdateNullable();
                Job.Insert(job, connection);

                if (lead.LeadStatusId == (int)LeadStatusEnum.Converted)
                    return;

                lead.LeadStatusId = (int)LeadStatusEnum.Converted;
                lead.UpdateNullable();
                Lead.Update(lead, connection);
				
                var historyItem = new LeadChangeHistory
                                      {
                                          Action = "Change status",
                                          DateChanged = DateTime.Now,
                                          Description = "to CONVERTED",
                                          LeadId = lead.Id,
                                          UserId = ContextHelper.GetCurrentUser().Id,
                                          LeadStatusId = lead.LeadStatusId
                                      };
                LeadChangeHistory.Insert(historyItem, connection);
            }

        }

    }
}
