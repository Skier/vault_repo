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

namespace DPI.Ordering.Training
{
	/// <summary>
	/// Summary description for QuizSummary.
	/// </summary>
	public class QuizSummary : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.ImageButton btnGetCert;
		protected System.Web.UI.WebControls.ImageButton btnMenu;
		protected System.Web.UI.WebControls.Label Label5;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			Label1.Text = Request.QueryString["result"];
			Label5.Text = "<font color='blue' size='2'>" +
						  Request.QueryString["name"] + 
						  "</font>, you are now certified.";

			if(Request.QueryString["wrong"].Length > 0)
				Label5.Text = "<font color='blue'>" +
							  Request.QueryString["name"] + 
							  "</font>, " +
							  "The Following questions were answered incorrectly: <font color=red>" +
							  Request.QueryString["wrong"] +
							"</font>";
			
			Label2.Text = "To review the tutorial again, click below.";

			if(Request.QueryString["result"].Trim() == "We're Sorry")
				Label2.Text = "Please review the training material so that you may try again to get certified." + 
							"You are allowed to try the certification as many times as necessary.<br><br>" +
							"To do so, simply click the button below.";

			btnMenu.Attributes.Add("onclick", "window.self.close();");
			btnGetCert.Visible = false;
			btnMenu.Visible = true;
			if(Request.QueryString["result"].Trim() == "We're Sorry")
			{
				btnGetCert.Visible = true;
				btnMenu.Visible = false;
			}
				
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{

			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent()
		{    
			this.btnGetCert.Click += new System.Web.UI.ImageClickEventHandler(this.btnGetCert_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnMainMenu_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			
		}

		private void btnTutorialMenu_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
		
		}

		private void btnGetCert_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("Certification_quiz.aspx", false);
		}
	}
}
