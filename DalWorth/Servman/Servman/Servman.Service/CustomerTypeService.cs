using System.Collections.Generic;
using Intuit.Sb.Cdm;
using Servman.Domain;

namespace Servman.Service
{
    public class CustomerTypeService
    {
        public static List<CustomerType> GetAll(ServmanCustomer servmanCustomer)
        {
            var idsContext = ContextHelper.GetCurrentQbContext();
            var realmId = ContextHelper.GetRealmId();
            
            var idsService = new Intuit.Platform.Client.Core.IDS.CustomerTypeService();
            var customerTypes = idsService.FindAll(idsContext, realmId);

            return customerTypes;
        }
    }
}
