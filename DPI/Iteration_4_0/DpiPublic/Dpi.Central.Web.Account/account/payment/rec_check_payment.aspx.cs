using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account.Payment
{
    public class CheckRecurringPaymentPage : BaseRecurringPaymentPage
    {
        #region Web Form Designer generated code

        protected ImageButton btnSubmit;
        protected ImageButton btnCancel;
        protected AccountInfoControl ctrlAccountInfo;
        protected HtmlGenericControl detailsDiv;
        protected CheckInfoControl ctrlCheckInfo;

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
            this.btnCancel.Click += new ImageClickEventHandler(this.CancelHandler);
            this.btnSubmit.Click += new ImageClickEventHandler(this.SubmitHandler);
            this.Load += new EventHandler(this.Page_Load);
            this.Init += new EventHandler(this.Page_Init);

        }

        #endregion

        #region Event Handlers

        private void Page_Init(object sender, EventArgs e)
        {
            EnsureOneClickBehaviour(btnSubmit);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                if (IsEditMode) {
                    ICustomerRecurringPayment payment = CustSvc.GetCustROP(Map, SelectedPaymentId);

                    UpdateAccountInfoControl(ref ctrlAccountInfo, payment);
                    UpdateCheckInfoControl(ref ctrlCheckInfo, payment);

                    ctrlCheckInfo.Enabled = false;
                } else {
                    UpdateAccountInfoControl(ref ctrlAccountInfo);
                }

                ctrlAccountInfo.FirstTabIndex = 0;
                ctrlCheckInfo.FirstTabIndex = (short) (ctrlAccountInfo.LastTabIndex + 1);
                btnSubmit.TabIndex = (short) (ctrlCheckInfo.LastTabIndex + 1);
                btnCancel.TabIndex = (short) (btnSubmit.TabIndex + 1);
            }

            SetTogglePanelEffect();
        }

        private void SubmitHandler(object sender, ImageClickEventArgs e)
        {
            if (!Page.IsValid) {
                return;
            }

            try {
                ICustomerRecurringPayment payment = GetPayment(SelectedPaymentId, PaymentType.Check);

                UpdatePayment(ref payment, ctrlAccountInfo);

                if (!IsEditMode) {
                    UpdatePayment(ref payment, ctrlCheckInfo);
                }

                SavePayment(payment, PaymentType.Check);
            } catch (Exception ex) {
                SetErrorMessage(ex);
            }
        }

        private void CancelHandler(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(SiteMap.REC_PAYMENTS_URL);
        }

        #endregion

        private void SetTogglePanelEffect()
        {
            detailsDiv.EnableViewState = false;
            ctrlCheckInfo.ToggleDetailsControl.Attributes.Add("onclick", "TogglePanel('" + detailsDiv.ClientID + "', 1, '" + ctrlCheckInfo.ToggleDetailsControl.ClientID + "');");
        }

        protected void UpdateCheckInfoControl(ref CheckInfoControl ctrlCheckInfo, ICustomerRecurringPayment payment)
        {
            ctrlCheckInfo.BankAccountNumber = "************" + payment.BAccNumber.Substring(payment.BAccNumber.Length - 4);
            ctrlCheckInfo.BankRouteNumber = "*********" + payment.BRouteNumber.Substring(payment.BRouteNumber.Length - 4);
            ctrlCheckInfo.DriverLicenseState = payment.DLStateNumber.Substring(0, 2);
            ctrlCheckInfo.DriverLicenseNumber = payment.DLStateNumber.Substring(2);
        }

        protected void UpdatePayment(ref ICustomerRecurringPayment payment, CheckInfoControl ctrlCheckInfo)
        {
            payment.BAccNumber = ctrlCheckInfo.BankAccountNumber;
            payment.BRouteNumber = ctrlCheckInfo.BankRouteNumber;
            payment.DLStateNumber = ctrlCheckInfo.DriverLicenseState + ctrlCheckInfo.DriverLicenseNumber;
        }
    }
}