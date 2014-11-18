using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Servman.Domain;
using Servman.Service;

namespace Servman.Intuit.Weborb
{
    public class CustomerServiceRepService
    {
        public CustomerServiceRep[] GetAll()
        {
            return Service.CustomerServiceRepService.GetAll(ContextHelper.GetCurrentCustomer()).ToArray();
        }

        public CustomerServiceRep GetByUserId(int id)
        {
            return Service.CustomerServiceRepService.GetByUserId(id, ContextHelper.GetCurrentCustomer());
        }

        public CustomerServiceRep Save(CustomerServiceRep employee)
        {
            return Service.CustomerServiceRepService.Save(employee, ContextHelper.GetCurrentCustomer());
        }
    }
}