using System;
using System.Text;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Controls
{
	public class BaseLoginPage : BasePage
	{
        protected const string PHONE_NUMBER_PARAMETER = "PhoneNumber";
        protected const string ACCOUNT_NUMBER_PARAMETER = "AccountNumber";
        protected const string PASSWORD_PARAMETER = "Password";

        protected AuthenticationResult Authenticate(IMap imap, string accountNumber, string phoneNumber, string password) 
        {
            AuthenticationResult result;

            if (phoneNumber.Length > 0) {
                result = LoginSvc.AuthenticateHomePhoneAccount(imap, phoneNumber, password);
            } else {
                result = LoginSvc.AuthenticateHomePhoneAccount(imap, int.Parse(accountNumber), password);
            }

            if (result.Status == AuthenticationStatus.Failed) {
                if (phoneNumber.Length > 0) {
                    result = LoginSvc.AuthenticateWirelessAccount(imap, phoneNumber, password);
                }
            }

            return result;
        }

        protected string GetUIMessage(AuthenticationStatus authenticationStatus, bool isPhoneNumberUsed) 
        {
            switch (authenticationStatus) {
                case AuthenticationStatus.NotSignUp:
                    return "Please select the link Web Access Sign Up to setup access for your account";
                case AuthenticationStatus.Failed:
                    if (isPhoneNumberUsed) {
                        return "Phone Number or Password entered is invalid";
                    }
                    
                    return "Account Number or Password entered is invalid";
            }

            return string.Empty;
        }

        protected string EncodePassword(string password)
        {
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            string encodedPassword = Convert.ToBase64String(passwordBytes);
            
            return encodedPassword;
        }

        protected string DecodePassword(string encodedPassword)
        {
            byte[] passwordBytes = Convert.FromBase64String(encodedPassword);
            string password = Encoding.ASCII.GetString(passwordBytes);

            return password;
        }
	}
}
