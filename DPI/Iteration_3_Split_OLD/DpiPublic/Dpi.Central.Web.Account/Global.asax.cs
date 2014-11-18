using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account 
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

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

            if (authTicket.Expiration < DateTime.Now) {
                return;
            }

            FormsIdentity id = new FormsIdentity(authTicket);

            // Attach the new principal object to the current HttpContext object.
            // This principal will flow throughout the request.
            Context.User = new GenericPrincipal(id, new string[0]);
		}

		protected void Application_Error(Object sender, EventArgs e)
		{
		    IMap imap = IMapFactory.getIMap();
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
			this.components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

