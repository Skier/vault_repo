using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security;
using Intuit.Platform.Client.Core;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service
{
    public class BaseService
    {
        private const string ModeDevelopment = "DEVELOPMENT";

        #region Init

        public static string Init()
        {
            return Init(null, "182801782", "bfit8895q");
        }

        public static string Init(string intuitTicket, string realmId, string dbId)
        {
            var context = InitPlatformSessionContext(intuitTicket, dbId);
            
            var isQbo = context.ServiceType == IntuitServicesType.QBO;
            var customer = InitCurrentCustomer(realmId, dbId, isQbo);

            var users = context.GetCurrentUserInfo();
            if (users == null || users.Count == 0)
                throw new Exception("Logged in user can not be null !");

            var currentQbUser = users[0];
            currentQbUser.Roles.Clear();
            currentQbUser.Roles.AddRange(context.GetUserRole(dbId, currentQbUser.ID).Roles);

            var session = InitCurrentSession(customer, currentQbUser, context.Ticket);
            ContextHelper.Ticket = session.Ticket;

            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                if (UserService.GetUserRoleName(currentQbUser) == UserRoleEnum.Administrator.ToString())
                    UserService.SyncUsers(context, customer, connection);
                else
                    UserService.SyncUser(currentQbUser, connection);
            }

            return session.Ticket;
        }

        #endregion

        #region InitPlatformSessionContext

        private static PlatformSessionContext InitPlatformSessionContext(string intuitTicket, string dbId)
        {
            var context = new PlatformSessionContext
                              {
                                  AppToken = ConfigurationManager.AppSettings["APP_TOKEN"],
                                  Host = PlatformHost.WorkPlaceSecure,
                                  RequestAuthorizer = null,
                                  AppDbId = dbId
                              };

            if (ConfigurationManager.AppSettings["Mode"] == ModeDevelopment && string.IsNullOrEmpty(intuitTicket))
            {
                context.UserID = ConfigurationManager.AppSettings["test_login"]; context.Password = ConfigurationManager.AppSettings["test_password"]; 
                //context.UserID = "valery@dekasoft.com.ua"; context.Password = "Dfkthbq"; // staff
                //context.UserID = "valery@deka.kp.km.ua"; context.Password = "gfhjkm"; // staff
            } else
            {
                context.Ticket = intuitTicket;
            }

            context.ForceAuthentication();
            context.ServiceType = context.GetIsRealmQBO(context.AppDbId)
                                      ? IntuitServicesType.QBO
                                      : IntuitServicesType.QBD;

            return context;
        }

        #endregion

        #region InitCurrentCustomer

        private static Customer InitCurrentCustomer(string realmId, string dbId, bool isQbo)
        {
            var currentCustomer = CustomerService.FindByRealmId(realmId);
            if (currentCustomer == null)
                currentCustomer = CustomerService.CreateServmanCustomer(realmId, dbId, isQbo);
            else
                Customer.Update(currentCustomer);

            return currentCustomer;
        }

        #endregion

        #region InitCurrentSession

        private static Session InitCurrentSession(Customer customer, UserInfo user, string intuitTiket)
        {
            Session.DeactivateByUserId(user.ID);

            var session = new Session
                              {
                                  AppToken = ConfigurationManager.AppSettings["APP_TOKEN"],
                                  IntuitTicket = Cryptographer.Cryptographer.Encrypt(intuitTiket),
                                  QbUserId = user.ID,
                                  CustomerId = customer.Id,
                                  SessionStart = DateTime.Now,
                                  Ticket = Guid.NewGuid().ToString(),
                                  IsActive = true
                              };

            return Session.Save(session);
        }

        #endregion

        #region GetOAuthConnectionStatus

        public static bool GetOAuthConnectionStatus()
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
                                   };

            if (ContextHelper.ActionDenied(allowedRoles))
                throw new SecurityException();

            var servmanCustomer = ContextHelper.GetCurrentCustomer();

            var oAuthConnection = OAuthConnection.GetByCustomerId(servmanCustomer.Id);
            return oAuthConnection != null && oAuthConnection.IsActive;
        }

        #endregion

        #region GetCurrentUser

        public static User GetCurrentUser()
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff,
                                       UserRoleEnum.BusinessPartner
                                   };

            if (ContextHelper.ActionDenied(allowedRoles))
                throw new SecurityException();

            return ContextHelper.GetCurrentUser();
        }

        #endregion


        #region TO BE REMOVED

        #region GetOAuthUrl

        private static string GetOAuthUrl()
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
                                   };

            if (ContextHelper.ActionDenied(allowedRoles))
                throw new SecurityException();

            var servmanCustomer = ContextHelper.GetCurrentCustomer();

            return (ConfigurationManager.AppSettings["OAuthRequestUrl"] + "?realmId=" + servmanCustomer.RealmId);
        }

        #endregion

        #region GetPaymentUrl

        private  static string GetPaymentUrl()
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
                                   };

            if (ContextHelper.ActionDenied( allowedRoles))
                throw new SecurityException();

            var servmanCustomer = ContextHelper.GetCurrentCustomer();

            return (ConfigurationManager.AppSettings["PaymentUrl"] + "?realmId=" + servmanCustomer.RealmId);
        }

        #endregion

        #region SignOut

        private static void SignOut()
        {
            var context = ContextHelper.GetCurrentQbContext();
            if ( context == null)
                return;

            context.SignOut();
        }

        #endregion

        #endregion
    }
}
