using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Web.Security;
using System.Configuration;

using DPI.Components;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Services;

namespace DPI.Ordering
{
	public class AutoLogon :  System.Web.UI.Page
	{
		static string url = null;

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		#region Event Handlers
		void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				if (IsPostBack)
					return;

				if (url == null)
					url = ConfigurationSettings.AppSettings["MainURL"];
			
				string ai =  GetAcctInfo();

				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.Logon.ToString(), 
					"N/A", "Ready. Private IP: " 
					+ Request.Form["IP"]
					+ ", Public IP: " 
					+ Request.ServerVariables["REMOTE_ADDR"] 
					+ "AcctInfo found: " 
					+ ai);

				Response.Redirect(url + ai, false);
			}
			catch (Exception ex)
			{
				FatalError.SaveErr(null, ex, null, this.ToString());
			}
		} 
		#endregion

		#region Implementation
		
		string GetAcctInfo()
		{
			string privateIP = Request.Form["IP"];
			string publicIP  = Request.ServerVariables["REMOTE_ADDR"];

			DPI_Err_Log.AddLogEntry(ErrLogSubSystems.Logon.ToString(), "N/A", "Begin. Private IP: " + privateIP
				+ ", Public IP: " + publicIP);


			if (privateIP == null)
			{
				SaveError("Private IP address is null");	
				return "";
			}

			if (privateIP.Trim().Length == 0)
			{
				SaveError("Private IP address is empty string");	
				return "";
			}

			if (publicIP == null)
			{
				SaveError("Public IP address is null");	
				return "";
			}
			
			if (publicIP.Trim().Length == 0)
			{
				SaveError("Public IP address is empty string");	
				return "";
			}

			return AutoLogin(privateIP.Trim(), publicIP.Trim());
		}

		string AutoLogin(string privateIP, string pubIP)
		{
			string acct = null;
			object pw   = null;

			LoginSvc.GetLoginInfo(pubIP, privateIP, out acct, out pw);

			if (acct == null)
			{
				SaveError("Acount not found, private ip:" + privateIP + ", public IP: " + pubIP );	
				return "";
			}

			if (pw == null)
			{
				SaveError("Password is not found");	
				return "";
			}

			return Const.TEMPKEY + "=" + LoginSvc.GetTempLoginInfo(acct, (string)pw).Id.ToString();
		}
		void SaveError(string msg)
		{
			ErrLogSvc.LogError(null, this.ToString(), "N/A", msg);	
		}
		#endregion
	}
}