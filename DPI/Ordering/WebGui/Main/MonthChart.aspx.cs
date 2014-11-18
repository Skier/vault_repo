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
	public class MonthChart : BasePage
	{
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e, false);
			CustomInit();
		}
		
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		protected System.Web.UI.WebControls.Label lblZipCode;
		protected System.Web.UI.WebControls.Label lblIlec;
		protected System.Web.UI.WebControls.Button btnClose;
		protected System.Web.UI.WebControls.Label lblDate;
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.PlaceHolder phldrMonthChart;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.ImageButton btnPrint;
		#endregion

		void CustomInit()
		{	
			const int MONTHS = 9;
			lblZipCode.Text = (string)wipper.Wip["Zip"];
			lblIlec.Text = ((IILECInfo)wipper.Wip["SelectedIlec"]).ILECName;

			IOrderSum sm = (IOrderSum)wipper.Wip["ordersummary"];
			try
			{
				phldrMonthChart.Controls.Add(new TableMonthChart(sm, MONTHS));
			}
			catch(Exception ex)
			{
				ErrLogSvc.LogError(
					wipper.IMap, this.ToString(), HttpContext.Current.User.Identity.Name, 
					ex.Message + ", " + ex.StackTrace);

				lblErrMsg.Text = "There has been an error generating the month chart. Please close the window and try again.";				
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