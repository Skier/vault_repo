namespace DPI.Ordering.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using DPI.Interfaces;
	using DPI.ClientComp;
	using DPI.Services;

	public abstract class PrintHeader : System.Web.UI.UserControl
	{
	#region Data
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.Image Image2;
		protected System.Web.UI.WebControls.Image Image3;
		protected System.Web.UI.WebControls.Image Image4;
		protected System.Web.UI.WebControls.ImageButton btnHome;
		protected System.Web.UI.WebControls.ImageButton btnQandA;
		protected System.Web.UI.WebControls.ImageButton btnAgentHotline;
		protected System.Web.UI.WebControls.Label Greeting;
		protected System.Web.UI.WebControls.Label lblVersion;
		protected System.Web.UI.WebControls.ImageButton btnForms;
		protected System.Web.UI.WebControls.Image Image5;
		protected System.Web.UI.WebControls.ImageButton btnImgLogout;
		protected System.Web.UI.WebControls.Button btnLogoutKiller;
		protected System.Web.UI.WebControls.ImageButton btnReporting;
	#endregion

	#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			CustomInit();
		}

		private void InitializeComponent()
		{
			this.btnImgLogout.Click += new System.Web.UI.ImageClickEventHandler(this.btnImgLogout_Click);
			this.btnHome.Click += new System.Web.UI.ImageClickEventHandler(this.btnHome_Click);
			this.btnReporting.Click += new System.Web.UI.ImageClickEventHandler(this.btnReports_Click);
			this.btnQandA.Click += new System.Web.UI.ImageClickEventHandler(this.btnQandA_Click);
			this.btnAgentHotline.Click += new System.Web.UI.ImageClickEventHandler(this.btnAgentHotline_Click);
			this.btnForms.Click += new System.Web.UI.ImageClickEventHandler(this.btnForms_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	#region Event Handlers
		private void CustomInit()
		{
			foreach(object ctrl in Controls)
				if (ctrl is System.Web.UI.WebControls.ImageButton)
				{
					((System.Web.UI.WebControls.ImageButton)ctrl).Attributes.Add("onClick", "clickedButton=true; ");
				}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			lblVersion.Text = "Version: 4.2.0.0";
			if(Context.User.Identity.IsAuthenticated)
				Greeting.Text = "&nbsp;&nbsp;&nbsp;<font color='#d2691e'>Welcome:</font> "
					          + DPI.ClientComp.User.GetUser(this).DisplayName;
		}

		private void NewOrder_Click(object sender, System.EventArgs e)
		{

			IMap imap = IMapFactory.getIMap();
			IUser user = (IUser)Session["User"];
			WIP wip = new NewOrderWip(user);
			try
			{
				imap.add(wip);
				Session["IMap"] = imap;
				Response.Redirect(((WIP)imap.find(WIP.IKeyS)).Current(), false);
			}
			catch (Exception ex)
			{
				ErrLogSvc.LogError(
					imap, this.ToString(), HttpContext.Current.User.Identity.Name, 
					ex.Message + ", " + ex.StackTrace);
			}
		}

		private void btnHome_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Session["IMap"] = null;
			Response.Redirect("MenuScreen.aspx", false);		
		}

		private void btnReports_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Session["IMap"] = null;
			Response.Redirect("ReportMain.aspx", false);
		}

		private void btnQandA_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Session["IMap"] = null;
			Response.Redirect("FAQs.aspx", false);
		}

		private void btnAgentHotline_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Session["IMap"] = null;
			Response.Redirect("AgentHotline.aspx", false);
		}
		private void btnForms_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Session["IMap"] = null;		
			Response.Redirect("Forms.aspx", false);
		}

		private void btnImgLogout_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("SignOut.aspx", false);		
		}
	#endregion
	}
}
