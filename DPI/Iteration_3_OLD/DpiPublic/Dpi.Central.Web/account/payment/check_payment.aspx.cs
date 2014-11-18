using System;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

using Dpi.Central.Web.Controls;
using TextBox=Dpi.Central.Web.Controls.TextBox;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account.Payment
{
    public class CheckPaymentPage : BasePaymentPage
    {
		private const string DUPLICATE_PAYMENT_FOUND = "Payment of {0} dollars on this check was already received today.";

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
		protected Dpi.Central.Web.Controls.ControlLabel lblAcctN;
		protected Dpi.Central.Web.Controls.ControlLabel lblPhoneN;
		protected Dpi.Central.Web.Controls.ControlLabel lblFName;
		protected Dpi.Central.Web.Controls.TextBox txtFName;
		protected Dpi.Central.Web.Controls.ControlLabel lblLName;
		protected Dpi.Central.Web.Controls.TextBox txtLName;
		protected Dpi.Central.Web.Controls.ControlLabel lblAddr;
		protected Dpi.Central.Web.Controls.TextBox txtAddr;
		protected Dpi.Central.Web.Controls.ControlLabel lblCity;
		protected Dpi.Central.Web.Controls.TextBox txtCity;
		protected System.Web.UI.WebControls.DropDownList lstState;
		protected Dpi.Central.Web.Controls.TextBox txtZIP;
		protected Dpi.Central.Web.Controls.ControlLabel lblEmail;
		protected Dpi.Central.Web.Controls.TextBox txtEmail;
		protected Dpi.Central.Web.Controls.ControlLabel lblAmt;
		protected System.Web.UI.WebControls.HyperLink lnkReturn;
		protected System.Web.UI.WebControls.ImageButton btnPay;
        protected TextBox txtRNum;



        protected override void OnInit(EventArgs e)
        {
            BindEvents();
            InitializeComponent();
            base.OnInit(e);
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

				txtAddr.Text = address.Street;
				txtCity.Text = address.City;
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
			
			
			PaymentResult result = PaymentSvc.MakePaymentByCheck(Map, GetAccountNumber(), bankCheck, amount, "public");

			switch (result.Code) 
			{
				case PaymentResultCode.Completed:
					SendNotification(amount, result.Payment.Id);

					Session[PAYMENT_AMOUNT_KEY] = amount;
					//Session[PAYMENT_CHECK_KEY] = bankCheck.BankAccountNumber;
					//Session[PAYMENT_TYPE_KEY] = BaseAccountPage.PaymentType.Check;
					
					Context.Items["payment"] = result.Payment;
					Server.Transfer(SiteMap.PAYMENT_RECIEPT_URL);
				
					//Response.Redirect(SiteMap.PAYMENT_RECIEPT_URL);
					break;
				case PaymentResultCode.Rejected: 
					ShowErrorMessage(string.Format(REJECTED_PAYMENT, result.Description));
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

		private void SendNotification(decimal amount, int paymentId)
		{
			string acctEmail = base.Cust.CustInfo.Email;
			string billEmail = txtEmail.Text;

			if (acctEmail != billEmail) 
			{
				EmailSender.SendCheckPaymentNotification(acctEmail, base.Acct.FirstName, amount, paymentId);
			}
            
			EmailSender.SendCheckPaymentNotification(billEmail, txtFName.Text, amount, paymentId);
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