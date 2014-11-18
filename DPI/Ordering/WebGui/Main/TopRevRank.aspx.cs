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

namespace DPI.Ordering.Main
{

	public class TopRevRank : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblRank;
		protected System.Web.UI.WebControls.Label lblVerbs;
		protected System.Web.UI.WebControls.ImageButton btnPrint;
		protected System.Web.UI.WebControls.PlaceHolder tblTopRevRank;
		protected System.Web.UI.WebControls.ImageButton btnDone;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnPrint.Attributes.Add("onClick", "window.print()");
			btnDone.Attributes.Add("onClick", "window.close()");
			DateTime rightNow = DateTime.Now;
			lblVerbs.Text = String.Format("{0:d}", rightNow);

			IStoreStats stat = StoreSvc.GetStoreStats(DPI.ClientComp.User.GetUser(this).LoginStoreCode);
			ICorp corp = StoreSvc.GetCorp(stat.CorpId);
	
			IStoreStats[] topNewSales = corp.Top10Revenue;

			Table TT = new Table();
			TT.CellPadding = 0;
			TT.CellSpacing = 0;
			TT.BorderWidth = Unit.Point(1);
			TT.Width=Unit.Pixel(400);
			TT.GridLines = GridLines.Horizontal;
			bool alt = false;
	
			TableRow rHead = new TableRow();
			rHead.HorizontalAlign = HorizontalAlign.Center;

			rHead.Cells.Add(AddCell("&nbsp;&nbsp;Rank", Color.Chocolate, Color.White));
			rHead.Cells.Add(AddCell("Store Number", Color.Chocolate, Color.White));
			rHead.Cells.Add(AddCell("Revenue", Color.Chocolate, Color.White));
			TT.Rows.Add(rHead);

			for (int i = 0 ; i < corp.Top10Revenue.Length ; i ++)
			{
				TableRow r = new TableRow();
				r.HorizontalAlign = HorizontalAlign.Center;
				if( corp.Top10Revenue[i].StoreCode == DPI.ClientComp.User.GetUser(this).LoginStoreCode)
				{
					r.Cells.Add(AddCell(corp.Top10Revenue[i].RevenueRank.ToString(), Color.Yellow, Color.Black));
					r.Cells.Add(AddCell(corp.Top10Revenue[i].StoreNumber , Color.Yellow, Color.Black));
					r.Cells.Add(AddCell(corp.Top10Revenue[i].Revenue.ToString(), Color.Yellow, Color.Black));
				}
				else
				{
					r.Cells.Add(AddCell(corp.Top10Revenue[i].RevenueRank.ToString(), alt ? Color.WhiteSmoke : Color.White, Color.Black));
					r.Cells.Add(AddCell(corp.Top10Revenue[i].StoreNumber , alt ? Color.WhiteSmoke : Color.White, Color.Black));
					r.Cells.Add(AddCell(corp.Top10Revenue[i].Revenue.ToString(), alt ? Color.WhiteSmoke : Color.White, Color.Black));
				}

				TT.ForeColor = Color.Black;
				TT.HorizontalAlign = HorizontalAlign.Center;

				TT.Rows.Add(r);
				alt = ! alt;
			}

			tblTopRevRank.Controls.Add(TT);
		}

		TableCell AddCell(string text, Color back, Color fontColor)
		{	
			TableCell cell = new TableCell();
			cell.BackColor = back;
			cell.ForeColor = fontColor;
			cell.Text = text;
			return cell;
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
