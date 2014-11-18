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

namespace DPI.Ordering
{

	public class CI_AccountSumm : BasePage
	{ 	
	
		#region Web Form Designer generated code
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label lblAmountDue;
		protected System.Web.UI.WebControls.Label lblAddress;
		protected System.Web.UI.WebControls.Label lblPhoneNumber;
		protected System.Web.UI.WebControls.Label lblDueDate;
		protected System.Web.UI.WebControls.Label lblLastDay;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.Label lblCurrCharges;
		protected System.Web.UI.WebControls.PlaceHolder phOrderSummary;
		protected System.Web.UI.WebControls.TextBox txtLDsrvc;
		protected System.Web.UI.WebControls.Label lblAccNumber;
		protected System.Web.UI.WebControls.ImageButton imgPastReminderNotice;
		protected System.Web.UI.WebControls.Label lblCityStateZip;
		protected System.Web.UI.WebControls.Label lblBalForward;
		protected System.Web.UI.WebControls.ImageButton btnGotoMain;
		protected System.Web.UI.WebControls.Label lblStatus;
		protected System.Web.UI.WebControls.Label lblRecurringPymts;
		protected System.Web.UI.WebControls.LinkButton lbRecurringSetup;
		protected System.Web.UI.WebControls.Label lblCustomerName;
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent()
		{  
			this.lbRecurringSetup.Click += new System.EventHandler(this.lbRecurringSetup_Click);
			this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrevious_Click);
			this.btnGotoMain.Click += new System.Web.UI.ImageClickEventHandler(this.btnGotoMain_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		
		#region Event Handlers
		void CustomInit()
		{
			try
			{
				btnPrevious.Visible = wipper.Wip.HasPrev;
				MultiClickBlocker.Block(this, btnPrevious);
				MultiClickBlocker.Block(this, btnGotoMain);
				MultiClickBlocker.Block(this, imgPastReminderNotice);
			}
			catch (Exception ex)
			{	
				ErrorHandler(ex);
			}	
		}	
		void Page_Load(object sender, System.EventArgs e)
		{
			if(IsPostBack)
				return;

			try 
			{	
				lblAmountDue.Text = 0m.ToString("C");
				
				CheckGetCustInfoExt();
				SetupAcctInfo();
				SetupCustInfo();
				SetupBillFilename();
				SetRecurringPymts();
			}
			catch (Exception ex)
			{	
				ErrorHandler(ex);
			}			
		}
		void btnGotoMain_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				Session["IMap"] = null;
				Response.Redirect("MenuScreen.aspx", false);
			}
			catch(Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void btnPrevious_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				wipper.Wip["CustInfoExt"] = null;
				wipper.Wip["AcctInfo"]    = null;				
				Response.Redirect(wipper.Wip.Prev(), false);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void lbRecurringSetup_Click(object sender, System.EventArgs e)
		{
			Server.Transfer(wipper.Wip.Next(), false);
		}
	#endregion

		#region Implementation
	
		void SetRecurringPymts()
		{
			if (!HttpContext.Current.User.IsInRole("Recurring")) //change later to enum
				return;

			wipper.Wip["CustomerROPs"] = CustSvc.GetCustROPByAccount(wipper.IMap, ((IAcctInfo)wipper.Wip["AcctInfo"]).AccNumber);
			
			lblRecurringPymts.Visible = lbRecurringSetup.Visible = true;
			
			lblRecurringPymts.Text = "Recurring Payments: Disabled";
			lbRecurringSetup.Text = "(Setup)";
			//Check if customer's recurring paymens is already active or not			
			if (IsRecurringActive())
			{
				lblRecurringPymts.Text = "Recurring Payments: Enabled";
				lbRecurringSetup.Text = "(View)";
			}

		}
		bool IsRecurringActive()
		{
			if (((ICustomerRecurringPayment[])wipper.Wip["CustomerROPs"]).Length == 0)
				return false;

			return ((ICustomerRecurringPayment[])wipper.Wip["CustomerROPs"])[0].Active;
		}
		void SetupCustInfo()
		{		
			if (wipper.Wip["CustInfoExt"] == null)
				return;
	
			if (((ICustInfoExt)wipper.Wip["CustInfoExt"]).CustInfo != null)
				lblCustomerName.Text  = ((ICustInfoExt)wipper.Wip["CustInfoExt"]).CustInfo.FormattedName;
	
			if (((ICustInfoExt)wipper.Wip["CustInfoExt"]).ServAddr != null)
			{
				lblAddress.Text   = ((ICustInfoExt)wipper.Wip["CustInfoExt"]).ServAddr.FormattedStreetAddress;
				lblCityStateZip.Text = ((ICustInfoExt)wipper.Wip["CustInfoExt"]).ServAddr.FormattedCityStateZip;
			}
		}
		void SetupAcctInfo()
		{
			if (wipper.Wip["AcctInfo"] == null)
				return;

			lblAccNumber.Text   = ((IAcctInfo)wipper.Wip["AcctInfo"]).AccNumber.ToString();
			lblPhoneNumber.Text = ((IAcctInfo)wipper.Wip["AcctInfo"]).PhNumFormated;
			lblStatus.Text		= ((IAcctInfo)wipper.Wip["AcctInfo"]).Status;
			lblBalForward.Text  = ((IAcctInfo)wipper.Wip["AcctInfo"]).BalForward.ToString("C");
			lblCurrCharges.Text = ((IAcctInfo)wipper.Wip["AcctInfo"]).CurrCharges.ToString("C");
			
			lblAmountDue.Text = ((IAcctInfo)wipper.Wip["AcctInfo"]).DueAmt.ToString("C");

			lblDueDate.Text = lblLastDay.Text = "";

			if (((IAcctInfo)wipper.Wip["AcctInfo"]).DueDate != DateTime.MinValue)
				lblDueDate.Text = ((IAcctInfo)wipper.Wip["AcctInfo"]).DueDate.ToShortDateString();
					
			if (((IAcctInfo)wipper.Wip["AcctInfo"]).DiscoDate > DateTime.MinValue)
				lblLastDay.Text = ((IAcctInfo)wipper.Wip["AcctInfo"]).DiscoDate.ToShortDateString();
		}
		void CheckGetCustInfoExt()
		{
			if (wipper.Wip["CustInfoExt"] != null)
				return;

			wipper.Wip["CustInfoExt"] 
				= CustSvc.GetCustInfoExt(wipper.IMap, ((IAcctInfo)wipper.Wip["AcctInfo"]).AccNumber);	
		}
		void SetupBillFilename()
		{
			string filename = GetBillFilename();
			
			if (filename == null)
			{
				imgPastReminderNotice.Attributes.Add("onClick", "window.alert('Previous bill is not available.');");
				return;
			}

			ViewState["LastBillFilename"] = Const.VIRTUAL_DIR_BILLVIEW +  filename;

			this.imgPastReminderNotice.Attributes.Add("onClick", "window.open('" 
				+ (string)ViewState["LastBillFilename"] 
				+ "',null,'height= 550, width=700 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");

		}
		string GetBillFilename()
		{	
			if (wipper.Wip["AcctInfo"] == null)
				return null;

			IPastReminderNotice notice 
				= CustSvc.GetReminderNotice(wipper.IMap, ((IAcctInfo)wipper.Wip["AcctInfo"]).AccNumber);
			
			if (notice == null)
				return null;

			if (notice.Filename == null)
				return null;
			
			if (notice.Filename.Trim().Length == 0)
				return null;

			return notice.Filename; 
		}

		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr( wipper.IMap, ex, Session["User"], this.ToString());
			lblErrMsg.Text = Const.GENERAL_ERROR;
			lblErrMsg.Visible = true;
		}
	#endregion		
	}
}