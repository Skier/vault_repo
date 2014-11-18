using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controllers;
using DPI.Components;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.Payment
{
    public class PromiseToPayPage : Page
    {
        #region Web Form Designer generated code

        protected ImageButton btnSubmit;
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

        private void Page_Load(object sender, System.EventArgs e) 
        {
            if (!IsPostBack) {
                PromiseToPayController controller = PromiseToPayController.Instance;
                bool doesPtpExist = controller.DoesPromiseToPayExist();
                if (doesPtpExist) {
                    PromiseToPay ptp = controller.GetPromiseToPay();
                    
                    ShowMessage("Promise to pay " + ptp.Amount.ToString("C") + " on " 
                        + ptp.Date.ToLongDateString() + " was already made on " 
                        + ptp.CreatedDate.ToLongDateString());
                } else {
                    DateTime ptpDate = controller.GetPromiseToPayDate();
                    IAcctInfo acctInfo = controller.RetrieveAccountInfo();

                    ShowMessage("Please press submit to confirm your promise to pay " 
                        + acctInfo.DueAmt.ToString("C") 
                        + " on " + ptpDate.ToLongDateString());
                }

                btnSubmit.Visible = !doesPtpExist;
            }
        }

        private void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            if (!IsValid) {
                return;
            }

            PromiseToPayController controller = PromiseToPayController.Instance;

            try {
                DateTime ptpDate = controller.GetPromiseToPayDate();
                IAcctInfo acctInfo = controller.RetrieveAccountInfo();
                controller.MakePromiseToPay(ptpDate, acctInfo.DueAmt);
                string name = Convertor.MakeFriendlyName(acctInfo.FirstName);
                ShowMessage(string.Format("Thank you {0}. " 
                    + "Your promise to pay {1} on {2} has been received.",
                    name, acctInfo.DueAmt.ToString("C"), 
                    ptpDate.ToLongDateString()));
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
    }
}