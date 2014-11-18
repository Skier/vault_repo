using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Account;

namespace Dpi.Central.Web
{
    public class FooterUserControl : UserControl
    {
        #region Web Form Designer generated code

        protected ImageButton logout;
        private bool logOutClicked = false;

        protected override void OnInit(EventArgs e)
        {
            CheckLogout();
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
        }

        private void CheckLogout()
        {
            logOutClicked = false;

            if (Page.IsPostBack) {
                string x = Request.Form[logout.UniqueID + ".x"];
                if (x != null && x.Length > 0) {
                    try {
                        logOutClicked = Convert.ToInt32(x) > 0;
                    } catch {
                        // ignore bad post data
                    }
                }

                if (logOutClicked) {
                    BaseAccountPage.Logout(Page is BaseAccountPage);
                }
            }

            logout.Visible = BaseAccountPage.IsAuthenticated();
        }

        public bool IsLogoutPossible
        {
            get { return logout.Visible; }
            set { logout.Visible = value; }
        }

        #endregion
    }
}