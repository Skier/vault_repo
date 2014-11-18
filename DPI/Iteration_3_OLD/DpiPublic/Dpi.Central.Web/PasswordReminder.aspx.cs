using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

using Dpi.Central.Web.Account;
using DPI.Interfaces;
using DPI.Services;
using DPI.Components;

namespace Dpi.Central.Web
{
    public class PasswordReminder : BaseAccountPage
    {
        #region Web Form Designer generated code

        protected Label lblAccountNumber;
        protected ValidationSummary vldSummary;
        protected CustomValidator vldCustErrorMsg;
        protected ImageButton btnSubmit;
        protected HyperLink lnkSignUp;
        protected HyperLink lnkLogin;
        protected TextBox txtAccountNumber;

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

        #region Event Handler

        private void SubmitHandler(object sender, ImageClickEventArgs e)
        {
            string accountNumber = txtAccountNumber.Text.Trim();

            if (!Page.IsValid) {
                return;
            }

            try {
                if (accountNumber == null ||
                    accountNumber.Length == 0 ||
                    !Regex.IsMatch(accountNumber, "^\\d{8}$")) {
                    ShowMessage("Account Number provided is invalid. Please verify and enter a valid Account Number");
                    return;
                }

                RemindPasswordResult result = RemindPassword(Int32.Parse(accountNumber));

                switch (result.Status) {
                    case WebValidationResultStatus.ValidCustomer:
                        string firstName = CustInfo.CapitalizeName(result.AccountInfo.FirstName);
                        ShowMessage(
                            string.Format(
                                "Thank you {0}. A temporary password has been sent to your email address.",
                                firstName));
                        break;
                    case WebValidationResultStatus.CustomerNotSetupYet:
                        ShowMessage("Please select the link Web Access Sign Up to setup access for your account");
                        lnkSignUp.Visible = true;
                        break;
                    case WebValidationResultStatus.InvalidCustomer:
                        ShowMessage(
                            "Account Number provided is invalid. Please verify and enter a valid Account Number");
                        break;
                }
            } catch (ArgumentException) {
                ShowMessage("The Account Number provided is invalid. Please verify and enter a valid Account Number.");
            } catch (Exception ex) {
                ShowMessage("Error: " + ex.Message);
            }
        }

        #endregion

        #region Private Methods

        private void ShowMessage(string message)
        {
            vldCustErrorMsg.IsValid = false;
            vldCustErrorMsg.ErrorMessage = message;
        }

        private struct RemindPasswordResult
        {
            public IAcctInfo AccountInfo;
            public WebValidationResultStatus Status;
        }

        private RemindPasswordResult RemindPassword(int accountNumber)
        {
            RemindPasswordResult result = new RemindPasswordResult();

            Logout(false);

            // TODO:  Still need to optimize it to get all the data right away
            RemindWebPasswordResult res1 = CustSvc.RemindWebPassword(Map, accountNumber);

            if (res1.Status == WebValidationResultStatus.ValidCustomer) {
                result.AccountInfo = CustSvc.GetAcctInfo(Map, accountNumber);
                result.Status = WebValidationResultStatus.ValidCustomer;
                EmailSender.SendPasswordRemider(res1.EmailAddress, res1.FirstName, res1.WebPassword);
            } else {
                result.Status = res1.Status;
            }

            return result;
        }

        #endregion
    }
}