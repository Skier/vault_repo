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
	public class SalesId : BasePage
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
			this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrevious_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator1;
		protected System.Web.UI.WebControls.TextBox txtSalesId;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.Label Label1;
		#endregion
	
		void CustomInit()
		{
			btnNext.Visible = wipper.Wip.HasNext;
			btnPrevious.Visible = wipper.Wip.HasPrev;
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			MultiClickBlocker.Block(this, btnNext);
			MultiClickBlocker.Block(this, btnPrevious);

			if(!LoginSvc.GetIfClerkIDRequested((IUser)Session["User"]))
				Response.Redirect(wipper.Wip.Next());
		}
		private void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs ea)
		{
				Label1.Visible = false;
				if(txtSalesId.Text.Trim().Length == 0)
				{
					Label1.Visible = true;
					return;
				}
				wipper.Wip.ClerkId = txtSalesId.Text;				 
				Response.Redirect(wipper.Wip.Next());
		}
		private void btnPrevious_Click(object sender, System.Web.UI.ImageClickEventArgs ea)
		{
			try
			{
				wipper.Wip["Zip"] = null;
				Response.Redirect(wipper.Wip.Prev(), false);
			}
			catch (Exception ex)
			{
				ErrLogSvc.LogError(
					wipper.IMap, this.ToString(), HttpContext.Current.User.Identity.Name,
					ex.Message + ", " + ex.StackTrace);
			}
		}
	}
}