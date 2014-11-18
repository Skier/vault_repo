using System;
using System.Data;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DPI.Reports;
using DPI.Services;
using DPI.Interfaces;
using DPI.ClientComp;

namespace DPI.Ordering
{
	public class Store_OrderStatus : System.Web.UI.Page

	{
	#region Member Variables
		protected DPI.Interfaces.Store_OrderStatusCriteria reportCriteria;		
		protected DPI.Reports.Store_OrderStatus oRpt;
		protected System.Web.UI.WebControls.Button cmdGO;
		protected System.Web.UI.WebControls.Label lblRptTitle;
		protected System.Web.UI.WebControls.Button cmdDone;
		protected System.Web.UI.WebControls.Button cmdPrint;
		protected System.Web.UI.HtmlControls.HtmlTable tblParameters;
		protected System.Web.UI.WebControls.Label lblStartDate;
		protected System.Web.UI.WebControls.ImageButton imgCalendarStartDate;
		protected System.Web.UI.WebControls.Calendar calStartDate;
		protected System.Web.UI.WebControls.Label lblEndDate;
		protected System.Web.UI.WebControls.ImageButton imgCalendarEndDate;
		protected System.Web.UI.WebControls.Calendar calEndDate;
		protected System.Web.UI.WebControls.Label lblErrorMsg;
		protected System.Web.UI.WebControls.DropDownList ddlStartMonth;
		protected System.Web.UI.WebControls.DropDownList ddlStartDay;
		protected System.Web.UI.WebControls.DropDownList ddlStartYear;
		protected System.Web.UI.WebControls.DropDownList ddlEndYear;
		protected System.Web.UI.WebControls.DropDownList ddlEndDay;
		protected System.Web.UI.WebControls.DropDownList ddlEndMonth;
		protected System.Web.UI.WebControls.ImageButton imgInstruction1;
		protected System.Web.UI.WebControls.ImageButton imgInstruction2;				
		protected CrystalDecisions.Web.CrystalReportViewer ReportViewer;
	#endregion

	#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			reportCriteria = (Store_OrderStatusCriteria)int.Parse(Request.QueryString["reportcriteria"]);			
		}		
		private void InitializeComponent()
		{    
			this.cmdDone.Click += new System.EventHandler(this.cmdDone_Click);
			this.ddlStartMonth.SelectedIndexChanged += new System.EventHandler(this.ddlStartMonth_SelectedIndexChanged);
			this.ddlStartDay.SelectedIndexChanged += new System.EventHandler(this.ddlStartDay_SelectedIndexChanged);
			this.ddlStartYear.SelectedIndexChanged += new System.EventHandler(this.ddlStartYear_SelectedIndexChanged);
			this.imgCalendarStartDate.Click += new System.Web.UI.ImageClickEventHandler(this.imgCalendarStartDate_Click);
			this.calStartDate.SelectionChanged += new System.EventHandler(this.calStartDate_SelectionChanged);
			this.ddlEndMonth.SelectedIndexChanged += new System.EventHandler(this.ddlEndMonth_SelectedIndexChanged);
			this.ddlEndDay.SelectedIndexChanged += new System.EventHandler(this.ddlEndDay_SelectedIndexChanged);
			this.ddlEndYear.SelectedIndexChanged += new System.EventHandler(this.ddlEndYear_SelectedIndexChanged);
			this.imgCalendarEndDate.Click += new System.Web.UI.ImageClickEventHandler(this.imgCalendarEndDate_Click);
			this.calEndDate.SelectionChanged += new System.EventHandler(this.calEndDate_SelectionChanged);
			this.cmdGO.Click += new System.EventHandler(this.cmdGO_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.Page_PreRender);

		}
	#endregion

	#region Events
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{  		
				ViewState["IsViewingReport"] = false;				
				
				BindDates();

				ddlStartMonth.SelectedValue = DateTime.Now.Month.ToString();
				ddlStartDay.SelectedValue = DateTime.Now.Day.ToString();
				ddlStartYear.SelectedValue = DateTime.Now.Year.ToString();

				ddlEndMonth.SelectedValue = DateTime.Now.Month.ToString();
				ddlEndDay.SelectedValue = DateTime.Now.Day.ToString();
				ddlEndYear.SelectedValue = DateTime.Now.Year.ToString();

				lblRptTitle.Text = Store_OrderStatusReportFactory.GetTitle(reportCriteria);
			}
			this.cmdPrint.Attributes.Add("onClick", "window.open('" + (string)Session["TempReportExportFile"] + "','_blank','height= 550, width=700 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");									
			
			this.imgInstruction1.Attributes.Add("onClick", "window.alert('" + (string)Store_OrderStatusReportFactory.GetInstructions(reportCriteria)[0] + "');");			
			this.imgInstruction2.Attributes.Add("onClick", "window.alert('" + (string)Store_OrderStatusReportFactory.GetInstructions(reportCriteria)[1] + "');");			

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
						oRpt = new DPI.Reports.Store_OrderStatus();						
						oRpt.SummaryInfo.ReportTitle = Store_OrderStatusReportFactory.GetTitle(reportCriteria);
						
