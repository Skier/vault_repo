using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Data
{
    public class DataNotFoundException: DalworthException
    {
        public DataNotFoundException(string message):base(message)
        {
        }
    }

    public class DataOutdatedException : DalworthException
    {
        public DataOutdatedException(string message)
            : base(message)
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
