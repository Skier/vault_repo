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
	public class DCRentway : BasePage
	{

	#region Data
		// labels
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label2;
		
		protected System.Web.UI.WebControls.Label lblEnrollFee;		
		protected System.Web.UI.WebControls.Label lblLoadAmt;
		protected System.Web.UI.WebControls.Label txtOrderTotal;

		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;

		protected System.Web.UI.WebControls.Image imgWorkflow;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary2;
		protected System.Web.UI.WebControls.PlaceHolder phError;

	#endregion

	#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		void InitializeComponent()
		{    
			this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrevious_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
	#endregion
	
	#region Event Handlers
		void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				Pre_Load();

				if(IsPostBack)
					return;

				CustomInit();
				ToPage();
			}
			catch (Exception ex)
			{
				FatalError.ShowErr(phError, ex.Message);
				FatalError.SaveErr(ex, Session["User"], this.ToString());
			}
		}
		void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{	
				ToPage();
				Response.Redirect(wipper.Wip.Next(), false);
			}
			catch (Exception ex)
			{				
				FatalError.ShowErr(phError, ex.Message);
				FatalError.SaveErr(ex, Session["User"], this.ToString());
			}
		}
		void btnPrevious_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				Response.Redirect(wipper.Wip.Prev(), false);
			}
			catch {}
		}

	#endregion

	#region Implementation	
		void CustomInit()
		{
	//		imgWorkflow.ImageUrl = wipper.Wip.Workflow.ImageTag.ToString();	
		}
		void Pre_Load()
		{
			btnPrevious.Visible = false;
			btnNext.Visible = wipper.Wip.HasNext;

			MultiClickBlocker.Block(this, btnNext);		
			MultiClickBlocker.Block(this, btnPrevious);	
		}
		void RefreshData()
		{
			ToPage();
		}
		void ToPage()
		{
			IOrderSum sumry = CustSvc.GetOrderSummary(wipper.IMap, (IDemand)wipper.Wip["Demand"]);
			lblEnrollFee.Text    = sumry.GetFeeAmt().ToString("C");

			if (sumry.GetProdAmt() > 0)
			{
				this.lblLoadAmt.Text = sumry.GetProdAmt().ToString("C");
				this.txtOrderTotal.Text =  (sumry.GetProdAmt() + sumry.GetFeeAmt()).ToString("C");
				return;
			}
			throw new ArgumentException("Product amount must be > 0, found: " + sumry.GetProdAmt().ToString("C"));
		}
	#endregion
	}
}