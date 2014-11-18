using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dpi.Central.Web
{
    public class HeaderUserControl : UserControl
    {
        protected ImageButton btnImgLogout;

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnImgLogout.Click += new ImageClickEventHandler(this.btnImgLogout_Click);
        }

        #endregion

        public void btnImgLogout_Click(object sender, ImageClickEventArgs e)
        {
            AuthenticationProvider.Instance.Logout();
        }

        public void ShowLogoutButton(bool show)
        {
            btnImgLogout.Visible = show;
        }
    }
}