using System;
  
namespace Dalworth.Server.Domain
{
    public enum WorkTransactionTaskActionEnum
    {
        Complete = 1,
        InProcess = 2,
        FailMustReturn = 3,
        Cancel = 5,
        Generated = 6,
        Booked = 7
    }

    public partial class WorkTransactionTaskAction
    {
        public WorkTransactionTaskAction(){}
    }
}
      