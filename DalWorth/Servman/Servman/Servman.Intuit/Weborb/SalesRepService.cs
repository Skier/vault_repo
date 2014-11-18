using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Servman.Domain;
using Servman.Service;

namespace Servman.Intuit.Weborb
{
    public class SalesRepService
    {
        public SalesRep[] GetAll()
        {
            return Service.SalesRepService.GetAll(ContextHelper.GetCurrentCustomer()).ToArray();
        }

        public SalesRep GetByUserId(int id)
        {
            return Service.SalesRepService.GetByUserId(id, ContextHelper.GetCurrentCustomer());
        }

        public SalesRep[] GetByCompanyPhoneId(int id)
        {
            return Service.SalesRepService.GetByCompanyPhoneId(id, ContextHelper.GetCurrentCustomer()).ToArray();
        }

        public SalesRep Save(SalesRep salesRep)
        {
            return Service.SalesRepService.Save(salesRep, ContextHelper.GetCurrentCustomer());
        }

        public SalesRep SaveWithPhones(SalesRep salesRep, Phone[] ownPhones, Phone[] companyPhones)
        {
            return Service.SalesRepService.SaveWithPhones(salesRep, ownPhones, companyPhones, ContextHelper.GetCurrentCustomer());
        }

        public void AddCompanyPhone(SalesRep salesRep, Phone phone)
        {
            Service.SalesRepService.AddCompanyPhone(salesRep, phone, ContextHelper.GetCurrentCustomer());
        }

        public void RemoveCompanyPhone(SalesRep salesRep, Phone phone)
        {
            Service.SalesRepService.RemoveCompanyPhone(salesRep, phone, ContextHelper.GetCurrentCustomer());
        }
    }
}