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
using System.Text;

using DPI.ClientComp;
using DPI.Services;
using DPI.Ordering;
using DPI.Interfaces;

namespace DPI.Ordering
{
	public class PL_OrdSummary : BasePage
	{
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
			CustomInit();
		}
		
		private void InitializeComponent()
		{    
			this.btnPrint.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton1_Click);
			this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrevious_Click);
			this.btnGotoMain.Click += new System.Web.UI.ImageClickEventHandler(this.btnGotoMain_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.PlaceHolder phldrOrdrDetails;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.ValidationSummary validSummary;
		protected System.Web.UI.WebControls.ImageButton btn_change;
		protected System.Web.UI.WebControls.Label lblZipCode;
		protected System.Web.UI.WebControls.Label lblOrderTotal;
		protected System.Web.UI.WebControls.Label lblFees;
		protected System.Web.UI.WebControls.Label lblAmountDue;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.ImageButton btnGotoMain;
		protected System.Web.UI.WebControls.ImageButton btnPrint;
		protected System.Web.UI.WebControls.Label lbl2monthtotal;
		protected System.Web.UI.WebControls.Label lblAmountDue2;
		protected System.Web.UI.WebControls.ImageButton btnMonthChart;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label lbl2month;
		protected System.Web.UI.WebControls.Label lblIlec;
		#endregion

		void CustomInit()
		{						
			btnPrevious.Visible = wipper.Wip.HasPrev;
			
			WipOrderSummary.CheckOrderSummary(wipper.IMap);
			IProdPrice[] prods = ((IOrderSum)wipper.Wip["ordersummary"]).Products;			
			
			phldrOrdrDetails.Controls.Add(new TableProdSum(prods));
			
			lbl2month.Text = ((IOrderSum)wipper.Wip["ordersummary"]).GetProdSubTotal(2).ToString("c");
			lbl2monthtotal.Text = ((IOrderSum)wipper.Wip["ordersummary"]).GetTaxAmt(2).ToString("c");
			lblAmountDue2.Text = ((IOrderSum)wipper.Wip["ordersummary"]).GetTotalAmtDue(2).ToString("c");

			lblOrderTotal.Text = ((IOrderSum)wipper.Wip["ordersummary"]).GetProdSubTotal(1).ToString("C");
			lblAmountDue.Text  = ((IOrderSum)wipper.Wip["ordersummary"]).GetTotalAmtDue(1).ToString("C");
			lblFees.Text = ((IOrderSum)wipper.Wip["ordersummary"]).GetTaxAmt(1).ToString("C");

			SetMouseOver();
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			lblIlec.Text = ((IILECInfo)wipper.Wip["selectedilec"]).ILECName;
			lblZipCode.Text = (string)wipper.Wip["zip"];
						
			MultiClickBlocker.Block(this, btnPrevious);
			MultiClickBlocker.Block(this, btnGotoMain);
			MultiClickBlocker.Block(this, btnPrint);
			btnPrint.Attributes.Add("onClick", "window.open('PLSummaryPrint.aspx', '_blank' ,'height= 600, width=700 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			MultiClickBlocker.Block(this, btnMonthChart);
			btnMonthChart.Attributes.Add("onClick", "window.open('MonthChart.aspx', '_blank' ,'height= 600, width=660 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=yes,resizable=yes')");
		}
		private void btnGotoMain_Click(object sender, System.Web.UI.ImageClickEventArgs ea)
		{
			Session["IMap"] = null;
			Response.Redirect("MenuScreen.aspx", false);
		}
		private void btnPrevious_Click(object sender, System.Web.UI.ImageClickEventArgs ea)
		{
			try
			{
				wipper.Wip["OrderSummary"] = null;
				Response.Redirect(wipper.Wip.Prev(), false);
			}
			catch (Exception ex)
			{	
				ErrLogSvc.LogError( 
					wipper.IMap, this.ToString(), HttpContext.Current.User.Identity.Name, 
					ex.Message + ", " + ex.StackTrace);
			}
		}

		private void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
		}	
	
		void SetMouseOver()
		{
			StringBuilder sb = new StringBuilder();
			string[] lines = ((IOrderSum)wipper.Wip["ordersummary"]).GetSumTaxDesc(1);
			
			for (int i = 0; i < lines.Length; i++)
				sb.Append(lines[i] + "<BR>");

			lblFees.Text = 
				"<a href='javascript:void(0);' onmouseover='return escape(\""
				+ sb.ToString()
				+ "\")'>" 
				+ lblFees.Text
				+ "</a>";

			sb = new StringBuilder();
			lines = ((IOrderSum)wipper.Wip["ordersummary"]).GetSumTaxDesc(2);
			
			for (int i = 0; i < lines.Length; i++)
				sb.Append(lines[i] + "<BR>");

			lbl2monthtotal.Text = 
				"<a href='javascript:void(0);' onmouseover='return escape(\""
				+ sb.ToString()
				+ "\")'>" 
				+ lbl2monthtotal.Text
				+ "</a>";
	
			Label6.Text = 
				"<a href='javascript:void(0);' onmouseover='return escape(\""
				+ ""
				+ "\")'>" 
				+ " "
				+ "</a>";

		}
	}
}