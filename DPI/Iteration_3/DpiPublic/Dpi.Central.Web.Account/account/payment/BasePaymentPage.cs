using System;
using System.Text;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account
{
    public class BasePaymentPage : BaseAccountPage
    {
		protected const string ADDRESS_INFO_IS_MISSED = "Address information for {0} account is missed.";

        #region Implementation

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