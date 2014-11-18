using System;
using System.Web;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web
{
    public class Global : HttpApplication
    {
        #region Event handlers

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

        #endregion
    }
}