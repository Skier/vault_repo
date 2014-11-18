namespace Dalworth.LeadCentral.Domain
{
    public class LeadAmountSummary : AmountSummary
    {
        public LeadAmountSummary()
        {
            IsInvoiced = false;
        }

        public int LeadId { get; set; }
        public string JobStatus { get; set; }
        public bool IsInvoiced { get; set; }
    }
}
