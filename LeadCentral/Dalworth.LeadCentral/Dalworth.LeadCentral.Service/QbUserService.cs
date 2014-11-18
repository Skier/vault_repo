using System;
using System.Collections.Generic;
using Dalworth.Common.SDK;
using Intuit.Platform.Client.Core;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service
{
    public class QbUserService
    {
        #region GetUserId

        internal static string GetUserId(PlatformSessionContext context, Customer customer, User user)
        {
            if (!String.IsNullOrEmpty(user.QbUserId))
                return user.QbUserId;

            var dbId = customer.AppDbId;
            var role = GetRoleInfoByName(context, customer, user.QbRoleName);
            if (role == null)
                throw new DalworthException(user.QbRoleName + " role doesnt exists in the system. Please create it before.");

            UserInfo qbUser;
            try
            {
                qbUser = context.GetUser(user.Email);
                return qbUser.ID;
            }
            catch (Exception ex)
            {
                return context.ProvisionUser(dbId, user.Email, user.FirstName, user.LastName, role.ID);
            }
        }

        #endregion 

        #region AddUserToRole

        public static void AddUserToRole(PlatformSessionContext context, Customer customer, string userId, string roleName)
        {
            var qbUser = context.GetUserRole(customer.AppDbId, userId);
            if (qbUser.Roles.Count == 1 && qbUser.Roles[0].Name == roleName)
                return;

            RemoveUserFromAllRoles(context, customer, userId);

            var roleId = GetRoleInfoByName(context, customer, roleName).ID;
            var dbId = customer.AppDbId;

            try
            {
                context.AddUserToRole(dbId, userId, roleId);
            }
            catch (Exception)
            {
            }
        }

        #endregion 

        #region RemoveUserFromAllRoles

        public static void RemoveUserFromAllRoles(PlatformSessionContext context, Customer customer, string userId)
        {
            var dbId = customer.AppDbId;
            try
            {
                context.RemoveUser(dbId, userId);
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region RemoveUserFromRoles

        public static void RemoveUserFromRoles(PlatformSessionContext context, Customer customer, string userId, string roleId)
        {
            var dbId = customer.AppDbId;
            try
            {
                context.RemoveUserFromRole(dbId, userId, roleId);
            }
            catch (Exception)
            {
            }
        }

        #endregion 

        #region SendInvitation

        public static void SendInvitation(PlatformSessionContext context, Customer customer, string userId, string message)
        {
            var dbId = customer.AppDbId;
            try
            {
                context.SendInvitation(dbId, userId, message);
            }
            catch (Exception)
            {
            }
        }

        #endregion 

        #region GetAppUsers

        internal static List<UserInfo> GetAppUsers(PlatformSessionContext context, Customer customer)
        {
            var dbId = customer.AppDbId;
            try
            {
                return context.GetUserRoles(dbId);
            }
            catch (Exception ex)
            {
                throw new DalworthException(ex);
            }
        }

        #endregion 

        #region GetQbUser

        private static UserInfo GetQbUser(PlatformSessionContext context, User user)
        {
            UserInfo qbUser = null;
            

            return qbUser;
        }

        #endregion 

        #region GetRoleInfoByName

        private static RoleInfo GetRoleInfoByName(PlatformSessionContext context, Customer customer, string roleName)
        {
            List<RoleInfo> roles;
            try
            {
                roles = context.GetRoleInfo(customer.AppDbId);
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

        #endregion
    }
}
