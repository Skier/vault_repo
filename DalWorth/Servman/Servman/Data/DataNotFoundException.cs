using System;
using System.Collections.Generic;
using System.Text;
using Servman.SDK;

namespace Servman.Data
{
    public class DataNotFoundException: DalworthException
    {
        public DataNotFoundException(string message):base(message)
        {
        }
    }


    public class DalworthSystemTransactionRequired : DalworthException
    {
        public DalworthSystemTransactionRequired()
            : base("System transaction context is required")
        { }
    }

}
