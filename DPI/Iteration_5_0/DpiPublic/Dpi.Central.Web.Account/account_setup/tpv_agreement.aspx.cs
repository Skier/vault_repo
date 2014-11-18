using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controls;

namespace Dpi.Central.Web.Account.AccountSetup
{
    public class TpvAgreementPage : BaseAccountSetupPage
    {
        #region Constants

        private const string BLANK_FIELD_ERROR = "<br>Please enter your Birthday to continue processing your order";

        #endregion

        #region Web Form Designer generated code

        protected ImageButton btnPrevious;
        protected ImageButton btnAgree;
        protected ImageButton btnDisagree;
        protected CustomValidator vldCstBirthday;
        protected DateBox txtBirthday;

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);

            txtBirthday.TabIndex = 1;
            btnAgree.TabIndex = 4;
            btnDisagree.TabIndex = 5;
            btnPrevious.TabIndex = 6;
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.vldCstBirthday.ServerValidate += new ServerValidateEventHandler(this.vldCstBirthday_ServerValidate);
            this.btnPrevious.Click += new ImageClickEventHandler(this.btnPrevious_Click);
            this.btnAgree.Click += new ImageClickEventHandler(this.btnAgree_Click);
            this.btnDisagree.Click += new ImageClickEventHandler(this.btnDisagree_Click);

        }

        #endregion

        private void btnPrevious_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(SiteMap.NEW_ACC_ORDER_SUMMARY_URL);
        }

        private void btnAgree_Click(object sender, ImageClickEventArgs e)
        {
            if (!IsValid) {
                return;
            }

            if (txtBirthday.IsNull) {
                vldCstBirthday.IsValid = false;
                vldCstBirthday.ErrorMessage = BLANK_FIELD_ERROR;
                return;
            }

            Model.Info.IsTpvAgreement = true;
            Model.Info.TpvBirthday = txtBirthday.Date;

            Response.Redirect(SiteMap.NEW_ACC_SERVICE_ADDRESS_URL);
        }

        private void btnDisagree_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(SiteMap.NEW_ACC_TPV_DISAGREEMENT_URL);
        }

        private void vldCstBirthday_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtBirthday.IsNull) {
                args.IsValid = true;
                vldCstBirthday.ErrorMessage = BLANK_FIELD_ERROR;
            } else if (!txtBirthday.IsValid) {
                args.IsValid = false;
                ((BaseValidator) source).ErrorMessage = "<br>The Birthday is invalid";
            } else if (DateTime.Now < txtBirthday.Date.AddYears(18)) {
                args.IsValid = false;
                ((BaseValidator) source).ErrorMessage = "<br>You must be 18 years of age or older";
            }
        }
    }
}