using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;

using DPI.ClientComp;
using DPI.Ordering;
using DPI.Reports;
using DPI.Interfaces;
using DPI.Services;

namespace DPI.Ordering
{
	public class ReportMain : System.Web.UI.Page
	{
		#region Member Variables

		protected System.Web.UI.WebControls.LinkButton lnkStore_DayTotals;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.ImageButton imgDTotals;
		protected System.Web.UI.WebControls.ImageButton imgDDetail;
		protected System.Web.UI.WebControls.LinkButton lnkStore_OrderStatus_All_PayDate;
		protected System.Web.UI.WebControls.LinkButton lnkStore_OrderStatus_All_AccountStatus;
		protected System.Web.UI.WebControls.LinkButton lnkStore_OrderStatus_Active_PayDate;
		protected System.Web.UI.WebControls.LinkButton lnkStore_OrderStatus_Disconnected_PayDate;
		protected System.Web.UI.WebControls.LinkButton lnkStore_OrderStatus_Pending_PayDate;
		protected System.Web.UI.WebControls.LinkButton lnkStore_OrderStatus_Active_DueDate;
		protected System.Web.UI.WebControls.ImageButton imgStore_OrderStatus_All_PayDate;
		protected System.Web.UI.WebControls.ImageButton imgStore_OrderStatus_All_AccountStatus;
		protected System.Web.UI.WebControls.ImageButton imgStore_OrderStatus_Active_PayDate;
		protected System.Web.UI.WebControls.ImageButton imgStore_OrderStatus_Disconnected_PayDate;
		protected System.Web.UI.WebControls.ImageButton imgStore_OrderStatus_Pending_PayDate;
		protected System.Web.UI.WebControls.ImageButton imgStore_OrderStatus_Active_DueDate;
		protected System.Web.UI.WebControls.Label lblReportMenu;
		protected System.Web.UI.WebControls.ImageButton imgStore_CustomerList;
		protected System.Web.UI.WebControls.LinkButton lnkStore_CustomerList;
		protected System.Web.UI.WebControls.ImageButton imgStore_CustomerList_Active_ActiveDate;
		protected System.Web.UI.WebControls.LinkButton lnkStore_CustomerList_Active_ActiveDate;
		protected System.Web.UI.WebControls.ImageButton imgStore_CustomerList_Disconnected_DueDate;
		protected System.Web.UI.WebControls.LinkButton lnkStore_CustomerList_Disconnected_DueDate;
		protected System.Web.UI.WebControls.ImageButton imgStore_Commission_VeriFone;
		protected System.Web.UI.WebControls.LinkButton lnkStore_Commission_VeriFone;
		protected System.Web.UI.WebControls.ImageButton imgStore_Commission_CellFone;
		protected System.Web.UI.WebControls.LinkButton lnkStore_Commission_CellFone;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.ImageButton imgCR;
		protected System.Web.UI.WebControls.LinkButton lnkCR;
		protected System.Web.UI.WebControls.LinkButton lnkPOPI;
		protected System.Web.UI.WebControls.ImageButton imgPOPI;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist2;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist3;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist4;
		protected System.Web.UI.HtmlControls.HtmlTable Table1;
		protected System.Web.UI.WebControls.ImageButton btnDailyReport;
		protected System.Web.UI.WebControls.ImageButton btnCustList;
		protected System.Web.UI.WebControls.ImageButton btnCommissions;
		protected System.Web.UI.WebControls.ImageButton btnCertification;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DropDownList ddlPayDateMonth;
		protected System.Web.UI.WebControls.DropDownList ddlPayDateDay;
		protected System.Web.UI.WebControls.DropDownList ddlPayDateYear;
		protected System.Web.UI.WebControls.ImageButton imgCalendar;
		protected System.Web.UI.WebControls.ImageButton btnEndOfDayRpt;		
		protected System.Web.UI.WebControls.LinkButton lnkStore_DayTotalsDetail;		
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			if(!HttpContext.Current.User.IsInRole(Const.PERMISSION_REPORTING))
				Response.Redirect("MenuScreen.aspx", false);

