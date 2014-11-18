using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Servman.Domain;
using Servman.Service;

namespace Servman.Intuit.Weborb
{
    public class OwnerService
    {
        public Owner[] GetAll()
        {
            return Service.OwnerService.GetAll(ContextHelper.GetCurrentCustomer()).ToArray();
        }

        public Owner GetByUserId(int id)
        {
            return Service.OwnerService.GetByUserId(id, ContextHelper.GetCurrentCustomer());
        }

        public Owner Save(Owner owner)
        {
            return Service.OwnerService.Save(owner, ContextHelper.GetCurrentCustomer());
        }
    }
}