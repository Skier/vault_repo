using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Dalworth.Server.Data
{
    public class TransactionRequiredAttribute:Attribute
    {
        public TransactionRequiredAttribute()
        {
        }
    }
}
