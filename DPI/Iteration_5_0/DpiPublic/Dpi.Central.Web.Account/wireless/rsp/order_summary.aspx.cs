using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPI.ClientComp;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.Wireless.Processes.Rsp
{
    public class OrderSummaryPage : RechargeServicePlanBasePage
    {
        #region Constants

        private const string UNKNOWN_RSP_PROCESS = "Type of RSP process in unknown: {0}.";

        #endregion

        #region Web Form Designer generated code

        protected ImageButton btnPrevious;
        protected Label lblOrderSummary;
        protected PlaceHolder phldrOrdrDetails;
        protected Label lblOrderTotal;
        protected Label lblAmountDue;
        protected ImageButton btnPayCreditCard;
        protected ImageButton btnPayCheck;
        protected Label lblFees;

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPrevious.Click += new ImageClickEventHandler(this.btnPrevious_Click);
            this.btnPayCreditCard.Click += new ImageClickEventHandler(this.btnPayCreditCard_Click);
            this.btnPayCheck.Click += new ImageClickEventHandler(this.btnPayCheck_Click);
            this.Load += new EventHandler(this.Page_Load);

        }

        #endregion

        #region Event Handler

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                IWireless_Products[] products;

                switch (GetRspProcess()) {
                    case RspProcess.RechargeSamePlan:
                        products = Model.LoadCustomerRechargedProducts();
                        break;
                    case RspProcess.RechargeDifferentPlan:
                        products = Model.LoadSelectedRechargedProducts();
                        break;
                    default:
                        throw new ApplicationException(string.Format(UNKNOWN_RSP_PROCESS, GetRspProcess()));
                }

                Table tblOrderSummary = new TableDpiWLOrderSum(products);
                tblOrderSummary.Width = Unit.Percentage(100.0);
                phldrOrdrDetails.Controls.Add(tblOrderSummary);

                IWirelessOrderSum orderSummary = Model.CreateOrderSummary(GetRspProcess().ToString());

                lblOrderTotal.Text = orderSummary.ProdSubTotal.ToString("C");
                lblAmountDue.Text = orderSummary.TotalAmtDue.ToString("C");
                lblFees.Text = orderSummary.TaxAmt.ToString("C");
            }
        }

        private void btnPrevious_Click(object sender, ImageClickEventArgs e)
        {
            switch (GetRspProcess()) {
                case RspProcess.RechargeSamePlan:
                    Response.Redirect(SiteMap.WRLS_SERVICE_INFO_URL);
                    break;
                case RspProcess.RechargeDifferentPlan:
                    Response.Redirect(SiteMap.RDP_SELECT_PRODUCTS_URL);
                    break;
                default:
                    throw new ApplicationException(string.Format(UNKNOWN_RSP_PROCESS, GetRspProcess()));
            }
        }

        private void btnPayCreditCard_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(SiteMap.RDP_PAY_CREDIT_CARD_URL);
        }

        private void btnPayCheck_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(SiteMap.RDP_PAY_CHECK_URL);
        }

        #endregion

        #region Private Methods

        private RspProcess GetRspProcess()
        {
            object value = Session[RechargeServicePlanBasePage.SK_RSP_PROCESS];

            if (value == null || value.GetType() != typeof (RspProcess)) {
                throw new ApplicationException("Rsp process is not set. Session key is " + RechargeServicePlanBasePage.SK_RSP_PROCESS);
            }

            return (RspProcess) value;
        }

        #endregion
    }
}