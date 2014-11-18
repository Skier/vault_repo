using System.Collections.Generic;
using System.Data;
using Servman.Domain;

namespace Servman.Service
{
    public class OwnerService
    {
        public static Owner Save(Owner owner, ServmanCustomer servmanCustomer)
        {
            if (owner.RelatedUser != null)
            {
                if (owner.IsActive)
                {
                    if (string.IsNullOrEmpty(owner.RelatedUser.QbUserId))
                        owner.RelatedUser.QbUserId = QbUserService.GetUserId(owner.RelatedUser, Owner.OwnerRoleName);

                    QbUserService.AddUserToRole(owner.RelatedUser.QbUserId, Owner.OwnerRoleName);
                }
                else
                {
                    QbUserService.RemoveUserFromRole(owner.RelatedUser.QbUserId, Owner.OwnerRoleName);
                }

                if (!string.IsNullOrEmpty(owner.RelatedUser.QbUserId))
                {
                    var user = UserService.GetUserByQbUserId(owner.RelatedUser.QbUserId, servmanCustomer);
                    if (user != null)
                        owner.RelatedUser.Id = user.Id;
                }

                owner.RelatedUser = UserService.Save(owner.RelatedUser, servmanCustomer);

                owner.UserId = owner.RelatedUser.Id;
            }

            if (owner.UserId == 0)
                owner.UserId = null;

            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                if (Owner.Exists(owner, connection))
                    Owner.Update(owner, connection);
                else
                    Owner.Insert(owner, connection);
            }
            return owner;
        }

        public static List<Owner> GetAll(ServmanCustomer servmanCustomer)
        {
            List<Owner> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = Owner.Find(connection);

                foreach (var bp in result)
                {
                    if (bp.UserId != null)
                        bp.RelatedUser = User.FindByPrimaryKey(bp.UserId.Value, connection);
                }
            }
            return result;
        }

        public static Owner GetByUserId(int userId, ServmanCustomer servmanCustomer)
        {
            Owner result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = Owner.GetByUserId(userId, connection);

                if (result.UserId != null)
                    result.RelatedUser = User.FindByPrimaryKey(result.UserId.Value, connection);
            }
            return result;
        }
    }
}
