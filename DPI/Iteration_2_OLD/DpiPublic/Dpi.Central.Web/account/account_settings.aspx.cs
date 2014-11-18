using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controllers;
using Dpi.Central.Web.Controls;
using TextBox=Dpi.Central.Web.Controls.TextBox;

namespace Dpi.Central.Web.Account
{
    public class AccountSettings : Page
    {
        #region Web Form Designer generated code

        protected ValidationSummary vldSummary;
        protected CustomValidator vldCustErrorMsg;
        protected ImageButton btnSubmit;
        protected Dpi.Central.Web.Controls.ControlLabel lblEMail;
        protected ControlLabel lblNewPassword;
        protected TextBox txtNewPassword;
        protected TextBox txtEMail;
        protected RegularExpressionValidator emailValidator;
        protected ControlLabel lblPasswordConfirm;
        protected TextBox txtPasswordConfirm;
        protected CustomValidator pwdConfirmValidator;
        protected Label lblChangedNotify;
        protected CheckBox chkShowPwd;

        protected override void OnInit(EventArgs e) {
            BindEvents();
            InitializeComponent();
            base.OnInit(e);
        }
        
        protected override void OnLoad(EventArgs e)
        {
            ControlHelper.NeedDhtmlUtils();
            base.OnLoad (e);
        }


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() 
        {

        }

        #endregion

        private void BindEvents() {
            Load += new EventHandler(Page_Load);
            pwdConfirmValidator.ServerValidate += new ServerValidateEventHandler(pwdConfirmValidator_ServerValidate);
            btnSubmit.Click += new ImageClickEventHandler(btnSubmit_Click);
        }

        private void btnSubmit_Click(object sender, ImageClickEventArgs e) {
            if (!Page.IsValid) {
                return;
            }

            try {
                AccountSettingsController.Instance.ChangeAccountSettings(Email, NewPassword);
                lblChangedNotify.Visible = true;
            } catch (Exception ex) {
                vldCustErrorMsg.IsValid = false;
                vldCustErrorMsg.ErrorMessage = ex.Message;
            }
        }

        private void Page_Load(object sender, EventArgs e) {
            chkShowPwd.Checked = false;
            if (!IsPostBack) {
                txtEMail.Text = AccountSettingsController.Instance.GetCustInfoExt().CustInfo.Email;
            }
        }

        private void pwdConfirmValidator_ServerValidate(object source, ServerValidateEventArgs args) {
            string pwd = txtNewPassword.Text;
            string cnf = txtPasswordConfirm.Text;
            
            if (pwd.Length == 0) return;
            
            if (pwd.Length > 0 && pwd != cnf) {
                args.IsValid = false;
                return;
            }
            
            if (pwd != pwd.Trim()) {
                pwdConfirmValidator.ErrorMessage = "Password cannot start and/or end with spaces. Please change it";
                args.IsValid = false;
            }
        }

        private string Email {
            get { return txtEMail.Text; }
        }

        private string NewPassword {
            get {
                string pw = txtNewPassword.Text.Trim();
                return pw.Length == 0 ? null : pw;
            }
        }
    }
}