using System;
using System.ComponentModel;
using System.Security;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using DPI.ClientComp;
using DPI.Interfaces;

namespace Dpi.Central.Web
{
    public class Global : HttpApplication
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        public Global()
        {
            InitializeComponent();
        }

        protected void Application_Start(Object sender, EventArgs e)
        {
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            // Extract the forms authentication cookie
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = Context.Request.Cookies[cookieName];
            if (null == authCookie) {
                return;
            }

            FormsAuthenticationTicket authTicket = null;

            try {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            } catch (Exception ex) {
                throw new SecurityException(ex.Message);
            }

            if (null == authTicket) {
                throw new SecurityException("Cookie failed to decrypt.");
            }

            FormsIdentity id = new FormsIdentity(authTicket);

            // Attach the new principal object to the current HttpContext object.
            // This principal will flow throughout the request.
            Context.User = new GenericPrincipal(id, new string[0]);
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            IMap imap = Controller.Instance.Map;
            Exception ex = Server.GetLastError();

            FatalError.SaveErr(imap, ex, "DPI Public Web Site", ex.Source);
        }

        protected void Session_End(Object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
        }

        protected void Application_End(Object sender, EventArgs e)
        {
        }

        #region Web Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new Container();
        }

        #endregion
    }
}