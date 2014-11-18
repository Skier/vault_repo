using System;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Account.Wireless.Processes.Rsp;
using DPI.Interfaces;
using DPI.Services;
using Panel = Dpi.Central.Web.Controls.Panel;

namespace Dpi.Central.Web.Account.Wireless
{
    public class ServiceInfoPage : BaseAccountPage
    {
        #region Web Form Designer generated code

        protected Panel pnlSubscriberInfoViewer;
        protected Panel pnlFeatureUsageViewer;
        protected ImageButton btnRechargeSamePlan;
        protected ImageButton btnRechargeDifferentPlan;
        protected Panel pnlServicePlanDescriptionViewer;
        protected Panel pnlPinDescriptionViewer;
        protected System.Web.UI.WebControls.Panel pnlServiceInfo;
        protected ServicePlanDescriptionViewer ctrlServicePlanDescription;
        protected System.Web.UI.WebControls.PlaceHolder phldrTab;
        protected PinDescriptionViewer ctrlPinDescriptionViewer;

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
            this.btnRechargeSamePlan.Click += new System.Web.UI.ImageClickEventHandler(this.btnRechargeSamePlan_Click);
            this.btnRechargeDifferentPlan.Click += new System.Web.UI.ImageClickEventHandler(this.btnRechargeDifferentPlan_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        #region EventHandlers

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                IWireless_Custdata customerInfo = CustSvc.GetWirelessCustData(Map, GetAccountNumber());

                IWirelessDeviceData resp = DpiWirelessSvc.GetWLDeviceDataResp(customerInfo.ESN);
                if (resp == null || !resp.Pass || resp.PlanStatus == DpiWLPlanStatus.Fail) {
                    base.SetErrorMessage(resp.ErrMessage);
                    return;
                }

                ISvcPlanDataResp servicePlan = DpiWirelessSvc.GetAvailableBalanceResp(resp.Provider, string.Empty, customerInfo.ESN);
                if (servicePlan == null || servicePlan.PlanStatus == null || servicePlan.PlanStatus == "Inactive") {
                    btnRechargeSamePlan.Enabled = btnRechargeDifferentPlan.Enabled = false;
                    return;
                }
                
                ctrlServicePlanDescription.LoadFrom(servicePlan, resp.Provider);

                if (servicePlan.ControlNumber == null) {
                    return;
                }

                IWireless_Products[] products = DpiWirelessSvc.GetProdsBySoc(Map, DpiWirelessSvc.GetSoc(servicePlan.ControlNumber), false);

                ctrlPinDescriptionViewer.LoadFrom(products);
            }
        }

        private void btnRechargeDifferentPlan_Click(object sender, System.Web.UI.ImageClickEventArgs e) 
        {
            Session[RechargeServicePlanBasePage.SK_RSP_PROCESS] = RspProcess.RechargeDifferentPlan;
            Response.Redirect(SiteMap.RDP_SELECT_PLAN_URL, true);
        }

        private void btnRechargeSamePlan_Click(object sender, System.Web.UI.ImageClickEventArgs e) 
        {
            Session[RechargeServicePlanBasePage.SK_RSP_PROCESS] = RspProcess.RechargeSamePlan;
            Response.Redirect(SiteMap.RDP_ORDER_SUMMARY_URL, true);
        }

        #endregion
    }
}