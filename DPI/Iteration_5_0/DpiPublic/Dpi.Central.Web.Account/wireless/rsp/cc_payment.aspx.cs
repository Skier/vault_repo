using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Account.Payment;
using DPI.Components;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.Wireless.Processes.Rsp
{
    public class CreditCardPaymentPage : RechargeServicePlanBasePage
    {
        #region Web Form Designer generated code

        protected ImageButton btnPay;
        protected Dpi.Central.Web.Controls.Footer _footer;
        protected AccountInfoControl ctrlAccountInfo;
        protected System.Web.UI.WebControls.ImageButton m_btnBack;
        protected CreditCardInfoControl ctrlCreditCardInfo;
        protected System.Web.UI.WebControls.TextBox txtPaymentDue;
        protected System.Web.UI.HtmlControls.HtmlGenericControl detailsDiv;
        protected System.Web.UI.HtmlControls.HtmlAnchor lnkToggleRecPaymentDetails;
        protected System.Web.UI.HtmlControls.HtmlGenericControl recPaymentDetailsDiv;
        protected System.Web.UI.WebControls.CheckBox chkRecurringPayment;

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
                ctrlCreditCardInfo.FirstTabIndex = (short)(ctrlAccountInfo.LastTabIndex + 1);
                btnPay.TabIndex = (short)(ctrlCreditCardInfo.LastTabIndex + 1);
                m_btnBack.TabIndex = (short)(btnPay.TabIndex + 1);
                
                PopulateInfo();
            }

            SetTogglePanelEffect();
            SetEnterKeyPressHandler(btnPay);
        }

        #endregion

        private void SetTogglePanelEffect() 
        {
            detailsDiv.EnableViewState = false;
            ctrlCreditCardInfo.ToggleDetailsControl.Attributes.Add("onclick", "TogglePanel('" + detailsDiv.ClientID + "', 0.5, '" + ctrlCreditCardInfo.ToggleDetailsControl.ClientID + "');");
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

            CreditCard card = new CreditCard(
                ctrlCreditCardInfo.CardType, ctrlCreditCardInfo.CcNumber, ctrlCreditCardInfo.CvNumber, 
                ctrlCreditCardInfo.ExpMonth, ctrlCreditCardInfo.ExpYear, ctrlAccountInfo.FirstName,
                ctrlAccountInfo.LastName, ctrlAccountInfo.Zip, ctrlAccountInfo.State, 
                ctrlAccountInfo.City, ctrlAccountInfo.StreetAddress, Model.CustomerData.ContactNumber,
                ctrlAccountInfo.Email);
            
            try {                
                PaymentResult result = Model.MakePayment(card);
                
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
            } catch (CreditCardValidationException) {
                SetErrorMessage("Credit Card number is invalid.");
            } catch (Exception ex) {
                SetErrorMessage(ex);
            }
        }

        #endregion
    }
}