using System;
using System.Collections.Generic;
using System.Text;

namespace QuickBooksAgent.Data
{
    public class DataNotFoundException: QuickBooksAgentException
    {
        public DataNotFoundException(string message):base(message)
        {
        }
    }


    public class QuickBooksAgentSystemTransactionRequired : QuickBooksAgentException
    {
        public QuickBooksAgentSystemTransactionRequired()
            : base("System transaction context is required")
        { }
    }

}
