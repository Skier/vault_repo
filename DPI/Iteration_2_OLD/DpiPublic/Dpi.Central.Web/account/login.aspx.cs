using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controllers;

namespace Dpi.Central.Web.Account
{
    public class LoginPage : Page
    {
        #region Web Form Designer generated code

        protected Label lblPhoneNumber;
        protected Label lblAccountNumber;
        protected Dpi.Central.Web.Controls.TextBox txtAccountNumber;
        protected Label lblPassword;
        protected Dpi.Central.Web.Controls.TextBox txtPassword;
        protected RegularExpressionValidator anRegExpValidator;
        protected RequiredFieldValidator pwdReqFldValidator;
        protected LinkButton lbtnSignUp;
        protected Dpi.Central.Web.Controls.TextBox txtNpa;
        protected Label lblOr;
        protected Dpi.Central.Web.Controls.TextBox txtNxx;
        protected Dpi.Central.Web.Controls.TextBox txtNumber;
        protected ValidationSummary vldSummary;
        protected CustomValidator vldCustErrorMsg;
        protected ImageButton btnSubmit;
        protected LinkButton lbtnForgotMyPassword;

        protected override void OnInit(EventArgs e) {
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnSubmit.Click += new System.Web.UI.ImageClickEventHandler(this._submitButton_Click);
            this.lbtnForgotMyPassword.Click += new System.EventHandler(this._forgotPasswordButton_Click);
            this.lbtnSignUp.Click += new System.EventHandler(this._signUpButton_Click);
            this.Load += new System.EventHandler(this.LoginPage_Load);

        }

        #endregion

        private void _submitButton_Click(object sender, ImageClickEventArgs e) {
            if (!Page.IsValid) {
                return;
            }

            try {
                LoginController.Instance.Login(
                    txtAccountNumber.Text.Trim(), PhoneNumber, txtPassword.Text.Trim());
            } catch (Exception ex) {
                vldCustErrorMsg.IsValid = false;
                vldCustErrorMsg.ErrorMessage = ex.Message;
            }
        }

        private void _signUpButton_Click(object sender, EventArgs e) {
            Response.Redirect(UrlDictionary.SIGN_UP_URL);
        }

        private void _forgotPasswordButton_Click(object sender, EventArgs e) {
            Response.Redirect(UrlDictionary.PASSWORD_REMINDER_URL);
        }

        private void LoginPage_Load(object sender, EventArgs e) {
            txtNpa.Attributes.Add("onpropertychange", "UpdateControls(event)");
            txtNxx.Attributes.Add("onpropertychange", "UpdateControls(event)");
            txtNumber.Attributes.Add("onpropertychange", "UpdateControls(event)");
            txtAccountNumber.Attributes.Add("onpropertychange", "UpdateControls(event)");
        }

        private string PhoneNumber {
            get {
                string pn = txtNpa.Text.Trim() + txtNxx.Text.Trim() + txtNumber.Text.Trim();

                if (pn.Length != 0 && pn.Length < 10) {
                    throw new FormatException("Phone Number is invalid.");
                }

                return pn;
            }
        }
    }
}