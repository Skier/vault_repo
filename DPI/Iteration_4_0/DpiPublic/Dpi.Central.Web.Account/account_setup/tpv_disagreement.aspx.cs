using System;
using System.Web.UI;
using Dpi.Central.Web.Controls;

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

        #region Test Stuff

        internal override AccountSetupModel Model
        {
            get
            {
                if (Mode == OperatingMode.Test) {
                    if (Session["AccountSetupModelTest"] == null) {
                        AccountSetupModel model = new OrderSummaryPage.TestModel(Map);
                        model.Info.Zip = "29115";
                        model.Info.Provider = new OrderSummaryPage.TestILECInfo();
                        Session["AccountSetupModelTest"] = model;
                    }

                    return (AccountSetupModel) Session["AccountSetupModelTest"];
                }

                return base.Model;
            }
        }

        #endregion
    }
}