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

using DPI.ClientComp;
using DPI.Services;
using DPI.Ordering;
using DPI.Interfaces;

namespace DPI.Ordering
{
	public class PLSummaryPrint : BasePage
	{
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e, false);
			CustomInit();
		}
		
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label lblAmountDue;
		protected System.Web.UI.WebControls.Label lblFees;
		protected System.Web.UI.WebControls.Label lblOrderTotal;
		protected System.Web.UI.WebControls.Label lblZipCode;
		protected System.Web.UI.WebControls.Label lblIlec;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.PlaceHolder phldrOrdrDetails;
		protected System.Web.UI.WebControls.Button btnClose;
		protected System.Web.UI.WebControls.Label lblDate;
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.ImageButton btnPrint;
		#endregion

		void CustomInit()
		{	
			try
			{
				IMap imap = (IMap)Session["IMap"];

				if (imap == null)
					throw new ApplicationException("No WIP");

				WIP wip = (WIP)imap.find(WIP.IKeyS);

				lblZipCode.Text = (string)wipper.Wip["Zip"];
				lblIlec.Text = ((IILECInfo)wipper.Wip["SelectedIlec"]).ILECName;

				WipOrderSummary.CheckOrderSummary(wipper.IMap);
			
				IProdPrice[] prods = ((IOrderSum)wipper.Wip["ordersummary"]).Products;			
			
				phldrOrdrDetails.Controls.Add(new TableProdSum(prods));
			
				lblOrderTotal.Text = ((IOrderSum)wipper.Wip["ordersummary"]).GetProdSubTotal(1).ToString("C"); //.ProdSubTotal.ToString("C");
				lblAmountDue.Text  = ((IOrderSum)wipper.Wip["ordersummary"]).GetTotalAmtDue(1).ToString("C"); //.TotalAmtDue.ToString("C");
				lblFees.Text = ((IOrderSum)wipper.Wip["ordersummary"]).GetTaxAmt(1).ToString("C");	//.TaxAmt.ToString("C");
			}
			catch (Exception Ex)
			{

			}

		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnPrint.Attributes.Add("onClick", "window.print()");
			btnClose.Attributes.Add("onClick", "window.close()");
			DateTime rightNow = DateTime.Now;
			lblDate.Text = String.Format("{0:d}", rightNow);
		}
	}
}