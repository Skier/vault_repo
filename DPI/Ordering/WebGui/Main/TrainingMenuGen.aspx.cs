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
	public class TrainingMenuGen : System.Web.UI.Page
	{
		#region Web Form Designer generated code
		
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Image Image3;
		protected System.Web.UI.WebControls.Image Image4;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnDocPath;
		protected System.Web.UI.WebControls.Image Image2;
		override protected void OnInit(EventArgs e)
		{			
			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion		
	
		void Page_Load(object sender, System.EventArgs e)
		{
			hdnDocPath.Value = ConfigurationSettings.AppSettings["DocPath"];
		}		
	}
}
