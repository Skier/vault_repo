using System;
using System.Web.UI;

namespace Dpi.Central.Web.Account.AccountSetup
{
    public class TpvDisagreement : BaseAccountSetupPage
    {
        protected System.Web.UI.WebControls.ImageButton btnPrevious;
        protected System.Web.UI.WebControls.ImageButton btnBackToPackageSelection;
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
            this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrevious_Click);
            this.btnBackToPackageSelection.Click += new System.Web.UI.ImageClickEventHandler(this.btnBackToPackageSelection_Click);

        }

        #endregion

        private void btnPrevious_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(SiteMap.NEW_ACC_TPV_AGREEMENT_URL);
        }

        private void btnBackToPackageSelection_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(SiteMap.NEW_ACC_SELECT_PACKAGE_URL);
        }
    }
}