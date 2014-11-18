using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web.Models.Profile
{
    public class TransactionList
    {
        public List<Transaction> Transactions { get; set; }
        public decimal CurrentBalance { get; set; }
        public string BillingStatus { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public void Load(IDbConnection connection)
        {
            var filter = new TransactionFilter {CreatedFrom = DateFrom, CreatedTo = DateTo};
            Transactions = BillingService.FindTransactions(filter, connection);
            
            CurrentBalance = BillingService.GetCurrentBalance(connection);
            BillingStatus = BillingService.GetBillingStatus();
        }
    }
}