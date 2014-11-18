using System.Collections.Generic;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service
{
    public class PartnerPhoneNumberService
    {
        public static List<PartnerPhoneNumber> GetByPartnerId(int businessPartnerId)
        {
            using(var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                return PartnerPhoneNumber.GetByPartnerId(businessPartnerId, connection);
            }
        }

        public static PartnerPhoneNumber GetById(int partnerPhoneNumberId)
        {
            PartnerPhoneNumber result = null;
            try
            {
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    result = PartnerPhoneNumber.FindByPrimaryKey(partnerPhoneNumberId, connection);
                }
            } catch{}

            return result;
        }

        public static PartnerPhoneNumber Save(PartnerPhoneNumber partnerPhoneNumber)
        {
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                return PartnerPhoneNumber.Save(partnerPhoneNumber, connection);
            }
        }

        public static void Delete(PartnerPhoneNumber partnerPhoneNumber)
        {
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                PartnerPhoneNumber.Delete(partnerPhoneNumber, connection);
            }
        }
    }
}
