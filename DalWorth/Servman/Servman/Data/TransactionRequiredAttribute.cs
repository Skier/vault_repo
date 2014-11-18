using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Servman.Data
{
    public class TransactionRequiredAttribute:Attribute
    {
        public TransactionRequiredAttribute()
        {
        }
    }
}
