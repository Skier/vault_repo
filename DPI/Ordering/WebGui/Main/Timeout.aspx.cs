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

namespace DPI.Ordering.Main
{
	public class Timeout : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			Page.RegisterStartupScript("jScript", GetScript());
		}
		string GetScript()
		{	
			if ( Cache["LoginType"] == null )
				return   "<script>	window.alert(\"Your session has expired. \\n \\n You will be redirected to "
					+ "the logon screen to enter your credentials. \");	window.open(\"../Logon.aspx\",'_blank','"
					+ "toolbar=yes,location=1,directories=1,status=1, menubar=1, scrollbars=1,resizable=1');" 
					+ "	self.close();</script>";
			
			return "<script>	window.alert('Your session has expired. \\n \\n For security purposes, "
				+ "please use the autologin link to securely access dPi WebCentral.');" 
				+ "	self.close();</script>";
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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}