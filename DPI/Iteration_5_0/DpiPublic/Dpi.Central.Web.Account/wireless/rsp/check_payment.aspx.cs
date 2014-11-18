using System;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Account.Payment;
using Dpi.Central.Web.Controls;
using DPI.Components;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.Wireless.Processes.Rsp
{
    public class CheckPaymentPage : RechargeServicePlanBasePage
    {
        #region Web Form Designer generated code

        protected ImageButton btnPay;
        protected Footer _footer;
        protected AccountInfoControl ctrlAccountInfo;
        protected ImageButton m_btnBack;
        protected CheckInfoControl ctrlCheckInfo;
        protected TextBox txtPaymentDue;
        protected System.Web.UI.HtmlControls.HtmlGenericControl detailsDiv;
        protected CheckBox chkRecurringPayment;

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.m_btnBack.Click += new System.Web.UI.ImageClickEventHandler(this.OnBackClick);
            this.btnPay.Click += new System.Web.UI.ImageClickEventHandler(this.OnPayClick);
            this.Load += new System.EventHandler(this.OnPageLoad);
            this.Init += new System.EventHandler(this.OnPageInit);

        }

        #endregion

        #region OnPageInit

        private void OnPageInit(object sender, EventArgs e)
        {
            EnsureOneClickBehaviour(btnPay);
        }

        #endregion

        #region OnPageLoad

        private void OnPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                ctrlAccountInfo.VisibilityForAccountNumber = ctrlAccountInfo.VisibilityForPhoneNumber = false;

                ctrlAccountInfo.FirstTabIndex = 1;
                ctrlCheckInfo.FirstTabIndex = (short) (ctrlAccountInfo.LastTabIndex + 1);
                btnPay.TabIndex = (short) (ctrlCheckInfo.LastTabIndex + 1);
                m_btnBack.TabIndex = (short) (btnPay.TabIndex + 1);

                PopulateInfo();
            }

            SetTogglePanelEffect();
            SetEnterKeyPressHandler(btnPay);
        }

        #endregion

        private void SetTogglePanelEffect()
        {
            detailsDiv.EnableViewState = false;
            ctrlCheckInfo.ToggleDetailsControl.Attributes.Add("onclick", "TogglePanel('" + detailsDiv.ClientID + "', 1, '" + ctrlCheckInfo.ToggleDetailsControl.ClientID + "');");
        }

        #region OnBackClick

        private void OnBackClick(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(SiteMap.RDP_ORDER_SUMMARY_URL);
        }

        #endregion

        #region PopulateInfo

        private void PopulateInfo()
        {
            IWireless_Custdata customerData = Model.CustomerData;

            ctrlAccountInfo.FirstName = customerData.NameFirst;
            ctrlAccountInfo.LastName = customerData.NameLast;
            ctrlAccountInfo.StreetAddress = customerData.Addr1 + " " + customerData.Addr2;
            ctrlAccountInfo.City = customerData.City;
            ctrlAccountInfo.State = customerData.State;
            ctrlAccountInfo.Zip = customerData.Zip;
            ctrlAccountInfo.Email = customerData.Email;
            
            decimal amountDue = Model.GetPaymentAmount();
            txtPaymentDue.Text = amountDue.ToString("C");
        }

        #endregion

        #region OnPayClick

        private void OnPayClick(object sender, ImageClickEventArgs e)
        {
            if (!IsValid) {
                return;
            }

            BankCheck bankCheck = new BankCheck(
                ctrlCheckInfo.BankRouteNumber, ctrlCheckInfo.BankAccountNumber, ctrlCheckInfo.DriverLicenseNumber,
                ctrlCheckInfo.DriverLicenseState, ctrlAccountInfo.FirstName, ctrlAccountInfo.LastName,
                ctrlAccountInfo.Zip, ctrlAccountInfo.State, ctrlAccountInfo.City,
                ctrlAccountInfo.StreetAddress, Model.CustomerData.ContactNumber, ctrlAccountInfo.Email);
            
            StringCollection errors = bankCheck.Validate();
            if (errors.Count > 0) {
                SetErrorMessage(errors[0]);
                return;
            }

            try {                
                PaymentResult result = Model.MakePayment(bankCheck);
                
                switch (result.Code) {
                    case PaymentResultCode.Completed:
                        Response.Redirect(SiteMap.RDP_RECEIPT_URL, false);
                        break;
                    case PaymentResultCode.Rejected:
                        SetErrorMessage(string.Format(REJECTED_PAYMENT, result.Description));
                        break;
                    case PaymentResultCode.UnableToComplete:
                        SetErrorMessage(UNABLE_TO_COMPLETE_PAYMENT);
                        break;
                    case PaymentResultCode.NeedVerification:
                        SetErrorMessage(string.Format(NEED_VERIFICATION, Model.GetPaymentAmount().ToString("C")));
                        break;
                    default:
                        throw new ApplicationException("Payment result code is unknown: " + result.Code + ".");
                }
            } catch (Exception ex) {
                SetErrorMessage(ex);
            }
        }

        #endregion
    }
}