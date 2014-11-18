using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Intuit.Ipp.Saml;
using Intuit.Platform.Client.Core;
using Dalworth.LeadCentral.Domain;
using Dalworth.Common.SDK;
using Configuration = Dalworth.Common.SDK.Configuration;

namespace Dalworth.LeadCentral.Service
{
    public class ContextHelper
    {
        #region Constants 

        private const string TicketKey =                "leadcentralticket";
        private const string DbIdKey =                  "leadcentraldbid";
        private const string AppBaseUrlKey =            "WORKPLACE_SERVER";
        private const string LeadCentralSessionKey =    "LEAD_CENTRAL_SESSION";
        private const string LeadCentralCustomerKey =   "LEAD_CENTRAL_CUSTOMER";
        private const string LeadCentralUserKey =       "LEAD_CENTRAL_USER";

        private static readonly string PrivateKeyName = ConfigurationManager.AppSettings["privateKeyLoc"];
        private static readonly string PrivateKeyPassword = ConfigurationManager.AppSettings["privateKeyPass"];
        private static readonly string IntuitPublicKeyName = ConfigurationManager.AppSettings["IntuitPublicKey"];

        #endregion

        #region Ticket

        public static string Ticket
        {
            get { return (string) HttpContext.Current.Session[TicketKey]; }
            set { HttpContext.Current.Session[TicketKey] = value; }
        }

        #endregion 

        #region GetCurrentUser

        public static User GetCurrentUser()
        {
            var user = (User)HttpContext.Current.Session[LeadCentralUserKey];
            if (user != null)
                return user;

            var customer = GetCurrentCustomer();
            var session = GetCurrentSession();

            using(var connection = CustomerService.GetConnection(customer))
            {
                user = User.FindByQbUserId(session.QbUserId, connection);
            }

            HttpContext.Current.Session[LeadCentralUserKey] = user;

            return user;
        }

        #endregion

        #region GetAppToken

        public static string GetAppToken()
        {
            return ConfigurationManager.AppSettings["APP_TOKEN"];
        }

        #endregion

        #region  GetAppUrl

        public static string GetAppUrl()
        {
            var appBaseUrl = ConfigurationManager.AppSettings[AppBaseUrlKey];
            var dbId = CurrentDbId;
            return appBaseUrl + dbId + "&#_top";
        }

        #endregion

        #region GetCurrentCustomer

        public static Customer GetCurrentCustomer()
        {
            var customer = (Domain.Customer)HttpContext.Current.Session[LeadCentralCustomerKey];
            if (customer != null)
                return customer;


            var session = GetCurrentSession();
            if (session == null)
            {
                throw new DalworthException("Session expired");
            }

            var result = CustomerService.GetByTicketId(Ticket);

            if (result == null)
                throw new DalworthException(string.Format("Customer not found - ticket:{0}", Ticket));

            HttpContext.Current.Session[LeadCentralCustomerKey] = result;
            return result;
        }

        #endregion

        #region Fault

        public  static void Fault()
        {
            throw new DalworthException("Session expired");
        }

        #endregion 

        #region GetCurrentQbContext

        public static PlatformSessionContext GetCurrentQbContext(Customer servmanCustomer)
        {
            var session = GetCurrentSession();

            var context = new PlatformSessionContext
            {
                AppToken = ConfigurationManager.AppSettings["APP_TOKEN"],
                Host = PlatformHost.WorkPlaceSecure,
                RequestAuthorizer = null,
                AppDbId = servmanCustomer.AppDbId,
                Ticket = Cryptographer.Cryptographer.Decrypt(session.IntuitTicket),
            };

            context.ForceAuthentication();
            context.ServiceType = servmanCustomer.IsQBO
                                      ? IntuitServicesType.QBO
                                      : IntuitServicesType.QBD;

            try
            {
                var currentUsers = context.GetCurrentUserInfo();
                return context;
            }
            catch(Exception)
            {
                throw new DalworthException("Session expired");
            }
        }

