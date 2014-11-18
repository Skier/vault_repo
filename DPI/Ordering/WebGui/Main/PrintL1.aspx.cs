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
	public class PrintL1 : System.Web.UI.Page
	{	 	
		#region Web Form Designer generated code

		protected System.Web.UI.HtmlControls.HtmlForm form_ProductL1;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.ImageButton btnPrint;
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.Label lblDate;
		protected System.Web.UI.WebControls.Button btnClose;
		protected System.Web.UI.WebControls.Label lblZipCode;
		protected System.Web.UI.WebControls.Label lblIlec;
		protected System.Web.UI.WebControls.PlaceHolder FeaturesGrid;
		
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
			IMap imap = (IMap)Session["IMap"];

			if (imap == null)
				throw new ApplicationException("No WIP");

			WIP wip = (WIP)imap.find(WIP.IKeyS);
			if (wip["TopProducts"] == null)
				throw new ApplicationException("No Top Products");
	
			lblZipCode.Text = (string)wip["Zip"];
			lblIlec.Text = ((IILECInfo)wip["SelectedIlec"]).ILECName;

			for (int i = 0; i < ((IProdPrice[])wip["TopProducts"]).Length; i++)
				if (((IProdPrice[])wip["TopProducts"])[i].ProdSelState == ProdSelectionState.Selected)
					wip["SelectedBasicService"] = ((IProdPrice[])wip["TopProducts"])[i];

			FeaturesGrid.Controls.Add(
				new TableProdL1((IKeyVal[])wip["Discounts"], (IProdPrice[])wip["TopProducts"], 
				FeatureMatrixAdapter.GetMatrix((IProdPrice[])wip["TopProducts"])));

			btnPrint.Attributes.Add("onClick", "window.print()");
			btnClose.Attributes.Add("onClick", "window.close()");


			//		lblErrMsg.Visible = true;
			//		lblErrMsg.Text = Const.GENERAL_ERROR;

		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			DateTime rightNow = DateTime.Now;
			lblDate.Text = String.Format("{0:d}", rightNow);
		}
	}
}