			InitializeComponent();
			base.OnInit(e);
			foreach(object ctrl in Controls)
				if ((ctrl is System.Web.UI.WebControls.ImageButton) || (ctrl is System.Web.UI.WebControls.LinkButton))
				{
					((System.Web.UI.WebControls.ImageButton)ctrl).Attributes.Add("onClick", "clickedButton=true; ");
					((System.Web.UI.WebControls.LinkButton)ctrl).Attributes.Add("onClick", "clickedButton=true; ");
				}
		}
		
		private void InitializeComponent()
		{    
			this.btnEndOfDayRpt.Click += new System.Web.UI.ImageClickEventHandler(this.btnEndOfDayRpt_Click);
			this.imgCalendar.Click += new System.Web.UI.ImageClickEventHandler(this.imgCalendar_Click);
			this.btnDailyReport.Click += new System.Web.UI.ImageClickEventHandler(this.btnDailyReport_Click);
			this.btnCustList.Click += new System.Web.UI.ImageClickEventHandler(this.btnCustList_Click);
			this.btnCommissions.Click += new System.Web.UI.ImageClickEventHandler(this.btnCommissions_Click);
			this.btnCertification.Click += new System.Web.UI.ImageClickEventHandler(this.btnCertification_Click);
			this.Unload += new System.EventHandler(this.Page_UnLoad);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		protected System.Web.UI.WebControls.ImageButton btnNewAccount;
		protected System.Web.UI.WebControls.LinkButton lnkNewAccount;
		protected System.Web.UI.WebControls.Label ss;
		protected System.Web.UI.HtmlControls.HtmlTable ReportMenu;
		#endregion	
	
		#region report Methods
		void Page_Load(object sender, System.EventArgs e)
		{				
			if (IsPostBack)
				return;
			
			if (StoreSvc.ShowEndOfDayRpt((IUser)Session["User"]))
				BindDates();			
			
			Session["TempReportExportFile"] = Const.VIRTUAL_DIR_TEMP + Session.SessionID.ToString() + ".pdf";						
		}
		void Page_UnLoad(object sender, System.EventArgs e)
		{
			if (System.IO.File.Exists(Server.MapPath((string)Session["TempReportExportFile"])))
			{
				System.IO.File.Delete(Server.MapPath((string)Session["TempReportExportFile"]));
			}
		}

		private void btnDailyReport_Click(object sender,System.Web.UI.ImageClickEventArgs e)
		{
			int i = DropDownList1.SelectedIndex;

			switch (i)
			{
				case 0 :
					return;		

				case 1 :
					Response.Redirect("Store_DayTotals.aspx?reportstyle=" + 
					((int)Store_DayTotalsStyle.Summary).ToString(), false);
					break;

				case 2 :
					Response.Redirect("Store_DayTotals.aspx?reportstyle=" + 
					((int)Store_DayTotalsStyle.Detail).ToString(), false);
					break;

				default :
					throw new ArgumentException("DropDownlist Selection is incorrect or missing");
					break;
			}
			
		}

		private void btnCustList_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			int i = Dropdownlist2.SelectedIndex;

			switch (i)
			{
				case 0 :
					return;		

				case 5 :
				{
					Response.Redirect("Store_OrderStatus.aspx?reportcriteria=" 
						+ ((int)Store_OrderStatusCriteria.All_PayDate).ToString(), false);
					break;
				}
				case 3 :
				{
					Response.Redirect("Store_OrderStatus.aspx?reportcriteria="
						+ ((int)Store_OrderStatusCriteria.Active_PayDate).ToString(), false);
					break;
				}
				case 8 :
				{
					Response.Redirect("Store_OrderStatus.aspx?reportcriteria=" 
						+  ((int)Store_OrderStatusCriteria.Pending_PayDate).ToString(), false);
					break;
				}
				case 2 :
				{
					Response.Redirect("Store_CustomerList.aspx?reportcriteria=" 
						+ ((int)Store_CustomerListCriteria.Active_ActiveDate).ToString(), false);	
					break;
				}
				case 4 :
				{
					Response.Redirect("Store_OrderStatus.aspx?reportcriteria=" 
						+ ((int)Store_OrderStatusCriteria.All_AccountStatus).ToString(), false);	
					break;
				}
				case 6 :
				{
					Response.Redirect("Store_OrderStatus.aspx?reportcriteria=" 
						+ ((int)Store_OrderStatusCriteria.Disconnected_PayDate).ToString(), false);	
					break;
				}
				case 1 :
				{
					Response.Redirect("Store_ActCustListDueDate.aspx?reportcriteria=" 
						+  ((int)Store_OrderStatusCriteria.Active_DueDate).ToString(), false);	
					break;
				}
				case 7 :
				{
					Response.Redirect("Store_CustomerList.aspx?reportcriteria=" 
						+ ((int)Store_CustomerListCriteria.Disconnected_DueDate).ToString(), false);		
					break;
				}
				case 9 :
				{
					Response.Redirect("PendOrderPymtInfoRpt.aspx", false);				
					break;
				}
				default :
					throw new ArgumentException("DropDownlist Selection is incorrect or missing");
					break;
			}		
		}

