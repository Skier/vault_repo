using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPI.ClientComp;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account.Payment
{
    public class PromiseToPayPage : BaseAccountPage
    {
        #region Web Form Designer generated code

        protected ImageButton btnSubmit;
        protected HyperLink lnkSummary;
        protected Label lblMessage;

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
            this.btnSubmit.Click += new ImageClickEventHandler(this.btnSubmit_Click);
            this.Load += new EventHandler(this.Page_Load);
            this.PreRender += new EventHandler(this.Page_PreRender);

        }

        #endregion

        #region Event handlers

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                bool doesPtpExist = CustSvc.DoesPromiseToPayExist(Map, GetAccountNumber());

                DateTime ptpDate = DateTime.MinValue;

                if (doesPtpExist) {
                    PromiseToPay ptp = CustSvc.GetPromiseToPay(Map, GetAccountNumber());

                    lblMessage.Text = string.Format(
                        "A Promise To Pay in the amount of {0}; scheduled to be received for no later than {1} was processed on {2}.",
                        ptp.PtpAmount.ToString("C"), ptp.PtpDate.ToLongDateString(), ptp.CreatedDate.ToLongDateString());
                } else {
                    ptpDate = CustSvc.GetPromiseToPayDate(Map, GetAccountNumber());

                    if (ptpDate == DateTime.MinValue) {
                        lblMessage.Text = string.Format("Promise to pay cannot be accepted");
                    } else {
                        lblMessage.Text = string.Format(
                            "Please press submit to process your Promise To Pay in the amount of {0}; scheduled to be received for no later than {1}",
                            Acct.DueAmt.ToString("C"), ptpDate.ToLongDateString());
                    }
                }

                btnSubmit.Visible = (!doesPtpExist) && (ptpDate != DateTime.MinValue);
            }
        }

        private void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            try {
                DateTime ptpDate = CustSvc.GetPromiseToPayDate(Map, GetAccountNumber());

                CustSvc.MakePromiseToPay(Map, GetAccountNumber(), ptpDate, Acct.DueAmt, Const.PUBLIC_WEB_USERID);
                ICustInfoExt2 custInfo = CustSvc.GetCustInfoExt2(Map, GetAccountNumber());
                EmailSender.SendPromiseToPayNotification(custInfo.CustInfo.Email, ptpDate, Acct.DueAmt, NameFormatter.Format(Acct));

                btnSubmit.Visible = false;

                lblMessage.Text = string.Format(
                    @"Thank You {0}. Your Promise To Pay in the amount of {1}; scheduled to be received for no later than {2} has been processed.",
                    NameFormatter.Capitalize(Acct.FirstName), Acct.DueAmt.ToString("C"), ptpDate.ToLongDateString());
            } catch (Exception ex) {
                SetErrorMessage(ex);
            }
        }

        private void Page_PreRender(object sender, EventArgs e)
        {
            if (btnSubmit.Visible) {
                SetEnterKeyPressHandler(btnSubmit);
            } else {
                RemoveEnterKeyPressHandler();
            }
        }

        #endregion
    }
}