using System.Collections.Generic;
using System.Data;
using Servman.Domain;

namespace Servman.Service
{
    public class CustomerServiceRepService
    {
        public static CustomerServiceRep Save(CustomerServiceRep customerServiceRep, ServmanCustomer servmanCustomer)
        {
            if (customerServiceRep.RelatedUser != null)
            {
                if (customerServiceRep.IsActive)
                {
                    if (string.IsNullOrEmpty(customerServiceRep.RelatedUser.QbUserId))
                        customerServiceRep.RelatedUser.QbUserId = QbUserService.GetUserId(customerServiceRep.RelatedUser, CustomerServiceRep.CustomerServiceRepRoleName);

                    QbUserService.AddUserToRole(customerServiceRep.RelatedUser.QbUserId, CustomerServiceRep.CustomerServiceRepRoleName);
                }
                else
                {
                    QbUserService.RemoveUserFromRole(customerServiceRep.RelatedUser.QbUserId, CustomerServiceRep.CustomerServiceRepRoleName);
                }

                if (!string.IsNullOrEmpty(customerServiceRep.RelatedUser.QbUserId))
                {
                    var user = UserService.GetUserByQbUserId(customerServiceRep.RelatedUser.QbUserId, servmanCustomer);
                    if (user != null)
                        customerServiceRep.RelatedUser.Id = user.Id;
                }

                customerServiceRep.RelatedUser = UserService.Save(customerServiceRep.RelatedUser, servmanCustomer);

                customerServiceRep.UserId = customerServiceRep.RelatedUser.Id;
            }

            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                if (CustomerServiceRep.Exists(customerServiceRep, connection))
                    CustomerServiceRep.Update(customerServiceRep, connection);
                else
                    CustomerServiceRep.Insert(customerServiceRep, connection);
            }

            return customerServiceRep;
        }

        public static List<CustomerServiceRep> GetAll(ServmanCustomer servmanCustomer)
        {
            List<CustomerServiceRep> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = CustomerServiceRep.Find(connection);

                foreach (var bp in result)
                {
                    bp.RelatedUser = User.FindByPrimaryKey(bp.UserId, connection);
                }
            }
            return result;
        }

        public static CustomerServiceRep GetByUserId(int userId, ServmanCustomer servmanCustomer)
        {
            CustomerServiceRep result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = CustomerServiceRep.GetByUserId(userId, connection);
                result.RelatedUser = User.FindByPrimaryKey(result.UserId, connection);
            }
            return result;
        }
    }
}
