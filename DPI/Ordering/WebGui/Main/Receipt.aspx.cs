using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using DPI.Interfaces;
using DPI.Reports;
using DPI.Services;
using DPI.ClientComp;

namespace DPI.Ordering
{
	public class Receipt : BasePage
	{
		#region Data
		protected CrystalDecisions.Web.CrystalReportViewer ReportViewer;
		protected CrystalDecisions.CrystalReports.Engine.ReportClass rptReceipt;		
		protected System.Web.UI.WebControls.ImageButton btnGotoMain;
		protected System.Web.UI.WebControls.Label lblZipCode;
		protected System.Web.UI.WebControls.Label lblIlec;
		protected System.Web.UI.WebControls.ImageButton Imagebutton1;
		protected System.Web.UI.WebControls.ImageButton btnSvcPwrSchedule;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnIsRAC;
		protected System.Web.UI.WebControls.ImageButton imgPrint;
		#endregion
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			CustomInit();
		}
		
		private void InitializeComponent()
		{    
			this.btnSvcPwrSchedule.Click += new System.Web.UI.ImageClickEventHandler(this.btnSvcPwrSchedule_Click);
			this.Imagebutton1.Click += new System.Web.UI.ImageClickEventHandler(this.Imagebutton1_Click);
			this.btnGotoMain.Click += new System.Web.UI.ImageClickEventHandler(this.btnGotoMain_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Event Handlers
		private void Page_Load(object sender, System.EventArgs e)
		{				
			ReportClass oRpt = ReceiptReportFactory.GetReceiptReport(wipper.Wip, DPI.ClientComp.User.GetUser(this));
			ReportViewer.ReportSource = oRpt;
			
			if (!IsPostBack)
			{	
				string fileName = ReceiptReportFactory.GetReceiptExportFilename(wipper.Wip);  
				ViewState["ReceiptReport"] = fileName;				
				oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(fileName));
			}	
			AddAttrs();
		}

		void btnGotoMain_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Session["IMap"] = null;
			Response.Redirect("MenuScreen.aspx", false);
		}

		void Imagebutton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
		
		}

		void btnSvcPwrSchedule_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				Response.Redirect("SatelliteSchedular.aspx", false);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}			
		}
		#endregion

		#region Implementations
		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr(wipper.IMap, ex, Session["User"], this.ToString());			
		}
		void AddAttrs()
		{
			imgPrint.Attributes.Add("onClick", "window.open('" + (string)ViewState["ReceiptReport"] + "', '_blank' ,'height= 550, width=700 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			Imagebutton1.Attributes.Add("onClick", "window.open('" + (string)ViewState["ReceiptReport"] + "', '_blank' ,'height= 550, width=700 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			btnSvcPwrSchedule.Attributes.Add("onClick", "clickedButton = true;");
		}
		void MultiBlock()
		{	
			MultiClickBlocker.Block(this, Imagebutton1);
			MultiClickBlocker.Block(this, imgPrint);
			MultiClickBlocker.Block(this, btnGotoMain);
		}
		void SetRacParam()
		{
			hdnIsRAC.Value = "no";
			
			if (StoreSvc.IsRac_WF((IUser)Session["User"]))
				hdnIsRAC.Value = "yes";
		}
		void CustomInit()
		{
			try
			{
				MultiBlock();
				SetRacParam();
				SetSatelliteParam();
				
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void SetSatelliteParam()
		{
			if (wipper.Wip.Workflow is SatelliteWF)
				btnSvcPwrSchedule.Visible = true;
		}
		#endregion		
	}
}
