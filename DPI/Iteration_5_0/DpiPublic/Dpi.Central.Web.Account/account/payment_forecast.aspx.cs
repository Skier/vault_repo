using System;
using System.Web.UI.WebControls;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account
{
    public class PaymentForecastPage : BaseAccountPage
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
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        #region EventHandlers

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                LoadPaymentForecastTable();
            }
        }

        #endregion

        #region Private Methods

        private void LoadPaymentForecastTable()
        {
            IOrderSum orderSummary = CustSvc.GetOrderSummary(Map, GetAccountNumber());
            if (orderSummary == null) {
                throw new ApplicationException("Payment forecast can not be built.");
            }

            Table forecastTable = new TableMonthChart(orderSummary, 9);

            Form.Controls.Add(forecastTable);
        }

        #endregion
    }
}