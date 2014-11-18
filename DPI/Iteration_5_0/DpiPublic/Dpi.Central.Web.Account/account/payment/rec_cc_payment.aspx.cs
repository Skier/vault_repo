using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controls;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account.Payment
{
    public class CreditCardRecurringPaymentPage : BaseRecurringPaymentPage
    {
        #region Web Form Designer generated code

        protected ImageButton btnSubmit;
        protected ImageButton btnCancel;
        protected Footer _footer;
        protected AccountInfoControl ctrlAccountInfo;
        protected System.Web.UI.HtmlControls.HtmlGenericControl detailsDiv;
        protected CreditCardInfoControl ctrlCreditCardInfo;

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
            this.btnCancel.Click += new System.Web.UI.ImageClickEventHandler(this.CancelHandler);
            this.btnSubmit.Click += new System.Web.UI.ImageClickEventHandler(this.SubmitHandler);
            this.Load += new System.EventHandler(this.Page_Load);
            this.Init += new System.EventHandler(this.Page_Init);

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
                    UpdateCreditCardInfoControl(ref ctrlCreditCardInfo, payment);

                    ctrlCreditCardInfo.Enabled = false;
                } else {
                    UpdateAccountInfoControl(ref ctrlAccountInfo);
                }

                ctrlAccountInfo.FirstTabIndex = 0;
                ctrlCreditCardInfo.FirstTabIndex = (short) (ctrlAccountInfo.LastTabIndex + 1);
                btnSubmit.TabIndex = (short) (ctrlCreditCardInfo.LastTabIndex + 1);
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
                ICustomerRecurringPayment payment = GetPayment(SelectedPaymentId, PaymentType.Credit);

                UpdatePayment(ref payment, ctrlAccountInfo);

                if (!IsEditMode) {
                    UpdatePayment(ref payment, ctrlCreditCardInfo);
                }

                SavePayment(payment, PaymentType.Credit);
            } catch (Exception ex) {
                SetErrorMessage(ex);
            }
        }

        private void CancelHandler(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(SiteMap.REC_PAYMENTS_URL);
        }

        #endregion

        #region Private Methods

        private void SetTogglePanelEffect() 
        {
            detailsDiv.EnableViewState = false;
            ctrlCreditCardInfo.ToggleDetailsControl.Attributes.Add("onclick", "TogglePanel('" + detailsDiv.ClientID + "', 0.5, '" + ctrlCreditCardInfo.ToggleDetailsControl.ClientID + "');");
        }

        protected void UpdateCreditCardInfoControl(ref CreditCardInfoControl ctrlCreditCardInfo, ICustomerRecurringPayment payment)
        {
            ctrlCreditCardInfo.CardType = CreditCard.GetCreditCardType(payment.BAccNumber);
            ctrlCreditCardInfo.CcNumber = "************" + payment.BAccNumber.Substring(payment.BAccNumber.Length - 4);
            ctrlCreditCardInfo.CvNumber = payment.CVV2;

            ctrlCreditCardInfo.InitExpirationDateControls();
            ctrlCreditCardInfo.ExpMonth = Int32.Parse(payment.ExpirationMonthYear.Trim().Length < 6 ? payment.ExpirationMonthYear.Trim().Substring(0, 1) : payment.ExpirationMonthYear.Trim().Substring(0, 2));
            ctrlCreditCardInfo.ExpYear = Int32.Parse(payment.ExpirationMonthYear.Trim().Substring(payment.ExpirationMonthYear.Trim().Length - 4));
        }

        protected void UpdatePayment(ref ICustomerRecurringPayment payment, CreditCardInfoControl ctrlCreditCardInfo)
        {
            payment.BAccNumber = ctrlCreditCardInfo.CcNumber;
            payment.ExpirationMonthYear = ctrlCreditCardInfo.ExpMonth.ToString() + ctrlCreditCardInfo.ExpYear.ToString();
            payment.CVV2 = ctrlCreditCardInfo.CvNumber;
        }

        #endregion
    }
}