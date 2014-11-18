using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web
{
    public class Global : HttpApplication
    {
        #region Event handlers

        protected void Application_Start(Object sender, EventArgs e) {
        }

        protected void Session_Start(Object sender, EventArgs e) {
        }

        protected void Application_BeginRequest(Object sender, EventArgs e) {
        }

        protected void Application_EndRequest(Object sender, EventArgs e) {
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e) {
            // Extract the forms authentication cookie
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = Request.Cookies[cookieName];
            if (null == authCookie) {
                return;
            }

            FormsAuthenticationTicket authTicket;

            try {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            } catch (Exception) {
                return;
                //TODO: throw new SecurityException(ex.Message);
            }

            if (null == authTicket) {
                return;
                //TODO: throw new SecurityException("Cookie failed to decrypt.");
            }

            FormsIdentity id = new FormsIdentity(authTicket);

            // Attach the new principal object to the current HttpContext object.
            // This principal will flow throughout the request.
            Context.User = new GenericPrincipal(id, new string[0]);
        }

        protected void Application_Error(Object sender, EventArgs e) {
            IMap imap = IMapFactory.getIMap();
            Exception ex = Server.GetLastError();
            FatalError.SaveErr(imap, ex, "DPI Public Web Site", ex.Source);
        }

        protected void Session_End(Object sender, EventArgs e) {
            FormsAuthentication.SignOut();
        }

        protected void Application_End(Object sender, EventArgs e) {
        }

        #endregion
    }
}