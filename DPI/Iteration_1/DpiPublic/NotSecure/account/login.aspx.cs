using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dpi.Central.Web.Account
{
    public class LoginPage : Page
    {
        #region Web Form Designer generated code

        protected Label lblPhoneNumber;
        protected Label lblAccountNumber;
        protected TextBox txtAccountNumber;
        protected Label lblPassword;
        protected TextBox txtPassword;
        protected RegularExpressionValidator anRegExpValidator;
        protected RequiredFieldValidator pwdReqFldValidator;
        protected LinkButton lbtnSignUp;
        protected TextBox txtNpa;
        protected Label lblOr;
        protected TextBox txtNxx;
        protected TextBox txtNumber;
        protected Label lblDefis2;
        protected Label lblDefis1;
        protected System.Web.UI.WebControls.ValidationSummary vldSummary;
        protected System.Web.UI.WebControls.CustomValidator vldCustErrorMsg;
        protected System.Web.UI.WebControls.ImageButton btnSubmit;
        protected LinkButton lbtnForgotMyPassword;

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
            this.lbtnForgotMyPassword.Click += new System.EventHandler(this._forgotPasswordButton_Click);
            this.lbtnSignUp.Click += new System.EventHandler(this._signUpButton_Click);
            this.Load += new System.EventHandler(this.LoginPage_Load);

        }

        #endregion

        private void _submitButton_Click(object sender, ImageClickEventArgs e)
        {
            if (!Page.IsValid) {
                return;
            }

            try {
                AuthenticationProvider.Instance.Login(
                    txtAccountNumber.Text, PhoneNumber, txtPassword.Text);
            } catch (Exception ex) {
                vldCustErrorMsg.IsValid = false;
                vldCustErrorMsg.ErrorMessage = ex.Message;
            }
        }

        private void _signUpButton_Click(object sender, EventArgs e)
        {
            Controller.Instance.SwitchToSignUp();
        }

        private void _forgotPasswordButton_Click(object sender, EventArgs e)
        {
            Controller.Instance.SwitchToPasswordRemider();
        }

        private void LoginPage_Load(object sender, EventArgs e) 
        {
            txtNpa.Attributes.Add("onpropertychange", "UpdateControls()");
            txtNxx.Attributes.Add("onpropertychange", "UpdateControls()");
            txtNumber.Attributes.Add("onpropertychange", "UpdateControls()");
            txtAccountNumber.Attributes.Add("onpropertychange", "UpdateControls()");
        }

        private string PhoneNumber
        {
            get
            {
                string pn = txtNpa.Text + txtNxx.Text + txtNumber.Text;
                
                if (pn.Length != 0 && pn.Length < 10) {
                    throw new FormatException("Phone Number is invalid.");
                }

                return pn;
            }
        }
    }
}