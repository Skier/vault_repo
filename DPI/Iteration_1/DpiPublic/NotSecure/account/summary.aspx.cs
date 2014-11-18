using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPI.Interfaces;
using DPI.Services;

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
            this.lbRecurringSetup.Click += new System.EventHandler(this.lbRecurringSetup_Click);
            this.lblActivDate.DataBinding += new System.EventHandler(this.ActivationDateDataBindingHandler);
            this.imgPastReminderNotice.DataBinding += new System.EventHandler(this.ReminderNoticeDataBindingHandler);
            this.Load += new System.EventHandler(this.Page_Load);
            this.DataBinding += new System.EventHandler(this.SummaryPage_DataBinding);

        }

        #endregion

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                ctrlHeader.ShowLogoutButton(true);

                try {
                    DataBind();
                } catch (Exception ex) {
                    lblErrMsg.Text = "Error: " + ex.Message;
                }
            }
        }

        private void SummaryPage_DataBinding(object sender, EventArgs e)
        {
            Controller controller = Controller.Instance;

            // General section.
            lblAccNumber.Text = controller.AccountNumber.ToString();
            lblPhoneNumber.Text = controller.AccountInfo.PhNumFormated;
            lblCustomerName.Text = controller.CustInfoExt2.CustInfo.FormattedName;

            if (controller.CustInfoExt2.ServAddr != null) {
                lblAddress.Text = controller.CustInfoExt2.ServAddr.FormattedStreetAddress;
                lblCityStateZip.Text = controller.CustInfoExt2.ServAddr.FormattedCityStateZip;
            }

            lblStatus.Text = controller.AccountInfo.Status;

            if (controller.AccountInfo.DueDate != DateTime.MinValue) {
                lblDueDate.Text = controller.AccountInfo.DueDate.ToShortDateString();
            } else {
                lblDueDate.Text = string.Empty;
            }

            if (controller.AccountInfo.DiscoDate > DateTime.MinValue) {
                lblLastDay.Text = controller.AccountInfo.DiscoDate.ToShortDateString();
            } else {
                lblLastDay.Text = string.Empty;
            }

            // Account Summary section.
            lblBalForward.Text = controller.AccountInfo.BalForward.ToString("C");
            lblCurrCharges.Text = controller.AccountInfo.CurrCharges.ToString("C");
            lblAmountDue.Text = controller.AccountInfo.DueAmt.ToString("C");

            // Recurring payment section.
            lblRecurringPymts.Visible = lbRecurringSetup.Visible = false;
//            string status = controller.AccountInfo.Status;
//            lbRecurringSetup.Enabled = status == "Active" ||
//                status == "Pending Activation" || status == "Pending Order";
        }

        private void ReminderNoticeDataBindingHandler(object sender, EventArgs e)
        {
            string filename = null;

            Controller controller = Controller.Instance;

            IPastReminderNotice notice = CustSvc.GetReminderNotice(
                controller.Map, controller.AccountNumber);

            if (notice != null && notice.Filename != null && notice.Filename.Trim().Length > 0) {
                filename = notice.Filename;
            }

            if (filename == null) {
                imgPastReminderNotice.Attributes.Add("onClick", "window.alert('"
                    + "Previous bill is not available.');");
            } else {
                this.imgPastReminderNotice.Attributes.Add("onClick", "window.open('"
                    + Const.VIRTUAL_DIR_BILLVIEW + filename + "',null,'height= 550,"
                    + " width=700, toolbar=no, location=no, directories=no, status=no,"
                    + " menubar=no, scrollbars=no,resizable=yes');return false;");
            }
        }

        private void lbRecurringSetup_Click(object sender, EventArgs e)
        {
            Controller.Instance.SwitchToRecurringPaymentManager();
        }

        private void ActivationDateDataBindingHandler(object sender, EventArgs e)
        {
            Controller controller = Controller.Instance;

            string message = string.Empty;

            switch (controller.AccountInfo.Status) {
                case "Pending Order":
                    if (controller.CustInfoExt2.ActivDate == DateTime.MinValue) {
                        message = "Your activation is in progress. We will notify you via email when it is complete. Please come back in 5-7 business days.";
                    } else {
                        message = "Your phone number is " + controller.AccountInfo.PhNumber + " and it will be activated on " + controller.CustInfoExt2.ActivDate;
                    }
                    break;
                case "Pending Activation":
                    if (controller.CustInfoExt2.ActivDate == DateTime.MinValue) {
                        message = "Your activation is in progress. We will notify you via email when it is complete. Please come back in 5-7 business days.";
                    } else {
                        message = "Your phone number is " + controller.AccountInfo.PhNumber + " and it will be activated on " + controller.CustInfoExt2.ActivDate;
                    }
                    break;
                case "Active":
                case "Pending Disconnect":
                case "Disconnected":
                    break;
                default:
                    throw new ApplicationException("Unknow account status: " + controller.AccountInfo.Status);
            }

            if (message != string.Empty) {
                lblActivDate.Text = message;
            } else {
                lblActivDateCap.Visible = lblActivDate.Visible = false;
            }
        }
    }
}