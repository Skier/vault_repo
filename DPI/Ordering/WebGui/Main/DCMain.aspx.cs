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

using DPI.Services;
using DPI.Interfaces;
using DPI.Ordering;
using DPI.ClientComp;

namespace DPI.Ordering
{
	public class DCMain : System.Web.UI.Page
	{
	#region Web Form Designer generated code
		protected System.Web.UI.WebControls.Image imgWorkflow;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.ImageButton btnMain;
		protected System.Web.UI.WebControls.RadioButtonList rblCardOption;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtSalesId;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{    
			this.btnMain.Click += new System.Web.UI.ImageClickEventHandler(this.btnMain_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
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

				MultiClickBlocker.Block(this, btnNext);
				MultiClickBlocker.Block(this, btnMain);
			}
			catch (Exception ex)
			{
				// add error code
			}
		}
 		void btnMain_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				Response.Redirect("MenuScreen.aspx", false);
			}
			catch (Exception ex)
			{
				// add error code
			}
		}	
		void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				if (!(ValidateSalesId()))
					return;

				IUser user = (IUser)Session["User"];
				if (rblCardOption.SelectedValue == "New")  
				{
					SaveAndTransfer(new DebCardWip(user.DisplayName, user.ClerkId, user.LoginStoreCode));
					return;
				}

				SaveAndTransfer(new DebCardReloadWip(user.DisplayName, user.ClerkId, user.LoginStoreCode));
			}
			catch (Exception ex)
			{
				// add error code
			}
		}
	#endregion	

	#region Implementation		
		void SaveAndTransfer(WIP wip)
		{
			try
			{
				if (ViewState["SalesId"] != null)
				{
					wip.ClerkId = (string)ViewState["SalesId"];
				}

				Session["IMap"] = IMapFactory.getIMap();
				((IMap)Session["IMap"]).add(wip);
				
				Response.Redirect(wip.Current(), false);				
			}
			catch (Exception ex)
			{
				// add error code
			}
		}
		bool ValidateSalesId()
		{
			this.lblErrMsg.Visible = false;
			this.lblErrMsg.Text = "";

			if(!(LoginSvc.GetIfClerkIDRequested((IUser)Session["User"])))
				return true;
			
			if (txtSalesId.Text.Trim().Length == 0)
			{
				lblErrMsg.Text = "Please enter Co-Worker Id";
				lblErrMsg.Visible = true;
				return false;
			}

			ViewState["SalesId"] = txtSalesId.Text.Trim();
			return true;
		}	
	#endregion	
	}
}