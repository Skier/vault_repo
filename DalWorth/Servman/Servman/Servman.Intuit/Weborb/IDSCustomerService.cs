using Intuit.Sb.Cdm;
using Servman.Domain;
using Servman.Service;

namespace Servman.Intuit.Weborb
{
    public class IDSCustomerService
    {
        public Customer[] GetAll()
        {
            return Service.IDSCustomerService.GetAll(ContextHelper.GetCurrentCustomer()).ToArray();
        }

        public Customer[] GetFilteredCustomers(CustomerFilter filter)
        {
            return Service.IDSCustomerService.GetFilteredCustomers(filter).ToArray();
        }

        public Customer Save(Customer customer)
        {
            return Service.IDSCustomerService.Save(customer);
        }

        public Customer GetByName(string customerName)
        {
            return Service.IDSCustomerService.GetByName(customerName);
        }
    }
}