using DPI.Interfaces;

namespace DPI.Components
{
    /// <summary>
    /// Represents a credit card payment of the external payment service 
    /// provided by <see cref="PaymentServiceProvider"/> class.
    /// </summary>
    public class CreditCardPayment : Payment
    {
        #region Static Members

        public static Payment find(UOW uow, int id) 
        {
            Payment payment = new CreditCardPayment();
            
            Payment.find(uow, id, ref payment);

            return payment;
        }

        #endregion

        #region Constructors

        public CreditCardPayment() : base()
        {
        }

        public CreditCardPayment(IMap imap) : base(imap) 
        {
        }

        public CreditCardPayment(UOW uow) : base(uow)
        {
        }

        #endregion

        #region Properties

        public CreditCardType CcType
        {
            get { return _ccType; }
            set
            {
                base.setState();
                _ccType = value;
            }
        }

        public string CcNumber
        {
            get { return _ccNumber; }
            set
            {
                base.setState();
                _ccNumber = value;
            }
        }

        public string CvNumber
        {
            get { return _cvNumber; }
            set
            {
                base.setState();
                _cvNumber = value;
            }
        }

        public int ExpYear
        {
            get { return _expYear; }
            set
            {
                base.setState();
                _expYear = value;
            }
        }

        public int ExpMonth
        {
            get { return _expMonth; }
            set
            {
                base.setState();
                _expMonth = value;
            }
        }

        public override PaymentType PaymentType
        {
            get { return PaymentType.Credit; }
        }

        #endregion

        #region Methods

        protected override SqlGateway loadSql()
        {
            return new CreditCardPaymentSql();
        }

        #endregion

        /// <summary>
        /// Represents Data Access Component for <see cref="CreditCardPayment"/> class.
        /// </summary>
        protected class CreditCardPaymentSql : PaymentSql
        {
            protected override Payment CreatePayment()
            {
                return new CreditCardPayment();
            }
        }
    }
}