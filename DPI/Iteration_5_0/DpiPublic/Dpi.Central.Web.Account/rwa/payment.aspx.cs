using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Account.Payment;
using Dpi.Central.Web.Controls;
using DPI.Components;

namespace Dpi.Central.Web.Account.Wireless.Processes.Rwa
{
    public class PaymentPage : ReplenishWirelessAccountBasePage
    {
        #region Web Form Designer generated code

        protected ImageButton btnPay;
        protected Footer _footer;
        protected AccountInfoControl ctrlAccountInfo;
        protected CreditCardInfoControl ctrlCreditCardInfo;
        protected HtmlAnchor lnkToggleRecPaymentDetails;
        protected HtmlGenericControl recPaymentDetailsDiv;
        protected RadioButtonList RadioButtonList1;
        protected RadioButton rbCreditCard;
        protected RadioButton rbCheck;
        protected RequiredFieldValidator vldRfAmt;
        protected TextBox txtPaymentAmount;
        protected CheckBox chkRecurringPayment;
        protected HtmlGenericControl divCreditCardDetails;
        protected HtmlGenericControl divCheckDetails;
        protected HtmlGenericControl divCreditCard;
        protected HtmlGenericControl divCheck;
        protected HtmlGenericControl divButtonsRow;
        protected System.Web.UI.WebControls.CustomValidator vldCstPaymentAmount;
        protected System.Web.UI.WebControls.ImageButton btnPrevious;
        protected CheckInfoControl ctrlCheckInfo;

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.vldCstPaymentAmount.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.vldCstPaymentAmount_ServerValidate);
            this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrevious_Click);
            this.btnPay.Click += new System.Web.UI.ImageClickEventHandler(this.OnPayClick);
            this.Load += new System.EventHandler(this.OnPageLoad);
            this.Init += new System.EventHandler(this.OnPageInit);

        }

        #endregion

        #region Event Handlers

        private void OnPageInit(object sender, EventArgs e) 
        {
            EnsureOneClickBehaviour(btnPay);
        }

        private void OnPageLoad(object sender, EventArgs e)
        {
            rbCreditCard.Attributes.Add("onclick", "ShowCreditCard();");
            rbCheck.Attributes.Add("onclick", "ShowCheck();");

            if (rbCreditCard.Checked) {
                divCreditCard.Style.Add("display", "visible");
                divCheck.Style.Add("display", "none");
                divButtonsRow.Style.Add("display", "visible");
                ctrlCreditCardInfo.EnabledValidators = true;
                ctrlCheckInfo.EnabledValidators = false;
            } else if (rbCheck.Checked) {
                divCreditCard.Style.Add("display", "none");
                divCheck.Style.Add("display", "visible");
                divButtonsRow.Style.Add("display", "visible");
                ctrlCreditCardInfo.EnabledValidators = false;
                ctrlCheckInfo.EnabledValidators = true;
            } else {
                divCreditCard.Style.Add("display", "none");
                divCheck.Style.Add("display", "none");
                divButtonsRow.Style.Add("display", "none");
                ctrlCreditCardInfo.EnabledValidators = true;
                ctrlCheckInfo.EnabledValidators = true;
            }

            divCreditCardDetails.Style.Add("display", "none");
            divCheckDetails.Style.Add("display", "none");

            if (!IsPostBack) {
                ctrlAccountInfo.VisibilityForAccountNumber = ctrlAccountInfo.VisibilityForPhoneNumber = false;
                SetTabOrder();
            }

            SetTogglePanelEffect();
            SetEnterKeyPressHandler(btnPay);
        }

        private void vldCstPaymentAmount_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args) 
        {
            try {
                decimal amount = decimal.Parse(txtPaymentAmount.Text);

                decimal min, max;
                DPI.Components.Payment.GetMinMaxPaymentAmount(out min, out max);
                if (amount < min) {
                    args.IsValid = false;
                    vldCstPaymentAmount.ErrorMessage = "<br>The minimum payment amount accepted is " + min.ToString("C");
                } else if (amount > max) {
                    args.IsValid = false;
                    vldCstPaymentAmount.ErrorMessage = "<br>The maximum payment amount accepted is " + max.ToString("C");
                } else {
                    args.IsValid = true;
                }
            } catch (Exception ex) {
                args.IsValid = false;
                vldCstPaymentAmount.ErrorMessage = string.Empty;
                SetErrorMessage(ex.Message);
            }
        }

        private void btnPrevious_Click(object sender, System.Web.UI.ImageClickEventArgs e) 
        {
            Response.Redirect(SiteMap.RWA_ORDER_SUMMARY_URL, false);
        }

        private void OnPayClick(object sender, ImageClickEventArgs e)
        {
            if (!IsValid) {
                return;
            }

            PaymentAmount = decimal.Parse(txtPaymentAmount.Text);

            try {
                PaymentResult result;

                if (rbCreditCard.Checked) {
                    CreditCard card = new CreditCard(
                        ctrlCreditCardInfo.CardType, ctrlCreditCardInfo.CcNumber, ctrlCreditCardInfo.CvNumber,
                        ctrlCreditCardInfo.ExpMonth, ctrlCreditCardInfo.ExpYear, ctrlAccountInfo.FirstName,
                        ctrlAccountInfo.LastName, ctrlAccountInfo.Zip, ctrlAccountInfo.State,
                        ctrlAccountInfo.City, ctrlAccountInfo.StreetAddress, base.PhoneNumber,
                        ctrlAccountInfo.Email);

                    result = base.MakePayment(card);
                } else if (rbCheck.Checked) {
                    BankCheck bankCheck = new BankCheck(
                        ctrlCheckInfo.BankRouteNumber, ctrlCheckInfo.BankAccountNumber, ctrlCheckInfo.DriverLicenseNumber,
                        ctrlCheckInfo.DriverLicenseState, ctrlAccountInfo.FirstName, ctrlAccountInfo.LastName,
                        ctrlAccountInfo.Zip, ctrlAccountInfo.State, ctrlAccountInfo.City,
                        ctrlAccountInfo.StreetAddress, base.PhoneNumber, ctrlAccountInfo.Email);

                    result = base.MakePayment(bankCheck);
                } else {
                    SetErrorMessage("Please choose the payment type.");
                    return;
                }

                switch (result.Code) {
                    case PaymentResultCode.Completed:
                        Response.Redirect(SiteMap.RWA_RECEIPT_URL, false);
                        break;
                    case PaymentResultCode.Rejected:
                        SetErrorMessage(string.Format(REJECTED_PAYMENT, result.Description));
                        break;
                    case PaymentResultCode.UnableToComplete:
                        SetErrorMessage(UNABLE_TO_COMPLETE_PAYMENT);
                        break;
                    case PaymentResultCode.NeedVerification:
                        SetErrorMessage(string.Format(NEED_VERIFICATION, base.PaymentAmount.ToString("C")));
                        break;
                    default:
                        throw new ApplicationException("Payment result code is unknown: " + result.Code + ".");
                }
            } catch (CreditCardValidationException) {
                SetErrorMessage("Credit Card number is invalid.");
            } catch (PaymentException ex) {
                SetErrorMessage("Payment processing failed. Confirmation number is " + ex.ConfirmationNumber + ". Please contact the support service for more information.");
            } catch (Exception ex) {
                SetErrorMessage(ex);
            }
        }

        #endregion

        #region Private Methods

        private void SetTabOrder() 
        {
            ctrlAccountInfo.FirstTabIndex = 1;
            rbCreditCard.TabIndex = rbCheck.TabIndex = (short) (ctrlAccountInfo.LastTabIndex + 1);
            ctrlCreditCardInfo.FirstTabIndex = ctrlCheckInfo.FirstTabIndex = (short) (rbCreditCard.TabIndex + 1);
            txtPaymentAmount.TabIndex = (short) (Math.Max(ctrlCreditCardInfo.LastTabIndex, ctrlCheckInfo.LastTabIndex) + 1);
            btnPay.TabIndex = (short) (txtPaymentAmount.TabIndex + 1);
        }

        private void SetTogglePanelEffect() 
        {
            divCreditCardDetails.EnableViewState = false;
            ctrlCreditCardInfo.ToggleDetailsControl.Attributes.Add("onclick", "TogglePanel('" + divCreditCardDetails.ClientID + "', 0.5, '" + ctrlCreditCardInfo.ToggleDetailsControl.ClientID + "');");

            divCheckDetails.EnableViewState = false;
            ctrlCheckInfo.ToggleDetailsControl.Attributes.Add("onclick", "TogglePanel('" + divCheckDetails.ClientID + "', 1, '" + ctrlCheckInfo.ToggleDetailsControl.ClientID + "');");
        }

        #endregion
    }
}