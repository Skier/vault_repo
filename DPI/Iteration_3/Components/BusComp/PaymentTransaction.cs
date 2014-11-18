namespace DPI.Components
{
    /// <summary>
    /// Represents a payment transaction of the external payment service 
    /// provided by <see cref="PaymentServiceProvider"/> class.
    /// </summary>
    public class PaymentTransaction : Verifone_Transaction
    {
        #region Static Members

        public static new PaymentTransaction find(UOW uow, int paymentTransactionId) 
        {
            return (PaymentTransaction) Verifone_Transaction.find(uow, paymentTransactionId);
        }

        #endregion

        #region Constructors

        public PaymentTransaction() : base()
        {
        }

        public PaymentTransaction(UOW uow) : base(uow)
        {
        }

        #endregion

        #region Properties

        public int Id
        {
            get { return base.Verifone_Transaction_ID; }
        }

        public int TransactionMethodId
        {
            get { return base.Transaction_Method_ID; }
            set { base.Transaction_Method_ID = value; }
        }

        public int TransactionTypeId 
        {
            get { return base.Transaction_Type_ID; }
            set { base.Transaction_Type_ID = value; }
        }

        #endregion
    }
}