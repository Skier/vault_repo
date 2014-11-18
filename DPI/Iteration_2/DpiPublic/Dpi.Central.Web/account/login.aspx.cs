using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;
using DPI.Services;

namespace Dpi.Central.Web.Account
{
    public class LoginPage : BaseAccountPage
    {
		#region Web Form Designer generated code

		protected Label lblPhoneNumber;
		protected Label lblAccountNumber;
		protected Dpi.Central.Web.Controls.TextBox txtAccountNumber;
		protected Label lblPassword;
		protected Dpi.Central.Web.Controls.TextBox txtPassword;
		protected RegularExpressionValidator anRegExpValidator;
		protected RequiredFieldValidator pwdReqFldValidator;
		protected Dpi.Central.Web.Controls.TextBox txtNpa;
		protected Label lblOr;
		protected Dpi.Central.Web.Controls.TextBox txtNxx;
		protected Dpi.Central.Web.Controls.TextBox txtNumber;
		protected ValidationSummary vldSummary;
		protected CustomValidator vldCustErrorMsg;
		protected ImageButton btnSubmit;
		protected System.Web.UI.WebControls.HyperLink lnkSignUp;
		protected System.Web.UI.WebControls.HyperLink lnkForgotMyPassword;

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
            this.Load += new System.EventHandler(this.LoginPage_Load);

        }

		#endregion

		#region EventHandlers

        private void _submitButton_Click(object sender, ImageClickEventArgs e) 
		{
            if (!Page.IsValid) {
                return;
            }

			try
			{
				LoginResult result = Login( txtAccountNumber.Text.Trim(), 
					txtNpa.Text.Trim() + txtNxx.Text.Trim() + txtNumber.Text.Trim(),
					txtPassword.Text.Trim());
            
				vldCustErrorMsg.IsValid = false;

				switch (result)
				{
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
						vldCustErrorMsg.ErrorMessage  = "Account Number or Password entered is invalid";
						break;
				
				} 
			}
			catch (Exception ex)
			{
				// This is only for unexpected.  No business logic should end up here.
				vldCustErrorMsg.ErrorMessage = "Error: " + ex.Message;
                vldCustErrorMsg.IsValid = false;
			}
        }

        private void _signUpButton_Click(object sender, EventArgs e) {
            Response.Redirect(UrlDictionary.SIGN_UP_URL);
        }

        private void _forgotPasswordButton_Click(object sender, EventArgs e) {
            Response.Redirect(UrlDictionary.PASSWORD_REMINDER_URL);
        }

		#endregion
     
        #region Implementations 
		protected override bool IsLoginPage()
		{
			return true;
		}

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


			if ((accountNumber == null || accountNumber.Length == 0) && (phoneNumber == null || phoneNumber.Length == 0))
			{
				result = LoginResult.MissingBothPhoneNumberAndAccountNumber;
				return result;
			}

			if (phoneNumber != null && phoneNumber.Length > 0  &&( !Regex.IsMatch(phoneNumber,"^\\d{10}$")) )
			{
				result = LoginResult.PhoneNumberNotFormattedCorrectly;
				return result;
			}

			if (accountNumber != null && accountNumber.Length > 0 && (!Regex.IsMatch(accountNumber,"^\\d{8}$")))
			{
				result = LoginResult.AccountNumberFormattedIncorrectly;
				return result;

			}

			if (password == null || password.Length == 0)
			{
				result = LoginResult.MissingPassword;
				return result;

			}

			WebValidationResult validationResult;

			if (phoneNumber.Length > 0) 
			{
				validationResult = CustSvc.ValidateWebAccessByPhNumber(
					Map, phoneNumber, password);
			} 
			else if (accountNumber.Length > 0) 
			{
				validationResult = CustSvc.ValidateWebAccessByAccountNumber(
					Map, int.Parse(accountNumber), password);
			} 
			else 
			{
				
				result = LoginResult.MissingPhoneNumberOrAccNumber;
				return result;
			}

			if (validationResult.Status == WebValidationResultStatus.ValidCustomer) 
			{
				accountNumber = validationResult.AccNumber.ToString(CultureInfo.InvariantCulture);
				CreateFormAuthenticationTicket(accountNumber);
				if (validationResult.IsPasswordTemporal) 
				{
					HttpContext.Current.Response.Redirect(UrlDictionary.CHANGE_PASSWORD_URL);
				} 
				else 
				{
					string redirectUrl = FormsAuthentication.GetRedirectUrl(accountNumber, false);
					if (!redirectUrl.ToUpper().EndsWith("DEFAULT.ASPX")) 
					{
						FormsAuthentication.RedirectFromLoginPage(accountNumber, false);
					} 
					else 
					{
						HttpContext.Current.Response.Redirect(UrlDictionary.ACCOUNT_SUMMARY_URL);
					}
				}
			} 
			else if (validationResult.Status == WebValidationResultStatus.CustomerNotSetupYet) 
			{
				result = LoginResult.CustomerNotSetupYet;
			} 
			else if (validationResult.Status == WebValidationResultStatus.InvalidCustomer) 
			{
				if (phoneNumber.Length > 0) 
				{
					result = LoginResult.InvalidPhoneNumberOrPassowrd; 
				} 
				else 
				{
					result = LoginResult.InvalidAccountNumberOrPassword; 
				}
			}
			return result;
		}


		
		#endregion

        private void LoginPage_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                Logout(false);
                base.ctrlFooter.IsLogoutPossible = false;
            }
        }
    }
}