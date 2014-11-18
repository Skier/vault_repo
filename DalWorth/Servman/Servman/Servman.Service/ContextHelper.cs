using System.Web;
using System.Web.SessionState;
using Intuit.Platform.Client.Core;
using Servman.Domain;
using Servman.SDK;

namespace Servman.Service
{
    public class ContextHelper
    {
        public static User GetCurrentUser()
        {
            var session = GetCurrentSession();
            return (User)session[SessionSettings.CURRENT_USER_KEY];
        }

        public static ServmanCustomer GetCurrentCustomer()
        {
            var session = GetCurrentSession();
            var result = (ServmanCustomer)session[SessionSettings.CURRENT_SERVMAN_CUSTOMER_KEY];

            if (result == null)
                throw new DalworthException("Session expired");
            
            return result;
        }

        public static PlatformSessionContext GetCurrentQbContext()
        {
            HttpSessionState session = GetCurrentSession();
            return (PlatformSessionContext)session[SessionSettings.PLATFORM_CONTEXT_KEY];
        }

        public static string GetDbId()
        {
            HttpSessionState session = GetCurrentSession();
            return (string)session[SessionSettings.DB_ID_KEY];
        }

        public static string GetAppToken()
        {
            HttpSessionState session = GetCurrentSession();
            return (string)session[SessionSettings.APP_TOKEN_KEY];
        }

        public static string GetTicket()
        {
            HttpSessionState session = GetCurrentSession();
            return (string)session[SessionSettings.TICKET_KEY];
        }

        public static string GetRealmId()
        {
            HttpSessionState session = GetCurrentSession();
            return (string)session[SessionSettings.REALM_ID_KEY];
        }

        private static HttpSessionState GetCurrentSession()
        {
            return HttpContext.Current.Session;
        }

        public static void SetPlatformSessionContext(PlatformSessionContext context)
        {
            if (GetCurrentSession()[SessionSettings.PLATFORM_CONTEXT_KEY] == null)
                GetCurrentSession().Add(SessionSettings.PLATFORM_CONTEXT_KEY, context);
            else
                GetCurrentSession()[SessionSettings.PLATFORM_CONTEXT_KEY] = context;
        }

        public static void SetCurrentCustomer(ServmanCustomer currentCustomer)
        {
            if (GetCurrentSession()[SessionSettings.CURRENT_SERVMAN_CUSTOMER_KEY] == null)
                GetCurrentSession().Add(SessionSettings.CURRENT_SERVMAN_CUSTOMER_KEY, currentCustomer);
            else
                GetCurrentSession()[SessionSettings.CURRENT_SERVMAN_CUSTOMER_KEY] = currentCustomer;
        }

        public static void SetCurrentUser(User user)
        {
            if (GetCurrentSession()[SessionSettings.CURRENT_USER_KEY] == null)
                GetCurrentSession().Add(SessionSettings.CURRENT_USER_KEY, user);
            else
                GetCurrentSession()[SessionSettings.CURRENT_USER_KEY] = user;
        }

        public static void SetRealmId(string realmId)
        {
            if (GetCurrentSession()[SessionSettings.REALM_ID_KEY] == null)
                GetCurrentSession().Add(SessionSettings.REALM_ID_KEY, realmId);
            else
                GetCurrentSession()[SessionSettings.REALM_ID_KEY] = realmId;
        }
    }
}
