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

using DPI.Components;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Services;

using System.Collections.Specialized;

namespace DPI.Ordering
{
	public class dPiTeleconnect :  System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.CheckBox chkPersistCookie;
		protected System.Web.UI.WebControls.Label lblMsg;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator1;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator2;
		protected System.Web.UI.HtmlControls.HtmlInputText txtUserName;
		protected System.Web.UI.HtmlControls.HtmlInputText txtUserPass;
		protected System.Web.UI.WebControls.ImageButton cmdLogin;
		protected System.Web.UI.WebControls.HyperLink HyperLink1;

	#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);

		}
		
		private void InitializeComponent()
		{    
			this.cmdLogin.Click += new System.Web.UI.ImageClickEventHandler(this.cmdLogin_Click);
			this.ID = "dPiTeleconnect";
			this.Load += new System.EventHandler(this.Page_Load);

		}
	#endregion

	#region	Event handlers
		void Page_Load(object sender, System.EventArgs e)
		{	
			try
			{
				if(IsPostBack)
					return;

				GetUser();				
				GetToken(); // rentway for now

				AutologinCheck();	
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		} 

		void cmdLogin_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string msg;
			try
			{	
				IUser user = DPI.ClientComp.User.GetUser(this);
	
				if (ViewState["Token"] != null)
					user.Token = (string)ViewState["Token"];

	
				if (new Cerberos().MakeCookie(Response, txtUserName.Value,
					txtUserPass.Value, out msg, ref user))
				{  
					
					if (user.AcctType.Trim().ToLower() !=  "autologin")
					{
						DPI.ClientComp.User.Save(this, user);
//						Response.Redirect("main/collapse.htm", false);
						Response.Redirect("main/MenuScreen.aspx", false);
					}

				}
				else
				{
					lblMsg.Text = "Login failed. Please check your username and/or password.";	

					if (msg != null)
						lblMsg.Text = msg;	// overrides default error message
				}
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
				lblMsg.Text = ex.Message;
			}
		}
	
	#endregion

	#region	Implementation
//		bool LogIt()
//		{
//			object oKey = new System.Configuration.AppSettingsReader().GetValue("LogUser",typeof(string));
//			if (oKey == null)
//			{
//				ErrLogSvc.LogError(this.ToString(), "N/A", "WebConfig.LogUser key is missing");
//				return false;
//			}
//			string lg = (string)oKey;
//			return lg.Trim().ToLower() == "yes";
//		}
		void GetToken()
		{
			if (Request.Form["Token"] == null)
				return;

			string t = Request.Form["Token"];
			ViewState["Token"] = Request.Form["Token"];
		}
		void GetUser()
		{
			if (Session["User"] != null)
				return; 

			IUser user = new User();
	

			
			if (HasCertificate((NameValueCollection)Request.Headers))
			{
				user.HasCertificate  = true;
				user.LoginStoreCode = GetCertStoreCode((NameValueCollection)Request.Headers); 
			}

			Session["User"]	= user; // save	
		}

		void AutologinCheck()
		{
			if (Request.QueryString["i"] == null)  // came from AutoLogin page
				return;
			
			if (Request.QueryString["i"].Trim() == string.Empty)
			{
				ErrLogSvc.LogError(null, "Logon.aspx", "N/A", "Query string is empty");	
				return;
			}
			AutoLogin();
		}

		void AutoLogin()
		{
			string msg = null;
			IUser user = (IUser)Session["User"];
			ITempAutologin ta = LoginSvc.GetAutoLogonByTempKey(int.Parse(Unpack(Request.QueryString["i"])));

			if (ta == null)
				ErrLogSvc.LogError(null, this.ToString(), "N/A", "TempAutoLogin is null, pars = " + Request.QueryString["i"]);	
				
			user.Token = ta.Token;
			user.StoreNumber = ta.StoreCode; //For RW we will store storenumber			

			if (new Cerberos().MakeCookie(Response, ta.AcctName, ta.PW, out msg, ref user))
			{
				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.Logon.ToString(), "N/A", "Autologin successful. Acct: " + ta.AcctName
					+ " storecode: " + user.LoginStoreCode + ", Clerk id: " + user.ClerkId);

				LoginSvc.DeleteTempAutoLogin(ta.Id);
				user.LoginStoreCode = GetStoreCode(user.LoginStoreCode, user.StoreNumber); // storecode for the given store number will take precedence over user account storecode
				
				Response.Redirect("main/collapse.htm", false);
			}
			else
			{
				
				ErrLogSvc.LogError(this.ToString(), "N/A", "Autologin failed. Redirected to Logon.aspx. " 
					+ "Acct name: " + ta.AcctName);

				ErrLogSvc.LogError(null, this.ToString(), "N/A", "AutoLogin failed, msg = " + msg);	
				Response.Redirect("Logon.aspx", false);
			} 
		}

		string Unpack(string par)
		{
			if (par.IndexOf("=") < 0)
				return null;

			return par.Substring(par.IndexOf("=") + 1,  par.Length - par.IndexOf("=") -1);  
		}

		bool HasCertificate(NameValueCollection bag)
		{
			string[] keys = bag.AllKeys; 
			
			for (int i = 0; i < keys.Length ; i++) 
				if(keys[i].ToString() == "SSL-ClientCert-Serial-Number")
					return true;

			return false;
		}

		string GetCertStoreCode(NameValueCollection bag)
		{	
			if (!HasCertificate(bag))
				return null;

			string[] keys = bag.AllKeys; 
			
			for (int i = 0; i < keys.Length ; i++) 
				if(keys[i].ToString() == "SSL-ClientCert-Subject-CN")
					if (Server.HtmlEncode(bag.GetValues(keys[i])[0]).Trim().Length > 0)
						return Server.HtmlEncode(bag.GetValues(keys[i])[0]);
			
			return null;
		}
		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr(null, ex, Session["User"], this.ToString());
		}
		string GetStoreCode(string storeCode, string storeNumber)
		{
			if (storeNumber == null)
				return storeCode;
	
			string storeNum = storeNumber.Trim();

			if (storeNum.Length == 0)
				return storeCode;
			
			ICorporation corp = StoreSvc.GetCorporation(storeCode);

			if (corp.RemLeadZerosFromStoreNum)
				storeNum = int.Parse(storeNumber).ToString();

			string sCode = StoreSvc.GetStoreCode(corp.CorpID, storeNum);

			if (storeCode.Length > 0)
				return sCode;

			return storeCode;
		}
	#endregion
	}
}