		private void btnCommissions_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			int i = Dropdownlist3.SelectedIndex;

			switch (i)
			{
				case 0 :
					return;		

				case 1 :
					Response.Redirect("Store_Commission.aspx?reportcriteria=" + 
						((int)Store_CommissionCriteria.Local_Commission).ToString(), false);
					break;

				case 2 :
					Response.Redirect("Store_WirelessCommission.aspx?reportcriteria=" + 
						((int)Store_CommissionCriteria.Cell_Commission).ToString(), false);
					break;

				default :
					throw new ArgumentException("DropDownlist Selection is incorrect or missing");
					break;
			}		
		}

		private void btnCertification_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			int i = Dropdownlist4.SelectedIndex;

			switch (i)
			{
				case 0 :
					return;		

				case 1 :
					Response.Redirect("CertTrainingReport.aspx", false);
					break;

				default :
					throw new ArgumentException("DropDownlist Selection is incorrect or missing");
					break;
			}		
		}
		void imgCalendar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("Store_EndOfDayReport.aspx?showCal=true", false);
		}
		void btnEndOfDayRpt_Click(object sender, System.EventArgs e)
		{
			lblErrMsg.Text = string.Empty;
			lblErrMsg.Visible = false;
			try
			{
				GetEndDate();
			}
			catch 
			{
				lblErrMsg.Text = "Invalid date. Please correct";
				lblErrMsg.Visible = true;
				return;
			}
			try
			{
				Response.Redirect("Store_EndOfDayReport.aspx?reportDate=" + GetEndDate().ToShortDateString(), false);		
			}
			catch (Exception ex)
			{

			}
		}
		private DateTime GetEndDate()
		{
			DateTime date = (new DateTime(int.Parse(ddlPayDateYear.SelectedValue), 
				int.Parse(ddlPayDateMonth.SelectedValue), int.Parse(ddlPayDateDay.SelectedValue)));

			return date;
		}
		void btnEndOfDayRpt_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			lblErrMsg.Text = string.Empty;
			lblErrMsg.Visible = false;
			try
			{
				GetEndDate();
			}
			catch 
			{
				lblErrMsg.Text = "Invalid date. Please correct";
				lblErrMsg.Visible = true;
				return;
			}
			try
			{
				Response.Redirect("Store_EndOfDayReport.aspx?reportDate=" + GetEndDate().ToShortDateString(), false);		
			}
			catch (Exception ex)
			{

			}		
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

			ddlPayDateMonth.SelectedValue = DateTime.Now.Month.ToString();
			ddlPayDateDay.SelectedValue = DateTime.Now.Day.ToString();
			ddlPayDateYear.SelectedValue = DateTime.Now.Year.ToString();
		}
		# endregion		
	}
}