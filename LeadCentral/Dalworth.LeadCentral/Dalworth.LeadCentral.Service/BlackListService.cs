using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service
{
    public class BlackListService
    {
        public static List<PhoneBlackList> GetAll()
        {
            using(var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                return PhoneBlackList.Find(connection);
            }
        }

        public static PhoneBlackList GetById(int id)
        {
            PhoneBlackList result = null;

            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                try
                {
                    result = PhoneBlackList.FindByPrimaryKey(id, connection);
                } catch(Exception){}
            }
            
            return result;
        }

        public static void Save(PhoneBlackList phoneBlackList)
        {
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                PhoneBlackList.Save(phoneBlackList, connection);
            }
        }

        public static void Delete(PhoneBlackList phoneBlackList)
        {
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                PhoneBlackList.Delete(phoneBlackList, connection);
            }
        }

        public static PhoneBlackList GetByCallerPhone(string phone, Customer customer)
        {
            using (var connection = CustomerService.GetConnection(customer))
            {
                var phones = PhoneBlackList.GetByCallerPhone(phone, connection);
                if (phones != null && phones.Count > 0)
                    return phones[0];
            }

            return null;
        }

        public static bool PhoneInBlackList(string phone)
        {
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                var phones = PhoneBlackList.GetByCallerPhone(phone, connection);
                if (phones != null && phones.Count > 0)
                    return true;
            }

            return false;
        }
    }
}
