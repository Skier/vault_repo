using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controllers;
using DPI.Interfaces;

namespace Dpi.Central.Web
{
    public class PasswordReminder : Page
    {
        #region Web Form Designer generated code

        protected Label lblAccountNumber;
        protected RequiredFieldValidator anReqFldValidator;
        protected RegularExpressionValidator anRegExpValidator;
        protected ValidationSummary vldSummary;
        protected CustomValidator vldCustErrorMsg;
        protected ImageButton btnSubmit;
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
            this.btnSubmit.Click += new ImageClickEventHandler(this.SubmitHandler);

        }

        #endregion

        #region Event Handler

        private void SubmitHandler(object sender, ImageClickEventArgs e)
        {
            if (!Page.IsValid) {
                return;
            }

            try {
                LoginController controller = LoginController.Instance;
                IAcctInfo accountInfo = controller.RemindPassword(AccountNumber);
                string firstName = Convertor.MakeFriendlyName(accountInfo.FirstName);
                ShowMessage("Thank you " + firstName 
                    + ". Your password was sent to your email address.");
            } catch (Exception ex) {
                ShowMessage("Error: " + ex.Message);
            }
        }

        #endregion

        #region Private Methods

        private void ShowMessage(string message)
        {
            vldCustErrorMsg.IsValid = false;
            vldCustErrorMsg.ErrorMessage = message;
        }

        #endregion

        #region Properties

        private int AccountNumber
        {
            get { return Int32.Parse(txtAccountNumber.Text.Trim()); }
        }

        #endregion
    }
}