using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPI.Services;

namespace Dpi.Central.Web
{
    public class SignupPage : Page
    {
        #region Web Form Designer generated code

        protected Label lblAccountNumber;
        protected Label lblEmail;
        protected Label lblPassword;
        protected TextBox txtAccountNumber;
        protected TextBox txtEmail;
        protected TextBox txtPassword;
        protected TextBox txtConfirmPassword;
        protected ImageButton btnSubmit;
        protected CustomValidator vldCustErrorMsg;
        protected ValidationSummary vldSummary;
        protected RequiredFieldValidator vldRfAccountNumber;
        protected RegularExpressionValidator vldReAccountNumber;
        protected RequiredFieldValidator vldRfEmail;
        protected RegularExpressionValidator vldReEmail;
        protected RequiredFieldValidator vldRfPassword;
        protected CompareValidator vldCmpConfirmPassword;
        protected System.Web.UI.WebControls.Label lblAccountLastName;
        protected System.Web.UI.WebControls.TextBox txtAccountLastName;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfAccountLastName;
        protected Label lblConfirmPassword;

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
            this.btnSubmit.Click += new System.Web.UI.ImageClickEventHandler(this.SubmitHandler);

        }

        #endregion

        private void SubmitHandler(object sender, ImageClickEventArgs e)
        {
            if (!Page.IsValid) {
                return;
            }

            WebValidationResult validationResult;

            int accNumber = int.Parse(txtAccountNumber.Text);
            validationResult = CustSvc.ValidateWebAccessByAccountNumber(
                Controller.Instance.Map, accNumber);

            switch (validationResult.Status) {
                case WebValidationResultStatus.InvalidCustomer:
                    vldCustErrorMsg.IsValid = false;
                    vldCustErrorMsg.ErrorMessage = "Error: Account number is invalid";
                    break;
                case WebValidationResultStatus.ValidCustomer:
                    vldCustErrorMsg.IsValid = false;
                    vldCustErrorMsg.ErrorMessage = "Error: Account already has web access. "
                        + "Please logon or use password reminder";
                    break;
                case WebValidationResultStatus.CustomerNotSetupYet:
                    try {
                        CustSvc.EnableWebAccess(
                            Controller.Instance.Map, accNumber, txtAccountLastName.Text.Trim(),
                            this.txtEmail.Text.Trim(), txtConfirmPassword.Text.Trim());
                    } catch (ArgumentException ex) {
                        vldCustErrorMsg.IsValid = false;
                        vldCustErrorMsg.ErrorMessage = ex.Message;
                        return;    
                    }

                    // Send email
                    Utilities.SendSingUpConfirmation(this.txtEmail.Text.Trim(), txtAccountLastName.Text.Trim());

                    AuthenticationProvider.Instance.Login(
                        txtAccountNumber.Text, string.Empty, txtPassword.Text);

                    break;
            }
        }
    }
}