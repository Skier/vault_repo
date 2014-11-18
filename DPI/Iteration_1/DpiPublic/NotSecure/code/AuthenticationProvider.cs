using System;
using System.Globalization;
using System.Web;
using System.Web.Security;
using DPI.Services;

namespace Dpi.Central.Web
{
    public class AuthenticationProvider
    {
        #region Static Members

        static AuthenticationProvider _instance;

        public static AuthenticationProvider Instance
        {
            get
            {
                lock (typeof(AuthenticationProvider)) {
                    if (_instance == null) {
                        _instance = new AuthenticationProvider();
                    }
                }

                return _instance;
            }
        }

        #endregion

        private AuthenticationProvider()
        {
        }

        public void Login(string accountNumber, string phoneNumber, string password)
        {
            Controller.Instance.ClearAll();

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
                    Controller.Instance.Map, phoneNumber, password);
            } else if (accountNumber.Length > 0) {
                validationResult = CustSvc.ValidateWebAccessByAccountNumber(
                    Controller.Instance.Map, int.Parse(accountNumber), password);
            } else {
                throw new ArgumentException("Error: Please enter Phone Number or Account Number");
            }

            if (validationResult.Status == WebValidationResultStatus.ValidCustomer) {
                accountNumber = validationResult.AccNumber.ToString(CultureInfo.InvariantCulture);
                CreateFormAuthenticationTicket(accountNumber);
                string redirectUrl = FormsAuthentication.GetRedirectUrl(accountNumber, true);
                if (!redirectUrl.ToUpper().EndsWith("DEFAULT.ASPX")) {
                    FormsAuthentication.RedirectFromLoginPage(accountNumber, true);
                } else {
                    Controller.Instance.SwitchToAccountSummary();
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

        public void Logout()
        {
            FormsAuthentication.SignOut();
            Controller.Instance.ClearAll();
            Controller.Instance.SwitchToAccountSummary();
        }

        private void CreateFormAuthenticationTicket(string accountNumber)
        {
            // Create the authentication ticket and store the roles in the
            // custom UserData property of the authentication ticket.
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                1, accountNumber, DateTime.Now, DateTime.Now.AddMinutes(20),
                false, string.Empty);

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            // Create a cookie and add the encrypted 
            // ticket to the cookie as data.
            HttpCookie authCookie = new HttpCookie(
                FormsAuthentication.FormsCookieName, encryptedTicket);

            // Add the cookie to the outgoing cookies collection.
            HttpContext.Current.Response.Cookies.Add(authCookie);
        }
    }
}