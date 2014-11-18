using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controllers;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.Payment
{
    public class RecurringPaymentManagerPage : Page
    {
        #region Constants

        const string DEACTIVATE_COMMAND = "Deactivate";

        #endregion

        #region Web Form Designer generated code

        protected Label lblErrMsg;
        protected LinkButton lbtnAddCCPayment;
        protected LinkButton lbtnAddBankPayment;
        protected DataGrid dgPayments;
        protected HeaderUserControl ctrlHeader;

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
            this.lbtnAddCCPayment.Click += new System.EventHandler(this.AddNewCCPaymentHandler);
            this.lbtnAddBankPayment.Click += new System.EventHandler(this.AddNewBankPaymentHandler);
            this.dgPayments.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.ItemCommandHandler);
            this.dgPayments.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.EditCommandHandler);
            this.dgPayments.DataBinding += new System.EventHandler(this.PaymentsDataBindingHandler);
            this.dgPayments.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.ItemDataBoundHandler);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        #region Event Handlers

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                ctrlHeader.ShowLogoutButton(true);

                try {
                    DataBind();
                } catch (Exception ex) {
                    ShowErrorMessage(ex);
                }
            }
        }

        #endregion

        #region Implementation

        private void ShowErrorMessage(Exception ex)
        {
            lblErrMsg.Text = "Error:" + ex.Message;
            lblErrMsg.Visible = true;
        }

        #endregion	

        private void ItemDataBoundHandler(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1) {
                ICustomerRecurringPayment payment = (ICustomerRecurringPayment) e.Item.DataItem;

                // Bind 'Type' column.
                int index = Finder.FindFirstIndex("Type", dgPayments);
                Label typeLabel = (Label) Finder.FindFirstControl(
                    typeof (Label), e.Item.Cells[index].Controls);
                typeLabel.Text = Convertor.Convert((PaymentType) payment.AccountTypeId);

                // Bind 'Status' column.
                index = Finder.FindFirstIndex("Status", dgPayments);
                Label statusLabel = (Label) Finder.FindFirstControl(
                    typeof (Label), e.Item.Cells[index].Controls);
                statusLabel.Text = payment.Active ? "Active" : "Inactive";

                // Bind 'Last 4 Digits' column.
                index = Finder.FindFirstIndex("Last 4 Digits", dgPayments);
                Label numberLabel = (Label) Finder.FindFirstControl(
                    typeof (Label), e.Item.Cells[index].Controls);
                numberLabel.Text = payment.BAccNumber.Substring(
                    payment.BAccNumber.Length - 4);

                // Disable command buttons if the payment is inactive.
                if (!payment.Active) {
                    index = Finder.FindFirstIndex("Modify", dgPayments);
                    LinkButton modifyButton = (LinkButton) Finder.FindFirstControl(
                        typeof (LinkButton), e.Item.Cells[index].Controls);
                    modifyButton.Enabled = false;

                    index = Finder.FindFirstIndex("Deactivate", dgPayments);
                    LinkButton deactivateButton = (LinkButton) Finder.FindFirstControl(
                        typeof (LinkButton), e.Item.Cells[index].Controls);
                    deactivateButton.Enabled = false;
                }

                // Just one active payment is allowed.
                if (payment.Active) {
                    lbtnAddCCPayment.Visible = lbtnAddBankPayment.Visible = false;
                }
            }
        }

        private void AddNewCCPaymentHandler(object sender, EventArgs e)
        {
            RecurringPaymentController.Instance.Mode = EditorMode.Creator;
            Response.Redirect(UrlDictionary.CC_RECURRING_PAYMENT_URL);
        }

        private void AddNewBankPaymentHandler(object sender, System.EventArgs e) 
        {
            RecurringPaymentController.Instance.Mode = EditorMode.Creator;
            Response.Redirect(UrlDictionary.BANK_RECURRING_PAYMENT_URL);
        }

        private void EditCommandHandler(object source, DataGridCommandEventArgs e)
        {
            int paymentId = (int) dgPayments.DataKeys[e.Item.ItemIndex];
            RecurringPaymentController.Instance.EditPayment(paymentId);
        }

        private void PaymentsDataBindingHandler(object sender, EventArgs e)
        {
            // Set "Add ..." links visible. If there is an active payment
            // then the links become invisible in ItemDataBoundHandler methods.
            // Just one active payment is allowed.
            lbtnAddCCPayment.Visible = lbtnAddBankPayment.Visible = true;

            RecurringPaymentController controller = RecurringPaymentController.Instance;
            ICustomerRecurringPayment[] payments = controller.RetrieveAllPayments();

            dgPayments.DataSource = payments;
        }

        private void ItemCommandHandler(object source, DataGridCommandEventArgs e) 
        {
            if (e.CommandName != DEACTIVATE_COMMAND) {
                return;
            }

            try {
                int paymentId = (int) dgPayments.DataKeys[e.Item.ItemIndex];
                RecurringPaymentController.Instance.DeactivatePayment(paymentId);
                DataBind();
            } catch (Exception ex) {
                ShowErrorMessage(ex);
            }
        }
    }
}