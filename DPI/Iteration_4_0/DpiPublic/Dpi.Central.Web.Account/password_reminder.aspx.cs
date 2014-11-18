using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Account;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web
{
    public class PasswordReminder : BaseImageAccountPage
    {
        #region Web Form Designer generated code

        protected ImageButton btnSubmit;
        protected System.Web.UI.HtmlControls.HtmlAnchor lnkSignUp;
        protected System.Web.UI.WebControls.RegularExpressionValidator vldReAccountNumber;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfAccountNumber;
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
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        #region Event Handler

        private void Page_Load(object sender, System.EventArgs e) 
        {
            if (!IsPostBack) {
                lnkSignUp.Visible = false;
            }
        }

        private void SubmitHandler(object sender, ImageClickEventArgs e)
        {
            string accountNumber = txtAccountNumber.Text.Trim();

            if (!Page.IsValid) {
                return;
            }

            try {
                RemindPasswordResult result = RemindPassword(Int32.Parse(accountNumber));

                switch (result.Status) {
                    case WebValidationResultStatus.ValidCustomer:
                        string firstName = NameFormatter.Capitalize(result.AccountInfo.FirstName);
                        SetErrorMessage(string.Format("Thank you {0}. A temporary password has been sent to your email address.", firstName));
                        break;
                    case WebValidationResultStatus.CustomerNotSetupYet:
                        SetErrorMessage("Please select the link Web Access Sign Up to setup access for your account");
                        lnkSignUp.Visible = true;
                        break;
                    case WebValidationResultStatus.InvalidCustomer:
                        SetErrorMessage("Account Number provided is invalid. Please verify and enter a valid Account Number");
                        break;
                }
            } catch (Exception ex) {
                SetErrorMessage(ex);
            }
        }

        #endregion

        #region Private Methods

        private struct RemindPasswordResult
        {
            public IAcctInfo AccountInfo;
            public WebValidationResultStatus Status;
        }

        private RemindPasswordResult RemindPassword(int accountNumber)
        {
            RemindPasswordResult result = new RemindPasswordResult();

            // TODO:  Still need to optimize it to get all the data right away
            RemindWebPasswordResult res1 = CustSvc.RemindWebPassword(Map, accountNumber);

            if (res1.Status == WebValidationResultStatus.ValidCustomer) {
                result.AccountInfo = CustSvc.GetAcctInfo(Map, accountNumber);
                result.Status = WebValidationResultStatus.ValidCustomer;
                EmailSender.SendPasswordRemider(res1.EmailAddress, NameFormatter.Format(result.AccountInfo.FirstName, result.AccountInfo.LastName), res1.WebPassword);
            } else {
                result.Status = res1.Status;
            }

            return result;
        }

        #endregion
    }
}