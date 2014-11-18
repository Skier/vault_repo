using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controllers;
using DPI.ClientComp;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.Payment
{
    public class BankReccuringPaymentEditorPage : Page
    {
        #region Web Form Designer generated code

        protected Label lblMessage;
        protected Label lblPhoneNumber;
        protected Label lblAccountNumber;
        protected TextBox txtAccountNumber;
        protected Label lblPassword;
        protected ImageButton btnSubmit;
        protected TextBox txtPassword;
        protected RegularExpressionValidator anRegExpValidator;
        protected RequiredFieldValidator pwdReqFldValidator;
        protected LinkButton lbtnSignUp;
        protected TextBox txtNpa;
        protected Label lblOr;
        protected TextBox txtNxx;
        protected TextBox txtNumber;
        protected Label lblDefis2;
        protected Label lblDefis1;
        protected Label lblErrMsg;
        protected ValidationSummary vldSummary;
        protected LinkButton lbtnForgotMyPassword;
        protected TextBox txtBankRouteNumber;
        protected TextBox txtBankAccountNumber;
        protected RequiredFieldValidator vldRfBankAccountNumber;
        protected RequiredFieldValidator vldRfBankRouteNumber;
        protected RequiredFieldValidator vldRfDrvLicState;
        protected DropDownList ddlDrvLicState;
        protected BillingAccountInfoEditor ctrlBillingAccountInfo;
        protected RegularExpressionValidator vldReBankAccountNumber;
        protected RegularExpressionValidator vldReBankRouteNumber;
        protected CustomValidator vldCustErrorMsg;
        protected ImageButton btnCancel;
        protected System.Web.UI.WebControls.Label lblDrvLicNumber;
        protected System.Web.UI.WebControls.TextBox txtDrvLicNumber;
        protected System.Web.UI.WebControls.Label lblDrvLicState;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfDrvLicNumber;
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
            this.ddlDrvLicState.DataBinding += new System.EventHandler(this.DLStateDataBindingHandler);
            this.btnSubmit.Click += new System.Web.UI.ImageClickEventHandler(this.SubmitHandler);
            this.btnCancel.Click += new System.Web.UI.ImageClickEventHandler(this.CancelHandler);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        #region Event Handlers

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                ctrlHeader.ShowLogoutButton(true);

                DataBind();

                RecurringPaymentController controller = RecurringPaymentController.Instance;

                if (controller.Mode == EditorMode.Editor) {
                    ICustomerRecurringPayment payment;
                    payment = controller.RetrievePayment(controller.PaymentId);

                    Update(ref payment, UpdateDirection.ToPage);

                    // Constrols disable.
                    txtBankAccountNumber.Enabled = false;
                    txtBankRouteNumber.Enabled = false;
                    lblDrvLicState.Visible = ddlDrvLicState.Visible = false;
                    lblDrvLicNumber.Visible = txtDrvLicNumber.Visible = false;

                    // Validators disable.
                    vldRfBankAccountNumber.Visible = vldReBankAccountNumber.Visible = false;
                    vldRfBankRouteNumber.Visible = vldReBankRouteNumber.Visible = false;
                    vldRfDrvLicNumber.Visible = vldRfDrvLicState.Visible = false;
                }
            }
        }

        private void DLStateDataBindingHandler(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList) sender;

            ddl.DataSource = DropDownListDate.GetStatesShort(true);
            ddl.DataTextField = "DDLText";
            ddl.DataValueField = "DDLValue";
        }

        private void SubmitHandler(object sender, ImageClickEventArgs e)
        {
            if (!Page.IsValid) {
                return;
            }

            RecurringPaymentController controller = RecurringPaymentController.Instance;
            ICustomerRecurringPayment payment = controller.GetCCPayment(PaymentType.Check);

            Update(ref payment, UpdateDirection.FromPage);

            try {
                controller.SavePayment(payment);
            } catch (Exception ex) {
                vldCustErrorMsg.IsValid = false;
                vldCustErrorMsg.ErrorMessage = ex.Message;
            }
        }

        private void CancelHandler(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(UrlDictionary.RECURRING_PAYMENT_MANAGER_URL);
        }

        #endregion

        #region Private Methods

        private void Update(ref ICustomerRecurringPayment payment, UpdateDirection updateDirection)
        {
            ctrlBillingAccountInfo.Update(ref payment, updateDirection);

            if (updateDirection == UpdateDirection.FromPage) {
                payment.DateModified = DateTime.Now;
                payment.Active = true;
                payment.Priority = 1;

                RecurringPaymentController controller = RecurringPaymentController.Instance;

                if (controller.Mode == EditorMode.Creator) {
                    payment.BAccNumber = txtBankAccountNumber.Text;
                    payment.BRouteNumber = txtBankRouteNumber.Text;
                    payment.DLStateNumber = ddlDrvLicState.SelectedValue + txtDrvLicNumber.Text;
                }
            } else {
                txtBankAccountNumber.Text = "************" + payment.BAccNumber.Substring(
                    payment.BAccNumber.Length - 4);
                txtBankRouteNumber.Text = "*********" + payment.BRouteNumber.Substring(
                    payment.BRouteNumber.Length - 3);
                ddlDrvLicState.SelectedValue = payment.DLStateNumber.Substring(0,2);
                txtDrvLicNumber.Text = payment.DLStateNumber.Substring(2);
            }
        }

        #endregion
    }
}