using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controls;

namespace Dpi.Central.Web.Account.AccountSetup
{
    public class AccountSummary : BaseAccountSetupPage
    {
        #region Web Form Designer generated code

        protected ImageButton m_btnPrintVersion;
        protected System.Web.UI.WebControls.HyperLink HyperLink1;
        protected ImageButton m_btnLogin;

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
            this.m_btnLogin.Click += new System.Web.UI.ImageClickEventHandler(this.OnLoginClick);

        }

        #endregion                

        #region OnLoginClick

        private void OnLoginClick(object sender, ImageClickEventArgs e)
        {
            Session["NEW_ACC_SIGNUP_TO_LOGIN_REDIRECT_ACCT_ID"] = Model.Info.CreatedAccount.AccNumber.ToString();
            ResetModel();
            Response.Redirect(SiteMap.LOGIN_URL);
        }

        #endregion

        #region Test Methods

        internal override AccountSetupModel Model 
        {
            get 
            {
                if (Mode == OperatingMode.Test) 
                {
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