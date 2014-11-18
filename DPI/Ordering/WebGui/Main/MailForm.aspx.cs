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
using System.Web.Mail;

namespace DPI.Ordering.Main
{
	/// <summary>
	/// Summary description for MailForm.
	/// </summary>
	public class MailForm : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected System.Web.UI.WebControls.Label lblResponse;
		protected System.Web.UI.WebControls.ImageButton btnNext;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Page.IsPostBack)
				lblResponse.Text = "Your email has been sent.";
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
			if(Request.Form["Email"] != "")
			{
				MailMessage objMail = new MailMessage();
				objMail.From = Request.Form["Email"]; 
				objMail.To = "rbobadilla@dpiteleconnect.com";
				objMail.Subject = Request.Form["Subject"];
				objMail.Body = Request.Form["Message"]; 
				objMail.BodyFormat = MailFormat.Text;
				SmtpMail.SmtpServer = "smtp.onebox.com";
				SmtpMail.Send(objMail);
			}else{
				lblResponse.Text = "Please enter an email address.";
			}
		}
	}
}
