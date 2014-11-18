using Servman.Domain;
using Servman.Service;

namespace Servman.Intuit.Weborb
{
    public class BusinessPartnerService
    {
        public BusinessPartner[] GetAll()
        {
            return Service.BusinessPartnerService.GetAll(ContextHelper.GetCurrentCustomer()).ToArray();
        }

        public BusinessPartner GetByUserId(int id)
        {
            return Service.BusinessPartnerService.GetByUserId(id, ContextHelper.GetCurrentCustomer());
        }

        public BusinessPartner[] GetByCompanyPhoneId(int id)
        {
            return Service.BusinessPartnerService.GetByCompanyPhoneId(id, ContextHelper.GetCurrentCustomer()).ToArray();
        }

        public BusinessPartner Save(BusinessPartner businessPartner)
        {
            return Service.BusinessPartnerService.Save(businessPartner, ContextHelper.GetCurrentCustomer());
        }

        public BusinessPartner SaveWithPhones(BusinessPartner businessPartner, Phone[] ownPhones, Phone[] companyPhones)
        {
            return Service.BusinessPartnerService.SaveWithPhones(businessPartner, ownPhones, companyPhones, ContextHelper.GetCurrentCustomer());
        }

        public void AddCompanyPhone(BusinessPartner businessPartner, Phone phone)
        {
            Service.BusinessPartnerService.AddCompanyPhone(businessPartner, phone,
                                                                            ContextHelper.GetCurrentCustomer());
        }

        public void RemoveCompanyPhone(BusinessPartner businessPartner, Phone phone)
        {
            Service.BusinessPartnerService.RemoveCompanyPhone(businessPartner, phone,
                                                                            ContextHelper.GetCurrentCustomer());
        }
    }
}