using System;
using System.Web.UI.WebControls;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace Dpi.Central.Web.Account.Payment
{
    public class RecieptPage : BasePaymentPage
    {
        #region Web Form Designer generated code

        protected Label lblAmt;
		protected System.Web.UI.WebControls.Label lblAccountNumber;
		protected System.Web.UI.WebControls.Label lblPhoneNumber;
		protected System.Web.UI.WebControls.Label lblStreetAddress;
		protected System.Web.UI.WebControls.Label lblEmail;
		protected System.Web.UI.WebControls.Label lblPaymentType;
		protected System.Web.UI.WebControls.Label lblPaymentAmount;
		protected System.Web.UI.WebControls.Label lblPaymentDate;
		protected System.Web.UI.WebControls.Label lblCity;
		protected System.Web.UI.WebControls.Label lblState;
		protected System.Web.UI.WebControls.Label lblZip;
		protected System.Web.UI.WebControls.Label lblCreditCard;
		protected System.Web.UI.WebControls.Label lblCheckNumber;
		protected System.Web.UI.WebControls.Label lblConfirmationNumber;
		protected System.Web.UI.HtmlControls.HtmlTableRow trCreditCardInfo;
		protected System.Web.UI.HtmlControls.HtmlTableRow trCheckInfo;
		protected System.Web.UI.WebControls.Label lblPayorFirstName;
		protected System.Web.UI.WebControls.Label lblPayorLastName;
		protected System.Web.UI.WebControls.Label lblFirstName;
		protected System.Web.UI.WebControls.Label lblLastName;
        protected Dpi.Central.Web.Controls.Footer _footer;
        protected HyperLink lnkReturn;

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
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        #region Event Handlers

        private void Page_Load(object sender, EventArgs e) 
        {
            if (!IsPostBack) 
			{
				DPI.Components.Payment payment = (DPI.Components.Payment)Session["payment"];
				lblAccountNumber.Text = NameFormatter.Capitalize(payment.Demand.Consumer.AccNumber.ToString());
				lblPhoneNumber.Text = NameFormatter.Capitalize(payment.Demand.Consumer.PhNumber);
				
				lblFirstName.Text = NameFormatter.Capitalize(Acct.FirstName);
				lblLastName.Text = NameFormatter.Capitalize(Acct.LastName);				
				IAddr address = payment.Demand.Consumer.MailAddr;

				lblStreetAddress.Text = address.Street;
				lblCity.Text = address.City;
				lblState.Text = address.State;
				lblZip.Text = address.Zipcode;
				lblEmail.Text = (string)Session["email"];
				
				if(payment.PaymentType  == PaymentType.Credit)
				{
					DPI.Components.CreditCard creditCard = (DPI.Components.CreditCard)Session["card"];
					lblPayorFirstName.Text = creditCard.FirstName;
					lblPayorLastName.Text = creditCard.LastName;

					CreditCardPayment creditCardPayment = (CreditCardPayment)payment;
			
					lblPaymentType.Text = "Credit Card - " +  MapCardTypeToString(creditCardPayment.CcType);
					string ccNumber = creditCardPayment.CcNumber;
					lblCreditCard.Text = "*************" + ccNumber.Substring(ccNumber.Length -4, 4);
					
					trCreditCardInfo.Style["display"] = "block";
				}
				else
				{
					DPI.Components.BankCheck bankCheck = (DPI.Components.BankCheck)Session["bankCheck"];
					lblPayorFirstName.Text = bankCheck.FirstName;
					lblPayorLastName.Text = bankCheck.LastName;

					BankCheckPayment bankCheckPayment = (BankCheckPayment)payment;

					lblPaymentType.Text = "Check";
					if (bankCheckPayment.BankAccountNumber.Length > 4)
					{
						lblCheckNumber.Text = "*************" + bankCheckPayment.BankAccountNumber.Substring(bankCheckPayment.BankAccountNumber.Length-4, 4);
					}

					trCheckInfo.Style["display"] = "block";
				}

				lblPaymentAmount.Text = payment.Amount.ToString(MONEY_FORMAT);
				lblPaymentDate.Text = payment.PaymentDate.ToString("MM/dd/yyyy hh:mm");
				lblConfirmationNumber.Text = payment.ConfNum.ToString();   
            }
        }

		private string  MapCardTypeToString(CreditCardType type)
		{
			switch (type) 
			{
				case CreditCardType.VISA:
					return "VISA";
				case CreditCardType.MasterCard:
					return "MASTER CARD";
				case CreditCardType.AmericanExpress:
					return "AMERICAN EXPRESS"; 
				case CreditCardType.DiscoverCard:
					return "DISCOVER";
				default:
					return "";

				
			}
		}

        #endregion
    }
}