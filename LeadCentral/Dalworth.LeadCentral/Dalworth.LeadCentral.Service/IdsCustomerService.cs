using System;
using System.Collections;
using System.Collections.Generic;
using Dalworth.Common.SDK;
using Intuit.Platform.Client.Core;
using Intuit.Sb.Cdm;
using Intuit.Sb.Cdm.Common;
using IntuitCore = Intuit.Platform.Client.Core;

namespace Dalworth.LeadCentral.Service
{
    public class IdsCustomerService
    {
        public static List<Customer> GetByIdList(PlatformSessionContext context, string realmId, List<IdType> ids)
        {
            if (context.ServiceType == IntuitServicesType.QBO)
                return QboGetByIdList(context, realmId, ids);

            return QbGetByIdList(context, realmId, ids);
        }

        private static List<Customer> QbGetByIdList(PlatformSessionContext context, 
                                                    string realmId, List<IdType> ids)
        {
            if (ids == null || ids.Count == 0)
                return null;

            var idSets = new List<IdSet>();
            idSets.Add(new IdSet {Id = ids.ToArray()});

            var itemElementName = new List<ItemsChoiceType4>();
            itemElementName.Add(ItemsChoiceType4.ListIdSet);

            var customerQuery = new QBQBOCustomerQuery
                                    {
                                        Items = idSets.ToArray(),
                                        ItemsElementName = itemElementName.ToArray()
                                    };

            var customerService = GetService(context);
            return customerService.GetCustomers(context, 
                                                realmId,
                                                customerQuery);
        }

        private static List<Customer> QboGetByIdList(PlatformSessionContext context, 
                                                     string realmId, List<IdType> ids)
        {
            if (ids == null || ids.Count == 0)
                return null;

            var customerService = GetService(context);

            var result = new List<Customer>();
            foreach (var id in ids)
            {
                var customer = customerService.FindById(context, realmId, id);
                customer.Id = id;
                result.Add(customer);
            }
            return result;
        }

        public static Hashtable GetCustomersHashtableByIdList(PlatformSessionContext context, string realmId, List<IdType> ids)
        {
            var result = new Hashtable();
            List<Customer> customers;

            if (context.ServiceType == IntuitServicesType.QBO)
                customers = QboGetByIdList(context, realmId, ids);
            else
                customers = GetByIdList(context, realmId, ids);

            if (customers != null)
            {
                foreach (var customer in customers)
                {
                    result[IdTypeUtil.GetQbIdString(customer.Id)] = customer;
                }
            }

            return result;
        }

        private static Intuit.Sb.Cdm.Common.CustomerService GetService(PlatformSessionContext context)
        {
            var service = new Intuit.Sb.Cdm.Common.CustomerService();
            try
            {
                context.GetCurrentUserInfo();
                return service;
            }
            catch (Exception)
            {
                throw new DalworthException("Session expired");
            }
        }


    }
}
