using System;
using System.Collections.Generic;
using System.Data;
using Servman.Domain;

namespace Servman.Service
{
    public class BusinessPartnerService
    {
        public static BusinessPartner Save(BusinessPartner partner, ServmanCustomer servmanCustomer)
        {
            if (partner.RelatedUser != null)
            {
                if (partner.IsActive)
                {
                    if (string.IsNullOrEmpty(partner.RelatedUser.QbUserId))
                        partner.RelatedUser.QbUserId = QbUserService.GetUserId(partner.RelatedUser, BusinessPartner.BusinessPartnerRoleName);

                    QbUserService.AddUserToRole(partner.RelatedUser.QbUserId, BusinessPartner.BusinessPartnerRoleName);
                } else
                {
                    QbUserService.RemoveUserFromRole(partner.RelatedUser.QbUserId, BusinessPartner.BusinessPartnerRoleName);
                }

                if (!string.IsNullOrEmpty(partner.RelatedUser.QbUserId))
                {
                    var user = UserService.GetUserByQbUserId(partner.RelatedUser.QbUserId, servmanCustomer);
                    if (user != null)
                        partner.RelatedUser.Id = user.Id;
                }

                partner.RelatedUser = UserService.Save(partner.RelatedUser, servmanCustomer);
                
                partner.UserId = partner.RelatedUser.Id;
            }

            if (partner.SalesRepId == 0)
                partner.SalesRepId = null;

            if (partner.UserId == 0)
                partner.UserId = null;

            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                if (BusinessPartner.Exists(partner, connection))
                    BusinessPartner.Update(partner, connection);
                else
                    BusinessPartner.Insert(partner, connection);
            }
            return partner;
        }

        public static List<BusinessPartner> GetAll(ServmanCustomer servmanCustomer)
        {
            List<BusinessPartner> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = BusinessPartner.Find(connection);

                foreach (var bp in result)
                {
                    if (bp.UserId != null)
                        bp.RelatedUser = User.FindByPrimaryKey(bp.UserId.Value, connection);
                }
            }
            return result;
        }

        public static BusinessPartner GetByUserId(int userId, ServmanCustomer servmanCustomer)
        {
            BusinessPartner result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = BusinessPartner.GetByUserId(userId, connection);
                
                if (result.UserId != null)
                    result.RelatedUser = User.FindByPrimaryKey(result.UserId.Value, connection);
            }
            return result;
        }


        public static List<BusinessPartner> GetByPhoneNumbers(string phoneFrom, string phoneTo, ServmanCustomer servmanCustomer)
        {
            List<BusinessPartner> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = BusinessPartner.GetByPhoneNumbers(phoneFrom, phoneTo, connection);

                foreach (var businessPartner in result)
                {
                    if (businessPartner.UserId != null)
                        businessPartner.RelatedUser = User.FindByPrimaryKey(businessPartner.UserId.Value, connection);
                }
            }
            return result;
        }

        public static List<BusinessPartner> GetByCompanyPhoneId(int phoneId, ServmanCustomer servmanCustomer)
        {
            List<BusinessPartner> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = BusinessPartner.GetByCompanyPhoneId(phoneId, connection);

                foreach (var businessPartner in result)
                {
                    if (businessPartner.UserId != null)
                        businessPartner.RelatedUser = User.FindByPrimaryKey(businessPartner.UserId.Value, connection);
                }
            }
            return result;
        }

        public static List<BusinessPartner> GetByOwnPhoneNumber(string phoneNo, ServmanCustomer servmanCustomer)
        {
            List<BusinessPartner> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = BusinessPartner.GetByOwnPhoneNumbers(phoneNo, connection);

                foreach (var businessPartner in result)
                {
                    if (businessPartner.UserId != null)
                        businessPartner.RelatedUser = User.FindByPrimaryKey(businessPartner.UserId.Value, connection);
                }
            }
            return result;
        }

        public static List<BusinessPartner> GetByCompanyPhoneNumber(string phoneNo, ServmanCustomer servmanCustomer)
        {
            List<BusinessPartner> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = BusinessPartner.GetByCompanyPhoneNumbers(phoneNo, connection);

                foreach (var businessPartner in result)
                {
                    if (businessPartner.UserId != null)
                        businessPartner.RelatedUser = User.FindByPrimaryKey(businessPartner.UserId.Value, connection);
                }
            }
            return result;
        }

        public static BusinessPartner SaveWithPhones(BusinessPartner businessPartner, Phone[] ownPhones, Phone[] companyPhones, ServmanCustomer servmanCustomer)
        {
            var partner = Save(businessPartner, servmanCustomer);
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                PhoneToBusinessPartner.RemoveByBusinessPartnerId(partner.Id, connection);

                var phoneToBPs = new List<PhoneToBusinessPartner>();
                PhoneToBusinessPartner phoneToBp;

                foreach (var ownPhone in ownPhones)
                {
                    var phone = PhoneService.SavePhone(ownPhone, connection);
                    phoneToBp = new PhoneToBusinessPartner
                                    {
                                        BusinessPartnerId = businessPartner.Id,
                                        PhoneId = phone.Id,
                                        IsIncoming = false
                                    };
                    phoneToBPs.Add(phoneToBp);
                }

                foreach (var companyPhone in companyPhones)
                {
                    var phone = PhoneService.SavePhone(companyPhone, connection);
                    phoneToBp = new PhoneToBusinessPartner
                                    {
                                        BusinessPartnerId = businessPartner.Id,
                                        PhoneId = phone.Id,
                                        IsIncoming = true
                                    };
                    phoneToBPs.Add(phoneToBp);
                }

                PhoneToBusinessPartner.Insert(phoneToBPs, connection);
            }

            return partner;
        }

        public static void AddCompanyPhone(BusinessPartner businessPartner, Phone phone, ServmanCustomer servmanCustomer)
        {
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                 var phoneToBp = new PhoneToBusinessPartner
                    {
                        BusinessPartnerId = businessPartner.Id,
                        PhoneId = phone.Id,
                        IsIncoming = true
                    };

                PhoneToBusinessPartner.Insert(phoneToBp, connection);
            }
        }

        public static void RemoveCompanyPhone(BusinessPartner businessPartner, Phone phone, ServmanCustomer servmanCustomer)
        {
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                var phoneToBusinessPartner = PhoneToBusinessPartner.FindByPrimaryKey(phone.Id, businessPartner.Id, connection);
                PhoneToBusinessPartner.Delete(phoneToBusinessPartner, connection);
            }
        }

    }
}
