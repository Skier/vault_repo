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
	public class FindCustomer : BasePage
	{	
	
	#region Web Form Designer generated code
		#region Data
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.TextBox txtAccNumber;
		protected System.Web.UI.WebControls.TextBox txtNumber;
		protected System.Web.UI.WebControls.TextBox txtNxx;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.Image imgWorkflow;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator1;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator2;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator3;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator4;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtSalesId;
		protected System.Web.UI.WebControls.TextBox txtNpa;
	#endregion	

		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			CustomInit();
		}
		
		private void InitializeComponent()
		{    
			this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrevious_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
	#endregion	
		
	#region Event handlers
		private void Page_Load(object sender, System.EventArgs e)
		{
			MultiClickBlocker.Block(this, btnNext);
			MultiClickBlocker.Block(this, btnPrevious);
		}
		
		void btnPrevious_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(wipper.Wip.Prev(), false);
		}		
		void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (!(ValidateSalesId()))
					return;
				
			if (!ValidateInput())
				return;

			if (txtNpa.Text.Length > 0) // have phone #
			{
				txtAccNumber.Text = ""; // clear acct # if any
				if (!GetAccountByPhone())
					return;
			}
			
			if	(txtAccNumber.Text.Length > 0) // have acct number
				if (!GetAccountById())
					return;

			MakeDemand();
			Response.Redirect(wipper.Wip.Next(), false);			
		}
				
	#endregion

	#region Implementations
		void CustomInit()
		{
			btnNext.Visible = wipper.Wip.HasNext;
			btnPrevious.Visible = wipper.Wip.HasPrev;
			imgWorkflow.ImageUrl = wipper.Wip.Workflow.ImageTag.ToString();
		}
		void MakeDemand()
		{
			if (wipper.Wip["Demand"] != null)
				return;

			IDemand dmd = DmdFactory.GetDemand(DemandType.Monthly.ToString());
			
			IUser user = (IUser)Session["User"];
			dmd.StoreCode = user.LoginStoreCode;
			dmd.ConsumerAgent = user.ClerkId;

			wipper.IMap.add((IMapObj)dmd);
			wipper.Wip["Demand"] = dmd;
		}
		bool ValidateSalesId()
		{
			this.lblErrMsg.Visible = false;
			this.lblErrMsg.Text = "";

			if(!(bool)wipper.Wip["SalesIdRequired"])
				return true;

			if(!(LoginSvc.GetIfClerkIDRequested((IUser)Session["User"])))
				return true;
			
			if (txtSalesId.Text.Trim().Length == 0)
			{
				lblErrMsg.Text = "Please enter Co-Worker Id";
				lblErrMsg.Visible = true;
				return false;
			}

			wipper.Wip.ClerkId = txtSalesId.Text.Trim();
			return true;
		}
			
		bool GetAccountById()
		{
			try
			{
				wipper.Wip["AcctInfo"] = CustSvc.GetAcctInfo(wipper.IMap, int.Parse(txtAccNumber.Text.Trim()));
				return ValidateAcct();
			}
			catch (FormatException fe)
			{
				lblErrMsg.Visible = true;
				lblErrMsg.Text = "Account number must be numeric. Please correct";
			}
			catch (Exception ex)
			{
				lblErrMsg.Text = "Customer account is not found.";
				lblErrMsg.Visible = true;
			}
			return false;
		}
		bool GetAccountByPhone()
		{	
			try
			{
				wipper.Wip["AcctInfo"] 
					= CustSvc.GetAcctInfo(wipper.IMap, txtNpa.Text + txtNxx.Text + txtNumber.Text);
				return ValidateAcct();
			}
			catch (Exception e)
			{
				lblErrMsg.Text = "Customer account is not found.";
				lblErrMsg.Visible = true;
			}
			return false;
		}

		bool ValidateInput()
		{
			if (txtAccNumber.Text.Length > 0)
				return true;

			if	((txtNpa.Text.Length > 0)
				&& (txtNxx.Text.Length > 0) 
				&& (txtNumber.Text.Length > 0))
				return true;
		
			lblErrMsg.Text = "Please enter Phone number or Account Number";
			lblErrMsg.Visible = true;
			return false;
		}

		bool ValidateAcct()
		{
			if (wipper.Wip["AcctInfo"] == null)
			{
				lblErrMsg.Text = "Customer account is not found.";
				lblErrMsg.Visible = true;
				return false;
			}
			if (((IAcctInfo)wipper.Wip["AcctInfo"]).IsActive)
				return true;
		
			lblErrMsg.Text = "Monthly payment can be only posted to active accounts. This account is inactive";
			lblErrMsg.Visible = true;
			return false;
		}

	#endregion

	}
}