using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Account.Controls;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;
using TextBox=Dpi.Central.Web.Account.Controls.TextBox;

namespace Dpi.Central.Web.Account
{
    public class AccountSettingsPage : BaseAccountPage
    {
        #region Constants

        // TODO: Refactor it. Where shall we place such constants?
        public const int MIN_PASSWORD_LENGTH = ChangePasswordPage.MIN_PASSWORD_LENGTH;
        public const int MAX_PASSWORD_LENGTH = ChangePasswordPage.MAX_PASSWORD_LENGTH;

        #endregion Constants

        #region Web Form Designer generated code

        protected ValidationSummary vldSummary;
        protected CustomValidator vldCustErrorMsg;
        protected ImageButton btnSubmit;
        protected ControlLabel lblEMail;
        protected ControlLabel lblNewPassword;
        protected TextBox txtNewPassword;
        protected TextBox txtEMail;
        protected RegularExpressionValidator emailValidator;
        protected ControlLabel lblPasswordConfirm;
        protected TextBox txtPasswordConfirm;
        protected System.Web.UI.WebControls.CustomValidator pwdConfirmValidator;
        protected System.Web.UI.WebControls.CustomValidator pwdValidator;
        protected System.Web.UI.WebControls.HyperLink lnkSummary;
        protected Label lblChangedNotify;

        protected override void OnInit(EventArgs e)
        {
            BindEvents();
            InitializeComponent();
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            ControlHelper.NeedDhtmlUtils();
            base.OnLoad(e);
        }


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pwdValidator.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.pwdValidator_ServerValidate);
            this.pwdConfirmValidator.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.pwdConfirmValidator_ServerValidate);
        }

        #endregion

        #region Event handlers

        private void BindEvents()
        {
            Load += new EventHandler(Page_Load);            
            btnSubmit.Click += new ImageClickEventHandler(btnSubmit_Click);
        }

        private void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            if (!Page.IsValid) {
                return;
            }

            try {
                ChangeAccountSettingsResult result =
                    CustSvc.ChangeAccountSettings(Map, GetAccountNumber(), Email, NewPassword);
                IAcctInfo acctInfo = CustSvc.GetAcctInfo(Map, GetAccountNumber());
                EmailSender.SendAccountChangeNotification(result.EmailAddress, result.OldEmailAddress, CustInfo.CapitalizeName(acctInfo.FirstName) + " " + CustInfo.CapitalizeName(acctInfo.LastName));

                lblChangedNotify.Visible = true;
            } catch (Exception ex) {
                vldCustErrorMsg.IsValid = false;
                vldCustErrorMsg.ErrorMessage = ex.Message;
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            txtNewPassword.MaxLength = MAX_PASSWORD_LENGTH;
            txtPasswordConfirm.MaxLength = MAX_PASSWORD_LENGTH;

            if (!IsPostBack) {
                ICustInfo2 custInfoExt = CustSvc.GetCustInfoExt(Map, GetAccountNumber()).CustInfo;
                txtEMail.Text = custInfoExt.Email;
            }
        }

        private void pwdConfirmValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtNewPassword.Text != txtPasswordConfirm.Text) {
                args.IsValid = false;                
            }
        }

        private void pwdValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {            
            if (txtPasswordConfirm.Text.Length == 0)
            {
                pwdValidator.ErrorMessage = "New Password and Confirm Password must match. Please verify the Passwords.";
                args.IsValid = false;
            } else if (txtNewPassword.Text != txtNewPassword.Text.Trim()) 
            {
                pwdValidator.ErrorMessage = "Password cannot start and/or end with spaces. Please change it";
                args.IsValid = false;                
            } else if (txtNewPassword.Text.Length > 0 && txtNewPassword.Text.Length < MIN_PASSWORD_LENGTH) 
            {
                pwdValidator.ErrorMessage =
                    string.Format("Password must have at least {0} characters", MIN_PASSWORD_LENGTH);
                args.IsValid = false;
            } else if (txtNewPassword.Text.Length > MAX_PASSWORD_LENGTH) 
            {
                pwdValidator.ErrorMessage =
                    string.Format("Password cannot be longer then {0} characters", MAX_PASSWORD_LENGTH);
                args.IsValid = false;
            } 
        }

        private string Email
        {
            get { return txtEMail.Text; }
        }

        private string NewPassword
        {
            get
            {
                string pw = txtNewPassword.Text.Trim();
                return pw.Length == 0 ? null : pw;
            }
        }

        #endregion
    }
}
