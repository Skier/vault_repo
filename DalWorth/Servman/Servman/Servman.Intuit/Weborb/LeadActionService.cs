using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Servman.Domain;
using Servman.Service;

namespace Servman.Intuit.Weborb
{
    public class LeadActionService
    {
        public LeadAction[] GetByLeadStatusId(int id)
        {
            return Service.LeadActionService.GetByLeadStatusId(id, ContextHelper.GetCurrentCustomer()).ToArray();
        }
    }
}