using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controls;

namespace Dpi.Central.Web.Account.Wireless.Processes.Rwa
{
    public class CustomerInfoPage : ReplenishWirelessAccountBasePage
    {
        #region Web Form Designer generated code

        protected PhoneNumberBox phnPhoneNumber;
        protected CustomValidator vldCstPhoneNumber;
        protected ImageButton btnProceed;
        protected RequiredFieldValidator vldRfPhoneNumber;

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.vldCstPhoneNumber.ServerValidate += new ServerValidateEventHandler(this.vldCstPhoneNumber_ServerValidate);
            this.btnProceed.Click += new ImageClickEventHandler(this.btnProceed_Click);
            this.Load += new EventHandler(this.OnPageLoad);
            this.Init += new EventHandler(this.OnPageInit);

        }

        #endregion

        #region Event Handlers

        private void OnPageInit(object sender, EventArgs e)
        {
            EnsureOneClickBehaviour(btnProceed);
        }

        private void OnPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                if (!IsFirstStep) {
                    phnPhoneNumber.PhoneNumber = PhoneNumber;
                }

                ControlHelper.SetPageOnLoadScript(this, "javascript:SetInitialFocus();");
                SetTabOrder();
            }

            SetEnterKeyPressHandler(btnProceed);
        }

        private void vldCstPhoneNumber_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = phnPhoneNumber.IsValid;
        }

        private void btnProceed_Click(object sender, ImageClickEventArgs e)
        {
            if (!IsValid) {
                return;
            }

            PhoneNumber = phnPhoneNumber.PhoneNumber;

            Response.Redirect(SiteMap.RWA_ORDER_SUMMARY_URL, true);
        }

        #endregion

        #region Private Methods

        private void SetTabOrder()
        {
            phnPhoneNumber.TabIndex = 1;
            btnProceed.TabIndex = 4;
        }

        #endregion
    }
}