using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web
{
    public class PasswordReminder : Page
    {
        #region Web Form Designer generated code

        protected Label lblAccountNumber;
        protected RequiredFieldValidator anReqFldValidator;
        protected RegularExpressionValidator anRegExpValidator;
        protected System.Web.UI.WebControls.ValidationSummary vldSummary;
        protected System.Web.UI.WebControls.CustomValidator vldCustErrorMsg;
        protected System.Web.UI.WebControls.ImageButton btnSubmit;
        protected TextBox txtAccountNumber;

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
            this.btnSubmit.Click += new System.Web.UI.ImageClickEventHandler(this._submitButton_Click);

        }

        #endregion

        private void _submitButton_Click(object sender, ImageClickEventArgs e)
        {
            if (!Page.IsValid) {
                return;
            }

            try {
                int acctNumber = int.Parse(txtAccountNumber.Text.Trim());
                WebValidationResultStatus status = CustSvc.RemindWebPassword(
                    Controller.Instance.Map, acctNumber);

                switch (status) {
                    case WebValidationResultStatus.ValidCustomer:
                        IAcctInfo accountInfo = CustSvc.GetAcctInfo(
                            Controller.Instance.Map, acctNumber);
                        ShowErrorMessage("Thank you " + accountInfo.FirstName 
                            + ". Your password was sent to your email address.");
                        break;
                    case WebValidationResultStatus.CustomerNotSetupYet:
                        ShowErrorMessage("Error: Please sing up for web access");
                        break;
                    case WebValidationResultStatus.InvalidCustomer:
                        ShowErrorMessage("Error: Invalid Account Number");
                        break;
                }
            } catch (Exception ex) {
                ShowErrorMessage("Error: " + ex.Message);
            }
        }

        private void ShowErrorMessage(string message)
        {
            vldCustErrorMsg.IsValid = false;
            vldCustErrorMsg.ErrorMessage = message;
        }
    }
}