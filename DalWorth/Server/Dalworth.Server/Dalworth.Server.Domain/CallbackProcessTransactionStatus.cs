using System;
using System.Xml.Serialization;

namespace Dalworth.Server.Domain
{
    public enum CallbackProcessTransactionStatusEnum
    {
        VisitCreated = 1,
        NotIntrested = 2,
        DoNotCall = 3,
        LeftMessage = 4,
        Busy = 5,
        CallReschedule = 6,
        EmailSent= 7
    }

    public partial class CallbackProcessTransactionStatus
    {
        public CallbackProcessTransactionStatus(){}
    }
}
      