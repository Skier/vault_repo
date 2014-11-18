using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account.Payment
{
    public class CreditCardPaymentPage : BasePaymentPage
    {
        #region Constants

        private const string CARD_IS_EXPIRED = "Your credit card is expired. Please correct Expiration Date or use another credit card";

        #endregion

        #region Web Form Designer generated code

        protected Dpi.Central.Web.Account.Controls.ControlLabel lblCNum;
        protected Label lblCardType;
        protected Dpi.Central.Web.Account.Controls.ControlLabel lblCVNum;
        protected CustomValidator vldCustErrorMsg;
        protected ValidationSummary vldSummary;
        protected Label lblAcctN;
        protected Label lblPhoneN;
        protected Dpi.Central.Web.Account.Controls.ControlLabel	lblAddr;
        protected Dpi.Central.Web.Account.Controls.TextBox txtAddr;
        protected Dpi.Central.Web.Account.Controls.ControlLabel lblCity;
        protected Dpi.Central.Web.Account.Controls.TextBox txtCity;
        protected DropDownList lstState;
        protected Dpi.Central.Web.Account.Controls.ControlLabel lblEmail;
        protected Dpi.Central.Web.Account.Controls.TextBox txtEmail;
        protected Label lblAmt;
        protected HyperLink lnkReturn;
        protected ImageButton btnPay;
        protected DropDownList lstExpMonth;
        protected DropDownList lstExpYear;
        protected Label lblAcctNumber;
        protected Label lblPhoneNumber;
        protected Dpi.Central.Web.Account.Controls.TextBox txtFirstName;
        protected Dpi.Central.Web.Account.Controls.TextBox txtLastName;
        protected Dpi.Central.Web.Account.Controls.TextBox txtZip;
        protected Dpi.Central.Web.Account.Controls.TextBox txtCcNumber;
        protected Dpi.Central.Web.Account.Controls.TextBox txtCvNumber;
        protected Dpi.Central.Web.Account.Controls.ControlLabel lblFirstName;
        protected Dpi.Central.Web.Account.Controls.ControlLabel lblLastName;        
        protected CustomValidator vldCstExpDate;
        protected CustomValidator vldCstCreditCardType;
		protected RequiredFieldValidator vldRfState;
        protected System.Web.UI.WebControls.RadioButton rbVisa;
        protected System.Web.UI.WebControls.RadioButton rbMasterCard;
        protected System.Web.UI.WebControls.RadioButton rbAmericanExpress;
        protected System.Web.UI.WebControls.RadioButton rbDiscover;		
        protected Label lblExp;

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.vldCstExpDate.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.expDateValidator_ServerValidate);
            this.vldCstCreditCardType.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.vldCstCreditCardType_ServerValidate);
            this.btnPay.Click += new System.Web.UI.ImageClickEventHandler(this.btnPay_Click);
            this.Load += new System.EventHandler(this.Page_Load);
            this.Init += new System.EventHandler(this.Page_Init);

        }

        #endregion

        #region Event Handlers

        private void Page_Init(object sender, EventArgs e)
        {
            lstExpMonth.EnableViewState = false;
            for (int i = 1; i <= 12; i++) {
                lstExpMonth.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            lstExpYear.EnableViewState = false;
            for (int i = DateTime.Today.Year; i < DateTime.Today.Year + 20; i++) {
                lstExpYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
            sbValid.Append("if (typeof(Page_ClientValidate) == 'function') { ");
            sbValid.Append("if (Page_ClientValidate() == false) { return false; }} ");
            sbValid.Append(this.Page.GetPostBackEventReference(this.btnPay));
            sbValid.Append(";");
            sbValid.Append("this.disabled = true;");
            btnPay.Attributes.Add("onclick", sbValid.ToString());
        }

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                LoadGeneralInformation();
                LoadPaymentAmount();
            } else {
                LoadPostBackDate();
            }
        }

        private void btnPay_Click(object sender, ImageClickEventArgs e)
        {
            if (!IsValid) {
                return;
            }

            try {
                MakePayment();
            } catch (CreditCardValidationException) {
                ShowErrorMessage("Credit Card number is invalid");
            } catch (Exception ex) {
                ShowErrorMessage(ex);
            }
        }

        private void expDateValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try {
                string ms = lstExpMonth.SelectedValue;
                string ys = lstExpYear.SelectedValue;

                int m = int.Parse(ms);
                int y = int.Parse(ys);

                DateTime exp = new DateTime(y, m, 1);
                DateTime now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                if (exp < now) {
                    vldCstExpDate.ErrorMessage = CARD_IS_EXPIRED;
                    args.IsValid = false;
                }
            } catch (Exception ex) {
                args.IsValid = false;
                ShowErrorMessage(ex);
            }
        }

        private void vldCstCreditCardType_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = rbVisa.Checked || rbMasterCard.Checked || rbAmericanExpress.Checked || rbDiscover.Checked;
        }

        #endregion

        #region Implementation

        private void LoadGeneralInformation()
        {
            lblAcctNumber.Text = base.Acct.AccNumber.ToString();
            lblPhoneNumber.Text = base.Acct.PhNumFormated;
            txtLastName.Text = base.Acct.LastName;
            txtFirstName.Text = base.Acct.FirstName;

            IAddr address;
            if (base.Cust.MailAddr != null) {
                address = base.Cust.MailAddr;
            } else if (base.Cust.ServAddr != null) {
                address = base.Cust.ServAddr;
            } else {
                throw new ApplicationException(string.Format(ADDRESS_INFO_IS_MISSED, base.Acct.AccNumber));
            }

            txtAddr.Text = ConvertToString(address);
            txtCity.Text = address.City.Trim();
            lstState.SelectedValue = address.State;
            txtZip.Text = address.Zipcode;
            txtEmail.Text = base.Cust.CustInfo.Email;
        }

        private void LoadPaymentAmount()
        {
            lblAmt.Text = base.GetPaymentAmount().ToString(MONEY_VALUE_FORMAT);
        }

        private void LoadPostBackDate()
        {
            lstExpMonth.SelectedValue = Request.Form[lstExpMonth.UniqueID];
            lstExpYear.SelectedValue = Request.Form[lstExpYear.UniqueID];
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

        private void MakePayment()
        {
            decimal amount = decimal.Parse(lblAmt.Text);
			IAcctInfo acct = base.Acct;

            CreditCard card = new CreditCard(
                GetCardType(),
                GetDigits(txtCcNumber.Text.Trim()),
                GetDigits(txtCvNumber.Text.Trim()),
                int.Parse(lstExpMonth.SelectedValue),
                int.Parse(lstExpYear.SelectedValue),
                txtFirstName.Text.Trim(),
                txtLastName.Text.Trim(),
                txtZip.Text.Trim(),
                lstState.SelectedValue,
                txtCity.Text.Trim(),
                txtAddr.Text.Trim(),
                Acct.PhNumber.Trim(),
                txtEmail.Text.Trim());

            PaymentResult result = PaymentSvc.MakeCreditCardPayment(Map, GetAccountNumber(), 
					card, amount,acct.DueAmt, 
					"public",
					"DPI-Web",  "Web-User");

            switch (result.Code) {
                case PaymentResultCode.Completed:
                    SendNotification(amount, result.Payment.ConfNum);

                    Session[PAYMENT_AMOUNT_KEY] = amount;
					Session["card"] = card;
                    Session["payment"] = result.Payment;
                    Session["email"] = txtEmail.Text.Trim();

                    Response.Redirect(SiteMap.PAYMENT_RECIEPT_URL);
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

        private CreditCardType GetCardType()
        {
            if (rbVisa.Checked) {
                return CreditCardType.VISA;
            } else if (rbMasterCard.Checked) {
                return CreditCardType.MasterCard;
            } else if (rbAmericanExpress.Checked) {
                return CreditCardType.AmericanExpress;
            } else if (rbDiscover.Checked) {
                return CreditCardType.DiscoverCard;
            } else {
                throw new ApplicationException("Credit Card Type is not selected.");
            }
        }

        private void SendNotification(decimal amount, int confNum)
        {
            string acctEmail = base.Cust.CustInfo.Email;
            EmailSender.SendCreditCardPaymentNotification(acctEmail, CustInfo.CapitalizeName(base.Acct.FirstName.Trim()) + " " + CustInfo.CapitalizeName(base.Acct.LastName.Trim()), amount, confNum);
            if (txtEmail.Text.Trim() != acctEmail) {
                EmailSender.SendCreditCardPaymentNotification(txtEmail.Text.Trim(), CustInfo.CapitalizeName(txtFirstName.Text.Trim()) + " " + CustInfo.CapitalizeName(txtLastName.Text.Trim()), amount, confNum);
            }
        }

        #endregion
    }
}