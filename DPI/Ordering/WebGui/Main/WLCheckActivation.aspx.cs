using System;

using DPI.Services;
using DPI.Interfaces;
using DPI.ClientComp;

namespace DPI.Ordering
{
	public class WLCheckActivation : BasePage
	{

	#region Web Form Designer generated code

		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.Button btnManActivation;
		protected System.Web.UI.WebControls.Button btnCheckAct;
		protected System.Web.UI.WebControls.Label lblSearch;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			CustomInit();
		}
		void InitializeComponent()
		{    
			this.btnCheckAct.Click += new System.EventHandler(this.btnCheckAct_Click);
			this.btnManActivation.Click += new System.EventHandler(this.btnManActivation_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
	#endregion		

	#region Event Handlers
		void Page_Load(object sender, System.EventArgs e)
		{
			try
			{					
				btnManActivation.Visible
					= btnManActivation.Enabled
					= (int)wipper.Wip["CheckActAttempts"] > 2;
				
				if(IsPostBack)
					return;

				wipper.Wip["CheckActAttempts"] = 0;
				btnCheckAct_Click(this, EventArgs.Empty);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}		
		void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try{} catch (Exception ex) {ErrorHandler(ex); }		
		}
		void btnCheckAct_Click(object sender, System.EventArgs e)
		{
			try
			{
				wipper.Wip["CheckActAttempts"] = (int)wipper.Wip["CheckActAttempts"] + 1;

				lblSearch.Visible = (int)wipper.Wip["CheckActAttempts"] > 1;
				
				if (CheckActivation())
				{
					Response.Redirect(wipper.Wip.Next(), false);
					return;
				}
			
				lblErrMsg .Text = ((ICellPhoneReceipt)wipper.Wip["Receipt"]).ErrMsg;
				lblErrMsg.Visible = true;
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}		
		}
		void btnManActivation_Click(object sender, System.EventArgs e)
		{
			try
			{
				GetReceiptText(false);
				Response.Redirect(wipper.Wip.Next(), false);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void CustomInit()
		{
			try
			{
				btnPrevious.Visible = btnNext.Visible = false;
				MultiBlock();		
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
	#endregion
	
	#region Implementation

		void MultiBlock()
		{
			MultiClickBlocker.Block(this, btnNext);
			MultiClickBlocker.Block(this, btnPrevious);						
		}
		void GetReceiptText(bool activated)
		{
			((ICellPhoneReceipt)wipper.Wip["Receipt"]).Receipt_Text 
					= PinSvc.GetReceipt(wipper.IMap, ((IPinProduct)wipper.Wip["SelectedPinProduct"]).Product_Id, activated);
			
			((ICellPhoneReceipt)wipper.Wip["Receipt"]).IsActivated = activated;
		}
		bool CheckActivation()
		{
			wipper.Wip["Receipt"] = PinSvc.Check_Activation(wipper.IMap, (IUser)Session["User"],
											(IPayInfo)wipper.Wip["PayInfo"],
											(ICellPhoneInfo)wipper.Wip["PhoneInfo"]);
			
			((ICellPhoneReceipt)wipper.Wip["Receipt"]).ConfNum = (string)wipper.Wip["ConfNum"];

			if (((ICellPhoneReceipt)wipper.Wip["Receipt"]).PhoneNumber == null)
				return false;

			if (((ICellPhoneReceipt)wipper.Wip["Receipt"]).PhoneNumber.Trim().Length == 0)
				return false;

			if (((ICellPhoneReceipt)wipper.Wip["Receipt"]).Status.Trim().ToLower() == "failed")
				return false;

			wipper.Wip["IsCompleted"] = true; 
			
			return true;
		}
		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr( wipper.IMap, ex, Session["User"], this.ToString());				
			lblErrMsg.Text = Const.GENERAL_ERROR;
			lblErrMsg.Visible = true;
		}
	#endregion
	}
}