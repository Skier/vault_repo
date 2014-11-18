using System;
using Dpi.Central.Web.Controls;

namespace Dpi.Central.Web.Account.AccountSetup
{
    public class AccountSummaryPrintVersionPage : BaseAccountSetupPage
    {
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

        }

        #endregion

        protected override void InitLayout() 
        {
        }

        #region Test Methods

        internal override AccountSetupModel Model {
            get {
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