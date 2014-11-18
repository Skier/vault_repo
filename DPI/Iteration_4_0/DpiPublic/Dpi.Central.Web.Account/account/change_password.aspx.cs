using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account
{
    /// <summary>
    /// The page is used ONLY for changing TEMPORARY password.
    /// </summary>
    public class ChangePasswordPage : BaseAccountPage
    {
        #region Constants

        public const int MIN_PASSWORD_LENGTH = 6;
        public const int MAX_PASSWORD_LENGTH = 25;

        #endregion

        #region Web Form Designer generated code

        protected ImageButton btnSubmit;
        protected TextBox txtNewPwd;
        protected TextBox txtConfirmPwd;
        protected System.Web.UI.WebControls.RegularExpressionValidator vldRePassword;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfPassword;
        protected System.Web.UI.WebControls.CompareValidator vldCmpConfirmPassword;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfConfirmPassword;
        protected Label lblResetMsg;

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
            this.btnSubmit.Click += new System.Web.UI.ImageClickEventHandler(this.btnSubmit_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        #region Event handlers

        private void Page_Load(object sender, EventArgs e)
        {
            txtNewPwd.MaxLength = MAX_PASSWORD_LENGTH;

            if (!IsPostBack) {
                SetErrorMessage("Please change your temporary password");
            }
        }

        private void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            if (!IsValid) {
                return;
            }

            try {
                ICustInfoExt custInfo = CustSvc.GetCustInfoExt(Map, GetAccountNumber());
                ChangeAccountSettingsResult result = CustSvc.ChangeAccountSettings(Map, GetAccountNumber(), custInfo.CustInfo.Email, txtNewPwd.Text.Trim());
                IAcctInfo acctInfo = CustSvc.GetAcctInfo(Map, GetAccountNumber());
                EmailSender.SendAccountChangeNotification(result.EmailAddress, result.OldEmailAddress, NameFormatter.Format(acctInfo));
                HttpContext.Current.Response.Redirect(SiteMap.ACCOUNT_SUMMARY_URL);
            } catch (Exception ex) {
                SetErrorMessage(ex);
            }
        }

        #endregion
    }
}