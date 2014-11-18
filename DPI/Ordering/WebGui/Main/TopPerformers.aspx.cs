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
using DPI.ClientComp;
using DPI.Interfaces;

namespace DPI.Ordering.Main
{
	public class TopPerformers : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblVerbs;
		protected System.Web.UI.WebControls.ImageButton btnDone;
		protected System.Web.UI.WebControls.ImageButton btnPrint;
		protected System.Web.UI.WebControls.Label lblRank;
		protected System.Web.UI.WebControls.PlaceHolder tblTopPerformers;
		
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
		

		#region Event Handlers
		void Page_Load(object sender, System.EventArgs e)
		{
			SetAttributes();
			StatType stype =(StatType)Enum.Parse(typeof(StatType), Request.QueryString["statType"]);
			Ranking ranking = Ranking.GetRanking(stype, DPI.ClientComp.User.GetUser(this).LoginStoreCode);

			lblRank.Text = ranking.RankText;
			tblTopPerformers.Controls.Add(MakeTable(ranking));
		}
		#endregion

		#region Implementation


		Table MakeTable(Ranking ranking)
		{
			IStoreStats stat = StoreSvc.GetStoreStats(DPI.ClientComp.User.GetUser(this).LoginStoreCode);
			return new TableStats(ranking.Attrs, ranking.Ranks, ranking.Stores, stat.StoreNumber, ranking.RankTitle);
		}
		void SetAttributes()
		{
			btnPrint.Attributes.Add("onClick", "window.print()");
			btnDone.Attributes.Add("onClick", "window.close()");		
			
			DateTime rightNow = DateTime.Now;
			lblVerbs.Text = String.Format("{0:d}", rightNow);

		}
		#endregion
	}
}