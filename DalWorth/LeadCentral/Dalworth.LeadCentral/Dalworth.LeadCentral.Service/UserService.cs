using System;
using System.Collections.Generic;
using System.Data;
using Intuit.Platform.Client.Core;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service
{
    public class UserService
    {
        public static User Save(string ticket, User user)
        {
            var currentUser = ContextHelper.GetCurrentUser(ticket);
            var context = ContextHelper.GetCurrentQbContext(ticket);
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            if (currentUser.Id != user.Id)
            {
                if (string.IsNullOrEmpty(user.QbUserId))
                {
                    user.QbUserId = QbUserService.GetUserId(context, servmanCustomer, user);

                    QbUserService.RemoveUserFromAllRoles(context, servmanCustomer, user.QbUserId);
                    if (user.IsActive)
                        QbUserService.AddUserToRole(context, servmanCustomer, user.QbUserId, user.RoleName);

                    SendInvitation(ticket, user, "");
                }
                else
                {
                    QbUserService.RemoveUserFromAllRoles(context, servmanCustomer, user.QbUserId);
                    if (user.IsActive)
                        QbUserService.AddUserToRole(context, servmanCustomer, user.QbUserId, user.RoleName);
                }
            }

            if (user.PhotoFileId == 0) user.PhotoFileId = null;

            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                user = User.Save(user, connection);
            }

            return user;
        }

        public static List<User> GetAll(IDbConnection connection)
        {
            return User.GetAll(connection);
        }

        public static List<User> GetAll(string ticket)
        {
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);
            return GetAll(servmanCustomer);
        }

        public static List<User> GetAll(ServmanCustomer servmanCustomer)
        {
            List<User> result;
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = GetAll(connection);
            }
            return result;
        }

        public static bool IsUserAdmin(UserInfo userInfo)
        {
            return QbUserHasRole(userInfo, UserRoleEnum.Administrator.ToString());
        }

        public static User GetUserByQbUserId(string ticket, string qbUserId)
        {
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            User result;

            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = User.GetByQbUserId(qbUserId, connection);
            }

            return result;
        }

        public static User SyncUser(ServmanCustomer servmanCustomer, UserInfo userInfo)
        {
            using(var connection = ServmanCustomerService.GetConnection(servmanCustomer))
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

        public static string GetUserRoleName(UserInfo userInfo)
        {
            var result = "";

            if (QbUserHasRole(userInfo, UserRoleEnum.BusinessPartner.ToString()))
                result = UserRoleEnum.BusinessPartner.ToString();

            if (QbUserHasRole(userInfo, UserRoleEnum.Staff.ToString()))
                result = UserRoleEnum.Staff.ToString();

            if (QbUserHasRole(userInfo, UserRoleEnum.Administrator.ToString()))
                result = UserRoleEnum.Administrator.ToString();

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

        public static void SendInvitation(string ticket, User user, string message)
        {
            var context = ContextHelper.GetCurrentQbContext(ticket);
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            if (user != null && !string.IsNullOrEmpty(user.QbUserId))
                QbUserService.SendInvitation(context, servmanCustomer, user.QbUserId, message);
        }

        public static void SyncUsers(PlatformSessionContext context, ServmanCustomer servmanCustomer)
        {
            var qbUsers = QbUserService.GetAppUsers(context, servmanCustomer);

            using(var connection = ServmanCustomerService.GetConnection(servmanCustomer))
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

        public static User RefreshUser(PlatformSessionContext context, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
