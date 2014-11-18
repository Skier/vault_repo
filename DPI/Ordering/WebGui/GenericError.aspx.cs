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
	/// <summary>
	/// Summary description for GenericError.
	/// </summary>
	public class GenericError : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected System.Web.UI.WebControls.Image Image1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
				Label2.Text = "We're sorry. There was a problem processing your page. <br>Please press the button below to return to the main menu screen.";
				ImageButton1.Visible = true;
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.ImageButton1.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
//			ErrLogSvc.LogError(
//				wipper.IMap, Const.SUBSYSTEM, HttpContext.Current.User.Identity.Name, "General Error:"
//				+ Server.GetLastError().Message
//				+ "Stack Trace:"
//				+ Server.GetLastError().StackTrace  );

			Response.Redirect("Main/MenuScreen.aspx", false);

		}

		private void ImageButton2_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("Main/TimeOut.aspx", false);
		}
	}
}
