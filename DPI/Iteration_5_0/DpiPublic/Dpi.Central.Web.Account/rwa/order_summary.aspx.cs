using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Account.Wireless.Processes.Rsp;
using DPI.ClientComp;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.Wireless.Processes.Rwa
{
    public class OrderSummaryPage : ReplenishWirelessAccountBasePage
    {
        #region Web Form Designer generated code

        protected ImageButton btnPrevious;
        protected Label lblOrderSummary;
        protected PlaceHolder phldrOrdrDetails;
        protected Label lblOrderTotal;
        protected Label lblAmountDue;
        protected ImageButton btnPayment;
        protected System.Web.UI.WebControls.ImageButton btnProceed;
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
            this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrevious_Click);
            this.btnProceed.Click += new System.Web.UI.ImageClickEventHandler(this.btnProceed_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        #region Event Handler

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                IWireless_Products[] products = Model.LoadCustomerRechargedProducts();

                Table tblOrderSummary = new TableDpiWLOrderSum(products);
                tblOrderSummary.Width = Unit.Percentage(100.0);
                phldrOrdrDetails.Controls.Add(tblOrderSummary);

                IWirelessOrderSum orderSummary = Model.CreateOrderSummary(RspProcess.RechargeSamePlan.ToString());

                lblOrderTotal.Text = orderSummary.ProdSubTotal.ToString("C");
                lblAmountDue.Text = orderSummary.TotalAmtDue.ToString("C");
                lblFees.Text = orderSummary.TaxAmt.ToString("C");
            }
        }

        private void btnPrevious_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(SiteMap.RWA_CUSTOMER_INFO_URL);
        }

        private void btnProceed_Click(object sender, System.Web.UI.ImageClickEventArgs e) 
        {
            Response.Redirect(SiteMap.RWA_PAYMENT_URL);
        }

        #endregion
    }
}