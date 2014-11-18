using System;
using System.ComponentModel;
using System.Web;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account
{
    /// <summary>
    /// Summary description for Global.
    /// </summary>
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
            AuthenticationHelper.AuthenticateRequest();
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            IMap imap = IMapFactory.getIMap();
            Exception ex = Server.GetLastError();
            FatalError.SaveErr(imap, ex, "DPI Public Web Site", ex.Source);
        }

        protected void Session_End(Object sender, EventArgs e)
        {
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