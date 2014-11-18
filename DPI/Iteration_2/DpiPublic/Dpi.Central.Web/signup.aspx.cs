using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Account;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web
{
    public class SignupPage : BaseAccountPage
    {
        #region Constants

        private const string INVALID_ACCOUNT_NUMBER =
            "The Account Number provided is invalid. Please verify and enter a valid Account Number";

        #endregion

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
        protected Label lblAccountLastName;
        protected TextBox txtAccountLastName;
        protected RequiredFieldValidator vldRfAccountLastName;
        protected System.Web.UI.WebControls.HyperLink lnkSummary1;
        protected System.Web.UI.WebControls.LinkButton lnkSummary;
        protected System.Web.UI.WebControls.HyperLink lnkLogin;
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
            this.lnkSummary.Click += new System.EventHandler(this.lnkSummary_Click);

        }

        #endregion

        #region Event Handlers

        private void SubmitHandler(object sender, ImageClickEventArgs e)
        {
            if (!Page.IsValid) {
                return;
            }

            string accountNumber = txtAccountNumber.Text.Trim();
            string lastName = txtAccountLastName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string passwordConf = txtConfirmPassword.Text.Trim();

            if (accountNumber == null ||
                accountNumber.Length == 0 ||
                !Regex.IsMatch(accountNumber, "^\\d{8}$")) {
                ShowError(INVALID_ACCOUNT_NUMBER);
                return;
            }

            if (lastName == null || lastName.Length == 0) {
                ShowError("Please enter the Last Name on the Account ");
                return;
            }

            if (email == null || email.Length == 0 ||
                !Regex.IsMatch(email, "\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*")) {
                ShowError("The Email address provided is invalid. Please verify and enter a valid Email address");
                return;
            }
            if (password == null || password.Length == 0) {
                ShowError("Please enter a Password");
                return;
            } else if (password.Length < ChangePasswordPage.MIN_PASSWORD_LENGTH) {
                ShowError(
                    string.Format(
                        "Password must have at least {0} characters", ChangePasswordPage.MIN_PASSWORD_LENGTH));
                return;
            } else if (password.Length > ChangePasswordPage.MAX_PASSWORD_LENGTH) {
                ShowError(
                    string.Format(
                        "Password cannot be longer then {0} characters", ChangePasswordPage.MAX_PASSWORD_LENGTH));
                return;
            }

            if (passwordConf == null || passwordConf.Length == 0) {
                ShowError("Password Confirmation is required");
                return;
            }

            if (!passwordConf.Equals(password)) {
                ShowError("Password and Confirm Password must match. Please verify the Passwords");
                return;
            }

            try {
                SingUpStatus status = SignUp(
                    int.Parse(accountNumber),
                    lastName,
                    email,
                    password);

                switch (status) {
                    case SingUpStatus.InvalidCustomer:
                        ShowError(INVALID_ACCOUNT_NUMBER);
                        break;
                    case SingUpStatus.ValidCustomer:
                        ShowError(
                            "The Account Number entered is already setup with web access. Please access the Account Login page to log into your account. If you have forgotten your password, on the Account Login page, click on the link “Forgot My Password”");
                        CreateFormAuthenticationTicket(accountNumber);
                        lnkSummary.Visible = true;
                        break;
                    case SingUpStatus.LastNameDoesNotMatch:
                        ShowError(
                            "The Account Last Name provided is invalid. Please verify and enter the Last Name on the Account");
                        break;
                }
            } catch (Exception ex) {
                ShowMessage("Error: " + ex.Message);
            }
        }

        #endregion

        #region Private Methods

        private enum SingUpStatus
        {
            Success,
            InvalidCustomer,
            ValidCustomer,
            LastNameDoesNotMatch
        }

        private SingUpStatus SignUp(int accNumber, string lastName, string email, string password)
        {
            // TODO:  Need to optimize it.  Right now going 2 times to db.  First to find it it is sing up already,
            // second to enable access.

            Logout(false);

            SingUpStatus result = SingUpStatus.Success;

            WebValidationResult res1;
            res1 = CustSvc.ValidateWebAccessByAccountNumber(Map, accNumber);
            switch (res1.Status) {
                case WebValidationResultStatus.CustomerNotSetupYet:
                    EnableWebAccessStatus res2 = CustSvc.EnableWebAccess(Map, accNumber, lastName, email, password);
                    switch (res2) {
                        case EnableWebAccessStatus.Success:
                            IAcctInfo acctInfo = CustSvc.GetAcctInfo(Map, accNumber);
                            EmailSender.SendNewAccountNotification(email, acctInfo.FirstName);
                            CreateFormAuthenticationTicket(accNumber.ToString());
                            HttpContext.Current.Response.Redirect(UrlDictionary.ACCOUNT_SUMMARY_URL);
                            result = SingUpStatus.Success;
                            break;

                        case EnableWebAccessStatus.AccountNumberInvalid:
                            result = SingUpStatus.InvalidCustomer;
                            break;
                        case EnableWebAccessStatus.LastNameDoesNotMatch:
                            result = SingUpStatus.LastNameDoesNotMatch;
                            break;
                    }
                    break;
                case WebValidationResultStatus.InvalidCustomer:
                    result = SingUpStatus.InvalidCustomer;
                    break;
                case WebValidationResultStatus.ValidCustomer:
                    result = SingUpStatus.ValidCustomer;
                    break;
            }

            return result;
        }

        private void ShowError(string message)
        {
            ShowMessage("Error: " + message);
        }

        private void ShowMessage(string message)
        {
            vldCustErrorMsg.IsValid = false;
            vldCustErrorMsg.ErrorMessage = message;
        }

        #endregion

        private void lnkSummary_Click(object sender, System.EventArgs e) {
            Response.Redirect(UrlDictionary.ACCOUNT_SUMMARY_URL);
        }
    }
}