using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controls;
using DPI.Services;
using Panel = Dpi.Central.Web.Controls.Panel;

namespace Dpi.Central.Web
{
    public class IndexPage : BaseLoginPage
    {
        #region Web Form Designer generated code

        protected HyperLink lnkNewAccount1;
        protected HyperLink lnkNewAccount2;
        protected HyperLink lnkWirelessReplenish;
        protected PhoneNumberBox phnPhoneNumber;
        protected CustomValidator vldCstPhoneNumber;
        protected TextBox txtAccountNumber;
        protected RegularExpressionValidator vldReAccountNumber;
        protected CustomValidator vldCstIdentity;
        protected TextBox txtPassword;
        protected RequiredFieldValidator vldRfPassword;
        protected RegularExpressionValidator vldRePassword;
        protected ImageButton btnSubmit;
        protected Panel pnlPublicAccountLogin;
        protected Dpi.Central.Web.Controls.Panel pnlDpiEnergy;
        protected System.Web.UI.HtmlControls.HtmlAnchor lnkPasswordReminder;
        protected System.Web.UI.HtmlControls.HtmlAnchor lnkWebAccessSignUp;
        protected Footer ctrlFooter;

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
            this.vldCstPhoneNumber.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.vldCstPhoneNumber_ServerValidate);
            this.vldCstIdentity.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.vldCstIdentity_ServerValidate);
            this.btnSubmit.Click += new System.Web.UI.ImageClickEventHandler(this.btnSubmit_Click);
            this.Load += new System.EventHandler(this.OnLoad);

        }

        protected override Control CreateErrorControl() 
        {
            return ErrorControlFactory.Instance.CreatePanelErrorControl();
        }

        #endregion

        #region Event Handlers

        private void OnLoad(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                lnkPasswordReminder.HRef = SiteMap.PASSWORD_REMINDER_URL;
                lnkWebAccessSignUp.HRef = SiteMap.SIGN_UP_URL;
                lnkNewAccount1.NavigateUrl = SiteMap.NEW_ACC_SELECT_PROVIDER_URL;
                lnkNewAccount2.NavigateUrl = SiteMap.NEW_ACC_SELECT_PROVIDER_URL;
                lnkWirelessReplenish.NavigateUrl = SiteMap.RWA_CUSTOMER_INFO_URL;
            }
        }

        private void vldCstPhoneNumber_ServerValidate(object source, ServerValidateEventArgs args)
        {
            PhoneNumberBox phoneNumberBox = (PhoneNumberBox) Page.FindControl(((BaseValidator) source).ControlToValidate);
            args.IsValid = phoneNumberBox.IsValid;
        }

        private void vldCstIdentity_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = txtAccountNumber.Text.Trim().Length != 0 || phnPhoneNumber.PhoneNumber.Trim().Length != 0;
        }

        private void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            if (!Page.IsValid) {
                return;
            }

            try {
                AuthenticationResult result = Authenticate(IMapFactory.getIMap(), txtAccountNumber.Text.Trim(), phnPhoneNumber.PhoneNumber, txtPassword.Text);
                if (result.Status == AuthenticationStatus.Success) {
                    LoginToAccountSite();
                }

                string message = GetUIMessage(result.Status, phnPhoneNumber.PhoneNumber.Length > 0);
                SetErrorMessage(message);
            } catch (Exception ex) {
                SetErrorMessage(ex);
            }
        }

        #endregion

        #region Private Methods

        private void LoginToAccountSite() 
        {
            StringBuilder urlBuilder = new StringBuilder(SiteMap.LOGIN_URL);
            urlBuilder.Append("?");
            
            if (phnPhoneNumber.PhoneNumber.Length != 0) {
                urlBuilder.Append(PHONE_NUMBER_PARAMETER);
                urlBuilder.Append("=");
                urlBuilder.Append(phnPhoneNumber.PhoneNumber);
                urlBuilder.Append("&");
            }

            if (txtAccountNumber.Text.Trim().Length != 0) {
                urlBuilder.Append(ACCOUNT_NUMBER_PARAMETER);
                urlBuilder.Append("=");
                urlBuilder.Append(txtAccountNumber.Text.Trim());
                urlBuilder.Append("&");
            }
            
            string password = EncodePassword(txtPassword.Text);
            urlBuilder.Append(PASSWORD_PARAMETER);
            urlBuilder.Append("=");
            urlBuilder.Append(password);

            string url = urlBuilder.ToString();
            Response.Redirect(url, true);
        }

        #endregion
    }
}