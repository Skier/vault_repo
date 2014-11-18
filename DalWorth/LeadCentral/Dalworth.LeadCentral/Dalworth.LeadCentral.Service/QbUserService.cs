using System;
using System.Collections.Generic;
using Intuit.Platform.Client.Core;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.SDK;

namespace Dalworth.LeadCentral.Service
{
    public class QbUserService
    {
        internal static string GetUserId(PlatformSessionContext context, ServmanCustomer servmanCustomer, User user)
        {
            if (!String.IsNullOrEmpty(user.QbUserId))
                return user.QbUserId;

            var dbId = servmanCustomer.AppDbId;
            var role = GetRoleInfoByName(context, servmanCustomer, user.RoleName);
            if (role == null)
                throw new DalworthException(user.RoleName + " role doesnt exists in the system. Please create it before.");

            var qbUser = GetQbUser(context, servmanCustomer, user);

            var userId = (qbUser == null) ? 
                context.ProvisionUser(dbId, user.Email, user.FirstName, user.LastName, role.ID) 
                : qbUser.ID;

            return userId;
        }

        public static void AddUserToRole(PlatformSessionContext context, ServmanCustomer servmanCustomer, string userId, string roleName)
        {
            var roleId = GetRoleInfoByName(context, servmanCustomer, roleName).ID;
            var dbId = servmanCustomer.AppDbId;

            try
            {
                context.AddUserToRole(dbId, userId, roleId);
            }
            catch (Exception)
            {
            }
        }

        public static void RemoveUserFromAllRoles(PlatformSessionContext context, ServmanCustomer servmanCustomer, string userId)
        {
            var dbId = servmanCustomer.AppDbId;
            try
            {
                context.RemoveUser(dbId, userId);
            }
            catch (Exception)
            {
            }
        }

        public static void RemoveUserFromRoles(PlatformSessionContext context, ServmanCustomer servmanCustomer, string userId, string roleId)
        {
            var dbId = servmanCustomer.AppDbId;
            try
            {
                context.RemoveUserFromRole(dbId, userId, roleId);
            }
            catch (Exception)
            {
            }
        }

        public static void SendInvitation(PlatformSessionContext context, ServmanCustomer servmanCustomer, string userId, string message)
        {
            var dbId = servmanCustomer.AppDbId;
            try
            {
                context.SendInvitation(dbId, userId, message);
            }
            catch (Exception)
            {
            }
        }

        internal static List<UserInfo> GetAppUsers(PlatformSessionContext context, ServmanCustomer servmanCustomer)
        {
            var dbId = servmanCustomer.AppDbId;
            try
            {
                return context.GetUserRoles(dbId);
            }
            catch (Exception ex)
            {
                throw new DalworthException(ex);
            }
        }

        private static UserInfo GetQbUser(PlatformSessionContext context, ServmanCustomer servmanCustomer, User user)
        {
            UserInfo qbUser = null;
            try
            {
                qbUser = context.GetUser(user.Email);
            }
            catch (Exception)
            {
            }

            return qbUser;
        }

        private static RoleInfo GetRoleInfoByName(PlatformSessionContext context, ServmanCustomer servmanCustomer, string roleName)
        {
            List<RoleInfo> roles;
            try
            {
                roles = context.GetRoleInfo(servmanCustomer.AppDbId);
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
