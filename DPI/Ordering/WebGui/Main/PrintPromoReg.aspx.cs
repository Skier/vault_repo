using System;
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
	public class PrintPromoReg : BasePage
	{	 	
		#region Web Form Designer generated code

		protected System.Web.UI.HtmlControls.HtmlForm form_ProductL1;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.ImageButton btnPrint;
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.Label lblDate;
		protected System.Web.UI.WebControls.PlaceHolder phPrintPromoReg;
		protected System.Web.UI.WebControls.Button btnClose;
		
		override protected void OnInit(EventArgs ea)
		{
			InitializeComponent();
			base.OnInit(ea);
			CustomInit();
		}

		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		void CustomInit()
		{
//		 	FeaturesGrid.Controls.Add(
//					new TableProdL1((IProdPrice[])wip["TopProducts"], 
//					FeatureMatrixAdapter.GetMatrix((IProdPrice[])wip["TopProducts"])));

			btnPrint.Attributes.Add("onClick", "window.print()");
			btnClose.Attributes.Add("onClick", "window.close()");


	//		lblErrMsg.Visible = true;
	//		lblErrMsg.Text = Const.GENERAL_ERROR;

		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			lblDate.Text = DateTime.Now.ToShortDateString();
		}
	}
}