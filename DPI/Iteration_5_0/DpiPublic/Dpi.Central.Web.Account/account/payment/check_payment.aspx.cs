using System;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controls;
using DPI.ClientComp;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account.Payment
{
    public class CheckPaymentPage : BasePaymentPage
    {
        #region Web Form Designer generated code

        protected ImageButton btnPay;
        protected Footer _footer;
        protected AccountInfoControl ctrlAccountInfo;
        protected System.Web.UI.WebControls.TextBox txtPaymentAmount;
        protected System.Web.UI.WebControls.ImageButton btnBack;
        protected System.Web.UI.HtmlControls.HtmlGenericControl detailsDiv;
        protected CheckInfoControl ctrlCheckInfo;

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
                txtPaymentAmount.Text = GetPaymentAmount().ToString(MONEY_VALUE_FORMAT);

                ctrlAccountInfo.EnabledPhoneNumber = false;

                ctrlAccountInfo.FirstTabIndex = 0;
                ctrlCheckInfo.FirstTabIndex = (short)(ctrlAccountInfo.LastTabIndex + 1);
                btnPay.TabIndex = (short)(ctrlCheckInfo.LastTabIndex + 1);
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
            } catch (CreditCardValidationException ex) {
                SetErrorMessage(ex.Message);
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
            ctrlCheckInfo.ToggleDetailsControl.Attributes.Add("onclick", "TogglePanel('" + detailsDiv.ClientID + "', 1, '" + ctrlCheckInfo.ToggleDetailsControl.ClientID + "');");
        }

        private void MakePayment()
        {
            decimal amount = decimal.Parse(txtPaymentAmount.Text);
            IAcctInfo acct = base.Acct;

            BankCheck bankCheck = new BankCheck(
                ctrlCheckInfo.BankRouteNumber, ctrlCheckInfo.BankAccountNumber, ctrlCheckInfo.DriverLicenseNumber,
                ctrlCheckInfo.DriverLicenseState, ctrlAccountInfo.FirstName, ctrlAccountInfo.LastName, 
                ctrlAccountInfo.Zip, ctrlAccountInfo.State, ctrlAccountInfo.City, 
                ctrlAccountInfo.StreetAddress, ctrlAccountInfo.PhoneNumber, ctrlAccountInfo.Email);

            StringCollection errors = bankCheck.Validate();

            if (errors.Count > 0) {
                SetErrorMessage(errors[0]);
                return;
            }

            PaymentResult result = PaymentSvc.MakePaymentByCheck(Map, GetAccountNumber(), bankCheck, amount, acct.DueAmt, "public", "DPI-Web", "Web-User");

            switch (result.Code) {
                case PaymentResultCode.Completed:
                    SendNotification(amount, result.Payment.ConfNum);

                    Session[PAYMENT_AMOUNT_KEY] = amount;
                    Session["bankCheck"] = bankCheck;
                    Session["payment"] = result.Payment;
                    Session["email"] = ctrlAccountInfo.Email;

                    Response.Redirect(SiteMap.PAYMENT_RECIEPT_URL);
                    break;
                case PaymentResultCode.Rejected:
                    string rejectedDetails = result.Description.Trim();
                    if (rejectedDetails.EndsWith(".")) {
                        rejectedDetails = rejectedDetails.Remove(rejectedDetails.Length - 1, 1);
                    }

                    SetErrorMessage(string.Format(REJECTED_PAYMENT, rejectedDetails));
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
            EmailSender.SendCheckPaymentNotification(acctEmail, NameFormatter.Format(Acct), amount, confNum);
            if (ctrlAccountInfo.Email != acctEmail) {
                EmailSender.SendCheckPaymentNotification(ctrlAccountInfo.Email, NameFormatter.Format(ctrlAccountInfo.FirstName, ctrlAccountInfo.LastName), amount, confNum);
            }
        }
        
        private void SendDeclinedNotification(decimal amount)
        {
            string acctEmail = base.Cust.CustInfo.Email;
            EmailSender.SendCheckPaymentDeclinedNotification(acctEmail, NameFormatter.Format(Acct), amount);
            if (ctrlAccountInfo.Email != acctEmail) {
                EmailSender.SendCheckPaymentDeclinedNotification(ctrlAccountInfo.Email, NameFormatter.Format(ctrlAccountInfo.FirstName, ctrlAccountInfo.LastName), amount);
            }
        }

        #endregion
    }
}