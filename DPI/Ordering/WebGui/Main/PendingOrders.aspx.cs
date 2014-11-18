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

namespace DPI.Ordering.Main
{
	public class PendingOrders : BasePage
	{
	#region Web Form Designer generated code
		protected System.Web.UI.WebControls.Image imgWorkflow;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.Label lblNoVoids;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtSalesId;
		protected System.Web.UI.WebControls.RadioButtonList rListTrans;
		protected System.Web.UI.WebControls.PlaceHolder PlaceHolder1;

		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			CustomInit();
		}

		private void InitializeComponent()
		{    
			this.ImageButton1.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton1_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
	#endregion	

	#region Event Handlers	
		void CustomInit()
		{
			try
			{
				imgWorkflow.ImageUrl = wipper.Wip.Workflow.ImageTag.ToString();
				SetBtnNext();
				lblNoVoids.Visible = false;	
				
				if (btnNext.Visible)
					btnNext.Attributes.Add("onclick","return confirmButton();");

				MultiClickBlocker.Block(this, btnNext);
				MultiClickBlocker.Block(this, ImageButton1);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}		
		void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				PlaceHolder1.Controls.Add(VoidManager.GetVoidableTrans
					(wipper.IMap, ((IUser)Session["User"]).LoginStoreCode, 
					rListTrans.SelectedValue, new EventHandler(CbCheckChanged)));
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void CbCheckChanged(object sender, EventArgs ea)
		{
			try
			{

				wipper.Wip["TranType"] =  VoidManager.GetVoidType(((WebControl)sender).Attributes["TranType"]);
				wipper.Wip["Tran"]     = int.Parse(((WebControl)sender).ID);
					
				SetBtnNext();
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{		
			try
			{
				if (!(ValidateSalesId()))
					return;

				VoidManager.VoidTransaction
					(wipper.IMap, (IUser)Session["User"], (VoidTranType)wipper.Wip["TranType"], (int)wipper.Wip["Tran"]);

				Response.Redirect(wipper.Wip.Next(), false);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				Session["IMap"] = null;
				Response.Redirect("MenuScreen.aspx", false);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		
	#endregion	

	#region Implementation	
		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr( wipper.IMap, ex, Session["User"], this.ToString());
			lblErrMsg.Text = Const.GENERAL_ERROR;
			lblErrMsg.Visible = true;
		}
		void SetBtnNext()
		{
			btnNext.Visible = (int)(wipper.Wip["Tran"]) > 0; 
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

			wipper.Wip.ClerkId = txtSalesId.Text.Trim();
			return true;
		}	
		
	#endregion	
		
	}
}