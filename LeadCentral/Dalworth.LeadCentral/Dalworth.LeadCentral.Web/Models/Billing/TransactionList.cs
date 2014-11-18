using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web.Models.Billing
{
    public class TransactionList
    {
        public List<Transaction> Transactions { get; set; }
        public decimal CurrentBalance { get; set; }

        public TransactionList(){}

        public void Load(IDbConnection connection)
        {
            Transactions = BillingService.FIndTransactions(connection);
            CurrentBalance = BillingService.GetCurrentBalance(connection);
        }
    }
}