using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Intuit.Platform.Client.Core;
using Servman.Domain;
using Servman.Service;

namespace Servman.Intuit.Weborb
{
    public class BaseServices
    {
        public User GetCurrentUser()
        {
            return ContextHelper.GetCurrentUser();
        }

        public void TestInit()
        {
            InitTestEnvironment();
            Init();
        }

        public void Init()
        {
            InitPlatformSessionContext();
            InitCurrentCustomer();
        }

        private static void InitTestEnvironment()
        {
            HttpSessionState session = HttpContext.Current.Session;
            session.RemoveAll();
            session.Add(SessionSettings.REALM_ID_KEY, "182801782");
            session.Add(SessionSettings.DB_ID_KEY, "bfit8895q");
            session.Add(SessionSettings.APP_TOKEN_KEY, "2jhrxudzefegichpht3mcn5de27");
            session.Add(SessionSettings.TICKET_KEY, String.Empty);
        }

        public void InitPlatformSessionContext()
        {
            //HttpSessionState session = ContextHelper.GetCurrentSession();
            var context = new PlatformSessionContext
                              {
                                  AppToken = ContextHelper.GetAppToken(),
                                  Host = PlatformHost.WorkPlaceSecure,
                                  RequestAuthorizer = null
                              };

            string ticket = ContextHelper.GetTicket();
            if (!string.IsNullOrEmpty(ticket))
            {
                context.Ticket = ticket;
            }
            else
            {
                context.UserID = "valery@affilia.com"; context.Password = "Ariel1o1"; // admin
                //context.UserID = "valery@dekasoft.com.ua"; context.Password = "Dfkthbq"; // business partner
                //context.UserID = "yaworsky@gmail.com"; context.Password = "vfkmJhrf";  //employee
            }

            context.ForceAuthentication();

            ContextHelper.SetPlatformSessionContext(context);
        }

        public void InitCurrentCustomer()
        {
            var platformContext = ContextHelper.GetCurrentQbContext();
            var dbId = ContextHelper.GetDbId();
            var realmId = ContextHelper.GetRealmId();

            platformContext.ForceAuthentication();

            var users = platformContext.GetCurrentUserInfo();
            var qbUser = users.Count() > 0 ? users[0] : null;

            if (qbUser != null)
            {
                qbUser.Roles.Clear();
                qbUser.Roles.AddRange(platformContext.GetUserRole(dbId, qbUser.ID).Roles);
            }

            SDK.Configuration.ConnectionString = ConfigurationManager.AppSettings["mainConnectionString"];
            
            var currentCustomer = ServmanCustomerService.InitServmanCustomer(realmId);
            ContextHelper.SetCurrentCustomer(currentCustomer);

            var user = Service.UserService.SyncUser(qbUser, currentCustomer);
            ContextHelper.SetCurrentUser(user);
        }

    }
}