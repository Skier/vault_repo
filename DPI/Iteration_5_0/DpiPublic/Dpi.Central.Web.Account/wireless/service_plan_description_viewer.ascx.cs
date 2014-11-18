using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dpi.Central.Web.Account.Wireless
{
    public class ServicePlanDescriptionViewer : UserControl
    {
        #region Web Form Designer generated code

        protected Label lblPlanStatus;
        protected Label lblExpirationDate;
        protected Label lblCashBalance;
        protected Label lblAnytimeUsedMins;
        protected Label lblStartDate;

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

        public void LoadFrom(ISvcPlanDataResp servicePlan, string provider)
        {
            if (servicePlan == null) {
                throw new ArgumentNullException("servicePlan");
            }

            if (provider == null) {
                throw new ArgumentNullException("provider");
            }

            if (provider.Length == 0) {
                throw new ArgumentException("Provider can not be empty.");
            }

            lblPlanStatus.Text = servicePlan.PlanStatus;                        
            lblAnytimeUsedMins.Text = servicePlan.AnytimeUsedMins;
            lblExpirationDate.Text = servicePlan.ExpirationDate.ToShortDateString();
            lblCashBalance.Text = servicePlan.CashBalance.ToString("C");
            lblStartDate.Text = servicePlan.StartDate.ToShortDateString();
        }
    }
}