using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Account;
using Dpi.Central.Web.Controls;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web
{
    public class SignupPage : BaseImageAccountPage
    {
        #region Constants

        private const string SERVICE_TYPE_IS_NOT_SELECTED = "Please select your service type";
        private const string INVALID_ACCOUNT_NUMBER = "The Account Number provided is invalid. Please verify and enter a valid Account Number";
        private const string ALREADY_SETUP_WITH_WEB_ACCESS = "The Account Number entered is already setup with web access. Please access the Account Login page to log into your account. If you have forgotten your password, on the Account Login page, click on the link “Forgot My Password”";
        private const string LAST_NAME_DOES_NOT_MATCH = "The Account Last Name provided is invalid. Please verify and enter the Last Name on the Account";
        private const string SUCCESS_SIGN_UP = "You have been signed up successfully for web access to your wireless account information.<br>&nbsp;&nbsp;&nbsp;Your password has been sent to your cell phone.";
        private const string INVALID_PHONE_NUMBER = "The Phone Number provided is invalid. Please verify and enter a valid Phone Number";
        private const string ALREADY_SETUP_WITH_WIRELESS_WEB_ACCESS = "The Phone Number entered is already setup with web access. Please access the Wireless Account Login page<br>&nbsp;&nbsp;&nbsp;to log into your account. If you have forgotten your password, on the Wireless Account Login page, click on the<br>&nbsp;&nbsp;&nbsp;link “Forgot My Password”";

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
        protected PhoneNumberBox phnPhoneNumber;
        protected CustomValidator vldCstPhoneNumber;
        protected CustomValidator vldCstIdentity;
        protected HtmlGenericControl divButtonsRow;
        protected RadioButton rbtnWireless;
        protected RadioButton rbtnOrdinary;
        protected HtmlGenericControl divOrdinarySignUpRow;
        protected HtmlGenericControl divWirelessSignUpRow;
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
            this.vldCstIdentity.ServerValidate += new ServerValidateEventHandler(this.vldCstIdentity_ServerValidate);
            this.vldCstPhoneNumber.ServerValidate += new ServerValidateEventHandler(this.vldCstPhoneNumber_ServerValidate);
            this.btnSubmit.Click += new ImageClickEventHandler(this.SubmitHandler);
            this.Load += new EventHandler(this.Page_Load);

        }

        #endregion

        #region Event Handlers

        private void Page_Load(object sender, EventArgs e)
        {
            rbtnWireless.Attributes.Add("onclick", "ShowWirelessSignUp();");
            rbtnOrdinary.Attributes.Add("onclick", "ShowOrdinarySignUp();");

            if (rbtnWireless.Checked) {
                divWirelessSignUpRow.Style.Add("display", "visible");
                divOrdinarySignUpRow.Style.Add("display", "none");
                divButtonsRow.Style.Add("display", "visible");

                vldCstPhoneNumber.Enabled = true;
                vldRfAccountNumber.Enabled = false;
                vldRfAccountLastName.Enabled = false;
                vldRfEmail.Enabled = false;
                vldRfPassword.Enabled = false;
                vldRfConfirmPassword.Enabled = false;
                vldReAccountNumber.Enabled = false;
                vldReEmail.Enabled = false;
                vldRePassword.Enabled = false;
                vldCmpConfirmPassword.Enabled = false;
            } else if (rbtnOrdinary.Checked) {
                divWirelessSignUpRow.Style.Add("display", "none");
                divOrdinarySignUpRow.Style.Add("display", "visible");
                divButtonsRow.Style.Add("display", "visible");

                vldCstPhoneNumber.Enabled = false;
                vldRfAccountNumber.Enabled = true;
                vldRfAccountLastName.Enabled = true;
                vldRfEmail.Enabled = true;
                vldRfPassword.Enabled = true;
                vldRfConfirmPassword.Enabled = true;
                vldReAccountNumber.Enabled = true;
                vldReEmail.Enabled = true;
                vldRePassword.Enabled = true;
                vldCmpConfirmPassword.Enabled = true;
            } else {
                divWirelessSignUpRow.Style.Add("display", "none");
                divOrdinarySignUpRow.Style.Add("display", "none");
                divButtonsRow.Style.Add("display", "none");

                vldCstPhoneNumber.Enabled = false;
                vldRfAccountNumber.Enabled = false;
                vldRfAccountLastName.Enabled = false;
                vldRfEmail.Enabled = false;
                vldRfPassword.Enabled = false;
                vldRfConfirmPassword.Enabled = false;
                vldReAccountNumber.Enabled = false;
                vldReEmail.Enabled = false;
                vldRePassword.Enabled = false;
                vldCmpConfirmPassword.Enabled = false;
            }
        }

        private void vldCstIdentity_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!rbtnWireless.Checked && !rbtnOrdinary.Checked) {
                args.IsValid = false;
                SetErrorMessage(SERVICE_TYPE_IS_NOT_SELECTED);
            }
        }

        private void vldCstPhoneNumber_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (phnPhoneNumber.PhoneNumber == string.Empty) {
                args.IsValid = false;
                vldCstPhoneNumber.ErrorMessage = "<br>Required field cannot be left blank";
            } else {
                args.IsValid = phnPhoneNumber.IsValid;
                vldCstPhoneNumber.ErrorMessage = "<br>The Phone Number provided is invalid";
            }
        }

        private void SubmitHandler(object sender, ImageClickEventArgs e)
        {
            if (!Page.IsValid) {
                return;
            }

            try {
                if (rbtnWireless.Checked) {
                    SignUpWirelessPhoneService(phnPhoneNumber.PhoneNumber);
                } else if (rbtnOrdinary.Checked) {
                    SignUpOrdinaryPhoneService(Int32.Parse(txtAccountNumber.Text.Trim()), txtAccountLastName.Text.Trim(), txtEmail.Text.Trim(), txtPassword.Text.Trim());
                }
            } catch (Exception ex) {
                SetErrorMessage(ex);
            }
        }

        #endregion

        #region Private Methods

        private void SignUpOrdinaryPhoneService(int accNumber, string lastName, string email, string password)
        {
            WebValidationResult webAccessValidationResult = CustSvc.ValidateWebAccessByAccountNumber(Map, accNumber);

            switch (webAccessValidationResult.Status) {
                case WebValidationResultStatus.CustomerNotSetupYet:
                    EnableWebAccessStatus signUpResult = CustSvc.EnableWebAccess(Map, accNumber, lastName, email, password);

                    switch (signUpResult) {
                        case EnableWebAccessStatus.Success:
                            IAcctInfo acctInfo = CustSvc.GetAcctInfo(Map, accNumber);
                            EmailSender.SendNewAccountNotification(email, NameFormatter.Format(acctInfo));
                            AuthenticationHelper.SetAuthCookie(accNumber.ToString(), new string[] {AuthenticationHelper.HOME_PHONE_ACCOUNT}, false);

                            HttpContext.Current.Response.Redirect(SiteMap.ACCOUNT_SUMMARY_URL);
                            break;
                        case EnableWebAccessStatus.AccountNumberInvalid:
                            SetErrorMessage(INVALID_ACCOUNT_NUMBER);
                            break;
                        case EnableWebAccessStatus.LastNameDoesNotMatch:
                            SetErrorMessage(LAST_NAME_DOES_NOT_MATCH);
                            break;
                    }
                    break;
                case WebValidationResultStatus.InvalidCustomer:
                    SetErrorMessage(INVALID_ACCOUNT_NUMBER);
                    break;
                case WebValidationResultStatus.ValidCustomer:
                    SetErrorMessage(ALREADY_SETUP_WITH_WEB_ACCESS);
                    break;
            }
        }

        private void SignUpWirelessPhoneService(string phoneNumber) 
        {
            WebValidationResult webAccessValidationResult = CustSvc.ValidateWirelessWebAccessByPhoneNumber(Map, phoneNumber);

            switch (webAccessValidationResult.Status) {
                case WebValidationResultStatus.CustomerNotSetupYet:
                    EnableWebAccessStatus signUpResult = CustSvc.EnableWirelessWebAccess(Map, phoneNumber);

                    switch (signUpResult) {
                        case EnableWebAccessStatus.Success:
                            IWireless_Custdata[] acctInfo = CustSvc.GetWirelessCustDataByPhone(Map, phoneNumber);
                            if (acctInfo.Length == 0) {
                                throw new ApplicationException("Retrieving wireless account information faild. Phone number is " + phoneNumber + ".");
                            }
                            EmailSender.SendWirelessSignUpNotification(acctInfo[0].Email, NameFormatter.Format(acctInfo[0].NameFirst, acctInfo[0].NameLast), acctInfo[0].WebPassword);
                            SetErrorMessage(SUCCESS_SIGN_UP);
                            break;
                        case EnableWebAccessStatus.AccountNumberInvalid:
                            SetErrorMessage(INVALID_PHONE_NUMBER);
                            break;
                        case EnableWebAccessStatus.LastNameDoesNotMatch:
                            throw new ApplicationException("Invalid value for the enabling wireless web access method.");
                    }
                    break;
                case WebValidationResultStatus.InvalidCustomer:
                    SetErrorMessage(INVALID_PHONE_NUMBER);
                    break;
                case WebValidationResultStatus.ValidCustomer:
                    SetErrorMessage(ALREADY_SETUP_WITH_WIRELESS_WEB_ACCESS);
                    break;
            }
        }

        #endregion
    }
}