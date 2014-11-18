using System;
using System.Collections.Generic;
using System.Data;
using Intuit.Platform.Client.Core;
using Servman.Domain;

namespace Servman.Service
{
    public class UserService
    {
        public static User Save(User user)
        {
            if(string.IsNullOrEmpty(user.QbUserId))
            {
                user.QbUserId = QbUserService.GetUserId(user);
                
                QbUserService.RemoveUserFromAllRoles(user.QbUserId);
                if (user.IsActive)
                    QbUserService.AddUserToRole(user.QbUserId, user.RoleName);

                SendInvitation(user, "");
            } else
            {
                QbUserService.RemoveUserFromAllRoles(user.QbUserId);
                if (user.IsActive)
                    QbUserService.AddUserToRole(user.QbUserId, user.RoleName);
            }

            if (user.PhotoFileId == 0) user.PhotoFileId = null;

            using (IDbConnection connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                user = User.Save(user, connection);
            }

            return user;
        }

        public static List<User> GetAll(IDbConnection connection)
        {
            return User.GetAll(connection);
        }

        public static List<User> GetAll()
        {
            List<User> result;
            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                result = GetAll(connection);
            }
            return result;
        }

        public static bool IsUserAdmin(UserInfo userInfo)
        {
            return QbUserHasRole(userInfo, User.AdministartorRoleName);
        }

        public static User GetUserByQbUserId(string qbUserId, ServmanCustomer servmanCustomer)
        {
            User result;

            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = User.GetByQbUserId(qbUserId, connection);
            }

            return result;
        }

        public static User SyncUser(UserInfo userInfo)
        {
            using(var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                return SyncUser(userInfo, connection);
            }
        }

        public static User SyncUser(UserInfo userInfo, IDbConnection connection)
        {
            var user = User.GetByQbUserId(userInfo.ID, connection);
            
            if (user == null)
            {
                user = new User();
                user.QbUserId = userInfo.ID;
                user.FirstName = userInfo.FirstName;
                user.LastName = userInfo.LastName;
                user.Name = String.IsNullOrEmpty(userInfo.ScreenName)
                                ? userInfo.FirstName + " " + userInfo.LastName
                                : userInfo.ScreenName;
                user.Email = userInfo.Email;
            }

            user.DateLastAccess = userInfo.LastAccess;
            user.RoleName = GetUserRoleName(userInfo);

            User.Save(user, connection);

            return user;
        }

        private static string GetUserRoleName(UserInfo userInfo)
        {
            var result = "";

            if (QbUserHasRole(userInfo, User.BusinessPartnerRoleName))
                result = User.BusinessPartnerRoleName;

            if (QbUserHasRole(userInfo, User.StaffRoleName))
                result = User.StaffRoleName;

            if (QbUserHasRole(userInfo, User.AdministartorRoleName))
                result = User.AdministartorRoleName;

            return result;
        }

        private static bool QbUserHasRole(UserInfo userInfo, string roleName)
        {
            foreach (var roleInfo in userInfo.Roles)
            {
                if (roleInfo.Name == roleName)
                    return true;
            }
            return false;
        }

        public static void SendInvitation(User user, string message)
        {
            if (user != null && !string.IsNullOrEmpty(user.QbUserId))
                QbUserService.SendInvitation(user.QbUserId, message);
        }

        public static void SyncUsers()
        {
            var qbUsers = QbUserService.GetAppUsers();

            using(var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                var localUsers = GetAll(connection);

                foreach(var user in localUsers)
                {
                    var userInfo = GetByQbUserId(user.QbUserId, qbUsers);
                    if (userInfo == null)
                    {
                        user.IsActive = false;
                    }
                    else
                    {
                        qbUsers.Remove(userInfo);
                        user.DateLastAccess = userInfo.LastAccess;
                    }

                    User.Save(user, connection);
                }

                foreach (var userInfo in qbUsers)
                {
                    var user = new User
                                   {
                                       QbUserId = userInfo.ID,
                                       IsActive = true,
                                       FirstName = userInfo.Name.Split(' ')[0],
                                       LastName = userInfo.Name.Split(' ').Length > 1 ? userInfo.Name.Split(' ')[1] : "",
                                       RoleName = userInfo.Roles[0].Name,
                                       Email = userInfo.Email ?? "",
                                       Name = userInfo.Name,
                                       DateLastAccess = userInfo.LastAccess
                                   };

                    User.Save(user, connection);
                }
            }
        }

        private static UserInfo GetByQbUserId(string qbUserId, IEnumerable<UserInfo> collection)
        {
            foreach (var user in collection)
            {
                if (user.ID == qbUserId)
                    return user;
            }
            return null;
        }

    }
}