        #endregion 

        #region GetCurrentQbContext

        public static PlatformSessionContext GetCurrentQbContext()
        {
            var servmanCustomer = GetCurrentCustomer();
            return servmanCustomer == null ? null : GetCurrentQbContext(servmanCustomer);
        }

        #endregion

        #region ActionDenied

        public static bool ActionDenied(List<UserRoleEnum> allowedRoles)
        {
            var user = GetCurrentUser();
            
            foreach (var role in allowedRoles)
            {
                if (user.QbRoleName == role.ToString())
                    return false;
            }
            return true;
        }

        #endregion

        #region InitContext 

        public static void InitContext()
        {
            var request = HttpContext.Current.Request;
            var response = HttpContext.Current.Response;

            if (request.TotalBytes > 0)
            {
                var myCert = LoadCertificate(PrivateKeyName, PrivateKeyPassword);
                var intuitPublicKey = new X509Certificate2(GetKeyPathName(IntuitPublicKeyName));

                var samlResponse = ServiceProvider.GetSamlResponse(request, myCert, intuitPublicKey);

                response.AddHeader("P3P", "CP=\"CAO DSP COR CURa ADMa DEVa PSAa CONo OUR STP BUS PHY ONL UNI FIN COM NAV INT DEM STA\"");

                var dbId = samlResponse.TargetUrl.Substring(samlResponse.TargetUrl.LastIndexOf("=") + 1);
                var realmId = samlResponse.RealmId;
                var intuitTicket = samlResponse.LoginTicket;

                if (RealmIsIncorrect(realmId))
                    throw new DalworthException("RealmId is incorrect");

                Ticket = BaseService.Init(intuitTicket, realmId, dbId);
                CurrentDbId = dbId;
            }
            else if (ConfigurationManager.AppSettings["Mode"] == "DEVELOPMENT")
            {
                Ticket = BaseService.Init();
                CurrentDbId = "bgbxqtrc3";
            } else
            {
                throw new DalworthException("Intuit request is empty");
            }
        }

        #endregion 

        #region private

        private static string CurrentDbId
        {
            get
            {
                var request = HttpContext.Current.Request;
                return request.Cookies[DbIdKey] != null ? request.Cookies[DbIdKey].Value : string.Empty;
            }
            set
            {
                var response = HttpContext.Current.Response;
                if (response.Cookies[DbIdKey] == null)
                    response.Cookies.Add(new HttpCookie(DbIdKey));

                response.Cookies[DbIdKey].Value = value;
            }
        }

        private static Session GetCurrentSession()
        {
            var session = (Domain.Session)HttpContext.Current.Session[LeadCentralSessionKey];
            if (session != null)
                return session;

            Configuration.ConnectionString = ConfigurationManager.AppSettings["mainConnectionString"];
            session = Session.GetByTicket(Ticket);
            HttpContext.Current.Session[LeadCentralSessionKey] = session;

            return session;
        }

        private static bool RealmIsIncorrect(string realmId)
        {
            return (string.Compare(StringUtil.ExtractDigits(realmId), realmId) != 0 && realmId.Length == 8);
        }

        private static string GetKeyPathName(string keyName)
        {
            return HttpContext.Current.Server.MapPath("~/Keys/" + keyName);
        }

        private static X509Certificate2 LoadCertificate(string fileName, string password)
        {

            var path = GetKeyPathName(fileName);
            if (!System.IO.File.Exists(path))
                throw new DalworthException("The certificate file " + fileName + " doesn't exist at path " + path);

            try
            {
                return new X509Certificate2(path, password, X509KeyStorageFlags.MachineKeySet);
            }
            catch (Exception exception)
            {
                throw new DalworthException("The certificate file " + fileName + " couldn't be loaded - " + exception.Message);
            }
        }

        #endregion 
    }
}
