using System;
using System.Collections.Generic;
using System.Configuration;
using Intuit.Platform.Client.Core;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.SDK;
using Configuration = Dalworth.LeadCentral.SDK.Configuration;

namespace Dalworth.LeadCentral.Service
{
    internal class ContextHelper
    {
        public static User GetCurrentUser(string ticket)
        {
            var servmanCustomer = GetCurrentCustomer(ticket);
            var servmanSession = GetCurrentSession(ticket);

            User user;
            using(var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                user = User.GetByQbUserId(servmanSession.QbUserId, connection);
            }
            user.RelatedCustomer = servmanCustomer;

            return user;
        }

        public static ServmanCustomer GetCurrentCustomer(string ticket)
        {
            var result = ServmanCustomerService.GetByTicketId(ticket);

            if (result == null)
                throw new DalworthException("Session expired");
            
            return result;
        }

        public static PlatformSessionContext GetCurrentQbContext(ServmanCustomer servmanCustomer, string ticket)
        {
            var servmanSession = GetCurrentSession(ticket);

            var context = new PlatformSessionContext
            {
                AppToken = ConfigurationManager.AppSettings["APP_TOKEN"],
                Host = PlatformHost.WorkPlaceSecure,
                RequestAuthorizer = null,
                AppDbId = servmanCustomer.AppDbId,
                Ticket = Cryptographer.Cryptographer.Decrypt(servmanSession.IntuitTicket),
            };

            context.ForceAuthentication();
            context.ServiceType = servmanCustomer.IsQBO
                                      ? IntuitServicesType.QBO
                                      : IntuitServicesType.QBD;

            try
            {
                context.GetCurrentUserInfo();
                return context;
            }
            catch(Exception)
            {
                throw new DalworthException("Session expired");
            }
        }

        public static PlatformSessionContext GetCurrentQbContext(string ticket)
        {
            var servmanCustomer = GetCurrentCustomer(ticket);

            return GetCurrentQbContext(servmanCustomer, ticket);
        }

        public static string GetDbId(string ticket)
        {
            return GetCurrentCustomer(ticket).AppDbId;
        }

        public static string GetRealmId(string ticket)
        {
            return GetCurrentCustomer(ticket).RealmId;
        }

        private static ServmanSession GetCurrentSession(string ticket)
        {
            Configuration.ConnectionString = ConfigurationManager.AppSettings["mainConnectionString"];
            return ServmanSession.GetByTicket(ticket);
        }

        public static bool ActionDenied(string ticket, List<UserRoleEnum> allowedRoles)
        {
            var user = GetCurrentUser(ticket);
            foreach (var role in allowedRoles)
            {
                if (user.RoleName == role.ToString())
                    return false;
            }
            return true;
        }
    }
}
