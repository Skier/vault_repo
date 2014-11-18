using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dpi.Central.Web.Account.Wireless
{
    public class FeatureUsageViewer : UserControl
    {
        #region Web Form Designer generated code

        protected Label lblMuAnyTime;
        protected Label lblMuNw;
        protected Label lblMuWeb;
        protected Label lblMuText;
        protected Label lblMu3gWeb;
        protected Label lblMu3gTalk;
        protected Label lblMu3gPic;

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

            lblMuAnyTime.Text = servicePlan.AnytimeUsedMins;
            lblMuNw.Text = servicePlan.NWUsedMins;
            lblMuWeb.Text = servicePlan.WebUsedMins;
            lblMuText.Text = servicePlan.TextUsedMins;
            lblMu3gWeb.Text = servicePlan.ThreeGWebUsedMins;
            lblMu3gTalk.Text = servicePlan.ThreeGPTTUsedMins;
            lblMu3gPic.Text = servicePlan.ThreeGPictureUsedMins;
        }
    }
}