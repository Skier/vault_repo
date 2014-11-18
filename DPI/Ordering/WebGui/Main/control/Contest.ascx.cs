namespace DPI.Ordering.Main.control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.IO;

	using DPI.ClientComp;
	using DPI.Interfaces;
	using DPI.Services;

	public class Contest : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.Image Image3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Image Image7;
		protected System.Web.UI.WebControls.Image Image4;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.LinkButton Linkbutton2;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Image Image5;

		private void Page_Load(object sender, System.EventArgs e)
		{
			string url = null;
			IUser userCred = (IUser)Session["User"];
			try
			{
				url = ContestResults.GetContestResultsUrl( userCred ); 

			}
			catch {}
			
			if (url == null)
				return;
			
			Linkbutton2.Visible = false;
		
			if(findFile(url))
			{
				Linkbutton2.Attributes.Add("onclick", "window.open('../main/Docs/" +
					url  +
					"', '_blank' ,'height= 600, width=500 ,toolbar=no, location=no," +
					"directories=no,status=no,menubar=no, scrollbars=yes,resizable=yes')");
				
				Linkbutton2.Visible = true;	
			}
		}

		bool findFile(string url)
		{
			string path = @Server.MapPath("docs");
			DirectoryInfo dir = new DirectoryInfo(@Server.MapPath("docs"));
			FileInfo[] pdfFiles = dir.GetFiles("*.pdf");
			for (int i = 0; i < pdfFiles.Length; i++)
				if (pdfFiles[i].Name.ToLower() == url.ToLower())
					return true;

			return false;
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
