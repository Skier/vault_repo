using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controls;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account.Payment
{
    public class RecurringPaymentsPage : BaseRecurringPaymentPage
    {
        #region Constants

        private const string CHANGE_STATUS_COMMAND = "ChangeStatus";

        #endregion

        #region Web Form Designer generated code

        protected LinkButton lbtnAddCCPayment;
        protected Footer _footer;
        protected HyperLink lnkReturn;
        protected LinkButton lbtnAddCheckPayment;
        protected ImageButton btnAddCCPayment;
        protected ImageButton btnAddCheckPayment;
        protected DataGrid dgPayments;

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
            this.btnAddCCPayment.Click += new System.Web.UI.ImageClickEventHandler(this.btnAddCCPayment_Click);
            this.btnAddCheckPayment.Click += new System.Web.UI.ImageClickEventHandler(this.btnAddCheckPayment_Click);
            this.dgPayments.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgPayments_ItemCommand);
            this.dgPayments.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgPayments_EditCommand);
            this.dgPayments.DataBinding += new System.EventHandler(this.dgPayments_DataBinding);
            this.dgPayments.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgPayments_ItemDataBound);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        #region Event Handlers

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                dgPayments.DataBind();
            }
        }

        private void dgPayments_DataBinding(object sender, EventArgs e)
        {
            ICustomerRecurringPayment[] payments = LoadRecurringPayments();
            dgPayments.DataSource = payments;
            dgPayments.Visible = payments.Length != 0;
        }

        private void dgPayments_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1) {
                ICustomerRecurringPayment payment = (ICustomerRecurringPayment) e.Item.DataItem;

                // Bind 'Type' column.
                Label typeLabel = (Label) e.Item.Cells[0].Controls[1];
                typeLabel.Text = Convert((PaymentType) payment.AccountTypeId);

                // Bind 'Status' column.
                Label statusLabel = (Label) e.Item.Cells[1].Controls[1];
                statusLabel.Text = payment.Active ? "Active" : "Inactive";

                // Bind 'Last 4 Digits' column.
                Label numberLabel = (Label) e.Item.Cells[2].Controls[1];
                if (payment.AccountTypeId == (int) PaymentType.Credit) {
                    numberLabel.Text = payment.BAccNumber.Substring(payment.BAccNumber.Length - 4);
                } else if (payment.AccountTypeId == (int) PaymentType.Check) {
                    numberLabel.Text = payment.BRouteNumber.Substring(payment.BRouteNumber.Length - 4);
                }

                // Change Status column.
                LinkButton changeStatusButton = (LinkButton) e.Item.Cells[4].Controls[0];
                changeStatusButton.Text = payment.Active ? "Deactivate" : "Activate";
            }
        }

        private void dgPayments_EditCommand(object source, DataGridCommandEventArgs e)
        {
            int paymentId = (int) dgPayments.DataKeys[e.Item.ItemIndex];
            ModifyRecurringPayment(paymentId);
        }

        private void dgPayments_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == CHANGE_STATUS_COMMAND) {
                int paymentId = (int) dgPayments.DataKeys[e.Item.ItemIndex];
                ChangeRecurringPaymentStatus(paymentId);
                dgPayments.DataBind();
            }
        }

        private void btnAddCCPayment_Click(object sender, ImageClickEventArgs e)
        {
            CreateRecurringPayment(PaymentType.Credit);
        }

        private void btnAddCheckPayment_Click(object sender, ImageClickEventArgs e)
        {
            CreateRecurringPayment(PaymentType.Check);
        }

        #endregion

        #region Private Methods

        private string Convert(PaymentType pType)
        {
            switch (pType) {
                case PaymentType.Cash:
                    return "Cash";

                case PaymentType.Check:
                    return "Check";

                case PaymentType.Credit:
                    return "Credit/Debit";

                case PaymentType.Debit:
                    return "Credit/Debit";

                default:
                    return pType.ToString();
            }
        }

        #endregion

        #region Implementation

        protected void CreateRecurringPayment(PaymentType paymentType)
        {
            Session["SELECTED_PAYMENT_ID_KEY"] = null;

            string nextStepUrl = paymentType == PaymentType.Credit ? SiteMap.REC_CC_PAYMENT_URL : SiteMap.REC_CHECK_PAYMENT_URL;

            ICustomerRecurringPayment activePayment = GetActiveRecurringPayment();
            if (activePayment != null) {
                Session["NEXT_STEP_URL_KEY"] = nextStepUrl;
                Response.Redirect(SiteMap.REC_DEACTIVATE_CONFIRMATION_URL);
            } else {
                Response.Redirect(nextStepUrl);
            }
        }

        protected void ModifyRecurringPayment(int paymentId)
        {
            Session["SELECTED_PAYMENT_ID_KEY"] = paymentId;

            ICustomerRecurringPayment payment = CustSvc.GetCustROP(Map, paymentId);
            string nextStepUrl = payment.AccountTypeId == (int) PaymentType.Credit ? SiteMap.REC_CC_PAYMENT_URL : SiteMap.REC_CHECK_PAYMENT_URL;

            Response.Redirect(nextStepUrl);
        }

        new protected void ChangeRecurringPaymentStatus(int paymentId)
        {
            ICustomerRecurringPayment payment = CustSvc.GetCustROP(Map, paymentId);
            if (payment.Active) {
                base.ChangeRecurringPaymentStatus(payment);
            } else {
                ICustomerRecurringPayment activePayment = GetActiveRecurringPayment();
                if (activePayment != null) {
                    Session["NEXT_STEP_URL_KEY"] = SiteMap.REC_PAYMENTS_URL;
                    Session["SELECTED_PAYMENT_ID_KEY"] = paymentId;
                    Response.Redirect(SiteMap.REC_DEACTIVATE_CONFIRMATION_URL);
                } else {
                    base.ChangeRecurringPaymentStatus(payment);
                }
            }
        }

        #endregion
    }
}