using System;
using System.Text;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.Payment
{
    public class BasePaymentPage : BaseAccountPage
    {
        #region Constants

        protected const string ADDRESS_INFO_IS_MISSED = "Address information for {0} account is missed.";
        protected const string CARD_IS_EXPIRED = "Your credit card is expired. Please correct Expiration Date or use another credit card";

        #endregion

        #region Protected Methods

        protected void UpdateAccountInfoControl(ref AccountInfoControl ctrlAccountInfo) 
        {
            ctrlAccountInfo.AccountNumber = base.Acct.AccNumber.ToString();
            ctrlAccountInfo.PhoneNumber = base.Acct.PhNumber;
            ctrlAccountInfo.LastName = base.Acct.LastName;
            ctrlAccountInfo.FirstName = base.Acct.FirstName;

            IAddr address;
            if (base.Cust.MailAddr != null) {
                address = base.Cust.MailAddr;
            } else if (base.Cust.ServAddr != null) {
                address = base.Cust.ServAddr;
            } else {
                throw new ApplicationException(string.Format(ADDRESS_INFO_IS_MISSED, base.Acct.AccNumber));
            }

            ctrlAccountInfo.StreetAddress = ConvertToString(address);
            ctrlAccountInfo.City = address.City.Trim();
            ctrlAccountInfo.State = address.State;
            ctrlAccountInfo.Zip = address.Zipcode;
            ctrlAccountInfo.Email = base.Cust.CustInfo.Email;
        }

        protected string GetDigits(string value) 
        {
            StringBuilder sb = new StringBuilder(value.Length);
            for (int i = 0; i < value.Length; i++) {
                char c = value[i];
                if (char.IsDigit(c)) {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        protected string ConvertToString(IAddr address)
        {
            StringBuilder sb = new StringBuilder();

            AppendAddressPart(address.StreetNum, sb, false);
            AppendAddressPart(address.StreetPrefix, sb, true); 
            AppendAddressPart(address.Street, sb, true); 
            AppendAddressPart(address.StreetType, sb, true);
            AppendAddressPart(address.StreetSuffix, sb, true); 
            AppendAddressPart(address.Unit, sb, true);

            return sb.ToString();
        }

        protected void AppendAddressPart(string part, StringBuilder sb, bool separator)
        {
            if (part != null) {
                if (separator) {
                    sb.Append(" ");
                }
                
                sb.Append(part.Trim());
            }
        }

        protected decimal GetPaymentAmount() 
        {
            object value = Session[PAYMENT_AMOUNT_KEY];
            if (value == null) {
                throw new ApplicationException(string.Format(SESSION_STATE_IS_INVALID, PAYMENT_AMOUNT_KEY));
            } else {
                return (decimal) value;
            }
        }

        protected void SetPaymentAmount(decimal paymentAmount)
        {
            Session[PAYMENT_AMOUNT_KEY] = paymentAmount;
        }

        #endregion
    }
}