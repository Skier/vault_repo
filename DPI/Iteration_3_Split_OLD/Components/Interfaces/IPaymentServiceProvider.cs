namespace DPI.Interfaces
{
    public enum PaymentResultCode
    {
        Completed,
        Rejected,
        NeedVerification,
        UnableToComplete
    }

    public enum CreditCardType 
    {
        AmericanExpress,
        DinersClub,
        DiscoverCard,
        JCB,
        MasterCard,
        VISA
    }

    public struct PaymentResult
    {
        private PaymentResultCode _code;
        private string _description;

        public PaymentResult(PaymentResultCode code, string description)
        {
            _code = code;
            _description = description;
        }

        public PaymentResultCode Code
        {
            get { return _code; }
        }

        public string Description
        {
            get { return _description; }
        }
    }

    public interface IPaymentServiceProvider
    {
        PaymentResult MakeCreditCardPayment(
            int accountNumber,
            CreditCardType ccType,
            string ccNumber,
            string ccCvNumber,
            int ccExpirationMounth,
            int ccExpirationYear,
            string billFirstName,
            string billLastName,
            string billZip,
            string billState,
            string billCity,
            string billAddress,
            string billEmail,
            string billPhone,
            decimal paymentAmount);

        PaymentResult MakeCheckPayment();
    }
}