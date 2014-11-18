using System;
using System.Configuration;
using System.IO;
using System.Security;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Xml;

namespace Dpi.Central.Web.Account
{
	internal sealed class AuthenticationHelper
	{
        private static bool _initialized = false;
        private static int _timeout;

		private AuthenticationHelper()
		{
		}

        internal static void AuthenticateRequest()
        {
            HttpContext cxt = HttpContext.Current;

            // Extract the forms authentication cookie.
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = cxt.Request.Cookies[cookieName];
            if (null == authCookie) {
                return;
            }

            FormsAuthenticationTicket authTicket;

            try {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            } catch (Exception ex) {
                throw new SecurityException("Cookie failed to decrypt.", ex);
            }

            if (null == authTicket) {
                throw new SecurityException("Cookie failed to decrypt.");
            }

            // It is possible that server time is not quite synchronized with
            // client time. So, we set cookie expiration time always +1 year
            // and check expiration time through the authentication ticket.
            if (authTicket.Expired) {
                return;
            }

            FormsIdentity id = new FormsIdentity(authTicket);

            // Attach the new principal object to the current HttpContext object.
            // This principal will flow throughout the request.
            cxt.User = new GenericPrincipal(id, new string[0]);

            if (FormsAuthentication.SlidingExpiration) {
                // Set new (renew) authentication cookie. New exp date is current time plus timeout.
                SetAuthCookie(id.Name);
            }
        }

        internal static void SetAuthCookie(string userName) 
        {
            Initialize();
            HttpCookie cookie = GetAuthCookie(userName, true);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        private static HttpCookie GetAuthCookie(string userName, bool createPersistentCookie) 
        {
            if (userName == null) {
                userName = string.Empty;
            }

            FormsAuthenticationTicket ticket = GetAuthTicket(userName, createPersistentCookie);

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            if ((encryptedTicket == null) || (encryptedTicket.Length < 1)) {
                throw new HttpException("Unable to encrypt authentication ticket.");
            }

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            cookie.Path = FormsAuthentication.FormsCookiePath;
            cookie.Secure = FormsAuthentication.RequireSSL;

            if (ticket.IsPersistent) {
                // It is possible that server time is not quite synchronized with
                // client time. So, set cookie expiration time +1 year and
                // check auth ticket expiration time during request authentication.
                cookie.Expires = ticket.Expiration.AddYears(1);
            }
            
            return cookie;
        }

        private static FormsAuthenticationTicket GetAuthTicket(string userName, bool createPersistentCookie) 
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(_timeout), createPersistentCookie, string.Empty, FormsAuthentication.FormsCookiePath);
            return ticket;
        }

        private static void Initialize()
        {
            if (!_initialized) {
                lock (typeof(AuthenticationHelper)) {
                    if (!_initialized) {
                        FormsAuthentication.Initialize();

                        try {
                            XmlDocument doc = new XmlDocument();

                            string path = HttpContext.Current.Server.MapPath("~/Web.config");
                
                            doc.Load(path);
                
                            XmlElement elem = (XmlElement)doc.SelectSingleNode("//system.web/authentication/forms");

                            if (elem == null) {
                                throw new InvalidOperationException("<forms> section not found in Web.config file.");
                            }

                            _timeout = int.Parse(elem.GetAttribute("timeout"));

                            _initialized = true;
                        } catch (FileNotFoundException ex) {
                            throw new ConfigurationException("Web.config file not found.", ex);
                        }
                    }
                }
            }
        }
	}
}
