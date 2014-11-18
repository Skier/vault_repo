using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account
{
    public class ChangePasswordPage : BaseAccountPage
    {
        #region Constants

        public const string PASSWORD_CHANGED_KEY = "PASSWORD_CHANGED_KEY";
        public const int MIN_PASSWORD_LENGTH = 6;
        public const int MAX_PASSWORD_LENGTH = 25;

        #endregion

        #region Web Form Designer generated code

        protected ImageButton btnSubmit;
        protected ValidationSummary vldSummary;
        protected Label lblNewPwd;
        protected Label lblConfirmPwd;
        protected TextBox txtNewPwd;
        protected TextBox txtConfirmPwd;
        protected Label lblResetMsg;
        protected CustomValidator vldCustErrorMsg;

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
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
                Session[PASSWORD_CHANGED_KEY] = false;
                vldCustErrorMsg.IsValid = false;
                vldCustErrorMsg.ErrorMessage = "Please change your temporary password";
            }
        }

        private void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            string newPassowrd;
            string confirmPassowrd;

            newPassowrd = txtNewPwd.Text.Trim();
            confirmPassowrd = txtConfirmPwd.Text.Trim();

            if (!IsValid) {
                return;
            }

            try {
                if (newPassowrd.Length == 0) {
                    ShowError("Password can not be empty");
                } else if (newPassowrd.Length < MIN_PASSWORD_LENGTH) {
                    ShowError(string.Format("Password must have at least {0} characters", MIN_PASSWORD_LENGTH));
                } else if (newPassowrd.Length > MAX_PASSWORD_LENGTH) {
                    ShowError(string.Format("Password cannot be longer then {0} characters", MAX_PASSWORD_LENGTH));
                }

                if (confirmPassowrd.Length == 0) {
                    ShowError("Confirmation Password can not be empty");
                }

                if (!newPassowrd.Equals(confirmPassowrd)) {
                    ShowError("New Password and Confirmation Password do not match");
                }

                if (!vldCustErrorMsg.IsValid) {
                    return;
                }

                ICustInfoExt custInfo = CustSvc.GetCustInfoExt(Map, GetAccountNumber());
                ChangeAccountSettingsResult result =
                    CustSvc.ChangeAccountSettings(Map, GetAccountNumber(), custInfo.CustInfo.Email, newPassowrd);
                IAcctInfo acctInfo = CustSvc.GetAcctInfo(Map, GetAccountNumber());
                EmailSender.SendAccountChangeNotification(result.EmailAddress, result.OldEmailAddress, CustInfo.CapitalizeName(acctInfo.FirstName) + " " + CustInfo.CapitalizeName(acctInfo.LastName));
                Session[PASSWORD_CHANGED_KEY] = true;
                HttpContext.Current.Response.Redirect(SiteMap.ACCOUNT_SUMMARY_URL);
            } catch (Exception ex) {
                vldCustErrorMsg.IsValid = false;
                vldCustErrorMsg.ErrorMessage = ex.Message;
            }
        }

        private void ShowError(string msg)
        {
            vldCustErrorMsg.IsValid = false;
            vldCustErrorMsg.ErrorMessage = msg;
        }

        #endregion
    }
}