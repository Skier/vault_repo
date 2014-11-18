using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPI.Services;
using TextBox=Dpi.Central.Web.Controls.TextBox;

namespace Dpi.Central.Web.Account
{
    public class LoginPage : BaseAccountPage
    {
        #region Web Form Designer generated code

        protected Label lblPhoneNumber;
        protected Label lblAccountNumber;
        protected TextBox txtAccountNumber;
        protected Label lblPassword;
        protected TextBox txtPassword;
        protected RegularExpressionValidator anRegExpValidator;
        protected RequiredFieldValidator pwdReqFldValidator;
        protected TextBox txtNpa;
        protected Label lblOr;
        protected TextBox txtNxx;
        protected TextBox txtNumber;
        protected ValidationSummary vldSummary;
        protected CustomValidator vldCustErrorMsg;
        protected HyperLink lnkForgotPwd;
        protected HyperLink lnkSignUp;
        protected ImageButton btnSubmit;

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
            this.btnSubmit.Click += new System.Web.UI.ImageClickEventHandler(this._submitButton_Click);
        }

        #endregion

        #region EventHandlers

        private void _submitButton_Click(object sender, ImageClickEventArgs e)
        {
            if (!Page.IsValid) {
                return;
            }

            try {
                LoginResult result = Login(
                    txtAccountNumber.Text.Trim(),
                    txtNpa.Text.Trim() + txtNxx.Text.Trim() + txtNumber.Text.Trim(),
                    txtPassword.Text.Trim());

                vldCustErrorMsg.IsValid = false;

                switch (result) {
                    case LoginResult.MissingBothPhoneNumberAndAccountNumber:
                        vldCustErrorMsg.ErrorMessage = "Please enter a Phone Number or Account Number";
                        break;
                    case LoginResult.MissingPassword:
                        vldCustErrorMsg.ErrorMessage = "Please enter a Password";
                        break;
                    case LoginResult.PhoneNumberNotFormattedCorrectly:
                        vldCustErrorMsg.ErrorMessage = "Phone Number entered is invalid. Please verify and enter a valid Phone Number";
                        break;
                    case LoginResult.AccountNumberFormattedIncorrectly:
                        vldCustErrorMsg.ErrorMessage = "Account Number is invalid. Please verify and enter a valid Account Number";
                        break;
                    case LoginResult.MissingPhoneNumberOrAccNumber:
                        vldCustErrorMsg.ErrorMessage = "Please enter Phone Number or Account Number";
                        break;
                    case LoginResult.CustomerNotSetupYet:
                        vldCustErrorMsg.ErrorMessage = "Please select the link Web Access Sign Up to setup access for your account";
                        break;
                    case LoginResult.InvalidPhoneNumberOrPassowrd:
                        vldCustErrorMsg.ErrorMessage = "Phone Number or Password entered is invalid";
                        break;
                    case LoginResult.InvalidAccountNumberOrPassword:
                        vldCustErrorMsg.ErrorMessage = "Account Number or Password entered is invalid";
                        break;
                }
            } catch (Exception ex) {
                // This is only for unexpected.  No business logic should end up here.
                vldCustErrorMsg.IsValid = false;
                vldCustErrorMsg.ErrorMessage = "Error: " + ex.Message;
            }
        }

        #endregion

        #region Implementations 

        private enum LoginResult
        {
            Success,
            MissingPhoneNumberOrAccNumber,
            CustomerNotSetupYet,
            InvalidAccountNumberOrPassword,
            InvalidPhoneNumberOrPassowrd,
            PhoneNumberNotFormattedCorrectly,
            AccountNumberFormattedIncorrectly,
            MissingBothPhoneNumberAndAccountNumber,
            MissingPassword
        }

        private LoginResult Login(string accountNumber, string phoneNumber, string password)
        {
            LoginResult result = LoginResult.Success;

            Logout(false);

            if ((accountNumber == null || accountNumber.Length == 0) && (phoneNumber == null || phoneNumber.Length == 0)) {
                result = LoginResult.MissingBothPhoneNumberAndAccountNumber;
                return result;
            }

            if (phoneNumber != null && phoneNumber.Length > 0 && (!Regex.IsMatch(phoneNumber, "^\\d{10}$"))) {
                result = LoginResult.PhoneNumberNotFormattedCorrectly;
                return result;
            }

            if (accountNumber != null && accountNumber.Length > 0 && (!Regex.IsMatch(accountNumber, "^\\d{8}$"))) {
                result = LoginResult.AccountNumberFormattedIncorrectly;
                return result;
            }

            if (password == null || password.Length == 0) {
                result = LoginResult.MissingPassword;
                return result;
            }

            WebValidationResult validationResult;

            if (phoneNumber.Length > 0) {
                validationResult = CustSvc.ValidateWebAccessByPhNumber(
                    Map, phoneNumber, password);
            } else if (accountNumber.Length > 0) {
                validationResult = CustSvc.ValidateWebAccessByAccountNumber(
                    Map, int.Parse(accountNumber), password);
            } else {
                result = LoginResult.MissingPhoneNumberOrAccNumber;
                return result;
            }

            if (validationResult.Status == WebValidationResultStatus.ValidCustomer) {
                accountNumber = validationResult.AccNumber.ToString(CultureInfo.InvariantCulture);
                CreateFormAuthenticationTicket(accountNumber);
                if (validationResult.IsPasswordTemporal) {
                    HttpContext.Current.Response.Redirect(SiteMap.CHANGE_PASSWORD_URL);
                } else {
                    string redirectUrl = FormsAuthentication.GetRedirectUrl(accountNumber, true);
                    if (!redirectUrl.ToUpper().EndsWith("DEFAULT.ASPX")) {
                        FormsAuthentication.RedirectFromLoginPage(accountNumber, true);
                    } else {
                        HttpContext.Current.Response.Redirect(SiteMap.ACCOUNT_SUMMARY_URL);
                    }
                }
            } else if (validationResult.Status == WebValidationResultStatus.CustomerNotSetupYet) {
                result = LoginResult.CustomerNotSetupYet;
            } else if (validationResult.Status == WebValidationResultStatus.InvalidCustomer) {
                if (phoneNumber.Length > 0) {
                    result = LoginResult.InvalidPhoneNumberOrPassowrd;
                } else {
                    result = LoginResult.InvalidAccountNumberOrPassword;
                }
            }
            return result;
        }

        #endregion
    }
}