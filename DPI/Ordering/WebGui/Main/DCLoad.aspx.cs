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
	public class DCLoad : BasePage
	{
		protected System.Web.UI.WebControls.Image imgWorkflow;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.ImageButton btnMain;
		protected System.Web.UI.WebControls.ImageButton btnNext;

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			CustomInit();
		}

		private void InitializeComponent()
		{    
			this.btnMain.Click += new System.Web.UI.ImageClickEventHandler(this.btnMain_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion	
	
		void CustomInit()
		{
			imgWorkflow.ImageUrl = wipper.Wip.Workflow.ImageTag.ToString();
		}		
		private void Page_Load(object sender, System.EventArgs e)
		{
		}

		private void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(wipper.Wip.Next(), false);
		}

		private void btnMain_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
		}
	}
}
