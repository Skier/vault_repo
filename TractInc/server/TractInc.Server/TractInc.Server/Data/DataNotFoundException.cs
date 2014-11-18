using TractInc.Server.SDK;

namespace TractInc.Server.Data
{
    public class DataNotFoundException: TractIncException
    {
        public DataNotFoundException(string message):base(message)
        {
        }
    }


    public class TractIncSystemTransactionRequired : TractIncException
    {
        public TractIncSystemTransactionRequired()
            : base("System transaction context is required")
        { }
    }

}
