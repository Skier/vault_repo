
namespace Dalworth.LeadCentral.Domain
{
    public partial class TransactionType
    {
        public TransactionType()
        {
        }
    }

    public enum TransactionTypeEnum
    {
        IncomeCall = 1,
        OutcomeCall = 2,
        IncomeSms = 3,
        OutcomeSms = 4,
        CallerIdLookup = 5,
        VoiceTranscribe = 6,
        ApplicationCharge = 7,
        PhoneNumberCharge = 8,
        RecurringPayment = 9,
        ExtraPayment = 10,
        IncomeTollFreeCall = 11
    }
}
      