						oRpt.SetDataSource(Store_OrderStatusReportFactory.GetDataSource(reportCriteria, 
							DPI.ClientComp.User.GetUser(this), GetStartDate(), GetEndDate()));		
						
						ReportViewer.ReportSource = oRpt;
						oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath((string)Session["TempReportExportFile"]));
						ReportViewer.Visible = true;
						
						cmdDone.Visible = cmdPrint.Visible = true;		
						cmdGO.Visible = tblParameters.Visible = lblRptTitle.Visible = false;								
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
		
		private void imgCalendarStartDate_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			calStartDate.Visible = !(calStartDate.Visible);
		}		
		private void calStartDate_SelectionChanged(object sender, System.EventArgs e)
		{
			ddlStartMonth.SelectedValue = calStartDate.SelectedDate.Month.ToString();
			ddlStartDay.SelectedValue = calStartDate.SelectedDate.Day.ToString();
			ddlStartYear.SelectedValue = calStartDate.SelectedDate.Year.ToString();			
			calStartDate.Visible = false;			
		}
		private void ddlStartMonth_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DateTime startDate = GetStartDate();
			calStartDate.SelectedDate = startDate;
			calStartDate.VisibleDate = startDate;			
		}
		private void ddlStartDay_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DateTime startDate = GetStartDate();
			calStartDate.SelectedDate = startDate;
			calStartDate.VisibleDate = startDate;			
		}
		private void ddlStartYear_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DateTime startDate = GetStartDate();
			calStartDate.SelectedDate = startDate;
			calStartDate.VisibleDate = startDate;			
		}
		
		private void imgCalendarEndDate_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			calEndDate.Visible = !(calEndDate.Visible);
		}
		private void calEndDate_SelectionChanged(object sender, System.EventArgs e)
		{
			ddlEndMonth.SelectedValue = calEndDate.SelectedDate.Month.ToString();
			ddlEndDay.SelectedValue = calEndDate.SelectedDate.Day.ToString();
			ddlEndYear.SelectedValue = calEndDate.SelectedDate.Year.ToString();			
			calEndDate.Visible = false;					
		}
		private void ddlEndMonth_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DateTime endDate = GetEndDate();
			calEndDate.SelectedDate = endDate;
			calEndDate.VisibleDate = endDate;			
		}
		private void ddlEndDay_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DateTime endDate = GetEndDate();
			calEndDate.SelectedDate = endDate;
			calEndDate.VisibleDate = endDate;			
		}
		private void ddlEndYear_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DateTime endDate = GetEndDate();
			calEndDate.SelectedDate = endDate;
			calEndDate.VisibleDate = endDate;			
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

	#region Implementations
		private void BindDates()
		{
			ddlStartMonth.DataSource = DropDownListDate.GetMonths();
			ddlStartMonth.DataTextField = "DDLText";
			ddlStartMonth.DataValueField  = "DDLValue";
			ddlStartMonth.DataBind();

			ddlStartDay.DataSource = DropDownListDate.GetDays();	
			ddlStartDay.DataTextField = "DDLText";
			ddlStartDay.DataValueField  = "DDLValue";
			ddlStartDay.DataBind();

			ddlStartYear.DataSource = DropDownListDate.GetYears();
			ddlStartYear.DataTextField = "DDLText";
			ddlStartYear.DataValueField  = "DDLValue";
			ddlStartYear.DataBind();

			ddlEndMonth.DataSource = DropDownListDate.GetMonths();
			ddlEndMonth.DataTextField = "DDLText";
			ddlEndMonth.DataValueField  = "DDLValue";
			ddlEndMonth.DataBind();

			ddlEndDay.DataSource = DropDownListDate.GetDays();	
			ddlEndDay.DataTextField = "DDLText";
			ddlEndDay.DataValueField  = "DDLValue";
			ddlEndDay.DataBind();

			ddlEndYear.DataSource = DropDownListDate.GetYears();
			ddlEndYear.DataTextField = "DDLText";
			ddlEndYear.DataValueField  = "DDLValue";
			ddlEndYear.DataBind();
		}

		private DateTime GetStartDate()
		{
			return (new DateTime(int.Parse(ddlStartYear.SelectedValue), 
				int.Parse(ddlStartMonth.SelectedValue), int.Parse(ddlStartDay.SelectedValue)));
		}
		private DateTime GetEndDate()
		{
			return (new DateTime(int.Parse(ddlEndYear.SelectedValue), 
				int.Parse(ddlEndMonth.SelectedValue), int.Parse(ddlEndDay.SelectedValue)));
		}
	#endregion
	}	
}
