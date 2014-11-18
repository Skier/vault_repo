using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Services;

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
        protected System.Web.UI.WebControls.Label lblSecurityCode;
        protected System.Web.UI.WebControls.Label lblExpDate;
        protected System.Web.UI.WebControls.Label lblExpDateSeparator;
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
            this.ddlCcType.DataBinding += new System.EventHandler(this.CreditCardTypeDataBindingHandler);
            this.ddlCcType.SelectedIndexChanged += new System.EventHandler(this.CreditCardTypeChangedHandler);
            this.ddlExpMonth.DataBinding += new System.EventHandler(this.ExpMonthDataBindingHandler);
            this.ddlExpYear.DataBinding += new System.EventHandler(this.ExpYearDataBindingHandler);
            this.vldCstExpDate.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.ExpirationDateValidationHandler);
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

                Controller controller = Controller.Instance;

                if (!controller.IsRecurringPaymentCreateMode) {
                    ICustomerRecurringPayment payment = Finder.FindRecurringPayment(
                        controller.RecurringPaymentId);

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

                // Save the flag in view state of the page.
                IsCreateMode = controller.IsRecurringPaymentCreateMode;
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

            Controller controller = Controller.Instance;

            ICustomerRecurringPayment payment;

            if (IsCreateMode) {
                payment = CustSvc.GetCustROP(controller.Map);

                payment.DateInserted = DateTime.Now;
                payment.UserId = "DPI Central";
                payment.AccNumber = Controller.Instance.AccountNumber;
                payment.AccountTypeId = (int) PaymentType.Credit;
            } else {
                payment = Finder.FindRecurringPayment(controller.RecurringPaymentId);
            }

            Update(ref payment, UpdateDirection.FromPage);

            try {
                CustSvc.PreSave(controller.Map);
                controller.ClearPayments();
                Utilities.SendRecurringPaymentConfirmation(payment.EmailAddress);
                controller.SwitchToRecurringPaymentManager();
            } catch (Exception ex) {
                vldCustErrorMsg.IsValid = false;
                vldCustErrorMsg.ErrorMessage = ex.Message;
            }
        }

        private void CancelHandler(object sender, ImageClickEventArgs e)
        {
            Controller.Instance.SwitchToRecurringPaymentManager();
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

        private void CreditCardTypeChangedHandler(object sender, System.EventArgs e) 
        {
            bool isNotDiscorver = ddlCcType.SelectedValue != DISCOVER_CC_TYPE;

            lblSecurityCode.Visible = isNotDiscorver;
            txtSecurityCode.Visible = isNotDiscorver;
            vldRfSecurityCode.Visible = vldReSecurityCode.Visible = isNotDiscorver;
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

                if (IsCreateMode) {
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

        #region Properties

        bool IsCreateMode
        {
            get 
            { 
                object value = ViewState["CREATE_MODE_KEY"];
                if (value == null) {
                    return false;
                }

                return (bool)value;
            }

            set { ViewState["CREATE_MODE_KEY"] = value; }
        }

        #endregion
    }
}