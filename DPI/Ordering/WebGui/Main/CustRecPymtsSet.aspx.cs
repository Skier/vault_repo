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

	public class CustRecPymtsSet : BasePage
	{ 	
	
		#region Web Form Designer generated code
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.PlaceHolder phOrderSummary;
		protected System.Web.UI.WebControls.TextBox txtLDsrvc;
		protected System.Web.UI.WebControls.ImageButton btnGotoMain;
		protected int selectedPage;
		protected System.Web.UI.WebControls.Label lblAccNumber;
		protected System.Web.UI.WebControls.TextBox txtBFirstName;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.TextBox txtBLastName;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.TextBox txtBAddress1;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator3;
		protected System.Web.UI.WebControls.TextBox txtBAddress2;
		protected System.Web.UI.WebControls.TextBox txtBCIty;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator4;
		protected System.Web.UI.WebControls.DropDownList ddlBState;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator5;
		protected System.Web.UI.WebControls.TextBox txtBZip;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator6;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator1;
		protected System.Web.UI.WebControls.TextBox txtNPA;
		protected System.Web.UI.WebControls.TextBox txtNxx;
		protected System.Web.UI.WebControls.TextBox txtLastFour;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator7;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator8;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator9;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator2;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator3;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator4;
		protected System.Web.UI.WebControls.TextBox txtEmailAddress;
		protected System.Web.UI.WebControls.RegularExpressionValidator Regularexpressionvalidator6;
		protected System.Web.UI.WebControls.DropDownList ddlStatus;
		protected System.Web.UI.WebControls.DropDownList ddlAcctType;
		protected System.Web.UI.WebControls.TextBox txtCardNumber;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator10;
		protected System.Web.UI.WebControls.TextBox txtBankRouteNum;
		protected System.Web.UI.WebControls.DropDownList ddlDLState;
		protected System.Web.UI.WebControls.TextBox txtDLNum;
		protected System.Web.UI.WebControls.DropDownList ddlExpMonth;
		protected System.Web.UI.WebControls.DropDownList ddlExpYear;
		protected System.Web.UI.WebControls.TextBox txtCVV2;
		protected System.Web.UI.WebControls.RegularExpressionValidator Regularexpressionvalidator5;
		protected System.Web.UI.WebControls.DropDownList ddlPriority;
		protected System.Web.UI.WebControls.ImageButton btnSubmit;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected string mode = "";
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
			CustomInit();
		}
		
		private void InitializeComponent()
		{  
			this.btnSubmit.Click += new System.Web.UI.ImageClickEventHandler(this.btnSubmit_Click);
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
				SetAttrs();				
				BindDates();				
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
				//SetDafaultDate();
				ShowCustInfo();
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
				Response.Redirect(wipper.Wip.Prev(), false);				
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void btnSubmit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				if (!ValidateIt())
					return;

				SetCustomerInfo();
				SetAcctActivityLog();
				CustSvc.PreSave(wipper.IMap);
				wipper.Wip["CustomerROPs"] = CustSvc.GetCustROPByAccount(wipper.IMap, ((IAcctInfo)wipper.Wip["AcctInfo"]).AccNumber);
				Response.Redirect(wipper.Wip.Prev(), false);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}


		#endregion

		#region Implementation
		bool ValidateIt()
		{
			if (!IsValid)
				return false;

			string msg = CheckNumOfEnabledAccts();// = CheckPriority(); At this time we are not checking priority but only one active account			
			msg += CheckBRoute();
			msg += CheckExpireDate();
			msg += CheckEmail();
			msg += CheckDLStateNum();
			
			if (msg.Trim().Length > 1)
			{
				ShowErr(msg);
				return false;
			}

			return true;
		}
		string CheckNumOfEnabledAccts()
		{
			if (ddlStatus.SelectedValue == "0")
				return "";

			for (int i = 0; i < ((ICustomerRecurringPayment[])wipper.Wip["CustomerROPs"]).Length; i++)
			{
				if (((ICustomerRecurringPayment[])wipper.Wip["CustomerROPs"])[i].Active)
					return " Only one active Recurring Account allowed";
			}

			return "";
		}
		//At this time only one priority
		string CheckPriority()
		{		
			for (int i = 0; i < ((ICustomerRecurringPayment[])wipper.Wip["CustomerROPs"]).Length; i++)
			{
				if ((string)wipper.Wip["OperationMode"] == "edit")
					if ((int)wipper.Wip["RecurringId"] == ((ICustomerRecurringPayment[])wipper.Wip["CustomerROPs"])[i].Id)
						continue;

				if (((ICustomerRecurringPayment[])wipper.Wip["CustomerROPs"])[i].Priority == int.Parse(ddlPriority.SelectedValue))
					return "Customer can not have same priority for two different account. Please change priority.";
			}
			
			return "";
		}
		string CheckDLStateNum()
		{
			if ((PaymentType)int.Parse(ddlAcctType.SelectedValue) == PaymentType.Credit)
				return "";
 
			if (ddlDLState.SelectedValue == "")
				return " Please select a State.";

			if (txtDLNum.Text.Length < 1)
				return " Please select driving number.";

			return "";
		}
		string CheckBRoute()
		{
			
			if ((PaymentType)int.Parse(ddlAcctType.SelectedValue) == PaymentType.Check)
				if (txtBankRouteNum.Text.Length < 1)
					return " Must enter Bank Route Number.";

			return "";
		}
		string CheckExpireDate()
		{
			if ((PaymentType)int.Parse(ddlAcctType.SelectedValue) == PaymentType.Check)
				return "";

			if (ddlExpMonth.SelectedValue == "")
				return " Please Select Expiration Month.";

			if (ddlExpYear.SelectedValue == "")
				return " Please Select Expiration Year.";

			if (new DateTime(int.Parse(ddlExpYear.SelectedValue), int.Parse(ddlExpMonth.SelectedValue) + 1, 1) 
				< DateTime.Now )
				return " Card expired.";

			return "";
		}
		string CheckEmail()
		{
			if ((PaymentType)int.Parse(ddlAcctType.SelectedValue) == PaymentType.Check)
				if (txtEmailAddress.Text.Length < 5)
					return " Please enter Email address.";

			return "";
		}
		void SetAttrs()
		{
			lblErrMsg.Visible = false;
			
			btnPrevious.Visible = true;
			if (((ICustomerRecurringPayment[])wipper.Wip["CustomerROPs"]).Length == 0)
				btnPrevious.Visible = false;
			
			MultiClickBlocker.Block(this, btnPrevious);
			MultiClickBlocker.Block(this, btnGotoMain);
			MultiClickBlocker.Block(this, btnSubmit);
			lblAccNumber.Text   = ((IAcctInfo)wipper.Wip["AcctInfo"]).AccNumber.ToString();
			ddlAcctType.Attributes.Add("onChange", "SetControls();");			
		}
		void BindDates()
		{
			ddlExpMonth.DataSource = DropDownListDate.GetCCMonths(true);
			ddlExpMonth.DataTextField = "DDLText";
			ddlExpMonth.DataValueField  = "DDLValue";
			ddlExpMonth.DataBind();

			ddlExpYear.DataSource = DropDownListDate.GetYears(DateTime.Now.Year, 12, true);
			ddlExpYear.DataTextField = "DDLText";
			ddlExpYear.DataValueField  = "DDLValue";
			ddlExpYear.DataBind();			
		}
		void SetDafaultDate()
		{
			ddlExpMonth.SelectedValue = DateTime.Now.ToString("MM");
			ddlExpYear.SelectedValue = DateTime.Now.Year.ToString();
		}
		void SetCustomerInfo()
		{
			ICustomerRecurringPayment customer = GetCustomer();
			
			
			
			customer.UserId = wipper.Wip.ClerkId;
			customer.AccNumber = int.Parse(lblAccNumber.Text);			
			customer.BillingFirstName = txtBFirstName.Text;
			customer.BillingLastName = txtBLastName.Text;			
			customer.BillingAddress = txtBAddress1.Text + " " + txtBAddress2.Text;			
			customer.BillingCity = txtBCIty.Text;
			customer.BillingState = ddlBState.SelectedValue;
			customer.BillingZip = txtBZip.Text; 
			customer.PhNumber = txtNPA.Text + txtNxx.Text + txtLastFour.Text;
			customer.EmailAddress = txtEmailAddress.Text;
			customer.Active = ddlStatus.SelectedValue == "1" ? true : false;
			
			if ((string)wipper.Wip["OperationMode"] == "add")
			{
				customer.AccountTypeId = int.Parse(ddlAcctType.SelectedValue);
				customer.BAccNumber = txtCardNumber.Text;
				customer.BRouteNumber = txtBankRouteNum.Text;
				customer.CVV2 = txtCVV2.Text;
			}
			customer.DLStateNumber = ddlDLState.SelectedValue + txtDLNum.Text;
			customer.ExpirationMonthYear = ddlExpMonth.SelectedValue + ddlExpYear.SelectedValue;			
			customer.Priority = int.Parse(ddlPriority.SelectedValue);
		}		
		void SetAcctActivityLog()
		{
			if ((string)wipper.Wip["OperationMode"] == "edit")
				OperSvc.SetAcctActivityLog(wipper.IMap, ((IAcctInfo)wipper.Wip["AcctInfo"]).AccNumber, "Edited Recurring Payments");
			else
				OperSvc.SetAcctActivityLog(wipper.IMap, ((IAcctInfo)wipper.Wip["AcctInfo"]).AccNumber, "Added Recurring Payments");
		}
		void InitializeCustInfo()
		{		
			if (wipper.Wip["CustInfoExt"] == null)
				return;
	
			if (((ICustInfoExt)wipper.Wip["CustInfoExt"]).CustInfo != null)
			{
				txtBFirstName.Text  = ((ICustInfoExt)wipper.Wip["CustInfoExt"]).CustInfo.FirstName;
				txtBLastName.Text  = ((ICustInfoExt)wipper.Wip["CustInfoExt"]).CustInfo.LastName;
				txtEmailAddress.Text = ((ICustInfoExt)wipper.Wip["CustInfoExt"]).CustInfo.Email;
			}
			if (((ICustInfoExt)wipper.Wip["CustInfoExt"]).CustInfo.PhNumber != null)
			{
				txtNPA.Text = ((ICustInfoExt)wipper.Wip["CustInfoExt"]).CustInfo.PhNumber.Substring(0, 3);
				txtNxx.Text = ((ICustInfoExt)wipper.Wip["CustInfoExt"]).CustInfo.PhNumber.Substring(3, 3);
				txtLastFour.Text = ((ICustInfoExt)wipper.Wip["CustInfoExt"]).CustInfo.PhNumber.Substring(6, 4);
			}
	
			if (((ICustInfoExt)wipper.Wip["CustInfoExt"]).MailAddr != null)
			{
				txtBAddress1.Text = ((ICustInfoExt)wipper.Wip["CustInfoExt"]).MailAddr.FormattedStreetAddress;
				txtBCIty.Text = ((ICustInfoExt)wipper.Wip["CustInfoExt"]).MailAddr.City;
				txtBZip.Text = ((ICustInfoExt)wipper.Wip["CustInfoExt"]).MailAddr.Zipcode;
				ddlBState.SelectedValue = ((ICustInfoExt)wipper.Wip["CustInfoExt"]).MailAddr.State;
			}
		}
		void InitializeCustInfo(ICustomerRecurringPayment customer)
		{		
			if (customer == null)
			{
				InitializeCustInfo();
				return;
			}

			txtBFirstName.Text = customer.BillingFirstName;
			txtBLastName.Text = customer.BillingLastName;
			txtBAddress1.Text = customer.BillingAddress;
			txtBCIty.Text = customer.BillingCity;
			ddlBState.SelectedValue = customer.BillingState;
			txtBZip.Text = customer.BillingZip;
			txtNPA.Text = customer.PhNumber.Substring(0,3);
			txtNxx.Text = customer.PhNumber.Substring(3,3);
			txtLastFour.Text = customer.PhNumber.Substring(6,4);
			txtEmailAddress.Text = customer.EmailAddress;
			ddlStatus.SelectedValue = (customer.Active == true) ? "1": "0";
			ddlAcctType.SelectedValue = customer.AccountTypeId.ToString();
			
			if (customer.BAccNumber.Length > 4)
				txtCardNumber.Text = customer.BAccNumber.Substring(customer.BAccNumber.Length - 4);
			
			if (customer.BRouteNumber != null)
				if (customer.BRouteNumber.Length > 4)
					txtBankRouteNum.Text = customer.BRouteNumber.Substring(customer.BRouteNumber.Length - 4);
			
			if (customer.DLStateNumber != null)
				if (customer.DLStateNumber.Length > 2)
				{
					ddlDLState.SelectedValue = customer.DLStateNumber.Substring(0,2);			
					txtDLNum.Text = customer.DLStateNumber.Substring(2);
				}
			
			if ((PaymentType)customer.AccountTypeId == PaymentType.Check)
				ddlExpMonth.Enabled = ddlExpYear.Enabled = false; 

			if ((customer.ExpirationMonthYear != null) && (customer.ExpirationMonthYear.Trim().Length == 6))
			{
				ddlExpMonth.SelectedValue = customer.ExpirationMonthYear.Substring(0,2);
				ddlExpYear.SelectedValue = customer.ExpirationMonthYear.Substring(customer.ExpirationMonthYear.Length - 4);
			}
			txtCVV2.Text = customer.CVV2;
			ddlPriority.SelectedValue = customer.Priority.ToString();
		}
		void ShowCustInfo()
		{
			if ((string)wipper.Wip["OperationMode"] == "edit")
			{
				InitializeCustInfo(GetCustomer((int)wipper.Wip["RecurringId"]));
				DisableAttrs(true);
				return;
			}
			
			InitializeCustInfo();
		}
		ICustomerRecurringPayment GetCustomer(int recurringId)
		{
			if (recurringId == 0)
				return null;

			for (int i = 0; i < ((ICustomerRecurringPayment[])wipper.Wip["CustomerROPs"]).Length; i++)
				if (((ICustomerRecurringPayment[])wipper.Wip["CustomerROPs"])[i].Id == recurringId)
					return ((ICustomerRecurringPayment[])wipper.Wip["CustomerROPs"])[i];
			
			return null;
		}
		ICustomerRecurringPayment GetCustomer()
		{
			if ((string)wipper.Wip["OperationMode"] == "add")
				return CustSvc.GetCustROP(wipper.IMap);

			return GetCustomer((int)wipper.Wip["RecurringId"]);
		}
		void DisableAttrs(bool isDisableed)
		{
			ddlAcctType.Enabled = txtCardNumber.Enabled 
			= txtBankRouteNum.Enabled = txtCVV2.Enabled = !isDisableed;
		}
		void ShowErr(string msg)
		{
			lblErrMsg.Text = msg;
			lblErrMsg.Visible = true;
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