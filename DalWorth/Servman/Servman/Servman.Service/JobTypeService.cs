using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intuit.Platform.Client.Core.IDS;
using Intuit.Sb.Cdm;
using Servman.Domain;

namespace Servman.Service
{
    public class JobTypeService
    {
        public static List<JobType> GetAll(ServmanCustomer servmanCustomer)
        {
            var idsContext = ContextHelper.GetCurrentQbContext();
            var realmId = ContextHelper.GetRealmId();
            
            var idsService = new Intuit.Platform.Client.Core.IDS.JobTypeService();
            var jobTypes = idsService.FindAll(idsContext, realmId);
            
            return jobTypes;
        }
    }
}
