using System.Collections.Generic;
using System.Data;
using Servman.Domain;

namespace Servman.Service
{
    public class LeadTypeService
    {
        public static LeadType Save(LeadType leadType, ServmanCustomer servmanCustomer)
        {
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                if (LeadType.Exists(leadType, connection))
                    LeadType.Update(leadType, connection);
                else
                    LeadType.Insert(leadType, connection);
            }
            return leadType;
        }

        public static List<LeadType> GetAll(ServmanCustomer servmanCustomer)
        {
            List<LeadType> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = LeadType.Find(connection);
            }
            return result;
        }

    }
}
