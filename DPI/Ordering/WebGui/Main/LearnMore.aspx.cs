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
	public class LearnMore : System.Web.UI.Page
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
			this.btnLogout.Click += new System.Web.UI.ImageClickEventHandler(this.btnLogout_Click);
			this.ImageButton1.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label lblLocation;
		protected System.Web.UI.WebControls.Label lblFeature;
		protected System.Web.UI.WebControls.Image imgDivide;
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected System.Web.UI.WebControls.ImageButton btnLogout;
		#endregion
	
		void CustomInit()
		{
//			btnNext.Visible = wipper.Wip.HasNext;
//			btnPrevious.Visible = wipper.Wip.HasPrev;
			
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnLogout.Attributes.Add("onClick", "clickedButton=true; ");			
			lblLocation.Text = "<b><font color='darkgray'>Home > Portal ></font> <font color='chocolate'>Learn More</font></b>";			
			ImageButton1.Attributes.Add("onClick","clickedButton=true; ");
		}
		private void btnLogout_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("SignOut.aspx", false);
		}

		private void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("MenuScreen.aspx", false);
		}
	}
}