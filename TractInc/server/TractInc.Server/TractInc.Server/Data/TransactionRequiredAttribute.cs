using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace TractInc.Server.Data
{
    public class TransactionRequiredAttribute:Attribute
    {
        public TransactionRequiredAttribute()
        {
        }
    }
}
