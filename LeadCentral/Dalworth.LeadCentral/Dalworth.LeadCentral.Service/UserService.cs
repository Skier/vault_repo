using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using Intuit.Platform.Client.Core;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service
{
    public class UserServiceException : Exception
    {
        public const int USER_EXISTS = 1;

        public enum  ErrorCodeEnum
        {
            IntuitError = 1,
            UserExists =2, 
            OtherError = 3
        }

        public ErrorCodeEnum ErrorCode { get; set; }

        public UserServiceException(ErrorCodeEnum errorCode, string message):base(message)
        {
            ErrorCode = errorCode;
        }

        public UserServiceException(ErrorCodeEnum errorCode, string message, Exception innerException):base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }

    public class UserService
    {
        #region Deactivate

        public static void Deactivate (User user, IDbConnection connection)
        {
            var context = ContextHelper.GetCurrentQbContext();

            try
            {
                context.RemoveUser(context.AppDbId, user.QbUserId);
                user.IsActive = false;
                User.Update(user, connection);
            }
            catch (Exception ex)
            {
                throw new UserServiceException(UserServiceException.ErrorCodeEnum.OtherError, ex.Message, ex);    
            }
        }

        #endregion 

        #region Create

        public static void Create (User user, IDbConnection connection)
        {
            var context = ContextHelper.GetCurrentQbContext();

            var roleInfo = context.GetRoleInfo(context.AppDbId).Find(tempRoleInfo => tempRoleInfo.Name == user.QbRoleName);
            try
            {
                user.QbUserId = context.ProvisionUser(context.AppDbId, user.Email, user.FirstName, user.LastName, roleInfo.ID);
                context.SendInvitation(context.AppDbId, user.QbUserId, "Invitation");
                User.Insert(user, connection);
            }
            catch (PlatformApiXmlHttpError ex)
            {
                    if (ex.ErrorCode == 111) // Already exists
                    {
                        if (user.Id > 0)
                            throw new UserServiceException(UserServiceException.ErrorCodeEnum.UserExists, user.Email);

                        try
                        {
                            var userInfo = context.GetUser(user.Email);

                            try
                            {
                                context.AddUserToRole(context.AppDbId, userInfo.ID, roleInfo.ID);
                            }
                            catch (PlatformApiXmlHttpError ex2)
                            {
                                if (ex2.ErrorCode != 113) // Already in the role
                                    throw ex2;
                            }
                            
                            context.SendInvitation(context.AppDbId, userInfo.ID, "Invitation");
                            user.QbUserId = userInfo.ID;
                            if (!string.IsNullOrEmpty(user.Email))
                                user.Email = userInfo.Email;

                            User.Insert(user, connection);
                            return;
                        }
                        catch (PlatformApiXmlHttpError ex1)
                        {
                            throw new UserServiceException(UserServiceException.ErrorCodeEnum.IntuitError, "Failed to add existing customer " + ex.Message, ex1);    
                        }
                    }

                    throw new UserServiceException(UserServiceException.ErrorCodeEnum.IntuitError, ex.Message,ex);    
            }
            catch (Exception ex)
            {
                throw new UserServiceException(UserServiceException.ErrorCodeEnum.OtherError, ex.Message, ex);    
            }
        }

        public static void Activate (User user, IDbConnection connection)
        {
            var context = ContextHelper.GetCurrentQbContext();
            var userInQb = context.GetUserRole(context.AppDbId, user.QbUserId);

            if (userInQb.Roles.Count > 0)
            {
                var userRole = userInQb.Roles.Find(roleInfo => roleInfo.Name == user.QbRoleName);
                if (userRole != null)
                {
                    user.IsActive = true;
                    User.Update(user, connection);
                    return;
                }

                context.RemoveUser(context.AppDbId, user.QbUserId);
            }

            var role = context.GetRoleInfo(context.AppDbId).Find(tempRoleInfo => tempRoleInfo.Name == user.QbRoleName);
            context.AddUserToRole(context.AppDbId, user.QbUserId, role.ID);
            user.IsActive = true;
            User.Update(user, connection);
        }

        public static void Update(User user,IDbConnection connection)
        {
            var context = ContextHelper.GetCurrentQbContext();
            var userInQb = context.GetUserRole(context.AppDbId, user.QbUserId);

            var alreadyInRole = false;
            foreach (var userRole in userInQb.Roles)
            {
                if (user.QbRoleName == userRole.Name)
                {
                    alreadyInRole = true;
                    continue;
                }

                if (user.QbRoleName != userRole.Name)
                {
                    context.RemoveUserFromRole(context.AppDbId, user.QbUserId, userRole.ID);
                }
            }

            if (!alreadyInRole)
            {
                var roleInfo =
                    context.GetRoleInfo(context.AppDbId).Find(tempRoleInfo => tempRoleInfo.Name == user.QbRoleName);
                context.AddUserToRole(context.AppDbId, user.QbUserId, roleInfo.ID);
            }

            User.Update(user, connection);
        }

        #endregion

        #region FindStaff

        public static List<User> FindStaff(IDbConnection connection)
        {
            var users = User.GetAll(connection);
            var result = new List<User>();
            foreach (var user in users)
            {
                if(!user.IsBusinessPartner)
                    result.Add(user);
            }
            return result;
        }

        #endregion 

        #region SyncUser

        public static User SyncUser(UserInfo userInfo, IDbConnection connection)
        {
            var user = User.FindByQbUserId(userInfo.ID, connection) ?? new User
                                                                          {
                                                                              QbUserId = userInfo.ID,
                                                                              DateCreated = DateTime.Now,
                                                                              FirstName = userInfo.FirstName,
                                                                              LastName = userInfo.LastName,
                                                                              ScreenName = String.IsNullOrEmpty(userInfo.ScreenName)
                                                                                               ? string.Format("{0} {1}", userInfo.FirstName, userInfo.LastName)
                                                                                               : userInfo.ScreenName,
                                                                              Email = userInfo.Email
                                                                          };

            user.DateLastAccess = userInfo.LastAccess;
            user.QbRoleName = GetUserRoleName(userInfo);

            User.Save(user, connection);

            return user;
        }

        #endregion

        #region GetUserRoleName

        public static string GetUserRoleName(UserInfo userInfo)
        {
            var isAdministrator = false;
            var isAccountant = false;
            var isStaff = false;
            var isBusinessPartner = false;

            foreach (var roleInfo in userInfo.Roles)
            {
                switch (roleInfo.Name)
                {
                    case User.UserRoleAdministrator:
                        isAdministrator = true;
                        break;
                    case User.UserRoleStaff:
                        isStaff = true;
                        break;
                    case User.UserRoleBusinessPartner:
                        isBusinessPartner = true;
                        break;
                    case User.UserRoleAccountant:
                        isAccountant = true;
                        break;
                }
            }

            if (isAdministrator)
                return User.UserRoleAdministrator;

            if (isBusinessPartner)
                return User.UserRoleBusinessPartner;

            if (isAccountant)
                return User.UserRoleAccountant;

            if (isStaff)
                return User.UserRoleStaff;

            return string.Empty;
        }

        #endregion

        #region SendInvitation

        public static void SendInvitation(User user, string message)
        {
            var context = ContextHelper.GetCurrentQbContext();

            if (user != null && !string.IsNullOrEmpty(user.QbUserId))
                context.SendInvitation(context.AppDbId, user.QbUserId, message);
        }

        #endregion

        #region SyncUsers

        public static void SyncUsers(PlatformSessionContext context, Customer customer, IDbConnection connection)
        {
            var qbUsers = context.GetUserRoles(customer.AppDbId);

            var localUsers = User.GetAll(connection);

            foreach(var user in localUsers)
            {
                var userInfo = qbUsers.Find(tmpUser => tmpUser.ID == user.QbUserId);

                if (userInfo == null)
                {
                    user.IsActive = false;
                }
                else
                {
                    qbUsers.Remove(userInfo);

                    if (!string.IsNullOrEmpty(userInfo.Email))
                        user.Email = userInfo.Email;

                    user.DateLastAccess = userInfo.LastAccess;
                    user.FirstName = userInfo.FirstName;
                    user.LastName = userInfo.LastName;
                    user.DateLastAccess = userInfo.LastAccess;
                    user.IsActive = true;
                    user.ScreenName = userInfo.Name;
                    user.QbRoleName = userInfo.Roles[0].Name;
                }

                User.Save(user, connection);
            }

            foreach (var userInfo in qbUsers)
            {
                var user = new User
                               {
                                    QbUserId = userInfo.ID,
                                    DateCreated = DateTime.Now,
                                    IsActive = true,
                                    FirstName = userInfo.FirstName,
                                    LastName = userInfo.LastName,
                                    QbRoleName = userInfo.Roles[0].Name,
                                    Email = userInfo.Email ?? "",
                                    ScreenName = userInfo.Name,
                                    DateLastAccess = userInfo.LastAccess
                                };

                User.Save(user, connection);
            }
        }

        #endregion

        #region FindByPrimaryKey

        public static User FindByPrimaryKey(int userId, IDbConnection connection)
        {
            var result = User.FindByPrimaryKey(userId, connection);
            if (result.BusinessPartnerId != null)
                result.RelatedBusinessPartner = BusinessPartnerService.FindById(result.BusinessPartnerId.Value, connection);

            return result;
        }

        #endregion

        #region Find

        public static List<User> Find(UserFilter filter, IDbConnection connection)
        {
            var result = User.LoadUsers(filter, connection);
                
            foreach (var user in result)
            {
                if (user.BusinessPartnerId != null)
                    user.RelatedBusinessPartner = BusinessPartner.FindByPrimaryKey(user.BusinessPartnerId.Value,
                                                                                    connection);
            }
           
            return result;
        }

        #endregion
    }
}
