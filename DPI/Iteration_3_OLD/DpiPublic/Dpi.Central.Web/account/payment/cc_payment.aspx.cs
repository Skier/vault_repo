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

        private const string CARD_IS_EXPIRED = "Your creadit card is expired. Please correct Expiration Date or use another credit card";

        #endregion

        #region Web Form Designer generated code

        protected Label lblCNum;
        protected Label lblCardType;
        protected RadioButtonList cardType;
        protected Label lblCVNum;
        protected CustomValidator vldCustErrorMsg;
        protected ValidationSummary vldSummary;
        protected Label lblAcctN;
        protected Label lblPhoneN;
        protected Label lblAddr;
        protected TextBox txtAddr;
        protected Label lblCity;
        protected TextBox txtCity;
        protected DropDownList lstState;
        protected Label lblEmail;
        protected TextBox txtEmail;
        protected Label lblAmt;
        protected HyperLink lnkReturn;
        protected ImageButton btnPay;
        protected DropDownList lstExpMonth;
        protected DropDownList lstExpYear;
        protected Label lblAcctNumber;
        protected Label lblPhoneNumber;
        protected TextBox txtFirstName;
        protected TextBox txtLastName;
        protected TextBox txtZip;
        protected TextBox txtCcNumber;
        protected TextBox txtCvNumber;
        protected Label lblFirstName;
        protected Label lblLastName;
        protected RequiredFieldValidator vldRfFirstName;
        protected RequiredFieldValidator vldRfLastName;
        protected RequiredFieldValidator vldRfCity;
        protected RequiredFieldValidator vldRfStreetAddress;
        protected RequiredFieldValidator vldRfZip;
        protected RequiredFieldValidator vldRfEmail;
        protected RequiredFieldValidator vldRfCardNumber;
        protected RequiredFieldValidator vldRfCvNumber;
        protected CustomValidator vldCstExpDate;
        protected CustomValidator vldCstCreditCardType;
        protected Label lblExp;

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.vldCstExpDate.ServerValidate += new ServerValidateEventHandler(this.expDateValidator_ServerValidate);
            this.vldCstCreditCardType.ServerValidate += new ServerValidateEventHandler(this.vldCstCreditCardType_ServerValidate);
            this.btnPay.Click += new ImageClickEventHandler(this.btnPay_Click);
            this.Load += new EventHandler(this.Page_Load);
            this.Init += new EventHandler(this.Page_Init);

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
            } catch (CreditCardValidationException ex) {
                ShowErrorMessage(ex.Message);
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
                if (exp < DateTime.Now) {
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
            args.IsValid = cardType.SelectedIndex != -1;
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

            txtAddr.Text = address.Street;
            txtCity.Text = address.City;
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

            CreditCard card = new CreditCard(
                MapCardType(cardType.SelectedValue),
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

            PaymentResult result = PaymentSvc.MakeCreditCardPayment(Map, GetAccountNumber(), card, amount, "public");

            switch (result.Code) {
                case PaymentResultCode.Completed:
                    SendNotification(amount, result.Payment.Id);

                    Session[PAYMENT_AMOUNT_KEY] = amount;
                    
                    Context.Items["payment"] = result.Payment;

                    Server.Transfer(SiteMap.PAYMENT_RECIEPT_URL);
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

        private CreditCardType MapCardType(string name)
        {
            switch (name) {
                case "VISA":
                    return CreditCardType.VISA;
                case "MAST":
                    return CreditCardType.MasterCard;
                case "AMEX":
                    return CreditCardType.AmericanExpress;
                case "DISC":
                    return CreditCardType.DiscoverCard;
                default:
                    throw new ArgumentOutOfRangeException("name", string.Format("Unsupported credit card type {0}", name));
            }
        }

        private void SendNotification(decimal amount, int paymentId)
        {
            string acctEmail = base.Cust.CustInfo.Email;
            string billEmail = txtEmail.Text;

            if (acctEmail != billEmail) {
                EmailSender.SendCreditCardPaymentNotification(acctEmail, base.Acct.FirstName, amount, paymentId);
            }

            EmailSender.SendCreditCardPaymentNotification(billEmail, txtFirstName.Text, amount, paymentId);
        }

        #endregion
    }
}