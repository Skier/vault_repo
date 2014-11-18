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

using DPI.ClientComp;
using DPI.Services;
using DPI.Ordering;
using DPI.Interfaces;

namespace DPI.Ordering
{
	public class LDCard : BasePage
	{
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.CheckBoxList CheckBoxList1;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.Label Label1;
	
		double amountTotal = 14.99;

		private void Page_Load(object sender, System.EventArgs e)
		{
			MultiClickBlocker.Block(this, btnNext);
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent()
		{    
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void btnPrevious_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
//			try
//			{
//				wipper.Wip["PPLongDistance"] = null;
//				Response.Redirect(wipper.Wip.Prev());
//			}
//			catch (Exception ex)
//			{				
//				lblErrMsg.Text = Const.GENERAL_ERROR;
//				wipper.Wip.LogError(ex.Message);
//			}
		}

		private void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
//				wipper.Wip["LongDistanceWIP"] 
//					= CustSvc.GetAcctInfo(wipper.IMap, txtNpa.Text + txtNxx.Text + txtNumber.Text);
				Response.Redirect(wipper.Wip.Next(), false);		
			}
			catch (Exception ex)
			{
				ErrLogSvc.LogError( 
					wipper.IMap, this.ToString(), HttpContext.Current.User.Identity.Name,
					ex.Message + ", " + ex.StackTrace);

				btnNext.Visible = false;
				lblErrMsg.Visible = true;
				lblErrMsg.Text = Const.GENERAL_ERROR;
			}		
		}
	}
}
