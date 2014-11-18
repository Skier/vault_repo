using System;
  
namespace Dalworth.Server.Domain
{
    public enum WorkTransactionTypeEnum
    {
        StartDayDone = 1,
        VisitCompleted = 2,
        VisitDeclined = 3,
        VisitAccepted = 4,
        VisitArrived = 5,
        Completed = 6,
        SubmitETC = 7,
        NoGo = 8,
        GPS = 9,
        VisitCompleteFailed = 10,
        VisitEquipmentTransfer = 11,
        VisitDispatched = 12
    }

    public partial class WorkTransactionType
    {
        public WorkTransactionType(){}
    }
}
      