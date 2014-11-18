using System;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

using Dpi.Central.Web.Account.Controls;
using TextBox=Dpi.Central.Web.Account.Controls.TextBox;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account.Payment
{
    public class CheckPaymentPage : BasePaymentPage
    {
        protected DropDownList expMonth;
        protected DropDownList expYear;
        protected DropDownList lstDrvState;
        protected TextBox txtDrvNum;
        protected ControlLabel lblDrvNum;
        protected ControlLabel lblDrvState;
        protected ControlLabel lblRNum;
        protected ControlLabel lblBANum;
        protected TextBox txtBANum;
		protected System.Web.UI.WebControls.CustomValidator vldCustErrorMsg;
		protected System.Web.UI.WebControls.ValidationSummary vldSummary;
		protected Dpi.Central.Web.Account.Controls.ControlLabel lblAcctN;
		protected Dpi.Central.Web.Account.Controls.ControlLabel lblPhoneN;
		protected Dpi.Central.Web.Account.Controls.ControlLabel lblFName;
		protected Dpi.Central.Web.Account.Controls.TextBox txtFName;
		protected Dpi.Central.Web.Account.Controls.ControlLabel lblLName;
		protected Dpi.Central.Web.Account.Controls.TextBox txtLName;
		protected Dpi.Central.Web.Account.Controls.ControlLabel lblAddr;
		protected Dpi.Central.Web.Account.Controls.TextBox txtAddr;
		protected Dpi.Central.Web.Account.Controls.ControlLabel lblCity;
		protected Dpi.Central.Web.Account.Controls.TextBox txtCity;
		protected System.Web.UI.WebControls.DropDownList lstState;
		protected Dpi.Central.Web.Account.Controls.TextBox txtZIP;
		protected Dpi.Central.Web.Account.Controls.ControlLabel lblEmail;
		protected Dpi.Central.Web.Account.Controls.TextBox txtEmail;
		protected Dpi.Central.Web.Account.Controls.ControlLabel lblAmt;
		protected System.Web.UI.WebControls.HyperLink lnkReturn;
		protected System.Web.UI.WebControls.ImageButton btnPay;
        protected TextBox txtRNum;
		protected System.Web.UI.WebControls.RequiredFieldValidator vldRfDrvState;
		protected RequiredFieldValidator vldRfState;


        protected override void OnInit(EventArgs e)
        {
            BindEvents();
            InitializeComponent();
            base.OnInit(e);

            System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
            sbValid.Append("if (typeof(Page_ClientValidate) == 'function') { ");
            sbValid.Append("if (Page_ClientValidate() == false) { return false; }} ");
            sbValid.Append(this.Page.GetPostBackEventReference(this.btnPay));
            sbValid.Append(";");
            sbValid.Append("this.disabled = true;");
            btnPay.Attributes.Add("onclick", sbValid.ToString());
        }

        protected void LoadPostData()
        {
            //base.LoadPostData();
            lstDrvState.SelectedValue = Request.Form[lstDrvState.UniqueID];
        }


        private void BindEvents()
        {
            //vldCustErrorMsg.ServerValidate += new ServerValidateEventHandler(vldCustErrorMsg_ServerValidate);
        }

        private void InitializeComponent()
        {
            this.btnPay.Click += new System.Web.UI.ImageClickEventHandler(this.btnPay_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        private void vldCustErrorMsg_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (lstDrvState.SelectedIndex < 1) {
                //vldCustErrorMsg.ErrorMessage = "Please select Driver License State";
                args.IsValid = false;
            }
        }

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				lblAcctN.Text = Acct.AccNumber.ToString();
				lblPhoneN.Text = Acct.PhNumFormated;
				txtFName.Text = Acct.FirstName;
				txtLName.Text = Acct.LastName;
				
				IAddr address;

				if (base.Cust.MailAddr != null) 
				{
					address = base.Cust.MailAddr;
				} 
				else if (base.Cust.ServAddr != null) 
				{
					address = base.Cust.ServAddr;
				} 
				else 
				{
					throw new ApplicationException(string.Format(ADDRESS_INFO_IS_MISSED, base.Acct.AccNumber));
				}

				txtAddr.Text = ConvertToString(address);
				txtCity.Text = address.City.Trim();
				lstState.SelectedValue = address.State;
				txtZIP.Text = address.Zipcode;
				txtEmail.Text = base.Cust.CustInfo.Email;

				txtDrvNum.Text = String.Empty;
				lstDrvState.SelectedIndex = 0;


				lblAmt.Text = GetPaymentAmount().ToString(MONEY_VALUE_FORMAT);
			}
		}

		private void btnPay_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (!IsValid) 
			{
				return;
			}

			try 
			{
				MakePayment();
			} 
			catch (CreditCardValidationException ex) 
			{
				ShowErrorMessage(ex.Message);
			} 
			catch (Exception ex) 
			{
				ShowErrorMessage(ex);
			}
		}

		private void MakePayment()
		{
			decimal amount = decimal.Parse(lblAmt.Text);
			IAcctInfo acct = base.Acct;

			BankCheck bankCheck = new BankCheck(
				GetDigits(txtRNum.Text),
				GetDigits(txtBANum.Text),
				GetDigits(txtDrvNum.Text),
				lstDrvState.SelectedValue,
				txtFName.Text.Trim(),
				txtLName.Text.Trim(),
				txtZIP.Text.Trim(),
				lstState.SelectedValue,
				txtCity.Text.Trim(),
				txtAddr.Text.Trim(),
				Acct.PhNumber.Trim(),
				txtEmail.Text.Trim());
			
			StringCollection errors = bankCheck.Validate();
			
			if(errors.Count > 0 )
			{
				ShowErrorMessage(errors[0]);
				return;
			}
			
			
			PaymentResult result = PaymentSvc.MakePaymentByCheck(
				Map, GetAccountNumber(), 
				bankCheck, amount,acct.DueAmt, "public", 
				"DPI-Web",  "Web-User");

			switch (result.Code) 
			{
				case PaymentResultCode.Completed:
					SendNotification(amount, result.Payment.ConfNum);

					Session[PAYMENT_AMOUNT_KEY] = amount;
					Session["bankCheck"] = bankCheck;
					Session["payment"] = result.Payment;
                    Session["email"] = txtEmail.Text.Trim();
				
					Response.Redirect(SiteMap.PAYMENT_RECIEPT_URL);
					break;
				case PaymentResultCode.Rejected: 
			        string rejectedDetails = result.Description.Trim();
			        if (rejectedDetails.EndsWith("."))
			            rejectedDetails = rejectedDetails.Remove(rejectedDetails.Length - 1, 1);
			        
					ShowErrorMessage(string.Format(REJECTED_PAYMENT, rejectedDetails));
					break;
				case PaymentResultCode.UnableToComplete: 
					ShowErrorMessage(UNABLE_TO_COMPLETE_PAYMENT);
					break;
				case PaymentResultCode.NeedVerification: 
					ShowErrorMessage(string.Format(NEED_VERIFICATION, amount.ToString("C")));
					break;
				default:
					throw new ApplicationException("Payment result code is unknown: " + result.Code + ".");
			}
		}

		private void SendNotification(decimal amount, int confNum)
		{
			string acctEmail = base.Cust.CustInfo.Email;
            EmailSender.SendCheckPaymentNotification(acctEmail, CustInfo.CapitalizeName(base.Acct.FirstName.Trim()) + " " + CustInfo.CapitalizeName(base.Acct.LastName.Trim()), amount, confNum);
            if (txtEmail.Text.Trim() != acctEmail) {
                EmailSender.SendCreditCardPaymentNotification(txtEmail.Text.Trim(), CustInfo.CapitalizeName(txtFName.Text.Trim()) + " " + CustInfo.CapitalizeName(txtLName.Text.Trim()), amount, confNum);
            }
		}

		private void ShowErrorMessage(string message)
		{
			vldCustErrorMsg.ErrorMessage = message;
			vldCustErrorMsg.IsValid = false;
		}

		private void ShowErrorMessage(Exception ex)
		{
#if DEBUG
			ShowErrorMessage(ex.Message);
#else
            ShowErrorMessage(INTERNAL_ERROR);
#endif
		}
    }
}