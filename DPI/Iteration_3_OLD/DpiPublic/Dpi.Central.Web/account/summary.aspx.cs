using System;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controls;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account
{
    public class SummaryPage : BaseAccountPage
    {
        #region Web Form Designer generated code

        protected ControlLabel lblAmountDue;
        protected ControlLabel lblCurrCharges;
        protected ControlLabel lblBalForward;
        protected ControlLabel lblLastDay;
        protected ControlLabel lblDueDate;
        protected ControlLabel lblStatus;
        protected ControlLabel lblCityStateZip;
        protected ControlLabel lblAddress;
        protected ControlLabel lblCustomerName;
        protected ControlLabel lblPhoneNumber;
        protected ControlLabel lblAccNumber;
        protected ControlLabel lblActivDate;

        protected LinkButton lbtnLogout;
        protected Label lblErrMsg;
        protected ControlLabel lblActivDateCap;
        protected HyperLink lnkPromiseToPay;
        protected HyperLink lnkPayment;
        protected HyperLink lnkAccSettings;
        protected HyperLink lnkRecurringSetup;
        protected HyperLink lnkOrders;
        protected ImageButton imgPastReminderNotice;

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

        private IAcctInfo acctInfo;
        private ICustInfoExt2 custInfo;
        private bool isPromiseToPayEnabled;
        private IPastReminderNotice reminderNotice;

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                // If it came from change password page.
                object value = Session[ChangePasswordPage.PASSWORD_CHANGED_KEY];
                if (value != null && value is bool) {
                    if (!(bool) value) {
                        Response.Redirect(SiteMap.CHANGE_PASSWORD_URL);
                        return;
                    }
                }

                try {
                    int acctNumber = GetAccountNumber();
                    // TODO Need to optimize it.  Make one call
                    acctInfo = CustSvc.GetAcctInfo(Map, acctNumber);
                    custInfo = CustSvc.GetCustInfoExt2(Map, acctNumber);
                    isPromiseToPayEnabled = CustSvc.IsEligibleForPromiseToPay(Map, acctNumber);
                    reminderNotice = CustSvc.GetReminderNotice(Map, acctNumber);

                    InitAccountInfo();
                    InitReminderNotice();
                    InitActivationDateText();
                } catch (Exception ex) {
                    lblErrMsg.Text = "Error: " + ex.Message;
                }
            }
        }

        #endregion

        #region InitFunctions

        private void InitAccountInfo()
        {
            // General section.
            lblAccNumber.Text = acctInfo.AccNumber.ToString();
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
            lnkRecurringSetup.Enabled = isActive;

            // Promise to pay
            lnkPromiseToPay.Enabled = isPromiseToPayEnabled;
        }

        private void InitReminderNotice()
        {
            string filename = null;
            if (reminderNotice != null && reminderNotice.Filename != null) {
                filename = reminderNotice.Filename.Trim();
            }

            if (filename == null || filename.Length == 0) {
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

        private void InitActivationDateText()
        {
            string message = string.Empty;

            switch (acctInfo.Status) {
                case "Pending Order":
                    if (custInfo.ActivDate == DateTime.MinValue) {
                        message =
                            "Your activation of service is in progress and takes approximately 5-7 business days. Upon receipt, we will update your account and send you an email with your assigned telephone number and estimated due date.";
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

        #endregion
    }
}