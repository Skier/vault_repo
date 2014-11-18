using System;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using DPI.Services;
using DPI.Reports;
using DPI.Interfaces;
using DPI.ClientComp;

namespace DPI.Ordering
{
	public class Store_EndOfDayReport : System.Web.UI.Page

	{
		#region Member Variables
		protected DPI.Interfaces.Store_DayTotalsStyle reportStyle;
//		protected CrystalDecisions.CrystalReports.Engine.ReportClass oRpt;
		protected System.Web.UI.WebControls.Button cmdGO;
		protected System.Web.UI.WebControls.Label lblRptTitle;
		protected System.Web.UI.WebControls.Button cmdDone;
		protected System.Web.UI.WebControls.Button cmdPrint;
		protected System.Web.UI.HtmlControls.HtmlTable tblParameters;				
		protected CrystalDecisions.Web.CrystalReportViewer ReportViewer;
		protected System.Web.UI.WebControls.Label lblPayDate;
		protected System.Web.UI.WebControls.ImageButton imgCalendar;
		protected System.Web.UI.HtmlControls.HtmlForm ReportformZ;
		protected System.Web.UI.WebControls.DropDownList ddlPayDateYear;
		protected System.Web.UI.WebControls.DropDownList ddlPayDateDay;
		protected System.Web.UI.WebControls.DropDownList ddlPayDateMonth;
		protected System.Web.UI.WebControls.Calendar calPayDate;
		protected bool showCal;
		protected System.Web.UI.WebControls.Label lblErrMsg;
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
			this.cmdGO.Click += new System.EventHandler(this.cmdGO_Click);
			this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
			this.cmdDone.Click += new System.EventHandler(this.cmdDone_Click);
			this.ddlPayDateMonth.SelectedIndexChanged += new System.EventHandler(this.ddlPayDateMonth_SelectedIndexChanged);
			this.ddlPayDateDay.SelectedIndexChanged += new System.EventHandler(this.ddlPayDateDay_SelectedIndexChanged);
			this.ddlPayDateYear.SelectedIndexChanged += new System.EventHandler(this.ddlPayDateYear_SelectedIndexChanged);
			this.imgCalendar.Click += new System.Web.UI.ImageClickEventHandler(this.imgCalendar_Click);
			this.calPayDate.SelectionChanged += new System.EventHandler(this.calPayDate_SelectionChanged);
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.Page_PreRender);

		}
		#endregion
		
	#region Event Handlers
		void CustomInit()
		{
			try
			{
				if (Request.QueryString["showCal"] != null)
					showCal = bool.Parse(Request.QueryString["showCal"]);
			
				if (Request.QueryString["ReportDate"] != null)
					ViewState["ReportDate"] = DateTime.Parse(Request.QueryString["ReportDate"]);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}	
		void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				if (IsPostBack)
					return;

				ViewState["IsViewingReport"] = true;

				if (showCal)
				{
					ViewState["IsViewingReport"] = false;
					BindDates();
					calPayDate.Visible = true;

					ddlPayDateMonth.SelectedValue = DateTime.Now.Month.ToString();
					ddlPayDateDay.SelectedValue = DateTime.Now.Day.ToString();
					ddlPayDateYear.SelectedValue = DateTime.Now.Year.ToString();
				}
			
				this.cmdPrint.Attributes.Add("onClick", "window.open('" + (string)Session["TempReportExportFile"] + "','_blank','height= 550, width=700 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
				ReportViewer.Visible = (bool)ViewState["IsViewingReport"];			
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void Page_PreRender(object sender, EventArgs e)
		{
			try
			{	
				if (showCal)
					return;

				MakeVisible();

				if (!(bool)ViewState["IsViewingReport"])
					return;
				
				MakeReport();
			}
			catch (Exception ex)
			{
				ViewState["IsViewingReport"] = false;
				MakeVisible();
				ErrorHandler(ex);
			}
		}	

		void imgCalendar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				ViewState["IsViewingReport"] = false;
				calPayDate.Visible = !(calPayDate.Visible);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}		
		void calPayDate_SelectionChanged(object sender, System.EventArgs e)
		{
			try
			{
				ddlPayDateMonth.SelectedValue = calPayDate.SelectedDate.Month.ToString();
				ddlPayDateDay.SelectedValue = calPayDate.SelectedDate.Day.ToString();
				ddlPayDateYear.SelectedValue = calPayDate.SelectedDate.Year.ToString();			
				calPayDate.Visible = false;			
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void ddlPayDateMonth_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				DateTime rptDate = GetRptDate();
				calPayDate.SelectedDate = rptDate;
				calPayDate.VisibleDate = rptDate;			
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void ddlPayDateDay_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				DateTime rptDate = GetRptDate();
				calPayDate.SelectedDate = rptDate;
				calPayDate.VisibleDate = rptDate;			
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void ddlPayDateYear_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				DateTime rptDate = GetRptDate();
				calPayDate.SelectedDate = rptDate;
				calPayDate.VisibleDate = rptDate;			
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void cmdGO_Click(object sender, System.EventArgs e)
		{
			try
			{
				showCal = false;
				calPayDate.Visible = false;
				ViewState["IsViewingReport"] = true;
				ViewState["ReportDate"] = GetRptDate();
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void cmdDone_Click(object sender, System.EventArgs e)
		{
			try
			{
				ViewState["IsViewingReport"] = false;
				Response.Redirect("ReportMain.aspx", false);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}		
		void cmdPrint_Click(object sender, System.EventArgs e)
		{
			try
			{
				showCal = false;
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
	#endregion

	#region Implementation
		void MakeVisible() // toggle
		{
			ReportViewer.Visible = cmdDone.Visible = cmdPrint.Visible    = (bool)ViewState["IsViewingReport"];
			cmdGO.Visible = tblParameters.Visible = lblRptTitle.Visible = !(bool)ViewState["IsViewingReport"];		
		}
		void MakeReport()
		{
			CrystalDecisions.CrystalReports.Engine.ReportClass oRpt = new DPI.Reports.Store_EndOfDayReport();

			oRpt.SummaryInfo.ReportTitle = "End Of Day Report";
			
			oRpt.SetDataSource(ReportSvc.Store_EndOfDay(DPI.ClientComp.User.GetUser(this),
				(DateTime)ViewState["ReportDate"]));
			
			ReportViewer.ReportSource = oRpt;
			
			oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, 
				Server.MapPath((string)Session["TempReportExportFile"]));
		}
		void ErrorHandler(Exception ex)
		{
			lblErrMsg.Visible = true;
			lblErrMsg.Text = Const.GENERAL_ERROR;
			FatalError.SaveErr(ex, Session["User"], this.ToString());
		}
		DateTime GetRptDate()
		{
			return (new DateTime(int.Parse(ddlPayDateYear.SelectedValue), 
				int.Parse(ddlPayDateMonth.SelectedValue), int.Parse(ddlPayDateDay.SelectedValue)));
		}
		void BindDates()
		{
			ddlPayDateMonth.DataSource = DropDownListDate.GetMonths();
			ddlPayDateMonth.DataTextField = "DDLText";
			ddlPayDateMonth.DataValueField  = "DDLValue";
			ddlPayDateMonth.DataBind();

			ddlPayDateDay.DataSource = DropDownListDate.GetDays();	
			ddlPayDateDay.DataTextField = "DDLText";
			ddlPayDateDay.DataValueField  = "DDLValue";
			ddlPayDateDay.DataBind();

			ddlPayDateYear.DataSource = DropDownListDate.GetYears();
			ddlPayDateYear.DataTextField = "DDLText";
			ddlPayDateYear.DataValueField  = "DDLValue";
			ddlPayDateYear.DataBind();
		}

	#endregion
	}	
}