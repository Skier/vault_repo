using System;

namespace Dpi.Central.Web.Account
{
    public class BasePaymentPage : BaseAccountPage
    {
		protected const string ADDRESS_INFO_IS_MISSED = "Address information for {0} account is missed.";

        #region Implementation

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