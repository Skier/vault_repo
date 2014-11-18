using Dalworth.Common.SDK;

namespace Dalworth.Common.Data
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
