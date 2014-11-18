using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPI.ClientComp;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account.Payment
{
    public class CreditCardPaymentPage : BasePaymentPage
    {
        #region Web Form Designer generated code

        protected ImageButton btnPay;
        protected AccountInfoControl ctrlAccountInfo;
        protected System.Web.UI.HtmlControls.HtmlGenericControl detailsDiv;
        protected System.Web.UI.WebControls.TextBox txtPaymentAmount;
        protected System.Web.UI.WebControls.ImageButton btnBack;
        protected CreditCardInfoControl ctrlCreditCardInfo;

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.btnBack.Click += new System.Web.UI.ImageClickEventHandler(this.btnBack_Click);
            this.btnPay.Click += new System.Web.UI.ImageClickEventHandler(this.btnPay_Click);
            this.Load += new System.EventHandler(this.Page_Load);
            this.Init += new System.EventHandler(this.Page_Init);

        }

        #endregion

        #region Event Handlers

        private void Page_Init(object sender, EventArgs e)
        {
            EnsureOneClickBehaviour(btnPay);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                UpdateAccountInfoControl(ref ctrlAccountInfo);
                txtPaymentAmount.Text = base.GetPaymentAmount().ToString(MONEY_VALUE_FORMAT);

                ctrlAccountInfo.EnabledPhoneNumber = false;

                ctrlAccountInfo.FirstTabIndex = 0;
                ctrlCreditCardInfo.FirstTabIndex = (short)(ctrlAccountInfo.LastTabIndex + 1);
                btnPay.TabIndex = (short)(ctrlCreditCardInfo.LastTabIndex + 1);
                btnBack.TabIndex = (short)(btnPay.TabIndex + 1);
            }

            SetTogglePanelEffect();
        }

        private void btnPay_Click(object sender, ImageClickEventArgs e)
        {
            if (!IsValid) {
                return;
            }

            try {
                MakePayment();
            } catch (CreditCardValidationException) {
                SetErrorMessage("Credit Card number is invalid");
            } catch (Exception ex) {
                SetErrorMessage(ex);
            }
        }

        private void btnBack_Click(object sender, System.Web.UI.ImageClickEventArgs e) 
        {
            Response.Redirect(SiteMap.PAYMENT_SELECTION_URL);
        }

        #endregion

        #region Implementation

        private void SetTogglePanelEffect() 
        {
            detailsDiv.EnableViewState = false;
            ctrlCreditCardInfo.ToggleDetailsControl.Attributes.Add("onclick", "TogglePanel('" + detailsDiv.ClientID + "', 0.5, '" + ctrlCreditCardInfo.ToggleDetailsControl.ClientID + "');");
        }

        private void MakePayment()
        {
            decimal amount = decimal.Parse(txtPaymentAmount.Text);

			IAcctInfo acct = base.Acct;

            CreditCard card = new CreditCard(
                ctrlCreditCardInfo.CardType, ctrlCreditCardInfo.CcNumber, ctrlCreditCardInfo.CvNumber, 
                ctrlCreditCardInfo.ExpMonth, ctrlCreditCardInfo.ExpYear, ctrlAccountInfo.FirstName,
                ctrlAccountInfo.LastName, ctrlAccountInfo.Zip, ctrlAccountInfo.State, 
                ctrlAccountInfo.City, ctrlAccountInfo.StreetAddress, ctrlAccountInfo.PhoneNumber, 
                ctrlAccountInfo.Email);

            PaymentResult result = PaymentSvc.MakeCreditCardPayment(Map, GetAccountNumber(), card, amount,acct.DueAmt, "public", "DPI-Web",  "Web-User");

            switch (result.Code) {
                case PaymentResultCode.Completed:
                    SendNotification(amount, result.Payment.ConfNum);

                    Session[PAYMENT_AMOUNT_KEY] = amount;
					Session["card"] = card;
                    Session["payment"] = result.Payment;
                    Session["email"] = ctrlAccountInfo.Email;

                    Response.Redirect(SiteMap.PAYMENT_RECIEPT_URL);
                    break;
                case PaymentResultCode.Rejected:
                    SetErrorMessage(string.Format(REJECTED_PAYMENT, result.Description));
                    break;
                case PaymentResultCode.UnableToComplete:
                    SetErrorMessage(UNABLE_TO_COMPLETE_PAYMENT);
                    break;
                case PaymentResultCode.NeedVerification:
                    SetErrorMessage(string.Format(NEED_VERIFICATION, amount.ToString("C")));
                    break;
                default:
                    throw new ApplicationException("Payment result code is unknown: " + result.Code + ".");
            }
            
            if (result.Code != PaymentResultCode.Completed)
                SendDeclinedNotification(amount);
        }

        private void SendNotification(decimal amount, int confNum)
        {
            string acctEmail = base.Cust.CustInfo.Email;
            EmailSender.SendCreditCardPaymentNotification(acctEmail, NameFormatter.Format(Acct), amount, confNum);
            if (ctrlAccountInfo.Email != acctEmail) {
                EmailSender.SendCreditCardPaymentNotification(ctrlAccountInfo.Email, NameFormatter.Format(ctrlAccountInfo.FirstName, ctrlAccountInfo.LastName), amount, confNum);
            }
        }
        
        private void SendDeclinedNotification(decimal amount)
        {
            string acctEmail = base.Cust.CustInfo.Email;
            EmailSender.SendCreditCardPaymentDeclinedNotification(acctEmail, NameFormatter.Format(Acct), amount);
            if (ctrlAccountInfo.Email != acctEmail) {
                EmailSender.SendCreditCardPaymentDeclinedNotification(ctrlAccountInfo.Email, NameFormatter.Format(ctrlAccountInfo.FirstName, ctrlAccountInfo.LastName), amount);
            }
        }

        #endregion
    }
}