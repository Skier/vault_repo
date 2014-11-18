using System;
using System.Text;
using System.Collections;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Web.Security;

using DPI.Interfaces;
using DPI.Components;
using DPI.Services;

namespace DPI.ClientComp
{
	public class Cerberos
	{
		// with certificate data
		public bool MakeCookie(HttpResponse response, string userName, object passWord, 
								out string msg, ref IUser user)
		{
			IPermission[] perms;
			DPI_Err_Log.AddLogEntry(ErrLogSubSystems.Logon.ToString(), "N/A", "New user: " + userName + @"/" + (string)passWord);
	
			if (!LoginSvc.Validate(userName, passWord, ref user, out perms, out msg))
			{
				if (msg == null)
					msg = "Login failed. Please check your username and/or password.";	// Default error message
				
				return false;
			}
			return MakeTicket(response, userName, perms);
		}
	
		bool MakeTicket(HttpResponse response, string userName, IPermission[] perms)
		{

			FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
				1,										// version
				userName,					            // user
				DateTime.Now,							// issued
				DateTime.Now.AddHours(8),				// expires
				true,									// persistent user cookie
				new Roles(perms).ToString(),			// string
				FormsAuthentication.FormsCookiePath);	

			HttpCookie cookie 
				= new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));

			response.Cookies.Add(cookie);
			return true;
		}
	}
}