using System;
using System.Web.UI.WebControls;
using DPI.ClientComp;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account
{
    public class SummaryPage : BaseAccountPage
    {
        #region Web Form Designer generated code

        protected System.Web.UI.WebControls.Label lblAmountDue;
        protected System.Web.UI.WebControls.Label lblCurrCharges;
        protected System.Web.UI.WebControls.Label lblBalForward;
        protected System.Web.UI.WebControls.Label lblLastDay;
        protected System.Web.UI.WebControls.Label lblDueDate;
        protected System.Web.UI.WebControls.Label lblStatus;
        protected System.Web.UI.WebControls.Label lblCityStateZip;
        protected System.Web.UI.WebControls.Label lblAddress;
        protected System.Web.UI.WebControls.Label lblCustomerName;
        protected System.Web.UI.WebControls.Label lblPhoneNumber;
        protected System.Web.UI.WebControls.Label lblAccNumber;
        protected System.Web.UI.WebControls.Label lblActivDate;
        protected LinkButton lbtnLogout;
        protected System.Web.UI.WebControls.Label lblActivDateCap;
        protected System.Web.UI.WebControls.ImageButton btnPaymentForecast;
        protected System.Web.UI.WebControls.ImageButton btnCustomerBill;
        protected System.Web.UI.HtmlControls.HtmlTableRow rowActivationDate;
        protected Tabs m_tabs;

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
                AccountSummary accSummary = GetAccountSummary();

                if (accSummary.IsWebPasswordTemporal) {
                    Response.Redirect(SiteMap.CHANGE_PASSWORD_URL);
                }

                InitAccountInfo(accSummary);
                InitCustomerBill(accSummary);
                InitPaymentForecast(accSummary);
                InitActivationDateText(accSummary);
                m_tabs.State.SelectedIndex = 0;
            }                        
        }

        #endregion

        #region Private Methods

        private AccountSummary GetAccountSummary()
        {
            object accSummary = Session[ACCOUNT_SUMMARY_KEY];

            if (accSummary != null && accSummary is AccountSummary) {
                return (AccountSummary)accSummary;
            }
            
            return CustSvc.GetAccountSummary(Map, GetAccountNumber());
        }

        private void InitAccountInfo(AccountSummary accSummary)
        {
            // General section.
            lblAccNumber.Text = accSummary.AccNumber.ToString();
            lblPhoneNumber.Text = PhoneNumberFormatter.Format(accSummary.PhNumber);
            lblCustomerName.Text = NameFormatter.Format(accSummary.NameFirst, accSummary.NameLast);
            lblAddress.Text = accSummary.FormattedStreetAddress;
            lblCityStateZip.Text = accSummary.FormattedCityStateZip;
            lblStatus.Text = accSummary.Status;
            lblDueDate.Text = accSummary.IsDueDateNull ? string.Empty : accSummary.DueDate.ToShortDateString();
            lblLastDay.Text = accSummary.IsDiscoDateNull ? string.Empty : accSummary.DiscoDate.ToShortDateString();

            // Account Summary section.
            lblBalForward.Text = accSummary.BalForward.ToString(MONEY_FORMAT);
            lblCurrCharges.Text = accSummary.CurrCharges.ToString(MONEY_FORMAT);
            lblAmountDue.Text = accSummary.DueAmt.ToString(MONEY_FORMAT);

            // Tab control.
            m_tabs.State.Tab6Enabled = accSummary.IsRecurringPaymentEnabled;
            m_tabs.State.Tab5Enabled = accSummary.IsPromiseToPayEnabled;
            m_tabs.State.Tab4Enabled = accSummary.IsPaymentEnabled;
        }

        private void InitCustomerBill(AccountSummary accSummary)
        {
            string action;
            if (accSummary.CustomerBillFileName == null || accSummary.CustomerBillFileName.Length == 0) {
                action = "window.alert('Previous bill is not available.');";
            } else {
                action = "window.open('" + Const.VIRTUAL_DIR_BILLVIEW + accSummary.CustomerBillFileName + "',null,'height= 550, width=700, toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no,resizable=yes');return false;";
            }

            btnCustomerBill.Attributes.Add("onClick", action);
        }

        private void InitPaymentForecast(AccountSummary accSummary) 
        {
            if (btnPaymentForecast.Visible = !accSummary.IsNewOrderDemandIdNull) {
                btnPaymentForecast.Attributes.Add("onClick", "window.open('" + SiteMap.PAYMENT_FORECAST_URL + "',null,'height= 280, width=700, toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no,resizable=yes');return false;");        
            }
        }

        private void InitActivationDateText(AccountSummary accSummary)
        {
            string message = string.Empty;

            switch (accSummary.Status) {
                case "Pending Order":
                    if (accSummary.ActivDate == DateTime.MinValue) {
                        message = "Your activation of service is in progress and takes approximately 5-7 business days. Upon receipt, we will update your account and send you an email with your assigned telephone number and estimated due date.";
                    } else if (DateTime.Now.Date < accSummary.ActivDate.Date) {
                        message = "Your phone number is " + accSummary.PhNumber + " and it will be activated on " + accSummary.ActivDate;
                    }
                    break;
                case "Pending Activation":
                    if (accSummary.ActivDate == DateTime.MinValue) {
                        message = "Your activation is in progress. We will notify you via email when it is complete. Please come back in 5-7 business days.";
                    } else if (DateTime.Now.Date < accSummary.ActivDate.Date) {
                        message = "Your phone number is " + accSummary.PhNumber + " and it will be activated on " + accSummary.ActivDate;
                    }
                    break;
                case "Active":
                case "Pending Disconnect":
                case "Disconnected":
                    break;
                default:
                    throw new ApplicationException("Unknow account status: " + accSummary.Status);
            }

            if (message != string.Empty) {
                lblActivDate.Text = message;
            } else {
                rowActivationDate.Visible = false;
            }
        }

        #endregion
    }
}