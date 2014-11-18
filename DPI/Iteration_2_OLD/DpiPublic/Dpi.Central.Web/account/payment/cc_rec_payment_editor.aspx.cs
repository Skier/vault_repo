using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controllers;
using DPI.ClientComp;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.Payment
{
    public class CreditCardReccuringPaymentEditorPage : Page
    {
        #region Constants

        private const string VISA_CC_TYPE = "Visa";
        private const string MASTER_CC_TYPE = "Master Card";
        private const string DISCOVER_CC_TYPE = "Discover";
        private const string AMEX_CC_TYPE = "AMEX";

        #endregion

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
        protected RequiredFieldValidator vldRfCcNumber;
        protected TextBox txtCcNumber;
        protected DropDownList ddlExpYear;
        protected DropDownList ddlExpMonth;
        protected TextBox txtSecurityCode;
        protected RequiredFieldValidator vldRfSecurityCode;
        protected RequiredFieldValidator vldRfExpMonth;
        protected RequiredFieldValidator vldRfExpYear;
        protected Label lblErrMsg;
        protected ValidationSummary vldSummary;
        protected LinkButton lbtnForgotMyPassword;
        protected BillingAccountInfoEditor ctrlBillingAccountInfo;
        protected RegularExpressionValidator vldReCcNumber;
        protected RegularExpressionValidator vldReSecurityCode;
        protected CustomValidator vldCstExpDate;
        protected CustomValidator vldCustErrorMsg;
        protected ImageButton btnCancel;
        protected RequiredFieldValidator vldRfCcType;
        protected DropDownList ddlCcType;
        protected Label lblCcType;
        protected Label lblSecurityCode;
        protected Label lblExpDate;
        protected Label lblExpDateSeparator;
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
            this.ddlCcType.DataBinding += new EventHandler(this.CreditCardTypeDataBindingHandler);
            this.ddlCcType.SelectedIndexChanged += new EventHandler(this.CreditCardTypeChangedHandler);
            this.ddlExpMonth.DataBinding += new EventHandler(this.ExpMonthDataBindingHandler);
            this.ddlExpYear.DataBinding += new EventHandler(this.ExpYearDataBindingHandler);
            this.vldCstExpDate.ServerValidate += new ServerValidateEventHandler(this.ExpirationDateValidationHandler);
            this.btnSubmit.Click += new ImageClickEventHandler(this.SubmitHandler);
            this.btnCancel.Click += new ImageClickEventHandler(this.CancelHandler);
            this.Load += new EventHandler(this.Page_Load);

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
                    ddlCcType.Enabled = false;
                    txtCcNumber.Enabled = false;
                    lblExpDate.Visible = lblExpDateSeparator.Visible = ddlExpMonth.Visible = ddlExpYear.Visible = false;
                    lblSecurityCode.Visible = txtSecurityCode.Visible = false;

                    // Validators disable.
                    vldRfCcType.Visible = false;
                    vldRfCcNumber.Visible = vldReCcNumber.Visible = false;
                    vldRfExpMonth.Visible = vldRfExpYear.Visible = vldCstExpDate.Visible = false;
                    vldRfSecurityCode.Visible = vldReSecurityCode.Visible = false;
                }
            }
        }

        private void ExpMonthDataBindingHandler(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList) sender;

            ddl.DataSource = DropDownListDate.GetCCMonths(true);
            ddl.DataTextField = "DDLText";
            ddl.DataValueField = "DDLValue";
        }

        private void ExpYearDataBindingHandler(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList) sender;

            ddl.DataSource = DropDownListDate.GetYears(DateTime.Now.Year, 12, true);
            ddl.DataTextField = "DDLText";
            ddl.DataValueField = "DDLValue";
        }

        private void SubmitHandler(object sender, ImageClickEventArgs e)
        {
            if (!Page.IsValid) {
                return;
            }

            RecurringPaymentController controller = RecurringPaymentController.Instance;
            ICustomerRecurringPayment payment = controller.GetCCPayment(PaymentType.Credit);

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

        private void ExpirationDateValidationHandler(object source, ServerValidateEventArgs args)
        {
            int month = int.Parse(ddlExpMonth.SelectedValue);
            int year = int.Parse(ddlExpYear.SelectedValue);

            DateTime selectedDate = new DateTime(year, month, DateTime.Now.Day).Date;

            args.IsValid = selectedDate > DateTime.Now.Date;
        }

        private void CreditCardTypeDataBindingHandler(object sender, EventArgs e)
        {
            ddlCcType.DataSource = new string[] {
                string.Empty, VISA_CC_TYPE, MASTER_CC_TYPE, DISCOVER_CC_TYPE, AMEX_CC_TYPE
            };
        }

        private void CreditCardTypeChangedHandler(object sender, EventArgs e)
        {
            bool isNotDiscorver = ddlCcType.SelectedValue != DISCOVER_CC_TYPE;
            bool isAmex = ddlCcType.SelectedValue == AMEX_CC_TYPE;

            lblSecurityCode.Visible = isNotDiscorver;
            txtSecurityCode.Visible = isNotDiscorver;
            vldRfSecurityCode.Visible = vldReSecurityCode.Visible = isNotDiscorver;

            txtSecurityCode.MaxLength = isAmex ? 4 : 3;
            vldReSecurityCode.ValidationExpression = string.Format(
                "\\d{{{0}}}", txtSecurityCode.MaxLength);
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
                    payment.BRouteNumber = ddlCcType.SelectedValue;
                    payment.BAccNumber = txtCcNumber.Text;
                    payment.DLStateNumber = payment.BillingState;
                    payment.ExpirationMonthYear = ddlExpMonth.SelectedValue
                        + ddlExpYear.SelectedValue;
                    payment.CVV2 = txtSecurityCode.Text;
                }
            } else {
                ddlCcType.SelectedValue = payment.BRouteNumber;
                txtCcNumber.Text = "************" + payment.BAccNumber.Substring(
                    payment.BAccNumber.Length - 4);
                ddlExpMonth.SelectedValue = payment.ExpirationMonthYear.Substring(0, 2);
                ddlExpYear.SelectedValue = payment.ExpirationMonthYear.Substring(
                    payment.ExpirationMonthYear.Length - 4);
                txtSecurityCode.Text = payment.CVV2;
            }
        }

        #endregion
    }
}