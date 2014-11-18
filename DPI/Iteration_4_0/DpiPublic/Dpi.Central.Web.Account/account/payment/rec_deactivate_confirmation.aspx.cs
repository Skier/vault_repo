using System;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.Payment
{
    public class DeactivateConfirmationPage : BaseRecurringPaymentPage
    {
        #region Constants

        const string CONFIRMATION_FORMAT = "An Active recurring payment option already exist with {0} Number ending in {1}.<br>Do you want to deactivate the existing recurring payment option and make this one active?";

        #endregion

        #region Web Form Designer generated code

        protected Dpi.Central.Web.Controls.Footer _footer;
        protected System.Web.UI.WebControls.ImageButton btnSubmit;
        protected System.Web.UI.WebControls.ImageButton btnCancel;
        protected System.Web.UI.WebControls.Label lblPaymentQuestion;

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();

            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSubmit.Click += new System.Web.UI.ImageClickEventHandler(this.btnSubmit_Click);
            this.btnCancel.Click += new System.Web.UI.ImageClickEventHandler(this.btnCancel_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        #region Event Handlers

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                if (Session["NEXT_STEP_URL_KEY"] != null && Session["NEXT_STEP_URL_KEY"] is string) {
                    NextStepUrl = (string)Session["NEXT_STEP_URL_KEY"];
                } else {
                    throw new ApplicationException("Session state is invalid: NEXT_STEP_URL_KEY is empty.");
                }

                ICustomerRecurringPayment activePayment = GetActiveRecurringPayment();
                if (activePayment == null) {
                    throw new ApplicationException("Active recurring payment is not found.");
                }
                
                if (activePayment.AccountTypeId == (int) PaymentType.Credit) {
                    lblPaymentQuestion.Text = string.Format(CONFIRMATION_FORMAT, "Credit Card", activePayment.BAccNumber.Substring(activePayment.BAccNumber.Length - 4));
                } else if (activePayment.AccountTypeId == (int) PaymentType.Check) {
                    lblPaymentQuestion.Text = string.Format(CONFIRMATION_FORMAT, "Bank Route", activePayment.BRouteNumber.Substring(activePayment.BRouteNumber.Length - 4));
                }
            }
        }

        private void btnSubmit_Click(object sender, System.Web.UI.ImageClickEventArgs e) 
        {
            ProcessSubmitConfirmation(NextStepUrl, SelectedPaymentId);
        }

        private void btnCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e) 
        {
            Response.Redirect(SiteMap.REC_PAYMENTS_URL);
        }

        #endregion

        #region Properties

        private string NextStepUrl
        {
            get {
                object value = ViewState["NEXT_STEP_URL_KEY"];
                if (value != null || value is string) {
                    return (string)value;
                }

                throw new ApplicationException("View state is invalid: NEXT_STEP_URL_KEY is empty.");
            }

            set { ViewState["NEXT_STEP_URL_KEY"] = value; }
        }

        #endregion

        #region Implementation

        private void ProcessSubmitConfirmation(string nextStepUrl, int selectedPaymentId)
        {
            ICustomerRecurringPayment activePayment = GetActiveRecurringPayment();
            if (activePayment == null) {
                throw new ApplicationException("Active recurring payment is not found.");
            }

            if (nextStepUrl == SiteMap.REC_PAYMENTS_URL) {
                if (selectedPaymentId == -1) {
                    throw new ApplicationException("Selected payment id can not be empty.");
                }

                ChangeRecurringPaymentStatus(activePayment);
                ChangeRecurringPaymentStatus(selectedPaymentId);
            } else {
                // This case is for creating a new recurring payment.
                Session["SELECTED_PAYMENT_ID_KEY"] = null;
            }

            Response.Redirect(nextStepUrl);
        }

        #endregion
    }
}