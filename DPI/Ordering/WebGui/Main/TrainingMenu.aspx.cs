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
using System.Configuration;

namespace DPI.Ordering.Training
{
	public class TrainingMenu : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton btnManual;
		protected System.Web.UI.WebControls.ImageButton btnTest;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.LinkButton lnkManual;
		protected System.Web.UI.WebControls.LinkButton lnkTest;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.Image Image2;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			LinkButton1.Attributes.Add("onclick", "window.open('" + GetDocPath() + "/HighTouch.pdf', '_blank' ,'height= 690, width=682 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=yes,resizable=yes')");
			lnkManual.Attributes.Add("onclick", "window.open('" + GetDocPath() + "/OnlineTraining_rac.pdf', '_blank' ,'height= 690, width=682 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=yes,resizable=yes')");
			btnManual.Attributes.Add("onclick", "window.open('" + GetDocPath() + "/OnlineTraining_rac.pdf', '_blank' ,'height= 690, width=682 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=yes,resizable=yes')");
			btnTest.Attributes.Add("onclick", "window.open('../training/certification_quiz.aspx', '_blank' ,'height= 750, width=682 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=yes,resizable=yes')");
			lnkTest.Attributes.Add("onclick", "window.open('../training/certification_quiz.aspx', '_blank' ,'height= 750, width=682 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=yes,resizable=yes')");
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
		string GetDocPath()
		{
			return ConfigurationSettings.AppSettings["DocPath"];			
		}
	}
}
