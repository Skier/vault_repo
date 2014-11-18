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
using System.Xml;
using System.Configuration;

using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Services;

namespace DPI.Ordering
{
	public class MenuScreen  : System.Web.UI.Page
	{
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			CustomInit();
		}
		
		private void InitializeComponent()
		{    
			this.lnkNewOrder.Click += new System.EventHandler(this.lnkNewOrder_Click);
			this.btnTutorial.Click += new System.EventHandler(this.btnTutorial_Click);
			this.lnkMonthlyPayment.Click += new System.EventHandler(this.lnkMonthlyPayment_Click);
			this.lnkGoGreed.Click += new System.EventHandler(this.lnkGoGreed_Click);
			this.lnkProductsOffered.Click += new System.EventHandler(this.lnkProductsOffered_Click);
			this.lnkAgentHotline.Click += new System.EventHandler(this.lnkAgentHotline_Click);
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#region Data
		protected System.Web.UI.WebControls.LinkButton lnkMonthlyPayment;
		protected System.Web.UI.WebControls.LinkButton lnkAgentHotline;
		protected System.Web.UI.WebControls.LinkButton lnkProductsOffered;
		protected System.Web.UI.WebControls.LinkButton lnkNewOrder;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.LinkButton btnTutorial;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.LinkButton lnkGoGreed;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnDocPath;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		#endregion
		#endregion
		
		#region Event Handlers
		private void Page_Load(object sender, System.EventArgs e)
		{
			LinkButton1.Attributes.Add("onclick", "window.open('mailform.htm', '_blank' ,'height= 600, width=500 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			lnkGoGreed.Attributes.Add("onclick", "CloseByName('dpipromoPopUp')");
			hdnDocPath.Value = ConfigurationSettings.AppSettings["DocPath"];
			//btnTutorial.Attributes.Add("onclick", "window.open('Docs/OnlineTutorial.pdf', '_blank' ,'height= 600, width=800 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			//if(LoginSvc.GetIfClerkIDRequested((IUser)Session["User"]))
		}

		void CustomInit()
		{
			SetGoGreed();
			lnkNewOrder.Visible = HttpContext.Current.User.IsInRole(lnkNewOrder.ID.Substring(3));
			//btnProductLookup.Visible = HttpContext.Current.User.IsInRole(btnProductLookup.ID.Substring(3));
			lnkMonthlyPayment.Visible = HttpContext.Current.User.IsInRole(lnkMonthlyPayment.ID.Substring(3));
		}
		private void lnkNewOrder_Click(object sender, System.EventArgs e)
		{
			IMap imap = IMapFactory.getIMap();
	
			IUser user = (IUser)Session["User"];
			WIP wip  = new NewOrderWip(user);
			imap.add(wip);
	
			Session["IMap"] = imap;
			Response.Redirect(wip.Current(), false);
		}
		
		private void lnkNewPayment_Click(object sender, System.EventArgs e)
		{
			IMap imap = IMapFactory.getIMap();
			IUser user = (IUser)Session["User"];
			WIP wip = new NewPaymentWip(user.DisplayName, user.ClerkId, user.LoginStoreCode);

			//	WIP wip  = new NewPaymentWip(HttpContext.Current.User.Identity.Name);
			imap.add(wip);
			Session["IMap"] = imap;
			Response.Redirect(wip.Current(), false);
		}

		private void lnkMonthlyPayment_Click(object sender, System.EventArgs e)
		{
			IMap imap = IMapFactory.getIMap();
			WIP wip = new MonthlyPaymentWip((IUser)Session["User"]);

			//WIP wip  = new MonthlyPaymentWip(HttpContext.Current.User.Identity.Name);
			imap.add(wip);
			Session["IMap"] = imap;
			Response.Redirect(wip.Current(), false);
		}

		private void btnPPLookup_Click(object sender, System.EventArgs e)
		{
			IMap imap = IMapFactory.getIMap();
			IUser user = (IUser)Session["User"];
			WIP wip = new PriceLookupWIP(user.DisplayName, user.ClerkId, user.LoginStoreCode);

			//	WIP wip  = new PriceLookupWIP(HttpContext.Current.User.Identity.Name);
			imap.add(wip);
			Session["IMap"] = imap;
			Response.Redirect(wip.Current(), false);		
		}

		private void lnkAgentHotline_Click(object sender, System.EventArgs e)
		{
			Session["IMap"] = null;
			Response.Redirect("AgentHotline.aspx", false);
		}

		private void lnkProductsOffered_Click(object sender, System.EventArgs e)
		{
			Session["IMap"] = null;
			Response.Redirect("LearnMore.aspx", false);
		}

		private void btnTutorial_Click(object sender, System.EventArgs e)
		{	
			lblErrMsg.Visible = false;

			try 
			{
				Session["IMap"] = null;
				IUser user = (IUser)Session["User"];

				if (user == null)
				{
					DoError("IUser is null", "User information is not found");
					return;
				}

				ICorporation ic =  StoreSvc.GetCorporation(user.LoginStoreCode);
				if (ic == null)
				{
					DoError("Corporation is null", "Corporation is not found");
					return;
				}
			
				if (ic.WebOrderingTrainingPage == null)
				{
					DoError("WebOrderingTrainingPage is null", "Tutorial is not found");
					return;
				}

				if (ic.WebOrderingTrainingPage.Trim().Length == 0)
				{
					DoError("WebOrderingTrainingPage is an empty string", "Tutorial is not found");
					return;
				}

				Response.Redirect(ic.WebOrderingTrainingPage, false);
			}
			catch (Exception ex) 
			{
				ErrLogSvc.LogError(this.ToString(), "N/A", 
					ex.Message + ", " + ex.StackTrace); 
			}
		}
		
		void btnProductLookup_Click(object sender, System.EventArgs e)
		{
			IMap imap = IMapFactory.getIMap();
			IUser user = (IUser)Session["User"];
			WIP wip = new PriceLookupWIP(user.DisplayName, user.ClerkId, user.LoginStoreCode);

			imap.add(wip);
			Session["IMap"] = imap;
			Response.Redirect(wip.Current(), false);
		}
		void lnkGoGreed_Click(object sender, System.EventArgs e)
		{
			WIP wip = new PromoWip(((IUser)Session["User"]).DisplayName, 
				((IUser)Session["User"]).ClerkId, 
				((IUser)Session["User"]).LoginStoreCode);

			IMap imap = IMapFactory.getIMap();
			imap.add(wip);
			Session["IMap"] = imap;
			
			Response.Redirect(wip.Current(), false);
		}
		#endregion
		
		#region Implementations
		void DoError(string logMessage, string uiMessage)
		{
			string name = "N/A";
			IUser user = (IUser)Session["User"];

			if (user != null)
				name = user.DisplayName;

			ErrLogSvc.LogError("MainMenu.aspx", name, logMessage);
			lblErrMsg.Text = "An error had occurred: " + uiMessage;
			lblErrMsg.Visible = true;
		}		
		
		void SetGoGreed()
		{
			if (!ContestResults.AllowIncentives((IUser)Session["User"]))
				return;

			if ((string)Session["showgogreed"] == "no")
				return;

			Session["showgogreed"] = "yes";
		}
		#endregion
	}
}