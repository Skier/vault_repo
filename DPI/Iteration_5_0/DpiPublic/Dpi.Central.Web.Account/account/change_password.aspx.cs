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

        #region Fields

        private IWireless_Custdata _wirelessCustData;

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

        #region Override Methods

        protected override void AddErrorControlToPage() 
        {
            Form.Controls.AddAt(2, new LiteralControl("<br>"));
            Form.Controls.AddAt(3, ErrorControl);
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
                if (HttpContext.Current.User.IsInRole(AuthenticationHelper.HOME_PHONE_ACCOUNT)) {
                    ChangeOrdinaryAccountPassword();
                } else if (HttpContext.Current.User.IsInRole(AuthenticationHelper.WIRELESS_ACCOUNT)) {
                    ChangeWirelessAccountPassword();
                }
            } catch (Exception ex) {
                SetErrorMessage(ex);
            }
        }

        #endregion

        #region Private Methods

        private void ChangeOrdinaryAccountPassword()
        {
            ICustInfoExt custInfo = CustSvc.GetCustInfoExt(Map, GetAccountNumber());
            ChangeAccountSettingsResult result = CustSvc.ChangeAccountSettings(Map, GetAccountNumber(), custInfo.CustInfo.Email, txtNewPwd.Text.Trim());
            IAcctInfo acctInfo = CustSvc.GetAcctInfo(Map, GetAccountNumber());
            
            EmailSender.SendAccountChangeNotification(result.EmailAddress, result.OldEmailAddress, NameFormatter.Format(acctInfo));

            HttpContext.Current.Response.Redirect(SiteMap.ACCOUNT_SUMMARY_URL);
        }

        private void ChangeWirelessAccountPassword() 
        {
            CustSvc.UpdateWirelessCustData(Map, GetAccountNumber(), new CustSvc.UpdateWirelessCustDataCallback(this.UpdateWirelessCustData));

            if (_wirelessCustData != null) {
                EmailSender.SendWirelessAccountChangeNotification(_wirelessCustData.Email, _wirelessCustData.Email, NameFormatter.Format(_wirelessCustData.NameFirst, _wirelessCustData.NameLast));
            }

            HttpContext.Current.Response.Redirect(SiteMap.WRLS_CUSTOMER_INFO_URL);
        }

        private void UpdateWirelessCustData(IWireless_Custdata custData)
        {
            _wirelessCustData = custData;

            custData.WebPassword = txtNewPwd.Text.Trim();
            custData.IsWebPasswordTemporal = false;
        }

        #endregion
    }
}