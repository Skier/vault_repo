using System.Collections.Generic;
using Servman.Domain;
using Servman.Service;

namespace Servman.Intuit.Weborb
{
    public class LeadTypeService
    {
        public LeadType[] GetAll()
        {
            return Service.LeadTypeService.GetAll(ContextHelper.GetCurrentCustomer()).ToArray();
        }

        public LeadType Save(LeadType leadType)
        {
            return Service.LeadTypeService.Save(leadType, ContextHelper.GetCurrentCustomer());
        }
    }
}