using System;
using System.Collections.Generic;
using System.Data;
using Servman.Domain;

namespace Servman.Service
{
    public class SalesRepService
    {
        public static SalesRep Save(SalesRep salesRep, ServmanCustomer servmanCustomer)
        {
            if (salesRep.RelatedUser != null)
            {
                if (salesRep.IsActive)
                {
                    if (string.IsNullOrEmpty(salesRep.RelatedUser.QbUserId))
                        salesRep.RelatedUser.QbUserId = QbUserService.GetUserId(salesRep.RelatedUser, SalesRep.SalesRepRoleName);

                    QbUserService.AddUserToRole(salesRep.RelatedUser.QbUserId, SalesRep.SalesRepRoleName);
                }
                else
                {
                    QbUserService.RemoveUserFromRole(salesRep.RelatedUser.QbUserId, SalesRep.SalesRepRoleName);
                }

                if (!string.IsNullOrEmpty(salesRep.RelatedUser.QbUserId))
                {
                    var user = UserService.GetUserByQbUserId(salesRep.RelatedUser.QbUserId, servmanCustomer);
                    if (user != null)
                        salesRep.RelatedUser.Id = user.Id;
                }

                salesRep.RelatedUser = UserService.Save(salesRep.RelatedUser, servmanCustomer);

                salesRep.UserId = salesRep.RelatedUser.Id;
            }

            if (salesRep.UserId == 0)
                salesRep.UserId = null;

            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                if (SalesRep.Exists(salesRep, connection))
                {
                    SalesRep.Update(salesRep, connection);
                }
                else
                {
                    SalesRep.Insert(salesRep, connection);
                }
            }
            return salesRep;
        }

        public static List<SalesRep> GetAll(ServmanCustomer servmanCustomer)
        {
            List<SalesRep> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = SalesRep.Find(connection);

                foreach (var salesRep in result)
                {
                    if (salesRep.UserId != null)
                        salesRep.RelatedUser = User.FindByPrimaryKey(salesRep.UserId.Value, connection);
                }
            }
            return result;
        }

        public static SalesRep GetByUserId(int userId, ServmanCustomer servmanCustomer)
        {
            SalesRep result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = SalesRep.GetByUserId(userId, connection);

                if (result.UserId != null)
                    result.RelatedUser = User.FindByPrimaryKey(result.UserId.Value, connection);
            }
            return result;
        }

        public static SalesRep SaveWithPhones(SalesRep salesRep, Phone[] ownPhones, Phone[] companyPhones, ServmanCustomer servmanCustomer)
        {
            var partner = Save(salesRep, servmanCustomer);
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                PhoneToSalesRep.RemoveBySalesRepId(partner.Id, connection);

                var phoneToSRs = new List<PhoneToSalesRep>();
                PhoneToSalesRep phoneToSr;

                foreach (var ownPhone in ownPhones)
                {
                    var phone = PhoneService.SavePhone(ownPhone, connection);
                    phoneToSr = new PhoneToSalesRep
                    {
                        SalesRepId = salesRep.Id,
                        PhoneId = phone.Id,
                        IsIncoming = false
                    };
                    phoneToSRs.Add(phoneToSr);
                }

                foreach (var companyPhone in companyPhones)
                {
                    var phone = PhoneService.SavePhone(companyPhone, connection);
                    phoneToSr = new PhoneToSalesRep
                    {
                        SalesRepId = salesRep.Id,
                        PhoneId = phone.Id,
                        IsIncoming = true
                    };
                    phoneToSRs.Add(phoneToSr);
                }

                PhoneToSalesRep.Insert(phoneToSRs, connection);
            }

            return partner;
        }

        public static void AddCompanyPhone(SalesRep salesRep, Phone phone, ServmanCustomer servmanCustomer)
        {
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                var phoneToSr = new PhoneToSalesRep()
                {
                    SalesRepId = salesRep.Id,
                    PhoneId = phone.Id,
                    IsIncoming = true
                };

                PhoneToSalesRep.Insert(phoneToSr, connection);
            }
        }

        public static void RemoveCompanyPhone(SalesRep salesRep, Phone phone, ServmanCustomer servmanCustomer)
        {
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                var phoneToSalesRep = PhoneToSalesRep.FindByPrimaryKey(phone.Id, salesRep.Id, connection);
                PhoneToSalesRep.Delete(phoneToSalesRep, connection);
            }
        }

        public static List<SalesRep> GetByCompanyPhoneId(int phoneId, ServmanCustomer servmanCustomer)
        {
            List<SalesRep> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = SalesRep.GetByCompanyPhoneId(phoneId, connection);

                foreach (var salesRep in result)
                {
                    if (salesRep.UserId != null)
                        salesRep.RelatedUser = User.FindByPrimaryKey(salesRep.UserId.Value, connection);
                }
            }
            return result;
        }

        public static List<SalesRep> GetByOwnPhoneNumber(string phoneNo, ServmanCustomer servmanCustomer)
        {
            List<SalesRep> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = SalesRep.GetByOwnPhoneNumbers(phoneNo, connection);

                foreach (var businessPartner in result)
                {
                    if (businessPartner.UserId != null)
                        businessPartner.RelatedUser = User.FindByPrimaryKey(businessPartner.UserId.Value, connection);
                }
            }
            return result;
        }

        public static List<SalesRep> GetByCompanyPhoneNumber(string phoneNo, ServmanCustomer servmanCustomer)
        {
            List<SalesRep> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = SalesRep.GetByCompanyPhoneNumbers(phoneNo, connection);

                foreach (var businessPartner in result)
                {
                    if (businessPartner.UserId != null)
                        businessPartner.RelatedUser = User.FindByPrimaryKey(businessPartner.UserId.Value, connection);
                }
            }
            return result;
        }

    }
}
