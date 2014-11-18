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
	public class CertTrainingReport : System.Web.UI.Page
	{
		#region Member Variables
		protected DPI.Interfaces.CertResultCriteria reportCriteria;		
		protected DPI.Reports.CertResult oRpt;
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
			//reportCriteria = (CertResultCriteria)int.Parse(Request.QueryString["reportcriteria"]);			
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
				reportCriteria = CertResultCriteria.Store;

				lblRptTitle.Text = CertResultsReportFactory.GetTitle(reportCriteria);
				CreateCertResultReport();
			}
			this.cmdPrint.Attributes.Add("onClick", "window.open('" + (string)Session["TempReportExportFile"] + "','_blank','height= 550, width=700 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");									
			
//			this.imgInstruction1.Attributes.Add("onClick", "window.alert('" + (string)Store_OrderStatusReportFactory.GetInstructions(reportCriteria)[0] + "');");			
//			this.imgInstruction2.Attributes.Add("onClick", "window.alert('" + (string)Store_OrderStatusReportFactory.GetInstructions(reportCriteria)[1] + "');");			

			//ReportViewer.Visible = (bool)ViewState["IsViewingReport"];			
		}
		private void Page_PreRender(object sender, EventArgs e)
		{
		// uncomment when we have multiple criteria

			if(IsPostBack)
			{
				if ((bool)ViewState["IsViewingReport"])
				{						
					CreateCertResultReport();
				}
			}
		}
		private void cmdGO_Click(object sender, System.EventArgs e)
		{

//			reportCriteria = CertResultCriteria.Store;
//
//			if (rbtnStore.SelectedValue == "Corporation")
//				reportCriteria = CertResultCriteria.Corporation;
//
//			ViewState["IsViewingReport"] = true;			
		}
		private void CreateCertResultReport()
		{
			try
			{
				oRpt = new DPI.Reports.CertResult();						
				oRpt.SummaryInfo.ReportTitle = CertResultsReportFactory.GetTitle(reportCriteria);
				oRpt.SetDataSource(GetCertResultData(reportCriteria));		
				ReportViewer.ReportSource = oRpt;
				oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath((string)Session["TempReportExportFile"]));
				ReportViewer.Visible=true;
				cmdDone.Visible=true;
				cmdPrint.Visible=true;		
				cmdGO.Visible=false;
				tblParameters.Visible = false;
				lblRptTitle.Visible=false;								
			}
			catch (ArgumentException ae)
			{
				lblErrorMsg.Text = ae.Message;
				ViewState["IsViewingReport"] = false;
			}
			catch (Exception)
			{
				lblErrorMsg.Text = Const.GENERAL_ERROR;
				ViewState["IsViewingReport"] = false;
			}
		}
		private DataSet GetCertResultData(CertResultCriteria reportCriteria)
		{
			switch (reportCriteria)
			{
				case CertResultCriteria.Store :
					return CertResultsReportFactory.GetDataSource(DPI.ClientComp.User.GetUser(this));
				case CertResultCriteria.Corporation :					
					return CertResultsReportFactory.GetDataSource(StoreSvc.GetCorporation(DPI.ClientComp.User.GetUser(this).LoginStoreCode).CorpID);				
				default:
					throw new ApplicationException("Unknow certification result criteria: " + reportCriteria.ToString());
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
