using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controllers;
using DPI.Interfaces;

namespace Dpi.Central.Web.account
{
    public class change_password : Page
    {
        #region Web Form Designer generated code

        protected ImageButton btnSubmit;
        protected ValidationSummary vldSummary;
        protected Label lblNewPwd;
        protected Label lblConfirmPwd;
        protected TextBox txtNewPwd;
        protected TextBox txtConfirmPwd;
        protected RequiredFieldValidator vldRfPassword;
        protected CompareValidator vldCmpConfirmPassword;
        protected Label lblResetMsg;
        protected CustomValidator vldCustErrorMsg;

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
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

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                vldCustErrorMsg.IsValid = false;
                vldCustErrorMsg.ErrorMessage = "Please reset temporary password";
            }
        }

        private void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            if (!IsValid) {
                return;
            }

            try {
                AccountSettingsController controller = AccountSettingsController.Instance;
                ICustInfoExt custInfo = controller.GetCustInfoExt();
                controller.ChangeAccountSettings(custInfo.CustInfo.Email, NewPassword);
                HttpContext.Current.Response.Redirect(UrlDictionary.ACCOUNT_SUMMARY_URL);
            } catch (Exception ex) {
                vldCustErrorMsg.IsValid = false;
                vldCustErrorMsg.ErrorMessage = ex.Message;
            }
        }

        private string NewPassword
        {
            get { return (txtNewPwd.Text.Trim().Length == 0) ? null : txtNewPwd.Text; }
        }
    }
}