using System;
using System.Globalization;
using System.Web;
using System.Web.Security;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Controllers
{
    public class LoginController : ControllerBase
    {
        #region Constants

        private const string NEW_ACCOUNT_EMAIL_FROM = "customerservice@dpiteleconnect.com";
        private const string NEW_ACCOUNT_EMAIL_SUBJECT = "dPi Teleconnect Web Access";

        private const string NEW_ACCOUNT_EMAIL_BODY =
            @"
Hello {0}. 

This email confirms that you have successfully setup web access to your dPi Teleconnect account. 

You can access your account information such as due date for payment and exact amount you owe. 
In addition, you can now print a copy of your current bill for review. 

During account registration, we require that you submit a valid email address.  dPi Teleconnect will 
not disclose your email address to any third party.  This email address will only be used to let you 
know about important account information, special offers from dPi Teleconnect, and to notify you of 
web access activity on your account. 

If you have any questions, please call Customer Service at: 1-800-350-4009. 

Thank you. 

dPi Teleconnect Customer Service 
email: customerservice@dpiteleconnect.com
web: www.dpiteleconnect.com 
phone: 1-800-350-4009";

        #endregion Constants

        #region Static Members

        private static LoginController _instance;

        public static LoginController Instance {
            get {
                lock (typeof (LoginController)) {
                    if (_instance == null) {
                        _instance = new LoginController();
                    }
                }

                return _instance;
            }
        }

        #endregion

        protected LoginController() : base() {
        }

        public void Login(string accountNumber, string phoneNumber, string password) {
            Logout(false);

            try {
                if (phoneNumber != string.Empty) {
                    ulong.Parse(phoneNumber);
                }
            } catch {
                throw new ArgumentException("Phone Number is invalid.");
            }

            WebValidationResult validationResult;

            if (phoneNumber.Length > 0) {
                validationResult = CustSvc.ValidateWebAccessByPhNumber(
                    Map, phoneNumber, password);
            } else if (accountNumber.Length > 0) {
                validationResult = CustSvc.ValidateWebAccessByAccountNumber(
                    Map, int.Parse(accountNumber), password);
            } else {
                throw new ArgumentException("Error: Please enter Phone Number or Account Number");
            }

            if (validationResult.Status == WebValidationResultStatus.ValidCustomer) {
                accountNumber = validationResult.AccNumber.ToString(CultureInfo.InvariantCulture);
                CreateFormAuthenticationTicket(accountNumber);
                if (validationResult.IsPasswordTemporal) {
                    HttpContext.Current.Response.Redirect(UrlDictionary.CHANGE_PASSWORD_URL);
                } else {
                    string redirectUrl = FormsAuthentication.GetRedirectUrl(accountNumber, true);
                    if (!redirectUrl.ToUpper().EndsWith("DEFAULT.ASPX")) {
                        FormsAuthentication.RedirectFromLoginPage(accountNumber, true);
                    } else {
                        HttpContext.Current.Response.Redirect(UrlDictionary.ACCOUNT_SUMMARY_URL);
                    }
                }
            } else if (validationResult.Status == WebValidationResultStatus.CustomerNotSetupYet) {
                throw new ApplicationException("Please Sign Up to access your account for the first time");
            } else if (validationResult.Status == WebValidationResultStatus.InvalidCustomer) {
                if (phoneNumber.Length > 0) {
                    throw new ApplicationException("Invalid Phone Number or Password");
                } else {
                    throw new ApplicationException("Invalid Account Number or Password");
                }
            }
        }

        public void Logout() {
            Logout(true);
        }

        public void Logout(bool redirect) {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Abandon();
            if (redirect) {
                HttpContext.Current.Response.Redirect(UrlDictionary.ACCOUNT_SUMMARY_URL);
            }
        }

        public void SignUp(int accNumber, string lastName, string email, string password) {
            Logout(false);

            WebValidationResult result;
            result = CustSvc.ValidateWebAccessByAccountNumber(Map, accNumber);

            switch (result.Status) {
                case WebValidationResultStatus.InvalidCustomer:
                    throw new ApplicationException("Error: Account number is invalid");
                case WebValidationResultStatus.ValidCustomer:
                    throw new ApplicationException(
                        "Account already has web access. Please logon or use password reminder");
                case WebValidationResultStatus.CustomerNotSetupYet:
                    CustSvc.EnableWebAccess(Map, accNumber, lastName, email, password);
                    SendConfirmation(email, lastName);
                    Login(accNumber.ToString(), string.Empty, password);
                    break;
            }
        }

        public IAcctInfo RemindPassword(int accountNumber) {
            Logout(false);

            WebValidationResultStatus status;
            status = CustSvc.RemindWebPassword(Map, accountNumber);

            switch (status) {
                case WebValidationResultStatus.ValidCustomer:
                    return CustSvc.GetAcctInfo(Map, accountNumber);
                case WebValidationResultStatus.CustomerNotSetupYet:
                    throw new ApplicationException("Error: Please sing up for web access");
                case WebValidationResultStatus.InvalidCustomer:
                    throw new ApplicationException("Error: Invalid Account Number");
                default:
                    throw new ApplicationException("Error: Unknown validation status: " + status);
            }
        }

        private void CreateFormAuthenticationTicket(string accountNumber) {
            // Create the authentication ticket and store the roles in the
            // custom UserData property of the authentication ticket.
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                1,
                accountNumber,
                DateTime.Now,
                DateTime.Now.AddMinutes(20),
                false,
                string.Empty);

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            // Create a cookie and add the encrypted 
            // ticket to the cookie as data.
            HttpCookie authCookie = new HttpCookie(
                FormsAuthentication.FormsCookieName, encryptedTicket);

            // Add the cookie to the outgoing cookies collection.
            HttpContext.Current.Response.Cookies.Add(authCookie);
        }

        private void SendConfirmation(string toAddress, string name) {
            MailMessage msg = new MailMessage();

            msg.AddEmailTo(toAddress);
            msg.EmailFrom = NEW_ACCOUNT_EMAIL_FROM;
            msg.EmailSubject = NEW_ACCOUNT_EMAIL_SUBJECT;
            msg.EmailMessage = string.Format(NEW_ACCOUNT_EMAIL_BODY, CustInfo.CapitalizeName(name));

            msg.SendMail();
        }
    }
}