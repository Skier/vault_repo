using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DPI.Services;

namespace Dpi.Central.Web.Account.Payment
{
    public class PaymentSelectionPage : BasePaymentPage
    {
        #region Constants

        #endregion

        #region Web Form Designer generated code

        protected HyperLink lnkReturn;
        protected HtmlTableRow pastDueRow;
        protected CustomValidator vldCustErrorMsg;
        protected ValidationSummary vldSummary;
        protected Label lblDueDate;
        protected TextBox txtAmt;
        protected ImageButton btnCheckPay;
        protected Label lblAcctNumber;
        protected Label lblPhoneNumber;
        protected Label lblBalForward;
        protected Label lblCurrentChargesAmt;
        protected ImageButton btnCreditCardPay;
        protected Label lblAcctName;
        protected RequiredFieldValidator vldRfAmt;
        protected System.Web.UI.WebControls.CustomValidator vldCstAmt;
        protected Label lblAmt;

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);

            this.Load += new EventHandler(this.Page_Load);
            this.btnCreditCardPay.Click += new ImageClickEventHandler(this.btnCreditCardPay_Click);
        }

        private void InitializeComponent()
        {
            this.btnCheckPay.Click += new System.Web.UI.ImageClickEventHandler(this.btnCheckPay_Click);
            this.vldCstAmt.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.vldCstAmt_ServerValidate);

        }

        #endregion

        #region Event Handlers

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                lblAcctNumber.Text = base.Acct.AccNumber.ToString();
                lblPhoneNumber.Text = base.Acct.PhNumFormated;
                lblAcctName.Text = string.Format(NAME_FORMAT, base.Acct.FirstName, base.Acct.LastName);

                if (base.Acct.BalForward == 0.0m) {
                    pastDueRow.Visible = false;
                } else {
                    lblBalForward.Text = base.Acct.BalForward.ToString(MONEY_FORMAT);
                }

                lblDueDate.Text = base.Acct.DueDate.ToShortDateString();
                lblCurrentChargesAmt.Text = base.Acct.CurrCharges.ToString(MONEY_FORMAT);
                txtAmt.Text = base.Acct.DueAmt.ToString(MONEY_VALUE_FORMAT);
            }
        }

        private void btnCreditCardPay_Click(object sender, ImageClickEventArgs e)
        {
            if (!IsValid) {
                return;
            }

            decimal paymentAmount = decimal.Parse(txtAmt.Text);
            base.SetPaymentAmount(paymentAmount);

            Response.Redirect(SiteMap.CC_PAYMENT_URL);
        }

        private void btnCheckPay_Click(object sender, ImageClickEventArgs e)
        {
            if (!IsValid) {
                return;
            }

            decimal paymentAmount = decimal.Parse(txtAmt.Text);
            base.SetPaymentAmount(paymentAmount);

            Response.Redirect(SiteMap.CHECK_PAYMENT_URL);
        }

        private void vldCstAmt_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args) 
        {
            try {
                decimal amount = decimal.Parse(txtAmt.Text);

                if (amount < PaymentSvc.MINIMUM_PAYMENT_AMOUNT) {
                    args.IsValid = false;
                    vldCstAmt.ErrorMessage = "The minimum payment amount accepted is " + PaymentSvc.MINIMUM_PAYMENT_AMOUNT.ToString("C");
                } else if (amount > PaymentSvc.MAXIMUM_PAYMENT_AMOUNT) {
                    args.IsValid = false;
                    vldCstAmt.ErrorMessage = "The maximum payment amount accepted is " + PaymentSvc.MAXIMUM_PAYMENT_AMOUNT.ToString("C");
                } else {
                    args.IsValid = true;
                }
            } catch {
                args.IsValid = false;
                vldCstAmt.ErrorMessage = "Payment Amount is invalid.";
            }
        }

        #endregion
    }
}