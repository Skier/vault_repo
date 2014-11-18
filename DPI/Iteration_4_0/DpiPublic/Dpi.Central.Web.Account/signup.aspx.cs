using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Account;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web
{
    public class SignupPage : BaseImageAccountPage
    {
        #region Constants

        private const string INVALID_ACCOUNT_NUMBER = "The Account Number provided is invalid. Please verify and enter a valid Account Number";
        private const string ALREADY_SETUP_WITH_WEB_ACCESS = "The Account Number entered is already setup with web access. Please access the Account Login page to log into your account. If you have forgotten your password, on the Account Login page, click on the link “Forgot My Password”";
        private const string LAST_NAME_DOES_NOT_MATCH = "The Account Last Name provided is invalid. Please verify and enter the Last Name on the Account";

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
        protected Label lblAccountLastName;
        protected TextBox txtAccountLastName;
        protected HyperLink lnkLogin;
        protected RequiredFieldValidator vldRfAccountNumber;
        protected RequiredFieldValidator vldRfAccountLastName;
        protected RequiredFieldValidator vldRfEmail;
        protected RequiredFieldValidator vldRfPassword;
        protected RequiredFieldValidator vldRfConfirmPassword;
        protected RegularExpressionValidator vldReAccountNumber;
        protected RegularExpressionValidator vldReEmail;
        protected RegularExpressionValidator vldRePassword;
        protected CompareValidator vldCmpConfirmPassword;
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

        #region Event Handlers

        private void SubmitHandler(object sender, ImageClickEventArgs e)
        {
            if (!Page.IsValid) {
                return;
            }

            try {
                SingUpStatus status = SignUp(Int32.Parse(txtAccountNumber.Text.Trim()), txtAccountLastName.Text.Trim(), txtEmail.Text.Trim(), txtPassword.Text.Trim());

                switch (status) {
                    case SingUpStatus.InvalidCustomer:
                        SetErrorMessage(INVALID_ACCOUNT_NUMBER);
                        break;
                    case SingUpStatus.ValidCustomer:
                        SetErrorMessage(ALREADY_SETUP_WITH_WEB_ACCESS);
                        break;
                    case SingUpStatus.LastNameDoesNotMatch:
                        SetErrorMessage(LAST_NAME_DOES_NOT_MATCH);
                        break;
                }
            } catch (Exception ex) {
                SetErrorMessage(ex);
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

            SingUpStatus result = SingUpStatus.Success;

            WebValidationResult res1;
            res1 = CustSvc.ValidateWebAccessByAccountNumber(Map, accNumber);
            switch (res1.Status) {
                case WebValidationResultStatus.CustomerNotSetupYet:
                    EnableWebAccessStatus res2 = CustSvc.EnableWebAccess(Map, accNumber, lastName, email, password);
                    switch (res2) {
                        case EnableWebAccessStatus.Success:
                            IAcctInfo acctInfo = CustSvc.GetAcctInfo(Map, accNumber);
                            EmailSender.SendNewAccountNotification(email, NameFormatter.Format(acctInfo));
                            AuthenticationHelper.SetAuthCookie(accNumber.ToString(), false);
                            HttpContext.Current.Response.Redirect(SiteMap.ACCOUNT_SUMMARY_URL);
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

        #endregion
    }
}