using System;

using DPI.Interfaces;
using DPI.Services;
using DPI.ClientComp;

namespace DPI.Ordering
{
	public class WirelessActivation : BasePage
	{

	#region Web Form Designer generated code

		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.TextBox txtESN;
		protected System.Web.UI.WebControls.TextBox txtZip;
		protected System.Web.UI.WebControls.LinkButton lbESN;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator1;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		

		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			CustomInit();
		}
		void InitializeComponent()
		{    
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
	#endregion		

	#region Event Handlers
		void Page_Load(object sender, System.EventArgs e) {}
		void CustomInit()
		{
			try
			{
				lbESN.Attributes.Add("onclick", "window.open('ESNInfo.htm', '_blank' ,'height= 200, width=320 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
				btnPrevious.Visible = wipper.Wip.HasPrev;
				btnNext.Visible = wipper.Wip.HasNext;
				MultiBlock();
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
				if (!ValidateIt())
					return;
			
				MakeCellInfo();

				ICellPhoneReceipt rct = PinSvc.ActivatePhone(wipper.IMap, (IUser)Session["User"], 
												(IPayInfo)wipper.Wip["PayInfo"],
												(ICellPhoneInfo)wipper.Wip["PhoneInfo"],
												(IPinProduct)wipper.Wip["SelectedPinProduct"]);

				if (rct.Pass)
				{
					CustSvc.PreSave(wipper.IMap);  // saves payinfo & demand
					wipper.Wip["ConfNum"] = rct.ConfNum;
					Response.Redirect(wipper.Wip.Next(), false);
					return;
				}

				lblErrMsg.Text = rct.ErrMsg;
				lblErrMsg.Visible = true;	
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}		
		}

	#endregion
	
	#region Implementation
		void MakeCellInfo()
		{
			ICellPhoneInfo pi =	PinSvc.GetCellInfo();
			
			pi.NewESN	= txtESN.Text.Trim();
			pi.Zip		= txtZip.Text.Trim();
			pi.WireleesProduct 
				= ((IPinProduct)wipper.Wip["SelectedPinProduct"]).Product_Id;
			
			wipper.Wip["PhoneInfo"] = pi;
		}
		bool ValidateIt()
		{
			lblErrMsg.Visible = true;
			lblErrMsg.Text = "";

			if (!IsValid)
				return false;
			
			if (!CheckZip())
			{
				lblErrMsg.Text = "Service is not available in this zip: " + txtZip.Text;
				return false;
			}

			lblErrMsg.Visible = false;
			return true;
		}

		void MultiBlock()
		{
			MultiClickBlocker.Block(this, btnNext);
			MultiClickBlocker.Block(this, btnPrevious);						
		}
		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr( wipper.IMap, ex, Session["User"], this.ToString());				
			lblErrMsg.Text = Const.GENERAL_ERROR;
			lblErrMsg.Visible = true;
		}
		bool CheckZip()
		{
			return ProdSvc.ValidZip(wipper.IMap, txtZip.Text);
		}
	#endregion

	}
}