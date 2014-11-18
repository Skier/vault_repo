using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controls;

namespace Dpi.Central.Web.Account.Payment
{
    public class PaymentSelectionPage : BasePaymentPage
    {
        #region Constants

        #endregion

        #region Web Form Designer generated code

        protected HyperLink lnkReturn;
        protected CustomValidator vldCustErrorMsg;
        protected ValidationSummary vldSummary;
        protected Label lblDueDate;
        protected TextBox txtAmt;
        protected ImageButton btnCheckPay;
        protected System.Web.UI.WebControls.Label lblAcctNumber;
        protected System.Web.UI.WebControls.Label lblPhoneNumber;
        protected System.Web.UI.WebControls.Label lblBalForward;
        protected System.Web.UI.WebControls.Label lblCurrentChargesAmt;
        protected ImageButton btnCreditCardPay;
        protected System.Web.UI.WebControls.Label lblAcctName;
        protected RequiredFieldValidator vldRfAmt;
        protected CustomValidator vldCstAmt;
        protected Footer _footer;
        protected System.Web.UI.WebControls.ImageButton btnBack;
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
            this.vldCstAmt.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.vldCstAmt_ServerValidate);
            this.btnCheckPay.Click += new System.Web.UI.ImageClickEventHandler(this.btnCheckPay_Click);

        }

        #endregion

        #region Event Handlers

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                lblAcctNumber.Text = base.Acct.AccNumber.ToString();
                lblPhoneNumber.Text = base.Acct.PhNumFormated;
                lblAcctName.Text = string.Format(NAME_FORMAT, base.Acct.FirstName, base.Acct.LastName);
                lblBalForward.Text = base.Acct.BalForward.ToString(MONEY_FORMAT);
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

        private void vldCstAmt_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Control postBackControl = GetPostBackControl(this);

            if (postBackControl == null) {
                args.IsValid = false;
                vldCstAmt.ErrorMessage = "<br>Please select a Payment Type to continue with the processing of your payment.";
            } else {
                try {
                    decimal amount = decimal.Parse(txtAmt.Text);

                    decimal min, max;
                    DPI.Components.Payment.GetMinMaxPaymentAmount(out min, out max);
                    if (amount < min) {
                        args.IsValid = false;
                        vldCstAmt.ErrorMessage = "<br>The minimum payment amount accepted is " + min.ToString("C");
                    } else if (amount > max) {
                        args.IsValid = false;
                        vldCstAmt.ErrorMessage = "<br>The maximum payment amount accepted is " + max.ToString("C");
                    } else {
                        args.IsValid = true;
                    }
                } catch {
                    args.IsValid = false;
                    vldCstAmt.ErrorMessage = "<br>Payment Amount provided is invalid.";
                }
            }
        }

        #endregion

        #region Helper

        private Control GetPostBackControl(Page page)
        {
            Control control = null;
            string ctrlname = page.Request.Params["__EVENTTARGET"];
            if (ctrlname != null && ctrlname != String.Empty) {
                control = page.FindControl(ctrlname);
            }
                // if __EVENTTARGET is null, the control is a button type and we need to 
                // iterate over the form collection to find it
            else {
                string ctrlStr = String.Empty;
                Control c = null;
                foreach (string ctl in page.Request.Form) {
                    // handle ImageButton controls ...
                    if (ctl.EndsWith(".x") || ctl.EndsWith(".y")) {
                        ctrlStr = ctl.Substring(0, ctl.Length - 2);
                        c = page.FindControl(ctrlStr);
                    } else {
                        c = page.FindControl(ctl);
                    }
                    if (c is Button ||
                        c is ImageButton) {
                        control = c;
                        break;
                    }
                }
            }
            return control;
        }

        #endregion
    }
}