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
	public class Store_DayTotals : System.Web.UI.Page

	{
		#region Member Variables
		protected DPI.Interfaces.Store_DayTotalsStyle reportStyle;
		protected CrystalDecisions.CrystalReports.Engine.ReportClass oRpt;
		protected System.Web.UI.WebControls.Button cmdGO;
		protected System.Web.UI.WebControls.Label lblRptTitle;
		protected System.Web.UI.WebControls.Button cmdDone;
		protected System.Web.UI.WebControls.Button cmdPrint;
		protected System.Web.UI.HtmlControls.HtmlTable tblParameters;				
		protected CrystalDecisions.Web.CrystalReportViewer ReportViewer;
		protected System.Web.UI.WebControls.Label lblPayDate;
		protected System.Web.UI.WebControls.ImageButton imgCalendar;
		protected System.Web.UI.HtmlControls.HtmlForm ReportformZ;
		protected System.Web.UI.WebControls.Label lblErrorMsg;
		protected System.Web.UI.WebControls.DropDownList ddlPayDateYear;
		protected System.Web.UI.WebControls.DropDownList ddlPayDateDay;
		protected System.Web.UI.WebControls.DropDownList ddlPayDateMonth;
		protected System.Web.UI.WebControls.Calendar calPayDate;
		#endregion
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			reportStyle = (Store_DayTotalsStyle)int.Parse(Request.QueryString["reportstyle"]);
		}		
		private void InitializeComponent()
		{    			
			this.cmdDone.Click += new System.EventHandler(this.cmdDone_Click);
			this.ddlPayDateMonth.SelectedIndexChanged += new System.EventHandler(this.ddlPayDateMonth_SelectedIndexChanged);
			this.ddlPayDateDay.SelectedIndexChanged += new System.EventHandler(this.ddlPayDateDay_SelectedIndexChanged);
			this.ddlPayDateYear.SelectedIndexChanged += new System.EventHandler(this.ddlPayDateYear_SelectedIndexChanged);
			this.imgCalendar.Click += new System.Web.UI.ImageClickEventHandler(this.imgCalendar_Click);
			this.calPayDate.SelectionChanged += new System.EventHandler(this.calPayDate_SelectionChanged);
			this.cmdGO.Click += new System.EventHandler(this.cmdGO_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.Page_PreRender);

		}
		#endregion
		#region Methods
		private void BindDates()
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
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{  		
				ViewState["IsViewingReport"] = false;				

				BindDates();

				ddlPayDateMonth.SelectedValue = DateTime.Now.Month.ToString();
				ddlPayDateDay.SelectedValue = DateTime.Now.Day.ToString();
				ddlPayDateYear.SelectedValue = DateTime.Now.Year.ToString();
			}
			this.cmdPrint.Attributes.Add("onClick", "window.open('" + (string)Session["TempReportExportFile"] + "','_blank','height= 550, width=700 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			ReportViewer.Visible = (bool)ViewState["IsViewingReport"];			
		}
		private void Page_PreRender(object sender, EventArgs e)
		{
			if(IsPostBack)
			{
				if ((bool)ViewState["IsViewingReport"])
				{			
					try
					{
						oRpt = DPI.Reports.Store_DayTotalsReportFactory.GetDayTotalsReport(reportStyle);
						oRpt.SummaryInfo.ReportTitle = "Daily Totals";
						oRpt.SetDataSource(ReportSvc.Store_GetDayTotals(DPI.ClientComp.User.GetUser(this), GetPayDate()));
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
			}
		}				
		private DateTime GetPayDate()
		{
			return (new DateTime(int.Parse(ddlPayDateYear.SelectedValue), 
				int.Parse(ddlPayDateMonth.SelectedValue), int.Parse(ddlPayDateDay.SelectedValue)));
		}
		private void imgCalendar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			calPayDate.Visible = !(calPayDate.Visible);
		}		
		private void calPayDate_SelectionChanged(object sender, System.EventArgs e)
		{
			ddlPayDateMonth.SelectedValue = calPayDate.SelectedDate.Month.ToString();
			ddlPayDateDay.SelectedValue = calPayDate.SelectedDate.Day.ToString();
			ddlPayDateYear.SelectedValue = calPayDate.SelectedDate.Year.ToString();			
			calPayDate.Visible = false;			
		}
		private void ddlPayDateMonth_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DateTime payDateDate = GetPayDate();
			calPayDate.SelectedDate = payDateDate;
			calPayDate.VisibleDate = payDateDate;			
		}
		private void ddlPayDateDay_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DateTime payDateDate = GetPayDate();
			calPayDate.SelectedDate = payDateDate;
			calPayDate.VisibleDate = payDateDate;			
		}
		private void ddlPayDateYear_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DateTime payDateDate = GetPayDate();
			calPayDate.SelectedDate = payDateDate;
			calPayDate.VisibleDate = payDateDate;			
		}
		private void cmdGO_Click(object sender, System.EventArgs e)
		{
			ViewState["IsViewingReport"] = true;			
		}
		private void cmdDone_Click(object sender, System.EventArgs e)
		{
			ViewState["IsViewingReport"] = false;
			Response.Redirect("ReportMain.aspx", false);
		}
		#endregion
	}	
}
