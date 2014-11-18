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
	public class ReceiptViewer: BasePage
	{	
		#region Web Form Designer generated code
		protected CrystalDecisions.Web.CrystalReportViewer ReportViewer;
		protected CrystalDecisions.CrystalReports.Engine.ReportClass rptReceipt;
		protected System.Web.UI.WebControls.Label lblZipCode;
		protected System.Web.UI.WebControls.Label lblIlec;
		protected System.Web.UI.WebControls.ImageButton Imagebutton1;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.ImageButton btnMain;
		protected System.Web.UI.WebControls.ImageButton imgPrint;

		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent()
		{    
			this.Imagebutton1.Click += new System.Web.UI.ImageClickEventHandler(this.Imagebutton1_Click);
			this.btnMain.Click += new System.Web.UI.ImageClickEventHandler(this.btnMain_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		
		#region Event Handlers
		void Page_Load(object sender, System.EventArgs e)
		{	
			try
			{
			
				ReportClass oRpt = ReceiptFactory.GetReportClass(wipper.IMap, wipper.Wip, DPI.ClientComp.User.GetUser(this));
				ReportViewer.ReportSource = oRpt;
				if (! this.IsPostBack)
				{	
					string fileName = ReceiptFactory.GetReceiptExportFilename(wipper.Wip);  
					ViewState["ReceiptReport"] = ReceiptFactory.GetReceiptExportFilename(wipper.Wip);
					oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(fileName));
				}	
				SetAttributes();			
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void btnMain_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				Session["IMap"] = null;
				Response.Redirect("MenuScreen.aspx", false);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void Imagebutton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try {}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}		
		}
		#endregion

		#region Implementations
		void MultiBlock()
		{
			MultiClickBlocker.Block(this, Imagebutton1);
			MultiClickBlocker.Block(this, imgPrint);
			MultiClickBlocker.Block(this, btnMain);
		}
		void SetAttributes()
		{
			imgPrint.Attributes.Add("onClick", "window.open('" + (string)ViewState["ReceiptReport"] + "', '_blank' ,'height= 550, width=700 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			Imagebutton1.Attributes.Add("onClick", "window.open('" + (string)ViewState["ReceiptReport"] + "', '_blank' ,'height= 550, width=700 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
		}
		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr(wipper.IMap, ex, Session["User"], this.ToString());
			lblErrMsg.Visible = true;
			ReportViewer.Visible = false;
			lblErrMsg.Text = Const.GENERAL_ERROR;
		}	
		#endregion
	}
}
