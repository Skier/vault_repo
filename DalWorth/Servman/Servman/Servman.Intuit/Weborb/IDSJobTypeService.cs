using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Intuit.Sb.Cdm;
using Servman.Service;

namespace Servman.Intuit.Weborb
{
    public class IDSJobTypeService
    {
        public JobType[] GetAll()
        {
            return JobTypeService.GetAll(ContextHelper.GetCurrentCustomer()).ToArray();
        }
    }
}
