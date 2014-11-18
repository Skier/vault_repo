using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPI.ClientComp;
using DPI.Components;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.Wireless.Processes.Rsp
{
    public class AccountSummaryControl : UserControl
    {
        #region Web Form Designer generated code

        protected Label m_lblPayorName;
        protected Label m_lblStreetAddress;
        protected Label m_lblCityStateZip;
        protected Label m_lblEmail;
        protected Label m_lblPaymentType;
        protected Label m_lblCreditCardNumberCaption;
        protected Label m_lblCreditCardNumber;
        protected Label m_lblPaymentDate;
        protected Label m_lblConfirmationNumber;
        protected Repeater productRepeater;
        protected Label m_lblAcctName;
        protected Label m_lblPhoneNumber;
        protected System.Web.UI.WebControls.Label m_lblProcessed;
        protected System.Web.UI.WebControls.Label m_lblPinNumber;
        protected System.Web.UI.WebControls.Label m_lblControlNumber;
        protected System.Web.UI.WebControls.Label m_lblMsl;
        protected System.Web.UI.WebControls.Label m_lblMsid;
        protected System.Web.UI.WebControls.Label m_lblSubTotal;
        protected System.Web.UI.WebControls.Label m_lblTotalAmountDue;
        protected System.Web.UI.WebControls.Label m_lblRechargePhoneNumber;
        protected Label m_lblTaxes;

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        #region Event Handlers

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                m_lblPhoneNumber.Text = Model.CustomerData.PhNumber;
                m_lblAcctName.Text = NameFormatter.Format(Model.CustomerData.NameFirst, Model.CustomerData.NameLast);
                m_lblPinNumber.Text = Model.Receipt.Pin;
                m_lblControlNumber.Text = Model.Receipt.ControlNumber;
                m_lblMsl.Text = Model.Receipt.Msl;
                m_lblMsid.Text = Model.Receipt.Msid;

                if (Model.PaymentType == PaymentType.Credit) {
                    LoadCreditCardInformation();
                } else {
                    LoadBankCheckInformation();
                }

                m_lblProcessed.Text = Model.Receipt.Pass ? "Yes" : "No";
                m_lblConfirmationNumber.Text = Model.Receipt.ConfNum;
                m_lblSubTotal.Text = Model.OrderSummary.ProdSubTotal.ToString("C");
                m_lblTaxes.Text = Model.OrderSummary.TaxAmt.ToString("C");
                m_lblTotalAmountDue.Text = Model.OrderSummary.TotalAmtDue.ToString("C");
                m_lblPaymentDate.Text = Model.PaymentResult.Payment.PaymentDate.ToString("MM/dd/yyyy hh:mm");
                
                LoadProductInformation();

                if (Model.Provider.ToLower().IndexOf("Telispire".ToLower()) != -1) {
                    m_lblRechargePhoneNumber.Text = "1-888-416-0172";
                } else {
                    m_lblRechargePhoneNumber.Text = "1-800-314-1630";
                }
            }
        }

        #endregion

        #region Private Methods

        private string MapCardTypeToString(CreditCardType type)
        {
            switch (type) {
                case CreditCardType.VISA:
                    return "VISA";
                case CreditCardType.MasterCard:
                    return "MASTER CARD";
                case CreditCardType.AmericanExpress:
                    return "AMERICAN EXPRESS";
                case CreditCardType.DiscoverCard:
                    return "DISCOVER";
                default:
                    throw new ApplicationException("Credit Card type is unknown: " + type + ".");
            }
        }

        private void LoadCreditCardInformation()
        {
            m_lblPayorName.Text = NameFormatter.Format(Model.CreditCard.FirstName, Model.CreditCard.LastName).ToUpper();
            m_lblStreetAddress.Text = Model.CreditCard.StreetAddress.ToUpper();
            m_lblCityStateZip.Text = (Model.CreditCard.City + " " + Model.CreditCard.State + " " + Model.CreditCard.Zip).ToUpper();
            m_lblEmail.Text = Model.CreditCard.Email;

            m_lblPaymentType.Text = "Credit Card - " + MapCardTypeToString(Model.CreditCard.Type);
            string ccNumber = Model.CreditCard.Number;
            m_lblCreditCardNumber.Text = "*************" + ccNumber.Substring(ccNumber.Length - 4, 4);
            m_lblCreditCardNumberCaption.Text = "Credit Card Number";
        }

        private void LoadBankCheckInformation()
        {
            m_lblPayorName.Text = NameFormatter.Format(Model.BankCheck.FirstName, Model.BankCheck.LastName).ToUpper();
            m_lblStreetAddress.Text = Model.BankCheck.StreetAddress.ToUpper();
            m_lblCityStateZip.Text = (Model.BankCheck.City + " " + Model.BankCheck.State + " " + Model.BankCheck.Zip).ToUpper();
            m_lblEmail.Text = Model.BankCheck.Email;

            m_lblPaymentType.Text = "Check";

            if (Model.BankCheck.BankAccountNumber.Length > 4) {
                string checkNumber = Model.BankCheck.BankAccountNumber;
                m_lblCreditCardNumber.Text = "*************" + checkNumber.Substring(checkNumber.Length - 4, 4);
            }

            m_lblCreditCardNumberCaption.Text = "Bank Account Number ";
        }

        private void LoadProductInformation()
        {
            ArrayList products = new ArrayList(Model.SelectedOptionalProducts.Count + 1);

            foreach (IWireless_Products product in Model.SelectedOptionalProducts) {
                products.Add(new DictionaryEntry(product.Product_name, product.Price));
            }

            products.Add(new DictionaryEntry(Model.SelectedMainProduct.Product_name, Model.SelectedMainProduct.Price));

            productRepeater.DataSource = products;
            productRepeater.DataBind();
        }

        #endregion

        #region Properties

        private RechargeServicePlanModel Model
        {
            get { return ((RechargeServicePlanBasePage) this.Page).Model; }
        }

        #endregion
    }
}