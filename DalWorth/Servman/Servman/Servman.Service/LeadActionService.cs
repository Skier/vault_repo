using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Servman.Domain;

namespace Servman.Service
{
    public class LeadActionService
    {
        public static List<LeadAction> GetByLeadStatusId(int id, ServmanCustomer servmanCustomer)
        {
            List<LeadAction> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = LeadAction.FindByLeadStatusId(id, connection);
            }
            return result;
        }
    }
}
