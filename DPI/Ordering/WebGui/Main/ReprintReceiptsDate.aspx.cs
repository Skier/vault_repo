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

using DPI.Services;
using DPI.Interfaces;
using DPI.Ordering;
using DPI.ClientComp;

namespace DPI.Ordering.Main
{
	public class ReprintReceiptsDate : BasePage
	{
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected System.Web.UI.WebControls.RadioButton RadioButton1;
		protected System.Web.UI.WebControls.RadioButton RadioButton2;
		protected System.Web.UI.WebControls.RadioButton RadioButton3;
		protected System.Web.UI.WebControls.RadioButton RadioButton4;
		protected System.Web.UI.WebControls.RadioButton RadioButton5;
		protected System.Web.UI.WebControls.RadioButton RadioButton6;
		protected System.Web.UI.WebControls.RadioButton RadioButton7;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.ImageButton imgCalendarStartDate;
		protected System.Web.UI.WebControls.Calendar calPayDate;
		protected System.Web.UI.WebControls.DropDownList ddlPayDateMonth;
		protected System.Web.UI.WebControls.DropDownList ddlPayDateDay;
		protected System.Web.UI.WebControls.DropDownList ddlPayDateYear;
		protected System.Web.UI.WebControls.Label Label2;
		DateTime cutoff = new DateTime(2005, 4, 24);

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			CustomInit();
		}

		private void InitializeComponent()
		{    
			this.imgCalendarStartDate.Click += new System.Web.UI.ImageClickEventHandler(this.imgCalendarStartDate_Click);
			this.ImageButton1.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton1_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.calPayDate.SelectionChanged += new System.EventHandler(this.calPayDate_SelectionChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion	
	
		void CustomInit()
		{
//			IUser user = (IUser)Session["User"];
//			if(StoreSvc.GetCorporation(user.LoginStoreCode).RAC_WF)

			//imgWorkflow.ImageUrl = wipper.Wip.Workflow.ImageTag.ToString();
			//lblNoVoids.Visible = false;

//			System.Collections.IEnumerator ie = Controls.GetEnumerator();
//			object o;
//			while(ie.MoveNext())
//				o = ie.Current;
		}		

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
			btnNext.Attributes.Add("onclick","return confirmButton();");
			MultiClickBlocker.Block(this, btnNext);
			MultiClickBlocker.Block(this, ImageButton1);

			if(IsPostBack)
				return;
			  		
			ViewState["IsViewingReport"] = false;				
			BindDates();
			ddlPayDateMonth.SelectedValue = DateTime.Now.Month.ToString();
			ddlPayDateDay.SelectedValue = DateTime.Now.Day.ToString();
			ddlPayDateYear.SelectedValue = DateTime.Now.Year.ToString();
		}

		private void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{		
			try
			{
				if(fromDate() < cutoff)
				{
					lblErrMsg.Text = "Receipt prior to 4/25/2005 are unavailable. Please choose another date.";
					return;
				}
				wipper.Wip["ToDate"] = GetPayDate();
				wipper.Wip["FromDate"] = fromDate();
				Response.Redirect(wipper.Wip.Next(), false);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}

		private void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Session["IMap"] = null;
			Response.Redirect("MenuScreen.aspx", false);		
		}

		private void imgCalendarStartDate_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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

		private DateTime GetPayDate()
		{
			return (new DateTime(int.Parse(ddlPayDateYear.SelectedValue), 
				int.Parse(ddlPayDateMonth.SelectedValue), int.Parse(ddlPayDateDay.SelectedValue), 23, 59, 59 ));
		}

		private DateTime fromDate()
		{

			return (
					new DateTime(	int.Parse(ddlPayDateYear.SelectedValue), 
									int.Parse(ddlPayDateMonth.SelectedValue), 
									int.Parse(ddlPayDateDay.SelectedValue), 
									0, 0, 0
								)
					);
		}

		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr( wipper.IMap, ex, Session["User"], this.ToString());
			btnNext.Visible = false;
			lblErrMsg.Visible = true;
			lblErrMsg.Text = Const.GENERAL_ERROR;
		}
	}
}
