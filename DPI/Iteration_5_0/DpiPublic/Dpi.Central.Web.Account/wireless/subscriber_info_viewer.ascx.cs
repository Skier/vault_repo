using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dpi.Central.Web.Account.Wireless
{
    public class SubscriberInfoViewer : UserControl
    {
        #region Web Form Designer generated code

        protected Label lblEsn;
        protected System.Web.UI.WebControls.Label lblMdn;
        protected Label lblCustomerSince;

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }

        #endregion

        public void LoadFrom(ISvcPlanDataResp servicePlan)
        {
            if (servicePlan == null) {
                throw new ArgumentNullException("servicePlan");
            }

            lblMdn.Text = servicePlan.Mdn;
            lblEsn.Text = servicePlan.Esn;
            lblCustomerSince.Text = servicePlan.CustomerSince.ToShortDateString();
        }
    }
}