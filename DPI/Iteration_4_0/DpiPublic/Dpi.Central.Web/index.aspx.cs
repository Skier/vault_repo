using System;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controls;

namespace Dpi.Central.Web
{
    public class IndexPage : BasePage
    {
        protected Dpi.Central.Web.Controls.Footer _footer;
        protected HyperLink m_lnkNewAccount1;
        protected HyperLink m_lnkNewAccount2;
        protected HyperLink m_lnkNewAccount3;

        #region AccountLoginUrl

        public string AccountLoginUrl
        {
            get
            {
                return SiteMap.PUBLIC_LOGIN_URL;
            }
        }

        #endregion
        
        #region PasswordReminderUrl

        public string PasswordReminderUrl {
            get {
                return SiteMap.PASSWORD_REMINDER_URL;
            }
        }

        #endregion
        
        #region SignupUrl

        public string SignupUrl {
            get {
                return SiteMap.SIGN_UP_URL;
            }
        }

        #endregion
                
        #region Web Form Designer generated code

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
            this.Load += new EventHandler(OnLoad);
        }

        #endregion

        #region OnLoad

        private void OnLoad(object sender, EventArgs e) {
            m_lnkNewAccount1.NavigateUrl = SiteMap.NEW_ACC_SELECT_PROVIDER_URL;
            m_lnkNewAccount2.NavigateUrl = SiteMap.NEW_ACC_SELECT_PROVIDER_URL;
            m_lnkNewAccount3.NavigateUrl = SiteMap.NEW_ACC_SELECT_PROVIDER_URL;
        }

        #endregion

    }
}