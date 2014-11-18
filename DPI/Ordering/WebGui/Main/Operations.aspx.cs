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
	public class Operations :  System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnRefreshData;
		protected System.Web.UI.WebControls.Label lblMsg;
		protected System.Web.UI.WebControls.Button btnMenu;
		protected System.Web.UI.WebControls.Button btnRestart;
		protected System.Web.UI.WebControls.Button btnStop;
		protected System.Web.UI.WebControls.Label Label1;
	
		void Page_Load(object sender, System.EventArgs e)
		{
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			if(!HttpContext.Current.User.IsInRole(Const.PERMISSION_OPERATIONS))
				Response.Redirect("MenuScreen.aspx", false);

			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent()
		{    
			this.btnRefreshData.Click += new System.EventHandler(this.btnReset_Click);
			this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
			this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		void btnReset_Click(object sender, System.EventArgs e)
		{
			OperSvc.RefreshData(HttpContext.Current.User.Identity.Name);
			lblMsg.Text = "Data has been refreshed @" + DateTime.Now.ToShortTimeString(); 
		}

		void btnMenu_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("MenuScreen.aspx", false);
		}

		void btnRestart_Click(object sender, System.EventArgs e)
		{
			OperSvc.StartThreads(this.ToString());
			lblMsg.Text = "WebSvcQueue spinner has been restarted @" + DateTime.Now.ToShortTimeString(); 
		}
		void btnStop_Click(object sender, System.EventArgs e)
		{
			OperSvc.StopThreads(this.ToString());
			lblMsg.Text = "WebSvcQueue spinner has been stopped @" + DateTime.Now.ToShortTimeString(); 
		}
	}
}
