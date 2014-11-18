namespace Dalworth.LeadCentral.Domain
{
    public class AmountSummary
    {
        public AmountSummary()
        {
            SubTotalAmt = TaxAmt = TotalAmt = 0;
        }

        public decimal SubTotalAmt { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal TotalAmt { get; set; }
    }
}
