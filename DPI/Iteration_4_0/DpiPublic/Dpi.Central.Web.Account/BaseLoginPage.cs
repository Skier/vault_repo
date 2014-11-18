using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using DPI.Components;
using DPI.Services;

namespace Dpi.Central.Web.Account
{        
	public class BaseLoginPage : BaseAccountPage
	{	
        #region LoginResult

        protected enum LoginResult {
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

        #endregion
	    
	    #region Login

	    protected LoginResult Login(string accountNumber, string phoneNumber, string password)
	    {
	        if ((accountNumber == null || accountNumber.Length == 0) && (phoneNumber == null || phoneNumber.Length == 0)) {
	            return LoginResult.MissingBothPhoneNumberAndAccountNumber;
	        }

	        if (phoneNumber != null && phoneNumber.Length > 0 && (!Regex.IsMatch(phoneNumber, "^\\d{10}$"))) {
	            return LoginResult.PhoneNumberNotFormattedCorrectly;
	        }

	        if (accountNumber != null && accountNumber.Length > 0 && (!Regex.IsMatch(accountNumber, "^\\d{8}$"))) {
	            return LoginResult.AccountNumberFormattedIncorrectly;
	        }

	        if (password == null || password.Length == 0) {
	            return LoginResult.MissingPassword;
	        }

	        WebValidationResult validationResult;

	        if (phoneNumber.Length > 0) {
	            validationResult = CustSvc.ValidateWebAccessByPhNumber(Map, phoneNumber, password);
	        } else if (accountNumber.Length > 0) {
	            validationResult = CustSvc.ValidateWebAccessByAccountNumber(Map, int.Parse(accountNumber), password);
	        } else {
	            return LoginResult.MissingPhoneNumberOrAccNumber;
	        }

	        if (validationResult.Status == WebValidationResultStatus.ValidCustomer) {
	            accountNumber = validationResult.AccNumber.ToString(CultureInfo.InvariantCulture);
	            AuthenticationHelper.SetAuthCookie(accountNumber, false);
	            if (validationResult.IsPasswordTemporal) {
	                HttpContext.Current.Response.Redirect(SiteMap.CHANGE_PASSWORD_URL);
	            } else {
	                InsertCustomerWebLogEntry(int.Parse(accountNumber));
	                HttpContext.Current.Response.Redirect(SiteMap.ACCOUNT_SUMMARY_URL);
	            }
	        } else if (validationResult.Status == WebValidationResultStatus.CustomerNotSetupYet) {
	            return LoginResult.CustomerNotSetupYet;
	        } else if (validationResult.Status == WebValidationResultStatus.InvalidCustomer) {
	            if (phoneNumber.Length > 0) {
	                return LoginResult.InvalidPhoneNumberOrPassowrd;
	            } else {
	                return LoginResult.InvalidAccountNumberOrPassword;
	            }
	        }

	        return LoginResult.Success;
	    }
	    
        private void InsertCustomerWebLogEntry(int accountId) {
            
            UOW uow = new UOW(Map, "BaseLoginPage.Login()");

            try {
                CustomerWebLog webLog = new CustomerWebLog(uow);

                uow.BeginTransaction();
                webLog.AcctNumber = accountId;
                uow.commit();
            } finally {
                uow.close();
            }
        }	    

	    #endregion

	    #region GetUIMessage

	    protected string GetUIMessage(LoginResult loginResult)
	    {
	        switch (loginResult) {
	            case LoginResult.MissingBothPhoneNumberAndAccountNumber:
	                return "Please enter a Phone Number or Account Number";
	            case LoginResult.MissingPassword:
	                return "Please enter a Password";
	            case LoginResult.PhoneNumberNotFormattedCorrectly:
	                return "Phone Number entered is invalid. Please verify and enter a valid Phone Number";
	            case LoginResult.AccountNumberFormattedIncorrectly:
	                return "Account Number is invalid. Please verify and enter a valid Account Number";
	            case LoginResult.MissingPhoneNumberOrAccNumber:
	                return "Please enter Phone Number or Account Number";
	            case LoginResult.CustomerNotSetupYet:
	                return "Please select the link Web Access Sign Up to setup access for your account";
	            case LoginResult.InvalidPhoneNumberOrPassowrd:
	                return "Phone Number or Password entered is invalid";
	            case LoginResult.InvalidAccountNumberOrPassword:
	                return "Account Number or Password entered is invalid";
	        }
	        return string.Empty;
	    }

	    #endregion
	}
}
