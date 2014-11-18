using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Dalworth.LeadCentral.Data
{
    public class TransactionRequiredAttribute:Attribute
    {
        public TransactionRequiredAttribute()
        {
        }
    }
}
