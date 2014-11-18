using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Account.Payment;
using DPI.Components;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.AccountSetup
{
    public class CreditCardPaymentPage : BaseAccountSetupPage
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
                chkRecurringPayment.TabIndex = (short)(ctrlCreditCardInfo.LastTabIndex + 1);
                btnPay.TabIndex = (short)(chkRecurringPayment.TabIndex + 1);
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

        private void OnBackClick(object sender, ImageClickEventArgs e) {
            Response.Redirect(SiteMap.NEW_ACC_SERVICE_ADDRESS_URL);
        }

        #endregion

        #region PopulateInfo

        private void PopulateInfo()
        {
            ctrlAccountInfo.FirstName = Model.Info.CustomerInfo.FirstName;
            ctrlAccountInfo.LastName = Model.Info.CustomerInfo.LastName;
            ctrlAccountInfo.StreetAddress = Model.Info.ServiceAddress.FormattedStreetAddress;
            ctrlAccountInfo.City = Model.Info.ServiceAddress.City;
            ctrlAccountInfo.State = Model.Info.ServiceAddress.State;
            ctrlAccountInfo.Zip = Model.Info.ServiceAddress.Zipcode;
            ctrlAccountInfo.Email = Model.Info.CustomerInfo.Email;
            
            decimal amountDue = Model.Info.OrderSummary.GetTotalAmtDue(1);
            txtPaymentDue.Text = amountDue.ToString("C");
        }

        #endregion

        #region OnPayClick

        private void OnPayClick(object sender, ImageClickEventArgs e)
        {
            if (!IsValid) {
                return;
            }
            
            Model.Info.PaymentType = PaymentType.Credit;
            CreditCard card = new CreditCard(
                ctrlCreditCardInfo.CardType, ctrlCreditCardInfo.CcNumber, ctrlCreditCardInfo.CvNumber, 
                ctrlCreditCardInfo.ExpMonth, ctrlCreditCardInfo.ExpYear, ctrlAccountInfo.FirstName,
                ctrlAccountInfo.LastName, ctrlAccountInfo.Zip, ctrlAccountInfo.State, 
                ctrlAccountInfo.City, ctrlAccountInfo.StreetAddress, Model.Info.CustomerInfo.Contact, 
                ctrlAccountInfo.Email);
            Model.Info.CreditCard = card;
            Model.Info.PaymentAmount = Model.Info.OrderSummary.GetTotalAmtDue(1);  
            Model.Info.SetupRecurringPayments = chkRecurringPayment.Checked;
            
            try {                
                Model.CreateAccount();
                Response.Redirect(SiteMap.NEW_ACC_SUMMARY_URL);
                
            } catch (CreditCardValidationException) {
                SetErrorMessage("Credit Card number is invalid.");
            } catch (PaymentServiceProviderException ex) {
                
                switch (ex.PaymentResult.Code) {
                    case PaymentResultCode.Rejected:
                        SetErrorMessage(string.Format(REJECTED_PAYMENT, ex.PaymentResult.Description));
                        break;
                    case PaymentResultCode.UnableToComplete:
                        SetErrorMessage(UNABLE_TO_COMPLETE_PAYMENT);
                        break;
                    case PaymentResultCode.NeedVerification:
                        SetErrorMessage(string.Format(NEED_VERIFICATION, Model.Info.PaymentAmount.ToString("C")));
                        break;
                    default:
                        throw new ApplicationException("Payment result code is unknown: " + ex.PaymentResult.Code + ".");
                }
            }
            catch (Exception ex) {
                SetErrorMessage(ex);
            }
        }

        #endregion
    }
}