using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service.Flex
{
    class BillingService
    {
        public Transaction[] GetAllTransactions(string ticket)
        {
            return Service.BillingService.GetAllTransactions(ticket).ToArray();
        }

        public int GetTransactionsCount(string ticket)
        {
            return Service.BillingService.GetTransactionsCount(ticket);
        }

        public Transaction[] GetTransactions(string ticket, int offset, int limit)
        {
            return Service.BillingService.GetTransactions(ticket, offset, limit).ToArray();
        }
    
    }
}
