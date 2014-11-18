using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Intuit.Sb.Cdm;
using Servman.Service;

namespace Servman.Intuit.Weborb
{
    public class IDSCustomerTypeService
    {
        public CustomerType[] GetAll()
        {
            return CustomerTypeService.GetAll(ContextHelper.GetCurrentCustomer()).ToArray();
        }
    }
}
