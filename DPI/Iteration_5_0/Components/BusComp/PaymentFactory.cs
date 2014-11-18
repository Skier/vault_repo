using System;
using DPI.Interfaces;

namespace DPI.Components
{
	public class PaymentFactory
	{
        private static PaymentFactory _instance;

        public static PaymentFactory GetInstance()
        {
            if (_instance == null) {
                lock (typeof(PaymentFactory)) {
                    if (_instance == null) {
                        _instance = new PaymentFactory();
                    }
                }
            }

            return _instance;
        }

		private PaymentFactory()
		{
		}

        public Payment CreatePayment(UOW uow, PaymentType paymentType)
        {
            if (uow == null) {
                throw new ArgumentNullException("uow");
            }

            switch (paymentType) {
                case PaymentType.Credit: 
                    return new CreditCardPayment(uow);
                case PaymentType.Check: 
                    return new BankCheckPayment(uow);
                default:
                    throw new ArgumentException("The payment type " + paymentType + " is not supported.");
            }
        }
	}
}
