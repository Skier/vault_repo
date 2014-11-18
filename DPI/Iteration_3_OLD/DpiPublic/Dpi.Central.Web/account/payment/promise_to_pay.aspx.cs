using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account.Payment
{
    public class PromiseToPayPage : BaseAccountPage
    {
        #region Web Form Designer generated code

        protected ImageButton btnSubmit;
        protected System.Web.UI.WebControls.HyperLink lnkSummary;
        protected System.Web.UI.WebControls.CustomValidator vldCustErrorMsg;

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
            this.btnSubmit.Click += new System.Web.UI.ImageClickEventHandler(this.btnSubmit_Click);
            this.Load += new System.EventHandler(this.Page_Load);
        }

        #endregion

        #region Event handlers

        private DateTime newPtpDate = DateTime.MinValue;
        private IAcctInfo acctInfo;

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

                bool doesPtpExist = CustSvc.DoesPromiseToPayExist(Map, GetAccountNumber());
                if (doesPtpExist) {
                    PromiseToPay ptp = CustSvc.GetPromiseToPay(Map, GetAccountNumber());

                    ShowMessage(
                        string.Format(
                            "A Promise To Pay in the amount of {0}; scheduled to be received for no later than {1} was processed on {2}.",
                            ptp.PtpAmount.ToString("C"),
                            ptp.PtpDate.ToLongDateString(),
                            ptp.CreatedDate.ToLongDateString()));
                } else {
                    newPtpDate = CustSvc.GetPromiseToPayDate(Map, GetAccountNumber());

					if( newPtpDate == DateTime.MinValue)
					{
						ShowMessage(
							string.Format(
							"Promise to pay cannot be accepted"));
						
					}
					else
					{
                    acctInfo = CustSvc.GetAcctInfo(Map, GetAccountNumber());

                    ShowMessage(
                        string.Format(
                            "Please press submit to process your Promise To Pay in the amount of {0}; scheduled to be received for no later than {1}",
                            acctInfo.DueAmt.ToString("C"),
                            newPtpDate.ToLongDateString())
                        );
                }
                }

                btnSubmit.Visible = (!doesPtpExist) && (newPtpDate != DateTime.MinValue);
            }
        }

        private void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            if (!IsValid) {
                return;
            }

            try {
                newPtpDate = CustSvc.GetPromiseToPayDate(Map, GetAccountNumber());
                acctInfo = CustSvc.GetAcctInfo(Map, GetAccountNumber());

                CustSvc.MakePromiseToPay(
                    Map,
                    GetAccountNumber(),
                    newPtpDate,
                    acctInfo.DueAmt,
                    Const.PUBLIC_WEB_USERID);
                ICustInfoExt2 custInfo = CustSvc.GetCustInfoExt2(Map, GetAccountNumber());

                EmailSender.SendPromiseToPayNotification(
                    custInfo.CustInfo.Email, newPtpDate, acctInfo.DueAmt, acctInfo.FirstName);

                string name = CustInfo.CapitalizeName(acctInfo.FirstName);
                ShowMessage(
                    string.Format(
                        @"Thank You {0}. Your Promise To Pay in the amount of {1}; scheduled to be received for no later than {2} has been processed.",
                        name,
                        acctInfo.DueAmt.ToString("C"),
                        newPtpDate.ToLongDateString()));
                btnSubmit.Visible = false;
            } catch (Exception ex) {
                ShowMessage("Error: " + ex.Message);
            }
        }

        private void ShowMessage(string message)
        {
            vldCustErrorMsg.IsValid = false;
            vldCustErrorMsg.ErrorMessage = message;
        }

        #endregion
    }
}