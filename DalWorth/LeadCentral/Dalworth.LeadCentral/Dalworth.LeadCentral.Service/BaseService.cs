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

        public static string Init(string intuitTicket, string realmId, string dbId)
        {
            var context = InitPlatformSessionContext(intuitTicket, dbId);
            
            var isQbo = context.ServiceType == IntuitServicesType.QBO;
            var servmanCustomer = InitCurrentCustomer(realmId, dbId, isQbo);

            var users = context.GetCurrentUserInfo();
            if (users == null || users.Count == 0)
                throw new Exception("Logged in user can not be null !");

            var currentQbUser = users[0];
            currentQbUser.Roles.Clear();
            currentQbUser.Roles.AddRange(context.GetUserRole(dbId, currentQbUser.ID).Roles);

            if (UserService.GetUserRoleName(currentQbUser) == UserRoleEnum.Administrator.ToString())
                UserService.SyncUsers(context, servmanCustomer);
            else
                UserService.SyncUser(servmanCustomer, currentQbUser);

            var servmanSession = InitCurrentSession(servmanCustomer, currentQbUser, context.Ticket);

            return servmanSession.Ticket;
        }

        public static string GetTestTicket()
        {
            var context = InitPlatformSessionContext(null, "bfit8895q");
            return context.Ticket;
        }

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
                context.UserID = "valery@affilia.com"; context.Password = "Ariel1o1"; // admin
                //context.UserID = "valery@dekasoft.com.ua"; context.Password = "Dfkthbq"; // staff
                //context.UserID = "valery@deka.kp.km.ua"; context.Password = "gfhjkm"; // staff
            } else
            {
                context.Ticket = intuitTicket;
            }

            context.ForceAuthentication();
            Host.Trace("LeadCentral", intuitTicket);
            context.ServiceType = context.GetIsRealmQBO(context.AppDbId)
                                      ? IntuitServicesType.QBO
                                      : IntuitServicesType.QBD;

            return context;
        }

        private static ServmanCustomer InitCurrentCustomer(string realmId, string dbId, bool isQbo)
        {
            var currentCustomer = ServmanCustomerService.FindByRealmId(realmId);
            if (currentCustomer == null)
            {
                currentCustomer = ServmanCustomerService.CreateServmanCustomer(realmId, dbId, isQbo);
                PhoneService.SyncTwilioPhones(currentCustomer);
            } else
            {
                currentCustomer.LastLoginDate = DateTime.Now;
                ServmanCustomer.Update(currentCustomer);
            }

            return currentCustomer;
        }

        private static ServmanSession InitCurrentSession(ServmanCustomer servmanCustomer, UserInfo user, string intuitTiket)
        {
            ServmanSession.DeactivateByUserId(user.ID);

            var session = new ServmanSession
                              {
                                  AppToken = ConfigurationManager.AppSettings["APP_TOKEN"],
                                  IntuitTicket = Cryptographer.Cryptographer.Encrypt(intuitTiket),
                                  QbUserId = user.ID,
                                  ServmanCustomerId = servmanCustomer.Id,
                                  SessionStart = DateTime.Now,
                                  Ticket = Guid.NewGuid().ToString(),
                                  IsActive = true
                              };

            return ServmanSession.Save(session);
        }

        public static bool IsBillingStatusOk(ServmanCustomer servmanCustomer)
        {
            var status = ApplicationStatus.GetByServmanCustomerId(servmanCustomer.Id);
            return (status == null || status.BillingStatus == "OK");
        }

        public static bool GetOAuthConnectionStatus(string ticket)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
                                   };

            if (ContextHelper.ActionDenied(ticket, allowedRoles))
                throw new SecurityException();

            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            var oAuthConnection = OAuthConnection.GetByCustomerId(servmanCustomer.Id);
            return oAuthConnection != null && oAuthConnection.IsActive;
        }

        public static User GetCurrentUser(string ticket)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff,
                                       UserRoleEnum.BusinessPartner
                                   };

            if (ContextHelper.ActionDenied(ticket, allowedRoles))
                throw new SecurityException();

            return ContextHelper.GetCurrentUser(ticket);
        }

        public static string GetCurrentRealmId(ServmanCustomer servmanCustomer)
        {
            return servmanCustomer.RealmId;
        }

/*
        public static ServmanCustomer UpdateServmanCustomer(string ticket, ServmanCustomer servmanCustomer)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
                                   };

            if (ContextHelper.ActionDenied(ticket, allowedRoles))
                throw new SecurityException();

            return ServmanCustomer.Save(servmanCustomer);
        }
*/

        public static string GetOAuthUrl(string ticket)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
                                   };

            if (ContextHelper.ActionDenied(ticket, allowedRoles))
                throw new SecurityException();

            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            return (ConfigurationManager.AppSettings["OAuthRequestUrl"] + "?realmId=" + servmanCustomer.RealmId);
        }

        public static string GetPaymentUrl(string ticket)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
                                   };

            if (ContextHelper.ActionDenied(ticket, allowedRoles))
                throw new SecurityException();

            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            return (ConfigurationManager.AppSettings["PaymentUrl"] + "?realmId=" + servmanCustomer.RealmId);
        }
    }
}
