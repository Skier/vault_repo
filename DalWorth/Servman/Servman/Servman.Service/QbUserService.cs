using System;
using System.Collections.Generic;
using Intuit.Platform.Client.Core;
using Servman.Domain;
using Servman.SDK;

namespace Servman.Service
{
    public class QbUserService
    {
        internal static string GetUserId(User user)
        {
            if (!String.IsNullOrEmpty(user.QbUserId))
                return user.QbUserId;

            var context = GetContext();
            var dbId = GetDbId();
            var role = GetRoleInfoByName(user.RoleName);
            if (role == null)
                throw new DalworthException(user.RoleName + " role doesnt exists in the system. Please create it before.");

            var qbUser = GetQbUser(user);

            var userId = (qbUser == null) ? 
                context.ProvisionUser(dbId, user.Email, user.FirstName, user.LastName, role.ID) 
                : qbUser.ID;

            return userId;
        }

        public static void AddUserToRole(string userId, string roleName)
        {
            var context = GetContext();
            var roleId = GetRoleInfoByName(roleName).ID;
            var dbId = GetDbId();

            try
            {
                context.AddUserToRole(dbId, userId, roleId);
            }
            catch (Exception)
            {
            }
        }

        public static void RemoveUserFromAllRoles(string userId)
        {
            var context = GetContext();
            var dbId = GetDbId();

            try
            {
                context.RemoveUser(dbId, userId);
            }
            catch (Exception)
            {
            }
        }

        public static void RemoveUserFromRoles(string userId, string roleId)
        {
            var context = GetContext();
            var dbId = GetDbId();

            try
            {
                context.RemoveUserFromRole(dbId, userId, roleId);
            }
            catch (Exception)
            {
            }
        }

        public static void SendInvitation(string userId, string message)
        {
            var context = GetContext();
            var dbId = GetDbId();
            try
            {
                context.SendInvitation(dbId, userId, message);
            }
            catch (Exception)
            {
            }
        }

        internal static List<UserInfo> GetAppUsers()
        {
            var context = GetContext();
            var dbId = GetDbId();

            return context.GetUserRoles(dbId);
        }

        private static string GetDbId()
        {
            return ContextHelper.GetDbId();
        }

        private static PlatformSessionContext GetContext()
        {
            return ContextHelper.GetCurrentQbContext();
        }

        private static UserInfo GetQbUser(User user)
        {
            UserInfo qbUser = null;
            try
            {
                qbUser = GetContext().GetUser(user.Email);
            }
            catch (Exception)
            {
            }

            return qbUser;
        }

        private static RoleInfo GetRoleInfoByName(string roleName)
        {

            List<RoleInfo> roles;
            try
            {
                roles = GetContext().GetRoleInfo(GetDbId());
            }
            catch (Exception ex)
            {
                throw new DalworthException(ex);
            }

            foreach (var roleInfo in roles)
            {
                if (roleInfo.Name == roleName)
                    return roleInfo;
            }

            return null;
        }

    }
}
