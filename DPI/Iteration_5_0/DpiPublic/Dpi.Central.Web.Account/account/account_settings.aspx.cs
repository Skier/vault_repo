using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Services;

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

        protected ImageButton btnSubmit;
        protected TextBox txtNewPassword;
        protected TextBox txtEMail;
        protected System.Web.UI.WebControls.RegularExpressionValidator vldRePassword;
        protected System.Web.UI.WebControls.CompareValidator vldCmpConfirmPassword;
        protected System.Web.UI.WebControls.RegularExpressionValidator vldReEmail;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfPassword;
        protected TextBox txtPasswordConfirm;

        protected override void OnInit(EventArgs e)
        {
            BindEvents();
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

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
                ChangeAccountSettingsResult result = CustSvc.ChangeAccountSettings(Map, GetAccountNumber(), txtEMail.Text.Trim(), txtNewPassword.Text.Trim().Length == 0 ? null : txtNewPassword.Text.Trim());
                IAcctInfo acctInfo = CustSvc.GetAcctInfo(Map, GetAccountNumber());
                EmailSender.SendAccountChangeNotification(result.EmailAddress, result.OldEmailAddress, NameFormatter.Format(acctInfo));
                base.SetErrorMessage("Your account settings have been saved");
            } catch (Exception ex) {
                base.SetErrorMessage(ex);
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

            base.SetEnterKeyPressHandler(btnSubmit);
        }

        #endregion
    }
}
