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
	public class ReprintReceiptPrint : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.ImageButton btnGotoMain;
		protected System.Web.UI.WebControls.Image Image2;

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			CustomInit();
		}

		private void InitializeComponent()
		{    
			this.btnGotoMain.Click += new System.Web.UI.ImageClickEventHandler(this.btnGotoMain_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion	
	
		void CustomInit()
		{
			//imgWorkflow.ImageUrl = wipper.Wip.Workflow.ImageTag.ToString();

//			System.Collections.IEnumerator ie = Controls.GetEnumerator();
//			object o;
//			while(ie.MoveNext())
//				o = ie.Current;
		}		
		private void Page_Load(object sender, System.EventArgs e)
		{
		}

		private void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect(, false);
		}

		private void btnGotoMain_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("MenuScreen.aspx", false);
		}
	}
}
