using System;
using System.Collections.Generic;
using System.Text;

namespace TractInc.Expense.Entity
{

    public class ClientDataObject
    {

        public int ClientId;

        public string ClientName;

        public string ClientAddress;

        public bool Active;

        public bool Deleted;

        public List<DefaultInvoiceRateDataObject> DefaultRates;

    }

}
