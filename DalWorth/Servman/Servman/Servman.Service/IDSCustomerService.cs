using System.Collections;
using System.Collections.Generic;
using Intuit.Platform.Client.Core;
using Intuit.Sb.Cdm;
using Intuit.Sb.Cdm.Common;
using Servman.Domain;
using IntuitCore = Intuit.Platform.Client.Core;

namespace Servman.Service
{
    public class IdsCustomerService
    {
        public static Customer Save(Customer customer)
        {
            var customerService = new CustomerService();
            if (customer.Id != null && !string.IsNullOrEmpty(customer.Id.Value))
            {
                customer = customerService.UpdateCustomer(ContextHelper.GetCurrentQbContext(),
                                                          ContextHelper.GetRealmId(),
                                                          customer);
            } 
            else
            {
                customer = customerService.AddCustomer(ContextHelper.GetCurrentQbContext(),
                                                       ContextHelper.GetRealmId(),
                                                       customer);
            }

            return customer;
        }

        public static List<Customer> GetByIdList(List<IdType> ids)
        {
            var context = ContextHelper.GetCurrentQbContext();
            
            if (context.ServiceType == IntuitServicesType.QBO)
                return QBOGetByIdList(ids);

            return QBGetByIdList(ids);
        }

        private static List<Customer> QBGetByIdList(List<IdType> ids)
        {
            if (ids == null || ids.Count == 0)
                return null;

            var idList = new List<IdType>();
            foreach (var id in ids)
            {
                idList.Add(id);
            }

            var idSets = new List<IdSet>();
            idSets.Add(new IdSet {Id = idList.ToArray()});

            var itemElementName = new List<ItemsChoiceType4>();
            itemElementName.Add(ItemsChoiceType4.ListIdSet);

            var customerQuery = new QBQBOCustomerQuery
                                    {
                                        Items = idSets.ToArray(),
                                        ItemsElementName = itemElementName.ToArray()
                                    };

            var customerService = new CustomerService();
            return customerService.GetCustomers(ContextHelper.GetCurrentQbContext(), 
                                                ContextHelper.GetRealmId(),
                                                customerQuery);
        }

        private static List<Customer> QBOGetByIdList(List<IdType> ids)
        {
            if (ids == null || ids.Count == 0)
                return null;

            var customerService = new CustomerService();
            var context = ContextHelper.GetCurrentQbContext();
            var realmId = ContextHelper.GetRealmId();

            var result = new List<Customer>();
            foreach (var id in ids)
            {
                var customer = customerService.FindById(context, realmId, id);
                customer.Id = id;
                result.Add(customer);
            }
            return result;
        }

        public static Hashtable GetCustomersHashtableByIdList(List<IdType> ids)
        {
            var result = new Hashtable();
            List<Customer> customers;

            if (ContextHelper.GetCurrentQbContext().ServiceType == IntuitServicesType.QBO)
                customers = QBOGetByIdList(ids);
            else
                customers = GetByIdList(ids);

            if (customers != null)
            {
                foreach (var customer in customers)
                {
                    result[IdTypeUtil.GetQbIdString(customer.Id)] = customer;
                }
            }

            return result;
        }

        public static List<Customer> GetCustomers(CustomerFilter filter)
        {
            var result = new List<Customer>();

            if (filter == null)
                return result;

            var customerQuery = new QBQBOCustomerQuery();
            if (filter.Name.Split(' ').Length > 1)
            {
                customerQuery.FirstLastName = filter.Name;
            } else
            {
                customerQuery.Item1 = filter.Name;
                customerQuery.Item1ElementName = Item1ChoiceType.FirstLastInside;
            }

            var customerService = new CustomerService();
            var customers = customerService.GetCustomers(ContextHelper.GetCurrentQbContext(), 
                                                         ContextHelper.GetRealmId(),
                                                         customerQuery);
            if (customers != null)
            {
                foreach (var customer in customers)
                {
                    result.Add(customer);
                }
            }

            return result;
        }

        public static Customer GetByName(string customerName)
        {
            var customerQuery = new QBQBOCustomerQuery();
            customerQuery.FirstLastName = customerName;

            var customerService = new CustomerService();
            var customers = customerService.GetCustomers(ContextHelper.GetCurrentQbContext(),
                                                         ContextHelper.GetRealmId(),
                                                         customerQuery);
            if (customers != null)
                return customers[0];

            return null;
        }
    }
}
