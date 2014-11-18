using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controllers;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account
{
    public class SummaryPage : Page
    {
        #region Web Form Designer generated code

        protected Label lblAmountDue;
        protected Label lblCurrCharges;
        protected Label lblBalForward;
        protected Label lblLastDay;
        protected Label lblDueDate;
        protected Label lblStatus;
        protected Label lblCityStateZip;
        protected Label lblAddress;
        protected Label lblCustomerName;
        protected Label lblPhoneNumber;
        protected Label lblAccNumber;
        protected LinkButton lbRecurringSetup;
        protected Label lblRecurringPymts;
        protected Label lblActivDate;
        protected LinkButton lbtnLogout;
        protected Label lblErrMsg;
        protected HeaderUserControl ctrlHeader;
        protected Label lblActivDateCap;
        protected LinkButton lbPromiseToPay;
        protected LinkButton lbtnChangeAccountSettings;
        protected System.Web.UI.WebControls.Label Label1;
        protected System.Web.UI.WebControls.LinkButton lbOrderStatus;
        protected ImageButton imgPastReminderNotice;

        protected override void OnInit(EventArgs e) {
            InitializeComponent();

            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.lbPromiseToPay.Click += new System.EventHandler(this.lbPromiseToPay_Click);
            this.lbRecurringSetup.Click += new System.EventHandler(this.lbRecurringSetup_Click);
            this.lbOrderStatus.Click += new System.EventHandler(this.lbOrderStatus_Click);
            this.lblActivDate.DataBinding += new System.EventHandler(this.ActivationDateDataBindingHandler);
            this.imgPastReminderNotice.DataBinding += new System.EventHandler(this.ReminderNoticeDataBindingHandler);
            this.lbtnChangeAccountSettings.Click += new System.EventHandler(this.lbtnChangeAccountSettings_Click);
            this.Load += new System.EventHandler(this.Page_Load);
            this.DataBinding += new System.EventHandler(this.SummaryPage_DataBinding);
        }

        #endregion

        private void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                ctrlHeader.ShowLogoutButton(true);

                try {
                    DataBind();
                } catch (Exception ex) {
                    lblErrMsg.Text = "Error: " + ex.Message;
                }
            }
        }

        private void SummaryPage_DataBinding(object sender, EventArgs e) {
            AccountSummaryController controller = AccountSummaryController.Instance;

            IAcctInfo acctInfo = controller.RetrieveAccountInfo();
            ICustInfoExt2 custInfo = controller.RetrieveCustInfoExt2();

            // General section.
            lblAccNumber.Text = controller.AccountNumber.ToString();
            lblPhoneNumber.Text = acctInfo.PhNumFormated;
            lblCustomerName.Text = custInfo.CustInfo.FormattedName;

            if (custInfo.ServAddr != null) {
                lblAddress.Text = custInfo.ServAddr.FormattedStreetAddress;
                lblCityStateZip.Text = custInfo.ServAddr.FormattedCityStateZip;
            }

            lblStatus.Text = acctInfo.Status;

            if (acctInfo.DueDate != DateTime.MinValue) {
                lblDueDate.Text = acctInfo.DueDate.ToShortDateString();
            } else {
                lblDueDate.Text = string.Empty;
            }

            if (acctInfo.DiscoDate > DateTime.MinValue) {
                lblLastDay.Text = acctInfo.DiscoDate.ToShortDateString();
            } else {
                lblLastDay.Text = string.Empty;
            }

            // Account Summary section.
            lblBalForward.Text = acctInfo.BalForward.ToString("C");
            lblCurrCharges.Text = acctInfo.CurrCharges.ToString("C");
            lblAmountDue.Text = acctInfo.DueAmt.ToString("C");

            string status = acctInfo.Status;
            bool isActive = status == "Active" ||
                status == "Pending Activation" || status == "Pending Order";

            // Recurring payment section.
            lbRecurringSetup.Enabled = isActive;

            // Promise to pay
            lbPromiseToPay.Enabled = //acctInfo.DueAmt > 0 &&
                PromiseToPayController.Instance.IsEligibleForPromiseToPay();
        }

        private void ReminderNoticeDataBindingHandler(object sender, EventArgs e) {
            AccountSummaryController controller = AccountSummaryController.Instance;
            string filename = controller.RetrieveReminderNoticeFileName();

            if (filename == null || filename == string.Empty) {
                imgPastReminderNotice.Attributes.Add(
                    "onClick",
                    "window.alert('"
                    + "Previous bill is not available.');");
            } else {
                imgPastReminderNotice.Attributes.Add(
                    "onClick",
                    "window.open('"
                    + Const.VIRTUAL_DIR_BILLVIEW + filename + "',null,'height= 550,"
                    + " width=700, toolbar=no, location=no, directories=no, status=no,"
                    + " menubar=no, scrollbars=no,resizable=yes');return false;");
            }
        }

        private void lbRecurringSetup_Click(object sender, EventArgs e) {
            Response.Redirect(UrlDictionary.RECURRING_PAYMENT_MANAGER_URL);
        }

        private void lbPromiseToPay_Click(object sender, EventArgs e) {
            Response.Redirect(UrlDictionary.PROMISE_TO_PAY_URL);
        }

        private void ActivationDateDataBindingHandler(object sender, EventArgs e) {
            AccountSummaryController controller = AccountSummaryController.Instance;

            IAcctInfo acctInfo = controller.RetrieveAccountInfo();
            ICustInfoExt2 custInfo = controller.RetrieveCustInfoExt2();

            string message = string.Empty;

            switch (acctInfo.Status) {
                case "Pending Order":
                    if (custInfo.ActivDate == DateTime.MinValue) {
                        message =
                            "Your activation is in progress. We will notify you via email when it is complete. Please come back in 5-7 business days.";
                    } else {
                        message = "Your phone number is " + acctInfo.PhNumber + " and it will be activated on " +
                                  custInfo.ActivDate;
                    }
                    break;
                case "Pending Activation":
                    if (custInfo.ActivDate == DateTime.MinValue) {
                        message =
                            "Your activation is in progress. We will notify you via email when it is complete. Please come back in 5-7 business days.";
                    } else {
                        message = "Your phone number is " + acctInfo.PhNumber + " and it will be activated on " +
                                  custInfo.ActivDate;
                    }
                    break;
                case "Active":
                case "Pending Disconnect":
                case "Disconnected":
                    break;
                default:
                    throw new ApplicationException("Unknow account status: " + acctInfo.Status);
            }

            if (message != string.Empty) {
                lblActivDate.Text = message;
            } else {
                lblActivDateCap.Visible = lblActivDate.Visible = false;
            }
        }

        private void lbtnChangeAccountSettings_Click(object sender, EventArgs e) {
            Response.Redirect(UrlDictionary.ACCOUNT_SETTINGS_URL);
        }

        private void lbOrderStatus_Click(object sender, EventArgs e) {
            Response.Redirect(UrlDictionary.ORDER_STATUS_URL);
        }
    }
}