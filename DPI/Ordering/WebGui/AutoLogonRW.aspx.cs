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
	public class AutoLogonRW :  System.Web.UI.Page
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

				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.Logon.ToString(), "N/A", 
					"Ready. Token: " + Request.Form["token"]
					+ ", StoreCode: " + Request.Form["storeId"]
					+ ", UserName: " + Request.Form["username"]
					+ ", transactionType: " + Request.Form["transactionType"]
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
			string token			= Request.Form["token"];			
			string storeNum		= Request.Form["storeId"];
			string userName			= Request.Form["username"];
			object password			= Request.Form["password"];
			string transactionType	= Request.Form["transactionType"];
			
			DPI_Err_Log.AddLogEntry(ErrLogSubSystems.Logon.ToString(), "N/A", "Begin. Token: " + token
				+ ", storeNum: " + storeNum
				+ ", UserName: " + userName
				+ ", transactionType: " + transactionType);

			if (token == null)
			{
				SaveError("token is null");	
				return "";
			}

			if (token.Trim().Length == 0)
			{
				SaveError("token is empty string");	
				return "";
			}

			if (storeNum == null)
			{
				SaveError("storeCode is null");	
				return "";
			}
			
			if (storeNum.Trim().Length == 0)
			{
				SaveError("StoreCode is empty string");	
				return "";
			}

			if (userName == null)
			{
				SaveError("UserName is null");	
				return "";
			}
			
			if (userName.Trim().Length == 0)
			{
				SaveError("UserName is empty string");	
				return "";
			}

			if (password == null)
			{
				SaveError("Password is null");	
				return "";
			}
			
			if (((string)password).Trim().Length == 0)
			{
				SaveError("Password is empty");	
				return "";
			}

			if (transactionType == null)
			{
				SaveError("TransactionType is null");	
				return "";
			}
			
			if (transactionType.Trim().Length == 0)
			{
				SaveError("TransactionType is empty string");	
				return "";
			}

			return AutoLogin(token.Trim(), storeNum.Trim(), userName.Trim(), password, transactionType.Trim());
		}

		string AutoLogin(string token, string storeNum, string userName, object pw, string tranType)
		{
			if (!LoginSvc.Validate(userName, (string)pw))
			{
				SaveError("User is not recognized, UserName:" + userName);	
				return "";
			}
			return Const.TEMPKEY + "=" + LoginSvc.GetTempLoginInfo(userName, (string)pw, token, storeNum, tranType).Id.ToString();
		}
		void SaveError(string msg)
		{
			ErrLogSvc.LogError(null, this.ToString(), "N/A", msg);	
		}		
		#endregion
	}
}