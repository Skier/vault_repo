using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DPI.Components;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.AccountSetup
{
    public class AccountSummaryControl : UserControl
    {
        #region Fields

        private AccountSetupInfo _info;
        private AccountSetupModel _model;

        #endregion

        #region Web Form Designer generated code

        protected Label m_lblAcctNumber;
        protected HtmlGenericControl m_rowLowIncomeLink;
        protected Label m_lblPayorName;
        protected Label m_lblStreetAddress;
        protected Label m_lblCityStateZip;
        protected Label m_lblEmail;
        protected Label m_lblPaymentType;
        protected Label m_lblCreditCardNumberCaption;
        protected Label m_lblCreditCardNumber;
        protected Label m_lblPaymentAmount;
        protected Label m_lblPaymentDate;
        protected Label m_lblConfirmationNumber;
        protected Repeater productRepeater;
        protected HtmlGenericControl termsAndConditionsDiv;
        protected System.Web.UI.WebControls.Label m_lblAcctName;
        protected System.Web.UI.HtmlControls.HtmlGenericControl starterKitSpan;
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
            this.Init += new System.EventHandler(this.Page_Init);

        }

        #endregion

        #region Event Handlers

        private void Page_Init(object sender, EventArgs e)
        {
            _model = ((BaseAccountSetupPage) this.Page).Model;
            _info = ((BaseAccountSetupPage) this.Page).Model.Info;
        }

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                m_lblAcctNumber.Text = _info.CreatedAccount.AccNumber.ToString();
                m_lblAcctName.Text = _info.CustomerInfo.FormattedName;

                m_rowLowIncomeLink.Visible = _model.IsProductLifeLine(_info.Package.Package) && (Page is AccountSummary);

                if (_info.PaymentType == PaymentType.Credit) {
                    LoadCreditCardInformation();
                } else {
                    LoadBankCheckInformation();
                }

                m_lblPaymentAmount.Text = _info.PaymentAmount.ToString("C");
                m_lblPaymentDate.Text = _info.PaymentResult.Payment.PaymentDate.ToString("MM/dd/yyyy hh:mm");
                m_lblConfirmationNumber.Text = _info.CreatedAccount.ConfNum.ToString();
                m_lblTaxes.Text = _info.OrderSummary.GetTaxAmt(1).ToString("C");

                LoadProductInformation();

                starterKitSpan.Visible = _model.IsInternetProductExist();
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
            m_lblPayorName.Text = (_info.CreditCard.FirstName + " " + _info.CreditCard.LastName).ToUpper();
            m_lblStreetAddress.Text = _info.CreditCard.StreetAddress.ToUpper();
            m_lblCityStateZip.Text = (_info.CreditCard.City + " " + _info.CreditCard.State + " " + _info.CreditCard.Zip).ToUpper();
            m_lblEmail.Text = _info.CreditCard.Email;

            m_lblPaymentType.Text = "Credit Card - " + MapCardTypeToString(_info.CreditCard.Type);
            string ccNumber = _info.CreditCard.Number;
            m_lblCreditCardNumber.Text = "*************" + ccNumber.Substring(ccNumber.Length - 4, 4);
            m_lblCreditCardNumberCaption.Text = "Credit Card Number";
        }

        private void LoadBankCheckInformation()
        {
            m_lblPayorName.Text = (_info.BankCheck.FirstName + " " + _info.BankCheck.LastName).ToUpper();
            m_lblStreetAddress.Text = _info.BankCheck.StreetAddress.ToUpper();
            m_lblCityStateZip.Text = (_info.BankCheck.City + " " + _info.BankCheck.State + " " + _info.BankCheck.Zip).ToUpper();
            m_lblEmail.Text = _info.BankCheck.Email;

            m_lblPaymentType.Text = "Check";

            if (_info.BankCheck.BankAccountNumber.Length > 4) {
                string checkNumber = _info.BankCheck.BankAccountNumber;
                m_lblCreditCardNumber.Text = "*************" + checkNumber.Substring(checkNumber.Length - 4, 4);
            }

            m_lblCreditCardNumberCaption.Text = "Bank Account Number ";
        }

        private void LoadProductInformation()
        {
            ArrayList products = _model.GetProductsForReceipt();

            productRepeater.DataSource = products;
            productRepeater.DataBind();

            termsAndConditionsDiv.Visible = IsInternetProductExist();
        }

        private bool IsInternetProductExist()
        {
            IProdPrice[] products = _info.OrderSummary.Products;
            foreach (IProdPrice product in products) {
                if (product.ProdType == ProdCategory.Internet.ToString()) {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}