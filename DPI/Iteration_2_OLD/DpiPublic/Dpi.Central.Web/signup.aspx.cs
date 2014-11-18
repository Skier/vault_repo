using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controllers;

namespace Dpi.Central.Web
{
    public class SignupPage : Page
    {
        #region Web Form Designer generated code

        protected Label lblAccountNumber;
        protected Label lblEmail;
        protected Label lblPassword;
        protected TextBox txtAccountNumber;
        protected TextBox txtEmail;
        protected TextBox txtPassword;
        protected TextBox txtConfirmPassword;
        protected ImageButton btnSubmit;
        protected CustomValidator vldCustErrorMsg;
        protected ValidationSummary vldSummary;
        protected RequiredFieldValidator vldRfAccountNumber;
        protected RegularExpressionValidator vldReAccountNumber;
        protected RequiredFieldValidator vldRfEmail;
        protected RegularExpressionValidator vldReEmail;
        protected RequiredFieldValidator vldRfPassword;
        protected CompareValidator vldCmpConfirmPassword;
        protected Label lblAccountLastName;
        protected TextBox txtAccountLastName;
        protected RequiredFieldValidator vldRfAccountLastName;
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
            this.btnSubmit.Click += new ImageClickEventHandler(this.SubmitHandler);

        }

        #endregion

        #region Event Handlers

        private void SubmitHandler(object sender, ImageClickEventArgs e)
        {
            if (!Page.IsValid) {
                return;
            }

            try {
                LoginController.Instance.SignUp(
                    AccountNumber, AccountLastName, 
                    AccountEmail, AccountPassword);
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

        #endregion

        #region Properties

        private int AccountNumber
        {
            get { return Int32.Parse(txtAccountNumber.Text.Trim()); }
        }

        private string AccountLastName
        {
            get { return txtAccountLastName.Text.Trim(); }
        }

        private string AccountEmail
        {
            get { return txtEmail.Text.Trim(); }
        }

        private string AccountPassword
        {
            get { return txtPassword.Text.Trim(); }
        }

        #endregion
    }
}