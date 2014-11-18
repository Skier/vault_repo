using System;
using System.Data;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DPI.Reports;
using DPI.Services;
using DPI.Interfaces;
using DPI.ClientComp;

namespace DPI.Ordering.Main
{
	public class PendOrderPymtInfoRpt : System.Web.UI.Page
	{
		#region Member Variables
		protected DPI.Interfaces.PendingOrderPaymentInfoCriteria reportCriteria;		
		protected DPI.Reports.PendingOrderPaymentInfo oRpt;
		protected System.Web.UI.WebControls.Button cmdGO;
		protected System.Web.UI.WebControls.Label lblRptTitle;
		protected System.Web.UI.WebControls.Button cmdDone;
		protected System.Web.UI.WebControls.Button cmdPrint;
		protected System.Web.UI.HtmlControls.HtmlTable tblParameters;
		protected System.Web.UI.WebControls.Label lblErrorMsg;
		protected System.Web.UI.WebControls.RadioButtonList rbtnStore;				
		protected CrystalDecisions.Web.CrystalReportViewer ReportViewer;
		#endregion
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			//reportCriteria = (PendingOrderPaymentInfoCriteria)int.Parse(Request.QueryString["reportcriteria"]);			
		}		
		private void InitializeComponent()
		{    
			this.cmdGO.Click += new System.EventHandler(this.cmdGO_Click);
			this.cmdDone.Click += new System.EventHandler(this.cmdDone_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.Page_PreRender);

		}
		#endregion
		#region Methods

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{  		
				//ViewState["IsViewingReport"] = false;		
				ViewState["IsViewingReport"] = true;				
				//no need to select criteria at this time as we have only one criteria
				reportCriteria = PendingOrderPaymentInfoCriteria.Corporation;

				lblRptTitle.Text =  PendingOrderPaymentInfoRF.GetTitle(reportCriteria);
				CreatePendingOrderPaymentInfoReport();
			}
			this.cmdPrint.Attributes.Add("onClick", "window.open('" + (string)Session["TempReportExportFile"] + "','_blank','height= 550, width=700 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");									
			

			//ReportViewer.Visible = (bool)ViewState["IsViewingReport"];			
		}
		private void Page_PreRender(object sender, EventArgs e)
		{
		// uncomment when we have multiple criteria

			
			if(IsPostBack)
			{
				if ((bool)ViewState["IsViewingReport"])
				{						
					CreatePendingOrderPaymentInfoReport();
				}
			}
		}
		private void cmdGO_Click(object sender, System.EventArgs e)
		{

//			reportCriteria = PendingOrderPaymentInfoCriteria.Store;
//
//			if (rbtnStore.SelectedValue == "Corporation")
//				reportCriteria = PendingOrderPaymentInfoCriteria.Corporation;
//
//			ViewState["IsViewingReport"] = true;			
		}
		private void CreatePendingOrderPaymentInfoReport()
		{
			try
			{
				oRpt = new DPI.Reports.PendingOrderPaymentInfo();						
				
				oRpt.SummaryInfo.ReportTitle = PendingOrderPaymentInfoRF.GetTitle(reportCriteria);
				oRpt.SetDataSource(GetPendingOrderPaymentInfoData(reportCriteria));		
				oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath((string)Session["TempReportExportFile"]));
				
				ReportViewer.ReportSource = oRpt;				
				ReportViewer.Visible = cmdDone.Visible = cmdPrint.Visible = true;		
				cmdGO.Visible = tblParameters.Visible = lblRptTitle.Visible =false;								
			}
			catch (ArgumentException ae)
			{
				lblErrorMsg.Text = ae.Message;
				ViewState["IsViewingReport"] = false;
			}
			catch (Exception ex)
			{
				lblErrorMsg.Text = Const.GENERAL_ERROR;
				ViewState["IsViewingReport"] = false;
			}
		}
		private DataSet GetPendingOrderPaymentInfoData(PendingOrderPaymentInfoCriteria reportCriteria)
		{
			switch (reportCriteria)
			{
//				case PendingOrderPaymentInfoCriteria.Store :
//					return PendingOrderPaymentInfoRF.GetDataSource(DPI.ClientComp.User.GetUser(this));
				
				case PendingOrderPaymentInfoCriteria.Corporation :					
					return PendingOrderPaymentInfoRF.GetDataSource(StoreSvc.GetCorporation(DPI.ClientComp.User.GetUser(this).LoginStoreCode).CorpID);				
				
				default:
					throw new ApplicationException("Unknow Pending Order Payment Info criteria: " + reportCriteria.ToString());
			}
		}
		private void cmdDone_Click(object sender, System.EventArgs e)
		{
			ViewState["IsViewingReport"] = false;
			Response.Redirect("ReportMain.aspx", false);
		}
		#endregion
	}
}
